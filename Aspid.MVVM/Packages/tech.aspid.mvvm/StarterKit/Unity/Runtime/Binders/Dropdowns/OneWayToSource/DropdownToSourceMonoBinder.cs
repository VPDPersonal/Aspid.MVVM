using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentToSourceMonoBinder{TMP_Dropdown}"/> that sends the cached <see cref="TMP_Dropdown"/>
    /// component reference to the ViewModel when binding is established.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Dropdown))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown To Source Binder")]
    public sealed class DropdownToSourceMonoBinder : ComponentToSourceMonoBinder<TMP_Dropdown> { }
}