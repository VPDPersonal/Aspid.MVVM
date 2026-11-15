using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Wraps a field or readable property of a <see cref="ViewAttribute">[View]</see> class in a binder, passing the member as its first constructor argument.
    /// </summary>
    /// <remarks>
    /// For an array, creates one binder per element; invalid types or constructor arguments produce generator errors.
    /// </remarks>
    /// <example>
    /// <code>
    /// [AsBinder(typeof(ImageSpriteBinder))]
    /// [SerializeField] private Image _icon;
    /// </code>
    /// <code>
    /// [AsBinder(typeof(AnimatorSetFloatBinder), "Speed")]
    /// [SerializeField] private Animator _speed;
    /// </code>
    /// </example>
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class AsBinderAttribute : Attribute
    {
#if UNITY_EDITOR || DEBUG
        /// <summary>
        /// Gets the binder type.
        /// </summary>
        /// <remarks>
        /// Exists only when <c>UNITY_EDITOR</c> or <c>DEBUG</c> is defined.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public readonly Type Type;
#endif

        /// <summary>
        /// Creates an attribute that wraps the member in a binder of <paramref name="type"/>.
        /// </summary>
        /// <param name="type">An accessible, non-abstract, closed type implementing <see cref="IBinder"/>.</param>
        /// <param name="arguments">
        /// The binder constructor arguments that follow the member; a <see langword="nameof"/> expression passes the named member
        /// of the View.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="arguments"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="type"/> is abstract, open generic or does not implement <see cref="IBinder"/>.</exception>
        [SuppressMessage(category: "ReSharper", checkId: "ParameterOnlyUsedForPreconditionCheck.Local")]
        public AsBinderAttribute(Type type, params object[] arguments)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            
            if (arguments is null)
                throw new ArgumentNullException(nameof(arguments));
            
            if (type.IsAbstract || type.ContainsGenericParameters || !typeof(IBinder).IsAssignableFrom(type))
                throw new ArgumentException("The binder type must be non-abstract, closed and implement IBinder.", nameof(type));

#if UNITY_EDITOR || DEBUG
            Type = type;
#endif
        }
    }
}
