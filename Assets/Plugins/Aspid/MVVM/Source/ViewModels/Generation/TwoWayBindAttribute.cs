namespace Aspid.MVVM;

/// <summary>
/// Marker attribute for fields within a class or structure marked with the <see cref="ViewModelAttribute"/>.
/// Used by the Source Generator to generate a property based on the marked field.
/// This attribute enforces <see cref="BindMode.TwoWay"/> binding mode.
/// Note: This attribute does not work with readonly fields.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class TwoWayBindAttribute : BaseBindAttribute { }