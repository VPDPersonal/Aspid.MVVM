using System;

namespace Aspid.UI.MVVM.Views.Generation
{
    /// <summary>
    /// Атрибут-маркер для полей и свойств внутри класса или структуры, помеченных атрибутом <see cref="ViewAttribute"/>.
    /// Используется Source Generator для генерации кода привязки по предоставленному типу <see cref="IBinder"/> во View.
    /// </summary>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class AsBinderAttribute : Attribute
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AsBinderAttribute"/> с указанным типом <see cref="IBinder"/> и дополнительными аргументами.
        /// </summary>
        /// <param name="type">Тип <see cref="IBinder"/>, который будет использоваться для связывания поля или свойства.</param>
        /// <param name="args">Дополнительные аргументы, которые могут быть переданы конструктору типа <see cref="IBinder"/>.</param>
        public AsBinderAttribute(Type type, params object[] args) { }
    }
}