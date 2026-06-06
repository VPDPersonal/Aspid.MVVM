#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class VectorConverterSpecificExtensions
    {
        public static IConverterVector2 ToConvert(this Func<Vector2, Vector2> converter) =>
            new ConverterVector2(converter);
        
        public static IConverterVector2 ToConvertSpecific(this IConverter<Vector2, Vector2> converter) =>
            new ConverterVector2(converter);
        
        public static IConverterVector2ToVector3 ToConvert(this Func<Vector2, Vector3> converter) =>
            new ConverterVector2ToVector3(converter);
        
        public static IConverterVector2ToVector3 ToConvertSpecific(this IConverter<Vector2, Vector3> converter) =>
            new ConverterVector2ToVector3(converter);
        
        public static IConverterVector3 ToConvert(this Func<Vector3, Vector3> converter) =>
            new ConverterVector3(converter);
        
        public static IConverterVector3 ToConvertSpecific(this IConverter<Vector3, Vector3> converter) =>
            new ConverterVector3(converter);
        
        public static IConverterVector3ToVector2 ToConvert(this Func<Vector3, Vector2> converter) =>
            new ConverterVector3ToVector2(converter);
        
        public static IConverterVector3ToVector2 ToConvertSpecific(this IConverter<Vector3, Vector2> converter) =>
            new ConverterVector3ToVector2(converter);
        
        private sealed class ConverterVector2 : GenericFuncConverter<Vector2, Vector2>, IConverterVector2
        {
            public ConverterVector2(IConverter<Vector2, Vector2> converter) 
                : base(converter) { }

            public ConverterVector2(Func<Vector2, Vector2> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterVector2ToVector3 : GenericFuncConverter<Vector2, Vector3>, IConverterVector2ToVector3
        {
            public ConverterVector2ToVector3(IConverter<Vector2, Vector3> converter) 
                : base(converter) { }

            public ConverterVector2ToVector3(Func<Vector2, Vector3> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterVector3 : GenericFuncConverter<Vector3, Vector3>, IConverterVector3
        {
            public ConverterVector3(IConverter<Vector3, Vector3> converter) 
                : base(converter) { }

            public ConverterVector3(Func<Vector3, Vector3> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterVector3ToVector2 : GenericFuncConverter<Vector3, Vector2>, IConverterVector3ToVector2
        {
            public ConverterVector3ToVector2(IConverter<Vector3, Vector2> converter) 
                : base(converter) { }

            public ConverterVector3ToVector2(Func<Vector3, Vector2> converter) 
                : base(converter) { }
        }
    }
}