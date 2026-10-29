#if UNITY_EDITOR || DEBUG
using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines a binder that can attach again to its current bindable member.
    /// </summary>
    /// <remarks>
    /// Exists only when <c>UNITY_EDITOR</c> or <c>DEBUG</c> is defined.
    /// </remarks>
    public interface IRebindableBinder
    {
        /// <summary>
        /// Detaches the binder and attaches it again to the same bindable member, so that a changed
        /// <see cref="IBinder.Mode"/> takes effect.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Rebind();
    }
}
#endif
