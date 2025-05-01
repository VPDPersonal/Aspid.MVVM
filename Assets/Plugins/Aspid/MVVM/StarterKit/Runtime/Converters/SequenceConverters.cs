namespace Aspid.MVVM.StarterKit
{
    public class SequenceConverters<T> : IConverter<T, T>
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        #if UNITY_2022_1_OR_NEWER
        [SerializeReferenceDropdown]
        [UnityEngine.SerializeReference] 
        #endif
        private IConverter<T, T>[] _converters;

        public SequenceConverters(params IConverter<T, T>[] converters)
        {
            _converters = converters;
        }

        public T Convert(T value)
        {
            foreach (var converter in _converters)
                value = converter.Convert(value);

            return value;
        }
    }
}
