using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Declares that the serializable half of this binder family is generated from the MonoBehaviour half it is
    /// applied to.
    /// </summary>
    /// <remarks>
    /// Emits <c>{Name}Binder</c> over the matching <c>Target*Binder</c>, carrying the body, the serialized options and the
    /// documentation across and synthesising the constructor from the options. A twin that already exists by name is skipped.
    /// The MonoBehaviour half stays hand-written: Unity needs a MonoScript asset, which only a file of its own provides.
    /// </remarks>
    /// <example>
    /// <code>
    /// [GenerateSerializableBinder]
    /// public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder&lt;Camera&gt; { … }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GenerateSerializableBinderAttribute : Attribute { }
}
