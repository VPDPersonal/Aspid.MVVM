#if UNITY_EDITOR
using Aspid.UI.MVVM.Views;

namespace Aspid.UI.MVVM.Mono
{
    /// <summary>
    /// Interface required for validating a Binder within the Editor.
    /// It must be implemented inside #if UNITY_EDITOR.
    /// </summary>
    public interface IMonoBinderValidable
    {
        /// <summary>
        /// The View to which the Binder relates.
        /// </summary>
        public IView? View { get; set; }
        
        /// <summary>
        /// The ID that must correspond to the name of any ViewModel property.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Resets the parameters.
        /// </summary>
        public void Reset()
        {
            Id = null;
            View = null;
        }
    }
}
#endif