#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEngine;

namespace Aspid.XmlDoc
{
    /// <summary>
    /// Parses C# XML documentation comments from <c>.cs</c> source files into <see cref="TypeDocumentation"/> objects.
    /// Supports standard doc tags, <c>&lt;include&gt;</c> resolution, and arbitrary custom tags.
    /// </summary>
    public class XmlDocParser
    {
        // Tags that map to typed fields — anything else goes to CustomTags.
        private static readonly HashSet<string> _knownTagNames = new(StringComparer.OrdinalIgnoreCase)
        {
            "summary", "remarks", "returns", "param", "typeparam",
            "example", "see", "seealso", "inheritdoc", "include",
        };

        private static readonly Regex _xmlCommentLine =
            new Regex(@"^\s*///\s?", RegexOptions.Compiled);

        private static readonly Regex _typeNameFromFile =
            new Regex(@"(?:class|struct|interface|enum|record)\s+(\w+)", RegexOptions.Compiled);

        private static readonly Regex _memberNameRegex =
            new Regex(@"\b(\w+)\s*(?:<[^>]*>)?\s*[\(\{]", RegexOptions.Compiled);

        private static readonly Regex _whitespaceRun =
            new Regex(@"\s+", RegexOptions.Compiled);

        private readonly XmlDocIncludeResolver _includeResolver;

        /// <param name="includeResolver">
        /// Resolver used for <c>&lt;include&gt;</c> tags. A default instance is used when <see langword="null"/>.
        /// </param>
        public XmlDocParser(XmlDocIncludeResolver? includeResolver = null)
        {
            _includeResolver = includeResolver ?? new XmlDocIncludeResolver();
        }

        /// <summary>
        /// Parses the XML doc comments from a single <c>.cs</c> file.
        /// Returns <see langword="null"/> if no XML comments are found.
        /// </summary>
        /// <param name="csFilePath">Absolute or relative path to the C# source file.</param>
        public TypeDocumentation? ParseType(string csFilePath)
        {
            if (!File.Exists(csFilePath))
                return null;

            var content = File.ReadAllText(csFilePath);
            var baseDir = Path.GetDirectoryName(Path.GetFullPath(csFilePath)) ?? string.Empty;
            var typeName = ExtractTypeName(content, csFilePath);

            return ParseTypeFromContent(content, typeName, baseDir);
        }

        /// <summary>
        /// Parses all <c>.cs</c> files in the given directory.
        /// Files without XML comments are skipped.
        /// </summary>
        /// <param name="directoryPath">Directory to search.</param>
        /// <param name="searchPattern">File glob pattern (default: <c>*.cs</c>).</param>
        /// <param name="recursive">Whether to search subdirectories.</param>
        public Dictionary<string, TypeDocumentation> ParseDirectory(
            string directoryPath,
            string searchPattern = "*.cs",
            bool recursive = true)
        {
            var result = new Dictionary<string, TypeDocumentation>();

            if (!Directory.Exists(directoryPath))
                return result;

            var option = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var file in Directory.EnumerateFiles(directoryPath, searchPattern, option))
            {
                var doc = ParseType(file);
                if (doc != null)
                    result[doc.TypeName] = doc;
            }

            return result;
        }

        // ── Internal ──────────────────────────────────────────────────────────

        private TypeDocumentation? ParseTypeFromContent(string content, string typeName, string baseDir)
        {
            // Extract the leading doc block (before the class/struct declaration)
            var typeCommentBlock = ExtractLeadingXmlComments(content);
            if (typeCommentBlock == null)
                return null;

            var root = ParseXmlBlock(typeCommentBlock);
            if (root == null)
                return null;

            ResolveIncludes(root, baseDir);

            var doc = new TypeDocumentation { TypeName = typeName };
            PopulateFromElement(root, doc.Examples, doc.CustomTags,
                out doc.Summary, out doc.Remarks,
                out doc.SummaryXml, out doc.RemarksXml);

            // Parse member doc blocks (constructors, methods, properties)
            ParseMemberBlocks(content, baseDir, doc.Members);

            return doc;
        }

        private void ParseMemberBlocks(string content, string baseDir,
            Dictionary<string, MemberDocumentation> members)
        {
            // Walk the file line by line, collecting consecutive /// blocks
            // followed by a member declaration.
            var lines = content.Split('\n');
            var commentLines = new List<string>();

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (_xmlCommentLine.IsMatch(line))
                {
                    commentLines.Add(_xmlCommentLine.Replace(line, string.Empty));
                    continue;
                }

                if (commentLines.Count > 0)
                {
                    // Try to extract a member name from the line that follows the comment block
                    var memberName = TryExtractMemberName(line);
                    if (memberName != null)
                    {
                        var memberDoc = ParseMemberFromLines(commentLines, memberName, baseDir);
                        if (memberDoc != null)
                            members[memberName] = memberDoc;
                    }
                    commentLines.Clear();
                }
            }
        }

        private MemberDocumentation? ParseMemberFromLines(
            List<string> commentLines, string memberName, string baseDir)
        {
            var xml = WrapInRoot(commentLines);
            var root = ParseXmlBlock(xml);
            if (root == null)
                return null;

            ResolveIncludes(root, baseDir);

            var doc = new MemberDocumentation { Name = memberName };

            foreach (var element in root.Elements())
            {
                var tagName = element.Name.LocalName.ToLowerInvariant();
                switch (tagName)
                {
                    case "summary":
                        doc.Summary = GetInnerText(element);
                        doc.SummaryXml = element;
                        break;
                    case "remarks":
                        doc.Remarks = GetInnerText(element);
                        doc.RemarksXml = element;
                        break;
                    case "returns":
                        doc.Returns = GetInnerText(element);
                        break;
                    case "param":
                        var paramName = element.Attribute("name")?.Value ?? string.Empty;
                        if (!string.IsNullOrEmpty(paramName))
                            doc.Parameters[paramName] = GetInnerText(element);
                        break;
                    case "typeparam":
                        var typeParamName = element.Attribute("name")?.Value ?? string.Empty;
                        if (!string.IsNullOrEmpty(typeParamName))
                            doc.TypeParameters[typeParamName] = GetInnerText(element);
                        break;
                    case "example":
                        doc.Examples.Add(element);
                        break;
                    case "see":
                    case "seealso":
                        doc.SeeAlso.Add(new SeeReference
                        {
                            Cref = element.Attribute("cref")?.Value,
                            Text = string.IsNullOrWhiteSpace(element.Value) ? null : element.Value.Trim(),
                        });
                        break;
                    case "inheritdoc":
                        doc.InheritsDoc = true;
                        break;
                    default:
                        if (!doc.CustomTags.TryGetValue(tagName, out var list))
                        {
                            list = new List<XElement>();
                            doc.CustomTags[tagName] = list;
                        }
                        list.Add(element);
                        break;
                }
            }

            return doc;
        }

        private void PopulateFromElement(XElement root,
            List<XElement> examples,
            Dictionary<string, List<XElement>> customTags,
            out string? summary,
            out string? remarks,
            out XElement? summaryXml,
            out XElement? remarksXml)
        {
            summary = null;
            remarks = null;
            summaryXml = null;
            remarksXml = null;

            foreach (var element in root.Elements())
            {
                var tagName = element.Name.LocalName.ToLowerInvariant();
                switch (tagName)
                {
                    case "summary":
                        summary = GetInnerText(element);
                        summaryXml = element;
                        break;
                    case "remarks":
                        remarks = GetInnerText(element);
                        remarksXml = element;
                        break;
                    case "example":
                        examples.Add(element);
                        break;
                    default:
                        if (!_knownTagNames.Contains(tagName))
                        {
                            if (!customTags.TryGetValue(tagName, out var list))
                            {
                                list = new List<XElement>();
                                customTags[tagName] = list;
                            }
                            list.Add(element);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Resolves all <c>&lt;include&gt;</c> elements in <paramref name="root"/> in-place.
        /// </summary>
        private void ResolveIncludes(XElement root, string baseDir)
        {
            var includes = root.Descendants("include").ToList();
            foreach (var include in includes)
            {
                var file = include.Attribute("file")?.Value;
                var path = include.Attribute("path")?.Value;
                if (string.IsNullOrEmpty(file) || string.IsNullOrEmpty(path))
                {
                    include.Remove();
                    continue;
                }

                var resolved = _includeResolver.Resolve(file, path, baseDir).ToList();
                if (resolved.Count == 0)
                {
                    include.Remove();
                    continue;
                }

                // Replace <include> with the resolved nodes
                foreach (var node in resolved)
                    include.AddBeforeSelf(new XElement(node));
                include.Remove();
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static string? ExtractLeadingXmlComments(string content)
        {
            var lines = content.Split('\n');
            var commentLines = new List<string>();
            var foundComment = false;

            foreach (var line in lines)
            {
                if (_xmlCommentLine.IsMatch(line))
                {
                    commentLines.Add(_xmlCommentLine.Replace(line, string.Empty));
                    foundComment = true;
                    continue;
                }

                if (foundComment)
                {
                    // Stop at the first non-comment non-blank line after we have collected something
                    var trimmed = line.Trim();
                    if (trimmed.Length == 0 || trimmed.StartsWith("["))
                        continue; // attributes or blank lines between comment and declaration are OK
                    break;
                }
            }

            return commentLines.Count == 0 ? null : WrapInRoot(commentLines);
        }

        private static string WrapInRoot(IEnumerable<string> lines)
        {
            var sb = new StringBuilder("<root>");
            foreach (var line in lines)
                sb.AppendLine(line);
            sb.Append("</root>");
            return sb.ToString();
        }

        private static XElement? ParseXmlBlock(string xml)
        {
            try
            {
                return XDocument.Parse(xml).Root;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[XmlDocParser] Failed to parse XML block: {ex.Message}");
                return null;
            }
        }

        private static string ExtractTypeName(string content, string filePath)
        {
            var match = _typeNameFromFile.Match(content);
            return match.Success
                ? match.Groups[1].Value
                : Path.GetFileNameWithoutExtension(filePath);
        }

        private static string? TryExtractMemberName(string line)
        {
            // Match: [access modifier(s)] [return type] MemberName( or {
            var match = _memberNameRegex.Match(line);
            return match.Success ? match.Groups[1].Value : null;
        }

        /// <summary>
        /// Public alias for <see cref="GetInnerText"/> so that external renderers (e.g. the viewer window)
        /// can extract plain text from a raw <see cref="XElement"/> without re-parsing the file.
        /// </summary>
        public static string GetInnerTextStatic(XElement element) => GetInnerText(element);

        /// <summary>
        /// Returns the inner text of an element with all inline XML tags stripped to plain text.
        /// <c>&lt;see cref="X"/&gt;</c> → display name of X; <c>&lt;c&gt;</c>, <c>&lt;langword&gt;</c> → their text content.
        /// </summary>
        private static string GetInnerText(XElement element)
        {
            var sb = new StringBuilder();
            foreach (var node in element.Nodes())
            {
                if (node is XText text)
                    sb.Append(text.Value);
                else if (node is XElement child)
                    sb.Append(InlineElementToPlainText(child));
            }

            // Collapse interior whitespace runs (newlines + indentation from doc comments).
            var raw = sb.ToString();
            return _whitespaceRun.Replace(raw, " ").Trim();
        }

        /// <summary>
        /// Converts an inline XML element to a plain-text string for use in <see cref="GetInnerText"/>.
        /// </summary>
        private static string InlineElementToPlainText(XElement element)
        {
            switch (element.Name.LocalName.ToLowerInvariant())
            {
                case "see":
                case "seealso":
                    var innerText = element.Value.Trim();
                    if (!string.IsNullOrEmpty(innerText)) return innerText;
                    return CrefToDisplayName(element.Attribute("cref")?.Value);

                case "paramref":
                case "typeparamref":
                    return element.Attribute("name")?.Value ?? element.Value;

                case "c":
                case "code":
                case "langword":
                    return element.Value;

                default:
                    // Recurse for any unknown inline wrappers.
                    return GetInnerText(element);
            }
        }

        /// <summary>
        /// Converts a <c>cref</c> attribute value to a short human-readable display name.
        /// Strips <c>T:</c>/<c>M:</c>/etc. prefixes, namespace qualification, and converts
        /// curly-brace generic notation to angle-bracket notation.
        /// </summary>
        public static string CrefToDisplayName(string? cref)
        {
            if (string.IsNullOrEmpty(cref)) return string.Empty;

            // Strip T:, M:, P:, F:, E: prefix.
            var colon = cref!.IndexOf(':');
            var name = colon >= 0 ? cref[(colon + 1)..] : cref;

            // Strip generic parameter list.
            var genericStart = name.IndexOf('{');
            var baseName = genericStart >= 0 ? name[..genericStart] : name;
            var genericPart = genericStart >= 0 ? name[genericStart..] : string.Empty;

            // Take only the last segment after the last dot.
            var dot = baseName.LastIndexOf('.');
            var simpleName = dot >= 0 ? baseName[(dot + 1)..] : baseName;

            // Curly braces → angle brackets (XML doc convention for generics).
            var generics = genericPart.Replace('{', '<').Replace('}', '>');

            return simpleName + generics;
        }

        /// <summary>
        /// Extracts a simple type name from a <c>cref</c> attribute for use in type lookups.
        /// Member crefs like <c>AudioSource.pitch</c> return the declaring type (<c>AudioSource</c>).
        /// </summary>
        public static string CrefToTypeName(string? cref)
        {
            if (string.IsNullOrEmpty(cref)) return string.Empty;

            var colon = cref!.IndexOf(':');
            var name = colon >= 0 ? cref[(colon + 1)..] : cref;

            // Strip generic params.
            var generic = name.IndexOf('{');
            if (generic >= 0) name = name[..generic];

            var parts = name.Split('.');

            // Heuristic: if last segment starts with a lowercase letter it is a member name;
            // take the preceding segment as the declaring type.
            var last = parts[^1];
            if (last.Length > 0 && char.IsLower(last[0]) && parts.Length >= 2)
                return parts[^2];

            return last;
        }
    }
}
