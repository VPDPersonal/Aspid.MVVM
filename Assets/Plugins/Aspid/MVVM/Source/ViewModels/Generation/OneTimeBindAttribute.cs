namespace Aspid.MVVM;

/// <summary>
/// Marker attribute for fields within a class or structure marked with the <see cref="ViewModelAttribute"/>.
/// Used by the Source Generator to generate a property based on the marked field.
/// This attribute enforces <see cref="BindMode.OneTime"/> binding mode.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class OneTimeBindAttribute : BaseBindAttribute { }