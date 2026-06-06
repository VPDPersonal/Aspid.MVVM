using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{TElement, Vector3, IConverter{Vector3, Vector3}}"/> that fixes
    /// the value type to <see cref="Vector3"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract class EnumGroupVector3MonoBinder<TElement> : EnumGroupMonoBinder<TElement, Vector3, Converter> { }
}