#nullable disable
using System;
using UnityEngine;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono
{
    /// <summary>
    /// Абстрактный класс, наследуемый от <see cref="MonoBehaviour"/>, реализующий базовую логику для связывания компонента с <see cref="IViewModel"/>.
    /// Включает методы для привязки и разрыва привязки компонента с ViewModel.
    /// Наследники должны реализовать один или несколько интерфейсов <see cref="IBinder{T}"/>, чтобы завершить реализацию конкретной логики привязки.
    /// </summary>
    public abstract partial class MonoBinder : MonoBehaviour, IBinder
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new("MonoBinder.Bind");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new("MonoBinder.Unbind");
#endif
        /// <summary>
        /// Указывает, разрешена ли привязка.
        /// Значение по умолчанию - true.
        /// </summary>
        protected virtual bool IsBind => true;
        
        /// <summary>
        /// Привязывает компонент к указанной <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel для привязки.</param>
        /// <param name="id">ID компонента для привязки, который совпадает с именем свойства у ViewModel.</param>
        public void Bind(IViewModel viewModel, string id)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                if (!IsBind) return;
                ThrowExceptionIfInvalidData(viewModel, id);
                
                OnBinding(viewModel, id);
                OnBindingDebug(viewModel, id);
                
                viewModel.AddBinder(this, id);
                OnBound(viewModel, id);
            }
        }

        partial void OnBindingDebug(IViewModel viewModel, string id);
        
        /// <summary>
        /// Логика выполняемая перед привязкой, которая может быть переопределена в производных классах.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel.</param>
        /// <param name="id">ID компонента, который совпадает с именем свойства у ViewModel.</param>
        protected virtual void OnBinding(IViewModel viewModel, string id) { }
        
        /// <summary>
        /// Логика выполняемая после привязки, которая может быть переопределена в производных классах.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel.</param>
        /// <param name="id">ID компонента, который совпадает с именем свойства у ViewModel.</param>
        protected virtual void OnBound(IViewModel viewModel, string id) { }
        
        /// <summary>
        /// Разрывает привязку компонента с указанной <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel для привязки.</param>
        /// <param name="id">ID компонента для привязки, который совпадает с именем свойства у ViewModel.</param>
        public void Unbind(IViewModel viewModel, string id)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                if (!IsBind) return;
                ThrowExceptionIfInvalidData(viewModel, id);

                OnUnbindingDebug(viewModel, id);
                OnUnbinding(viewModel, id);
                
                viewModel.RemoveBinder(this, id);
                OnUnbound(viewModel, id);
            }
        }
        
        partial void OnUnbindingDebug(IViewModel viewModel, string id);
        
        /// <summary>
        /// Логика выполняемая перед разрывом привязки, которая может быть переопределена в производных классах.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel.</param>
        /// <param name="id">ID компонента, который совпадает с именем свойства у ViewModel.</param>
        protected virtual void OnUnbinding(IViewModel viewModel, string id) { }
        
        /// <summary>
        /// Логика выполняемая после разрыва привязки, которая может быть переопределена в производных классах.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel.</param>
        /// <param name="id">ID компонента, который совпадает с именем свойства у ViewModel.</param>
        protected virtual void OnUnbound(IViewModel viewModel, string id) { }
        
        private static void ThrowExceptionIfInvalidData(IViewModel viewModel, string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));
        }
    }
}