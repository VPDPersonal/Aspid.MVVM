using System;

namespace Aspid.MVVM.StarterKit.ViewModels
{
    public static class DynamicPropertyFactory
    {
        public static IDynamicProperty Create<T>(DynamicPropertyData<T> dynamicProperty) =>
            Create(dynamicProperty.Value, dynamicProperty.Mode);
        
        public static IDynamicProperty Create<T>(T value, BindMode mode = BindMode.OneTime) => mode switch
        {
            BindMode.OneWay => new OneWayDynamicProperty<T>(value),
            BindMode.TwoWay => new TwoWayDynamicProperty<T>(value),
            BindMode.OneTime => new OneTimeDynamicProperty<T>(value),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}