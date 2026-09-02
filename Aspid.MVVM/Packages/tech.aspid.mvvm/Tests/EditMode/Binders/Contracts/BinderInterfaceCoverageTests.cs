using System;
using System.Linq;
using NUnit.Framework;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Guard test: every public <c>SetValue(T)</c> overload on a binder must be reachable through
    /// <see cref="IBinder{T}"/>.
    /// </summary>
    /// <remarks>
    /// This scans the shipped assemblies rather than a hand-written list, so a new binder that forgets an interface
    /// fails here instead of in a scene.
    /// </remarks>
    [TestFixture]
    public sealed class BinderInterfaceCoverageTests
    {
        [Test]
        public void EverySetValueOverload_IsReachableThroughIBinder()
        {
            var unreachable = new List<string>();

            foreach (var type in BinderTypes())
            {
                var reachable = type.GetInterfaces()
                    .Where(contract => contract.IsGenericType
                        && contract.GetGenericTypeDefinition() == typeof(IBinder<>))
                    .Select(contract => contract.GetGenericArguments()[0])
                    .ToHashSet();

                foreach (var parameter in DeclaredSetValueParameters(type))
                {
                    if (!reachable.Contains(parameter))
                        unreachable.Add($"{Describe(type)}.SetValue({Describe(parameter)})");
                }
            }

            Assert.IsEmpty(
                unreachable,
                "SetValue overloads unreachable through IBinder<T> — Bind throws BinderInvalidCastException for them:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, unreachable.OrderBy(entry => entry)));
        }

        private static IEnumerable<Type> BinderTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.GetName().Name.StartsWith("Aspid.MVVM", StringComparison.Ordinal))
                .Where(assembly => !assembly.GetName().Name.Contains("Tests", StringComparison.Ordinal));

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsInterface) continue;
                    if (typeof(IBinder).IsAssignableFrom(type)) yield return type;
                }
            }
        }

        /// <summary>
        /// Parameter types of the single-argument, non-generic <c>SetValue</c> overloads a type declares itself.
        /// Inherited overloads are covered when the declaring type is scanned.
        /// </summary>
        private static IEnumerable<Type> DeclaredSetValueParameters(Type type) =>
            type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(method => method.Name == "SetValue")
                .Where(method => !method.IsGenericMethod)
                .Select(method => method.GetParameters())
                .Where(parameters => parameters.Length == 1)
                .Select(parameters => parameters[0].ParameterType);

        private static string Describe(Type type)
        {
            if (!type.IsGenericType) return type.Name;

            var name = type.Name[..type.Name.IndexOf('`')];
            var arguments = string.Join(", ", type.GetGenericArguments().Select(Describe));

            return $"{name}<{arguments}>";
        }
    }
}
