using System;

namespace Aspid.UI.MVVM.ViewModels.Generation
{
    /// <summary>
    /// Атрибут-маркер для полей внутри класса или структуры, помеченных атрибутом <see cref="ViewModelAttribute"/>.
    /// Используется Source Generator для генерации свойство только для чтения на основе помеченного поля.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ReadOnlyBindAttribute : Attribute { }
}