using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that a binder added through the inspector starts in a mode its own <c>[BindModeOverride]</c> allows.
    /// </summary>
    /// <remarks>
    /// The serialized <c>_mode</c> field starts at <see cref="BindMode.TwoWay"/>, which the field's own
    /// <c>[BindMode(OneWay, OneTime)]</c> forbids — a binder added and left alone was rejected by the bindable member
    /// it was pointed at. There is no single constant that fixes this: the 30 <c>*ToSourceMonoBinder</c> types allow
    /// only <see cref="BindMode.OneWayToSource"/>, and two <c>*ByBindMonoBinder</c> types allow only
    /// <see cref="BindMode.OneTime"/>, so flipping the constant to <see cref="BindMode.OneWay"/> would simply move
    /// the breakage. The default is therefore per-type, applied through Unity's <c>Reset</c> callback.
    /// <para/>
    /// <c>AddComponent</c> does not call <c>Reset</c>; <see cref="ObjectFactory"/> does, which is the path the
    /// inspector's "Add Component" takes. Binders created with <c>AddComponent</c> at runtime still get the raw field
    /// initializer — closing that is a separate change.
    /// </remarks>
    [TestFixture]
    public sealed class BinderDefaultModeTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) UnityEngine.Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void AddedThroughTheInspector_AnOrdinaryBinderStartsOneWay()
        {
            var gameObject = NewGameObject();
            gameObject.AddComponent<TMPro.TextMeshProUGUI>();

            var binder = ObjectFactory.AddComponent<TextMonoBinder>(gameObject);

            Assert.AreEqual(BindMode.OneWay, binder.Mode);
        }

        [Test]
        public void AddedThroughTheInspector_AToSourceBinderStartsOneWayToSource()
        {
            var gameObject = NewGameObject();
            gameObject.AddComponent<TMPro.TextMeshProUGUI>();

            var binder = ObjectFactory.AddComponent<TextToSourceMonoBinder>(gameObject);

            Assert.AreEqual(BindMode.OneWayToSource, binder.Mode);
        }

        [Test]
        public void AddedThroughTheInspector_AByBindBinderStartsOneTime()
        {
            var binder = ObjectFactory.AddComponent<GameObjectVisibleByBindMonoBinder>(NewGameObject());

            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        /// <summary>
        /// The guard that generalises the three cases above: whatever a binder declares as its default must be a mode
        /// its own <c>[BindModeOverride]</c> permits.
        /// </summary>
        [Test]
        public void EveryBinder_DefaultModeIsAllowedByItsOwnOverride()
        {
            var defaultMode = typeof(MonoBinder)
                .GetProperty("DefaultMode", BindingFlags.Instance | BindingFlags.NonPublic)!;

            var offenders = new List<string>();

            foreach (var type in BinderTypes())
            {
                var allowed = AllowedModes(type);
                if (allowed is null) continue;

                var binder = (MonoBinder)NewGameObject().AddComponent(type);
                var mode = (BindMode)defaultMode.GetValue(binder)!;

                if (!allowed.Contains(mode))
                    offenders.Add($"{type.Name}: DefaultMode = {mode}, разрешено {string.Join(", ", allowed)}");
            }

            Assert.IsEmpty(
                offenders,
                "Биндеры, чей DefaultMode запрещён их собственным [BindModeOverride]:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, offenders.OrderBy(entry => entry)));
        }

        private static IEnumerable<Type> BinderTypes() =>
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.GetName().Name.StartsWith("Aspid.MVVM", StringComparison.Ordinal))
                .Where(assembly => !assembly.GetName().Name.Contains("Tests", StringComparison.Ordinal))
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type is { IsAbstract: false, IsGenericTypeDefinition: false })
                .Where(type => typeof(MonoBinder).IsAssignableFrom(type));

        /// <summary>
        /// The modes a type's nearest <c>[BindModeOverride]</c> permits, or <see langword="null"/> when the type
        /// carries none and therefore inherits the field-level default set.
        /// </summary>
        private static IReadOnlyCollection<BindMode> AllowedModes(Type type)
        {
            for (var current = type; current is not null; current = current.BaseType)
            {
                var attribute = current.GetCustomAttribute<BindModeOverrideAttribute>(inherit: false);
                if (attribute is null) continue;

                var all = Enum.GetValues(typeof(BindMode)).Cast<BindMode>().Where(mode => mode != BindMode.None);
                if (attribute.IsAll || (attribute.IsOne && attribute.IsTwo)) return all.ToArray();

                var modes = new HashSet<BindMode>(attribute.Modes);
                if (attribute.IsOne) modes.UnionWith(new[] { BindMode.OneWay, BindMode.OneTime });
                if (attribute.IsTwo) modes.UnionWith(new[] { BindMode.TwoWay, BindMode.OneWayToSource });

                // Пустой набор без флагов документирован как эквивалент IsAll.
                return modes.Count is 0 ? all.ToArray() : modes.ToArray();
            }

            return null;
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("DefaultMode");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
