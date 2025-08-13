#if UNITY_EDITOR
#nullable enable
using System.ComponentModel;

namespace Aspid.MVVM.Unity
{
    /// <summary>
    /// Interface required for validating a Binder within the Editor.
    /// It must be implemented inside #if UNITY_EDITOR.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public interface IMonoBinderValidable : IBinder
    {
        /// <summary>
        /// Is there a component?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsMonoExist { get; }
        
        /// <summary>
        /// The View to which the Binder relates.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IMonoBinderSource? Source { get; set; }
        
        /// <summary>
        /// The ID that must correspond to the name of any ViewModel property.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string? Id { get; set; }

        /// <summary>
        /// Resets the parameters.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void Reset()
        {
            if (!IsMonoExist) return;
            
            Id = null;
            Source = null;
        }
    }
}
#endif