#if UNITY_EDITOR
using Aspid.UI.MVVM.Views;

namespace Aspid.UI.MVVM.Mono
{
    /// <summary>
    /// Интерфейс, который нужен для валидации Binder внутри Editor.
    /// Необходимо реализовывать внутри #if UNITY_EDITOR.
    /// </summary>
    public interface IMonoBinderValidable
    {
        /// <summary>
        /// View, к которой относится Binder.
        /// </summary>
        public IView? View { get; set; }
        
        /// <summary>
        /// Id, который должен соответствовать имени свойства любой ViewModel.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Сброс параметров
        /// </summary>
        public void Reset()
        {
            Id = null;
            View = null;
        }
    }
}
#endif