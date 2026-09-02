using System.IO;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Asserts that a constructor's documented <c>BindMode</c> constraint matches the guard it actually runs.
    /// </summary>
    /// <remarks>
    /// <c>ThrowExceptionIfTwo</c> rejects both <see cref="BindMode.TwoWay"/> and
    /// <see cref="BindMode.OneWayToSource"/>, so documentation naming only <see cref="BindMode.TwoWay"/> is
    /// misleading. The check reads the package source rather than reflecting over the assemblies, because XML
    /// comments do not survive into IL, and it walks the inheritance graph so a type that inherits the guard from
    /// its base is covered too.
    /// </remarks>
    [TestFixture]
    public sealed class BindModeDocContractTests
    {
        private const string Guard = "ThrowExceptionIfTwo()";

        /// <summary>
        /// The wording that names only TwoWay: the constraint sentence ends right after the tag.
        /// </summary>
        private const string TwoWayOnly = "Must not be <see cref=\"BindMode.TwoWay\"/>.";

        private static readonly Regex DeclarationWithBase =
            new(@"\b(?:class|struct)\s+(?<name>[A-Za-z_][A-Za-z0-9_]*)\s*(?:<[^>]*>)?\s*:\s*(?<base>[A-Za-z_][A-Za-z0-9_]*)");

        private static readonly Regex AnyClass = new(@"\bclass\s+([A-Za-z_][A-Za-z0-9_]*)");

        [Test]
        public void EveryConstructorUnderTheTwoWayGuard_DocumentsOneWayToSourceToo()
        {
            var sources = ReadPackageSources();
            var guarded = CollectGuardedTypes(sources);

            var offenders = sources
                .Where(source => source.Value.Contains(TwoWayOnly))
                .Where(source => DeclaredTypes(source.Value).Overlaps(guarded))
                .Select(source => source.Key)
                .OrderBy(path => path)
                .ToArray();

            Assert.IsEmpty(offenders,
                "These files document only a TwoWay restriction, but ThrowExceptionIfTwo also rejects OneWayToSource:\n" +
                string.Join("\n", offenders));
        }

        /// <summary>
        /// The types whose own constructor runs the guard, plus everything that inherits from them.
        /// </summary>
        private static HashSet<string> CollectGuardedTypes(Dictionary<string, string> sources)
        {
            var guarded = new HashSet<string>();

            foreach (var source in sources.Values.Where(source => source.Contains(Guard)))
                guarded.UnionWith(DeclaredTypes(source));

            bool grew;
            do
            {
                grew = false;

                foreach (var match in sources.Values.SelectMany(source => DeclarationWithBase.Matches(source).Cast<Match>()))
                {
                    if (!guarded.Contains(match.Groups["base"].Value)) continue;
                    grew |= guarded.Add(match.Groups["name"].Value);
                }
            }
            while (grew);

            return guarded;
        }

        private static HashSet<string> DeclaredTypes(string source) =>
            AnyClass.Matches(source).Cast<Match>().Select(match => match.Groups[1].Value).ToHashSet();

        private static Dictionary<string, string> ReadPackageSources()
        {
            var root = Path.GetFullPath("Packages/tech.aspid.mvvm");
            Assert.IsTrue(Directory.Exists(root), $"Package sources not found: {root}");

            var sources = Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories)
                .Where(path => !path.Contains($"{Path.DirectorySeparatorChar}Tests{Path.DirectorySeparatorChar}"))
                .ToDictionary(path => path[root.Length..], File.ReadAllText);

            Assert.IsNotEmpty(sources, "No .cs files found in the package — the check would be meaningless");
            return sources;
        }
    }
}
