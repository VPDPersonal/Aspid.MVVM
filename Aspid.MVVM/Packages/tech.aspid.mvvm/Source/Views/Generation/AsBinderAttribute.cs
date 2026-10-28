using System;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
namespace Aspid.MVVM
{
    /// <summary>
    /// Wraps the field or property of a <see cref="ViewAttribute">[View]</see> class in a binder of <see cref="Type"/>, created with the member as its first
    /// constructor argument.
    /// </summary>
    /// <remarks>
    /// For an array, creates one binder per element.
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
        /// <param name="type">The binder type: a non-abstract <see cref="IBinder"/>; any other type skips the member.</param>
        /// <param name="arguments">
        /// The binder constructor arguments that follow the member; a <see langword="nameof"/> expression passes the named member
        /// of the View.
        /// </param>
        public AsBinderAttribute(Type type, params object[] arguments)
        {
#if UNITY_EDITOR || DEBUG
            Type = type;
#endif
        }
    }
}
