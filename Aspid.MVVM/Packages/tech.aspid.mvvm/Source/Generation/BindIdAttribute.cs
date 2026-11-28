using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sets the identifier used to match a View binder to a ViewModel member.
    /// </summary>
    /// <remarks>
    /// The generator uses the default identifier when the supplied name is blank. The default strips the member prefix,
    /// capitalizes the name and adds <c>Command</c> for <see cref="RelayCommandAttribute">[RelayCommand]</see> methods.
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
    [SuppressMessage(category: "ReSharper", checkId: "ClassNeverInstantiated.Global")]
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class BindIdAttribute : Attribute
    {
#if UNITY_EDITOR || DEBUG
        /// <summary>
        /// The binding identifier.
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
