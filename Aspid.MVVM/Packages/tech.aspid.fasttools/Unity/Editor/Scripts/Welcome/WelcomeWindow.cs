using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using UnityEditor.PackageManager.UI;
using System.Text.RegularExpressions;
using Aspid.FastTools.UIElements.Editors.Internal;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    internal sealed class WelcomeWindow : EditorWindow
    {
        private const long ToastVisibleDurationMs = 2500;
        private const float ToastEdgeMargin = 8f;
        private const float ToastCursorOffset = 16f;

        private const string MenuPath = "Tools/Aspid 🐍/Welcome FastTools";

        private const string PackageName = "tech.aspid.fasttools";
        private const string PackageRootPath = "Assets/Aspid/FastTools";

        private const string SamplesPath = PackageRootPath + "/Samples";
        private const string PackageManifestPath = PackageRootPath + "/package.json";
        private const string GitHubUrl = "https://github.com/VPDPersonal/Aspid.FastTools";
        private const string GitHubReleasesUrl = GitHubUrl + "/releases";
        private const string GitHubReleaseTagUrlFormat = GitHubReleasesUrl + "/tag/v{0}";
        private const string AssetStoreUrl = "https://assetstore.unity.com/packages/slug/365584";

        private const string UxmlResourcePath = "UI/Windows/Welcome/Aspid-FastTools-Welcome";

        private const string LogoName = "welcome-logo";
        private const string ToastName = "welcome-toast";
        private const string GitHubName = "welcome-github";
        private const string VersionName = "welcome-version";
        private const string SamplesListName = "welcome-samples-list";
        private const string ToastVisibleClass = "aspid-fasttools-welcome__toast--visible";

        private Label _toast;
        private VisualElement _samplesList;
        private IVisualElementScheduledItem _toastShow;
        private IVisualElementScheduledItem _toastHide;

        private static string SeenKey => $"Aspid.FastTools.WelcomeWindow.Seen::{ProjectPath}";

        public static bool HasBeenSeen
        {
            get => EditorPrefs.GetBool(SeenKey, false);
            private set => EditorPrefs.SetBool(SeenKey, value);
        }

        private static string ProjectPath
        {
            get
            {
                var projectDirectory = Directory.GetParent(Application.dataPath);
                return projectDirectory?.FullName ?? Application.dataPath;
            }
        }

        [MenuItem(MenuPath, priority = 0)]
        public static void ShowWindow()
        {
            var window = GetWindow<WelcomeWindow>(
                utility: false,
                title: "Aspid FastTools Welcome",
                focus: true);

            window.minSize = new Vector2(560f, 420f);
            window.Show();

            HasBeenSeen = true;
        }

        private void CreateGUI()
        {
            titleContent = new GUIContent(text: "Aspid FastTools Welcome");

            var root = rootVisualElement;
            root.Clear();

            var tree = Resources.Load<VisualTreeAsset>(UxmlResourcePath);
            if (tree == null)
            {
                Debug.LogError($"WelcomeWindow: failed to load UXML at Resources/{UxmlResourcePath}.uxml");
                return;
            }

            tree.CloneTree(root);

            _toast = root.Q<Label>(ToastName);
            if (_toast != null)
            {
                // Reparent to the EditorWindow root so absolute coordinates we set in ShowToast
                // are panel-relative (welcome-content has padding that would offset positioning).
                _toast.RemoveFromHierarchy();
                rootVisualElement.Add(_toast);
                _toast.SetPickingMode(PickingMode.Ignore);
            }

            PopulateSamples(root);
            SetUpFooter(root);
            SetUpLogoLink(root);
        }

        private static void SetUpLogoLink(VisualElement root)
        {
            var logo = root.Q<AspidAnimatedLogo>(LogoName);
            logo?.AddManipulator(new Clickable(() => Application.OpenURL(AssetStoreUrl)));
        }

        private void ShowToast(string message, Vector2 mousePosition)
        {
            if (_toast == null) return;

            _toast.text = message;

            // Tentative position; clamping happens after the toast resolves its size on the
            // next layout pass (see _toastShow callback below).
            _toast.style.top = mousePosition.y + ToastCursorOffset;
            _toast.style.left = mousePosition.x;

            // Defer the visible class so the opacity:0 baseline is committed first; otherwise
            // Unity batches the position update with the class change and the fade-in snaps.
            _toastShow?.Pause();
            _toastShow = _toast.schedule.Execute(() =>
            {
                ClampToastWithinPanel(mousePosition);
                _toast.AddClass(ToastVisibleClass);
            }).StartingIn(16);

            _toastHide?.Pause();
            _toastHide = _toast.schedule.Execute(HideToast).StartingIn(ToastVisibleDurationMs);
        }

        private void ClampToastWithinPanel(Vector2 mousePosition)
        {
            if (_toast == null) return;

            var panelWidth = rootVisualElement.layout.width;
            var panelHeight = rootVisualElement.layout.height;
            var toastWidth = _toast.layout.width;
            var toastHeight = _toast.layout.height;

            if (float.IsNaN(toastWidth) || float.IsNaN(toastHeight)) return;
            if (toastWidth <= 0f || toastHeight <= 0f) return;

            var left = mousePosition.x;
            if (left + toastWidth + ToastEdgeMargin > panelWidth)
                left = panelWidth - toastWidth - ToastEdgeMargin;
            if (left < ToastEdgeMargin)
                left = ToastEdgeMargin;

            var top = mousePosition.y + ToastCursorOffset;
            if (top + toastHeight + ToastEdgeMargin > panelHeight)
                top = mousePosition.y - toastHeight - ToastEdgeMargin;
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

            var package = PackageInfo.FindForPackageName(PackageName);
            if (package is not null)
            {
                AddUpmSamples(package);
                return;
            }

            if (AssetDatabase.IsValidFolder(SamplesPath))
                AddLocalSamples();
        }

        private void AddUpmSamples(PackageInfo package)
        {
            foreach (var sample in Sample.FindByPackage(package.name, package.version))
                _samplesList.Add(CreateUpmSampleButton(sample));
        }

        private AspidGradientButton CreateUpmSampleButton(Sample sample)
        {
            var displayName = sample.displayName;
            var captured = sample;

            if (sample.isImported)
            {
                return new AspidGradientButton(displayName, "Select  ▼", evt =>
                {
                    var assetPath = ToProjectRelativePath(captured.importPath);
                    PingAsset(assetPath);
                    ShowToast($"“{displayName}” selected in the Project window", GetMousePosition(evt));
                });
            }

            return new AspidGradientButton(displayName, "Import  ▼", evt =>
            {
                var pointer = GetMousePosition(evt);

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

        private void AddLocalSamples()
        {
            foreach (var subfolder in AssetDatabase.GetSubFolders(SamplesPath))
            {
                var fileName = Path.GetFileName(subfolder);
                if (string.IsNullOrEmpty(fileName)) continue;

                _samplesList.Add(new AspidGradientButton(fileName, evt =>
                {
                    PingAsset(subfolder);
                    ShowToast($"“{fileName}” selected in the Project window", GetMousePosition(evt));
                }));
            }
        }

        private static Vector2 GetMousePosition(EventBase evt) => evt switch
        {
            IPointerEvent pointer => new Vector2(pointer.position.x, pointer.position.y),
            IMouseEvent mouse => mouse.mousePosition,
            _ => Vector2.zero,
        };

        private static void SetUpFooter(VisualElement root)
        {
            var version = ReadPackageVersion();
            var versionLabel = root.Q<Label>(VersionName);

            if (versionLabel is not null)
            {
                versionLabel.text = "v" + version;

                var releaseUrl = version is "?"
                    ? GitHubReleasesUrl
                    : string.Format(GitHubReleaseTagUrlFormat, version);

                versionLabel.AddManipulator(new Clickable(() => Application.OpenURL(releaseUrl)));
            }

            var githubLabel = root.Q<Label>(GitHubName);
            githubLabel?.AddManipulator(new Clickable(() => Application.OpenURL(GitHubUrl)));
        }

        private static string ReadPackageVersion()
        {
            var package = PackageInfo.FindForPackageName(PackageName);
            if (package is not null && !string.IsNullOrEmpty(package.version))
                return package.version;

            var manifest = AssetDatabase.LoadAssetAtPath<TextAsset>(PackageManifestPath);
            if (manifest is null) return "?";

            var match = Regex.Match(
                input: manifest.text,
                pattern: "\"version\"\\s*:\\s*\"([^\"]+)\"");

            return match.Success ? match.Groups[1].Value : "?";
        }

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

            var projectRoot = dataPath.Substring(0, dataPath.Length - "Assets".Length);
            return normalized.StartsWith(projectRoot, StringComparison.OrdinalIgnoreCase)
                ? normalized.Substring(projectRoot.Length)
                : normalized;
        }
    }
}
