namespace Aspid.MVVM.Shared;

internal static class BindablePropertyNames
{
    internal static string RemovePrefix(string name)
    {
        var count = name.StartsWith("_") ? 1 : name.StartsWith("m_") || name.StartsWith("s_") ? 2 : 0;
        return name.Substring(count);
    }

    internal static string FromField(string name)
    {
        var value = RemovePrefix(name);
        return value.Length == 0 ? value : char.ToUpperInvariant(value[0]) + value.Substring(1);
    }
}
