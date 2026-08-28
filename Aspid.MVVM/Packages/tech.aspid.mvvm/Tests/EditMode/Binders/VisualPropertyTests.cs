using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.Rendering;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the visual properties of components the package already bound in part: <see cref="Image"/>'s type and
    /// fill options, <see cref="RawImage.uvRect"/>, <see cref="LineRenderer"/>'s width and loop,
    /// <see cref="Renderer"/>'s sorting and shadows, and <see cref="Selectable"/>'s transition and target graphic.
    /// </summary>
    [TestFixture]
    public sealed class VisualPropertyTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void TheImageOptions_ReachTheImage()
        {
            var image = New<Image>();

            ((IBinder<Image.Type>)image.gameObject.AddComponent<ImageTypeMonoBinder>()).SetValue(Image.Type.Filled);
            ((IBinder<bool>)image.gameObject.AddComponent<ImagePreserveAspectMonoBinder>()).SetValue(true);
            ((IBinder<int>)image.gameObject.AddComponent<ImageFillOriginMonoBinder>()).SetValue(2);
            ((IBinder<bool>)image.gameObject.AddComponent<ImageFillClockwiseMonoBinder>()).SetValue(false);

            Assert.AreEqual(Image.Type.Filled, image.type, "Тип изображения не доехал");
            Assert.IsTrue(image.preserveAspect, "preserveAspect не доехал");
            Assert.AreEqual(2, image.fillOrigin, "fillOrigin не доехал");
            Assert.IsFalse(image.fillClockwise, "fillClockwise не доехал");
        }

        [Test]
        public void UvRect_ReachesTheRawImage_AndRefusesANonFiniteComponent()
        {
            var raw = New<RawImage>();
            var binder = raw.gameObject.AddComponent<RawImageUvRectMonoBinder>();

            ((IBinder<Rect>)binder).SetValue(new Rect(0f, 0.5f, 2f, 2f));
            Assert.AreEqual(new Rect(0f, 0.5f, 2f, 2f), raw.uvRect, "UV-прямоугольник не доехал");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Rect>)binder).SetValue(new Rect(0f, float.NaN, 1f, 1f));
            Assert.AreEqual(new Rect(0f, 0.5f, 2f, 2f), raw.uvRect, "Нефинитная компонента дошла до RawImage");
        }

        [Test]
        public void TheLineRendererOptions_ReachTheRenderer()
        {
            var line = New<LineRenderer>();

            ((IBinder<float>)line.gameObject.AddComponent<LineRendererWidthMultiplierMonoBinder>()).SetValue(3f);
            ((IBinder<bool>)line.gameObject.AddComponent<LineRendererLoopMonoBinder>()).SetValue(true);

            Assert.AreEqual(3f, line.widthMultiplier, 0.001f, "Множитель ширины не доехал");
            Assert.IsTrue(line.loop, "Замыкание линии не доехало");
        }

        [Test]
        public void SortingOrderAndShadows_ReachAnyRenderer()
        {
            var renderer = New<MeshRenderer>();

            ((IBinder<int>)renderer.gameObject.AddComponent<RendererSortingOrderMonoBinder>()).SetValue(7);
            ((IBinder<ShadowCastingMode>)renderer.gameObject.AddComponent<RendererShadowCastingMonoBinder>()).SetValue(ShadowCastingMode.Off);

            Assert.AreEqual(7, renderer.sortingOrder, "Порядок сортировки не доехал");
            Assert.AreEqual(ShadowCastingMode.Off, renderer.shadowCastingMode, "Режим теней не доехал");
        }

        /// <summary>
        /// Unity ignores a sorting layer name no layer has and leaves the object where it was, which looks exactly like
        /// a depth bug — so the binder reports it instead.
        /// </summary>
        [Test]
        public void ASortingLayerThatDoesNotExist_IsReported()
        {
            var renderer = New<MeshRenderer>();
            var binder = renderer.gameObject.AddComponent<RendererSortingLayerNameMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("No sorting layer named"));
            ((IBinder<string>)binder).SetValue("NoSuchLayer");

            Assert.AreEqual("Default", renderer.sortingLayerName, "Несуществующий слой всё же записался");
        }

        [Test]
        public void TheDefaultSortingLayer_IsAccepted()
        {
            var renderer = New<MeshRenderer>();
            var binder = renderer.gameObject.AddComponent<RendererSortingLayerNameMonoBinder>();

            Assert.DoesNotThrow(() => ((IBinder<string>)binder).SetValue("Default"));
            Assert.AreEqual("Default", renderer.sortingLayerName, "Слой Default не принят");
        }

        [Test]
        public void TheSelectableOptions_ReachTheControl()
        {
            var button = New<Button>();
            var graphic = New<Image>();

            ((IBinder<Selectable.Transition>)button.gameObject.AddComponent<SelectableTransitionMonoBinder>()).SetValue(Selectable.Transition.None);
            ((IBinder<Graphic>)button.gameObject.AddComponent<SelectableTargetGraphicMonoBinder>()).SetValue(graphic);

            Assert.AreEqual(Selectable.Transition.None, button.transition, "Режим перехода не доехал");
            Assert.AreSame(graphic, button.targetGraphic, "Целевой graphic не доехал");
        }

        [Test]
        public void ADestroyedTargetGraphic_ArrivesAsNull()
        {
            var button = New<Button>();
            var graphic = New<Image>();
            var binder = button.gameObject.AddComponent<SelectableTargetGraphicMonoBinder>();

            ((IBinder<Graphic>)binder).SetValue(graphic);
            Object.DestroyImmediate(graphic);
            ((IBinder<Graphic>)binder).SetValue(graphic);

            Assert.IsFalse(button.targetGraphic, "Уничтоженный graphic остался живым для Unity");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var image = New<Image>();
            var raw = New<RawImage>();
            var line = New<LineRenderer>();
            var mesh = New<MeshRenderer>();
            var button = New<Button>();

            Assert.IsTrue(new ImageTypeBinder(image).IsBind);
            Assert.IsTrue(new ImagePreserveAspectBinder(image).IsBind);
            Assert.IsTrue(new ImageFillOriginBinder(image).IsBind);
            Assert.IsTrue(new ImageFillClockwiseBinder(image).IsBind);
            Assert.IsTrue(new RawImageUvRectBinder(raw).IsBind);
            Assert.IsTrue(new LineRendererWidthMultiplierBinder(line).IsBind);
            Assert.IsTrue(new LineRendererLoopBinder(line).IsBind);
            Assert.IsTrue(new RendererSortingOrderBinder(mesh).IsBind);
            Assert.IsTrue(new RendererSortingLayerNameBinder(mesh).IsBind);
            Assert.IsTrue(new RendererShadowCastingBinder(mesh).IsBind);
            Assert.IsTrue(new SelectableTransitionBinder(button).IsBind);
            Assert.IsTrue(new SelectableTargetGraphicBinder(button).IsBind);
        }

        private T New<T>()
            where T : Component
        {
            var gameObject = new GameObject(typeof(T).Name);
            _spawned.Add(gameObject);

            return gameObject.AddComponent<T>();
        }
    }
}
