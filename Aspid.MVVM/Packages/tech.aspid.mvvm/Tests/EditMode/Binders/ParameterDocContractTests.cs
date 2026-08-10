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
    /// The compiler is silent about this on purpose: <c>CS1573</c> fires only when <em>some</em> parameters are
    /// documented, treating a member with none as a deliberate choice. It was not deliberate — 451 members
    /// across the package had a summary somebody wrote by hand and not one <c>&lt;param&gt;</c> under it.
    /// <para/>
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
            Assert.IsTrue(Directory.Exists(root), $"Корень пакета не найден: {root}");

            var complaints = new List<string>();
            var summaries = 0;

            foreach (var source in Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories))
            {
                if (source.Contains($"{Path.DirectorySeparatorChar}Tests{Path.DirectorySeparatorChar}")) continue;
                Inspect(source, root, complaints, ref summaries);
            }

            // Обход, не нашедший ни одного документированного члена, прошёл бы как чистый.
            Assert.Greater(summaries, 200, "Обход не нашёл членов с summary — тест прошёл бы впустую");

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
            report.AppendLine($"Членов с summary, но без <param>: {complaints.Count}");

            foreach (var complaint in complaints.OrderBy(text => text))
                report.AppendLine("  " + complaint);

            return report.ToString();
        }
    }
}
