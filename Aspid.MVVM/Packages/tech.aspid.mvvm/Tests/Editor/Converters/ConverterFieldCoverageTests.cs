using System;
using System.Linq;
using System.Text;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using Aspid.FastTools.Types;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Every <c>[SerializeReference]</c> converter field should offer something. A field whose type
    /// has no pickable implementation renders as a dropdown containing only <c>&lt;None&gt;</c> —
    /// a control that looks configurable and cannot be configured.
    /// </summary>
    /// <remarks>
    /// A candidate is a concrete type assignable to the field, constructible by the picker, and not
    /// hidden from it. Open generic converters count when they can be closed over the field's own
    /// conversion types, which is what the picker does for a generic field.
    /// <para>
    /// Composition containers do not count. <see cref="SequenceConverters{T}"/> is assignable to
    /// every same-type field, so counting it would mark every such field covered while the dropdown
    /// offers nothing but an empty chain to put converters into — the vacuous pass this test exists
    /// to prevent.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ConverterFieldCoverageTests
    {
        /// <summary>
        /// Types that hold other converters rather than convert anything themselves.
        /// </summary>
        private static readonly HashSet<Type> Containers = new()
        {
            typeof(SequenceConverters<>),
        };

        /// <summary>
        /// Field types with no pickable converter yet, each naming the catalogue family that will
        /// fill it. Shrinking this list is the point of it — <see cref="TheGapListHasNoStaleEntries"/>
        /// fails once an entry stops being a gap.
        /// </summary>
        private static readonly Dictionary<string, string> KnownGaps = new()
        {
            ["IConverter<Color, Color>"] = "family 9 (Colour) — ColorAlpha, ColorTint, ColorGrayscale…",
            ["IConverter<ColorBlock, ColorBlock>"] = "family 10 (ColorBlock) — ColorToColorBlock…",
            ["IConverter<Enum, IEnumerable<OptionData>>"] = "family 16 (Enum) — EnumToDropdownOptionData",
            ["IConverter<Material, Material>"] = "family 12 (Assets) — IndexToMaterial, MaterialInstance…",
            ["IConverter<Mesh, Mesh>"] = "family 12 (Assets) — IndexToMesh, BoolToMesh…",
            ["IConverter<PhysicsMaterial, PhysicsMaterial>"] = "family 12 (Assets) — IndexToPhysicsMaterial…",
            ["IConverter<Quaternion, Quaternion>"] = "family 14 (Rotations) — AngleToQuaternion, LookRotation…",
            ["IConverter<RectOffset, RectOffset>"] = "family 13 (Layout) — IntToRectOffset, RectOffsetScale…",
            ["IConverter<Texture, Texture>"] = "family 11 (Textures) — IndexToTexture, BoolToTexture…",
        };

        [Test]
        public void EveryConverterFieldHasAPickableConverter()
        {
            var uncovered = ConverterFieldTypes()
                .Where(type => !KnownGaps.ContainsKey(Describe(type)))
                .Where(type => !Candidates(type).Any())
                .ToArray();

            Assert.IsEmpty(uncovered, Explain(uncovered));
        }

        [Test]
        public void TheGapListHasNoStaleEntries()
        {
            var byName = new Dictionary<string, Type>();
            foreach (var type in ConverterFieldTypes())
                byName[Describe(type)] = type;

            var filled = KnownGaps.Keys
                .Where(name => !byName.ContainsKey(name) || Candidates(byName[name]).Any())
                .ToArray();

            Assert.IsEmpty(
                filled,
                "These entries are no longer gaps — the field is gone, or it now has a pickable "
                + "converter. Drop them from KnownGaps:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, filled.Select(name => "  - " + name)));
        }

        [Test]
        public void TheScanSeesTheConverterFields() =>
            Assert.That(ConverterFieldTypes().Count(), Is.GreaterThan(10));

        private static IEnumerable<Type> ConverterFieldTypes() => Assemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsAbstract && !type.ContainsGenericParameters)
            .SelectMany(SerializedReferenceFields)
            .Select(field => field.FieldType)
            // Still open after substitution — nothing is ever picked against it.
            .Where(type => !type.ContainsGenericParameters)
            .Where(type => typeof(IConverter).IsAssignableFrom(type))
            .Distinct();

        // Most converter fields are private and declared on a generic binder base, so DeclaredOnly on
        // the concrete type misses them and inheritance does not surface private fields. Walking the
        // base chain does both: each base is already a constructed type, so reflection hands back the
        // field with its type arguments substituted — `TConverter` arrives as `IConverterColor`.
        private static IEnumerable<FieldInfo> SerializedReferenceFields(Type type)
        {
            for (var current = type; current is not null && current != typeof(object); current = current.BaseType)
            {
                var fields = current.GetFields(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

                foreach (var field in fields)
                    if (field.IsDefined(typeof(SerializeReference), inherit: false))
                        yield return field;
            }
        }

        private static IEnumerable<Assembly> Assemblies() => new[]
        {
            typeof(IConverter).Assembly,
            typeof(IConverterVector3).Assembly,
            typeof(MonoBinder).Assembly,
            typeof(Binder).Assembly,
        }.Distinct();

        private static IEnumerable<Type> Candidates(Type fieldType) => Assemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsInterface && !type.IsAbstract)
            .Where(type => typeof(IConverter).IsAssignableFrom(type))
            .Where(type => !Containers.Contains(type))
            .Where(IsConstructible)
            .Where(type => !IsHidden(type))
            .Select(type => Close(type, fieldType))
            .Where(type => type is not null && fieldType.IsAssignableFrom(type));

        // The picker closes an open generic over the field's own conversion types. Only the arities
        // the package actually ships are attempted: one type argument taken from either side of the
        // conversion, or two taken from both.
        private static Type Close(Type type, Type fieldType)
        {
            if (!type.IsGenericTypeDefinition) return type;
            if (!TryGetConversion(fieldType, out var from, out var to)) return null;

            return type.GetGenericArguments().Length switch
            {
                1 => TryMake(type, from) ?? TryMake(type, to),
                2 => TryMake(type, from, to),
                _ => null,
            };
        }

        private static Type TryMake(Type definition, params Type[] arguments)
        {
            try
            {
                return definition.MakeGenericType(arguments);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private static bool TryGetConversion(Type type, out Type from, out Type to)
        {
            var converter = IsClosedConverter(type)
                ? type
                : type.GetInterfaces().FirstOrDefault(IsClosedConverter);

            if (converter is null)
            {
                from = to = null;
                return false;
            }

            var arguments = converter.GetGenericArguments();
            from = arguments[0];
            to = arguments[1];
            return true;
        }

        private static bool IsClosedConverter(Type type) =>
            type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IConverter<,>);

        private static bool IsConstructible(Type type) => type.GetConstructor(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            binder: null,
            Type.EmptyTypes,
            modifiers: null) is not null;

        private static bool IsHidden(Type type) =>
            type.GetCustomAttribute<TypeSelectorDisplayAttribute>(inherit: false)?.Hidden is true;

        // Markers and their generic aliases both appear as field types; a readable rendering keeps
        // the gap list legible and stable across the two spellings.
        private static string Describe(Type type)
        {
            if (!type.IsGenericType) return type.Name;

            var name = type.Name;
            var arity = name.IndexOf('`');
            if (arity >= 0) name = name[..arity];

            return $"{name}<{string.Join(", ", type.GetGenericArguments().Select(Describe))}>";
        }

        private static string Explain(IReadOnlyCollection<Type> uncovered)
        {
            if (uncovered.Count == 0) return string.Empty;

            var message = new StringBuilder()
                .AppendLine("These converter field types have no pickable converter, so their Inspector")
                .AppendLine("dropdown offers nothing but <None>. Either ship a converter for them, drop")
                .AppendLine("the field, or record the gap in KnownGaps with the family that will fill it:")
                .AppendLine();

            foreach (var type in uncovered)
                message.Append("  - ").AppendLine(Describe(type));

            return message.ToString();
        }
    }
}
