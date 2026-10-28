using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates the <see cref="IViewModel"/> implementation of the class from its bind members and
    /// <see cref="RelayCommandAttribute">[RelayCommand]</see> methods.
    /// </summary>
    /// <remarks>
    /// The class must be <see langword="partial"/>; a struct is ignored.
    /// </remarks>
    /// <example>
    /// <code>
    /// [ViewModel]
    /// public partial class PlayerViewModel { }
    /// </code>
    /// </example>
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewModelAttribute : Attribute { }
}
