using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Helpers for resolving an open generic type definition into a concrete closed type inside the
    /// <see cref="TypeSelectorWindow"/> argument-selection flow: candidate definitions, argument
    /// inference, constraint filters and closed-type construction/validation.
    /// </summary>
    /// <remarks>
    /// Carries no dependency on any particular feature (e.g. <c>[SerializeReference]</c>); the
    /// Unity-serializability of an argument is supplied by the caller as a separate filter.
    /// </remarks>
    internal static class GenericTypeResolver
    {
        // Cached once per domain: the open-generic flow sweeps every domain type per parameter page (twice, with the
        // candidate scan), which stalls large projects. Static state is cleared on every domain reload, so the cache is
        // implicitly invalidated whenever assemblies could change. Built lazily so touching the constraint-only helpers
        // never pays for (or fails on) the full domain sweep.
        private static List<Type> _domainTypes;

        private static List<Type> DomainTypes => _domainTypes ??= TypeUtility.EnumerateDomainTypes().ToList();

        /// <summary>
        /// Enumerates the open generic type definitions whose closed form could be assigned to
        /// <paramref name="fieldType"/> and, when <paramref name="narrowTypes"/> are supplied,
        /// to every one of them.
        /// </summary>
        /// <remarks>
        /// The narrowing check matters because these definitions are injected verbatim via the selector's
        /// <c>additionalTypes</c> path, which otherwise bypasses the narrowing filter applied to the
        /// ordinary candidate scan.
        /// </remarks>
        internal static IEnumerable<Type> GetAssignableGenericDefinitions(Type fieldType, params Type[] narrowTypes)
        {
            if (fieldType is null) yield break;

            foreach (var type in DomainTypes)
            {
                if (!IsAssignableGenericDefinition(type)) continue;
                if (!CanCloseToFieldType(type, fieldType)) continue;
                if (CanCloseToAllNarrowing(type, narrowTypes)) yield return type;
            }
        }

        /// <summary>
        /// Attempts to close <paramref name="openDefinition"/> using the type arguments of a closed-generic
        /// <paramref name="fieldType"/> (e.g. a <c>Modifier&lt;float&gt;</c> field directly determines the
        /// argument of a <c>Modifier&lt;&gt;</c> candidate). Returns <see langword="false"/> when the field is
        /// not a closed generic or the inferred type would not be assignable.
        /// </summary>
        internal static bool TryInferFromFieldType(Type fieldType, Type openDefinition, out Type closed)
        {
            closed = null;

            if (fieldType is null || !fieldType.IsGenericType || fieldType.ContainsGenericParameters) return false;

            var fieldArguments = fieldType.GetGenericArguments();
            if (fieldArguments.Length != openDefinition.GetGenericArguments().Length) return false;

            var fieldDefinition = fieldType.GetGenericTypeDefinition();
            var matchesDefinition = GenericBaseDefinitions(openDefinition)
                .Any(definition => definition == fieldDefinition);

            if (!matchesDefinition) return false;

            try
            {
                closed = openDefinition.MakeGenericType(fieldArguments);
            }
            catch (Exception)
            {
                closed = null;
                return false;
            }

            return fieldType.IsAssignableFrom(closed);
        }

        /// <summary>
        /// Returns the explicit base-type/interface constraints of <paramref name="parameter"/> (excluding
        /// other type parameters), or <c>{ typeof(object) }</c> when it has none. Used as the base-type filter
        /// for the argument's candidate list.
        /// </summary>
        internal static Type[] GetConstraintBaseTypes(Type parameter)
        {
            var constraints = parameter.GetGenericParameterConstraints()
                .Where(constraint => !constraint.IsGenericParameter && !constraint.ContainsGenericParameters)
                .ToArray();

            return constraints.Length > 0 ? constraints : new[] { typeof(object) };
        }

        /// <summary>
        /// Returns <see langword="true"/> when <paramref name="candidate"/> satisfies the special
        /// (<c>struct</c>/<c>class</c>/<c>new()</c>) constraints declared on <paramref name="parameter"/>.
        /// </summary>
        internal static bool SatisfiesSpecialConstraints(Type parameter, Type candidate)
        {
            if (candidate is null) return false;

            var special = parameter.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
            var requireValueType = (special & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0;
            var requireReferenceType = (special & GenericParameterAttributes.ReferenceTypeConstraint) != 0;
            var requireDefaultCtor = (special & GenericParameterAttributes.DefaultConstructorConstraint) != 0;

            if (requireValueType && !candidate.IsValueType) return false;
            if (requireReferenceType && candidate.IsValueType) return false;

            return !requireDefaultCtor || candidate.IsValueType || candidate.GetConstructor(Type.EmptyTypes) is not null;
        }

        /// <summary>
        /// Closes <paramref name="openDefinition"/> over <paramref name="arguments"/> and validates the result
        /// against every entry of <paramref name="fieldTypes"/>. Returns <see langword="false"/> with a
        /// human-readable <paramref name="error"/> when construction throws (a violated parameter constraint)
        /// or the closed type is not assignable to the field.
        /// </summary>
        internal static bool TryConstruct(Type openDefinition, Type[] arguments, Type[] fieldTypes, out Type closed, out string error)
        {
            closed = null;
            error = null;

            try
            {
                closed = openDefinition.MakeGenericType(arguments);
            }
            catch (Exception exception)
            {
                error = $"Cannot construct {FormatDefinitionName(openDefinition)}: {exception.Message}";
                return false;
            }

            // The chosen arguments may satisfy the type parameters' own constraints yet still produce a type
            // that is not assignable to the managed-reference field — guard against a value Unity would drop.
            if (fieldTypes is not null)
            {
                foreach (var fieldType in fieldTypes)
                {
                    if (fieldType is null || fieldType == typeof(object)) continue;
                    if (fieldType.IsAssignableFrom(closed)) continue;

                    error = $"{closed.Name} is not assignable to {fieldType.Name}.";
                    closed = null;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns <see langword="true"/> when <paramref name="closed"/> is assignable to every meaningful entry of
        /// <paramref name="fieldTypes"/> (nulls and the unconstrained <see cref="object"/> sentinel impose no
        /// restriction). Mirrors the assignability guard <see cref="TryConstruct"/> applies, for callers that already
        /// hold a constructed closed type and only need to validate it.
        /// </summary>
        internal static bool IsAssignableToFieldTypes(Type closed, Type[] fieldTypes)
        {
            if (closed is null) return false;
            if (fieldTypes is null) return true;

            foreach (var fieldType in fieldTypes)
            {
                if (fieldType is null || fieldType == typeof(object)) continue;
                if (!fieldType.IsAssignableFrom(closed)) return false;
            }

            return true;
        }

        /// <summary>
        /// Predicate identifying open generic type definitions that can be offered for a field once closed
        /// over concrete arguments: non-abstract generic classes that are neither
        /// <see cref="UnityEngine.Object"/> nor delegates, and that are not compiler-generated.
        /// </summary>
        /// <remarks>
        /// Compiler-emitted types (anonymous types, closure/iterator display classes such as
        /// <c>&lt;&gt;c__11&lt;T&gt;</c> or <c>&lt;&gt;f__AnonymousType0&lt;…&gt;</c>) must be excluded
        /// here because these definitions are added verbatim via the selector's <c>additionalTypes</c>
        /// path, which bypasses the name/<see cref="CompilerGeneratedAttribute"/> checks applied to
        /// ordinary candidates.
        /// </remarks>
        private static bool IsAssignableGenericDefinition(Type type) =>
            type is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: true } &&
            !typeof(UnityEngine.Object).IsAssignableFrom(type) &&
            !typeof(Delegate).IsAssignableFrom(type) &&
            !IsCompilerGenerated(type);

        private static bool IsCompilerGenerated(Type type) =>
            type.IsDefined(typeof(CompilerGeneratedAttribute), false)
            || type.Name.Contains('<')
            || type.Name.Contains('>');

        // Nulls and the unconstrained `object` sentinel impose no restriction, mirroring the
        // concrete-type narrowing filter.
        private static bool CanCloseToAllNarrowing(Type openDefinition, Type[] narrowTypes)
        {
            if (narrowTypes is null) return true;

            foreach (var narrowType in narrowTypes)
            {
                if (narrowType is null || narrowType == typeof(object)) continue;
                if (!CanCloseToFieldType(openDefinition, narrowType)) return false;
            }

            return true;
        }

        /// <summary>
        /// Short display form of an open definition with its parameter names (<c>Modifier&lt;T&gt;</c>).
        /// </summary>
        private static string FormatDefinitionName(Type definition)
        {
            var baseName = TypeUtility.StripArity(definition.Name);
            var arguments = string.Join(", ", definition.GetGenericArguments().Select(argument => argument.Name));
            return $"{baseName}<{arguments}>";
        }

        private static bool CanCloseToFieldType(Type openDefinition, Type fieldType)
        {
            if (fieldType.IsGenericType)
            {
                var fieldDefinition = fieldType.GetGenericTypeDefinition();
                return GenericBaseDefinitions(openDefinition).Any(definition => definition == fieldDefinition);
            }

            if (fieldType.IsAssignableFrom(openDefinition)) return true;
            if (openDefinition.GetInterfaces().Contains(fieldType)) return true;

            for (var current = openDefinition.BaseType; current is not null; current = current.BaseType)
                if (current == fieldType) return true;

            return false;
        }

        private static IEnumerable<Type> GenericBaseDefinitions(Type openDefinition)
        {
            if (openDefinition.IsGenericType)
                yield return openDefinition.GetGenericTypeDefinition();

            for (var current = openDefinition.BaseType; current is not null; current = current.BaseType)
            {
                if (current.IsGenericType)
                    yield return current.GetGenericTypeDefinition();
            }

            foreach (var contract in openDefinition.GetInterfaces())
            {
                if (contract.IsGenericType)
                    yield return contract.GetGenericTypeDefinition();
            }
        }
    }
}
