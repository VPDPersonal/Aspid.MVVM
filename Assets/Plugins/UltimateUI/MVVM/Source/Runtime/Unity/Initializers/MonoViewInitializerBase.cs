using UnityEngine;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.ViewModels;

namespace UltimateUI.MVVM.Unity.Initializers
{
    public abstract class MonoViewInitializerBase : MonoBehaviour
    {
        protected abstract IView View { get; }
        
        protected abstract IViewModel ViewModel { get; }

        protected void Initialize() => View.Initialize(ViewModel);
    }
}