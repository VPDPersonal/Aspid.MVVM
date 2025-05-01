using System;

namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class StringEmptyToBoolConverter : IConverter<string?, bool>
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        private bool _isInvert;

        public StringEmptyToBoolConverter()
            : this(false) { }
        
        public StringEmptyToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        public bool Convert(string? value)
        {
            var to = string.IsNullOrEmpty(value);
            return _isInvert ? !to : to;
        }
    }
}