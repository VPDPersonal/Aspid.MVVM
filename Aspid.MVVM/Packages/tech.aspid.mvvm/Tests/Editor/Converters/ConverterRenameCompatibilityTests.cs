using System;
using System.Linq;
using NUnit.Framework;
using System.Reflection;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// A tripwire on the names that cannot be changed without losing authored data.
    /// </summary>
    /// <remarks>
    /// <c>[MovedFrom]</c> and <c>[FormerlySerializedAs]</c> cover an object's own serialized data, but
    /// <b>not</b> a prefab-instance override, which is keyed by the stored type string and the property
    /// path. Renaming <c>SequenceConverters</c> emptied a converter a shipped sample scene had authored
    /// as an override, with <c>[MovedFrom]</c> present and correct.
    /// <para>
    /// So these names are frozen: renaming one is a data migration needing an upgrade script over every
    /// scene and prefab, and this fixture says so at the moment somebody tries.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ConverterRenameCompatibilityTests
    {
        [TestCase("SequenceConverters`1")]
        [TestCase("GenericToString`1")]
        public void FrozenConverterTypeName_IsUnchanged(string name)
        {
            var found = typeof(IConverter).Assembly.GetTypes().Any(type => type.Name == name);

            Assert.IsTrue(
                found,
                $"{name} is gone or renamed. Its name is stored verbatim in every scene and prefab "
                + "that uses it, and a prefab-instance override will not be remapped by [MovedFrom]. "
                + "Renaming it silently empties the field. If the rename is genuinely wanted, it "
                + "needs an upgrade script that rewrites the stored type strings.");
        }

        [TestCase(typeof(Vector3CombineConverter), "_preConvertor")]
        [TestCase(typeof(Vector3CombineConverter), "_postConvertor")]
        [TestCase(typeof(Vector2CombineConverter), "_preConvertor")]
        [TestCase(typeof(Vector2CombineConverter), "_postConvertor")]
        [TestCase(typeof(Vector2ToVector3Converter), "_values")]
        [TestCase(typeof(Vector3ToVector2Converter), "_values")]
        public void FrozenSerializedFieldName_IsUnchanged(Type type, string field)
        {
            var found = type.GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(
                found,
                $"{type.Name}.{field} is gone or renamed. The name is the property path a "
                + "prefab-instance override is keyed by, and [FormerlySerializedAs] does not reach "
                + "those — the override is dropped on load without a diagnostic. "
                + "The spelling of _preConvertor is deliberate; it is not a typo left unfixed.");
        }
    }
}
