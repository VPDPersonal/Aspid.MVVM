using System.Reflection;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Aspid.MVVM.Tests
{
    [Category("AuditP2")]
    public sealed class ConverterNullTests : SceneFixture
    {
        [TestCase(false)]
        [TestCase(true)]
        public void TextConverterNull_ClearsInput(bool serializable)
        {
            var field = Spawn<TMP_InputField>("Input");
            field.text = "original";
            if (serializable)
                new InputFieldBinder(field, converter: new NullText()).SetValue("secret");
            else
            {
                var binder = field.gameObject.AddComponent<InputFieldMonoBinder>();
                Set(binder, "_converter", new NullText());
                binder.SetValue("secret");
            }
            Assert.IsTrue(string.IsNullOrEmpty(field.text));
        }

        [Test]
        public void EventConverterNull_DoesNotRestoreInput()
        {
            var binder = Spawn<UnityEventStringMonoBinder>("Event");
            var evt = new UnityEvent<string>();
            var received = "unset";
            evt.AddListener(value => received = value);
            Set(binder, "_set", evt);
            Set(binder, "_converter", new NullText());
            binder.SetValue("secret");
            Assert.IsNull(received);
            Set(binder, "_converter", null);
            binder.SetValue("original");
            Assert.AreEqual("original", received);
        }

        [Test]
        public void MaterialConverterNull_ClearsCollectionSlot()
        {
            var renderer = Spawn<MeshRenderer>("Renderer");
            var material = Track(new Material(Shader.Find("Hidden/InternalErrorShader")));
            renderer.SetMaterials(new NullMaterial(), new[] { material });
            Assert.IsNull(renderer.sharedMaterials[0]);
            renderer.SetMaterials(null, new[] { material });
            Assert.AreSame(material, renderer.sharedMaterials[0]);
        }

        private static void Set(object instance, string name, object value) =>
            instance.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(instance, value);

        private sealed class NullText : IConverter<string, string>
        {
            public string Convert(string value) => null;
        }

        private sealed class NullMaterial : IConverter<Material, Material>
        {
            public Material Convert(Material value) => null;
        }
    }
}
