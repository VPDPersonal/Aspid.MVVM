#nullable enable
using System;
using Aspid.FastTools.Types;
using UnityEngine;
#if UNITY_6000_0_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a function or a generic converter as one of the named Unity asset converter
    /// interfaces.
    /// </summary>
    /// <remarks>
    /// <c>ToConvert</c> takes a function, <c>ToConvertSpecific</c> takes a converter that is
    /// already the right shape but not the named type. Both exist because a binder before
    /// Unity 2023.1 declares its field as the named interface rather than the generic one,
    /// so a lambda cannot be assigned to it directly.
    /// </remarks>
    public static class ConverterSpecificExtensions
    {
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterMesh"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterMesh ToConvert(this Func<Mesh?, Mesh?> converter) =>
            new ConverterMesh(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterMesh"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterMesh ToConvertSpecific(this IConverter<Mesh?, Mesh?> converter) =>
            new ConverterMesh(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterMaterial"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterMaterial ToConvert(this Func<Material?, Material?> converter) =>
            new ConverterMaterial(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterMaterial"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterMaterial ToConvertSpecific(this IConverter<Material?, Material?> converter) =>
            new ConverterMaterial(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterQuaternion"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterQuaternion ToConvert(this Func<Quaternion, Quaternion> converter) =>
            new ConverterQuaternion(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterQuaternion"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterQuaternion ToConvertSpecific(this IConverter<Quaternion, Quaternion> converter) =>
            new ConverterQuaternion(converter);
        
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverterPhysicsMaterial"/>.
        /// </summary>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterPhysicsMaterial ToConvert(this Func<PhysicsMaterial?, PhysicsMaterial?> converter) =>
            new ConverterPhysicsMaterial(converter);
        
        /// <summary>
        /// Wraps the specified converter as an <see cref="IConverterPhysicsMaterial"/>.
        /// </summary>
        /// <param name="converter">The converter to wrap.</param>
        /// <returns>A converter of the named interface type.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverterPhysicsMaterial ToConvertSpecific(this IConverter<PhysicsMaterial?, PhysicsMaterial?> converter) =>
            new ConverterPhysicsMaterial(converter);
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterMesh : GenericFuncConverter<Mesh?, Mesh?>, IConverterMesh
        {
            public ConverterMesh(IConverter<Mesh?, Mesh?> converter) 
                : base(converter) { }

            public ConverterMesh(Func<Mesh?, Mesh?> converter)
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterMaterial : GenericFuncConverter<Material?, Material?>, IConverterMaterial
        {
            public ConverterMaterial(IConverter<Material?, Material?> converter) 
                : base(converter) { }

            public ConverterMaterial(Func<Material?, Material?> converter) 
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterQuaternion : GenericFuncConverter<Quaternion, Quaternion>, IConverterQuaternion
        {
            public ConverterQuaternion(IConverter<Quaternion, Quaternion> converter)
                : base(converter) { }

            public ConverterQuaternion(Func<Quaternion, Quaternion> converter)
                : base(converter) { }
        }
        
        [TypeSelectorDisplay(Hidden = true)]
        private sealed class ConverterPhysicsMaterial : GenericFuncConverter<PhysicsMaterial?, PhysicsMaterial?>, IConverterPhysicsMaterial
        {
            public ConverterPhysicsMaterial(IConverter<PhysicsMaterial?, PhysicsMaterial?> converter) 
                : base(converter) { }

            public ConverterPhysicsMaterial(Func<PhysicsMaterial?, PhysicsMaterial?> converter) 
                : base(converter) { }
        }
    }
}