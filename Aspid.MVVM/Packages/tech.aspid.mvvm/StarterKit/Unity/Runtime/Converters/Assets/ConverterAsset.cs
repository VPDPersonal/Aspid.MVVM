#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A converter authored once as an asset and shared by every field that references it.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>
    /// A <c>[SerializeReference]</c> converter is data belonging to one field, so a large gradient or
    /// enum map has to be re-authored in every prefab that wants it; an asset is authored once and
    /// referenced.
    /// <para>
    /// A managed reference cannot hold a <see cref="ScriptableObject"/>, so a field points at one
    /// through <see cref="ConverterAssetReference{TFrom, TTo}"/>. Unity cannot create an asset of an
    /// open generic type either, so each usable asset is a sealed subclass that closes the arguments.
    /// </para>
    /// </remarks>
    public abstract class ConverterAsset<TFrom, TTo> : ScriptableObject, IConverter<TFrom?, TTo?>
    {
        [Tooltip("The converter this asset shares. When empty, the default value is returned.")]
        [SerializeReference] private IConverter<TFrom?, TTo?>? _converter;

        /// <summary>
        /// Converts the specified value using the shared converter.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The converted value, or the default of <typeparamref name="TTo"/> when the asset is empty.
        /// </returns>
        public TTo? Convert(TFrom? value) =>
            _converter is null ? default : _converter.Convert(value);
    }
}
