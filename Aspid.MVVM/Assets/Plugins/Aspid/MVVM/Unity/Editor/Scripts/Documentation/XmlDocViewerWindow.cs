#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools;
using Aspid.FastTools.Editors;
using Aspid.XmlDoc;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Editor window that displays parsed XML documentation for a type selected via <see cref="TypeSelectorWindow"/>.
    /// Inline <c>&lt;see cref="..."/&gt;</c> references are rendered as clickable chips
    /// that navigate to the referenced type within the viewer.
    /// </summary>
    public sealed class XmlDocViewerWindow : EditorWindow
    {
        private const string WindowTitle = "XML Doc Viewer";

        private const float MinWidth = 480f;
        private const float MinHeight = 360f;

        private static StyleSheet? _styleSheet;

        private static StyleSheet? DocStyleSheet =>
            _styleSheet ??= Resources.Load<StyleSheet>("Styles/aspid-mvvm-binder-reference");

        private static readonly XmlDocParser _parser = new();

        private static readonly System.Text.RegularExpressions.Regex _whitespaceRun =
            new System.Text.RegularExpressions.Regex(@"\s+",
                System.Text.RegularExpressions.RegexOptions.Compiled);

        // Groups: 1=line comment, 2=block comment, 3=verbatim string, 4=string, 5=char,
        //         6=keyword, 7=camelCase method (before '('), 8=PascalCase ctor after 'new' (before '('),
        //         9=PascalCase method (before '('), 10=PascalCase type, 11=number
        private static readonly System.Text.RegularExpressions.Regex _csharpTokenRegex =
            new System.Text.RegularExpressions.Regex(
                @"(//[^\n]*)" +
                @"|(\/\*[\s\S]*?\*\/)" +
                @"|(@""(?:[^""]|"""")*"")" +
                @"|(""(?:\\.|[^""\\])*"")" +
                @"|('(?:\\.|[^'\\])')" +
                @"|\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|partial|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|var|virtual|void|volatile|while|async|await|yield|get|set|value|add|remove|when|where)\b" +
                @"|\b([a-z_]\w*)(?=\s*\()" +
                @"|(?<=new[ \t]+)([A-Z]\w*)(?=\s*\()" +
                @"|\b([A-Z]\w*)(?=\s*\()" +
                @"|\b([A-Z]\w*)\b" +
                @"|\b(\d+(?:\.\d+)?(?:[fFdDmMlLuU]*)?)\b",
                System.Text.RegularExpressions.RegexOptions.Compiled);

        private string? _currentAqn;
        private TypeDocumentation? _currentDoc;

        private Label? _typeLabel;
        private Button? _selectButton;
        private VisualElement? _docContainer;

        [MenuItem("Tools/🐍 Aspid/XML Doc Viewer", priority = 200)]
        public static void ShowWindow()
        {
            var window = GetWindow<XmlDocViewerWindow>();
            window.titleContent = new GUIContent(WindowTitle);
            window.minSize = new Vector2(MinWidth, MinHeight);
            window.Show();
        }

        private void CreateGUI()
        {
            if (DocStyleSheet != null)
                rootVisualElement.styleSheets.Add(DocStyleSheet);

            rootVisualElement.Add(CreateLayout());
        }

        private VisualElement CreateLayout()
        {
            var root = new VisualElement()
                .SetFlexGrow(1)
                .SetFlexDirection(FlexDirection.Column)
                .SetPadding(top: 12, bottom: 12, left: 12, right: 12);

            root.Add(CreateSelectorRow());
            root.Add(CreateDocScrollView());

            return root;
        }

        private VisualElement CreateSelectorRow()
        {
            _typeLabel = new Label("— no type selected —")
                .SetFlexGrow(1);
            _typeLabel.style.color = new Color(0.6f, 0.6f, 0.6f);
            _typeLabel.style.unityFontStyleAndWeight = FontStyle.Italic;

            _selectButton = new Button(OpenTypeSelector).SetText("Select Type…");

            return new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .SetAlignItems(Align.Center)
                .SetMargin(bottom: 12)
                .AddChild(_typeLabel)
                .AddChild(_selectButton);
        }

        private VisualElement CreateDocScrollView()
        {
            var scroll = new ScrollView(ScrollViewMode.Vertical);
            scroll.SetFlexGrow(1);
            scroll.verticalScrollerVisibility = ScrollerVisibility.Auto;
            scroll.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

            _docContainer = new VisualElement().SetFlexDirection(FlexDirection.Column);
            scroll.Add(_docContainer);

            return scroll;
        }

        // ── Type selection ────────────────────────────────────────────────────

        private void OpenTypeSelector()
        {
            var buttonBounds = _selectButton!.worldBound;
            var screenRect = new Rect(
                position.x + buttonBounds.x,
                position.y + buttonBounds.yMax + 22f,
                buttonBounds.width,
                0f);

            TypeSelectorWindow.Show(
                types: new[] { typeof(MonoBehaviour) },
                screenRect: screenRect,
                currentAqn: _currentAqn ?? string.Empty,
                onSelected: OnTypeSelected);
        }

        private void OnTypeSelected(string aqn)
        {
            _currentAqn = aqn;
            _currentDoc = null;

            var type = Type.GetType(aqn);
            if (type != null)
            {
                if (_typeLabel != null)
                {
                    _typeLabel.text = type.FullName ?? type.Name;
                    _typeLabel.style.unityFontStyleAndWeight = FontStyle.Normal;
                    _typeLabel.style.color = new Color(0.85f, 0.85f, 0.85f);
                }

                var sourcePath = FindSourceFile(type.Name);
                if (sourcePath != null)
                    _currentDoc = _parser.ParseType(sourcePath);
            }

            RefreshDocDisplay();
        }

        // ── Cref navigation ───────────────────────────────────────────────────

        private void OpenCrefInViewer(string cref)
        {
            var typeName = XmlDocParser.CrefToTypeName(cref);
            if (string.IsNullOrEmpty(typeName)) return;

            var type = FindTypeByName(typeName);
            if (type == null) return;

            var sourcePath = FindSourceFile(type.Name);
            if (sourcePath == null) return;

            var aqn = type.AssemblyQualifiedName;
            if (aqn != null)
                OnTypeSelected(aqn);
        }

        private static void OpenCrefInIde(string cref)
        {
            var typeName = XmlDocParser.CrefToTypeName(cref);
            if (string.IsNullOrEmpty(typeName)) return;

            var guids = AssetDatabase.FindAssets($"t:Script {typeName}");
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (Path.GetFileNameWithoutExtension(assetPath) != typeName) continue;

                var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
                if (asset != null)
                {
                    AssetDatabase.OpenAsset(asset);
                    return;
                }
            }
        }

        private static Type? FindTypeByName(string typeName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var type = assembly.GetTypes().FirstOrDefault(t => t.Name == typeName);
                    if (type != null) return type;
                }
                catch
                {
                    // Skip assemblies that can't be reflected.
                }
            }
            return null;
        }

        // ── Documentation display ─────────────────────────────────────────────

        private void RefreshDocDisplay()
        {
            if (_docContainer == null) return;
            _docContainer.Clear();

            if (_currentDoc == null)
            {
                var empty = new Label("No documentation found for this type.")
                    .SetMargin(top: 24);
                empty.style.color = new Color(0.4f, 0.4f, 0.4f);
                empty.style.unityTextAlign = TextAnchor.UpperCenter;
                _docContainer.Add(empty);
                return;
            }

            if (_currentDoc.SummaryXml != null)
                AddRichSection(_docContainer, "Summary", _currentDoc.SummaryXml);
            else if (!string.IsNullOrWhiteSpace(_currentDoc.Summary))
                AddPlainSection(_docContainer, "Summary", _currentDoc.Summary!);

            if (_currentDoc.RemarksXml != null)
                AddRichSection(_docContainer, "Remarks", _currentDoc.RemarksXml);
            else if (!string.IsNullOrWhiteSpace(_currentDoc.Remarks))
                AddPlainSection(_docContainer, "Remarks", _currentDoc.Remarks!);

            if (_currentDoc.Examples.Count > 0)
            {
                _docContainer.Add(CreateSectionLabel("Examples"));
                foreach (var example in _currentDoc.Examples)
                    _docContainer.Add(RenderExampleElement(example));
            }

            if (_currentDoc.Members.Count > 0)
            {
                _docContainer.Add(CreateSectionLabel("Members").SetMargin(top: 16));
                foreach (var (_, memberDoc) in _currentDoc.Members)
                    _docContainer.Add(CreateMemberCard(memberDoc));
            }
        }

        // ── Section helpers ───────────────────────────────────────────────────

        private void AddRichSection(VisualElement container, string title, XElement xml)
        {
            container.Add(CreateSectionLabel(title));
            container.Add(RenderXmlElement(xml));
        }

        private static void AddPlainSection(VisualElement container, string title, string text)
        {
            container.Add(CreateSectionLabel(title));
            container.Add(CreateDescriptionLabel(text));
        }

        private static Label CreateSectionLabel(string title)
        {
            var label = new Label(title);
            label.AddToClassList("binder-name");
            label.SetMargin(top: 8, bottom: 4);
            return label;
        }

        private static Label CreateDescriptionLabel(string text)
        {
            var label = new Label(text);
            label.AddToClassList("binder-description");
            label.style.whiteSpace = WhiteSpace.Normal;
            return label;
        }

        // ── Example renderer ──────────────────────────────────────────────────

        private VisualElement RenderExampleElement(XElement example)
        {
            var container = new VisualElement()
                .SetFlexDirection(FlexDirection.Column)
                .SetMargin(bottom: 4);

            foreach (var node in example.Nodes())
            {
                switch (node)
                {
                    case XText textNode:
                    {
                        var text = textNode.Value.Trim();
                        if (!string.IsNullOrEmpty(text))
                            container.Add(CreateDescriptionLabel(text));
                        break;
                    }
                    case XElement child:
                    {
                        var tag = child.Name.LocalName.ToLowerInvariant();
                        if (tag == "code")
                            container.Add(CreateCodeBlock(child.Value));
                        else
                            container.Add(RenderXmlElement(child));
                        break;
                    }
                }
            }

            return container;
        }

        private static VisualElement CreateCodeBlock(string code)
        {
            var label = new Label(HighlightCSharp(code.Trim()));
            label.AddToClassList("code-block");
            label.enableRichText = true;
            label.style.whiteSpace = WhiteSpace.Normal;
            return label;
        }

        private static string HighlightCSharp(string code)
        {
            // Preserve indentation: replace leading whitespace per line with non-breaking spaces
            // so UIElements WhiteSpace.Normal doesn't collapse them.
            var lines = code.Replace("\r\n", "\n").Split('\n');
            var indentBuilder = new System.Text.StringBuilder();
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var trimmed = line.TrimStart(' ', '\t');
                var indentLen = line.Length - trimmed.Length;
                var nbspCount = 0;
                for (var j = 0; j < indentLen; j++)
                    nbspCount += line[j] == '\t' ? 2 : 1;
                indentBuilder.Append(new string('\u00A0', nbspCount));
                indentBuilder.Append(trimmed);
                if (i < lines.Length - 1)
                    indentBuilder.Append('\n');
            }

            return _csharpTokenRegex.Replace(indentBuilder.ToString(), match =>
            {
                if (match.Groups[1].Success || match.Groups[2].Success)
                    return $"<color=#6A9955>{match.Value}</color>";
                if (match.Groups[3].Success || match.Groups[4].Success || match.Groups[5].Success)
                    return $"<color=#CE9178>{match.Value}</color>";
                if (match.Groups[6].Success)
                    return $"<color=#569CD6>{match.Value}</color>";
                if (match.Groups[7].Success)  // camelCase method
                    return $"<color=#DCDCAA>{match.Value}</color>";
                if (match.Groups[8].Success)  // PascalCase ctor after 'new' → type color
                    return $"<color=#4EC9B0>{match.Value}</color>";
                if (match.Groups[9].Success)  // PascalCase method
                    return $"<color=#DCDCAA>{match.Value}</color>";
                if (match.Groups[10].Success) // PascalCase type
                    return $"<color=#4EC9B0>{match.Value}</color>";
                if (match.Groups[11].Success) // number
                    return $"<color=#B5CEA8>{match.Value}</color>";
                return match.Value;
            });
        }

        // ── Rich XML renderer ─────────────────────────────────────────────────

        /// <summary>
        /// Renders an XElement as a flow-layout VisualElement.
        /// Text nodes become Labels; <c>&lt;see cref&gt;</c> nodes become clickable chips.
        /// Falls back to a plain Label when no inline elements are present.
        /// </summary>
        private VisualElement RenderXmlElement(XElement xml)
        {
            var hasInlineRefs = xml.Descendants()
                .Any(e => e.Name.LocalName is "see" or "seealso");

            if (!hasInlineRefs)
                return CreateDescriptionLabel(XmlDocParser.GetInnerTextStatic(xml));

            // Flow container: row-direction with wrapping for inline mixed content.
            var flow = new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .SetFlexWrap(Wrap.Wrap)
                .SetAlignItems(Align.Center)
                .SetMargin(bottom: 4);

            RenderNodesInto(flow, xml);

            return flow;
        }

        private void RenderNodesInto(VisualElement container, XElement parent)
        {
            foreach (var node in parent.Nodes())
            {
                switch (node)
                {
                    case XText textNode:
                    {
                        // Collapse whitespace runs that come from doc-comment indentation.
                        var text = _whitespaceRun.Replace(textNode.Value, " ");
                        if (!string.IsNullOrEmpty(text))
                            container.Add(CreateInlineTextLabel(text));
                        break;
                    }
                    case XElement child:
                    {
                        var tag = child.Name.LocalName.ToLowerInvariant();
                        if (tag is "see" or "seealso")
                        {
                            container.Add(CreateCrefChip(child));
                        }
                        else if (tag is "code")
                        {
                            container.Add(CreateCodeBlock(child.Value));
                        }
                        else
                        {
                            // For <c>, <langword>, <paramref>, <typeparamref> and any
                            // future inline elements — recurse into children.
                            RenderNodesInto(container, child);
                        }
                        break;
                    }
                }
            }
        }

        private static Label CreateInlineTextLabel(string text)
        {
            var label = new Label(text);
            label.AddToClassList("binder-description");
            label.style.marginTop = 0;
            label.style.marginBottom = 0;
            label.style.whiteSpace = WhiteSpace.Normal;
            return label;
        }

        private Label CreateCrefChip(XElement element)
        {
            var cref = element.Attribute("cref")?.Value ?? string.Empty;
            var displayText = string.IsNullOrEmpty(element.Value.Trim())
                ? XmlDocParser.CrefToDisplayName(cref)
                : element.Value.Trim();

            var chip = new Label(displayText);
            chip.AddToClassList("binder-tag");
            chip.AddToClassList("binder-tag-mode");
            chip.AddToClassList("cref-link");
            chip.tooltip = cref;

            if (!string.IsNullOrEmpty(cref))
                chip.RegisterCallback<ClickEvent>(_ => ShowCrefMenu(cref));

            return chip;
        }

        private void ShowCrefMenu(string cref)
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Open in Viewer"), false, () => OpenCrefInViewer(cref));
            menu.AddItem(new GUIContent("Open in IDE"), false, () => OpenCrefInIde(cref));
            menu.ShowAsContext();
        }

        // ── Member card ───────────────────────────────────────────────────────

        private VisualElement CreateMemberCard(MemberDocumentation doc)
        {
            var card = new VisualElement();
            card.AddToClassList("binder-card");

            // Name row with optional inheritdoc badge.
            var nameRow = new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .SetAlignItems(Align.Center)
                .SetMargin(bottom: 4);

            var nameLabel = new Label(doc.Name);
            nameLabel.AddToClassList("binder-name");
            nameLabel.style.marginBottom = 0;
            nameRow.Add(nameLabel);

            if (doc.InheritsDoc)
                nameRow.Add(CreateTag("inheritdoc", "binder-tag-mode"));

            card.Add(nameRow);

            // Summary (rich).
            if (doc.SummaryXml != null)
                card.Add(RenderXmlElement(doc.SummaryXml));
            else if (!string.IsNullOrWhiteSpace(doc.Summary))
                card.Add(CreateDescriptionLabel(doc.Summary!));

            // Remarks (rich).
            if (doc.RemarksXml != null)
            {
                var remarks = RenderXmlElement(doc.RemarksXml);
                remarks.style.marginTop = 4;
                card.Add(remarks);
            }
            else if (!string.IsNullOrWhiteSpace(doc.Remarks))
                card.Add(CreateDescriptionLabel(doc.Remarks!));

            // Returns.
            if (!string.IsNullOrWhiteSpace(doc.Returns))
                card.Add(CreateLabelValueRow("Returns", doc.Returns!));

            // Parameters.
            foreach (var (paramName, paramText) in doc.Parameters)
                card.Add(CreateLabelValueRow(paramName, paramText));

            // Type parameters.
            foreach (var (typeName, typeText) in doc.TypeParameters)
                card.Add(CreateLabelValueRow($"<{typeName}>", typeText));

            // See-also references.
            foreach (var see in doc.SeeAlso)
            {
                if (!string.IsNullOrEmpty(see.Cref))
                    card.Add(CreateTag(see.Cref!, "binder-tag-type"));
            }

            return card;
        }

        private static VisualElement CreateLabelValueRow(string label, string value)
        {
            var row = new VisualElement().SetFlexDirection(FlexDirection.Row);
            row.AddToClassList("binder-row");

            var lbl = new Label(label);
            lbl.AddToClassList("binder-label");

            var val = new Label(value);
            val.AddToClassList("binder-value");
            val.style.whiteSpace = WhiteSpace.Normal;
            val.SetFlexGrow(1);

            return row.AddChild(lbl).AddChild(val);
        }

        private static Label CreateTag(string text, string extraClass)
        {
            var tag = new Label(text);
            tag.AddToClassList("binder-tag");
            tag.AddToClassList(extraClass);
            return tag;
        }

        // ── Source file lookup ────────────────────────────────────────────────

        private static string? FindSourceFile(string typeName)
        {
            var guids = AssetDatabase.FindAssets($"t:Script {typeName}");
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (Path.GetFileNameWithoutExtension(assetPath) == typeName)
                    return assetPath;
            }
            return null;
        }
    }
}
