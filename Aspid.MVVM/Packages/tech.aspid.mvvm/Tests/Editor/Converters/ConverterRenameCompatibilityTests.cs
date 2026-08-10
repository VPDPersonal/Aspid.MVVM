using System;
using System.Linq;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.Serialization;
using UnityEngine.Scripting.APIUpdating;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// The renames of this release must keep loading scenes authored before them.
    /// </summary>
    /// <remarks>
    /// A <c>[SerializeReference]</c> value is stored by type name and reached by field name. Rename
    /// either and Unity finds nothing where the reference used to be, sets it to
    /// <see langword="null"/>, and says nothing — the binder simply stops converting. Two attributes
    /// prevent that, and both were confirmed against a real asset before this test was written:
    /// YAML rewritten to the old name reimported into a live instance of the new type.
    /// <para>
    /// What a test can still add is that nobody removes the attributes. They look like clutter years
    /// from now, and the thing they protect gives no sign when it breaks.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ConverterRenameCompatibilityTests
    {
        [TestCase(typeof(SequenceConverter<>), "SequenceConverters")]
        [TestCase(typeof(GenericToStringConverter<>), "GenericToString")]
        public void RenamedConverter_KeepsItsFormerTypeName(Type type, string formerName)
        {
            var moved = type.GetCustomAttribute<MovedFromAttribute>(inherit: false);

            Assert.IsNotNull(
                moved,
                $"{type.Name} was renamed from {formerName} and carries no [MovedFrom]. Every scene "
                + "that stores one loses it on load, without a diagnostic.");

            Assert.AreEqual(
                formerName,
                FormerClassName(moved!),
                $"[MovedFrom] on {type.Name} names the wrong former type.");
        }

        // MovedFromAttribute keeps its payload in an internal `data` field, so the only way to read
        // back what it was given is reflection. Worth it: an attribute that names the wrong former
        // type is indistinguishable from a correct one until a scene silently loses its converter.
        private static string? FormerClassName(MovedFromAttribute attribute)
        {
            var data = typeof(MovedFromAttribute)
                .GetField("data", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(attribute);

            return data?.GetType()
                .GetField("className", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.GetValue(data) as string;
        }

        [TestCase(typeof(Vector3CombineConverter), "_preConverter", "_preConvertor")]
        [TestCase(typeof(Vector3CombineConverter), "_postConverter", "_postConvertor")]
        [TestCase(typeof(Vector2CombineConverter), "_preConverter", "_preConvertor")]
        [TestCase(typeof(Vector2CombineConverter), "_postConverter", "_postConvertor")]
        [TestCase(typeof(Vector2ToVector3Converter), "_mode", "_values")]
        [TestCase(typeof(Vector3ToVector2Converter), "_mode", "_values")]
        public void RenamedField_KeepsItsFormerName(Type type, string field, string formerName)
        {
            var info = type.GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(info, $"{type.Name} has no field {field}.");

            var former = info!.GetCustomAttributes<FormerlySerializedAsAttribute>(inherit: false)
                .Select(attribute => attribute.oldName)
                .ToArray();

            Assert.Contains(
                formerName,
                former,
                $"{type.Name}.{field} was renamed from {formerName} and carries no "
                + "[FormerlySerializedAs], so its authored value is dropped on load.");
        }
    }
}
