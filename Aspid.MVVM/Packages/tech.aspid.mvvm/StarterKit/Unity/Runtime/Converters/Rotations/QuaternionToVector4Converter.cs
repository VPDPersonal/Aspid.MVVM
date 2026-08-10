#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a rotation as its four raw numbers.
    /// </summary>
    /// <remarks>
    /// A shader property, a save record or a network packet takes a <see cref="Vector4"/> where the
    /// game holds a <see cref="Quaternion"/>. The two carry the same four numbers, so nothing is
    /// computed here — the point is that the picker offers the step at all.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Rotation", Name = "Quaternion To Vector4", Tooltip = "Reads a rotation as its four raw numbers")]
    public sealed class QuaternionToVector4Converter : IConverter<Quaternion, Vector4>
    {
        /// <summary>
        /// Reads the specified rotation as four numbers.
        /// </summary>
        /// <param name="value">The rotation to read.</param>
        /// <returns>The four numbers, in x, y, z, w order.</returns>
        public Vector4 Convert(Quaternion value) => new(value.x, value.y, value.z, value.w);
    }
}
