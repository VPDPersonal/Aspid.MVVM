using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    public class SequenceConverters<T> : IConverter<T, T>
    {
        [SerializeReference] private IConverter<T, T>[] _converters;

        public T Convert(T value)
        {
            foreach (var converter in _converters)
                value = converter.Convert(value);

            return value;
        }
    }
}