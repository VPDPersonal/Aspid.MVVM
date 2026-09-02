using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Declares that the serializable half of this binder family is generated from the MonoBehaviour half it is
    /// applied to.
    /// </summary>
    /// <remarks>
    /// The MonoBehaviour half stays hand-written because Unity resolves a component through a MonoScript asset, which
    /// exists only for a type declared in a file of its own. Emits <c>{Name}Binder</c> over the matching
    /// <c>Target*Binder</c> base, carrying the body, the serialized options and the class documentation across, and
    /// synthesising the constructor from those options.
    /// <para/>
    /// Generation is skipped when the twin already exists by name, so a hand-written one stays untouched.
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
