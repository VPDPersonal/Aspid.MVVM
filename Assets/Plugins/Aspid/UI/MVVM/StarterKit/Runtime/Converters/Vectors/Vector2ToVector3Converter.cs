using System;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Converters.Vectors
{
    [Serializable]
    public sealed class Vector2ToVector3Converter : IConverterVector2ToVector3
    {
        [SerializeField] private Values _values;
        [SerializeField] private float _thirdValue;

        public Vector2ToVector3Converter() { }

        public Vector2ToVector3Converter(Values values, float thirdValue)
        {
            _values = values;
            _thirdValue = thirdValue;
        }

        public Vector3 Convert(Vector2 value) => _values switch
        {
            Values.XY => new Vector3(value.x, value.y, _thirdValue),
            Values.XZ => new Vector3(value.x, _thirdValue, value.y),
            Values.YZ => new Vector3(_thirdValue, value.x, value.y),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        public enum Values
        {
            XY,
            XZ,
            YZ,
        }
    }
}