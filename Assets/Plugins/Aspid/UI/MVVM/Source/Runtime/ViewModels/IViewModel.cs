namespace Aspid.UI.MVVM.ViewModels
{
    /// <summary>
    /// Интерфейс для ViewModel, который поддерживает привязку данных.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Добавляет Binder для указанного свойства ViewModel.
        /// </summary>
        /// <param name="binder">Binder, который будет добавлен.</param>
        /// <param name="propertyName">Имя свойства, к которому будет привязан Binder</param>
        public void AddBinder(IBinder binder, string propertyName);

        /// <summary>
        /// Удаляет Binder для указанного свойства ViewModel.
        /// </summary>
        /// <param name="binder">Binder, который будет удален.</param>
        /// <param name="propertyName">Имя свойства, от которого будет отвязан Binder.</param>
        public void RemoveBinder(IBinder binder, string propertyName);
    }
}