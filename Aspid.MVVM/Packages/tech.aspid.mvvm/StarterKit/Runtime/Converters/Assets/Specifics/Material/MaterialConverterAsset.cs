#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ConverterAsset{TFrom, TTo}"/> for <see cref="Material"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Material/Material Converter", fileName = "MaterialConverter")]
    public sealed class MaterialConverterAsset : ConverterAsset<Material?, Material?> { }
}
