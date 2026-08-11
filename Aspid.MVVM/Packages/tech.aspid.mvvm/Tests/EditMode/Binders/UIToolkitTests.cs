using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using Button = UnityEngine.UIElements.Button;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the UI Toolkit binders — the runtime layer that was uGUI and TextMeshPro from end to end.
    /// </summary>
    /// <remarks>
    /// A project on the stack Unity itself recommends could not use the framework at all. These pin the element lookup
    /// everything else stands on, and the behaviour of each leaf binder against a real visual tree.
    /// </remarks>
    [TestFixture]
    public sealed class UIToolkitTests
    {
        private readonly List<GameObject> _spawned = new();
        private readonly List<Object> _assets = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            foreach (var asset in _assets)
            {
                if (asset) Object.DestroyImmediate(asset);
            }

            _spawned.Clear();
            _assets.Clear();
        }

        [Test]
        public void LabelText_ReachesTheLabel()
        {
            var (document, label) = NewDocument<Label>("title");
            var binder = NewBinder<ElementLabelTextMonoBinder>(document, "title");

            binder.SetValue("Hello");

            Assert.AreEqual("Hello", label.text, "Текст не доехал до Label");
        }

        /// <summary>
        /// A label showing the word <c>null</c> is never what was meant.
        /// </summary>
        [Test]
        public void LabelText_ANullValueBecomesAnEmptyString()
        {
            var (document, label) = NewDocument<Label>("title");
            var binder = NewBinder<ElementLabelTextMonoBinder>(document, "title");

            binder.SetValue<string>(null);

            Assert.AreEqual(string.Empty, label.text, "Null записан в Label как слово");
        }

        /// <summary>
        /// A hidden element must take no space, which is what <see cref="DisplayStyle.None"/> means and what
        /// <see cref="VisualElement.visible"/> does not do.
        /// </summary>
        [Test]
        public void Display_HidesThroughTheDisplayStyle()
        {
            var (document, element) = NewDocument<VisualElement>("panel");
            var binder = NewBinder<ElementDisplayMonoBinder>(document, "panel");

            ((IBinder<bool>)binder).SetValue(false);
            Assert.AreEqual(DisplayStyle.None, element.style.display.value, "Элемент не скрыт через display");

            ((IBinder<bool>)binder).SetValue(true);
            Assert.AreEqual(DisplayStyle.Flex, element.style.display.value, "Элемент не показан обратно");
        }

        [Test]
        public void Enabled_ReachesTheElement()
        {
            var (document, element) = NewDocument<VisualElement>("panel");
            var binder = NewBinder<ElementEnabledMonoBinder>(document, "panel");

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(element.enabledSelf, "Элемент не выключен");
        }

        [Test]
        public void Class_IsAddedAndRemoved()
        {
            var (document, element) = NewDocument<VisualElement>("panel");
            var binder = NewBinder<ElementClassMonoBinder>(document, "panel");
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_class").stringValue = "selected";
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            ((IBinder<bool>)binder).SetValue(true);
            Assert.IsTrue(element.ClassListContains("selected"), "Класс не добавлен");

            ((IBinder<bool>)binder).SetValue(false);
            Assert.IsFalse(element.ClassListContains("selected"), "Класс не снят");
        }

        /// <summary>
        /// The button's enabled state has to follow the command, the way the uGUI binder drives <c>interactable</c>: a
        /// command that cannot run leaves a button that cannot be pressed.
        /// </summary>
        /// <remarks>
        /// The click itself is driven by Unity's own input pipeline, which an EditMode test has no panel to feed. What is
        /// pinned here is everything around it — the command is taken, its state reaches the button, and unbinding lets
        /// go.
        /// </remarks>
        [Test]
        public void ButtonCommand_FollowsCanExecute_AndLetsGoOnUnbind()
        {
            var (document, button) = NewDocument<Button>("go");
            var binder = NewBinder<ElementButtonCommandMonoBinder>(document, "go");

            var canExecute = true;
            // ReSharper disable once AccessToModifiedClosure
            var command = new RelayCommand(() => { }, () => canExecute);

            ((IBinder<IRelayCommand>)binder).SetValue(command);
            Assert.IsTrue(button.enabledSelf, "Кнопка выключена, хотя команда разрешает выполнение");

            canExecute = false;
            command.NotifyCanExecuteChanged();

            Assert.IsFalse(button.enabledSelf, "Кнопка осталась активной, хотя команда отказывает");

            binder.Bind(new OneWayBindableMember<IRelayCommand>(command));
            binder.Unbind();

            canExecute = true;
            Assert.DoesNotThrow(command.NotifyCanExecuteChanged, "Отвязанный биндер всё ещё слушает команду");
        }

        /// <summary>
        /// A document builds its tree in <c>OnEnable</c>, so a binder that resolved eagerly would search an empty root and
        /// the failure would look like a wrong name. The lookup is lazy — and a name that matches nothing is reported.
        /// </summary>
        [Test]
        public void AnElementThatIsNotThere_IsReported()
        {
            var (document, _) = NewDocument<Label>("title");
            var binder = NewBinder<ElementLabelTextMonoBinder>(document, "missing");

            LogAssert.Expect(LogType.Error, new Regex("No Label named 'missing'"));
            binder.SetValue("value");
        }

        [Test]
        public void WithoutADocument_TheBinderSaysSo()
        {
            var gameObject = NewGameObject();
            var binder = gameObject.AddComponent<ElementLabelTextMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("No UIDocument assigned"));
            binder.SetValue("value");
        }

        private (UIDocument Document, TElement Element) NewDocument<TElement>(string name)
            where TElement : VisualElement, new()
        {
            var gameObject = NewGameObject();
            var document = gameObject.AddComponent<UIDocument>();

            var panelSettings = ScriptableObject.CreateInstance<PanelSettings>();
            _assets.Add(panelSettings);

            document.panelSettings = panelSettings;

            var element = new TElement { name = name };
            document.rootVisualElement.Add(element);

            return (document, element);
        }

        private T NewBinder<T>(UIDocument document, string elementName)
            where T : MonoBinder
        {
            var binder = document.gameObject.AddComponent<T>();
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_document").objectReferenceValue = document;
            serializedObject.FindProperty("_elementName").stringValue = elementName;
            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("UIToolkit");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
