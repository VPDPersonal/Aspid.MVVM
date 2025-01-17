namespace Aspid.MVVM.StarterKit.ViewModels
{
    public readonly ref struct DynamicPropertyData<T>
    {
        public readonly T Value;
        public readonly string Id;

        public DynamicPropertyData(string id, T value)
        {
            Id = id;
            Value = value;
        }
    }
}