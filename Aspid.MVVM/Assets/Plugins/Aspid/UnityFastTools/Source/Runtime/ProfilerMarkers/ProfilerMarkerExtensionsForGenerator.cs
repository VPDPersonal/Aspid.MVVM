using Unity.Profiling;

// The namespace is intentionally omitted, as this block serves only as a marker for the Source Generator.
public static class ProfilerMarkerExtensionsForGenerator
{
    public static ProfilerMarker.AutoScope Marker(this object _) => default;
        
    public static ProfilerMarker.AutoScope WithName(this in ProfilerMarker.AutoScope marker, string _) => marker;
}