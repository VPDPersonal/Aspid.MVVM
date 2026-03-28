#nullable enable
using System.Xml.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.XmlDoc
{
    /// <summary>
    /// Documentation extracted from C# XML doc comments for a single type.
    /// </summary>
    public class TypeDocumentation
    {
        /// <summary>Plain-text content of the <c>&lt;summary&gt;</c> tag (inline tags stripped).</summary>
        public string? Summary;

        /// <summary>Raw <c>&lt;summary&gt;</c> element, preserved for rich rendering with inline references.</summary>
        public XElement? SummaryXml;

        /// <summary>Plain-text content of the <c>&lt;remarks&gt;</c> tag (inline tags stripped).</summary>
        public string? Remarks;

        /// <summary>Raw <c>&lt;remarks&gt;</c> element, preserved for rich rendering with inline references.</summary>
        public XElement? RemarksXml;

        /// <summary>Contents of all <c>&lt;example&gt;</c> tags on the type.</summary>
        public readonly List<XElement> Examples = new();

        /// <summary>Documentation for members (methods, properties, constructors) keyed by member name.</summary>
        public readonly Dictionary<string, MemberDocumentation> Members = new();

        /// <summary>
        /// Any unrecognized or custom tags on the type, keyed by tag name.
        /// Each value is the list of raw <see cref="XElement"/> instances for that tag name,
        /// preserving attributes and nested structure.
        /// </summary>
        public readonly Dictionary<string, List<XElement>> CustomTags = new();
    }

    /// <summary>
    /// Documentation extracted from XML doc comments for a single member (method, property, constructor).
    /// </summary>
    public class MemberDocumentation
    {
        /// <summary>The member name as it appears in the doc comment.</summary>
        public string Name = string.Empty;

        /// <summary>Plain-text content of the <c>&lt;summary&gt;</c> tag (inline tags stripped).</summary>
        public string? Summary;

        /// <summary>Raw <c>&lt;summary&gt;</c> element, preserved for rich rendering with inline references.</summary>
        public XElement? SummaryXml;

        /// <summary>Plain-text content of the <c>&lt;remarks&gt;</c> tag (inline tags stripped).</summary>
        public string? Remarks;

        /// <summary>Raw <c>&lt;remarks&gt;</c> element, preserved for rich rendering with inline references.</summary>
        public XElement? RemarksXml;

        /// <summary>Content of the <c>&lt;returns&gt;</c> tag.</summary>
        public string? Returns;

        /// <summary>Contents of <c>&lt;param&gt;</c> tags, keyed by parameter name.</summary>
        public readonly Dictionary<string, string> Parameters = new();

        /// <summary>Contents of <c>&lt;typeparam&gt;</c> tags, keyed by type parameter name.</summary>
        public readonly Dictionary<string, string> TypeParameters = new();

        /// <summary>All <c>&lt;see&gt;</c> and <c>&lt;seealso&gt;</c> references found in this member's doc block.</summary>
        public readonly List<SeeReference> SeeAlso = new();

        /// <summary>
        /// Whether the member is marked with <c>&lt;inheritdoc/&gt;</c>.
        /// </summary>
        public bool InheritsDoc;

        /// <summary>
        /// When docs are resolved via <c>&lt;inheritdoc/&gt;</c>, stores the source type's name.
        /// </summary>
        public string? InheritedFrom;

        /// <summary>
        /// Any unrecognized or custom tags, keyed by tag name.
        /// Each value is the list of raw <see cref="XElement"/> instances for that tag name,
        /// preserving attributes and nested structure.
        /// </summary>
        public readonly Dictionary<string, List<XElement>> CustomTags = new();
    }

}