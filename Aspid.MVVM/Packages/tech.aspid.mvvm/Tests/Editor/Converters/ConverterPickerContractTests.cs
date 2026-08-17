using System;
using System.Linq;
using System.Text;
using System.Reflection;
using NUnit.Framework;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// These fixtures name the [Obsolete] converter aliases on purpose — guarding the deprecated
// surface is the point.
#pragma warning disable CS0618 // Type or member is obsolete

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Contract between the shipped converters and the <c>[SerializeReference]</c> type picker:
    /// a type the picker cannot construct must not be offered.
    /// </summary>
    /// <remarks>
    /// The picker falls back to <c>FormatterServices.GetUninitializedObject</c> when
    /// <see cref="Activator"/> fails, which skips field initialisers and leaves a delegate-backed
    /// adapter with a null delegate. It filters by neither accessibility nor constructor, so the
    /// guard has to live on the type as <c>[TypeSelectorDisplay(Hidden = true)]</c>.
    /// </remarks>
    [TestFixture]
    internal sealed class ConverterPickerContractTests
    {
        [Test]
        public void EveryConverterWithoutAParameterlessConstructor_IsHiddenFromThePicker()
        {
            var offenders = ConverterTypes()
                .Where(type => !HasParameterlessConstructor(type))
                .Where(type => !IsHidden(type))
                .ToArray();

            Assert.IsEmpty(offenders, Explain(offenders));
        }

        [Test]
        public void EveryConverterTheInspectorCanAuthor_IsVisibleInThePicker()
        {
            var hidden = ConverterTypes()
                .Where(HasParameterlessConstructor)
                .Where(IsHidden)
                .ToArray();

            Assert.IsEmpty(
                hidden,
                "These converters can be constructed by the picker but are hidden from it. "
                + "If that is intentional the exclusion belongs in this test:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, hidden.Select(t => "  - " + t.FullName)));
        }

        // Guards against the two tests above passing vacuously: both are "no offenders" assertions,
        // so if the scan ever stopped seeing the private adapters — a changed namespace, an assembly
        // rename, an attribute the compiler stopped emitting — they would go green while the picker
        // filled back up with types it cannot construct.
        [Test]
        public void TheScanSeesBothPopulationsItGuards()
        {
            var types = ConverterTypes().ToArray();

            Assert.That(types.Length, Is.GreaterThan(30), "converter types found");
            Assert.That(
                types.Count(t => !HasParameterlessConstructor(t)),
                Is.GreaterThan(20),
                "converters the picker cannot construct — the population the hidden-check guards");
            Assert.That(types.Count(IsHidden), Is.GreaterThan(20), "converters carrying the attribute");
        }

        private static IEnumerable<Type> ConverterTypes() => Assemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsInterface && !type.IsAbstract)
            .Where(type => typeof(IConverter).IsAssignableFrom(type))
            // Converter assets are UnityEngine.Objects, which a managed reference cannot hold, so the
            // picker never offers them and neither rule here applies.
            .Where(type => !typeof(UnityEngine.Object).IsAssignableFrom(type));

        private static IEnumerable<Assembly> Assemblies() => new[]
        {
            typeof(IConverter).Assembly,
            typeof(IConverterVector3).Assembly,
        }.Distinct();

        // The picker calls Activator.CreateInstance(type, nonPublic: true), so a private or
        // protected parameterless constructor is enough to make a type constructible.
        private static bool HasParameterlessConstructor(Type type) => type.GetConstructor(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            binder: null,
            Type.EmptyTypes,
            modifiers: null) is not null;

        private static bool IsHidden(Type type) =>
            type.GetCustomAttribute<TypeSelectorDisplayAttribute>(inherit: false)?.Hidden is true;

        private static string Explain(IReadOnlyCollection<Type> offenders)
        {
            if (offenders.Count == 0) return string.Empty;

            var message = new StringBuilder()
                .AppendLine("These converters have no parameterless constructor, so picking one from the")
                .AppendLine("Inspector yields an instance with null fields and throws on the first push.")
                .AppendLine("Mark each with [TypeSelectorDisplay(Hidden = true)]:")
                .AppendLine();

            foreach (var type in offenders)
                message.Append("  - ").AppendLine(type.FullName);

            return message.ToString();
        }
    }
}
