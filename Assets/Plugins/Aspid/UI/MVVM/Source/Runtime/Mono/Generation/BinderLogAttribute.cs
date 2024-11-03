using System;
using System.Diagnostics;

namespace Aspid.UI.MVVM.Mono.Generation
{
    /// <summary>
    /// Атрибут-маркер для методов в частичных классах или структурах, реализующих <see cref="IBinder{T}"/>.
    /// Применяется только к методам <c>SetValue</c>, которые являются неявной реализацией интерфейса <see cref="IBinder{T}"/>.
    /// Используется для логирования в процессе разработки.
    /// Используется Source Generator для генерации явной реализации метода <c>SetValue</c> с добавлением логики логирования.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BinderLogAttribute : Attribute { }
}