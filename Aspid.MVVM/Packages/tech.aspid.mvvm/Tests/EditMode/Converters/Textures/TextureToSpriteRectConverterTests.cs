using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="TextureToSpriteRectConverter"/>, including the destroyed-texture and
    /// render-texture corners the docs commit to.
    /// </summary>
    [TestFixture]
    public sealed class TextureToSpriteRectConverterTests : SceneFixture
    {
        [Test]
        public void Texture_MeasuresTheWholePixelRect() =>
            Assert.AreEqual(new Rect(0f, 0f, 8f, 4f), new TextureToSpriteRectConverter().Convert(NewTexture(8, 4)));

        [Test]
        public void Null_ReturnsZero() =>
            Assert.AreEqual(Rect.zero, new TextureToSpriteRectConverter().Convert(null));

        // The case the null check exists for. An asset unloaded under a bound RawImage leaves a live
        // managed reference behind, so `is null` and `??` both wave it through and the width read
        // throws inside the binder's push. Only Unity's overloaded == catches it.
        [Test]
        public void DestroyedTexture_ReturnsZeroRatherThanThrowing()
        {
            var texture = NewTexture(8, 4);
            var converter = new TextureToSpriteRectConverter();

            // While it is alive it has to measure, or the zero below would prove nothing.
            Assert.AreEqual(new Rect(0f, 0f, 8f, 4f), converter.Convert(texture));

            Destroy(texture);

            Assert.AreEqual(Rect.zero, converter.Convert(texture));
        }

        // Typed on Texture rather than Texture2D so a render target measures the same way — the
        // reason a RawImage-facing ViewModel can hold the base type.
        [Test]
        public void RenderTexture_MeasuresTheSameWay()
        {
            var texture = Track(new RenderTexture(16, 8, 0));

            Assert.AreEqual(new Rect(0f, 0f, 16f, 8f), new TextureToSpriteRectConverter().Convert(texture));
        }

        // The docs promise a Texture2D-typed binder still takes it. That holds only while IConverter
        // keeps its `in` on the input, so this assignment is the real assertion — losing the variance
        // annotation breaks the compile here rather than in a project's binder.
        [Test]
        public void AssignedToATexture2DTypedConverter_StillMeasures()
        {
            IConverter<Texture2D, Rect> converter = new TextureToSpriteRectConverter();

            Assert.AreEqual(new Rect(0f, 0f, 2f, 2f), converter.Convert(NewTexture(2, 2)));
        }

        private Texture2D NewTexture(int width, int height) =>
            Track(new Texture2D(width, height));
    }
}
