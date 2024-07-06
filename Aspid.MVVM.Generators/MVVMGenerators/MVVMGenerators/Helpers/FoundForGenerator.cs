namespace MVVMGenerators.Helpers;

public readonly struct FoundForGenerator<T>(bool isNeed, T container)
{
    public readonly bool IsNeed = isNeed;
    public readonly T Container = container;
}