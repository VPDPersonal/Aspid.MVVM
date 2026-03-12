using UnityEngine;
using System.Runtime.CompilerServices;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="SwitcherBinder{TTarget, Vector3, IConverter{Vector3, Vector3}}"/> that fixes
    /// the value type to <see cref="Vector3"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that exposes the target property.</typeparam>
    public abstract class SwitcherVector3Binder<TTarget> : SwitcherBinder<TTarget, Vector3, Converter>
    {
        /// <inheritdoc />
        protected SwitcherVector3Binder(
            TTarget target, 
            Vector3 trueValue, 
            Vector3 falseValue,
            IConverter<Vector3, Vector3>? converter, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, GetConverter(converter), mode) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Converter? GetConverter(IConverter<Vector3, Vector3>? converter)
        {
            #if UNITY_2023_1_OR_NEWER
            return converter;
            #else
            return converter?.ToConvertSpecific();
            #endif
        }
    }
}