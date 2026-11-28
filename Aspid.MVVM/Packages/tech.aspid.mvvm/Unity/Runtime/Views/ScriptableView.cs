using UnityEngine;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="ScriptableObject"/> for views with generated binding lifecycle methods.
    /// </summary>
    [View]
    [SuppressMessage(category: "ReSharper", checkId: "MemberCanBeProtected.Global")]
    public abstract partial class ScriptableView : ScriptableObject
    {
        /// <summary>
        /// Called when Unity destroys the component to dispose the view.
        /// </summary>
        /// <remarks>Overrides must call the base implementation.</remarks>
        protected virtual void OnDestroy() =>
            Dispose();
        
        /// <summary>
        /// Deinitializes the view and releases its bindings without destroying any Unity objects.
        /// </summary>
        /// <remarks>Overrides must call the base implementation.</remarks>
        public virtual void Dispose() =>
            Deinitialize();
    }
}
