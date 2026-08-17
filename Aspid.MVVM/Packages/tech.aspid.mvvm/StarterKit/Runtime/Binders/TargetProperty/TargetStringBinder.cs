using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget, string, IConverter{string, string}}"/> that binds a <see langword="string"/> property.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see langword="string"/> property.</typeparam>
    public abstract class TargetStringBinder<TTarget> : TargetBinder<TTarget, string, Converter>
    {
        /// <inheritdoc/>
        protected TargetStringBinder(TTarget target, IConverter<string?, string?>? converter, BindMode mode) 
            : base(target, converter, mode) { }
        
    }
}