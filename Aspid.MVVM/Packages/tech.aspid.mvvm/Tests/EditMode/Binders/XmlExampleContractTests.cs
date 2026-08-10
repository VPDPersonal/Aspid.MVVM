using System;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Checks the <c>&lt;include&gt;</c> blocks that pull binder examples out of the
    /// <c>XmlExampleDoc-*.xml</c> files, and the examples themselves.
    /// </summary>
    /// <remarks>
    /// Nothing enforced this before, and every way of getting it wrong is quiet. A path that does not resolve
    /// leaves the class with no example and produces a compiler warning nobody reads; an example whose
    /// <c>&lt;member&gt;</c> no file references is written, maintained and rendered nowhere. Three rules are
    /// checked here — the include resolves, the member it names exists, and no member sits unreferenced — plus
    /// the convention that MonoBehaviour binders carry no examples at all, being configured in the Inspector.
    /// </remarks>
    [TestFixture]
    public sealed class XmlExampleContractTests
    {
        private static readonly Regex IncludePattern = new(
            @"<include\s+file=""(?<file>[^""]+)""\s+path=""[^""]*member\[@name='(?<member>[^']+)'\][^""]*""",
            RegexOptions.Compiled);

        [Test]
        public void EveryIncludeResolvesAndEveryExampleIsUsed()
        {
            var root = PackageRoot();
            var complaints = new List<string>();
            var used = new HashSet<string>();

            foreach (var source in Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories))
                Inspect(source, used, complaints);

            CheckForOrphans(root, used, complaints);

            if (complaints.Count > 0)
                Assert.Fail(Report(complaints));
        }

        private static void Inspect(string source, HashSet<string> used, List<string> complaints)
        {
            var text = File.ReadAllText(source);
            var matches = IncludePattern.Matches(text);
            if (matches.Count == 0) return;

            var name = Path.GetFileNameWithoutExtension(source);

            // Проверяется по имени файла, а не по типу: тест читает исходники, а не отражение,
            // и разбирать иерархию классов ради того же ответа было бы лишним.
            if (name.EndsWith("MonoBinder", StringComparison.Ordinal))
                complaints.Add($"{name}: <include> на MonoBehaviour-биндере — конвенция их не предполагает");

            foreach (Match match in matches)
            {
                var file = Path.Combine(Path.GetDirectoryName(source)!, match.Groups["file"].Value);
                var member = match.Groups["member"].Value;

                if (!File.Exists(file))
                {
                    complaints.Add($"{name}: путь не резолвится — {match.Groups["file"].Value}");
                    continue;
                }

                if (Members(file).Contains(member)) used.Add(Key(file, member));
                else complaints.Add($"{name}: в {Path.GetFileName(file)} нет member «{member}»");
            }
        }

        private static void CheckForOrphans(string root, HashSet<string> used, List<string> complaints)
        {
            foreach (var file in Directory.GetFiles(root, "XmlExampleDoc-*.xml", SearchOption.AllDirectories))
            {
                foreach (var member in Members(file))
                {
                    if (used.Contains(Key(file, member))) continue;
                    complaints.Add($"{Path.GetFileName(file)}: member «{member}» никем не включён");
                }
            }
        }

        private static IEnumerable<string> Members(string file) =>
            XDocument.Load(file).Descendants("member")
                .Select(member => member.Attribute("name")?.Value)
                .Where(name => !string.IsNullOrEmpty(name))
                .ToArray();

        private static string Key(string file, string member) =>
            Path.GetFileName(file) + "|" + member;

        private static string PackageRoot()
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), "Packages", "tech.aspid.mvvm");
            Assert.IsTrue(Directory.Exists(root), $"Корень пакета не найден: {root}");

            return root;
        }

        private static string Report(List<string> complaints)
        {
            var report = new StringBuilder();
            report.AppendLine($"Нарушений контракта примеров: {complaints.Count}");

            foreach (var complaint in complaints)
                report.AppendLine("  " + complaint);

            return report.ToString();
        }
    }
}
