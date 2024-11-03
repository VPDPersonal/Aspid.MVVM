using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Views.Extensions;
using Aspid.UI.MVVM.ViewModels.Extensions;

namespace Aspid.UI.MVVM.Mono.Initializers
{
    /// <summary>
    /// Абстрактный базовый класс для инициализации View с использованием ViewModel.
    /// Наследует <see cref="MonoBehaviour"/> и предоставляет механизм автоматической инициализации и освобождения ресурсов.
    /// </summary>
    public abstract class MonoViewInitializerBase : MonoBehaviour
    {
        [SerializeField] private bool _isDisposeViewOnDestroy;
        [SerializeField] private bool _isDisposeViewModelOnDestroy;
        
        /// <summary>
        /// Абстрактное свойство, которое должно возвращать View.
        /// </summary>
        protected abstract IView View { get; }
        
        /// <summary>
        /// Абстрактное свойство, которое должно возвращать ViewModel.
        /// </summary>
        protected abstract IViewModel ViewModel { get; }

        /// <summary>
        /// Инициализирует View, связывая его с ViewModel.
        /// </summary>
        protected void Initialize() => View.Initialize(ViewModel);

        protected virtual void OnDestroy()
        {
            if (_isDisposeViewOnDestroy) 
                View.DisposeView();
            
            if (_isDisposeViewModelOnDestroy) 
                ViewModel.DisposeViewModel();
        }
    }
}