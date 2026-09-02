using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="MaterialPropertyBlock"/> binders — the path to a shader value that does not instantiate
    /// a material per object.
    /// </summary>
    [TestFixture]
    public sealed class PropertyBlockTests : SceneFixture
    {
        [Test]
        public void AFloat_ReachesTheBlock()
        {
            var (renderer, binder) = New<RendererPropertyBlockFloatMonoBinder>("_Cutoff");

            binder.Bind(new OneWayStructBindableMember<float>(0f));
            ((IBinder<float>)binder).SetValue(0.25f);

            var block = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(block);

            Assert.AreEqual(0.25f, block.GetFloat("_Cutoff"), 0.001f, "The value did not reach the property block");
        }

        [Test]
        public void AColor_ReachesTheBlock()
        {
            var (renderer, binder) = New<RendererPropertyBlockColorMonoBinder>("_Tint");

            binder.Bind(new OneWayStructBindableMember<Color>(Color.white));
            ((IBinder<Color>)binder).SetValue(Color.red);

            var block = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(block);

            Assert.AreEqual(Color.red, block.GetColor("_Tint"), "The color did not reach the property block");
        }

        /// <summary>
        /// Shader vectors are always four components, so a <see cref="Vector2"/> arrives with the rest at zero.
        /// </summary>
        [Test]
        public void AVector2_ArrivesWithTheRestAtZero()
        {
            var (renderer, binder) = New<RendererPropertyBlockVectorMonoBinder>("_Offset");

            binder.Bind(new OneWayStructBindableMember<Vector4>(Vector4.zero));
            ((IBinder<Vector2>)binder).SetValue(new Vector2(1f, 2f));

            var block = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(block);

            Assert.AreEqual(new Vector4(1f, 2f, 0f, 0f), block.GetVector("_Offset"), "The vector did not reach the property block");
        }

        /// <summary>
        /// The renderer is left alone until the property name is filled in, and the omission is reported once at binding
        /// time rather than on every value.
        /// </summary>
        [Test]
        public void ABlankPropertyName_IsReportedOnce()
        {
            var (_, binder) = New<RendererPropertyBlockFloatMonoBinder>(string.Empty);

            LogAssert.Expect(LogType.Error, new Regex("No shader property name set"));
            binder.Bind(new OneWayStructBindableMember<float>(0f));

            Assert.DoesNotThrow(() => ((IBinder<float>)binder).SetValue(1f), "A value without a property name threw");
            Assert.DoesNotThrow(() => ((IBinder<float>)binder).SetValue(2f), "A second value logged a second error");
        }

        private (Renderer Renderer, T Binder) New<T>(string propertyName)
            where T : MonoBinder
        {
            var renderer = Spawn("PropertyBlock").AddComponent<MeshRenderer>();
            var binder = renderer.gameObject.AddComponent<T>();

            var serializedObject = new UnityEditor.SerializedObject(binder);
            serializedObject.FindProperty("_propertyName").stringValue = propertyName;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return (renderer, binder);
        }
    }
}
