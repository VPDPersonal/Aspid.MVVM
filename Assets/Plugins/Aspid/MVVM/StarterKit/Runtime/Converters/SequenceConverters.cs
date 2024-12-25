#if UNITY_2023_1_OR_NEWER
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    public class SequenceConverters<T> : IConverter<T, T>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private IConverter<T, T>[] _converters;

        public T Convert(T value)
        {
            foreach (var converter in _converters)
                value = converter.Convert(value);

            return value;
        }
    }
}
#endif