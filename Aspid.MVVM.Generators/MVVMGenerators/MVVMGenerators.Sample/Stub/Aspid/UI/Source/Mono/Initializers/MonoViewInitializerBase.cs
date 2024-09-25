using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono.Initializers
{
    public abstract class MonoViewInitializerBase : MonoBehaviour
    {
        protected abstract IView View { get; }
        
        protected abstract IViewModel ViewModel { get; }

        protected void Initialize() => View.Initialize(ViewModel);
    }
}