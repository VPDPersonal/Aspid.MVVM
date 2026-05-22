using System;
using System.Linq;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Specifies which ViewModel types are available as design-time ViewModels for a View in the Unity Editor.
    /// Apply this attribute to a <see cref="MonoView"/> or <see cref="ScriptableView"/> class to restrict
    /// or extend the list of types shown in the design ViewModel selector.
    /// </summary>
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public sealed class ShowDesignViewModelAttribute : Attribute
    {
        /// <summary>
        /// The ViewModel types available for selection in the design ViewModel dropdown.
        /// Always includes <see cref="IViewModel"/> unless <see cref="StrictType"/> is <c>true</c>
        /// and the provided type already implements it.
        /// </summary>
        public readonly Type[] Types;

        /// <summary>
        /// Indicates whether the type filter is strict.
        /// When <c>true</c>, only the exact specified type is shown, and it must implement <see cref="IViewModel"/>.
        /// </summary>
        public readonly bool StrictType;

        /// <summary>
        /// Initializes the attribute allowing any <see cref="IViewModel"/> implementation
        /// to be used as a design ViewModel.
        /// </summary>
        public ShowDesignViewModelAttribute()
        {
            Types = new[] { typeof(IViewModel) };
        }

        /// <summary>
        /// Initializes the attribute with multiple allowed ViewModel types.
        /// <see cref="IViewModel"/> is appended automatically if none of the provided types implement it.
        /// </summary>
        /// <param name="types">The ViewModel types to include in the design ViewModel selector.</param>
        public ShowDesignViewModelAttribute(params Type[] types)
        {
            var hasViewModel = types.Any(type => typeof(IViewModel).IsAssignableFrom(type));

            Types = hasViewModel
                ? types
                : types.Append(typeof(IViewModel)).ToArray();
        }

        /// <summary>
        /// Initializes the attribute with a single ViewModel type.
        /// </summary>
        /// <param name="type">The ViewModel type to show in the design ViewModel selector.</param>
        /// <param name="strictType">
        /// When <c>true</c>, only the exact <paramref name="type"/> is shown.
        /// The type must implement <see cref="IViewModel"/>; otherwise an <see cref="ArgumentException"/> is thrown.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="strictType"/> is <c>true</c> and <paramref name="type"/>
        /// does not implement <see cref="IViewModel"/>.
        /// </exception>
        public ShowDesignViewModelAttribute(Type type, bool strictType = false)
        {
            StrictType = strictType;
            var implementsViewModel = typeof(IViewModel).IsAssignableFrom(type);

            if (strictType && !implementsViewModel)
                throw new ArgumentException($"Type {type.Name} does not implement IViewModel, but strictType is set to true.", nameof(type));

            Types = implementsViewModel
                ? new[] { type }
                : new[] { type, typeof(IViewModel) };
        }
    }
}
