using System;
using System.Linq;
using NUnit.Framework;
using System.Reflection;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// The named converter aliases and their wrappers are on their way out, and must say so.
    /// </summary>
    /// <remarks>
    /// Forty <c>IConverterXToY</c> interfaces and seventy <c>ToConvert</c> wrappers exist for one
    /// reason: Unity before 2023.1 could not serialize a <c>[SerializeReference]</c> field of an open
    /// generic type. The package requires Unity 6000.0, so that reason is gone, but the surface has to
    /// outlive it by a release — a field a project declares as one of these would otherwise
    /// deserialize to <see langword="null"/> without a word.
    /// <para>
    /// The risk this guards is a new alias being added out of habit, or an existing one losing its
    /// attribute in a merge. Either would restart the clock on a deprecation that is meant to end.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ConverterDeprecationTests
    {
        [Test]
        public void EveryNamedConverterAliasIsObsolete()
        {
            var live = NamedAliases()
                .Where(type => !type.IsDefined(typeof(ObsoleteAttribute), inherit: false))
                .ToArray();

            Assert.IsEmpty(
                live,
                "These named converter aliases are not marked [Obsolete], so nothing tells a project "
                + "to move off them before they are removed:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, live.Select(type => "  - " + type.Name)));
        }

        [Test]
        public void EveryConverterWrapperIsObsolete()
        {
            var live = Wrappers()
                .Where(method => !method.IsDefined(typeof(ObsoleteAttribute), inherit: false))
                .Where(method => !method.DeclaringType!.IsDefined(typeof(ObsoleteAttribute), inherit: false))
                .ToArray();

            Assert.IsEmpty(
                live,
                "These converter wrappers are not marked [Obsolete]:"
                + Environment.NewLine
                + string.Join(
                    Environment.NewLine,
                    live.Select(method => $"  - {method.DeclaringType!.Name}.{method.Name}")));
        }

        // Both checks above pass vacuously if the scan stops finding the surface it guards. The
        // counts are the ones the deprecation was written against; they may only fall.
        [Test]
        public void TheScanSeesTheSurfaceItGuards()
        {
            Assert.That(NamedAliases().Count(), Is.EqualTo(40), "named converter aliases");
            Assert.That(Wrappers().Count(), Is.EqualTo(70), "ToConvert / ToConvertSpecific wrappers");
        }

        private static IEnumerable<Type> Assemblies() => new[]
            {
                typeof(IConverter).Assembly,
                typeof(SpriteToTextureConverter).Assembly,
            }
            .Distinct()
            .SelectMany(assembly => assembly.GetTypes());

        // An alias carries no members of its own and closes IConverter<,> over two concrete types.
        // That is exactly what separates it from IConverter itself and from ITwoWayConverter.
        private static IEnumerable<Type> NamedAliases() => Assemblies()
            .Where(type => type.IsInterface)
            .Where(type => type != typeof(IConverter))
            .Where(type => typeof(IConverter).IsAssignableFrom(type))
            .Where(type => !type.IsGenericType)
            .Where(type => type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Length == 0);

        // ConverterExtensions.ToConvert<TFrom, TTo> shares the name but is the replacement, not the
        // legacy: it wraps a lambda as IConverter<TFrom, TTo> and outlives every alias here. The
        // wrappers being deprecated are all non-generic, one per named alias.
        private static IEnumerable<MethodInfo> Wrappers() => Assemblies()
            .Where(type => type.IsSealed && type.IsAbstract) // static class
            .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
            .Where(method => method.Name is "ToConvert" or "ToConvertSpecific")
            .Where(method => !method.IsGenericMethodDefinition);
    }
}
