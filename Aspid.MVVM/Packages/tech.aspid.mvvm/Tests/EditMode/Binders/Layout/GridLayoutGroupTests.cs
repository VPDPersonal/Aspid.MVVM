using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="GridLayoutGroup"/> binders.
    /// </summary>
    [TestFixture]
    public sealed class GridLayoutGroupTests : SceneFixture
    {
        [Test]
        public void CellSize_ReachesTheGrid_AndIsNeverNegative()
        {
            var grid = Spawn<GridLayoutGroup>("GridLayoutGroup");
            var binder = grid.gameObject.AddComponent<GridLayoutGroupCellSizeMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(64f, 64f));
            Assert.AreEqual(new Vector2(64f, 64f), grid.cellSize, "The cell size did not reach the grid");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)binder).SetValue(new Vector2(-10f, float.NaN));
            Assert.AreEqual(Vector2.zero, grid.cellSize, "A negative or non-finite size was not clamped");
        }

        /// <summary>
        /// Negative spacing is a layout, not a mistake — overlapping cards are made that way. Only a non-finite
        /// value is refused.
        /// </summary>
        [Test]
        public void Spacing_KeepsNegativeValues_AndRefusesNonFiniteOnes()
        {
            var grid = Spawn<GridLayoutGroup>("GridLayoutGroup");
            var binder = grid.gameObject.AddComponent<GridLayoutGroupSpacingMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(-20f, 5f));
            Assert.AreEqual(new Vector2(-20f, 5f), grid.spacing, "The negative spacing was not kept");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)binder).SetValue(new Vector2(float.NaN, 5f));
            Assert.AreEqual(new Vector2(-20f, 5f), grid.spacing, "A non-finite spacing reached the grid");
        }

        [Test]
        public void ConstraintAndCount_ReachTheGrid()
        {
            var grid = Spawn<GridLayoutGroup>("GridLayoutGroup");
            var constraint = grid.gameObject.AddComponent<GridLayoutGroupConstraintMonoBinder>();
            var count = grid.gameObject.AddComponent<GridLayoutGroupConstraintCountMonoBinder>();

            ((IBinder<GridLayoutGroup.Constraint>)constraint).SetValue(GridLayoutGroup.Constraint.FixedColumnCount);
            ((IBinder<int>)count).SetValue(4);

            Assert.AreEqual(GridLayoutGroup.Constraint.FixedColumnCount, grid.constraint, "The constraint did not reach the grid");
            Assert.AreEqual(4, grid.constraintCount, "The count did not reach the grid");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var grid = Spawn<GridLayoutGroup>("GridLayoutGroup");

            Assert.IsTrue(new GridLayoutGroupCellSizeBinder(grid).CanBind);
            Assert.IsTrue(new GridLayoutGroupSpacingBinder(grid).CanBind);
            Assert.IsTrue(new GridLayoutGroupConstraintBinder(grid).CanBind);
            Assert.IsTrue(new GridLayoutGroupConstraintCountBinder(grid).CanBind);
        }
    }
}
