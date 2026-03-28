# XML Documentation System

Parsing and rendering C# XML documentation comments within Unity Editor.

## Purpose

This system enables:
1. **Parse** XML documentation comments from `.cs` files into structured data objects
2. **Resolve** `<include>` tags that reference external XML files
3. **Display** documentation in a rich Unity Editor window with multi-tab navigation

## Architecture

**Namespace:** `Aspid.FastTools.XmlDoc`

### 5 Core Components:

**XmlDocModel.cs** — Data Model
- `TypeDocumentation` — metadata for a type (class, interface, struct, enum)
  - `Summary`, `Remarks` (plain text)
  - `SummaryXml`, `RemarksXml` (raw `XElement` for rich rendering)
  - `Members` (Dictionary of MemberDocumentation)
  - `Examples`, `CustomTags` (for extensibility)

- `MemberDocumentation` — metadata for a type member (method, property, constructor)
  - `Name`, `Summary`, `Remarks`, `Returns`
  - `SummaryXml`, `RemarksXml` (raw `XElement` for rich rendering)
  - `Parameters`, `TypeParameters` (name → description dictionaries)
  - `SeeAlso` (list of `SeeReference`)
  - `InheritsDoc` (flag for `<inheritdoc/>`)
  - `InheritedFrom` (source type name when docs resolved via `<inheritdoc/>`)
  - `CustomTags` (unknown tag storage)

**Models/SeeReference.cs** — Reference Model
- `SeeReference` — represents `<see cref="..."/>` or `<seealso cref="..."/>`
  - `Cref` — raw cref attribute value (e.g. `"T:System.String"`)

**XmlDocParser.cs** — XML Comment Parser
- `ParseType(filePath)` — parses a single `.cs` file, returns `TypeDocumentation?`
- Supported tags: `summary`, `remarks`, `returns`, `param`, `typeparam`, `example`, `see`, `seealso`, `inheritdoc`, `include`, plus any custom tags
- Parsing pipeline:
  1. Read file, extract leading `///` comment block for the type
  2. Wrap in `<root>` element, parse as XDocument
  3. Resolve `<include>` tags via XmlDocIncludeResolver
  4. Populate TypeDocumentation
  5. Walk file line-by-line to find member doc blocks
  6. Parse each member block into MemberDocumentation
- Static utility methods:
  - `CrefToDisplayName(cref)` — converts `T:System.String` → `String`
  - `CrefToTypeName(cref)` — extracts type name from cref attribute (handles member crefs like `AudioSource.pitch` → `AudioSource`)
  - `GetInnerTextStatic(element)` — extracts plain text from XElement with inline tags stripped

**XmlDocIncludeResolver.cs** — `<include>` Tag Resolver
- Loads external XML files by relative or absolute path
- Applies XPath to select nodes: `<include file="docs.xml" path="/docs/MyType/summary"/>`
- Caches loaded `XDocument` instances in memory by absolute file path

**XmlDocViewerWindow.cs** — Editor Display Window
- Accessible via menu: `Tools > 🐍 Aspid > XML Doc Viewer`
- Type selection via `TypeSelectorWindow` (filtered to `MonoBehaviour` subtypes), opens as a dropdown popup anchored below the "Select Type…" button
- Emerald green brand theme (stylesheet: `aspid-xmldoc-viewer.uss`, CSS prefix: `doc-*`)
- Static `_parser` instance shared across all windows

### Layout

Three-column layout (inside 20px padding):

```
┌─────────────────────────────────────────────────────┐
│  [tab panel wrapper]  │drag│  [content area]         │
│  ┌────────────────┐   │    │  ┌──────────────────┐   │
│  │ toggle strip   │   │    │  │ header           │   │
│  │ tab panel      │   │    │  │ doc scroll view  │   │
│  │  (scrollable)  │   │    │  │                  │   │
│  └────────────────┘   │    │  └──────────────────┘   │
└─────────────────────────────────────────────────────┘
```

- **Tab panel wrapper** — collapsible column (default width 148px, min 26px when collapsed)
  - **Toggle strip** — contains a `‹`/`›` button to collapse/expand the tab panel
  - **Tab panel** (`doc-tab-panel`) — scrollable list of `TabEntry` items (`doc-tab`)
    - Each tab item: type name label + `×` close button
    - Active tab has `doc-tab--active` class
- **Drag handle** (`doc-drag-handle`) — resizable splitter between tab panel and content
  - Collapse threshold: 60px; below this on release → auto-collapse
  - Hidden when tab panel is collapsed
- **Content area** — flex-grow column:
  - **Header** (`doc-header`): namespace label (hidden when empty) + row with type name label + "Open" button (opens in IDE) + "Select Type…" button
  - **Doc scroll view** — vertical scroll, contains `_docContainer`
- **Grid background** — absolute-positioned, pointer-ignored overlay drawing subtle white 40×40 dot grid via `generateVisualContent` (Painter2D)

### Tab System

- `TabEntry` — internal sealed class: `Aqn`, `TypeName`, `Namespace`, `Doc`, `TabButton`
- `_tabs: List<TabEntry>`, `_activeTabIndex: int`
- `OpenInTab(aqn)` — reuses existing tab if already open; otherwise creates a new tab and activates it
- `OpenInNewWindow(aqn)` — static; creates a new `XmlDocViewerWindow` instance with `_pendingAqn` set (applied after `CreateGUI`)
- `OnTypeSelected(aqn)` — if an active tab exists, navigates in-place (updates tab label + refreshes display); otherwise opens a new tab
- `CloseTab(index)` — removes tab, activates adjacent tab or resets header if no tabs remain

### Cref Navigation

Click on an inline `<see cref="..."/>` chip → `GenericMenu` with three items:
- **Open in Tab** — `OpenCrefInTab(cref)` → resolves type → `OpenInTab`
- **Open in New Window** — `OpenCrefInNewWindow(cref)` → resolves type → `OpenInNewWindow`
- **Open in IDE** — `OpenCrefInIde(cref)` → `AssetDatabase.OpenAsset` on matching script file

`ResolveCrefType(cref)` — uses `CrefToTypeName` then `FindTypeByName` (searches all loaded assemblies).

### `inheritdoc` Resolution

`ResolveInheritedDocs(type, doc)` — called after parsing:
1. Collects members with `InheritsDoc == true` and no Summary/SummaryXml
2. Walks `EnumerateBaseTypes(type)`: base class chain (excluding `object`) then all interfaces
3. For each base type, finds its source file via `FindSourceFile`, parses it with `_parser`
4. Copies `Summary`, `SummaryXml`, `Remarks`, `RemarksXml`, `Returns`, params, type params to the member; sets `InheritedFrom`

## Rendering

### `RefreshDocDisplay`

Renders the active tab's `TypeDocumentation` into `_docContainer`:
1. Summary — rich (`RenderXmlElement`) if `SummaryXml` present, else plain text
2. Remarks — same pattern
3. Examples — section header + `RenderExampleElement` for each
4. Members — section header + `CreateMemberCard` for each

### Rich XML Renderer (`RenderXmlElement`)

- If the element has no `see`/`seealso` descendants → plain `Label` via `GetInnerTextStatic`
- Otherwise → flex row-wrap container rendered by `RenderNodesInto`:
  - `XText` → `CreateInlineTextLabel` (whitespace collapsed)
  - `see`/`seealso` → `CreateCrefChip` (clickable, shows `ShowCrefMenu`)
  - `code` → `CreateCodeBlock` (highlighted)
  - Other elements → recurse `RenderNodesInto`

### Member Card (`CreateMemberCard`)

1. Name row: member name label + optional `inheritdoc` badge + optional `from BaseType` label
2. Summary (rich or plain)
3. Remarks (rich or plain)
4. Params/TypeParams — `doc-param-row` with name chip + description
5. Returns — `doc-param-row` with "Returns" label + description
6. SeeAlso — `doc-seealso-chip` labels (non-clickable, tooltip shows raw cref)

### C# Syntax Highlighting (`HighlightCSharp`)

Tabs converted to 2 non-breaking spaces; indentation preserved with `\u00A0`.
`_csharpTokenRegex` — 11 capture groups, VS Dark theme colors:

| Group | Pattern | Color |
|-------|---------|-------|
| 1 | `//` line comments | `#6A9955` (green) |
| 2 | `/* */` block comments | `#6A9955` (green) |
| 3 | Verbatim strings `@"..."` | `#CE9178` (orange) |
| 4 | Regular strings `"..."` | `#CE9178` (orange) |
| 5 | Char literals `'.'` | `#CE9178` (orange) |
| 6 | C# keywords | `#569CD6` (blue) |
| 7 | camelCase identifiers before `(` | `#DCDCAA` (yellow) |
| 8 | PascalCase after `new ` before `(` | `#4EC9B0` (teal) |
| 9 | PascalCase before `(` | `#DCDCAA` (yellow) |
| 10 | PascalCase type names | `#4EC9B0` (teal) |
| 11 | Numeric literals | `#B5CEA8` (light green) |

## Parsing Workflow

```csharp
// In MyClass.cs:
/// <summary>Type description</summary>
/// <remarks>Additional notes</remarks>
/// <example>
///   <code>var x = new MyClass();</code>
/// </example>
public class MyClass
{
    /// <summary>Method description</summary>
    /// <param name="value">Parameter description</param>
    /// <returns>Return description</returns>
    public void MyMethod(int value) { }
}
```

**Parsing steps:**
1. `XmlDocParser.ParseType("MyClass.cs")`
2. Extract leading `/// ...` block, wrap in `<root>...</root>`
3. Parse as XDocument
4. Resolve any `<include>` elements
5. Populate TypeDocumentation (Summary, Remarks, Examples)
6. Walk all lines to find member blocks (method/property declarations preceded by `///`)
7. Return fully populated TypeDocumentation object

## Supported XML Tags

**Type-level tags:**
- `<summary>` — required brief description
- `<remarks>` — additional details
- `<example>` — usage examples (multiple allowed)
- `<include file="..." path="..."/>` — external file inclusion

**Member-level tags:**
- `<summary>` — member description
- `<remarks>` — additional notes
- `<param name="...">` — parameter description
- `<typeparam name="...">` — generic type parameter description
- `<returns>` — return value description
- `<see cref="...">` — inline reference (no text → auto-generate display name)
- `<seealso cref="...">` — related type reference
- `<inheritdoc/>` — sets `InheritsDoc = true`; viewer resolves docs from base types at display time

**Inline tags (within text):**
- `<c>code</c>` — inline code
- `<code>...</code>` — code block
- `<see cref="X"/>` — reference
- `<seealso cref="X"/>` — related reference
- `<paramref name="x"/>` — parameter reference
- `<typeparamref name="T"/>` — type parameter reference
- `<langword keyword="true"/>` — language keyword

**Custom tags:**
Any unrecognized tag is stored in the `CustomTags` dictionary (for both types and members).
This allows extending documentation without modifying the parser.

## Extending the System

**Add a new standard tag:**
1. Add tag name to `_knownTagNames` in XmlDocParser
2. Add field to TypeDocumentation or MemberDocumentation
3. Add case in `ParseMemberFromLines` or `PopulateFromElement`
4. (Optional) Add rendering logic in XmlDocViewerWindow

**Add a custom tag (without parser changes):**
```csharp
/// <summary>My method</summary>
/// <customparam name="x">Custom metadata</customparam>
public void MyMethod() { }
```
Parser automatically stores this in `CustomTags["customparam"]`.

## Caching in XmlDocIncludeResolver

- Loaded `XDocument` instances are cached by absolute file path
- Cache persists for the lifetime of the resolver instance

## Usage Examples

**Parse a single file:**
```csharp
var parser = new XmlDocParser();
var doc = parser.ParseType("Assets/MyType.cs");
if (doc != null)
{
    Debug.Log($"Summary: {doc.Summary}");
    foreach (var (name, member) in doc.Members)
        Debug.Log($"  - {name}: {member.Summary}");
}
```

**Resolve an `<include>` tag:**
```csharp
var resolver = new XmlDocIncludeResolver();
var elements = resolver.Resolve("docs.xml", "/docs/MyType/summary", "Assets/");
foreach (var elem in elements)
    Debug.Log(XmlDocParser.GetInnerTextStatic(elem));
```

## Integration Points

- Used by **XmlDocViewerWindow** to visualize documentation in the Editor
- Can be extended for:
  - Markdown documentation generation
  - Documentation validation (analyzer rules)
  - Export to other formats (HTML, PDF, etc.)

## Features & Limitations

- **Plain text extraction**: all tags removed, text only (Summary/Remarks string fields)
- **Raw XML preservation**: `SummaryXml`/`RemarksXml` fields retain full structure for rich UI
- **Inline syntax highlighting**: C# code in `<code>` blocks highlighted via regex (VS Dark theme)
- **Link navigation**: click on `<see cref="..."/>` chip → context menu → "Open in Tab", "Open in New Window", or "Open in IDE"
- **Multi-tab navigation**: multiple types open simultaneously; tab panel collapsible and resizable
- **XPath for includes**: standard XPath 1.0 via `XDocument.XPathSelectElements`
- **Type selector**: filtered to `MonoBehaviour` subtypes via `TypeSelectorWindow`
- **Member detection**: heuristic regex on the line following a `///` block — detects constructors, methods, properties

## Code Conventions

- Methods prefixed with `Try...` / `Parse...` return `null` if not found
- `GetInnerText` / `GetInnerTextStatic` strip all inline elements and return plain text
- `CrefToDisplayName` used for UI link display; `CrefToTypeName` used for type lookups
- Static regex patterns are compiled once and reused
- `_parser` is a static shared `XmlDocParser` instance across all viewer windows
- Namespace: `Aspid.FastTools.XmlDoc`
