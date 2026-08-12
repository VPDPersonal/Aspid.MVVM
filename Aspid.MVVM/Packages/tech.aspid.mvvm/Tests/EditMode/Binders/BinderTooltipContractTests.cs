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
    /// Checks that every serialized field a user configures carries a <c>[Tooltip]</c>.
    /// </summary>
    /// <remarks>
    /// The Inspector shows a field by its name alone — no type, no units, no range, and no hint that leaving it
    /// empty is allowed. 95 fields across the binders and converters had nothing to hover over, including every
    /// <c>_converter</c> slot on the UnityEvent binders and every <c>_param</c> on the command ones.
    /// <para/>
    /// Two kinds of field are exempt because a user never sees them: the binder's own bookkeeping, which is
    /// prefixed with a double underscore, and anything marked <see cref="UnityEngine.HideInInspector"/>.
    /// </remarks>
    [TestFixture]
    public sealed class BinderTooltipContractTests
    {
        private static readonly Regex Field = new(@"^\s*\[(?:SerializeField|SerializeReference)\]", RegexOptions.Compiled);

        [Test]
        public void EveryConfigurableSerializedFieldHasATooltip()
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), "Packages", "tech.aspid.mvvm");
            Assert.IsTrue(Directory.Exists(root), $"Корень пакета не найден: {root}");

            var complaints = new List<string>();
            var fields = 0;

            foreach (var source in Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories))
            {
                if (!IsConfigurable(source)) continue;
                Inspect(source, root, complaints, ref fields);
            }

            // Обход, не нашедший ни одного поля, прошёл бы как чистый.
            Assert.Greater(fields, 300, "Обход не нашёл сериализуемых полей — тест прошёл бы впустую");

            if (complaints.Count > 0)
                Assert.Fail(Report(complaints));
        }

        private static bool IsConfigurable(string source)
        {
            var separator = Path.DirectorySeparatorChar;

            if (source.Contains($"{separator}Tests{separator}")) return false;
            if (source.Contains($"{separator}Validation{separator}")) return false;

            return source.Contains($"{separator}Binders{separator}") || source.Contains($"{separator}Converters{separator}");
        }

        private static void Inspect(string source, string root, List<string> complaints, ref int fields)
        {
            var lines = File.ReadAllLines(source);

            for (var index = 0; index < lines.Length; index++)
            {
                if (!Field.IsMatch(lines[index])) continue;

                var declaration = lines[index].Trim();
                if (declaration.Contains("__") || lines[index].Contains("[HideInInspector]")) continue;

                fields++;

                // Атрибуты поля могут стоять и на его строке, и на соседних сверху. Подъём
                // останавливается на строке, которая сама объявляет поле: два [SerializeField]
                // подряд иначе читались бы как один блок, и подсказка соседа сошла бы за свою.
                var above = index;
                var documented = lines[index].Contains("[Tooltip(");

                while (!documented && above > 0)
                {
                    var previous = lines[above - 1];
                    var trimmed = previous.TrimStart();

                    // Между атрибутами встречаются построчные комментарии — например подавления ReSharper.
                    var partOfBlock = trimmed.StartsWith("[") || trimmed.StartsWith("//");
                    if (!partOfBlock || Field.IsMatch(previous)) break;

                    above--;
                    documented = lines[above].Contains("[Tooltip(");
                }

                if (!documented)
                    complaints.Add($"{Path.GetRelativePath(root, source)}:{index + 1} — {declaration}");
            }
        }

        private static string Report(List<string> complaints)
        {
            var report = new StringBuilder();
            report.AppendLine($"Сериализуемых полей без [Tooltip]: {complaints.Count}");

            foreach (var complaint in complaints.OrderBy(text => text))
                report.AppendLine("  " + complaint);

            return report.ToString();
        }
    }
}
