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
    [TestFixture]
    public sealed class ThrownExceptionDocContractTests
    {
        private static readonly Regex Guard = new(@"\.Ensure(?:Valid|ForwardOnly|Forward|Reverse|Not)\(", RegexOptions.Compiled);

        private static readonly Regex Member = new(
            @"^\s*(?:(?:public|protected|internal|private)\s|[A-Za-z_][A-Za-z0-9_.<>?]*\s+[A-Za-z_][A-Za-z0-9_.<>]*\s*\()",
            RegexOptions.Compiled);

        private static readonly Regex NonPublicMember = new(
            @"^\s*(?:private\b|internal\b(?!\s+protected\b))",
            RegexOptions.Compiled);

        [TestCase("private", 0)]
        [TestCase("internal", 0)]
        [TestCase("private protected", 0)]
        [TestCase("public", 1)]
        [TestCase("protected", 1)]
        [TestCase("protected internal", 1)]
        [TestCase("internal protected", 1)]
        public void ExceptionDocumentationIsRequiredOnlyForExternallyAccessibleGuards(string access, int expected)
        {
            var lines = new[] { $"    {access} void Validate()", "    {", "        mode.EnsureValid();", "    }" };
            var complaints = new List<string>();
            var guards = 0;

            Inspect(lines, "Guard.cs", complaints, ref guards);

            Assert.That(complaints.Count, Is.EqualTo(expected));
        }

        [Test]
        public void EveryModeGuardIsDocumentedAsThrowing()
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), "Packages", "tech.aspid.mvvm");
            Assert.IsTrue(Directory.Exists(root), $"Package root not found: {root}");

            var complaints = new List<string>();
            var guards = 0;

            foreach (var source in Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories))
            {
                if (source.Contains($"{Path.DirectorySeparatorChar}Tests{Path.DirectorySeparatorChar}")) continue;
                Inspect(source, root, complaints, ref guards);
            }

            // A floor well below the real count (94 when it was set), not the count itself. The serializable twins
            // are generated, so their guards are not in the package sources and do not count here.
            Assert.Greater(guards, 50, "The sweep found no mode guards, so the check would pass vacuously");

            if (complaints.Count > 0)
                Assert.Fail(Report(complaints));
        }

        private static void Inspect(string source, string root, List<string> complaints, ref int guards)
        {
            var lines = File.ReadAllLines(source);
            Inspect(lines, Path.GetRelativePath(root, source), complaints, ref guards);
        }

        private static void Inspect(string[] lines, string source, List<string> complaints, ref int guards)
        {
            for (var index = 0; index < lines.Length; index++)
            {
                if (!Guard.IsMatch(lines[index])) continue;

                var declaration = index;
                while (declaration > 0 && !Member.IsMatch(lines[declaration])) declaration--;
                if (NonPublicMember.IsMatch(lines[declaration])) continue;

                guards++;

                var above = declaration - 1;
                while (above >= 0 && lines[above].TrimStart().StartsWith("[")) above--;

                var documented = false;
                while (above >= 0 && lines[above].TrimStart().StartsWith("///"))
                {
                    if (lines[above].Contains("<exception")) documented = true;
                    above--;
                }

                if (!documented)
                    complaints.Add($"{source}:{declaration + 1}");
            }
        }

        private static string Report(List<string> complaints)
        {
            var report = new StringBuilder();
            report.AppendLine($"Mode guards without <exception>: {complaints.Count}");

            foreach (var complaint in complaints.Distinct().OrderBy(text => text))
                report.AppendLine("  " + complaint);

            return report.ToString();
        }
    }
}
