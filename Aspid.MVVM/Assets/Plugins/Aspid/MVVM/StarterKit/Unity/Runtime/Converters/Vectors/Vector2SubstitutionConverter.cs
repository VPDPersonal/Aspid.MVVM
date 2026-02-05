#nullable enable
using System;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class Vector2SubstitutionConverter : IConverterVector2
    {
        [SerializeField] private Mode _mode;

        public Vector2SubstitutionConverter()
            : this(Mode.XY) { }
        
        public Vector2SubstitutionConverter(Mode mode)
        {
            _mode = mode;
        }

        public Vector2 Convert(Vector2 value) => _mode switch
        {
            Mode.XY => new Vector2(value.x, value.y),
            Mode.YX => new Vector2(value.y, value.x),
            
            Mode.YY => new Vector2(value.y, value.y),
            Mode.XX => new Vector2(value.x, value.x),
            _ => throw new ArgumentOutOfRangeException()
        };

        public enum Mode
        {
            XY,
            YX,
            
            YY,
            XX,
        }
    }
}