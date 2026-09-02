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
    /// Tests for the <see cref="RectMask2D.padding"/> binder.
    /// </summary>
    [TestFixture]
    public sealed class RectMask2DPaddingTests : SceneFixture
    {
        /// <summary>
        /// The padding is a <see cref="Vector4"/> and the bound value is a <see cref="Vector3"/>, so the fourth side must
        /// keep what it had — otherwise binding three sides silently zeroes the fourth.
        /// </summary>
        [Test]
        public void MaskPadding_KeepsTheFourthSide()
        {
            var mask = Spawn<RectMask2D>("Rect");
            mask.padding = new Vector4(1f, 2f, 3f, 4f);

            var binder = mask.gameObject.AddComponent<RectMask2DPaddingMonoBinder>();
            ((IBinder<Vector3>)binder).SetValue(new Vector3(5f, 6f, 7f));

            Assert.AreEqual(new Vector4(5f, 6f, 7f, 4f), mask.padding, "The fourth side was not kept");
        }

        [Test]
        public void MaskPadding_RefusesANonFiniteComponent()
        {
            var mask = Spawn<RectMask2D>("Rect");
            mask.padding = new Vector4(1f, 1f, 1f, 1f);

            var binder = mask.gameObject.AddComponent<RectMask2DPaddingMonoBinder>();
            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector3>)binder).SetValue(new Vector3(2f, float.NaN, 2f));

            Assert.AreEqual(new Vector4(1f, 1f, 1f, 1f), mask.padding, "A non-finite component reached the mask");
        }
    }
}
