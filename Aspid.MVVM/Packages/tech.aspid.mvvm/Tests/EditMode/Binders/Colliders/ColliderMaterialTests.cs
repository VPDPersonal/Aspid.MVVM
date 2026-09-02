using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests asserting that reading a collider's material does not replace it with a clone.
    /// </summary>
    /// <remarks>
    /// Reading <see cref="Collider.material"/> instantiates a private copy, so the binders read through <see cref="Collider.sharedMaterial"/>
    /// and write through <see cref="Collider.material"/>, which does not clone.
    /// </remarks>
    [TestFixture]
    public sealed class ColliderMaterialTests : SceneFixture
    {
        /// <summary>
        /// Pins Unity's behaviour, not ours: the premise the fix rests on.
        /// </summary>
        [Test]
        public void UnityColliderMaterial_ReadReplacesTheAssetWithAClone()
        {
            var (collider, ice) = NewCollider();

            var read = collider.material;

            Assert.AreNotSame(ice, read, "Unity stopped cloning the material on read");
            Assert.AreNotSame(ice, collider.sharedMaterial, "Unity stopped replacing sharedMaterial with a clone");
            Assert.IsTrue(read.name.Contains("Instance"), $"Expected a clone, got '{read.name}'");
        }

        /// <summary>
        /// Pins the other half: assigning does not clone, which is why the setter is left alone.
        /// </summary>
        [Test]
        public void UnityColliderMaterial_AssignmentDoesNotClone()
        {
            var (collider, ice) = NewCollider();

            collider.material = ice;

            Assert.AreSame(ice, collider.sharedMaterial, "Unity started cloning the material on assignment");
        }

        [Test]
        public void MaterialBinder_OneWayToSource_ReportsTheAssetItself()
        {
            var (collider, ice) = NewCollider();
            var binder = collider.gameObject.AddComponent<ColliderMaterialMonoBinder>();

            SetMode(binder, BindMode.OneWayToSource);

            PhysicsMaterial received = null;
            var member = new OneWayToSourceBindableMember<PhysicsMaterial>(value => received = value);
            ((IBinder)binder).Bind(member);

            Assert.AreSame(ice, received, "The ViewModel received a clone instead of the asset");
            Assert.AreSame(ice, collider.sharedMaterial, "Binding replaced the collider's material with a clone");
        }

        private (Collider collider, PhysicsMaterial material) NewCollider()
        {
            var material = Track(new PhysicsMaterial("Ice"));
            var collider = Spawn<BoxCollider>("Collider");
            collider.sharedMaterial = material;

            return (collider, material);
        }

        private static void SetMode(MonoBinder binder, BindMode mode)
        {
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            Assert.AreEqual(mode, binder.Mode, "Could not set the binder's mode");
        }
    }
}
