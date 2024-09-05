using System;
using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Converters
{
    public interface IConverter<in TFrom, out TTo>
    {
        public TTo Convert(TFrom value);

        public Func<TFrom, TTo> GetFunc() => Convert;
    }
    
    //
    // For Unity 2022 and Older
    //
    
    public interface IConverterIntToInt : IConverter<int, int> { }
    
    public interface IConverterLongToLong : IConverter<long, long> { }
    
    public interface IConverterStringToBool : IConverter<string, bool> { }
    
    public interface IConverterFloatToFloat : IConverter<float, float> { }
    
    public interface IConverterDoubleToDouble : IConverter<double, double> { }
    
    public interface IConverterStringToString : IConverter<string, string> { }
    
    public interface IConverterObjectToString : IConverter<object, string> { }
    
    public interface IConverterColorToColor : IConverter<Color, Color> { }
    
    public interface IConverterVector2ToVector2 : IConverter<Vector2, Vector2> { }
    
    public interface IConverterVector3ToVector3 : IConverter<Vector3, Vector3> { }
    
    public interface IConverterVectorToVector3 : IConverter<Vector2, Vector3> { }
    
    public interface IConverterQuaternionToQuaternion : IConverter<Quaternion, Quaternion> { }
}