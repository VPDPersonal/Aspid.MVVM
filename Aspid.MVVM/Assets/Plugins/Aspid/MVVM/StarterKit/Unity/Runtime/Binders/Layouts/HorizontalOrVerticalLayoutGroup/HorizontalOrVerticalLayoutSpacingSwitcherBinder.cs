#nullable enable
using System;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that switches the <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property
    /// on a <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup"/> between two values based on a bound boolean ViewModel value.
    /// </summary>
    [Serializable]
    public sealed class HorizontalOrVerticalLayoutSpacingSwitcherBinder : SwitcherBinder<HorizontalOrVerticalLayoutGroup, float, Converter>
    {
        public HorizontalOrVerticalLayoutSpacingSwitcherBinder(
            HorizontalOrVerticalLayoutGroup target,
            float trueValue,
            float falseValue,
            BindMode bindMode = BindMode.OneWay)
            : this(target, trueValue, falseValue, converter: null, bindMode) { }

        public HorizontalOrVerticalLayoutSpacingSwitcherBinder(
            HorizontalOrVerticalLayoutGroup target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode bindMode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, bindMode) { }

        protected override void SetValue(float value) =>
            Target.spacing = value;
    }
}