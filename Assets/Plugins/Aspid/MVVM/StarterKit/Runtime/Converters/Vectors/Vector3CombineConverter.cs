#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class Vector3CombineConverter
    {
        [SerializeField] private Mode _mode;

        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector3, Vector3>? _preConvertor;
#else
        private IConverterVector3? _preConvertor;
#endif
      
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector3, Vector3>? _postConvertor;
#else
        private IConverterVector3? _postConvertor;
#endif
        
        public Vector3CombineConverter() 
            : this(Mode.XYZ) { }
        
        public Vector3CombineConverter(Mode mode)
        {
            _mode = mode;
            _preConvertor = default;
            _postConvertor = default;
        }

        public Vector3CombineConverter(
            Mode mode,
            IConverterVector3? preConvertor, 
            IConverterVector3? postConvertor)
        {
            _mode = mode;
            _preConvertor = preConvertor;
            _postConvertor = postConvertor;
        }
        
#if UNITY_2023_1_OR_NEWER
        public Vector3CombineConverter(
            Mode mode,
            Func<Vector3, Vector3> preConvertor,
            Func<Vector3, Vector3> postConvertor)
            : this(mode, preConvertor.ToConvert(), postConvertor.ToConvert()) { }
        
        public Vector3CombineConverter(
            Mode mode, 
            IConverter<Vector3, Vector3>? preConvertor, 
            IConverter<Vector3, Vector3>? postConvertor)
        {
            _mode = mode;
            _preConvertor = preConvertor;
            _postConvertor = postConvertor;
        }
#endif
        
        public Vector3 Convert(Vector3 from, Vector2 to) =>
            Convert(from, (Vector3)to);
        
        public Vector3 Convert(Vector2 from, Vector3 to) =>
            Convert((Vector3)from, to);
        
        public Vector3 Convert(Vector2 from, Vector2 to) =>
            Convert((Vector3)from, (Vector3)to);
        
        public Vector3 Convert(Vector3 from, Vector3 to)
        {
            from = _preConvertor?.Convert(from) ?? from;
            
            from = _mode switch
            {
                Mode.X => new Vector3(from.x, to.y, to.z),
                Mode.Y => new Vector3(to.x, from.y, to.z),
                Mode.Z => new Vector3(to.x, to.y, from.z),
                Mode.XY => new Vector3(from.x, from.y, to.z),
                Mode.XZ => new Vector3(from.x, to.y, from.z),
                Mode.YZ => new Vector3(to.x, from.y, from.z),
                Mode.XYZ => new Vector3(from.x, from.y, from.z),
                _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
            };
            
            return _postConvertor?.Convert(from) ?? from;
        }

        public static Vector3CombineConverter Default => new(Mode.XYZ);
        
        public enum Mode
        {
            X,
            Y,
            Z,
            XY,
            XZ,
            YZ,
            XYZ,
        }
    }
}