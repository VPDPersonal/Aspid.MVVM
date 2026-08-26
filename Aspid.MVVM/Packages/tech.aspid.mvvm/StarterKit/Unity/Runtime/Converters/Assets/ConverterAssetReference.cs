#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Forwards conversion to a shared <see cref="ConverterAsset{TFrom, TTo}"/>.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>
    /// A managed reference cannot hold a <see cref="ScriptableObject"/>, so a converter field points
    /// at the asset through this ordinary converter instead.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Asset",
        Name = "Converter Asset Reference",
        Tooltip = "Forwards conversion to a shared ConverterAsset")]
    public class ConverterAssetReference<TFrom, TTo> : IConverter<TFrom?, TTo?>
    {
        [Tooltip("The shared converter asset. " +
            "When empty or destroyed, the reference reports an error and returns the default value.")]
        [SerializeField] private ConverterAsset<TFrom, TTo>? _asset;

        protected ConverterAssetReference() { }

        /// <param name="asset">The shared converter asset.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="asset"/> is <see langword="null"/> or already destroyed. The empty
        /// shape belongs to the Inspector, which reports it and returns the default value.
        /// </exception>
        public ConverterAssetReference(ConverterAsset<TFrom, TTo> asset)
        {
            _asset = asset ? asset : throw new ArgumentNullException(nameof(asset));
        }

        /// <summary>
        /// Converts the specified value using the referenced asset.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The converted value, or the default value when no asset is assigned or the assigned asset
        /// has been destroyed. Both are reported as errors, every time.
        /// </returns>
        public TTo? Convert(TFrom? value)
        {
            if (_asset != null)
                return _asset.Convert(value);

            this.LogError(
                problem: "no asset assigned, or the assigned asset has been destroyed",
                consequence: "Returning the default value.");

            return default;
        }
    }
}
