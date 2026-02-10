#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_Text))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text To Source Binder")]
    public sealed class TextToSourceMonoBinder : ComponentToSourceMonoBinder<TMP_Text> { }
}
#endif