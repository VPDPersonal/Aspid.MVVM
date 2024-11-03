using System;
using System.Diagnostics;

namespace Aspid.UI.MVVM.Mono.Views
{
    /// <summary>
    /// Атрибут, используемый для указания обязательного типа для Binder,
    /// реализующих интерфейс <see cref="IMonoBinderValidable"/>.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class RequireBinderAttribute : Attribute
    {
        /// <summary>
        /// Тип, который должен поддерживать Binder.
        /// </summary>
        /// <example>
        /// Если указанный тип — <c>string</c>, то Binder должен реализовывать интерфейс <c>IBinder{string}</c>.
        /// Это гарантирует, что Binder поддерживает работу с указанным типом во время выполнения.
        /// </example>
        public Type Type { get; }
        
        /// <summary>
        /// Инициализирует новый экземпляр атрибута <see cref="RequireBinderAttribute"/> 
        /// с указанным типом, который должен поддерживаться Binder.
        /// </summary>
        /// <param name="type">Тип, который должен поддерживаться Binder.</param>
        public RequireBinderAttribute(Type type)
        {
            Type = type;
        }
    }
}