using UnityEngine;
using AspidUI.MVVM.Views;
using AspidUI.MVVM.ViewModels;

namespace AspidUI.MVVM.Unity.Initializers
{
    public abstract class MonoViewInitializerBase : MonoBehaviour
    {
        protected abstract IView View { get; }
        
        protected abstract IViewModel ViewModel { get; }

        protected void Initialize() => View.Initialize(ViewModel);
    }
}