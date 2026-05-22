#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ComponentToSourceMonoBinder{TMP_Dropdown}"/> that reads the current
    /// <see cref="TMP_Dropdown"/> state back to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Dropdown))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown To Source Binder")]
    public sealed class DropdownToSourceMonoBinder : ComponentToSourceMonoBinder<TMP_Dropdown> { }
}
#endif