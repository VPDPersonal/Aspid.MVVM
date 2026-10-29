using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates a <see cref="RelayCommand"/> property, named after the method with a <c>Command</c> suffix, for the method of
    /// a <see cref="ViewModelAttribute">[ViewModel]</see> class.
    /// </summary>
    /// <remarks>
    /// The method returns <see langword="void"/> and takes up to four parameters.
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
    [AttributeUsage(validOn: AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class RelayCommandAttribute : Attribute
    {
        /// <summary>
        /// The name of a <see cref="bool"/> method or property of the same class that allows execution; <see langword="null"/> to
        /// always allow it.
        /// </summary>
        /// <remarks>
        /// A method takes either the command's parameters or none.
        /// </remarks>
        public string? CanExecute;
    }
}
