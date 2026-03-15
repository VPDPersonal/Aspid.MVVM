#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="TargetBinder{TTarget}"/> that binds the <see cref="Object.name"/> property
    /// to a <see langword="string"/> ViewModel property.
    /// </summary>
    /// <remarks>
    /// When <see cref="BindMode.OneWayToSource"/> is active, the current <see cref="Object.name"/>
    /// is propagated to the ViewModel when binding is established.
    /// </remarks>
    /// <include file="XmlExampleDoc-Object-Name-1.1.0.xml" path="doc//member[@name='ObjectNameBinder']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed class ObjectNameBinder : TargetBinder<Object>,
        IBinder<string>, 
        IReverseBinder<string>
    {
        /// <inheritdoc/>
        public event Action<string?>? ValueChanged;
        
        [SerializeReferenceDropdown]
        [Tooltip("Optional converter applied to the string value before it is set on the target or sent back to the ViewModel.")]
        [SerializeReference] private Converter? _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="ObjectNameBinder"/> with the specified target and binding mode.
        /// </summary>
        /// <param name="target">The <see cref="GameObject"/> whose <see cref="Object.name"/> will be bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ObjectNameBinder(GameObject target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ObjectNameBinder"/> with the specified target, optional converter, and binding mode.
        /// </summary>
        /// <param name="target">The <see cref="GameObject"/> whose <see cref="Object.name"/> will be bound.</param>
        /// <param name="converter">
        /// An optional converter to transform the value before applying it or propagating it back to the ViewModel.
        /// Pass <see langword="null"/> to use the value unchanged.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>. Defaults to <see cref="BindMode.OneWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ObjectNameBinder(GameObject target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _converter = converter;
        }

        /// <inheritdoc/>
        public void SetValue(string? value) =>
            Target.name = GetConvertedValue(value);
        
        /// <summary>
        /// Called after binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, propagates the current <see cref="Object.name"/> to the ViewModel.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(Target.name));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value ?? string.Empty;
    }
}