#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class Vector2CombineConverter
    {
        [SerializeField] private Mode _mode;

        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _preConvertor;
      
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _postConvertor;
        
        public Vector2CombineConverter() :
            this(Mode.XY) { }
        
        public Vector2CombineConverter(Mode mode)
        {
            _mode = mode;
            _preConvertor = default;
            _postConvertor = default;
        }
        
        public Vector2CombineConverter(
            Mode mode,
            Func<Vector2, Vector2> preConvertor,
            Func<Vector2, Vector2> postConvertor)
            : this(mode, preConvertor.ToConvert(), postConvertor.ToConvert()) { }
        
        public Vector2CombineConverter(
            Mode mode,
            Converter? preConvertor, 
            Converter? postConvertor)
        {
            _mode = mode;
            _preConvertor = preConvertor;
            _postConvertor = postConvertor;
        }
        
        public Vector2 Convert(Vector2 from, Vector2 to)
        {
            from = _preConvertor?.Convert(from) ?? from;
            
            from = _mode switch
            {
                Mode.X => new Vector2(from.x, to.y),
                Mode.Y => new Vector2(to.x, from.y),
                Mode.XY => new Vector2(from.x, from.y),
                _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
            };
            
            return _postConvertor?.Convert(from) ?? from;
        }

        public static Vector2CombineConverter Default => new(Mode.XY);
        
        public enum Mode
        {
            X,
            Y,
            XY,
        }
    }
}