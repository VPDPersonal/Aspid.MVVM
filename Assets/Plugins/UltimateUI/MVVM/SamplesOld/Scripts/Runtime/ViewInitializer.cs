using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Views;
using UnityEngine;
using VContainer;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples
{
    public class ViewInitializer : MonoBehaviour
    {
        // [SerializeField] private View _view;
        //
        // private IViewModel _viewModel;
        // private IObjectResolver _objectResolver;
        //
        // // private ViewModelBase ViewModel => _viewModel ??= _objectResolver.Resolve(type);
        // //
        // // [VContainer.Inject]
        // // private void Constructor(IObjectResolver objectResolver)
        // // {
        // //     _IObjectResolver = objectResolver;
        // // }
        //
        // public void Awake()
        // {
        //     _viewModel.Bind(_view.GetBinders());
        // }
        //
        // public void OnDestroy()
        // {
        //     _viewModel.Unbind(_view.GetBinders());
        // }
    }
}