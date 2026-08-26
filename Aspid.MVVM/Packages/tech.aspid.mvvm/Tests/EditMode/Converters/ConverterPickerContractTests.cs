using System;
using System.Linq;
using System.Text;
using System.Reflection;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that the type picker only offers converters an Inspector can actually build.
    /// </summary>
    /// <remarks>
    /// A converter with no parameterless constructor cannot be created by the picker: it falls back to
    /// <c>FormatterServices.GetUninitializedObject</c>, which leaves every field null and produces an instance that
    /// throws on the first pushed value. Such a type has to carry <see cref="TypeSelectorDisplayAttribute.Hidden"/>.
    /// The whole family was marked once by hand and one region was missed, so the rule is asserted over the
    /// assemblies instead of trusted to review.
    /// </remarks>
    [TestFixture]
    public sealed class ConverterPickerContractTests
    {
        private static readonly Assembly[] ConverterAssemblies =
        {
            typeof(StringFormatConverter).Assembly,
            typeof(IConverter<,>).Assembly,
        };

        [Test]
        public void EveryConverterWithoutAParameterlessConstructor_IsHiddenFromThePicker()
        {
            var offenders = new StringBuilder();

            foreach (var type in ConverterAssemblies.Distinct().SelectMany(assembly => assembly.GetTypes()))
            {
                if (!IsConverter(type)) continue;
                if (type.IsAbstract || type.IsInterface) continue;
                if (HasParameterlessConstructor(type)) continue;
                if (IsHiddenFromPicker(type)) continue;

                offenders.AppendLine($"    {type.FullName}");
            }

            Assert.IsEmpty(
                offenders.ToString(),
                "Эти конвертеры пикер предложит, но создать не сможет — им нужен [TypeSelectorDisplay(Hidden = true)]:\n"
                + offenders);
        }

        private static bool IsConverter(Type type) =>
            type.GetInterfaces().Any(@interface =>
                @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IConverter<,>));

        private static bool HasParameterlessConstructor(Type type) =>
            type.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                binder: null,
                Type.EmptyTypes,
                modifiers: null) is not null;

        private static bool IsHiddenFromPicker(Type type) =>
            type.GetCustomAttribute<TypeSelectorDisplayAttribute>(inherit: false)?.Hidden ?? false;
    }
}
