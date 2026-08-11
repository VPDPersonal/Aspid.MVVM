using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the canvas scaling and layout fitting binders: <see cref="CanvasScaler"/>,
    /// <see cref="GridLayoutGroup"/>, <see cref="ContentSizeFitter"/> and <see cref="AspectRatioFitter"/>.
    /// </summary>
    /// <remarks>
    /// These four components decide how a UI answers a screen it was not designed for, and none of them could be
    /// bound — while their immediate neighbours, <see cref="LayoutElement"/> and the horizontal and vertical layout
    /// groups, already could.
    /// </remarks>
    [TestFixture]
    public sealed class CanvasScalingAndFittingTests
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

        #region CanvasScaler
        [Test]
        public void UiScaleMode_ReachesTheScaler()
        {
            var scaler = New<CanvasScaler>();
            var binder = scaler.gameObject.AddComponent<CanvasScalerUiScaleModeMonoBinder>();

            ((IBinder<CanvasScaler.ScaleMode>)binder).SetValue(CanvasScaler.ScaleMode.ConstantPixelSize);

            Assert.AreEqual(CanvasScaler.ScaleMode.ConstantPixelSize, scaler.uiScaleMode, "Режим масштабирования не доехал");
        }

        [Test]
        public void ScaleFactor_ReachesTheScaler_AndIsClamped()
        {
            var scaler = New<CanvasScaler>();
            var binder = scaler.gameObject.AddComponent<CanvasScalerScaleFactorMonoBinder>();

            ((IBinder<float>)binder).SetValue(1.5f);
            Assert.AreEqual(1.5f, scaler.scaleFactor, 0.001f, "Масштаб не доехал до скейлера");

            ((IBinder<float>)binder).SetValue(-3f);
            Assert.AreEqual(0.01f, scaler.scaleFactor, 0.001f, "Отрицательный масштаб не поднят до минимума Unity");

            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(scaler.scaleFactor), "NaN дошёл до скейлера");
        }

        /// <summary>
        /// The scaler divides the screen size by this value, so zero would scale the canvas to infinity.
        /// </summary>
        [Test]
        public void ReferenceResolution_IsNeverBelowOne()
        {
            var scaler = New<CanvasScaler>();
            var binder = scaler.gameObject.AddComponent<CanvasScalerReferenceResolutionMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(1920f, 1080f));
            Assert.AreEqual(new Vector2(1920f, 1080f), scaler.referenceResolution, "Разрешение не доехало");

            ((IBinder<Vector2>)binder).SetValue(new Vector2(0f, float.NaN));
            Assert.AreEqual(new Vector2(1f, 1f), scaler.referenceResolution, "Нулевое или нефинитное разрешение не поднято до единицы");
        }

        [Test]
        public void MatchWidthOrHeight_IsClampedToTheDocumentedRange()
        {
            var scaler = New<CanvasScaler>();
            var binder = scaler.gameObject.AddComponent<CanvasScalerMatchWidthOrHeightMonoBinder>();

            ((IBinder<float>)binder).SetValue(2f);

            Assert.AreEqual(1f, scaler.matchWidthOrHeight, 0.001f, "Значение вне 0..1 не обрезано");
        }
        #endregion

        #region GridLayoutGroup
        [Test]
        public void CellSize_ReachesTheGrid_AndIsNeverNegative()
        {
            var grid = New<GridLayoutGroup>();
            var binder = grid.gameObject.AddComponent<GridLayoutGroupCellSizeMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(64f, 64f));
            Assert.AreEqual(new Vector2(64f, 64f), grid.cellSize, "Размер ячейки не доехал");

            ((IBinder<Vector2>)binder).SetValue(new Vector2(-10f, float.NaN));
            Assert.AreEqual(Vector2.zero, grid.cellSize, "Отрицательный или нефинитный размер не обрезан");
        }

        /// <summary>
        /// Negative spacing is a layout, not a mistake — overlapping cards are made that way. Only a non-finite
        /// value is refused.
        /// </summary>
        [Test]
        public void Spacing_KeepsNegativeValues_AndRefusesNonFiniteOnes()
        {
            var grid = New<GridLayoutGroup>();
            var binder = grid.gameObject.AddComponent<GridLayoutGroupSpacingMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(-20f, 5f));
            Assert.AreEqual(new Vector2(-20f, 5f), grid.spacing, "Отрицательный отступ не сохранён");

            ((IBinder<Vector2>)binder).SetValue(new Vector2(float.NaN, 5f));
            Assert.AreEqual(new Vector2(-20f, 5f), grid.spacing, "Нефинитный отступ дошёл до сетки");
        }

        [Test]
        public void ConstraintAndCount_ReachTheGrid()
        {
            var grid = New<GridLayoutGroup>();
            var constraint = grid.gameObject.AddComponent<GridLayoutGroupConstraintMonoBinder>();
            var count = grid.gameObject.AddComponent<GridLayoutGroupConstraintCountMonoBinder>();

            ((IBinder<GridLayoutGroup.Constraint>)constraint).SetValue(GridLayoutGroup.Constraint.FixedColumnCount);
            ((IBinder<int>)count).SetValue(4);

            Assert.AreEqual(GridLayoutGroup.Constraint.FixedColumnCount, grid.constraint, "Ограничение не доехало");
            Assert.AreEqual(4, grid.constraintCount, "Количество не доехало");
        }
        #endregion

        #region Fitters
        [Test]
        public void ContentSizeFitter_BothAxesAreBindable()
        {
            var fitter = New<ContentSizeFitter>();
            var horizontal = fitter.gameObject.AddComponent<ContentSizeFitterHorizontalFitMonoBinder>();
            var vertical = fitter.gameObject.AddComponent<ContentSizeFitterVerticalFitMonoBinder>();

            ((IBinder<ContentSizeFitter.FitMode>)horizontal).SetValue(ContentSizeFitter.FitMode.PreferredSize);
            ((IBinder<ContentSizeFitter.FitMode>)vertical).SetValue(ContentSizeFitter.FitMode.Unconstrained);

            Assert.AreEqual(ContentSizeFitter.FitMode.PreferredSize, fitter.horizontalFit, "Горизонтальный режим не доехал");
            Assert.AreEqual(ContentSizeFitter.FitMode.Unconstrained, fitter.verticalFit, "Вертикальный режим не доехал");
        }

        [Test]
        public void AspectRatioFitter_ModeAndRatioReachTheFitter()
        {
            var fitter = New<AspectRatioFitter>();
            var mode = fitter.gameObject.AddComponent<AspectRatioFitterAspectModeMonoBinder>();
            var ratio = fitter.gameObject.AddComponent<AspectRatioFitterAspectRatioMonoBinder>();

            ((IBinder<AspectRatioFitter.AspectMode>)mode).SetValue(AspectRatioFitter.AspectMode.WidthControlsHeight);
            ((IBinder<float>)ratio).SetValue(16f / 9f);

            Assert.AreEqual(AspectRatioFitter.AspectMode.WidthControlsHeight, fitter.aspectMode, "Режим не доехал");
            Assert.AreEqual(16f / 9f, fitter.aspectRatio, 0.001f, "Соотношение не доехало");
        }

        /// <summary>
        /// Unity clamps the ratio with comparisons, and every comparison against <c>NaN</c> is false — so the
        /// binder has to refuse it before the clamp does not.
        /// </summary>
        [Test]
        public void AspectRatio_RefusesANonFiniteValue()
        {
            var fitter = New<AspectRatioFitter>();
            // Пока режим None, фиттер вне play mode пересчитывает соотношение из текущего rect,
            // и записанное значение не выживает — это документировано в самом биндере.
            fitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;

            var binder = fitter.gameObject.AddComponent<AspectRatioFitterAspectRatioMonoBinder>();

            ((IBinder<float>)binder).SetValue(2f);
            ((IBinder<float>)binder).SetValue(float.NaN);

            Assert.AreEqual(2f, fitter.aspectRatio, 0.001f, "NaN дошёл до фиттера");
        }
        #endregion

        #region The serializable twins
        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var scaler = New<CanvasScaler>();
            var grid = New<GridLayoutGroup>();
            var sizeFitter = New<ContentSizeFitter>();
            var aspectFitter = New<AspectRatioFitter>();

            Assert.IsTrue(new CanvasScalerUiScaleModeBinder(scaler).IsBind);
            Assert.IsTrue(new CanvasScalerScaleFactorBinder(scaler).IsBind);
            Assert.IsTrue(new CanvasScalerReferenceResolutionBinder(scaler).IsBind);
            Assert.IsTrue(new CanvasScalerMatchWidthOrHeightBinder(scaler).IsBind);
            Assert.IsTrue(new GridLayoutGroupCellSizeBinder(grid).IsBind);
            Assert.IsTrue(new GridLayoutGroupSpacingBinder(grid).IsBind);
            Assert.IsTrue(new GridLayoutGroupConstraintBinder(grid).IsBind);
            Assert.IsTrue(new GridLayoutGroupConstraintCountBinder(grid).IsBind);
            Assert.IsTrue(new ContentSizeFitterHorizontalFitBinder(sizeFitter).IsBind);
            Assert.IsTrue(new ContentSizeFitterVerticalFitBinder(sizeFitter).IsBind);
            Assert.IsTrue(new AspectRatioFitterAspectModeBinder(aspectFitter).IsBind);
            Assert.IsTrue(new AspectRatioFitterAspectRatioBinder(aspectFitter).IsBind);
        }

        /// <summary>
        /// The enum binders reject <see cref="BindMode.TwoWay"/> in their base: none of these properties raises a
        /// change event, so the mode would be a channel that never delivers.
        /// </summary>
        [Test]
        public void TheEnumBinders_RefuseTwoWay()
        {
            var scaler = New<CanvasScaler>();

            Assert.Throws<System.ArgumentException>(
                () => _ = new CanvasScalerUiScaleModeBinder(scaler, BindMode.TwoWay),
                "TwoWay принят режимом, в котором обратный канал невозможен");
        }
        #endregion

        private T New<T>()
            where T : Component
        {
            var gameObject = new GameObject(typeof(T).Name);
            _spawned.Add(gameObject);

            return gameObject.AddComponent<T>();
        }
    }
}
