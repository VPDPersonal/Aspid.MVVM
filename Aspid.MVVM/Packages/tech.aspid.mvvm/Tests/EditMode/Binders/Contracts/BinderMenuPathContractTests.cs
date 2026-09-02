using System;
using System.Linq;
using System.Text;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Checks that every binder's <see cref="AddComponentMenu"/> path starts under <c>Aspid/MVVM/Binders</c> and
    /// separates the component name from its property with an en dash rather than a hyphen.
    /// </summary>
    [TestFixture]
    public sealed class BinderMenuPathContractTests
    {
        private const string Root = "Aspid/MVVM/Binders/";

        [Test]
        public void EveryBinderMenuPathFollowsTheConvention()
        {
            var complaints = new List<string>();
            var paths = MenuPaths().ToArray();

            Assert.Greater(paths.Length, 300, "The sweep found no binders — the check would pass vacuously");

            foreach (var (binder, path) in paths)
            {
                if (!path.StartsWith(Root, StringComparison.Ordinal))
                    complaints.Add($"{binder.Name}: does not start with \"{Root}\" — \"{path}\"");
                else if (path.Contains(" - ", StringComparison.Ordinal))
                    complaints.Add($"{binder.Name}: hyphen instead of an en dash — \"{path}\"");
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
            report.AppendLine($"Menu paths off convention: {complaints.Count}");

            foreach (var complaint in complaints)
                report.AppendLine("  " + complaint);

            return report.ToString();
        }
    }
}
