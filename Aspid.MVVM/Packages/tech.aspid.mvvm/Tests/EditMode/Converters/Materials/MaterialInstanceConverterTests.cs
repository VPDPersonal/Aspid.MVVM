using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="MaterialInstanceConverter"/> — the reused instance copy, the
    /// pass-through option, and the null path.
    /// </summary>
    [TestFixture]
    public sealed class MaterialInstanceConverterTests : SceneFixture
    {
        [Test]
        public void MaterialInstance_ReturnsACopyAndReusesIt()
        {
            var shader = Shader.Find("Unlit/Color") ?? Shader.Find("Sprites/Default");
            var material = Track(new Material(shader) { name = "Shared" });
            var converter = new MaterialInstanceConverter();

            var first = converter.Convert(material);

            Assert.AreNotSame(material, first);
            Assert.AreSame(first, converter.Convert(material));
            Assert.IsNotNull(first);
            Assert.IsTrue(first.name.Contains("Instance"));
        }

        [Test]
        public void MaterialInstance_CanBeTurnedOff()
        {
            var shader = Shader.Find("Unlit/Color") ?? Shader.Find("Sprites/Default");
            var material = Track(new Material(shader));

            Assert.AreSame(material, new MaterialInstanceConverter(instantiate: false).Convert(material));
        }

        [Test]
        public void MaterialInstance_NullPassesThrough() =>
            Assert.IsNull(new MaterialInstanceConverter().Convert(null));
    }
}
