using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract class ViewInitializerBase : MonoBehaviour
    {
        public bool IsInitialized { get; protected set; }
        
        public IViewModel ViewModel { get; protected set; }
    }
}