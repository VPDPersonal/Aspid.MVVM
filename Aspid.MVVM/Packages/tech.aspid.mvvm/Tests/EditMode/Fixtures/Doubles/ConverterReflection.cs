#nullable enable
using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Builds and field-pokes composition-converter wrappers the way the Inspector does: through a
    /// non-public parameterless constructor and reflection over serialized fields.
    /// </summary>
    internal static class ConverterReflection
    {
        /// <summary>
        /// Builds <typeparamref name="T"/> the way the type picker builds it —
        /// <c>Activator.CreateInstance(type, nonPublic: true)</c> — with every field left at its
        /// default.
        /// </summary>
        public static T Empty<T>()
            where T : class =>
            (T)Activator.CreateInstance(typeof(T), nonPublic: true)!;

        /// <summary>
        /// Sets a private instance field on <paramref name="target"/> by name, failing the test if the
        /// field does not exist.
        /// </summary>
        public static T SetField<T>(T target, string name, object? value)
            where T : class
        {
            var field = target!.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            NUnit.Framework.Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");

            field!.SetValue(target, value);
            return target;
        }
    }
}
