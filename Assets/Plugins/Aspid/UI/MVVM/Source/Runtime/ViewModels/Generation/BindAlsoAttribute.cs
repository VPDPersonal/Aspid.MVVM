using System;

namespace Aspid.UI.MVVM.ViewModels.Generation
{
    /// <summary>
    /// Атрибут-маркер для полей внутри класса или структуры, помеченных аттрибутом <see cref="ViewModelAttribute"/>.
    /// Используется Source Generator для генерации вызова события в сгенерированном свойстве с именем propertyName.
    /// Для корректной работы данного атрибута требуется также наличие атрибута <see cref="BindAttribute"/> или <see cref="ReadOnlyBindAttribute"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BindAlsoAttribute : Attribute
    {
        public BindAlsoAttribute(string propertyName) { }
    }
}