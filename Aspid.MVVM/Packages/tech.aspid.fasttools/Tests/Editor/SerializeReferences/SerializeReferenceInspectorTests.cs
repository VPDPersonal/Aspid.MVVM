using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// EditMode integration coverage that needs a live <see cref="SerializedObject"/>: the rid-sharing contract of
    /// <see cref="SerializeReferenceLinker"/> (a single managedReferenceId across two fields) and the
    /// <see cref="SerializeReferenceRequiredGate"/> violation logic for both the managed-reference and string shapes.
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceInspectorTests
    {
        [Test]
        public void LinkTo_SharesTheSameRid()
        {
            var obj = ScriptableObject.CreateInstance<LinkerTestObject>();
            try
            {
                var serialized = new SerializedObject(obj);
                serialized.FindProperty("a").managedReferenceValue = new TestSword { damage = 7 };
                serialized.ApplyModifiedProperties();

                Assert.IsTrue(SerializeReferenceLinker.LinkTo(serialized.FindProperty("b"), "a"));
                serialized.Update();

                var ridA = serialized.FindProperty("a").managedReferenceId;
                var ridB = serialized.FindProperty("b").managedReferenceId;

                Assert.AreNotEqual(-2L, ridA, "Field 'a' should hold a real managed reference.");
                Assert.AreEqual(ridA, ridB, "Link to Existing must put both fields on a single shared managedReferenceId.");
            }
            finally { UnityEngine.Object.DestroyImmediate(obj); }
        }

        [Test]
        public void IsViolation_RequiredManagedRef_TrueWhenEmpty_FalseWhenSet()
        {
            var obj = ScriptableObject.CreateInstance<RequiredTestObject>();
            try
            {
                var serialized = new SerializedObject(obj);
                Assert.IsTrue(SerializeReferenceRequiredGate.IsViolation(serialized.FindProperty("requiredRef")),
                    "An empty required managed reference is a violation.");

                var prop = serialized.FindProperty("requiredRef");
                prop.managedReferenceValue = new TestSword();
                serialized.ApplyModifiedProperties();
                serialized.Update();

                Assert.IsFalse(SerializeReferenceRequiredGate.IsViolation(serialized.FindProperty("requiredRef")),
                    "A set required managed reference is not a violation.");
            }
            finally { UnityEngine.Object.DestroyImmediate(obj); }
        }

        [Test]
        public void IsViolation_RequiredInNestedContainer_TrueWhenEmpty_FalseWhenSet()
        {
            // Parity pin for ASP-52: the live half resolves the attribute through the container hop, so the
            // inspector notice and the YAML scan agree on nested required fields.
            var obj = ScriptableObject.CreateInstance<NestedRequiredTestObject>();
            try
            {
                var serialized = new SerializedObject(obj);
                Assert.IsTrue(SerializeReferenceRequiredGate.IsViolation(serialized.FindProperty("_loadout.primary")),
                    "An empty required managed reference inside a serializable container is a violation.");

                var prop = serialized.FindProperty("_loadout.primary");
                prop.managedReferenceValue = new TestSword();
                serialized.ApplyModifiedProperties();
                serialized.Update();

                Assert.IsFalse(SerializeReferenceRequiredGate.IsViolation(serialized.FindProperty("_loadout.primary")),
                    "A set nested required managed reference is not a violation.");
            }
            finally { UnityEngine.Object.DestroyImmediate(obj); }
        }

        [Test]
        public void IsViolation_RequiredString_TrueWhenEmpty_FalseWhenSet()
        {
            var obj = ScriptableObject.CreateInstance<RequiredTestObject>();
            try
            {
                var serialized = new SerializedObject(obj);
                Assert.IsTrue(SerializeReferenceRequiredGate.IsViolation(serialized.FindProperty("requiredString")),
                    "An empty required string type field is a violation.");

                serialized.FindProperty("requiredString").stringValue = "Some.Namespace.SomeType, Some.Assembly";
                serialized.ApplyModifiedProperties();
                serialized.Update();

                Assert.IsFalse(SerializeReferenceRequiredGate.IsViolation(serialized.FindProperty("requiredString")),
                    "A populated required string type field is not a violation.");
            }
            finally { UnityEngine.Object.DestroyImmediate(obj); }
        }
    }
}
