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
    /// Checks that a member which validates its binding mode says so in an <c>&lt;exception&gt;</c> tag.
    /// </summary>
    /// <remarks>
    /// 155 of the 172 validation sites in the package threw without documenting it. The guard is not a detail:
    /// it is the difference between a binder that ignores an unsupported mode and one that refuses to construct,
    /// and a caller reading the documentation had no way to tell which they were getting. Worse, the two guard
    /// families throw different types — <c>ThrowExceptionIfTwo</c> and its relatives raise
    /// <see cref="System.InvalidOperationException"/>, while <c>ThrowExceptionIfMatches</c> and
    /// <c>ThrowExceptionIfNone</c> raise <see cref="System.ArgumentException"/>.
    /// </remarks>
    [TestFixture]
    public sealed class ThrownExceptionDocContractTests
    {
        private static readonly Regex Guard = new(@"\.ThrowExceptionIf[A-Za-z]*\(", RegexOptions.Compiled);

        /// <summary>
        /// Matches a member declaration, including an explicit interface implementation.
        /// </summary>
        private static readonly Regex Member = new(
            @"^\s*(?:(?:public|protected|internal|private)\s|[A-Za-z_][A-Za-z0-9_.<>?]*\s+[A-Za-z_][A-Za-z0-9_.<>]*\s*\()",
            RegexOptions.Compiled);

        [Test]
        public void EveryModeGuardIsDocumentedAsThrowing()
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), "Packages", "tech.aspid.mvvm");
            Assert.IsTrue(Directory.Exists(root), $"Корень пакета не найден: {root}");

            var complaints = new List<string>();
            var guards = 0;

            foreach (var source in Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories))
            {
                if (source.Contains($"{Path.DirectorySeparatorChar}Tests{Path.DirectorySeparatorChar}")) continue;
                Inspect(source, root, complaints, ref guards);
            }

            // Обход, не нашедший ни одной проверки, прошёл бы как чистый.
            Assert.Greater(guards, 100, "Обход не нашёл проверок режима — тест прошёл бы впустую");

            if (complaints.Count > 0)
                Assert.Fail(Report(complaints));
        }

        private static void Inspect(string source, string root, List<string> complaints, ref int guards)
        {
            var lines = File.ReadAllLines(source);

            for (var index = 0; index < lines.Length; index++)
            {
                if (!Guard.IsMatch(lines[index])) continue;

                guards++;

                var declaration = index;
                while (declaration > 0 && !Member.IsMatch(lines[declaration])) declaration--;

                var above = declaration - 1;
                while (above >= 0 && lines[above].TrimStart().StartsWith("[")) above--;

                var documented = false;
                while (above >= 0 && lines[above].TrimStart().StartsWith("///"))
                {
                    if (lines[above].Contains("<exception")) documented = true;
                    above--;
                }

                if (!documented)
                    complaints.Add($"{Path.GetRelativePath(root, source)}:{declaration + 1}");
            }
        }

        private static string Report(List<string> complaints)
        {
            var report = new StringBuilder();
            report.AppendLine($"Проверок режима без <exception>: {complaints.Count}");

            foreach (var complaint in complaints.Distinct().OrderBy(text => text))
                report.AppendLine("  " + complaint);

            return report.ToString();
        }
    }
}
