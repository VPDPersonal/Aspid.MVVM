namespace Aspid.Generator.Helpers;

public readonly struct FoundForGenerator<T>
{
    public readonly bool IsNeed;
    public readonly T Container;

    public FoundForGenerator(T container)
    {
        IsNeed = true;
        Container = container;
    }
}