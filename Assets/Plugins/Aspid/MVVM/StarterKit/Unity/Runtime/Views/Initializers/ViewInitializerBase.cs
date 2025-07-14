using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    public abstract class ViewInitializerBase : MonoBehaviour
    {
        public bool IsInitialized { get; protected set; }
        
        public IViewModel ViewModel { get; protected set; }
    }
}