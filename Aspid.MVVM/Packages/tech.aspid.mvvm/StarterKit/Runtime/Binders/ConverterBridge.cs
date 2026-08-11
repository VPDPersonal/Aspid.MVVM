#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides the conversion between a generic <see cref="IConverter{TFrom, TTo}"/> and the non-generic alias a
    /// serialized field needs before Unity 2023.1.
    /// </summary>
    /// <remarks>
    /// A <c>[SerializeReference]</c> field could not be typed on a generic interface until 2023.1, so every binder with a
    /// converter declares its field as a non-generic alias on older versions and as the generic interface on newer ones —
    /// and each of them carried its own copy of the same four-line bridge to convert a constructor argument to whichever
    /// one the field is. There were twelve copies of it, differing only in the type.
    /// <para/>
    /// On 2023.1 and newer every method here is the identity, and the JIT removes the call.
    /// </remarks>
    public static class ConverterBridge
    {
        /// <summary>
        /// Converts an <see langword="int"/> converter to the form the serialized field takes.
        /// </summary>
        /// <param name="converter">The converter to bridge, or <see langword="null"/>.</param>
        /// <returns>The converter in the form the field expects, or <see langword="null"/>.</returns>
        #if UNITY_2023_1_OR_NEWER
        public static IConverter<int, int>? Int(IConverter<int, int>? converter) => converter;
        #else
        public static IConverterInt? Int(IConverter<int, int>? converter) => converter?.ToConvertSpecific();
        #endif

        /// <inheritdoc cref="Int"/>
        #if UNITY_2023_1_OR_NEWER
        public static IConverter<float, float>? Float(IConverter<float, float>? converter) => converter;
        #else
        public static IConverterFloat? Float(IConverter<float, float>? converter) => converter?.ToConvertSpecific();
        #endif

        /// <inheritdoc cref="Int"/>
        #if UNITY_2023_1_OR_NEWER
        public static IConverter<string?, string?>? String(IConverter<string?, string?>? converter) => converter;
        #else
        public static IConverterString? String(IConverter<string?, string?>? converter) => converter?.ToConvertSpecific();
        #endif
    }
}
