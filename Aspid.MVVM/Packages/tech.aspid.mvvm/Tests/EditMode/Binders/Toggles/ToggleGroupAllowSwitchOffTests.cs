using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="ToggleGroup.allowSwitchOff"/> binder.
    /// </summary>
    [TestFixture]
    public sealed class ToggleGroupAllowSwitchOffTests : SceneFixture
    {
        [Test]
        public void AllowSwitchOff_ReachesTheGroup()
        {
            var group = Spawn<ToggleGroup>("Group");
            var binder = group.gameObject.AddComponent<ToggleGroupAllowSwitchOffMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);
            Assert.IsTrue(group.allowSwitchOff, "Allowing switch-off did not reach the group");

            ((IBinder<bool>)binder).SetValue(false);
            Assert.IsFalse(group.allowSwitchOff, "Forbidding switch-off did not reach the group");
        }

        [Test]
        public void TheSerializableTwin_AcceptsItsTarget()
        {
            var group = Spawn<ToggleGroup>("Group");

            Assert.IsTrue(new ToggleGroupAllowSwitchOffBinder(group).CanBind);
        }
    }
}
