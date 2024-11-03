using System;

namespace Aspid.UI.MVVM.ViewModels.Generation
{
    /// <summary>
    /// Атрибут-маркер для классов и структур.
    /// Используется Source Generator для генерации реализации интерфейса <see cref="IViewModel"/>.
    /// Так же позволяет Source Generator анализировать блоки кода внутри помеченного класса или структуры.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewModelAttribute : Attribute { }
}