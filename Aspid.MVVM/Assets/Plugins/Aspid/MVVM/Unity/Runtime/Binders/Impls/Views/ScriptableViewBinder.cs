using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class ScriptableViewBinder<TView> : TargetBinder<TView>
        where TView : ScriptableObject, IView
    {
        public ScriptableViewBinder(TView target, BindMode mode = BindMode.OneWay)
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
    
    public class ScriptableViewBinder : ScriptableViewBinder<ScriptableView>
    {
        public ScriptableViewBinder(ScriptableView target, BindMode mode = BindMode.OneWay) 
            : base(target, mode) { }
    }
}