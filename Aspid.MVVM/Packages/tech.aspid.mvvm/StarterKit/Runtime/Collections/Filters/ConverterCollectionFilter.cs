#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICollectionFilter{T}"/> that passes an element when an <see cref="IConverter{TFrom, TTo}"/>
    /// to <see cref="bool"/> answers <see langword="true"/> for it. An empty slot passes everything.
    /// </summary>
    /// <typeparam name="T">The element type being filtered.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid",
        Name = "Converter",
        Tooltip = "Passes an element when a converter to bool answers true for it")]
    public class ConverterCollectionFilter<T> : ICollectionFilter<T>
    {
        [Tooltip("Converter that decides whether an element passes.")]
        [TypeSelector]
        [SerializeReference] private IConverter<T, bool>? _converter;

        protected ConverterCollectionFilter() { }

        /// <param name="converter">The converter that decides whether an element passes.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        public ConverterCollectionFilter(IConverter<T, bool> converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <inheritdoc/>
        public bool Matches(T item) =>
            _converter is null || _converter.Convert(item);
    }
}
