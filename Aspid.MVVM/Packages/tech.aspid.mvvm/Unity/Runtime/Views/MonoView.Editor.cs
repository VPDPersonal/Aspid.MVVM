#if UNITY_EDITOR
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public partial class MonoView
    {
        [Tooltip("The type filters for the design ViewModel selector.")]
        [SerializeField] private string[] _designViewModelAssemblyQualifiedNames =
        {
            typeof(IViewModel).AssemblyQualifiedName,
        };

        [Tooltip("The ViewModel type used to configure bindings in the Inspector.")]
        [TypeSelector(nameof(_designViewModelAssemblyQualifiedNames))]
        [SerializeField] private string _designViewModel;
    }
}
#endif
