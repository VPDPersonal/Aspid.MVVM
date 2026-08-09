#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines two <see cref="Vector2"/> values by selecting which components to use from each vector.
    /// Supports optional pre- and post-conversion transformations.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector2 Combine", Tooltip = "Combines two  values by selecting which components to use from each vector")]
    public sealed class Vector2CombineConverter
    {
        [Tooltip("Which components come from the bound vector; the rest come from the reference one.")]
        [SerializeField] private Mode _mode;

        [Tooltip("Applied to the bound vector before the components are selected.")]
        [SerializeReference] private Converter? _preConvertor;

        [Tooltip("Applied to the combined result.")]
        [SerializeReference] private Converter? _postConvertor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2CombineConverter"/> class with XY mode.
        /// </summary>
        public Vector2CombineConverter() :
            this(Mode.XY) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2CombineConverter"/> class.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        public Vector2CombineConverter(Mode mode)
        {
            _mode = mode;
            _preConvertor = default;
            _postConvertor = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2CombineConverter"/> class with conversion functions.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        /// <param name="preConvertor">Applied to the bound vector before the components are selected.</param>
        /// <param name="postConvertor">Applied to the combined result.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when either function is <see langword="null"/>. Use the converter overload with
        /// <see langword="null"/> to leave a stage out.
        /// </exception>
        public Vector2CombineConverter(
            Mode mode,
            Func<Vector2, Vector2> preConvertor,
            Func<Vector2, Vector2> postConvertor)
            : this(mode, preConvertor.ToConvert(), postConvertor.ToConvert()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2CombineConverter"/> class with converter interfaces.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        /// <param name="preConvertor">
        /// Applied to the bound vector before the components are selected, or <see langword="null"/> to
        /// leave that stage out.
        /// </param>
        /// <param name="postConvertor">
        /// Applied to the combined result, or <see langword="null"/> to leave that stage out.
        /// </param>
        public Vector2CombineConverter(
            Mode mode,
            Converter? preConvertor,
            Converter? postConvertor)
        {
            _mode = mode;
            _preConvertor = preConvertor;
            _postConvertor = postConvertor;
        }

        /// <summary>
        /// Combines two vectors by selecting components from each based on the configured mode.
        /// </summary>
        /// <param name="from">The first vector, whose components are selected based on the mode.</param>
        /// <param name="to">The second vector, whose components fill in the remaining parts.</param>
        /// <returns>The combined vector.</returns>
        public Vector2 Convert(Vector2 from, Vector2 to)
        {
            from = _preConvertor?.Convert(from) ?? from;

            from = _mode switch
            {
                Mode.X => new Vector2(from.x, to.y),
                Mode.Y => new Vector2(to.x, from.y),
                Mode.XY => new Vector2(from.x, from.y),
                _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
            };

            return _postConvertor?.Convert(from) ?? from;
        }

        /// <summary>
        /// Gets a default converter instance that uses XY mode.
        /// </summary>
        public static Vector2CombineConverter Default => new(Mode.XY);

        /// <summary>
        /// Specifies which components to take from the first vector when combining.
        /// </summary>
        public enum Mode
        {
            X,
            Y,
            XY,
        }
    }
}