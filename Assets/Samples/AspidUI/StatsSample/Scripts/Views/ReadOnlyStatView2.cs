using TMPro;
using UnityEngine;
using AspidUI.MVVM;
using System.Globalization;
using AspidUI.MVVM.Unity.Views;
using AspidUI.MVVM.Views.Generation;
using AspidUI.MVVM.StarterKit.Binders;

namespace AspidUI.StatsSample.Views
{
    [View]
    public partial class ReadOnlyStatView2 : MonoView
    {
        [AsBinder(typeof(TextBinderProvider))]
        [SerializeField] private TextMeshProUGUI[] _cool;
        
        [AsBinder(typeof(TextBinderProvider))]
        [SerializeField] private TextMeshProUGUI[] _power;
        
        [AsBinder(typeof(TextBinderProvider))]
        [SerializeField] private TextMeshProUGUI[] _reflexes;
        
        [AsBinder(typeof(TextBinderProvider))]
        [SerializeField] private TextMeshProUGUI[] _intelligence;
        
        [AsBinder(typeof(TextBinderProvider))]
        [SerializeField] private TextMeshProUGUI[] _technicalAbility;
        
        [AsBinder(typeof(TextBinderProvider))]
        [SerializeField] private TextMeshProUGUI[] _skillPointsAvailable;
    }

    public class TextBinderProvider : Binder, IBinder<string>, INumberBinder
    {
        private readonly TextMeshProUGUI _text;

        public TextBinderProvider(TextMeshProUGUI text)
        {
            _text = text;
        }

        public void SetValue(string value) => _text.text = value;

        public void SetValue(int value) => _text.text = value.ToString();

        public void SetValue(long value) => _text.text = value.ToString();

        public void SetValue(float value) => _text.text = value.ToString(CultureInfo.InvariantCulture);

        public void SetValue(double value) => _text.text = value.ToString(CultureInfo.InvariantCulture);
    }
}