using System;
using System.Linq;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Every binder that carries a converter and can send a value back must convert it back.
    /// </summary>
    /// <remarks>
    /// This fixture does not name binders: it walks the assemblies, finds every type that owns a
    /// converter field and a reverse channel, and requires the reverse path to exist.
    /// </remarks>
    [TestFixture]
    public sealed class ReverseConversionCoverageTests
    {
        [Test]
        public void EveryBinderWithAConverterAndAReverseChannel_ConvertsOnTheWayBack()
        {
            var missing = ReverseBindersWithAConverter()
                .Where(type => !HasReversePath(type))
                .ToArray();

            Assert.IsEmpty(
                missing,
                "These binders hold a converter and can push a value to the ViewModel, but never call " +
                "ConvertBack. Either they send the View's presentation back as if it were the " +
                "ViewModel's own value, or they send the raw value while the guide promises " +
                "otherwise:" +
                Environment.NewLine +
                string.Join(Environment.NewLine, missing.Select(type => "  - " + type.Name)));
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

        // The reverse path is either GetConvertedBackValue, inherited from the fixed base, or a ConvertBack
        // call inside one of the binder's own methods, as the binders with their own converter field make it.
        private static bool HasReversePath(Type type)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

            for (var current = type; current is not null && current != typeof(object); current = current.BaseType)
            {
                var methods = current.GetMethods(flags);

                if (methods.Any(method => method.Name is "GetConvertedBackValue" || CallsConvertBack(method))) return true;
            }

            return false;
        }

        // Every call and callvirt operand in the IL is resolved and checked by name. The bytes are scanned rather
        // than decoded, so an operand-sized run of other instructions can also be tried; it resolves to nothing
        // or to another method.
        private static bool CallsConvertBack(MethodInfo method)
        {
            const byte call = 0x28;
            const byte callvirt = 0x6F;

            var il = method.GetMethodBody()?.GetILAsByteArray();
            if (il is null) return false;

            for (var index = 0; index + 4 < il.Length; index++)
            {
                if (il[index] is not (call or callvirt)) continue;

                var token = BitConverter.ToInt32(il, index + 1);
                if (token >> 24 is not (0x06 or 0x0A or 0x2B)) continue;

                try
                {
                    var callee = method.Module.ResolveMethod(
                        token,
                        method.DeclaringType?.GetGenericArguments(),
                        method.IsGenericMethod ? method.GetGenericArguments() : null);

                    if (callee.Name is "ConvertBack") return true;
                }
                catch (ArgumentException) { }
            }

            return false;
        }

        private static IEnumerable<FieldInfo> Declared(Type type) => type
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
    }
}
