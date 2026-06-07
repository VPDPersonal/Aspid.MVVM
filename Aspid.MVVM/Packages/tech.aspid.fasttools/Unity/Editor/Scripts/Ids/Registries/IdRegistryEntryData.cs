// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal readonly struct IdRegistryEntryData
    {
        public readonly int Id;
        public readonly string Name;
        public readonly bool IsDuplicate;
        public readonly int OriginalIndex;

        public IdRegistryEntryData(int originalIndex, string name, int id, bool isDuplicate)
        {
            Id = id;
            Name = name;
            IsDuplicate = isDuplicate;
            OriginalIndex = originalIndex;
        }
    }
}
