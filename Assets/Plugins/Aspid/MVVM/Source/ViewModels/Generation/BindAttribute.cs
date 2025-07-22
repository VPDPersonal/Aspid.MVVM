namespace Aspid.MVVM;

/// <summary>
/// Marker attribute for fields within a class or structure marked with the <see cref="ViewModelAttribute"/>.
/// Used by the Source Generator to generate a property based on the marked field.
/// If the default constructor is used, the field will be set with <see cref="BindMode.TwoWay"/>.
/// For <c>readonly</c> fields, the binding mode will be set to <see cref="BindMode.OneTime"/>.
/// If the constructor with one parameter is used for a <c>readonly</c> field, <see cref="BindMode.OneTime"/> and
/// <see cref="BindMode.OneWay"/> will both function as <see cref="BindMode.OneTime"/>. All other modes for 
/// <c>readonly</c> fields will not be supported.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class BindAttribute : BaseBindAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BindAttribute"/> class with the default binding mode.
    /// For non-readonly fields, the default mode is <see cref="BindMode.TwoWay"/>.
    /// For readonly fields, the default mode is <see cref="BindMode.OneTime"/>.
    /// /// </summary>
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