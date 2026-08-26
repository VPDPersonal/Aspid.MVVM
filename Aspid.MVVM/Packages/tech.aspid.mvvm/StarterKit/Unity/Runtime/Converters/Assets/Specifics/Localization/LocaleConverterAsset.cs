#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using UnityEngine;
using UnityEngine.Localization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Locale"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Localization/Locale Converter", fileName = "LocaleConverter")]
    public sealed class LocaleConverterAsset : ConverterAsset<Locale?, Locale?> { }
}
#endif
