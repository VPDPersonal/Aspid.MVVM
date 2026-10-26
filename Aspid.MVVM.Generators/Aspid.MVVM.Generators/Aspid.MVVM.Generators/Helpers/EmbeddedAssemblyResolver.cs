using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM.Generators.Helpers;

/// <summary>
/// Loads the helper assemblies embedded into this DLL as resources.
/// The generator ships as a single file, so its dependencies can't be found next to it.
/// </summary>
internal static class EmbeddedAssemblyResolver
{
    private static readonly object Lock = new();
    private static readonly Dictionary<string, Assembly> Loaded = new(StringComparer.Ordinal);
    private static bool _isInitialized;

    private static readonly Assembly Self = typeof(EmbeddedAssemblyResolver).Assembly;

    private static readonly HashSet<string> EmbeddedNames = new(StringComparer.Ordinal)
    {
        "Aspid.Generators.Helper",
        "Aspid.Generators.Helper.Unity",
    };

    [ModuleInitializer]
    internal static void Initialize()
    {
        lock (Lock)
        {
            if (_isInitialized) return;
            _isInitialized = true;
        }

        AppDomain.CurrentDomain.AssemblyResolve += Resolve;
    }

    private static Assembly? Resolve(object? sender, ResolveEventArgs args)
    {
        var name = new AssemblyName(args.Name).Name;
        if (name is null || !EmbeddedNames.Contains(name)) return null;
        if (!IsOurRequest(args.RequestingAssembly)) return null;

        lock (Lock)
        {
            if (Loaded.TryGetValue(name, out var cached)) return cached;

            using var stream = Self.GetManifestResourceStream(name + ".dll");
            if (stream is null) return null;

            var bytes = new byte[stream.Length];
            var read = 0;
            while (read < bytes.Length)
            {
                var chunk = stream.Read(bytes, read, bytes.Length - read);
                if (chunk <= 0) break;
                read += chunk;
            }

#pragma warning disable RS1035 // Assembly.Load is the only way to load an embedded dependency.
            var assembly = Assembly.Load(bytes);
#pragma warning restore RS1035
            Loaded[name] = assembly;
            return assembly;
        }
    }

    // Several copies of this generator can live in one compiler server (shadow copies after a
    // rebuild). Answer only for our own copy so another copy's resolver doesn't hand out its helpers.
    private static bool IsOurRequest(Assembly? requesting)
    {
        if (requesting is null) return true;
        if (requesting == Self) return true;

        lock (Lock)
        {
            foreach (var loaded in Loaded.Values)
                if (requesting == loaded) return true;
        }

        return false;
    }
}
