using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(ScrollRect))]
    [AddComponentMenu("Binders/UI/Commands/ScrollRect Command Binder")]
    public sealed class ScrollRectCommandMonoBinder : MonoCommandBinder<Vector2>
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
    }
}