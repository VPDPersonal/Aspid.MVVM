using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public abstract partial class ScriptableViewMonoBinder<TView> : MonoBinder, IBinder<IViewModel>
        where TView : ScriptableObject, IView
    {
        [SerializeField] private TView _view;
        
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
            _view.Initialize(viewModel);
        
        protected void DeinitializeView() => 
            _view.Deinitialize();
    }
    
    [AddComponentMenu("Aspid/MVVM/Binders/Views/ScriptableView Binder")]
    public class ScriptableViewMonoBinder : ScriptableViewMonoBinder<ScriptableView> { }
}