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
