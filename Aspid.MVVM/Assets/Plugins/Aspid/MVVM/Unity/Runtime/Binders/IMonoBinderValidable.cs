#if UNITY_EDITOR
#nullable enable
using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Extends <see cref="IBinder"/> with Editor-only properties and reset capability
    /// for validating and configuring <see cref="MonoBinder"/> components.
    /// </summary>
    /// <remarks>
    /// Implementations must be guarded with <c>#if UNITY_EDITOR</c>.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public interface IMonoBinderValidable : IBinder
    {
        /// <summary>
        /// <see langword="true"/> if the underlying MonoBehaviour component exists; otherwise, <see langword="false"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsMonoExist { get; }

        /// <summary>
        /// Gets or sets the <see cref="IView"/> associated with this binder.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IView? View { get; set; }

        /// <summary>
        /// Gets or sets the binding ID, which must match the name of a ViewModel property.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string? Id { get; set; }

        /// <summary>
        /// Sets <see cref="Id"/> and <see cref="View"/> to <see langword="null"/>, clearing the binder's configuration.
        /// Has no effect if <see cref="IsMonoExist"/> is <see langword="false"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void Reset()
        {
            if (!IsMonoExist) return;

            Id = null;
            View = null;
        }
    }
}
#endif