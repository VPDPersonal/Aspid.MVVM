#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class Vector3CombineConverter
    {
        [SerializeField] private Mode _mode;

        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _preConvertor;
      
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _postConvertor;
        
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
            Func<Vector3, Vector3> preConvertor,
            Func<Vector3, Vector3> postConvertor)
            : this(mode, preConvertor.ToConvert(), postConvertor.ToConvert()) { }

        public Vector3CombineConverter(
            Mode mode,
            Converter? preConvertor, 
            Converter? postConvertor)
        {
            _mode = mode;
            _preConvertor = preConvertor;
            _postConvertor = postConvertor;
        }
        
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