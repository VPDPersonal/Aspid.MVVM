using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinderWithConverter{T1, T2}">TargetBinderWithConverter&lt;TTarget, string&gt;</see> that binds a <see langword="string"/> property.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see langword="string"/> property.</typeparam>
    [Serializable]
    public abstract class TargetStringBinder<TTarget> : TargetBinderWithConverter<TTarget, string>
    {
        /// <inheritdoc/>
        protected TargetStringBinder(TTarget target, IConverter<string?, string?>? converter, BindMode mode) 
            : base(target, converter, mode) { }
        
    }
}