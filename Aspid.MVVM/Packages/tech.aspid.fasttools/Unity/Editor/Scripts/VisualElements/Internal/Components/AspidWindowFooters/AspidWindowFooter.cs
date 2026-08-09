using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using System.Text.RegularExpressions;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// The shared bottom bar for Aspid FastTools editor windows: a faded <see cref="AspidDividingLine"/> above a row
    /// pairing the package version (left, linking to its tagged GitHub release) with a GitHub link (right). The version
    /// is read from the installed UPM package, falling back to the bundled <c>package.json</c>, then to <c>"?"</c>.
    /// Transparent by design, so a host window's shared canvas reads continuously behind it.
    /// </summary>
    [UxmlElement(libraryPath = "Aspid/FastTools")]
    internal sealed partial class AspidWindowFooter : VisualElement
    {
        private const string PackageName = "tech.aspid.fasttools";
        private const string PackageManifestPath = "Assets/Aspid/FastTools/package.json";

        private const string GitHubUrl = "https://github.com/VPDPersonal/Aspid.FastTools";
        private const string GitHubReleasesUrl = GitHubUrl + "/releases";
        private const string GitHubReleaseTagUrlFormat = GitHubReleasesUrl + "/tag/v{0}";

        private const string StyleSheetPath = "UI/Components/Aspid-FastTools-WindowFooter";

        private const string RootClass = "aspid-fasttools-window-footer";
        private const string RowClass = RootClass + "__row";
        private const string VersionClass = RootClass + "__version";
        private const string KeysClass = RootClass + "__keys";
        private const string LinkClass = RootClass + "__link";

        /// <summary>
        /// Builds the footer, reads the package version, and wires the version and GitHub links.
        /// </summary>
        public AspidWindowFooter() : this(showKeysHint: true) { }

        /// <param name="showKeysHint">Whether to show the centred keyboard-ring key. Hosts without the ring
        /// (the Unity-native settings pages) pass <c>false</c> so the footer never promises keys that do nothing.</param>
        public AspidWindowFooter(bool showKeysHint)
        {
            this.AddAspidThemeStyleSheets()
                .AddStyleSheetsFromResource(StyleSheetPath)
                .AddClass(RootClass);

            var version = ReadPackageVersion();

            var releaseUrl = version is "?"
                ? GitHubReleasesUrl
                : string.Format(GitHubReleaseTagUrlFormat, version);

            var versionLabel = new Label("v" + version).AddClass(VersionClass);
            versionLabel.AddManipulator(new Clickable(() => Application.OpenURL(releaseUrl)));

            var githubLabel = new Label("GitHub").AddClass(LinkClass);
            githubLabel.AddManipulator(new Clickable(() => Application.OpenURL(GitHubUrl)));

            var row = new VisualElement().AddClass(RowClass);
            row.AddChild(versionLabel);

            // The keyboard-ring key: every hosting window drives its tabs with the same ring, and the ring is
            // otherwise invisible until the first arrow press. Absolutely centred over the row (see the USS) and
            // click-transparent, so the version / GitHub links keep their edges and their hits.
            if (showKeysHint)
                row.AddChild(new Label("↑↓ navigate   ⏎ activate   esc dismiss")
                    .AddClass(KeysClass)
                    .SetPickingMode(PickingMode.Ignore));

            row.AddChild(githubLabel);

            this.AddChild(new AspidDividingLine(AspidDividingLinePreset.Default.SetTheme(ThemeStyle.Type.Darkness)))
                .AddChild(row);
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
    }
}
