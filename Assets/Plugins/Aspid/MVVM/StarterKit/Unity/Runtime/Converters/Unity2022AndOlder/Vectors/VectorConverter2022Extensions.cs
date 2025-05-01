#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    public static class VectorConverter2022Extensions
    {
        public static IConverterVector2 ToConvert(this Func<Vector2, Vector2> converter) =>
            new ConverterVector2(converter);
        
        public static IConverterVector2 ToConvert2022(this IConverter<Vector2, Vector2> converter) =>
            new ConverterVector2(converter);
        
        public static IConverterVector2ToVector3 ToConvert(this Func<Vector2, Vector3> converter) =>
            new ConverterVector2ToVector3(converter);
        
        public static IConverterVector2ToVector3 ToConvert2022(this IConverter<Vector2, Vector3> converter) =>
            new ConverterVector2ToVector3(converter);
        
        public static IConverterVector3 ToConvert(this Func<Vector3, Vector3> converter) =>
            new ConverterVector3(converter);
        
        public static IConverterVector3 ToConvert2022(this IConverter<Vector3, Vector3> converter) =>
            new ConverterVector3(converter);
        
        public static IConverterVector3ToVector2 ToConvert(this Func<Vector3, Vector2> converter) =>
            new ConverterVector3ToVector2(converter);
        
        public static IConverterVector3ToVector2 ToConvert2022(this IConverter<Vector3, Vector2> converter) =>
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