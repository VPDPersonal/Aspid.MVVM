using System;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class ArithmeticNumberConverter : Aspid.MVVM.StarterKit.ArithmeticNumberConverter, 
        IConverterDouble, IConverterIntToDouble, IConverterLongToDouble, IConverterFloatToDouble, 
        IConverterFloat, IConverterIntToFloat, IConverterLongToFloat, IConverterDoubleToFloat, 
        IConverterInt, IConverterLongToInt, IConverterFloatToInt, IConverterDoubleToInt, 
        IConverterLong, IConverterIntToLong, IConverterFloatToLong, IConverterDoubleToLong { }
}