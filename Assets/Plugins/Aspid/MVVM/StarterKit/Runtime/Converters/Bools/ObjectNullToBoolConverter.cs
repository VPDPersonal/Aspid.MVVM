using System;

namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class ObjectNullToBoolConverter : IConverter<object, bool>
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField] 
#endif
        private bool _isInvert;

        public ObjectNullToBoolConverter()
            : this(false) { }
        
        public ObjectNullToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        public bool Convert(object? value)
        {
            var to = value is null;
            return _isInvert ? !to : to;
        }
    }
}