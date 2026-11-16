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

            binder.Bind(new BindContext(new OneWayBindableMember<IRelayCommand>(command), binder));
            binder.Unbind(new UnbindContext(binder));

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

            LogAssert.Expect(LogType.Error, new Regex("holds no Label named 'missing'"));
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

            binder.Bind(new BindContext(new OneWayBindableMember<System.Collections.Generic.IReadOnlyList<object>>(new List<object> { "a" }), binder));
            binder.Unbind(new UnbindContext(binder));

            Assert.IsNull(listView.itemsSource, "The source was not cleared on unbind");
        }

        [Test]
        public void WithoutADocument_TheBinderSaysSo()
        {
            var binder = Spawn<ElementLabelTextMonoBinder>("UIToolkit");

            LogAssert.Expect(LogType.Error, new Regex("no UIDocument is assigned"));
            binder.SetValue("value");
        }

        [TestCase(BindMode.TwoWay)]
        [TestCase(BindMode.OneWayToSource)]
        public void Slider_AllNumericReverseChannels(BindMode mode)
        {
            CheckNumericReverse<int>(mode, 3, 7);
            CheckNumericReverse<long>(mode, 3L, 7L);
            CheckNumericReverse<float>(mode, 3f, 7f);
            CheckNumericReverse<double>(mode, 3d, 7d);
        }

        [Test]
        public void RebindToSource_DoesNotReplayThePreviousViewModelValue()
        {
            var (document, slider) = NewDocument<Slider>("value");
            slider.highValue = 100;
            var binder = NewBinder<ElementSliderValueMonoBinder>(document, "value");
            binder.Bind(new BindContext(new OneWayBindableMember<float>(3f), binder));
            binder.Unbind(new UnbindContext(binder));
            slider.value = 8f;
            binder.Mode = BindMode.OneWayToSource;
            var received = new List<float>();
            binder.Bind(new BindContext(new TwoWayBindableMember<float>(0f, received.Add), binder));
            CollectionAssert.AreEqual(new[] { 8f }, received);
        }

        private void CheckNumericReverse<T>(BindMode mode, T initial, T expected)
        {
            var (document, slider) = NewDocument<Slider>("value");
            slider.highValue = 100;
            slider.value = 7;
            var binder = NewBinder<ElementSliderValueMonoBinder>(document, "value");
            binder.Mode = mode;
            var received = new List<T>();
            var member = new TwoWayBindableMember<T>(initial, received.Add);
            binder.Bind(new BindContext(member, binder));
            if (mode is BindMode.OneWayToSource)
                CollectionAssert.AreEqual(new[] { expected }, received);
            received.Clear();
            using (var change = ChangeEvent<float>.GetPooled(3, 7))
            {
                change.target = slider;
                slider.SendEvent(change);
            }
            CollectionAssert.AreEqual(new[] { expected }, received);
            binder.Unbind(new UnbindContext(binder));
            received.Clear();
            using (var change = ChangeEvent<float>.GetPooled(7, 8))
            {
                change.target = slider;
                slider.SendEvent(change);
            }
            Assert.IsEmpty(received);
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
