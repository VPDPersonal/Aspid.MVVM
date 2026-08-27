using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that applies an optional
    /// <see cref="IConverter{TFrom, TTo}"/> to values in both binding directions; in <see cref="BindMode.OneWayToSource"/>,
    /// the current property value is converted before being sent back to the ViewModel.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    /// <typeparam name="TProperty">The type of the property being bound.</typeparam>
    public abstract class ComponentMonoBinderWithConverter<TComponent, TProperty> : ComponentMonoBinder<TComponent, TProperty>
        where TComponent : Component
    {
        [Tooltip("Optional converter for the component. Reverses only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<TProperty, TProperty> _converter;

        /// <inheritdoc/>
        protected override TProperty GetConvertedValue(TProperty value) =>
            _converter is not null ? _converter.Convert(value) : value;

        /// <inheritdoc/>
        protected override TProperty GetConvertedBackValue(TProperty value) =>
            _converter is ITwoWayConverter<TProperty, TProperty> twoWay ? twoWay.ConvertBack(value) : value;

        /// <inheritdoc/>
        protected override void OnBound()
        {
            WarnAboutOneWayConverter();
            base.OnBound();
        }

        private void WarnAboutOneWayConverter()
        {
            if (Mode is not (BindMode.OneWayToSource or BindMode.TwoWay)) return;
            if (_converter is null or ITwoWayConverter<TProperty, TProperty>) return;

            Debug.LogWarning(
                $"{GetType().Name} is bound as {Mode} with {_converter.GetType().Name}, which converts one way only. Values sent back to the ViewModel are not converted.",
                context: this);
        }
    }
}
