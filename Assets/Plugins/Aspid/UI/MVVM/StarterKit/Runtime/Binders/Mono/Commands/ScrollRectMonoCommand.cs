using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands
{
    [RequireComponent(typeof(ScrollRect))]
    public sealed class ScrollRectMonoCommand : MonoCommandAdapter<Vector2>
    {
        [SerializeField] private ScrollRect _scrollRect;
        
        private void Awake()
        {
            if (!_scrollRect)
                _scrollRect = GetComponent<ScrollRect>();
        }

        private void OnEnable() => _scrollRect.onValueChanged.AddListener(InvokeCommand);

        private void OnDisable() => _scrollRect.onValueChanged.RemoveListener(InvokeCommand);
    }
}