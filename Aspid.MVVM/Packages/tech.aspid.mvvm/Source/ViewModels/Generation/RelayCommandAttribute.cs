using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates a relay command for the marked method of a <see cref="ViewModelAttribute">[ViewModel]</see> class.
    /// </summary>
    /// <remarks>
    /// The generated property uses the method name with a <c>Command</c> suffix. The method must be non-generic,
    /// return <see langword="void"/> and take up to four value parameters.
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
        /// The name of a boolean field, readable property or predicate method that allows execution.
        /// The default, <see langword="null"/>, always allows execution.
        /// </summary>
        /// <remarks>
        /// Method overload resolution, generic type inference and constraints follow C# delegate conversion rules; an invalid predicate reports generator error <c>ASPIDVM004</c>.
        /// </remarks>
        public string? CanExecute;
    }
}
