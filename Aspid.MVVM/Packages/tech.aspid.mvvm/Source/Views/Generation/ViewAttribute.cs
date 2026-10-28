using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates the <see cref="IView"/> implementation of the class, which binds its binder fields and properties.
    /// </summary>
    /// <remarks>
    /// The class must be <see langword="partial"/>; a struct is ignored.
    /// </remarks>
    /// <example>
    /// <code>
    /// [View]
    /// public partial class PlayerView : MonoView { }
    /// </code>
    /// </example>
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether a View that implements <see cref="IView{T}"/> gets a serialized
        /// <c>MonoBinder</c> array for each bindable member of <c>T</c> that it does not declare. <see langword="true"/> by default.
        /// </summary>
        /// <remarks>
        /// Has no effect when <c>T</c> is an interface or the View derives from <c>ScriptableObject</c>.
        /// </remarks>
        public bool AutoBinderFields { get; set; } = true;
    }
}
