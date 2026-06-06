// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="BaseBindAttribute"/> applied to fields of a type carrying <see cref="ViewModelAttribute"/>;
    /// directs the Source Generator to emit a bindable property for the field.
    /// The default constructor selects <see cref="BindMode.TwoWay"/> for mutable fields and <see cref="BindMode.OneTime"/>
    /// for <see langword="readonly"/> fields. When the mode-taking constructor is used on a <see langword="readonly"/> field,
    /// <see cref="BindMode.OneTime"/> and <see cref="BindMode.OneWay"/> both resolve to <see cref="BindMode.OneTime"/>;
    /// any other mode is rejected.
    /// </summary>
    public sealed class BindAttribute : BaseBindAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindAttribute"/> class with the default binding mode.
        /// For non-readonly fields, the default mode is <see cref="BindMode.TwoWay"/>.
        /// For readonly fields, the default mode is <see cref="BindMode.OneTime"/>.
        /// </summary>
        public BindAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindAttribute"/> class with the specified binding mode.
        /// For readonly fields, only <see cref="BindMode.OneTime"/> and <see cref="BindMode.OneWay"/> are supported, 
        /// both of which will behave as <see cref="BindMode.OneTime"/>.
        /// Other modes are not supported for readonly fields.
        /// </summary>
        /// <param name="mode">The desired binding mode for the field.</param>
        public BindAttribute(BindMode mode) { }
    }
}
