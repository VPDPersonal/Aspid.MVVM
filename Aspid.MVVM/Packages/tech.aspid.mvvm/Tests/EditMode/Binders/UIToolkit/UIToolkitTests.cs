using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Button = UnityEngine.UIElements.Button;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the UI Toolkit binders.
    /// </summary>
    [TestFixture]
    public sealed class UIToolkitTests : SceneFixture
    {
        [Test]
        public void LabelText_ReachesTheLabel()
        {
            var (document, label) = NewDocument<Label>("title");
            var binder = NewBinder<ElementLabelTextMonoBinder>(document, "title");

            binder.SetValue("Hello");

            Assert.AreEqual("Hello", label.text, "The text did not reach the Label");
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

            Assert.AreEqual(string.Empty, label.text, "Null was written to the Label as a word");
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
            Assert.AreEqual(DisplayStyle.None, element.style.display.value, "The element was not hidden through display");

            ((IBinder<bool>)binder).SetValue(true);
            Assert.AreEqual(DisplayStyle.Flex, element.style.display.value, "The element was not shown back");
        }

        [Test]
        public void Enabled_ReachesTheElement()
        {
            var (document, element) = NewDocument<VisualElement>("panel");
            var binder = NewBinder<ElementEnabledMonoBinder>(document, "panel");

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(element.enabledSelf, "The element was not disabled");
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
            Assert.IsTrue(element.ClassListContains("selected"), "The class was not added");

            ((IBinder<bool>)binder).SetValue(false);
            Assert.IsFalse(element.ClassListContains("selected"), "The class was not removed");
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
            Assert.IsTrue(button.enabledSelf, "The button is disabled although the command allows execution");

            canExecute = false;
            command.NotifyCanExecuteChanged();

            Assert.IsFalse(button.enabledSelf, "The button stayed enabled although the command refuses");

            binder.Bind(new OneWayBindableMember<IRelayCommand>(command));
            binder.Unbind();

            canExecute = true;
            Assert.DoesNotThrow(command.NotifyCanExecuteChanged, "The unbound binder still listens to the command");
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

        /// <summary>
        /// A ListView owns its recycling, so the binder only hands it a source and tells it what changed — and the source
        /// it takes is a mutable <see cref="System.Collections.IList"/>, which a ViewModel's collection is not.
        /// </summary>
        [Test]
        public void ListViewItemsSource_WrapsAReadOnlyCollectionWithoutCopyingIt()
        {
            var (document, listView) = NewDocument<ListView>("items");
            var binder = NewBinder<ElementListViewItemsSourceMonoBinder>(document, "items");

            var items = new List<object> { "a", "b", "c" };
            ((IBinder<System.Collections.Generic.IReadOnlyList<object>>)binder).SetValue(items);

            Assert.AreEqual(3, listView.itemsSource.Count, "The source did not reach the ListView");
            Assert.AreEqual("b", listView.itemsSource[1], "The source items do not match the collection");

            Assert.Throws<System.NotSupportedException>(() => listView.itemsSource.Add("d"),
                "The source allowed writing into the ViewModel's collection");
        }

        /// <summary>
        /// A recycled panel must not show the previous ViewModel's items for a frame.
        /// </summary>
        [Test]
        public void ListViewItemsSource_IsClearedOnUnbind()
        {
            var (document, listView) = NewDocument<ListView>("items");
            var binder = NewBinder<ElementListViewItemsSourceMonoBinder>(document, "items");

            binder.Bind(new OneWayBindableMember<System.Collections.Generic.IReadOnlyList<object>>(new List<object> { "a" }));
            binder.Unbind();

            Assert.IsNull(listView.itemsSource, "The source was not cleared on unbind");
        }

        [Test]
        public void WithoutADocument_TheBinderSaysSo()
        {
            var binder = Spawn<ElementLabelTextMonoBinder>("UIToolkit");

            LogAssert.Expect(LogType.Error, new Regex("No UIDocument assigned"));
            binder.SetValue("value");
        }

        private (UIDocument Document, TElement Element) NewDocument<TElement>(string name)
            where TElement : VisualElement, new()
        {
            var document = Spawn<UIDocument>("UIToolkit");
            document.panelSettings = Track(ScriptableObject.CreateInstance<PanelSettings>());

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
    }
}
