using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract partial class ViewMonoBinder<TView> : ComponentMonoBinder<TView>, IBinder<IViewModel>
        where TView : Component, IView
    {
        [BinderLog]
        public void SetValue(IViewModel viewModel)
        {
            DeinitializeView();

            if (viewModel is not null)
                InitializeView(viewModel);
        }

        protected override void OnUnbound() =>
            DeinitializeView();
        
        protected void InitializeView(IViewModel viewModel) => 
            CachedComponent.Initialize(viewModel);
        
        protected void DeinitializeView() => 
            CachedComponent.Deinitialize();
    }
}