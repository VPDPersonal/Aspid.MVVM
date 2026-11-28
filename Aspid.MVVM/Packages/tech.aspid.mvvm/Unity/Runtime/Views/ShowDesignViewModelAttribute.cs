using System;
using System.Linq;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Shows and configures the design ViewModel selector on a <see cref="MonoView"/> Inspector.
    /// </summary>
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public sealed class ShowDesignViewModelAttribute : Attribute
    {
        /// <summary>
        /// The selectable ViewModel types, including <see cref="IViewModel"/> when none implements it.
        /// </summary>
        public readonly Type[] Types;

        /// <summary>
        /// Whether the selector is locked to the supplied concrete ViewModel type.
        /// </summary>
        public readonly bool StrictType;

        /// <summary>
        /// Creates an attribute that allows any <see cref="IViewModel"/> implementation.
        /// </summary>
        public ShowDesignViewModelAttribute()
        {
            Types = new[] { typeof(IViewModel) };
        }

        /// <summary>
        /// Creates an attribute with filters for selectable types.
        /// </summary>
        /// <param name="types">The type filters. Adds <see cref="IViewModel"/> if no filter is assignable to it.</param>
        /// <exception cref="ArgumentNullException"><paramref name="types"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">An element of <paramref name="types"/> is <see langword="null"/>.</exception>
        public ShowDesignViewModelAttribute(params Type[] types)
        {
            if (types is null)
                throw new ArgumentNullException(nameof(types));
            if (types.Any(type => type is null))
                throw new ArgumentException("ViewModel filters cannot contain null types.", nameof(types));

            var hasViewModel = types.Any(type => typeof(IViewModel).IsAssignableFrom(type));

            Types = hasViewModel
                ? types
                : types.Append(typeof(IViewModel)).ToArray();
        }

        /// <summary>
        /// Creates an attribute with one type filter and an optional lock.
        /// </summary>
        /// <param name="type">The type filter. Adds <see cref="IViewModel"/> if the filter is not assignable to it.</param>
        /// <param name="strictType">When <see langword="true"/>, selects the concrete type and locks the selector.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="strictType"/> is <see langword="true"/> and <paramref name="type"/> is not a concrete, closed <see cref="IViewModel"/> type.
        /// </exception>
        public ShowDesignViewModelAttribute(Type type, bool strictType = false)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            StrictType = strictType;
            var implementsViewModel = typeof(IViewModel).IsAssignableFrom(type);

            if (strictType
                && (!implementsViewModel || type.IsAbstract || type.IsInterface || type.ContainsGenericParameters))
            {
                throw new ArgumentException("Strict selection requires a concrete, closed IViewModel type.", nameof(type));
            }

            Types = implementsViewModel
                ? new[] { type }
                : new[] { type, typeof(IViewModel) };
        }
    }
}
