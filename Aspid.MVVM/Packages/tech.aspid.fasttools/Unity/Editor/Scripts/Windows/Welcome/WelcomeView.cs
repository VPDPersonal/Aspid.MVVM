using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using UnityEditor.PackageManager.UI;
using Aspid.FastTools.UIElements.Editors.Internal;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// The Welcome panel as a reusable element: hero, samples list, the logo asset-store link and the cursor toast.
    /// Hosted as the leftmost "home" tab of the Managed References window (see TabWindow).
    /// The UI is cloned from the same UXML the standalone window used; only the toast positioning is
    /// retargeted from the window root to this view (the view sits below the tab strip).
    /// </summary>
    internal sealed class WelcomeView : VisualElement
    {
        private const string UssClassPrefix = "aspid-fasttools-welcome__";
        private const string UxmlResourcePath = "UI/Windows/Welcome/Aspid-FastTools-Welcome";

        private const long ToastVisibleDurationMs = 2500;
        private const float ToastEdgeMargin = 8f;
        private const float ToastCursorOffset = 16f;

        private const string PackageName = "tech.aspid.fasttools";
        private const string PackageRootPath = "Assets/Aspid/FastTools";

        private const string SamplesPath = PackageRootPath + "/Samples";
        private const string AssetStoreUrl = "https://assetstore.unity.com/packages/slug/365584";
        private const string GitHubUrl = "https://github.com/VPDPersonal/Aspid.FastTools";
        private const string DocumentationUrl = GitHubUrl + "/blob/main/Aspid.FastTools/Packages/tech.aspid.fasttools/Documentation/EN/README.md";

        private const string DocsLinkName = "welcome-link-docs";
        private const string GitHubLinkName = "welcome-link-github";
        private const string StoreLinkName = "welcome-link-store";


        private const string LogoName = "welcome-logo";
        private const string ToastName = "welcome-toast";
        private const string ScrollName = "welcome-scroll";
        private const string SamplesListName = "welcome-samples-list";

        private const string SampleCardClass = UssClassPrefix + "sample";
        private const string NavTargetClass = UssClassPrefix + "nav-target";
        private const string SampleInfoClass = UssClassPrefix + "sample-info";
        private const string SampleTitleClass = UssClassPrefix + "sample-title";
        private const string SampleSweepClass = UssClassPrefix + "sample-sweep";
        private const string SampleHeaderClass = UssClassPrefix + "sample-header";
        private const string ToastVisibleClass = UssClassPrefix + "toast--visible";
        private const string SampleDividerClass = UssClassPrefix + "sample-divider";
        private const string SampleStateDotClass = UssClassPrefix + "sample-state-dot";
        private const string SampleHeaderRowClass = UssClassPrefix + "sample-header-row";
        private const string SampleDescriptionClass = UssClassPrefix + "sample-description";
        private const string SampleHeaderHoverClass = UssClassPrefix + "sample--header-hover";
        private const string SampleSweepRemoveClass = UssClassPrefix + "sample-sweep--remove";
        private const string SampleHeaderRemoveClass = UssClassPrefix + "sample-header--remove";
        private const string SampleStateDotImportedClass = UssClassPrefix + "sample-state-dot--imported";

        private readonly Label _toast;

        private ScrollView _scroll;
        private VisualElement _samplesList;
        private IVisualElementScheduledItem _toastShow;
        private IVisualElementScheduledItem _toastHide;

        // Keyboard navigation: one flat focus ring over the sample cards' header buttons in list order (hero
        // links stay mouse-only), shared with the other window tabs.
        private readonly NavRing _ring;

        public WelcomeView()
        {
            style.flexGrow = 1;

            var tree = Resources.Load<VisualTreeAsset>(UxmlResourcePath);
            if (tree == null)
            {
                Debug.LogError($"WelcomeView: failed to load UXML at Resources/{UxmlResourcePath}.uxml");
                return;
            }

            tree.CloneTree(this);

            _toast = this.Q<Label>(ToastName);
            if (_toast != null)
            {
                // Reparent to the view root so the absolute coordinates we set in ShowToast are view-relative
                // (welcome-content has padding that would offset positioning).
                _toast.RemoveFromHierarchy();
                Add(_toast);
                _toast.SetPickingMode(PickingMode.Ignore);
            }

            _scroll = this.Q<ScrollView>(ScrollName);

            // The shared keyboard ring: the view holds focus (grabbed on attach) so keys reach it before anything is
            // highlighted. Built before PopulateSamples, which registers the sample cards onto it.
            _ring = new NavRing(
                host: this,
                navTargetClass: NavTargetClass,
                scrollTo: element => _scroll?.ScrollTo(element));

            PopulateSamples(this);
            SetUpLogoLink(this);
            SetUpHeroLinks(this);
        }

        private static void SetUpHeroLinks(VisualElement root)
        {
            SetUpLink(root, DocsLinkName, DocumentationUrl);
            SetUpLink(root, GitHubLinkName, GitHubUrl);
            SetUpLink(root, StoreLinkName, AssetStoreUrl);
        }

        private static void SetUpLogoLink(VisualElement root)
        {
            var logo = root.Q<AspidAnimatedLogo>(LogoName);
            logo?.AddManipulator(new Clickable(() => Application.OpenURL(AssetStoreUrl)));
        }

        private static void SetUpLink(VisualElement root, string name, string url)
        {
            var link = root.Q<Label>(name);
            link?.AddManipulator(new Clickable(() => Application.OpenURL(url)));
        }

        private void ShowToast(string message, Vector2 mousePosition)
        {
            if (_toast == null) return;

            _toast.text = message;

            // The view sits below the tab strip, so the event's panel-space cursor position must be converted to
            // view-local coordinates before it drives the toast's absolute top/left.
            var local = this.WorldToLocal(mousePosition);

            // Tentative position; clamping happens after the toast resolves its size on the
            // next layout pass (see _toastShow callback below).
            _toast.style.top = local.y + ToastCursorOffset;
            _toast.style.left = local.x;

            // Defer the visible class so the opacity:0 baseline is committed first; otherwise
            // Unity batches the position update with the class change and the fade-in snaps.
            _toastShow?.Pause();
            _toastShow = _toast.schedule.Execute(() =>
            {
                ClampToastWithinPanel(local);
                _toast.AddClass(ToastVisibleClass);
            }).StartingIn(16);

            _toastHide?.Pause();
            _toastHide = _toast.schedule.Execute(HideToast).StartingIn(ToastVisibleDurationMs);
        }

        private void ClampToastWithinPanel(Vector2 local)
        {
            if (_toast == null) return;

            var panelWidth = layout.width;
            var panelHeight = layout.height;
            var toastWidth = _toast.layout.width;
            var toastHeight = _toast.layout.height;

            if (float.IsNaN(toastWidth) || float.IsNaN(toastHeight)) return;
            if (toastWidth <= 0f || toastHeight <= 0f) return;

            var left = local.x;
            if (left + toastWidth + ToastEdgeMargin > panelWidth)
                left = panelWidth - toastWidth - ToastEdgeMargin;
            if (left < ToastEdgeMargin)
                left = ToastEdgeMargin;

            var top = local.y + ToastCursorOffset;
            if (top + toastHeight + ToastEdgeMargin > panelHeight)
                top = local.y - toastHeight - ToastEdgeMargin;
            if (top < ToastEdgeMargin)
                top = ToastEdgeMargin;

            _toast.style.left = left;
            _toast.style.top = top;
        }

        private void HideToast() =>
            _toast?.RemoveClass(ToastVisibleClass);

        private void PopulateSamples(VisualElement root)
        {
            _samplesList = root.Q<VisualElement>(SamplesListName);
            RebuildSamplesList();
        }

        private void RebuildSamplesList()
        {
            if (_samplesList is null) return;

            _samplesList.Clear();

            // The cards are rebuilt from scratch, so an Import/Remove triggered from the keyboard rebuilds the very
            // ring it came from — Rebuild puts the highlight back on the same slot, so the keyboard flow continues
            // from where it acted.
            _ring.Rebuild(() =>
            {
                var package = PackageInfo.FindForPackageName(PackageName);
                if (package is not null) AddUpmSamples(package);
                else if (AssetDatabase.IsValidFolder(SamplesPath)) AddLocalSamples();
            });
        }

        private void AddUpmSamples(PackageInfo package)
        {
            foreach (var sample in Sample.FindByPackage(package.name, package.version))
                _samplesList.Add(CreateUpmSampleCard(sample));
        }

        private VisualElement CreateUpmSampleCard(Sample sample)
        {
            var displayName = sample.displayName;
            var description = sample.description;
            var captured = sample;

            if (sample.isImported)
            {
                // An imported sample's one action is taking the copy back out — deletion is confirmed and the
                // sample stays reimportable right after, so the direct verb replaces a Reimport/Remove menu.
                return CreateSampleCard(displayName, description, "Remove",
                    pointer => RemoveSample(captured, displayName, pointer),
                    imported: true);
            }

            return CreateSampleCard(displayName, description, "Import", imported: false, onClick: pointer =>
            {
                if (!captured.Import(Sample.ImportOptions.HideImportWindow))
                {
                    ShowToast($"Failed to import “{displayName}”", pointer);
                    return;
                }

                AssetDatabase.Refresh();
                ShowToast($"“{displayName}” imported into Assets/Samples", pointer);
                RebuildSamplesList();
            });
        }

        /// <summary>
        /// Builds a sample card in the References group-card idiom: a glass box whose whole header row is one flat
        /// clickable button (the display name on the left, the <paramref name="actionText"/> verb pinned to the
        /// right, an accent glow on hover), with the package.json description (when present) wrapping below.
        /// A state dot ahead of the title carries <paramref name="imported"/>: brand-blue while the sample is
        /// not imported yet (the unread-marker idiom), green once it is; an imported card's destructive verb
        /// also hovers in the error tone. <see langword="null"/> drops the dot — the state doesn't apply
        /// (local, non-UPM samples). <paramref name="onClick"/> receives the panel-space anchor for the result
        /// toast: the cursor on a mouse click, the header's center on a keyboard Enter.
        /// </summary>
        private VisualElement CreateSampleCard(
            string displayName,
            string description,
            string actionText,
            Action<Vector2> onClick,
            bool? imported = null)
        {
            var card = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Darkness))
                .AddClass(SampleCardClass);

            var action = new AspidGradientButton(actionText, evt => onClick(GetMousePosition(evt)))
                .AddClass(SampleHeaderClass);
            if (imported == true)
                action.AddClass(SampleHeaderRemoveClass);

            // Registered as the card's header, so the ring drives both halves of the sweep: the divider sweep below
            // sits outside the button and rides a card modifier, lit by mouse hover and by keyboard focus alike.
            _ring.RegisterHeader(action, card, SampleHeaderHoverClass, () => onClick(action.worldBound.center));

            var info = new VisualElement()
                .AddClass(SampleInfoClass)
                .SetPickingMode(PickingMode.Ignore);

            if (imported.HasValue)
            {
                // Kept pickable (no click handler — presses bubble through to the header button) so its
                // tooltip can explain the state.
                var dot = new VisualElement().AddClass(SampleStateDotClass);

                if (imported.Value)
                    dot.AddClass(SampleStateDotImportedClass);

                dot.tooltip = imported.Value ? "Imported" : "Not imported yet";
                info.AddChild(dot);
            }

            info.AddChild(new Label(displayName)
                .AddClass(SampleTitleClass)
                .SetPickingMode(PickingMode.Ignore));

            action.AddLeadingContent(info);

            var header = new VisualElement()
                .AddClass(SampleHeaderRowClass)
                .AddChild(action);

            card.AddChild(header);

            if (!string.IsNullOrEmpty(description))
            {
                card.AddChild(new AspidDividingLine(AspidDividingLinePreset.Default
                        .SetTheme(ThemeStyle.Type.Light)
                        .SetSize(AspidDividingLineSizeStyle.Type.Thin))
                    .AddClass(SampleDividerClass));

                // The accent sweep riding the divider: a hairline that scales in from the left while the header
                // button is hovered (via the --header-hover card modifier). Red on an imported (Remove) card,
                // brand green otherwise.
                var sweep = new VisualElement().AddClass(SampleSweepClass);
                if (imported == true)
                    sweep.AddClass(SampleSweepRemoveClass);
                card.AddChild(sweep);

                card.AddChild(new Label(description)
                    .AddClass(SampleDescriptionClass));
            }

            return card;
        }

        private void RemoveSample(Sample sample, string displayName, Vector2 pointer)
        {
            var target = ToProjectRelativePath(sample.importPath);

            var confirmed = EditorUtility.DisplayDialog(
                $"Remove “{displayName}”",
                $"This deletes “{target}” from the project, discarding any local changes to the copy. Continue?",
                "Remove",
                "Cancel");

            if (!confirmed) return;

            if (!AssetDatabase.DeleteAsset(target))
            {
                ShowToast($"Failed to remove “{displayName}”", pointer);
                return;
            }

            AssetDatabase.Refresh();
            ShowToast($"“{displayName}” removed from Assets/Samples", pointer);
            RebuildSamplesList();
        }

        private void AddLocalSamples()
        {
            foreach (var subfolder in AssetDatabase.GetSubFolders(SamplesPath))
            {
                var fileName = Path.GetFileName(subfolder);
                if (string.IsNullOrEmpty(fileName)) continue;

                _samplesList.Add(CreateSampleCard(fileName, null, "Show", pointer =>
                {
                    PingAsset(subfolder);
                    ShowToast($"“{fileName}” selected in the Project window", pointer);
                }));
            }
        }

        private static Vector2 GetMousePosition(EventBase evt) => evt switch
        {
            IPointerEvent pointer => new Vector2(pointer.position.x, pointer.position.y),
            IMouseEvent mouse => mouse.mousePosition,
            _ => Vector2.zero,
        };

        private static void PingAsset(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath)) return;

            var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
            if (asset is null) return;

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
            EditorGUIUtility.PingObject(asset);
        }

        private static string ToProjectRelativePath(string path)
        {
            if (string.IsNullOrEmpty(path)) return path;

            var normalized = path.Replace('\\', '/');
            if (normalized.StartsWith("Assets/", StringComparison.Ordinal) || normalized == "Assets")
                return normalized;

            var dataPath = Application.dataPath.Replace('\\', '/');
            if (!dataPath.EndsWith("/Assets", StringComparison.Ordinal)) return normalized;

            var projectRoot = dataPath[..^"Assets".Length];
            return normalized.StartsWith(projectRoot, StringComparison.OrdinalIgnoreCase)
                ? normalized[projectRoot.Length..]
                : normalized;
        }
    }
}
