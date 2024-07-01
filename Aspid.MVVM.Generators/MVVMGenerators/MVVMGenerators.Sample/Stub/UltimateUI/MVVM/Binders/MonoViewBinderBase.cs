using UnityEngine;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewBinders
{
    public abstract class MonoViewBinderBase : MonoBehaviour
    {
        protected abstract IView View { get; }
        
        protected abstract IViewModel ViewModel { get; }

        protected void Bind() => ViewBinder.Bind(View, ViewModel);
        
        protected void Unbind() => ViewBinder.Unbind(View, ViewModel);
    }
}