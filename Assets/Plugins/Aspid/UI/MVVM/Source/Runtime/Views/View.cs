using System;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Views
{
    /// <summary>
    /// Абстрактный класс для View, реализующий интерфейс <see cref="IView"/>.
    /// Предоставляет методы для инициализации и деинициализации View с <see cref="IViewModel"/> для привязки.
    /// </summary>
    public abstract class View : IView, IDisposable
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _initializeMarker = new("View.Initialize");
        private static readonly Unity.Profiling.ProfilerMarker _deinitializationMarker = new("View.Deinitialization");
#endif
        
        /// <summary>
        /// Получает связанный ViewModel.
        /// Если представление не инициализировано, может возвращать <c>null</c>.
        /// </summary>
        public IViewModel? ViewModel { get; private set; }
        
        /// <summary>
        /// Инициализирует представление с заданным <see cref="IViewModel"/> для привязки.
        /// </summary>
        /// <param name="viewModel">Объект <see cref="IViewModel"/> для инициализации View.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="viewModel"/> равен <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">Выбрасывается, если Мшуц уже инициализировано.</exception>
        public void Initialize(IViewModel viewModel)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_initializeMarker.Auto())
#endif
            {
                if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));
                if (ViewModel is not null) throw new InvalidOperationException("View is already initialized.");
                
                ViewModel = viewModel;
                InitializeIternal(viewModel);
            }
        }

        /// <summary>
        /// Абстрактный метод для внутренней инициализации View. 
        /// Должен быть переопределен в производном классе для реализации специфической логики инициализации.
        /// </summary>
        /// <param name="viewModel">Объект <see cref="IViewModel"/> для инициализации View.</param>
        protected abstract void InitializeIternal(IViewModel viewModel);

        /// <summary>
        /// Деинициализирует View, обнуляя связанный <see cref="ViewModel"/>.
        /// </summary>
        public void Deinitialize()
        {
            if (ViewModel == null) return;
            
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_deinitializationMarker.Auto())
#endif
            {
                DeinitializeIternal();
                ViewModel = null;
            }
        }

        /// <summary>
        /// Абстрактный метод для внутренней деинициализации представления. 
        /// Должен быть переопределен в производном классе для реализации специфической логики деинициализации.
        /// </summary>
        protected abstract void DeinitializeIternal();
        
        /// <summary>
        /// Освобождает используемые ресурсы.
        /// Может быть переопределен наследником.
        /// Вызывает <see cref="Deinitialize"/> для правильной очистки.
        public virtual void Dispose() => Deinitialize();
    }
}