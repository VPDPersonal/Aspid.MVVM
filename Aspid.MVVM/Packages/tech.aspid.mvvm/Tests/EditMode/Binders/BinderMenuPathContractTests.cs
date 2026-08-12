using System;
using System.Linq;
using System.Text;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Checks that every binder's <see cref="AddComponentMenu"/> path follows the package's own convention.
    /// </summary>
    /// <remarks>
    /// The Add Component menu is how most people find a binder, and a path that drifts puts one in a different
    /// branch of the tree from its siblings. Two rules are worth pinning: the path starts under
    /// <c>Aspid/MVVM/Binders</c>, and the component name is separated from its property by an en dash, which is
    /// what the overwhelming majority already used — thirty binders had drifted to a hyphen.
    /// </remarks>
    [TestFixture]
    public sealed class BinderMenuPathContractTests
    {
        private const string Root = "Aspid/MVVM/Binders/";

        [Test]
        public void EveryBinderMenuPathFollowsTheConvention()
        {
            var complaints = new List<string>();
            var paths = MenuPaths().ToArray();

            // Without this the test would pass by finding nothing — a wrong base type or assembly filter would
            // read as a clean sweep. The package shipped well over three hundred menu entries when this was
            // written; the floor only has to be high enough that an empty sweep cannot hide.
            Assert.Greater(paths.Length, 300, "Обход не нашёл биндеров — проверка прошла бы впустую");

            foreach (var (binder, path) in paths)
            {
                if (!path.StartsWith(Root, StringComparison.Ordinal))
                    complaints.Add($"{binder.Name}: не начинается с «{Root}» — «{path}»");
                else if (path.Contains(" - ", StringComparison.Ordinal))
                    complaints.Add($"{binder.Name}: дефис вместо тире — «{path}»");
            }

            if (complaints.Count > 0)
                Assert.Fail(Report(complaints));
        }

        private static IEnumerable<(Type Binder, string Path)> MenuPaths() =>
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.GetName().Name.StartsWith("Aspid.MVVM", StringComparison.Ordinal))
                .SelectMany(SafeTypes)
                .Where(type => typeof(MonoBinder).IsAssignableFrom(type))
                .Select(type => (type, type.GetCustomAttribute<AddComponentMenu>()?.componentMenu))
                .Where(pair => !string.IsNullOrEmpty(pair.componentMenu))
                .Select(pair => (pair.type, pair.componentMenu))
                .OrderBy(pair => pair.type.Name);

        private static IEnumerable<Type> SafeTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException exception)
            {
                return exception.Types.Where(type => type is not null);
            }
        }

        private static string Report(List<string> complaints)
        {
            var report = new StringBuilder();
            report.AppendLine($"Путей меню не по конвенции: {complaints.Count}");

            foreach (var complaint in complaints)
                report.AppendLine("  " + complaint);

            return report.ToString();
        }
    }
}
