#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides the conversion between a generic <see cref="IConverter{TFrom, TTo}"/> and the non-generic alias a
    /// serialized field needs before Unity 2023.1, for the Unity value types.
    /// </summary>
    /// <remarks>
    /// The Unity-side half of <see cref="ConverterBridge"/>; see it for why the bridge exists at all. Kept in a separate
    /// type rather than as a partial of the same one, because the two live in different assemblies.
    /// </remarks>
    public static class ConverterBridgeUnity
    {
        /// <summary>
        /// Converts a <see cref="Color"/> converter to the form the serialized field takes.
        /// </summary>
        /// <param name="converter">The converter to bridge, or <see langword="null"/>.</param>
        /// <returns>The converter in the form the field expects, or <see langword="null"/>.</returns>
        #if UNITY_2023_1_OR_NEWER
        public static IConverter<Color, Color>? Color(IConverter<Color, Color>? converter) => converter;
        #else
        public static IConverterColor? Color(IConverter<Color, Color>? converter) => converter?.ToConvertSpecific();
        #endif

        /// <inheritdoc cref="Color"/>
        #if UNITY_2023_1_OR_NEWER
        public static IConverter<Vector2, Vector2>? Vector2(IConverter<Vector2, Vector2>? converter) => converter;
        #else
        public static IConverterVector2? Vector2(IConverter<Vector2, Vector2>? converter) => converter?.ToConvertSpecific();
        #endif

        /// <inheritdoc cref="Color"/>
        #if UNITY_2023_1_OR_NEWER
        public static IConverter<Vector3, Vector3>? Vector3(IConverter<Vector3, Vector3>? converter) => converter;
        #else
        public static IConverterVector3? Vector3(IConverter<Vector3, Vector3>? converter) => converter?.ToConvertSpecific();
        #endif

        /// <inheritdoc cref="Color"/>
        #if UNITY_2023_1_OR_NEWER
        public static IConverter<Quaternion, Quaternion>? Quaternion(IConverter<Quaternion, Quaternion>? converter) => converter;
        #else
        public static IConverterQuaternion? Quaternion(IConverter<Quaternion, Quaternion>? converter) => converter?.ToConvertSpecific();
        #endif
    }
}
