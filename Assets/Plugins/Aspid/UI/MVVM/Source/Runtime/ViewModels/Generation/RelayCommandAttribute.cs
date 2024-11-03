using System;

namespace Aspid.UI.MVVM.ViewModels.Generation
{
    /// <summary>
    /// Атрибут-маркер для методов внутри класса или структуры, помеченных аттрибутом <see cref="ViewModelAttribute"/>.
    /// Используется Source Generator для генерации свойства типа <see cref="Aspid.UI.MVVM.Commands.IRelayCommand"/>
    /// или его перегруженных версий в зависимости от количества параметров метода.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class RelayCommandAttribute : Attribute
    {
        /// <summary>
        /// Имя метода, который определяет, может ли команда быть выполнена (CanExecute).
        /// Этот метод должен возвращать значение типа <see cref="bool"/>.
        /// Если не указано, команда всегда может быть выполнена.
        /// </summary>
        public string? CanExecute;
    }
}