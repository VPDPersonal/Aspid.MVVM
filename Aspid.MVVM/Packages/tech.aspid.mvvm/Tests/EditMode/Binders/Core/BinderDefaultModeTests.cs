using System;
using System.Linq;
using UnityEditor;
using UnityEngine.UI;
using NUnit.Framework;
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
    /// <c>AddComponent</c> does not call Unity's <c>Reset</c> callback; <see cref="ObjectFactory"/> does, which is
    /// the path the inspector's "Add Component" takes — these tests go through it for that reason.
    /// </remarks>
    [TestFixture]
    public sealed class BinderDefaultModeTests : SceneFixture
    {
        [Test]
        public void AddedThroughTheInspector_AnOrdinaryBinderStartsOneWay()
        {
            var gameObject = Spawn();
            gameObject.AddComponent<TMPro.TextMeshProUGUI>();

            var binder = ObjectFactory.AddComponent<TextMonoBinder>(gameObject);

            Assert.AreEqual(BindMode.OneWay, binder.Mode);
        }

        [Test]
        public void AddedThroughTheInspector_AToSourceBinderStartsOneWayToSource()
        {
            var gameObject = Spawn();
            gameObject.AddComponent<TMPro.TextMeshProUGUI>();

            var binder = ObjectFactory.AddComponent<TextToSourceMonoBinder>(gameObject);

            Assert.AreEqual(BindMode.OneWayToSource, binder.Mode);
        }

        [Test]
        public void AddedThroughTheInspector_AByBindBinderStartsOneTime()
        {
            var binder = ObjectFactory.AddComponent<GameObjectVisibleByBindMonoBinder>(Spawn());

            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        [Test]
        public void SelectableToSourceMonoBinder_DefaultsToTheOnlyModeItSupports()
        {
            var gameObject = Spawn();
            gameObject.AddComponent<Button>();

            Assert.AreEqual(BindMode.OneWayToSource, gameObject.AddComponent<SelectableToSourceMonoBinder>().Mode);
        }

        [Test]
        public void GameObjectToSourceMonoBinder_DefaultsToTheOnlyModeItSupports()
        {
            Assert.AreEqual(BindMode.OneWayToSource, Spawn().AddComponent<GameObjectToSourceMonoBinder>().Mode);
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

                var binder = (MonoBinder)Spawn().AddComponent(type);
                var mode = (BindMode)defaultMode.GetValue(binder)!;

                if (!allowed.Contains(mode))
                    offenders.Add($"{type.Name}: DefaultMode = {mode}, allowed {string.Join(", ", allowed)}");
            }

            Assert.IsEmpty(
                offenders,
                "Binders whose DefaultMode is forbidden by their own [BindModeOverride]:"
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

                // An empty set with no flags is documented as equivalent to IsAll.
                return modes.Count is 0 ? all.ToArray() : modes.ToArray();
            }

            return null;
        }
    }
}
