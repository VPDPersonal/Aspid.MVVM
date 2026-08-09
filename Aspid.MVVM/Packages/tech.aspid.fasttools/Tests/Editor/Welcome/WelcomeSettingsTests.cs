using NUnit.Framework;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

namespace Aspid.FastTools.Editors.Tests
{
    /// <summary>
    /// Behavioural coverage for the Welcome tab's per-user auto-show setting: the store defaults to on, raises
    /// <c>Changed</c> only on a real change, resets to its default, and the switch built by
    /// <see cref="WelcomeSettingsUI"/> mirrors the store live (the same live-sync contract as the other settings
    /// sections).
    /// </summary>
    [TestFixture]
    internal sealed class WelcomeSettingsTests
    {
        private bool _autoShow;

        [SetUp]
        public void SetUp() =>
            _autoShow = WelcomeSettings.AutoShowEnabled;

        [TearDown]
        public void TearDown() =>
            WelcomeSettings.AutoShowEnabled = _autoShow;

        [Test]
        public void Setter_RaisesChanged_OnlyOnRealChange()
        {
            WelcomeSettings.AutoShowEnabled = true;

            var fired = 0;

            WelcomeSettings.Changed += Handler;
            try
            {
                WelcomeSettings.AutoShowEnabled = true;
                Assert.AreEqual(0, fired, "Re-assigning the same value must not raise Changed.");

                WelcomeSettings.AutoShowEnabled = false;
                Assert.AreEqual(1, fired, "A genuine change must raise Changed exactly once.");
            }
            finally
            {
                WelcomeSettings.Changed -= Handler;
            }

            void Handler() => fired++;
        }

        [Test]
        public void ResetToDefaults_RestoresAutoShow()
        {
            WelcomeSettings.AutoShowEnabled = false;
            WelcomeSettings.ResetToDefaults();
            Assert.IsTrue(WelcomeSettings.AutoShowEnabled, "The reset must restore auto-show to on.");
        }

        [Test]
        public void BuildControls_LiveSyncsSwitchFromSettings()
        {
            WelcomeSettings.AutoShowEnabled = true;

            var container = new VisualElement();
            WelcomeSettingsUI.BuildControls(container);

            var autoShow = container.Q<AspidSwitch>();
            Assert.IsNotNull(autoShow, "BuildControls must emit the auto-show switch.");

            // Mutating the store (as another surface would) must reach the control without a manual refresh.
            WelcomeSettings.AutoShowEnabled = false;
            Assert.IsFalse(autoShow.value, "The auto-show switch must mirror the settings live.");
        }
    }
}
