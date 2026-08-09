using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Every generic converter a scene or prefab stores must have an ahead-of-time hint behind it.
    /// </summary>
    /// <remarks>
    /// A <c>[SerializeReference]</c> converter closed over a value type exists in a build only as a
    /// string in YAML, so IL2CPP has no reason to emit its code and the scene fails to load on a
    /// device while working in the editor. This reads the YAML the way the player will and checks the
    /// hints in <c>ConverterAotHints</c> cover what it finds.
    /// <para>
    /// It cannot prove a build succeeds — only an IL2CPP build can. What it can do is fail the moment
    /// somebody authors an instantiation nobody seeded, which is when the information is cheap.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ConverterAotCoverageTests
    {
        // managedReferenceFullTypename: <assembly> <namespace.Type`1[[Arg, Asm, …]]>
        private static readonly Regex ManagedReference = new(
            @"managedReferenceFullTypename:\s*(?<assembly>\S+)\s+(?<type>\S.*)$",
            RegexOptions.Compiled);

        private static readonly Regex GenericArgument = new(@"\[([A-Za-z0-9_.+`\[\]]+),", RegexOptions.Compiled);

        [Test]
        public void EveryGenericConverterInAScene_IsSeeded()
        {
            var seeded = ConverterAotHints.SeededTypes.Select(type => type.Name).ToHashSet();

            var unseeded = SerializedConverterTypes()
                .Where(name => name.Contains('`'))
                .SelectMany(ElementTypeNames)
                .Where(name => !IsReferenceTypeOrSeeded(name, seeded))
                .Distinct()
                .ToArray();

            Assert.IsEmpty(
                unseeded,
                "These value types close a serialized generic converter but are not in "
                + "ConverterAotHints.SeededTypes, so the instantiation may not exist in an IL2CPP "
                + "build:" + Environment.NewLine
                + string.Join(Environment.NewLine, unseeded.Select(name => "  - " + name)));
        }

        // Guards against the whole check passing because the paths went stale and nothing was read.
        [Test]
        public void TheScanReadsTheProjectsScenesAndPrefabs() =>
            Assert.That(SerializedFiles().Count(), Is.GreaterThan(0), "scene and prefab files read");

        private static IEnumerable<string> SerializedConverterTypes()
        {
            foreach (var file in SerializedFiles())
            foreach (var line in File.ReadLines(file))
            {
                var match = ManagedReference.Match(line);
                if (!match.Success) continue;

                var type = match.Groups["type"].Value.Trim();
                if (type.StartsWith("Aspid.MVVM", StringComparison.Ordinal)) yield return type;
            }
        }

        private static IEnumerable<string> SerializedFiles()
        {
            var roots = new[]
            {
                Path.Combine(UnityEngine.Application.dataPath),
                Path.GetFullPath(Path.Combine(UnityEngine.Application.dataPath, "..", "Packages")),
            };

            foreach (var root in roots)
            {
                if (!Directory.Exists(root)) continue;

                foreach (var file in Directory.EnumerateFiles(root, "*.unity", SearchOption.AllDirectories))
                    yield return file;

                foreach (var file in Directory.EnumerateFiles(root, "*.prefab", SearchOption.AllDirectories))
                    yield return file;
            }
        }

        private static IEnumerable<string> ElementTypeNames(string typeName)
        {
            foreach (Match match in GenericArgument.Matches(typeName))
            {
                var full = match.Groups[1].Value;
                var dot = full.LastIndexOf('.');
                yield return dot >= 0 ? full[(dot + 1)..] : full;
            }
        }

        // Instantiations over reference types share one compiled body, so only value types need a
        // hint. Anything not known to be a shipped value type is assumed to be a reference type —
        // this errs towards silence rather than towards a wall of false positives.
        private static bool IsReferenceTypeOrSeeded(string name, ICollection<string> seeded) =>
            seeded.Contains(name) || !IsKnownValueType(name);

        private static bool IsKnownValueType(string name) =>
            Type.GetType($"System.{name}")?.IsValueType is true
            || typeof(UnityEngine.Vector3).Assembly.GetType($"UnityEngine.{name}")?.IsValueType is true;
    }
}
