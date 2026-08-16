using System;
using System.Linq;
using System.Text;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Every serialized field of a converter needs a <see cref="TooltipAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Converters are configured almost entirely through the Inspector, where XML documentation is
    /// invisible — a tooltip is the only place an explanation reaches the person setting the value.
    /// The package shipped 33 serialized converter fields and not one tooltip, which made the most
    /// useful documentation in the package also the missing kind.
    /// </remarks>
    [TestFixture]
    internal sealed class ConverterInspectorContractTests
    {
        [Test]
        public void EverySerializedConverterFieldHasATooltip()
        {
            var undocumented = ConverterTypes()
                .SelectMany(type => SerializedFields(type).Select(field => (type, field)))
                .Where(pair => pair.field.GetCustomAttribute<TooltipAttribute>(inherit: false) is null)
                .ToArray();

            Assert.IsEmpty(undocumented, Explain(undocumented));
        }

        /// <summary>
        /// Every converter the picker offers should say where it belongs and what it is for.
        /// </summary>
        /// <remarks>
        /// The dropdown lists them by namespace, and they share one — so without a group the whole
        /// catalogue arrives as a single flat list of a hundred-odd entries, which is a worse way to
        /// find a converter than knowing its name already.
        /// </remarks>
        [Test]
        public void EveryPickableConverterIsGroupedAndDescribed()
        {
            var ungrouped = ConverterTypes()
                .Where(type => !type.IsAbstract)
                .Where(type => !typeof(UnityEngine.Object).IsAssignableFrom(type))
                .Where(type => type.IsDefined(typeof(SerializableAttribute), inherit: false))
                .Select(type => (type, display: type.GetCustomAttribute<Aspid.FastTools.Types.TypeSelectorDisplayAttribute>(inherit: false)))
                .Where(pair => pair.display is null || string.IsNullOrEmpty(pair.display.Group))
                .Select(pair => pair.type)
                .ToArray();

            Assert.IsEmpty(
                ungrouped,
                "These converters appear in the picker with no group, so they land in one flat list:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, ungrouped.Select(type => "  - " + type.Name)));
        }

        /// <summary>
        /// A picker tooltip must not have a hole where a type name should be.
        /// </summary>
        /// <remarks>
        /// Tooltips are derived from each converter's XML <c>&lt;summary&gt;</c>, and the derivation used
        /// to drop <c>&lt;see cref="…"/&gt;</c> elements instead of substituting the name they referred
        /// to. Both symptoms are exact: a doubled space where the element was, or a trailing space.
        /// </remarks>
        [Test]
        public void EveryPickerTooltipIsWhole()
        {
            var broken = ConverterTypes()
                .Select(type => (type, tooltip: type
                    .GetCustomAttribute<Aspid.FastTools.Types.TypeSelectorDisplayAttribute>(inherit: false)
                    ?.Tooltip))
                .Where(pair => !string.IsNullOrEmpty(pair.tooltip))
                .Where(pair => pair.tooltip!.Contains("  ") || pair.tooltip.Trim() != pair.tooltip)
                .ToArray();

            Assert.IsEmpty(
                broken,
                "These picker tooltips have a gap where a type name belongs:"
                + Environment.NewLine
                + string.Join(
                    Environment.NewLine,
                    broken.Select(pair => $"  - {pair.type.Name}: \"{pair.tooltip}\"")));
        }

        // Guards the check above from passing because the scan stopped finding fields.
        [Test]
        public void TheScanSeesTheSerializedFields() =>
            Assert.That(
                ConverterTypes().SelectMany(SerializedFields).Count(),
                Is.GreaterThan(30),
                "serialized converter fields found");

        private static IEnumerable<Type> ConverterTypes() => new[]
            {
                typeof(IConverter).Assembly,
                typeof(IConverterVector3).Assembly,
            }
            .Distinct()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsInterface)
            .Where(type => typeof(IConverter).IsAssignableFrom(type));

        private static IEnumerable<FieldInfo> SerializedFields(Type type) => type
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
            .Where(IsSerialized);

        // Unity serializes a public field implicitly, and a private one only when it is marked. A
        // field marked NonSerialized is state rather than configuration and never reaches the
        // Inspector.
        private static bool IsSerialized(FieldInfo field)
        {
            if (field.IsDefined(typeof(NonSerializedAttribute), inherit: false)) return false;
            if (field.IsDefined(typeof(SerializeField), inherit: false)) return true;
            if (field.IsDefined(typeof(SerializeReference), inherit: false)) return true;

            return field.IsPublic;
        }

        private static string Explain(IReadOnlyCollection<(Type Type, FieldInfo Field)> undocumented)
        {
            if (undocumented.Count == 0) return string.Empty;

            var message = new StringBuilder()
                .AppendLine("These serialized converter fields have no [Tooltip]. They are configured in")
                .AppendLine("the Inspector, where XML documentation is invisible, so the tooltip is the")
                .AppendLine("only explanation their reader will ever see:")
                .AppendLine();

            foreach (var (type, field) in undocumented)
                message.Append("  - ").Append(type.Name).Append('.').AppendLine(field.Name);

            return message.ToString();
        }
    }
}
