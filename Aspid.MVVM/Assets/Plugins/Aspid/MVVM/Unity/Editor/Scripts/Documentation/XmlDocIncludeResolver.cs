#nullable enable
using System;
using System.IO;
using UnityEngine;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.XmlDoc
{
    /// <summary>
    /// Resolves <c>&lt;include file="..." path="..."/&gt;</c> tags in C# XML documentation,
    /// loading external XML files and selecting nodes via XPath.
    /// Loaded documents are cached by absolute file path.
    /// </summary>
    public class XmlDocIncludeResolver
    {
        private readonly Dictionary<string, XDocument> _cache = new();

        /// <summary>
        /// Loads <paramref name="includeFile"/> relative to <paramref name="baseDirectory"/>,
        /// applies the XPath <paramref name="xpath"/>, and returns the matched elements.
        /// Returns an empty sequence if the file does not exist or no nodes match.
        /// </summary>
        /// <param name="includeFile">The relative (or absolute) path to the XML file.</param>
        /// <param name="xpath">The XPath expression used to select nodes inside the file.</param>
        /// <param name="baseDirectory">The directory used to resolve relative file paths.</param>
        public IEnumerable<XElement> Resolve(string includeFile, string xpath, string baseDirectory)
        {
            var absolutePath = Path.IsPathRooted(includeFile)
                ? includeFile
                : Path.GetFullPath(Path.Combine(baseDirectory, includeFile));

            var doc = LoadXmlFile(absolutePath);
            return doc == null ? Array.Empty<XElement>() : doc.XPathSelectElements(xpath);
        }

        private XDocument? LoadXmlFile(string absolutePath)
        {
            if (_cache.TryGetValue(absolutePath, out var cached))
                return cached;

            if (!File.Exists(absolutePath))
                return null;

            try
            {
                var doc = XDocument.Load(absolutePath);
                _cache[absolutePath] = doc;
                return doc;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[XmlDocIncludeResolver] Failed to load '{absolutePath}': {ex.Message}");
                return null;
            }
        }
    }
}
