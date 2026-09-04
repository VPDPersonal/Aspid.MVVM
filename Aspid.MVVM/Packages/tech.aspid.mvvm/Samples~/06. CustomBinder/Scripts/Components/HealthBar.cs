using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.CustomBinder
{
    // A project UI component that knows nothing about MVVM.
    public sealed class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Gradient _gradient = new();

        private float _value = 1f;

        public float Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp01(value);
                _fill.fillAmount = _value;
                _fill.color = _gradient.Evaluate(_value);
                _label.text = $"{Mathf.RoundToInt(_value * 100f)}%";
            }
        }
    }
}
