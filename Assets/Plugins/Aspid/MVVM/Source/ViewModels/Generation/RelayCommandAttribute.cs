namespace Aspid.MVVM;

/// <summary>
/// Marker attribute for methods within a class or structure marked with the <see cref="ViewModelAttribute"/>.
/// Used by the Source Generator to generate a property of type <see cref="IRelayCommand"/> 
/// or its overloaded versions depending on the number of parameters of the method.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public sealed class RelayCommandAttribute : Attribute
{
    /// <summary>
    /// The name of the method that determines whether the command can be executed (CanExecute).
    /// This method must return a value of type <see cref="bool"/>.
    /// If not specified, the command can always be executed.
    /// </summary>
    public string? CanExecute;
}