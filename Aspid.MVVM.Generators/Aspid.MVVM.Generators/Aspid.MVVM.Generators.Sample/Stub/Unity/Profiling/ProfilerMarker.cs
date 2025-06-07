// ReSharper disable once CheckNamespace

using System;

namespace Unity.Profiling;

public class ProfilerMarker : IDisposable
{
    public ProfilerMarker(string name) { }

    public IDisposable Auto() => this;

    public void Dispose() { }
}