using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Aspid.MVVM.Tests
{
    [Category("AuditP1")]
    public sealed class MaterialOwnershipTests : SceneFixture
    {
        [UnityTest]
        public IEnumerator MaterialSlots_SurviveAFrameAndReleaseWithTheirOwner()
        {
            var renderer = Spawn<MeshRenderer>("Materials");
            var binder = renderer.gameObject.AddComponent<RendererMaterialsMonoBinder>();
            var source = Track(new Material(Shader.Find("Sprites/Default")));
            var second = Track(new Material(source));
            typeof(RendererMaterialsMonoBinder).GetField("_converter", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(binder, new MaterialInstanceConverter());
            binder.SetValue((IReadOnlyCollection<Material>)new[] { source, second });
            var copies = renderer.sharedMaterials;
            yield return null;
            Assert.IsTrue(copies[0] && copies[1]);
            Assert.AreNotSame(copies[0], copies[1]);
            binder.SetValue((IReadOnlyCollection<Material>)new[] { source, second });
            CollectionAssert.AreEqual(copies, renderer.sharedMaterials);
            Object.Destroy(binder);
            yield return null;
            yield return null;
            Assert.IsFalse(copies[0]);
            Assert.IsFalse(copies[1]);
            Assert.IsTrue(source && second);
        }
    }
}
