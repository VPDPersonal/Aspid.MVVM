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
    /// A <c>[SerializeReference]</c> value is stored by type name and reached by field name. Unity
    /// offers <c>[MovedFrom]</c> and <c>[FormerlySerializedAs]</c> to survive renaming either — and
    /// both were measured here rather than assumed, because the failure they are supposed to prevent
    /// is silent.
    /// <para>
    /// They cover an object's own serialized data. They do <b>not</b> cover a prefab-instance
    /// override, which is keyed by the stored type string and the property path. Renaming
    /// <c>SequenceConverters</c> to <c>SequenceConverter</c> emptied a converter that a shipped
    /// sample scene had authored as an override — 24 console errors and one binder that stopped
    /// converting, with `[MovedFrom]` present and correct. The same test on a renamed field lost the
    /// override too. Authoring a prefab and tweaking one instance is the common case, not an edge one.
    /// </para>
    /// <para>
    /// So these names are frozen. Renaming one is not a refactor; it is a data migration, and it
    /// needs an upgrade script that rewrites the stored strings in every scene and prefab. This
    /// fixture exists to say that at the moment somebody tries, rather than after a user reports a
    /// binder that quietly does nothing.
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
