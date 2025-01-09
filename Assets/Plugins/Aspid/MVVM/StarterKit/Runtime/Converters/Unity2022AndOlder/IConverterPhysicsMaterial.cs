#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Converters
{
    public interface IConverterPhysicsMaterial : IConverter<PhysicsMaterial, PhysicsMaterial> { }
}