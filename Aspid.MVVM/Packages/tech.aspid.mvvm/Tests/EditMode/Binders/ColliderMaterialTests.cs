using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests asserting that reading a collider's material does not replace it with a clone.
    /// </summary>
    /// <remarks>
    /// <see cref="Collider.material"/> is an instancing property: reading it makes Unity swap the assigned asset
    /// for a private copy named <c>"… (Instance)"</c> and keep that copy until the collider is destroyed. The
    /// binders read it in <see cref="BindMode.OneWayToSource"/>, so the ViewModel received a clone that no longer
    /// compared equal to the asset it had handed over, and every distinct collider bound this way left one behind.
    /// <para/>
    /// The setter is unaffected and stays on <see cref="Collider.material"/> — assigning does not clone, which is
    /// pinned below, and assigning <see cref="Collider.sharedMaterial"/> instead would edit the asset for every
    /// other collider using it.
    /// </remarks>
    [TestFixture]
    public sealed class ColliderMaterialTests
    {
        private readonly List<Object> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var spawned in _spawned)
            {
                if (spawned) Object.DestroyImmediate(spawned);
            }

            _spawned.Clear();
        }

        /// <summary>
        /// Pins Unity's behaviour, not ours: the premise the fix rests on.
        /// </summary>
        [Test]
        public void UnityColliderMaterial_ReadReplacesTheAssetWithAClone()
        {
            var (collider, ice) = NewCollider();

            var read = collider.material;

            Assert.AreNotSame(ice, read, "Unity перестала клонировать материал при чтении");
            Assert.AreNotSame(ice, collider.sharedMaterial, "Unity перестала подменять sharedMaterial клоном");
            Assert.IsTrue(read.name.Contains("Instance"), $"Ожидался клон, получено '{read.name}'");
        }

        /// <summary>
        /// Pins the other half: assigning does not clone, which is why the setter is left alone.
        /// </summary>
        [Test]
        public void UnityColliderMaterial_AssignmentDoesNotClone()
        {
            var (collider, ice) = NewCollider();

            collider.material = ice;

            Assert.AreSame(ice, collider.sharedMaterial, "Unity начала клонировать материал при записи");
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

            Assert.AreSame(ice, received, "Во ViewModel уехал клон вместо ассета");
            Assert.AreSame(ice, collider.sharedMaterial, "Привязка подменила материал коллайдера клоном");
        }

        private (Collider collider, PhysicsMaterial material) NewCollider()
        {
            var gameObject = new GameObject("Collider");
            _spawned.Add(gameObject);

            var material = new PhysicsMaterial("Ice");
            _spawned.Add(material);

            var collider = gameObject.AddComponent<BoxCollider>();
            collider.sharedMaterial = material;

            return (collider, material);
        }

        private static void SetMode(MonoBinder binder, BindMode mode)
        {
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            Assert.AreEqual(mode, binder.Mode, "Не удалось выставить режим биндера");
        }
    }
}
