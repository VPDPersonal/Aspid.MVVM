using System;

namespace Aspid.UI.MVVM.ViewModels.Generation
{
    /// <summary>
    /// Атрибут-маркер для полей внутри класса или структуры, помеченных аттрибутом <see cref="ViewModelAttribute"/>.
    /// Используется Source Generator для изменения модификатора доступа сгенерированных свойств, которые по умолчанию имеют модификатор private.
    /// Для корректной работы данного атрибута требуется также наличие атрибута <see cref="BindAttribute"/> или <see cref="ReadOnlyBindAttribute"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class AccessAttribute : Attribute
    {
        /// <summary>
        /// Модификатор доступа для аксессора get.
        /// </summary>
        public Access Get { get; set; }
        
        /// <summary>
        /// Модификатор доступа для аксессора set.
        /// </summary>
        public Access Set { get; set; }
        
        /// <summary>
        /// Устанавливает модификатор доступа для сгенерированных свойств. По умолчанию используется <see cref="Access.Private"/>.
        /// </summary>
        /// <param name="access">Модификатор доступа для get и set аксессоров.</param>
        public AccessAttribute(Access access = Access.Private) { }
    }
}