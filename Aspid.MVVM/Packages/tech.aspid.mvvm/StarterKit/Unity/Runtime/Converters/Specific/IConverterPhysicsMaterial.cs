#nullable enable
#if UNITY_6000_0_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface IConverterPhysicsMaterial : IConverter<PhysicsMaterial?, PhysicsMaterial?> { }
}