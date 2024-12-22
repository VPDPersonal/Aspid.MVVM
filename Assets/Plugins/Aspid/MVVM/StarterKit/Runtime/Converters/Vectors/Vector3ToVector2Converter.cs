#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class Vector3ToVector2Converter : IConverterVector3ToVector2
    {
        [SerializeField] private Values _values = Values.XY;

        public Vector3ToVector2Converter() { }

        public Vector3ToVector2Converter(Values values)
        {
            _values = values;
        }

        public Vector2 Convert(Vector3 value) => _values switch
        {
            Values.XY => new Vector2(value.x, value.y),
            Values.XZ => new Vector2(value.x, value.z),
            Values.YX => new Vector2(value.y, value.x),
            Values.YZ => new Vector2(value.y, value.z),
            Values.ZX => new Vector2(value.z, value.x),
            Values.ZY => new Vector2(value.z, value.y),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        public enum Values
        {
            XY,
            XZ,
            YX,
            YZ,
            ZX,
            ZY,
        }
    }
}