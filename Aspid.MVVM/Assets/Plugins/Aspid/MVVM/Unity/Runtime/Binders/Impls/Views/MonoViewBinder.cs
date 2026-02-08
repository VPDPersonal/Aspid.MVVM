using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class MonoViewBinder<TView> : TargetBinder<TView>, IBinder<IViewModel>
        where TView : Object, IView
    {
        public MonoViewBinder(TView target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        public void SetValue(IViewModel viewModel)
        {
            DeinitializeView();

            if (viewModel is not null)
                InitializeView(viewModel);
        }

        protected override void OnUnbound() =>
            DeinitializeView();
        
        protected void InitializeView(IViewModel viewModel) => 
            Target.Initialize(viewModel);
        
        protected void DeinitializeView() => 
            Target.Deinitialize();
    }

    public class MonoViewBinder : MonoViewBinder<MonoView>
    {
        public MonoViewBinder(MonoView target, BindMode mode = BindMode.OneWay) 
            : base(target, mode) { }
    }
}