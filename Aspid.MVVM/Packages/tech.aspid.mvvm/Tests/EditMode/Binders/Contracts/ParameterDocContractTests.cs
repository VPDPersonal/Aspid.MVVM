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
    /// Checks that a member with a hand-written <c>&lt;summary&gt;</c> also documents its parameters.
    /// </summary>
    /// <remarks>
    /// A member documented with <c>&lt;inheritdoc/&gt;</c> is exempt: its parameters are described wherever the
    /// documentation is inherited from.
    /// </remarks>
    [TestFixture]
    public sealed class ParameterDocContractTests
    {
        private static readonly Regex Member = new(
            @"^\s*(?:public|protected|internal)\s[^;=]*?\b(?<name>[A-Za-z_][A-Za-z0-9_]*)\s*\((?<parameters>[^)]*)\)",
            RegexOptions.Compiled);

        [Test]
        public void EveryHandWrittenSummaryDocumentsItsParameters()
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), "Packages", "tech.aspid.mvvm");
            Assert.IsTrue(Directory.Exists(root), $"Package root not found: {root}");

            var complaints = new List<string>();
            var summaries = 0;

            foreach (var source in Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories))
            {
                if (source.Contains($"{Path.DirectorySeparatorChar}Tests{Path.DirectorySeparatorChar}")) continue;
                Inspect(source, root, complaints, ref summaries);
            }

            Assert.Greater(summaries, 200, "The sweep found no documented members — the check would pass vacuously");

            if (complaints.Count > 0)
                Assert.Fail(Report(complaints));
        }

        private static void Inspect(string source, string root, List<string> complaints, ref int summaries)
        {
            var lines = File.ReadAllLines(source);

            for (var index = 0; index < lines.Length; index++)
            {
                var match = Member.Match(lines[index]);
                if (!match.Success || match.Groups["parameters"].Value.Trim().Length == 0) continue;

                var above = index - 1;
                while (above >= 0 && lines[above].TrimStart().StartsWith("[")) above--;

                var block = new List<string>();
                while (above >= 0 && lines[above].TrimStart().StartsWith("///"))
                {
                    block.Add(lines[above]);
                    above--;
                }

                if (block.Count == 0) continue;

                var text = string.Join("\n", block);
                if (text.Contains("<inheritdoc")) continue;

                summaries++;
                if (text.Contains("<param")) continue;

                complaints.Add($"{Path.GetRelativePath(root, source)}:{index + 1} — {match.Groups["name"].Value}");
            }
        }

        private static string Report(List<string> complaints)
        {
            var report = new StringBuilder();
            report.AppendLine($"Members with a summary but no <param>: {complaints.Count}");

            foreach (var complaint in complaints.OrderBy(text => text))
                report.AppendLine("  " + complaint);

            return report.ToString();
        }
    }
}
