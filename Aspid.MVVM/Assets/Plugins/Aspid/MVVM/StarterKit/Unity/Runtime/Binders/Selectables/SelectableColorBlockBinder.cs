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
    /// <see cref="TargetBinder{Selectable, ColorBlock, Converter}"/> that sets the <see cref="Selectable.colors"/> property.
    /// </summary>
    /// <example>
    /// Set the Selectable colors based on a ColorBlock ViewModel value.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private SelectableColorBlockBinder _colors;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public ColorBlock _colors;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private Selectable _selectable;
    ///    
    ///     private SelectableColorBlockBinder Colors =>
    ///         new(_selectable);
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public ColorBlock _colors;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public class SelectableColorBlockBinder : TargetBinder<Selectable, ColorBlock, Converter>
    {
        /// <inheritdoc/>
        protected sealed override ColorBlock Property
        {
            get => Target.colors;
            set => Target.colors = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SelectableColorBlockBinder"/> targeting the specified <see cref="Selectable"/>
        /// with no converter.
        /// </summary>
        /// <param name="target">The <see cref="Selectable"/> whose <see cref="Selectable.colors"/> property is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public SelectableColorBlockBinder(Selectable target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="SelectableColorBlockBinder"/> targeting the specified <see cref="Selectable"/>.
        /// </summary>
        /// <param name="target">The <see cref="Selectable"/> whose <see cref="Selectable.colors"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public SelectableColorBlockBinder(Selectable target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}