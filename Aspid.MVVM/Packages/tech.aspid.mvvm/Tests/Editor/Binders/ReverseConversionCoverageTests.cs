using System;
using System.Linq;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Every binder that carries a converter and can send a value back must convert it back.
    /// </summary>
    /// <remarks>
    /// Three separate rounds of this fix each left binders behind, because each looked for the shape
    /// it had just fixed rather than for the property itself. The base
    /// <see cref="TargetBinder{TTarget, TProperty, TConverter}"/> was fixed first; four binders with
    /// a private converter field were found later by grepping for the symptom; two more —
    /// <c>RendererMaterials</c> — hid because the conversion sat in a loop rather than on the line
    /// that raised the event.
    /// <para>
    /// So this fixture does not name binders. It walks the assemblies, finds every type that owns a
    /// converter field and a reverse channel, and requires the reverse path to exist. A binder added
    /// tomorrow is covered without anyone remembering to add it here.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ReverseConversionCoverageTests
    {
        [Test]
        public void EveryBinderWithAConverterAndAReverseChannel_ConvertsOnTheWayBack()
        {
            var missing = ReverseBindersWithAConverter()
                .Where(type => !HasReversePath(type))
                .ToArray();

            Assert.IsEmpty(
                missing,
                "These binders hold a converter and can push a value to the ViewModel, but never call "
                + "ConvertBack. Either they send the View's presentation back as if it were the "
                + "ViewModel's own value, or they send the raw value while the guide promises "
                + "otherwise:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, missing.Select(type => "  - " + type.Name)));
        }

        // Guards the check above from passing because the scan stopped finding binders.
        [Test]
        public void TheScanSeesTheBindersItGuards() =>
            Assert.That(ReverseBindersWithAConverter().Count(), Is.GreaterThan(10), "binders found");

        private static IEnumerable<Type> ReverseBindersWithAConverter() => new[]
            {
                typeof(IConverter).Assembly,
                typeof(SpriteToTextureConverter).Assembly,
            }
            .Distinct()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsInterface && !type.IsAbstract)
            .Where(CanSendItsConvertedTypeBack);

        // The property that matters is narrower than "has a converter and some reverse contract":
        // the binder must be able to send back a value OF THE TYPE ITS CONVERTER CONVERTS. That is
        // what makes the forward converter applicable in the wrong direction, and it is what makes
        // ConvertBack meaningful.
        //
        // It excludes two families that would otherwise look guilty. The Animator binders convert a
        // float or an int but reverse an Action<T> and an IRelayCommand<T> — they hand the ViewModel
        // a callback, not a value, and there is nothing to convert back. The Debug binders implement
        // the untyped IAnyReverseBinder and never raise it at all; its add and remove only log.
        private static bool CanSendItsConvertedTypeBack(Type type)
        {
            var converted = ConvertedTypes(type).ToArray();
            if (converted.Length == 0) return false;

            var reversed = type.GetInterfaces()
                .Where(contract => contract.IsGenericType)
                .Where(contract => contract.GetGenericTypeDefinition() == typeof(IReverseBinder<>))
                .Select(contract => contract.GetGenericArguments()[0])
                .ToArray();

            return converted.Any(reversed.Contains);
        }

        // The TFrom of every converter field the binder owns, including inherited ones.
        private static IEnumerable<Type> ConvertedTypes(Type type)
        {
            for (var current = type; current is not null && current != typeof(object); current = current.BaseType)
            foreach (var field in Declared(current))
            {
                if (!typeof(IConverter).IsAssignableFrom(field.FieldType)) continue;

                var closed = field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(IConverter<,>)
                    ? field.FieldType
                    : field.FieldType.GetInterfaces().FirstOrDefault(
                        contract => contract.IsGenericType && contract.GetGenericTypeDefinition() == typeof(IConverter<,>));

                if (closed is not null) yield return closed.GetGenericArguments()[0];
            }
        }

        // The reverse path is either inherited from the fixed base or declared locally. Both spell it
        // the same way, which is why the name is the thing to look for.
        private static bool HasReversePath(Type type)
        {
            for (var current = type; current is not null && current != typeof(object); current = current.BaseType)
            {
                var methods = current.GetMethods(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

                if (methods.Any(method => method.Name is "GetConvertedBackValue")) return true;
            }

            return false;
        }

        private static IEnumerable<FieldInfo> Declared(Type type) => type
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
    }
}
