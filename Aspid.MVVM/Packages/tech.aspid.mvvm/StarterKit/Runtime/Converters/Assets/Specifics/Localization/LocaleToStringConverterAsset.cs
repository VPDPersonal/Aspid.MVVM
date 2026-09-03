#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using UnityEngine;
using UnityEngine.Localization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Locale"/> to <see cref="string"/> conversions.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Localization/Locale To String Converter", fileName = "LocaleToStringConverter")]
    public sealed class LocaleToStringConverterAsset : ConverterAsset<Locale?, string?> { }
}
#endif
