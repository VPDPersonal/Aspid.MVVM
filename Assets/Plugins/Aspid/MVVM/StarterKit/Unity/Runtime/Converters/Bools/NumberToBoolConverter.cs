using System;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class NumberToBoolConverter : Aspid.MVVM.StarterKit.NumberToBoolConverter,       
        IConverterFloatToBool, 
        IConverterDoubleToBool,
        IConverterIntToBool, 
        IConverterLongToBool { }
}