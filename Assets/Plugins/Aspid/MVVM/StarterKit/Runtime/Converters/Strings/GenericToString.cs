using System;

namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class GenericToString<TFrom> : IConverter<TFrom?, string?>
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private string? _format;
        
        public GenericToString() { }
        
        public GenericToString(string? format)
        {
            _format = format;
        }

        public string? Convert(TFrom? value)
        {
            if (value is null) return null;
            return string.IsNullOrEmpty(_format) ? ToStringValue(value) : string.Format(_format, value);
        }

        protected virtual string ToStringValue(TFrom value) => 
            value!.ToString();
    }
}