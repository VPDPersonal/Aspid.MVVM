using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace Aspid.MVVM.Tests
{
    [Category("AuditP1")]
    public sealed class VisualTreeLifetimeTests : SceneFixture
    {
        [UnityTest]
        public IEnumerator RebuiltTree_RestoresOneTimeAndReverseChannels()
        {
            var document = Spawn<UIDocument>("Document");
            document.panelSettings = Track(ScriptableObject.CreateInstance<PanelSettings>());
            var root = document.rootVisualElement;
            root.Add(new Label { name = "label" });
            root.Add(new Slider(0, 100) { name = "slider" });
            root.Add(new TextField { name = "text" });
            var label = Add<ElementLabelTextMonoBinder, Label>(document, "label", BindMode.OneTime);
            var slider = Add<ElementSliderValueMonoBinder, Slider>(document, "slider", BindMode.TwoWay);
            var text = Add<ElementTextFieldValueMonoBinder, TextField>(document, "text", BindMode.TwoWay);
            var numbers = new List<int>();
            var strings = new List<string>();
            label.Bind(new BindContext(new OneTimeBindableMember<string>("Retained"), label));
            slider.Bind(new BindContext(new TwoWayBindableMember<int>(3, numbers.Add), slider));
            text.Bind(new BindContext(new TwoWayBindableMember<string>("Initial", strings.Add), text));
            for (var cycle = 0; cycle < 5; cycle++)
            {
                var oldSlider = root.Q<Slider>("slider");
                document.enabled = false;
                yield return null;
                document.enabled = true;
                root = document.rootVisualElement;
                root.Add(new Label { name = "label" });
                root.Add(new Slider(0, 100) { name = "slider" });
                root.Add(new TextField { name = "text" });
                yield return null;
                yield return null;
                Assert.AreEqual("Retained", root.Q<Label>("label").text);
                Assert.AreEqual(3f, root.Q<Slider>("slider").value);
                Assert.AreEqual("Initial", root.Q<TextField>("text").value);
                numbers.Clear();
                strings.Clear();
                oldSlider.value = 99;
                root.Q<Slider>("slider").value = 7;
                root.Q<TextField>("text").value = "Changed";
                CollectionAssert.AreEqual(new[] { 7 }, numbers);
                CollectionAssert.AreEqual(new[] { "Changed" }, strings);
            }
            slider.Unbind(new UnbindContext(slider));
            numbers.Clear();
            root.Q<Slider>("slider").value = 8;
            Assert.IsEmpty(numbers);
        }

        [UnityTest]
        public IEnumerator LateTree_RestoresOneTimeValue()
        {
            var document = Spawn<UIDocument>("Late Document");
            document.panelSettings = Track(ScriptableObject.CreateInstance<PanelSettings>());
            document.enabled = false;
            var binder = Add<ElementLabelTextMonoBinder, Label>(document, "label", BindMode.OneTime);
            binder.Bind(new BindContext(new OneTimeBindableMember<string>("Late"), binder));
            yield return null;
            document.enabled = true;
            var label = new Label { name = "label" };
            document.rootVisualElement.Add(label);
            yield return null;
            yield return null;
            Assert.AreEqual("Late", label.text);
        }

        [UnityTest]
        public IEnumerator ReplacedAsset_RestoresButtonCommand()
        {
            var document = Spawn<UIDocument>("Button Document");
            document.panelSettings = Track(ScriptableObject.CreateInstance<PanelSettings>());
            var button = new Button { name = "button" };
            document.rootVisualElement.Add(button);
            var binder = Add<ElementButtonCommandMonoBinder, Button>(document, "button", BindMode.OneTime);
            var clicks = 0;
            binder.Bind(new BindContext(new OneTimeBindableMember<IRelayCommand>(new RelayCommand(() => clicks++)), binder));
            document.visualTreeAsset = Track(ScriptableObject.CreateInstance<VisualTreeAsset>());
            var replacement = new Button { name = "button" };
            document.rootVisualElement.Add(replacement);
            yield return null;
            yield return null;
            using (var submit = NavigationSubmitEvent.GetPooled())
            {
                submit.target = replacement;
                replacement.SendEvent(submit);
            }
            Assert.AreEqual(1, clicks);
            using (var submit = NavigationSubmitEvent.GetPooled())
            {
                submit.target = button;
                button.SendEvent(submit);
            }
            Assert.AreEqual(1, clicks);
            binder.Unbind(new UnbindContext(binder));
        }

        private T Add<T, TElement>(UIDocument document, string name, BindMode mode)
            where T : VisualElementMonoBinder<TElement>
            where TElement : VisualElement
        {
            var binder = document.gameObject.AddComponent<T>();
            typeof(VisualElementMonoBinder<TElement>).GetField("_document", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(binder, document);
            typeof(VisualElementMonoBinder<TElement>).GetField("_elementName", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(binder, name);
            binder.Mode = mode;
            return binder;
        }
    }
}
