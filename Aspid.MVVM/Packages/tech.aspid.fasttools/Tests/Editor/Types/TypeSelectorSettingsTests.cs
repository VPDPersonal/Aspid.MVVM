using System.Linq;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

namespace Aspid.FastTools.Types.Editors.Tests
{
    /// <summary>
    /// Behavioural coverage for the type selector's per-user settings:
    /// <list type="bullet">
    /// <item><see cref="TypeSelectorSettings"/> clamps the recents capacity and raises <c>Changed</c> only on a real
    /// change;</item>
    /// <item><see cref="TypeSelectorPreferences"/> honours the configurable capacity — including capacity 0 pausing
    /// recording without wiping the collected history — and clears its stores on demand;</item>
    /// <item><see cref="NavigationController"/> composes the Favorites root section only while its toggle is on, and
    /// the Recent section only while the capacity is above 0 (its off switch — no separate toggle exists);</item>
    /// <item>the row counters: <see cref="TreeNode.TypeCount"/> counts the pickable type leaves under a container
    /// recursively, and a composed section title carries the number of rows the section actually surfaced;</item>
    /// <item>the controls built by <see cref="TypeSelectorSettingsView"/> mirror the store live, matching the References
    /// section's live-sync contract.</item>
    /// </list>
    /// </summary>
    [TestFixture]
    internal sealed class TypeSelectorSettingsTests
    {
        // The stored favorites / recents live as JSON under the store's own project-scoped keys; the fixture
        // snapshots the raw JSON so the developer's real lists survive the tests. Referenced from the store — a
        // re-spelled copy would rot if the key composition changed, and the restore would then write a dead key
        // after the API had already cleared the real one.
        private static string FavoritesKey => TypeSelectorPreferences.FavoritesKey;
        private static string RecentsKey => TypeSelectorPreferences.RecentsKey;

        private bool _showFavorites;
        private int _recentsCapacity;
        private string _favoritesJson;
        private string _recentsJson;

        [SetUp]
        public void SetUp()
        {
            // Snapshot the developer's real settings and stored lists so the assertions below can mutate them freely.
            _showFavorites = TypeSelectorSettings.ShowFavorites;
            _recentsCapacity = TypeSelectorSettings.RecentsCapacity;
            _favoritesJson = EditorPrefs.GetString(FavoritesKey, string.Empty);
            _recentsJson = EditorPrefs.GetString(RecentsKey, string.Empty);

            // Start each test from an empty slate.
            TypeSelectorPreferences.ClearFavorites();
            TypeSelectorPreferences.ClearRecents();
        }

        [TearDown]
        public void TearDown()
        {
            TypeSelectorSettings.ShowFavorites = _showFavorites;
            TypeSelectorSettings.RecentsCapacity = _recentsCapacity;

            // Clear first: it invalidates TypeSelectorPreferences' favorites cache, so the restored JSON is re-read
            // lazily instead of being shadowed by the stale in-session set.
            TypeSelectorPreferences.ClearFavorites();
            TypeSelectorPreferences.ClearRecents();
            if (!string.IsNullOrEmpty(_favoritesJson)) EditorPrefs.SetString(FavoritesKey, _favoritesJson);
            if (!string.IsNullOrEmpty(_recentsJson)) EditorPrefs.SetString(RecentsKey, _recentsJson);
        }

        // -----------------------------------------------------------------------------------------------------
        // A — the settings store: clamping and the change signal
        // -----------------------------------------------------------------------------------------------------

        [Test]
        public void RecentsCapacity_ClampsToRange()
        {
            TypeSelectorSettings.RecentsCapacity = 999;
            Assert.AreEqual(TypeSelectorSettings.MaxRecentsCapacity, TypeSelectorSettings.RecentsCapacity,
                "A capacity above the maximum must clamp to MaxRecentsCapacity.");

            TypeSelectorSettings.RecentsCapacity = -5;
            Assert.AreEqual(0, TypeSelectorSettings.RecentsCapacity, "A negative capacity must clamp to 0.");
        }

        [Test]
        public void Setter_RaisesChanged_OnlyOnRealChange()
        {
            TypeSelectorSettings.ShowFavorites = true;

            var fired = 0;
            void Handler() => fired++;
            TypeSelectorSettings.Changed += Handler;
            try
            {
                TypeSelectorSettings.ShowFavorites = true;
                Assert.AreEqual(0, fired, "Re-assigning the same value must not raise Changed.");

                TypeSelectorSettings.ShowFavorites = false;
                Assert.AreEqual(1, fired, "A genuine change must raise Changed exactly once.");
            }
            finally
            {
                TypeSelectorSettings.Changed -= Handler;
            }
        }

        [Test]
        public void ResetToDefaults_RestoresSettings_ButKeepsSavedLists()
        {
            var aqn = typeof(int).AssemblyQualifiedName;
            TypeSelectorPreferences.ToggleFavorite(aqn);
            TypeSelectorSettings.ShowFavorites = false;
            TypeSelectorSettings.RecentsCapacity = 3;

            TypeSelectorSettings.ResetToDefaults();

            Assert.IsTrue(TypeSelectorSettings.ShowFavorites, "The reset must restore the Favorites section to on.");
            Assert.AreEqual(TypeSelectorSettings.DefaultRecentsCapacity, TypeSelectorSettings.RecentsCapacity,
                "The reset must restore the recents capacity to its default.");
            Assert.IsTrue(TypeSelectorPreferences.IsFavorite(aqn),
                "The reset covers settings only — the saved Favorites list must survive.");
        }

        // -----------------------------------------------------------------------------------------------------
        // B — the preference stores honour the configurable capacity and clear on demand
        // -----------------------------------------------------------------------------------------------------

        [Test]
        public void RecordRecent_TrimsToCapacity_InMruOrder()
        {
            TypeSelectorSettings.RecentsCapacity = 3;

            TypeSelectorPreferences.RecordRecent(typeof(int).AssemblyQualifiedName);
            TypeSelectorPreferences.RecordRecent(typeof(string).AssemblyQualifiedName);
            TypeSelectorPreferences.RecordRecent(typeof(float).AssemblyQualifiedName);
            TypeSelectorPreferences.RecordRecent(typeof(double).AssemblyQualifiedName);

            var recents = TypeSelectorPreferences.LoadRecents();
            Assert.AreEqual(3, recents.Count, "Recording past the capacity must trim the list to the capacity.");
            Assert.AreEqual(typeof(double).AssemblyQualifiedName, recents[0], "The most recent pick must come first (MRU).");
            Assert.IsFalse(recents.Contains(typeof(int).AssemblyQualifiedName), "The oldest pick must have been trimmed away.");
        }

        [Test]
        public void RecordRecent_CapacityZero_PausesWithoutWipingHistory()
        {
            TypeSelectorSettings.RecentsCapacity = 8;
            TypeSelectorPreferences.RecordRecent(typeof(int).AssemblyQualifiedName);
            TypeSelectorPreferences.RecordRecent(typeof(string).AssemblyQualifiedName);

            TypeSelectorSettings.RecentsCapacity = 0;
            TypeSelectorPreferences.RecordRecent(typeof(float).AssemblyQualifiedName);

            Assert.AreEqual(0, TypeSelectorPreferences.LoadRecents().Count,
                "At capacity 0 the visible recents list must be empty.");
            Assert.AreEqual(2, TypeSelectorPreferences.RecentsCount,
                "Recording at capacity 0 must be a no-op — the stored history is paused, not wiped.");

            TypeSelectorSettings.RecentsCapacity = 8;
            var recents = TypeSelectorPreferences.LoadRecents();
            Assert.AreEqual(2, recents.Count, "Raising the capacity back must bring the collected history back.");
            Assert.AreEqual(typeof(string).AssemblyQualifiedName, recents[0], "The restored history must keep its MRU order.");
        }

        [Test]
        public void LoadRecents_CapsAtLoweredCapacity_WithoutRewritingStore()
        {
            TypeSelectorSettings.RecentsCapacity = 8;
            TypeSelectorPreferences.RecordRecent(typeof(int).AssemblyQualifiedName);
            TypeSelectorPreferences.RecordRecent(typeof(string).AssemblyQualifiedName);
            TypeSelectorPreferences.RecordRecent(typeof(float).AssemblyQualifiedName);

            TypeSelectorSettings.RecentsCapacity = 1;

            var recents = TypeSelectorPreferences.LoadRecents();
            Assert.AreEqual(1, recents.Count, "Lowering the capacity must hide the surplus immediately.");
            Assert.AreEqual(typeof(float).AssemblyQualifiedName, recents[0], "The kept entry must be the most recent pick.");
            Assert.AreEqual(3, TypeSelectorPreferences.RecentsCount,
                "The surplus stays stored (trimmed only by the next RecordRecent), so raising the capacity restores it.");
        }

        [Test]
        public void ClearFavorites_EmptiesStoreAndFavoriteFlag()
        {
            var aqn = typeof(int).AssemblyQualifiedName;
            TypeSelectorPreferences.ToggleFavorite(aqn);
            Assert.IsTrue(TypeSelectorPreferences.IsFavorite(aqn), "Sanity: the toggle must have favorited the type.");

            TypeSelectorPreferences.ClearFavorites();

            Assert.AreEqual(0, TypeSelectorPreferences.FavoritesCount, "Clearing must drop every stored favorite.");
            Assert.IsFalse(TypeSelectorPreferences.IsFavorite(aqn), "The membership cache must be invalidated by the clear.");
        }

        [Test]
        public void ClearRecents_EmptiesStore()
        {
            TypeSelectorPreferences.RecordRecent(typeof(int).AssemblyQualifiedName);
            TypeSelectorPreferences.ClearRecents();
            Assert.AreEqual(0, TypeSelectorPreferences.RecentsCount, "Clearing must drop the whole recents history.");
        }

        // -----------------------------------------------------------------------------------------------------
        // C — the picker's root page composes Favorites only while its toggle is on, and Recent only while the
        //     capacity is above 0 (the capacity doubles as the section's off switch)
        // -----------------------------------------------------------------------------------------------------

        [Test]
        public void NavigationController_OmitsFavorites_WhenToggleIsOff()
        {
            var aqn = typeof(int).AssemblyQualifiedName;
            TypeSelectorPreferences.ToggleFavorite(aqn);

            TypeSelectorSettings.ShowFavorites = true;
            Assert.IsTrue(HasSection(BuildController(aqn), NavigationController.FavoritesSection),
                "With the toggle on, a stored favorite in the candidate set must compose the Favorites section.");

            TypeSelectorSettings.ShowFavorites = false;
            Assert.IsFalse(HasSection(BuildController(aqn), NavigationController.FavoritesSection),
                "With the toggle off, the Favorites section must not be composed.");
        }

        [Test]
        public void NavigationController_OmitsRecent_WhenCapacityIsZero()
        {
            var aqn = typeof(int).AssemblyQualifiedName;
            TypeSelectorSettings.RecentsCapacity = 8;
            TypeSelectorPreferences.RecordRecent(aqn);

            Assert.IsTrue(HasSection(BuildController(aqn), NavigationController.RecentSection),
                "With a positive capacity, a recorded pick in the candidate set must compose the Recent section.");

            TypeSelectorSettings.RecentsCapacity = 0;
            Assert.IsFalse(HasSection(BuildController(aqn), NavigationController.RecentSection),
                "Capacity 0 is the Recent section's off switch — the section must not be composed.");
        }

        [Test]
        public void TypeCount_CountsDescendantTypeLeavesRecursively()
        {
            var root = new TreeNode("/");
            var container = new TreeNode("Container");
            var nested = new TreeNode("Nested");

            nested.Children.Add(new TreeNode("A", typeof(int).AssemblyQualifiedName));
            container.Children.Add(nested);
            container.Children.Add(new TreeNode("B", typeof(string).AssemblyQualifiedName));
            root.Children.Add(container);
            root.Children.Add(new TreeNode("C", typeof(float).AssemblyQualifiedName));

            Assert.AreEqual(3, root.TypeCount, "The root must count every descendant type leaf, at any depth.");
            Assert.AreEqual(2, container.TypeCount, "A container must count only the leaves under itself.");
            Assert.AreEqual(0, new TreeNode("Empty").TypeCount, "A childless container holds no types.");
        }

        [Test]
        public void AppendedSectionTitle_CarriesItsSurfacedRowCount()
        {
            TypeSelectorSettings.ShowFavorites = true;
            TypeSelectorPreferences.ToggleFavorite(typeof(int).AssemblyQualifiedName);
            TypeSelectorPreferences.ToggleFavorite(typeof(string).AssemblyQualifiedName);

            // A favorite outside the candidate set is not surfaced — it must not inflate the counter either.
            TypeSelectorPreferences.ToggleFavorite(typeof(double).AssemblyQualifiedName);

            var root = new TreeNode("/");
            root.Children.Add(new TreeNode("Int32", typeof(int).AssemblyQualifiedName));
            root.Children.Add(new TreeNode("String", typeof(string).AssemblyQualifiedName));

            var controller = new NavigationController(root, composeSections: true);

            var title = controller.CurrentItems.Single(node =>
                node.IsSectionTitle && node.SectionKey == NavigationController.FavoritesSection);

            Assert.AreEqual(2, title.TypeCount,
                "The section title's counter must match the rows the section actually surfaced.");
        }

        private static NavigationController BuildController(string candidateAqn)
        {
            var root = new TreeNode("/");
            root.Children.Add(new TreeNode("Int32", candidateAqn));
            return new NavigationController(root, composeSections: true);
        }

        private static bool HasSection(NavigationController controller, string section) =>
            controller.CurrentItems.Any(node => node.IsSectionTitle && node.SectionKey == section);

        // -----------------------------------------------------------------------------------------------------
        // D — the settings controls mirror the store live
        // -----------------------------------------------------------------------------------------------------

        [Test]
        public void BuildControls_LiveSyncsControlsFromSettings()
        {
            TypeSelectorSettings.ShowFavorites = true;
            TypeSelectorSettings.RecentsCapacity = 8;

            var container = new VisualElement();
            TypeSelectorSettingsView.BuildControls(container);

            var switches = container.Query<AspidSwitch>().ToList();
            Assert.AreEqual(1, switches.Count,
                "BuildControls must emit exactly the Favorites switch — Recent is governed by the capacity slider alone.");
            var showFavorites = switches[0];
            var capacity = container.Q<SliderInt>();
            Assert.IsNotNull(capacity, "BuildControls must emit the recents-capacity slider.");

            // Mutating the store (as the persisted settings or another surface would) must reach these controls
            // without a manual refresh — they re-read their value off TypeSelectorSettings.Changed.
            TypeSelectorSettings.ShowFavorites = false;
            TypeSelectorSettings.RecentsCapacity = 3;

            Assert.IsFalse(showFavorites.value, "The Favorites switch must mirror the settings live.");
            Assert.AreEqual(3, capacity.value, "The capacity slider must mirror the settings live.");
        }
    }
}
