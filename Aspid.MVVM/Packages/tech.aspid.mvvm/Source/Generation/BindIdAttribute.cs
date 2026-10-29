using System;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
namespace Aspid.MVVM
{
    /// <summary>
    /// Sets the identifier that pairs a ViewModel member with the View binders of the same identifier.
    /// </summary>
    /// <remarks>
    /// Without it, the identifier is the member name without its prefix, capitalized, with a <c>Command</c> suffix for a
    /// <see cref="RelayCommandAttribute">[RelayCommand]</see> method; the attribute value replaces all of it.
    /// </remarks>
    /// <example>
    /// <code>
    /// [Bind]
    /// [BindId("Health")]
    /// private int _hp;
    /// </code>
    /// <code>
    /// [BindId("Health")]
    /// [SerializeField] private MonoBinder[] _hpBinders;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class BindIdAttribute : Attribute
    {
#if UNITY_EDITOR || DEBUG
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <remarks>
        /// Exists only when <c>UNITY_EDITOR</c> or <c>DEBUG</c> is defined.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public readonly string Id;
#endif

        /// <summary>
        /// Creates an attribute with the identifier <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The identifier that replaces the generated one.</param>
        public BindIdAttribute(string id)
        {
#if UNITY_EDITOR || DEBUG
            Id = id;
#endif
        }
    }
}
