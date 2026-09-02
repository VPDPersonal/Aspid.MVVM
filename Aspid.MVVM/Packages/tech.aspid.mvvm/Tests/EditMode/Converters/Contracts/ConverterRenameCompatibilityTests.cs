using System;
using System.Linq;
using NUnit.Framework;
using System.Reflection;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A tripwire on the names that cannot be changed without migrating authored data.
    /// </summary>
    /// <remarks>
    /// <c>[MovedFrom]</c> and <c>[FormerlySerializedAs]</c> do not cover prefab-instance overrides, which are keyed by the stored type string.
    /// After a deliberate rename, run the repair tool over every scene and prefab, then update the names here.
    /// </remarks>
    [TestFixture]
    public sealed class ConverterRenameCompatibilityTests
    {
        [TestCase("SequenceConverter`1")]
        [TestCase("GenericToStringConverter`1")]
        public void FrozenConverterTypeName_IsUnchanged(string name)
        {
            var found = typeof(IConverter).Assembly.GetTypes().Any(type => type.Name == name);

            Assert.IsTrue(
                found,
                $"{name} is gone or renamed. Its name is stored verbatim in every scene and prefab " +
                "that uses it, and a prefab-instance override will not be remapped by [MovedFrom]. " +
                "Renaming it silently empties the field. If the rename is deliberate, run the repair " +
                "tool that rewrites the stored type strings over every scene and prefab, then update " +
                "this test case to the new name.");
        }

        [TestCase(typeof(Vector3CombineConverter), "_preConvertor")]
        [TestCase(typeof(Vector3CombineConverter), "_postConvertor")]
        [TestCase(typeof(Vector2CombineConverter), "_preConvertor")]
        [TestCase(typeof(Vector2CombineConverter), "_postConvertor")]
        [TestCase(typeof(Vector2Vector3Converter), "_values")]
        public void FrozenSerializedFieldName_IsUnchanged(Type type, string field)
        {
            var found = type.GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(
                found,
                $"{type.Name}.{field} is gone or renamed. The name is the property path a " +
                "prefab-instance override is keyed by, and [FormerlySerializedAs] does not reach " +
                "those — the override is dropped on load without a diagnostic. " +
                "The spelling of _preConvertor is deliberate; it is not a typo left unfixed.");
        }
    }
}
