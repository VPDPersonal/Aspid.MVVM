#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – Text")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_Text")]
    [BindModeOverride(IsAll = true)]
    public sealed partial class InputFieldMonoBinder : ComponentMonoBinder<TMP_InputField>, 
        INumberBinder, 
        IBinder<string>,
        INumberReverseBinder,
        IReverseBinder<string> 
    {
        public event Action<string> ValueChanged;
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;
        
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        private bool _isNotifyValueChanged = true;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying) return;
            
            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
            {
                CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
                CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
                CachedComponent.onSubmit.RemoveListener(OnValueChanged);
                CachedComponent.onSelect.RemoveListener(OnValueChanged);
                CachedComponent.onDeselect.RemoveListener(OnValueChanged);
                
                Subscribe();
            }
        }

        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            
            Subscribe();
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.text);
        }

        protected override void OnUnbound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            Unsubscribe();
        }

        [BinderLog]
        public void SetValue(string value)
        {
            _isNotifyValueChanged = false;
            CachedComponent.text = _converter?.Convert(value) ?? value;
            _isNotifyValueChanged = true;
        }

        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void OnValueChanged(string value)
        {
            if (!_isNotifyValueChanged) return;
            
            ValueChanged?.Invoke(value);

            if (CachedComponent.contentType 
                is not (TMP_InputField.ContentType.IntegerNumber 
                or TMP_InputField.ContentType.DecimalNumber)) return;
           
            if (IntValueChanged != null || LongValueChanged != null)
            {
                if (!long.TryParse(value, out var integerValue)) return;

                if (integerValue is <= int.MaxValue and >= int.MinValue)
                    IntValueChanged?.Invoke((int)integerValue);

                LongValueChanged?.Invoke(integerValue);
            }
            
            if (FloatValueChanged != null || DoubleValueChanged != null)
            {
                if (!double.TryParse(value, out var decimalValue)) return;
                
                if (decimalValue is <= float.MaxValue and >= float.MinValue)
                    FloatValueChanged?.Invoke((float)decimalValue);

                DoubleValueChanged?.Invoke(decimalValue);
            }
        }
    }
}
#endif