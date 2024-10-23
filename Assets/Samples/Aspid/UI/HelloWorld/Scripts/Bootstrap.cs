using UnityEngine;

namespace Aspid.UI.HelloWorld
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] private View _view;

        private Model _model;
        private MomentViewModel _commandViewModel;
        
        private void Awake()
        {
            _model = new Model();
            _commandViewModel = new MomentViewModel(_model);
            _view.Initialize(_commandViewModel);
        }

        private void OnDestroy()
        {
            _view.Deinitialize();
            _commandViewModel.Dispose();
        }
    }
}