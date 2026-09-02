using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Checks that every name in <see cref="AddBinderContextMenuAttribute.SerializePropertyNames"/> is a serialized
    /// property the target component actually has.
    /// </summary>
    [TestFixture]
    public sealed class BinderSerializedPropertyContractTests : SceneFixture
    {
        [Test]
        public void EverySerializedPropertyNameResolvesOnItsTargetComponent()
        {
            var complaints = new List<string>();
            var skipped = new List<string>();

            // Adding an arbitrary component runs its Awake, and Unity logs whatever that throws instead of
            // letting it reach us — LocalizeStringEvent complains about missing LocalizationSettings unless
            // another fixture happened to create them first. Without this the sweep passes or fails by test
            // order, which is worse than not having it.
            LogAssert.ignoreFailingMessages = true;

            var binders = BindersWithContextMenu().ToArray();

            Assert.Greater(binders.Length, 300, "The sweep found no binders — the check would pass vacuously");

            try
            {
                foreach (var binder in binders)
                {
                    var attribute = binder.GetCustomAttributes(typeof(AddBinderContextMenuAttribute), false)
                        .Cast<AddBinderContextMenuAttribute>()
                        .First();

                    Inspect(binder, attribute, complaints, skipped);
                }
            }
            finally
            {
                LogAssert.ignoreFailingMessages = false;
            }

            if (complaints.Count > 0)
                Assert.Fail(Report(complaints, skipped));
        }

        private void Inspect(
            Type binder,
            AddBinderContextMenuAttribute attribute,
            List<string> complaints,
            List<string> skipped)
        {
            var names = attribute.SerializePropertyNames;
            if (names is not { Length: > 0 }) return;

            var target = attribute.Type;
            if (target is null || target.IsAbstract || !typeof(Component).IsAssignableFrom(target))
            {
                skipped.Add($"{binder.Name} → {target?.Name ?? "null"}");
                return;
            }

            var host = Spawn("Contract");
            var component = Attach(host, target);

            if (!component)
            {
                skipped.Add($"{binder.Name} → {target.Name} (could not be added)");
                return;
            }

            var known = NamesOf(component);
            var missing = names.Where(name => !known.Contains(name)).ToArray();

            if (missing.Length > 0)
                complaints.Add($"{binder.Name} → {target.Name}: {string.Join(", ", missing)}");
        }

        /// <summary>
        /// Collects the <see cref="SerializedProperty.name"/> of every serialized property, at any depth.
        /// </summary>
        /// <remarks>
        /// The context menu matches on the leaf name of whichever property was right-clicked, so a nested one
        /// such as <c>m_OnClick.m_PersistentCalls.m_Calls</c> is offered under the name <c>m_Calls</c>. Looking
        /// only at the root would call those names broken when they are not.
        /// </remarks>
        private static HashSet<string> NamesOf(Component component)
        {
            var names = new HashSet<string>();
            var property = new SerializedObject(component).GetIterator();

            while (property.Next(enterChildren: true))
                names.Add(property.name);

            return names;
        }

        private static Component Attach(GameObject host, Type target) =>
            typeof(Transform).IsAssignableFrom(target) && !typeof(RectTransform).IsAssignableFrom(target)
                ? host.transform
                : host.AddComponent(target);

        private static IEnumerable<Type> BindersWithContextMenu() =>
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.GetName().Name.StartsWith("Aspid.MVVM"))
                .SelectMany(SafeTypes)
                .Where(type => type.GetCustomAttributes(typeof(AddBinderContextMenuAttribute), false).Length > 0)
                .OrderBy(type => type.Name);

        private static IEnumerable<Type> SafeTypes(System.Reflection.Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (System.Reflection.ReflectionTypeLoadException exception)
            {
                return exception.Types.Where(type => type is not null);
            }
        }

        private static string Report(List<string> complaints, List<string> skipped)
        {
            var report = new StringBuilder();
            report.AppendLine($"Serialized properties that did not resolve: {complaints.Count}");

            foreach (var complaint in complaints)
                report.AppendLine("  " + complaint);

            if (skipped.Count > 0)
                report.AppendLine($"(skipped targets that are not a concrete component: {skipped.Count})");

            return report.ToString();
        }
    }
}
