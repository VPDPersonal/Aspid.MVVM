using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(ScrollRect))]
    [AddPropertyContextMenu(typeof(ScrollRect), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/ScrollRect Command Binder")]
    [AddComponentContextMenu(typeof(ScrollRect),"Add ScrollRect Binder/ScrollRect Command Binder")]
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