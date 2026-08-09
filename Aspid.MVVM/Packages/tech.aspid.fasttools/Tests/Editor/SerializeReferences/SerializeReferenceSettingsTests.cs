using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Behavioural coverage for the SerializeReference settings propagation contract (ASP-22):
    /// <list type="bullet">
    /// <item>the excluded-folder set raises the dedicated <see cref="SerializeReferenceSettings.ExcludedFoldersChanged"/>
    /// signal only when it genuinely changes — so the usage index drops its warm copy on a real change but an unrelated
    /// setting never triggers a costly index rebuild;</item>
    /// <item>the shared controls built by <see cref="SerializeReferenceSettingsUI"/> mirror the store live, so the
    /// in-window Settings tab and the Project Settings page stay in sync when either edits a value.</item>
    /// </list>
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceSettingsTests
    {
        private bool _autoDeAlias;
        private bool _breakageDetection;
        private string[] _excludedFolders;
        private GateSeverity _buildSeverity;

        [SetUp]
        public void SetUp()
        {
            // Snapshot the project's real settings so the assertions below can mutate them freely and restore on teardown.
            _autoDeAlias = SerializeReferenceSettings.AutoDeAliasEnabled;
            _breakageDetection = SerializeReferenceSettings.BreakageDetectionEnabled;
            _excludedFolders = SerializeReferenceSettings.ExcludedFolders;
            _buildSeverity = SerializeReferenceSettings.BuildSeverity;
        }

        [TearDown]
        public void TearDown()
        {
            SerializeReferenceSettings.AutoDeAliasEnabled = _autoDeAlias;
            SerializeReferenceSettings.BreakageDetectionEnabled = _breakageDetection;
            SerializeReferenceSettings.ExcludedFolders = _excludedFolders;
            SerializeReferenceSettings.BuildSeverity = _buildSeverity;
        }

        // -----------------------------------------------------------------------------------------------------
        // A — excluded folders drive the dedicated index-invalidation signal (and nothing else does)
        // -----------------------------------------------------------------------------------------------------

        // Counts how many times ExcludedFoldersChanged fires while `mutate` runs, leaving the static event clean.
        private static int ExcludedFoldersChangedCount(Action mutate)
        {
            var fired = 0;
            void Handler() => fired++;
            SerializeReferenceSettings.ExcludedFoldersChanged += Handler;
            try { mutate(); }
            finally { SerializeReferenceSettings.ExcludedFoldersChanged -= Handler; }
            return fired;
        }

        [Test]
        public void ExcludedFolders_NewValue_RaisesExcludedFoldersChanged()
        {
            SerializeReferenceSettings.ExcludedFolders = Array.Empty<string>();

            var fired = ExcludedFoldersChangedCount(() =>
                SerializeReferenceSettings.ExcludedFolders = new[] { "Assets/Third Party/" });

            Assert.AreEqual(1, fired, "A genuinely new excluded-folder set must raise ExcludedFoldersChanged exactly once.");
        }

        [Test]
        public void ExcludedFolders_SameValue_DoesNotRaiseExcludedFoldersChanged()
        {
            SerializeReferenceSettings.ExcludedFolders = new[] { "Assets/Plugins/" };

            // Same paths, fresh array instance: the set did not move, so the warm index must not be dropped.
            var fired = ExcludedFoldersChangedCount(() =>
                SerializeReferenceSettings.ExcludedFolders = new[] { "Assets/Plugins/" });

            Assert.AreEqual(0, fired, "Re-assigning an identical set must not raise ExcludedFoldersChanged (no needless index rebuild).");
        }

        [Test]
        public void UnrelatedSetting_DoesNotRaiseExcludedFoldersChanged()
        {
            var fired = ExcludedFoldersChangedCount(() =>
            {
                SerializeReferenceSettings.BreakageDetectionEnabled = !SerializeReferenceSettings.BreakageDetectionEnabled;
                SerializeReferenceSettings.AutoDeAliasEnabled = !SerializeReferenceSettings.AutoDeAliasEnabled;
                SerializeReferenceSettings.BuildSeverity = GateSeverity.Fail;
            });

            Assert.AreEqual(0, fired, "Toggling an unrelated setting must never raise ExcludedFoldersChanged (the index stays warm).");
        }

        [Test]
        public void AnySetting_RaisesGeneralChanged()
        {
            var fired = 0;
            void Handler() => fired++;
            SerializeReferenceSettings.Changed += Handler;
            try
            {
                SerializeReferenceSettings.BreakageDetectionEnabled = !SerializeReferenceSettings.BreakageDetectionEnabled;
                Assert.GreaterOrEqual(fired, 1, "Every setter must still raise the general Changed for repaint and live-sync.");
            }
            finally { SerializeReferenceSettings.Changed -= Handler; }
        }

        // -----------------------------------------------------------------------------------------------------
        // B — the per-scope resets restore exactly their own defaults
        // -----------------------------------------------------------------------------------------------------

        [Test]
        public void ResetSharedToDefaults_RestoresCommittedDefaults_AndLeavesUserSettingsAlone()
        {
            SerializeReferenceSettings.AutoDeAliasEnabled = false;
            SerializeReferenceSettings.BuildSeverity = GateSeverity.Fail;
            SerializeReferenceSettings.ExcludedFolders = new[] { "Assets/Third Party/" };
            SerializeReferenceSettings.BreakageDetectionEnabled = false;

            SerializeReferenceSettings.ResetSharedToDefaults();

            Assert.IsTrue(SerializeReferenceSettings.AutoDeAliasEnabled, "The shared reset must restore auto de-alias to on.");
            Assert.AreEqual(GateSeverity.Warn, SerializeReferenceSettings.BuildSeverity, "The shared reset must restore the gate to Warn.");
            Assert.IsEmpty(SerializeReferenceSettings.ExcludedFolders, "The shared reset must drop every excluded folder.");
            Assert.IsFalse(SerializeReferenceSettings.BreakageDetectionEnabled,
                "The shared reset must not touch the per-user breakage-detection setting.");
        }

        [Test]
        public void ResetUserToDefaults_RestoresBreakageDetection_AndLeavesSharedSettingsAlone()
        {
            SerializeReferenceSettings.BreakageDetectionEnabled = false;
            SerializeReferenceSettings.BuildSeverity = GateSeverity.Fail;

            SerializeReferenceSettings.ResetUserToDefaults();

            Assert.IsTrue(SerializeReferenceSettings.BreakageDetectionEnabled, "The per-user reset must restore breakage detection to on.");
            Assert.AreEqual(GateSeverity.Fail, SerializeReferenceSettings.BuildSeverity,
                "The per-user reset must not touch the shared gate severity.");
        }

        // -----------------------------------------------------------------------------------------------------
        // C — the shared controls mirror the store live (the two settings surfaces stay in sync)
        // -----------------------------------------------------------------------------------------------------

        [Test]
        public void BuildControls_LiveSyncsControlsFromSettings()
        {
            SerializeReferenceSettings.AutoDeAliasEnabled = true;
            SerializeReferenceSettings.BreakageDetectionEnabled = true;
            SerializeReferenceSettings.BuildSeverity = GateSeverity.Warn;
            SerializeReferenceSettings.ExcludedFolders = Array.Empty<string>();

            var container = new VisualElement();
            SerializeReferenceSettingsUI.BuildControls(container);

            // The two boolean settings render as iOS-style AspidSwitch fields (BaseField<bool>), not plain Toggles.
            // Looked up by label, so reordering the rows never silently swaps the assertions.
            var switches = container.Query<AspidSwitch>().ToList();
            Assert.AreEqual(2, switches.Count,
                "BuildControls must emit the breakage-detection and auto-de-alias switches.");
            var breakageDetection = switches.Single(s => s.label == "Breakage detection");
            var autoDeAlias = switches.Single(s => s.label.StartsWith("Auto de-alias"));
            var severity = container.Q<EnumField>();
            var folders = container.Q<SerializeReferenceExcludedFoldersField>();
            Assert.IsNotNull(severity, "BuildControls must emit the build-gate EnumField.");
            Assert.IsNotNull(folders, "BuildControls must emit the excluded-folders field.");

            // Mutating the shared store (as the other surface would) must reach these controls without a manual refresh:
            // the switches and the gate re-read their value off SerializeReferenceSettings.Changed, and the folders list
            // rebuilds off the dedicated ExcludedFoldersChanged signal.
            SerializeReferenceSettings.AutoDeAliasEnabled = false;
            SerializeReferenceSettings.BreakageDetectionEnabled = false;
            SerializeReferenceSettings.BuildSeverity = GateSeverity.Fail;
            SerializeReferenceSettings.ExcludedFolders = new[] { "Assets/Plugins/", "Assets/Generated/" };

            Assert.IsFalse(autoDeAlias.value, "The auto-de-alias switch must mirror Settings live.");
            Assert.IsFalse(breakageDetection.value, "The breakage-detection switch must mirror Settings live.");
            Assert.AreEqual(GateSeverity.Fail, (GateSeverity)severity.value, "The build-gate field must mirror Settings live.");

            // The list-based folders field renders one path Label per excluded folder; both new paths must appear live.
            var listedPaths = folders.Query<Label>().ToList().Select(label => label.text).ToList();
            Assert.Contains("Assets/Plugins/", listedPaths, "The folders field must list every excluded path live.");
            Assert.Contains("Assets/Generated/", listedPaths, "The folders field must list every excluded path live.");
        }

        // -----------------------------------------------------------------------------------------------------
        // D — the scope filter routes each control to the page that owns its storage
        // -----------------------------------------------------------------------------------------------------

        [Test]
        public void BuildControls_UserScope_EmitsOnlyPerUserControls()
        {
            var container = new VisualElement();
            SerializeReferenceSettingsUI.BuildControls(container, AspidSettingsScope.User);

            var labels = container.Query<AspidSwitch>().ToList().Select(s => s.label).ToList();
            Assert.AreEqual(1, labels.Count, "The user scope must emit exactly the one locally-stored switch.");
            Assert.IsTrue(labels.Contains("Breakage detection"), "Breakage detection is per-user and belongs to the user scope.");
            Assert.IsNull(container.Q<EnumField>(), "The build gate is shared and must not leak onto a per-user page.");
            Assert.IsNull(container.Q<SerializeReferenceExcludedFoldersField>(), "Excluded folders are shared and must not leak onto a per-user page.");
        }

        [Test]
        public void BuildControls_SharedScope_EmitsOnlyTeamWideControls()
        {
            var container = new VisualElement();
            SerializeReferenceSettingsUI.BuildControls(container, AspidSettingsScope.Shared);

            var labels = container.Query<AspidSwitch>().ToList().Select(s => s.label).ToList();
            Assert.AreEqual(1, labels.Count, "The shared scope must emit exactly the auto-de-alias switch.");
            Assert.IsTrue(labels.Single().StartsWith("Auto de-alias"), "Auto de-alias is the one shared switch.");
            Assert.IsNotNull(container.Q<EnumField>(), "The build gate is shared and must render on the shared page.");
            Assert.IsNotNull(container.Q<SerializeReferenceExcludedFoldersField>(), "Excluded folders are shared and must render on the shared page.");
        }
    }
}
