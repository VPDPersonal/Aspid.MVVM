using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.VirtualizedList
{
    [RequireComponent(typeof(Graphic))]
    public sealed class Highlighted : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Color _highlightColor;
        [SerializeField] [Min(0)] private float _fadeDuration = 0.1f;

        private Graphic _graphic;

        private void Awake() =>
            _graphic = GetComponent<Graphic>();

        private void OnEnable() =>
            CrossFadeColor(Color.white, 0);

        public void OnPointerEnter(PointerEventData eventData) =>
            CrossFadeColor(_highlightColor, _fadeDuration);

        public void OnPointerExit(PointerEventData eventData) =>
            CrossFadeColor(Color.white, _fadeDuration);
    
        private void CrossFadeColor(Color color, float duration) =>
            _graphic.CrossFadeColor(color, duration, true, true);
    }
}