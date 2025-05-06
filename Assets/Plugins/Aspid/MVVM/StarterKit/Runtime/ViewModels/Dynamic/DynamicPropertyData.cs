using System;

namespace Aspid.MVVM.StarterKit
{
    public readonly ref struct DynamicPropertyData<T>
    {
        public readonly Id Id;
        public readonly T Value;
        public readonly BindMode Mode;

        public DynamicPropertyData(string id, T value, BindMode mode = BindMode.OneWay)
        {
            if (mode is BindMode.None or BindMode.OneWayToSource)
                throw new ArgumentException($"BindMode.None and BindMode.OneWayToSource are not supported. Mode = {mode}");
            
            Mode = mode;
            Value = value;
            Id = new Id(id);
        }
    }
}