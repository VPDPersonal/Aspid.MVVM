using TMPro;
using UnityEngine;
using UltimateUI.MVVM;
using System.Globalization;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.Views.Generation;
using UltimateUI.MVVM.StarterKit.Binders;

namespace UltimateUI.Samples.StatsSample.Views
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

        protected override void InitializeIternal(IViewModel viewModel)
        {
            
        }
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