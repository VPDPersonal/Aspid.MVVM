using System;

namespace Aspid.UI.MVVM.Views.Generation
{
    /// <summary>
    /// Атрибут-маркер для классов и структур.
    /// Используется Source Generator для генерации реализации интерфейса <see cref="IView"/>.
    /// Так же позволяет Source Generator анализировать блоки кода внутри помеченного класса или структуры.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewAttribute : Attribute { }
}