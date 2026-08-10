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
    /// <remarks>
    /// A hook is an extension point, and an undocumented override answers neither of the questions its next
    /// reader has: when does this run, and must an override of it call the base implementation? Ten of them had
    /// no documentation at all, while their twins two folders away were documented in full — the kind of gap
    /// that only grows, because the next binder is written by copying whichever neighbour was opened first.
    /// </remarks>
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
            Assert.IsTrue(Directory.Exists(root), $"Корень пакета не найден: {root}");

            var complaints = new List<string>();
            var checkedHooks = 0;

            foreach (var source in Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories))
            {
                if (source.Contains($"{Path.DirectorySeparatorChar}Tests{Path.DirectorySeparatorChar}")) continue;
                Inspect(source, root, complaints, ref checkedHooks);
            }

            // Обход, не нашедший ни одного хука, прошёл бы как чистый.
            Assert.Greater(checkedHooks, 50, "Обход не нашёл переопределений хуков — проверка прошла бы впустую");

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

                // Документация — блок ///, стоящий прямо над объявлением, возможно через строки атрибутов.
                var above = index - 1;
                while (above >= 0 && lines[above].TrimStart().StartsWith("[")) above--;

                if (above >= 0 && lines[above].TrimStart().StartsWith("///")) continue;

                complaints.Add($"{Path.GetRelativePath(root, source)}:{index + 1} — {match.Groups[1].Value}");
            }
        }

        private static string Report(List<string> complaints)
        {
            var report = new StringBuilder();
            report.AppendLine($"Переопределений хуков без документации: {complaints.Count}");

            foreach (var complaint in complaints.OrderBy(text => text))
                report.AppendLine("  " + complaint);

            return report.ToString();
        }
    }
}
