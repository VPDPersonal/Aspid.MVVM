using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates a <see cref="RelayCommand"/> property, named after the method with a <c>Command</c> suffix, for the method of
    /// a <see cref="ViewModelAttribute">[ViewModel]</see> class.
    /// </summary>
    /// <remarks>
    /// The method is non-generic, returns <see langword="void"/> and takes up to four value parameters.
    /// </remarks>
    /// <example>
    /// <code>
    /// [RelayCommand]
    /// private void Save() { }
    /// </code>
    /// <code>
    /// [RelayCommand(CanExecute = nameof(CanBuy))]
    /// private void Buy(int count) { }
    ///
    /// private bool CanBuy(int count) => count &lt;= _stock;
    /// </code>
    /// </example>
    [AttributeUsage(validOn: AttributeTargets.Method, Inherited = false)]
    public sealed class RelayCommandAttribute : Attribute
    {
        /// <summary>
        /// The name of a <see cref="bool"/> field, readable property or compatible method accessible from the class that allows execution; <see langword="null"/> to
        /// always allow it.
        /// </summary>
        /// <remarks>
        /// Method overload resolution, generic type inference and constraints follow C# delegate conversion rules; an invalid predicate reports generator error <c>ASPIDVM004</c>.
        /// </remarks>
        public string? CanExecute;
    }
}
