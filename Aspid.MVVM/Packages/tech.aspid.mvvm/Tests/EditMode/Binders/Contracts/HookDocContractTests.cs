using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Checks that every overridden binder lifecycle hook carries documentation.
    /// </summary>
    [TestFixture]
    public sealed class HookDocContractTests
    {
        private static readonly Regex Declaration = new(
            @"^\s*protected (?:sealed )?override void (OnBound|OnUnbound|OnBinding|OnUnbinding)\s*\(",
            RegexOptions.Compiled);

        [Test]
        public void EveryOverriddenHookIsDocumented()
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), "Packages", "tech.aspid.mvvm");
            Assert.IsTrue(Directory.Exists(root), $"Package root not found: {root}");

            var complaints = new List<string>();
            var checkedHooks = 0;

            foreach (var source in Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories))
            {
                if (source.Contains($"{Path.DirectorySeparatorChar}Tests{Path.DirectorySeparatorChar}")) continue;
                Inspect(source, root, complaints, ref checkedHooks);
            }

            Assert.Greater(checkedHooks, 50, "The sweep found no hook overrides — the check would pass vacuously");

            if (complaints.Count > 0)
                Assert.Fail(Report(complaints));
        }

        private static void Inspect(string source, string root, List<string> complaints, ref int checkedHooks)
        {
            var lines = File.ReadAllLines(source);

            for (var index = 0; index < lines.Length; index++)
            {
                var match = Declaration.Match(lines[index]);
                if (!match.Success) continue;

                checkedHooks++;

                // Documentation is a /// block directly above the declaration, possibly past attribute lines.
                var above = index - 1;
                while (above >= 0 && lines[above].TrimStart().StartsWith("[")) above--;

                if (above >= 0 && lines[above].TrimStart().StartsWith("///")) continue;

                complaints.Add($"{Path.GetRelativePath(root, source)}:{index + 1} — {match.Groups[1].Value}");
            }
        }

        private static string Report(List<string> complaints)
        {
            var report = new StringBuilder();
            report.AppendLine($"Hook overrides without documentation: {complaints.Count}");

            foreach (var complaint in complaints.OrderBy(text => text))
                report.AppendLine("  " + complaint);

            return report.ToString();
        }
    }
}
