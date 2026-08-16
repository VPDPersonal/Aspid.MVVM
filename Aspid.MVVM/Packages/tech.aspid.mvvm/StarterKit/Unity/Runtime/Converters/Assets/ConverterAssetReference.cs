#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Forwards conversion to a shared <see cref="ConverterAsset{TFrom, TTo}"/>.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>
    /// A managed reference cannot hold a <see cref="ScriptableObject"/>, so a converter field cannot
    /// point at an asset directly. This is the bridge: it is an ordinary converter, appears in the
    /// ordinary dropdown, and holds the asset in a plain object field. Every existing binder gains
    /// shared converters without changing.
    /// </remarks>
    [Serializable]
    public sealed class ConverterAssetReference<TFrom, TTo> : IConverter<TFrom?, TTo?>
    {
        [Tooltip("The shared converter asset. When empty, the default value is returned.")]
        [SerializeField] private ConverterAsset<TFrom, TTo>? _asset;

        public ConverterAssetReference() { }

        /// <param name="asset">The shared converter asset.</param>
        public ConverterAssetReference(ConverterAsset<TFrom, TTo>? asset)
        {
            _asset = asset;
        }

        /// <summary>
        /// Converts the specified value using the referenced asset.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The converted value, or the default of <typeparamref name="TTo"/> when no asset is assigned.
        /// </returns>
        public TTo? Convert(TFrom? value)
        {
            // Unity's overloaded != also catches an asset deleted from under a live reference.
            if (_asset != null)
                return _asset.Convert(value);

            Debug.LogError($"{nameof(ConverterAssetReference<TFrom, TTo>)}: no asset assigned. Returning the default value.");
            return default;
        }
    }
}
