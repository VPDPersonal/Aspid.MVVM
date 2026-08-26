#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A converter authored once as an asset and shared by every field that references it.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>
    /// A converter field points at the asset through <see cref="ConverterAssetReference{TFrom, TTo}"/>,
    /// because a managed reference cannot hold a <see cref="ScriptableObject"/>. Each usable asset is
    /// a sealed subclass closing the type arguments — Unity cannot create an asset of an open generic.
    /// </remarks>
    public abstract class ConverterAsset<TFrom, TTo> : ScriptableObject, IConverter<TFrom?, TTo?>
    {
        [Tooltip("The converter this asset shares. Required; it must not lead back to this asset.")]
        [TypeSelector]
        [SerializeReference] private IConverter<TFrom?, TTo?>? _converter;

        [NonSerialized] private bool _isConverting;

        /// <summary>
        /// Converts the specified value using the shared converter.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The converted value, or the default value when the asset holds no converter or its
        /// converter leads back to this asset. Both are reported as errors, every time.
        /// </returns>
        public TTo? Convert(TFrom? value)
        {
            if (_converter is null)
            {
                this.LogError(problem: "no converter assigned",
                    consequence: "Returning the default value.");

                return default;
            }

            // A converter reaching back to this asset would recurse until the process dies, and a
            // stack overflow cannot be caught — the cycle has to be refused before it starts.
            if (_isConverting)
            {
                this.LogError(
                    problem: "its converter leads back to this asset",
                    consequence: "Returning the default value.");

                return default;
            }

            _isConverting = true;

            try
            {
                return _converter.Convert(value);
            }
            finally
            {
                _isConverting = false;
            }
        }
    }
}
