#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Forces IL2CPP to generate the generic converter instantiations a scene can ask for.
    /// </summary>
    /// <remarks>
    /// A <c>[SerializeReference]</c> converter closed over a value type — <c>SequenceConverters&lt;float&gt;</c>
    /// — exists in a build only as a string in YAML. Nothing calls it, so ahead-of-time compilation
    /// has no reason to emit its code, and the scene fails to load on a device while working in the
    /// editor. Naming the instantiation somewhere reachable is what makes it exist.
    /// <para>
    /// Only value types are listed. Instantiations over reference types share one compiled body, so
    /// they need no hint. Nothing calls <see cref="Seed"/> — being present and preserved is the whole
    /// job.
    /// </para>
    /// <para>
    /// A <c>link.xml</c> would be the other way to do this, but the converters share a namespace with
    /// every binder in the package, so preserving them by pattern would preserve those too and grow
    /// every build that uses the StarterKit.
    /// </para>
    /// </remarks>
    [Preserve]
    internal static class ConverterAotHints
    {
        /// <summary>
        /// The value types the hints cover. A converter closed over anything else is not seeded.
        /// </summary>
        internal static readonly Type[] SeededTypes =
        {
            typeof(bool),
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(TimeSpan),
            typeof(Color),
            typeof(Color32),
            typeof(Vector2),
            typeof(Vector3),
            typeof(Vector4),
            typeof(Vector2Int),
            typeof(Vector3Int),
            typeof(Quaternion),
            typeof(Rect),
            typeof(Bounds),
            typeof(ColorBlock),
        };

        [Preserve]
        private static void Seed()
        {
            SeedFor<bool>();
            SeedFor<int>();
            SeedFor<long>();
            SeedFor<float>();
            SeedFor<double>();
            SeedFor<TimeSpan>();
            SeedFor<Color>();
            SeedFor<Color32>();
            SeedFor<Vector2>();
            SeedFor<Vector3>();
            SeedFor<Vector4>();
            SeedFor<Vector2Int>();
            SeedFor<Vector3Int>();
            SeedFor<Quaternion>();
            SeedFor<Rect>();
            SeedFor<Bounds>();
            SeedFor<ColorBlock>();
        }

        [Preserve]
        private static void SeedFor<T>()
        {
            _ = new SequenceConverters<T>();
            _ = new PassthroughConverter<T>();
            _ = new ConditionalConverter<T>();
            _ = new GenericToString<T>();
            _ = new CachedConverter<T, T>();
            _ = new SafeConverter<T, T>();
            _ = new NullGuardConverter<T, T>();
            _ = new ComposeConverter<T, T, T>();
            _ = new ConverterAssetReference<T, T>();
        }
    }
}
