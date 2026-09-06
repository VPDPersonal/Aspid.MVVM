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
namespace Aspid.MVVM
{
    // The Welcome tab: the hero (animated logo, title, blurb and quick links), the samples list with Import / Remove
    // cards and the cursor toast reporting each action. Built in code so the icons are resolved through Resources
    // rather than by asset GUID.
    internal sealed class WelcomeView : VisualElement
    {
        private const string UssClassPrefix = "aspid-mvvm-welcome__";
        private const string StyleSheetPath = "Styles/Windows/Aspid-MVVM-Welcome";

        private const long ToastVisibleDurationMs = 2500;
        private const float ToastEdgeMargin = 8f;
        private const float ToastCursorOffset = 16f;

        private const string PackageName = "tech.aspid.mvvm";
        private const string PackageRootPath = "Assets/Aspid/MVVM";

        private const string SamplesPath = PackageRootPath + "/Samples";
        private const string AssetStoreUrl = "https://assetstore.unity.com/packages/slug/298463";
        private const string GitHubUrl = "https://github.com/VPDPersonal/Aspid.MVVM";
        private const string DocumentationUrl = "https://vpdpersonal.github.io/Aspid.MVVM/";

        private const string Icon1ResourcePath = "Icons/aspid_icon_medium_green_1020x1008";
        private const string Icon2ResourcePath = "Icons/aspid_icon_medium_yellow_1020x1008";
        private const string Icon3ResourcePath = "Icons/aspid_icon_medium_red_1020x1008";

        private const string ScrollClass = UssClassPrefix + "scroll";
        private const string ContentClass = UssClassPrefix + "content";
        private const string HeroClass = UssClassPrefix + "hero";
        private const string LogoClass = UssClassPrefix + "logo";
        private const string HeroTextClass = UssClassPrefix + "hero-text";
        private const string DescriptionClass = UssClassPrefix + "description";
        private const string HeroLinksClass = UssClassPrefix + "hero-links";
        private const string HeroLinkClass = UssClassPrefix + "hero-link";
        private const string HeroLinkSeparatorClass = UssClassPrefix + "hero-link-separator";
        private const string CardClass = UssClassPrefix + "card";
        private const string CardHeaderClass = UssClassPrefix + "card-header";
        private const string ToastClass = UssClassPrefix + "toast";
        private const string ToastVisibleClass = UssClassPrefix + "toast--visible";

        private const string SampleCardClass = UssClassPrefix + "sample";
        private const string NavTargetClass = UssClassPrefix + "nav-target";
        private const string SampleInfoClass = UssClassPrefix + "sample-info";
        private const string SampleTitleClass = UssClassPrefix + "sample-title";
        private const string SampleSweepClass = UssClassPrefix + "sample-sweep";
        private const string SampleHeaderClass = UssClassPrefix + "sample-header";
        private const string SampleDividerClass = UssClassPrefix + "sample-divider";
        private const string SampleStateDotClass = UssClassPrefix + "sample-state-dot";
        private const string SampleHeaderRowClass = UssClassPrefix + "sample-header-row";
        private const string SampleDescriptionClass = UssClassPrefix + "sample-description";
        private const string SampleHeaderHoverClass = UssClassPrefix + "sample--header-hover";
        private const string SampleSweepRemoveClass = UssClassPrefix + "sample-sweep--remove";
        private const string SampleHeaderRemoveClass = UssClassPrefix + "sample-header--remove";
        private const string SampleStateDotImportedClass = UssClassPrefix + "sample-state-dot--imported";

        private readonly Label _toast;
        private readonly ScrollView _scroll;
        private readonly VisualElement _samplesList;

        // Keyboard navigation: one flat focus ring over the sample cards' header buttons in list order (hero links
        // stay mouse-only), shared with the other window tabs.
        private readonly NavRing _ring;

        private IVisualElementScheduledItem _toastShow;
        private IVisualElementScheduledItem _toastHide;

        public WelcomeView()
        {
            style.flexGrow = 1;
            this.AddStyleSheetsFromResource(StyleSheetPath);

            _samplesList = new VisualElement();

            _scroll = new ScrollView(ScrollViewMode.Vertical).AddClass(ScrollClass);
            _scroll.AddChild(CreateHero())
                .AddChild(new VisualElement().AddClass(CardClass)
                    .AddChild(new AspidLabel("Samples", AspidLabelPreset.Default
                            .SetLabelTheme(ThemeStyle.Type.Lightness)
                            .SetLabelSize(AspidLabelSizeStyle.Type.H2)
                            .SetLineTheme(ThemeStyle.Type.Dark)
                            .SetLineStatus(StatusStyle.Type.Success))
                        .AddClass(CardHeaderClass))
                    .AddChild(_samplesList));

            this.AddChild(new VisualElement().AddClass(ContentClass)
                .AddChild(_scroll));

            // A direct child of the view, so the absolute coordinates set in ShowToast are view-relative (the content
            // element has padding that would offset positioning).
            _toast = new Label().AddClass(ToastClass).SetPickingMode(PickingMode.Ignore);
            this.AddChild(_toast);

            // The shared keyboard ring: the view holds focus (grabbed on attach) so keys reach it before anything is
            // highlighted. Built before RebuildSamplesList, which registers the sample cards onto it.
            _ring = new NavRing(
                host: this,
                navTargetClass: NavTargetClass,
                scrollTo: element => _scroll.ScrollTo(element));

            RebuildSamplesList();
        }

        private static VisualElement CreateHero()
        {
            var logo = new AspidAnimatedLogo()
                .SetImage1(Resources.Load<Texture2D>(Icon1ResourcePath))
                .SetImage2(Resources.Load<Texture2D>(Icon2ResourcePath))
                .SetImage3(Resources.Load<Texture2D>(Icon3ResourcePath))
                .AddClass(LogoClass);
            logo.AddManipulator(new Clickable(() => Application.OpenURL(AssetStoreUrl)));

            var description = new AspidLabel(
                    "A high-performance, Source Generator-based MVVM framework for Unity: zero reflection in bindings, " +
                    "minimal allocations, generated ViewModels, Views and binders, ready-to-use StarterKit components.",
                    AspidLabelPreset.Default
                        .SetLabelTheme(ThemeStyle.Type.Light)
                        .SetLineSize(AspidDividingLineSizeStyle.Type.None)
                        .SetFontStyle(FontStyle.Normal)
                        .SetSelectable())
                .AddClass(DescriptionClass);

            var links = new VisualElement().AddClass(HeroLinksClass)
                .AddChild(CreateLink("Documentation", DocumentationUrl))
                .AddChild(CreateLinkSeparator())
                .AddChild(CreateLink("GitHub", GitHubUrl))
                .AddChild(CreateLinkSeparator())
                .AddChild(CreateLink("Asset Store", AssetStoreUrl));

            return new VisualElement().AddClass(HeroClass)
                .AddChild(logo)
                .AddChild(new VisualElement().AddClass(HeroTextClass)
                    .AddChild(new AspidAnimatedTitle("Welcome to Aspid.MVVM"))
                    .AddChild(description)
                    .AddChild(links));
        }

        private static Label CreateLink(string text, string url)
        {
            var link = new Label(text).AddClass(HeroLinkClass);
            link.AddManipulator(new Clickable(() => Application.OpenURL(url)));

            return link;
        }

        private static Label CreateLinkSeparator() =>
            new Label("·").AddClass(HeroLinkSeparatorClass);

        private void ShowToast(string message, Vector2 mousePosition)
        {
            _toast.text = message;

            // The view sits below the tab strip, so the event's panel-space cursor position must be converted to
            // view-local coordinates before it drives the toast's absolute top/left.
            var local = this.WorldToLocal(mousePosition);

            // Tentative position; clamping happens after the toast resolves its size on the next layout pass.
            _toast.style.top = local.y + ToastCursorOffset;
            _toast.style.left = local.x;

            // Defer the visible class so the opacity:0 baseline is committed first; otherwise Unity batches the
            // position update with the class change and the fade-in snaps.
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
            _toast.RemoveClass(ToastVisibleClass);

        private void RebuildSamplesList()
        {
            _samplesList.Clear();

            // The cards are rebuilt from scratch, so an Import/Remove triggered from the keyboard rebuilds the very
            // ring it came from; Rebuild puts the highlight back on the same slot.
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
                // An imported sample's one action is taking the copy back out: deletion is confirmed and the sample
                // stays reimportable right after, so the direct verb replaces a Reimport/Remove menu.
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

        // A sample card: a glass box whose header row is one flat button, with the description wrapping below. The
        // state dot reads brand-blue while the sample is not imported and green once it is; a null state drops it
        // entirely, as for local non-UPM samples. onClick receives the panel-space anchor for the result toast: the
        // cursor on a click, the header's center on a keyboard Enter.
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
                // Kept pickable (no click handler, presses bubble through to the header button) so its tooltip can
                // explain the state.
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

            card.AddChild(new VisualElement()
                .AddClass(SampleHeaderRowClass)
                .AddChild(action));

            if (!string.IsNullOrEmpty(description))
            {
                card.AddChild(new AspidDividingLine(AspidDividingLinePreset.Default
                        .SetTheme(ThemeStyle.Type.Light)
                        .SetSize(AspidDividingLineSizeStyle.Type.Thin))
                    .AddClass(SampleDividerClass));

                // The accent sweep riding the divider: a hairline that scales in from the left while the header
                // button is hovered. Red on an imported (Remove) card, brand green otherwise.
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
