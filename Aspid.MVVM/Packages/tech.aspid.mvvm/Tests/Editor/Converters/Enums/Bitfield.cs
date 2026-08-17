namespace Aspid.MVVM.StarterKit.Tests
{
    // A member above int.MaxValue that only an unsigned backing type can hold.
    internal enum Bitfield : uint
    {
        Empty = 0,
        Full = uint.MaxValue,
    }
}
