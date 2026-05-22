using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="Attribute"/> applied to methods of a type carrying <see cref="ViewModelAttribute"/>;
    /// directs the Source Generator to emit a matching <see cref="IRelayCommand"/> (or one of its generic
    /// overloads, picked by the method's parameter count) that wraps the decorated method.
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class RelayCommandAttribute : Attribute
    {
        /// <summary>
        /// The name of the method that determines whether the command can be executed (CanExecute).
        /// This method must return a value of type <see cref="bool"/>.
        /// If not specified, the command can always be executed.
        /// </summary>
        public string? CanExecute;
    }
}
