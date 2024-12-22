#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class Vector2CombineConverter
    {
        [SerializeField] private Mode _mode;

        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2>? _preConvertor;
#else
        private IConverterVector2? _preConvertor;
#endif
      
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2>? _postConvertor;
#else
        private IConverterVector2? _postConvertor;
#endif
        
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
            IConverterVector2? preConvertor, 
            IConverterVector2? postConvertor)
        {
            _mode = mode;
            _preConvertor = preConvertor;
            _postConvertor = postConvertor;
        }

#if UNITY_2023_1_OR_NEWER
        public Vector2CombineConverter(
            Mode mode,
            Func<Vector2, Vector2> preConvertor,
            Func<Vector2, Vector2> postConvertor)
            : this(mode, preConvertor.ToConvert(), postConvertor.ToConvert()) { }
        
        public Vector2CombineConverter(
            Mode mode, 
            IConverter<Vector2, Vector2>? preConvertor, 
            IConverter<Vector2, Vector2>? postConvertor)
        {
            _mode = mode;
            _preConvertor = preConvertor;
            _postConvertor = postConvertor;
        }
#endif
        
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