using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using System.Text.RegularExpressions;
using Aspid.FastTools.UIElements.Editors.Internal;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // The window's bottom bar: a faded dividing line above a row pairing the package version (left, linking to its
    // tagged GitHub release) with the keyboard-ring key (center) and a GitHub link (right). The version is read from
    // the installed UPM package, falling back to the Asset Store copy's package.json, then to "?". Transparent by
    // design, so the window's dotted canvas reads continuously behind it.
    internal sealed class AspidMvvmWindowFooter : VisualElement
    {
        private const string PackageName = "tech.aspid.mvvm";
        private const string PackageManifestPath = "Assets/Aspid/MVVM/package.json";
        private const string GitHubUrl = "https://github.com/VPDPersonal/Aspid.MVVM";
        private const string GitHubReleasesUrl = GitHubUrl + "/releases";
        private const string GitHubReleaseTagUrlFormat = GitHubReleasesUrl + "/tag/v{0}";
        private const string StyleSheetPath = "Styles/Windows/Aspid-MVVM-WindowFooter";
        private const string RootClass = "aspid-mvvm-window-footer";
        private const string RowClass = RootClass + "__row";
        private const string VersionClass = RootClass + "__version";
        private const string KeysClass = RootClass + "__keys";
        private const string LinkClass = RootClass + "__link";

        public AspidMvvmWindowFooter()
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

            // The ring is otherwise invisible until the first arrow press. Centered over the row and
            // click-transparent, so the version and GitHub links keep their edges and their hits.
            var row = new VisualElement().AddClass(RowClass)
                .AddChild(versionLabel)
                .AddChild(new Label("↑↓ navigate   ⏎ activate   esc dismiss")
                    .AddClass(KeysClass)
                    .SetPickingMode(PickingMode.Ignore))
                .AddChild(githubLabel);

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
