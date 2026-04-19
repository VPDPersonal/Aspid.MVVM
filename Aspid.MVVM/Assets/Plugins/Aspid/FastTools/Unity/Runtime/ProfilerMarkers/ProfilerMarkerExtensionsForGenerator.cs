using Unity.Profiling;

// ReSharper disable once CheckNamespace
// The namespace is intentionally omitted, as this block serves only as a marker for the Source Generator.
public static class ProfilerMarkerExtensionsForGenerator
{
    /// <summary>
    /// Marker for the source generator. At runtime this method is never called —
    /// the generator replaces every call site with a unique <see cref="ProfilerMarker"/> scoped to the enclosing type, method, and line number.
    /// </summary>
    public static ProfilerMarker.AutoScope Marker(this object _) => default;

    /// <summary>
    /// Marker for the source generator. Allows specifying a custom display name for the generated <see cref="ProfilerMarker"/>.
    /// At runtime this method is never called — the generator uses the supplied name when creating the marker.
    /// </summary>
    public static ProfilerMarker.AutoScope WithName(this in ProfilerMarker.AutoScope marker, string _) => marker;
}
