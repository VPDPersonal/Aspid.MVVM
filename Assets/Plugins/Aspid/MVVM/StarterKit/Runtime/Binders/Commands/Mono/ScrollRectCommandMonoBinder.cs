using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(ScrollRect))]
    [AddComponentMenu("MVVM/Binders/UI/Commands/ScrollRect Command Binder")]
    public sealed class ScrollRectCommandMonoBinder : MonoCommandBinder<Vector2>, IBinder<IRelayCommand<Vector3>>
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

        public void SetValue(IRelayCommand<Vector3> command) =>
            SetValue(new RelayCommand<Vector2>(
                execute: value => command.Execute(value), 
                canExecute: value => command.CanExecute(value)));
    }
}