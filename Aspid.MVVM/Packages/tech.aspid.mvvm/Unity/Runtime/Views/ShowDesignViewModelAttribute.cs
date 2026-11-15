using System;
using System.Linq;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Configures the design ViewModel selector of a <see cref="MonoView"/> in the Editor.
    /// </summary>
    /// <example>
    /// <code>
    /// [ShowDesignViewModel(typeof(PlayerViewModel), strictType: true)]
    /// public partial class PlayerView : MonoView { }
    /// </code>
    /// </example>
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public sealed class ShowDesignViewModelAttribute : Attribute
    {
        /// <summary>
        /// The ViewModel types available for selection in the design ViewModel dropdown.
        /// Includes <see cref="IViewModel"/> when none of the supplied types implements that interface.
        /// </summary>
        public readonly Type[] Types;

        /// <summary>
        /// Whether the selector is locked to the supplied concrete ViewModel type.
        /// </summary>
        public readonly bool StrictType;

        /// <summary>
        /// Creates the attribute allowing any <see cref="IViewModel"/> implementation
        /// to be used as a design ViewModel.
        /// </summary>
        public ShowDesignViewModelAttribute()
        {
            Types = new[] { typeof(IViewModel) };
        }

        /// <summary>
        /// Creates the attribute with multiple allowed ViewModel types.
        /// <see cref="IViewModel"/> is appended automatically if none of the provided types implement it.
        /// </summary>
        /// <param name="types">The ViewModel types to include in the design ViewModel selector.</param>
        /// <exception cref="ArgumentNullException"><paramref name="types"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">A filter type is <see langword="null"/>.</exception>
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
        /// Creates the attribute with a single ViewModel type.
        /// </summary>
        /// <param name="type">The ViewModel type to show in the design ViewModel selector.</param>
        /// <param name="strictType">When <see langword="true"/>, selects the concrete type and locks the selector.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// Strict mode is requested for a type that is abstract, open generic or does not implement <see cref="IViewModel"/>.
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
