using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(ScrollRect))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/ScrollRect Command Binder")]
    public sealed partial class ScrollRectCommandMonoBinder : MonoCommandBinder<Vector2>, IBinder<IRelayCommand<Vector3>>
    {
        [Header("Component")]
        [SerializeField] private ScrollRect _scrollRect;
        
        private void Awake()
        {
            if (!_scrollRect)
                _scrollRect = GetComponent<ScrollRect>();
        }

        private void OnEnable() =>
            _scrollRect.onValueChanged.AddListener(InvokeCommand);

        private void OnDisable() =>
            _scrollRect.onValueChanged.RemoveListener(InvokeCommand);

        [BinderLog]
        public void SetValue(IRelayCommand<Vector3> command) =>
            SetValue(new RelayCommand<Vector2>(
                execute: value => command.Execute(value), 
                canExecute: value => command.CanExecute(value)));
    }
}