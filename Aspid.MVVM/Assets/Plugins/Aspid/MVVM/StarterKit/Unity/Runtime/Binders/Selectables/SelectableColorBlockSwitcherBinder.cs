#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.UI.ColorBlock, UnityEngine.UI.ColorBlock>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColorBlock;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{Selectable, ColorBlock, Converter}"/> that switches the <see cref="Selectable.colors"/>
    /// property between two <see cref="ColorBlock"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the Selectable colors between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private SelectableColorBlockSwitcherBinder _isSelected;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isSelected;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Selectable _selectable;
    ///    
    ///     private SelectableColorBlockSwitcherBinder IsSelected => new(
    ///         _selectable, _selectedColors, _defaultColors);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isSelected;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class SelectableColorBlockSwitcherBinder : SwitcherBinder<Selectable, ColorBlock, Converter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SelectableColorBlockSwitcherBinder"/> targeting the specified <see cref="Selectable"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="Selectable"/> whose <see cref="Selectable.colors"/> property is switched.</param>
        /// <param name="trueValue">The <see cref="ColorBlock"/> assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="ColorBlock"/> assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public SelectableColorBlockSwitcherBinder(
            Selectable target,
            ColorBlock trueValue,
            ColorBlock falseValue,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="SelectableColorBlockSwitcherBinder"/> targeting the specified <see cref="Selectable"/>.
        /// </summary>
        /// <param name="target">The <see cref="Selectable"/> whose <see cref="Selectable.colors"/> property is switched.</param>
        /// <param name="trueValue">The <see cref="ColorBlock"/> assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="ColorBlock"/> assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="converter">The converter used to transform the bound value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode to use.</param>
        public SelectableColorBlockSwitcherBinder(
            Selectable target,
            ColorBlock trueValue,
            ColorBlock falseValue,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(ColorBlock value) =>
            Target.colors = value;
    }
}
#endif