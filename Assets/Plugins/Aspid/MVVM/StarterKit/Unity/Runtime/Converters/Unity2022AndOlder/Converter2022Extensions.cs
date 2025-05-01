#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    public static class Converter2022Extensions
    {
        public static IConverterMesh ToConvert(this Func<Mesh?, Mesh?> converter) =>
            new ConverterMesh(converter);
        
        public static IConverterMesh ToConvert2022(this IConverter<Mesh?, Mesh?> converter) =>
            new ConverterMesh(converter);
        
        public static IConverterMaterial ToConvert(this Func<Material?, Material?> converter) =>
            new ConverterMaterial(converter);
        
        public static IConverterMaterial ToConvert2022(this IConverter<Material?, Material?> converter) =>
            new ConverterMaterial(converter);
        
        public static IConverterQuaternion ToConvert(this Func<Quaternion, Quaternion> converter) =>
            new ConverterQuaternion(converter);
        
        public static IConverterQuaternion ToConvert2022(this IConverter<Quaternion, Quaternion> converter) =>
            new ConverterQuaternion(converter);
        
        public static IConverterPhysicsMaterial ToConvert(this Func<PhysicsMaterial?, PhysicsMaterial?> converter) =>
            new ConverterPhysicsMaterial(converter);
        
        public static IConverterPhysicsMaterial ToConvert2022(this IConverter<PhysicsMaterial?, PhysicsMaterial?> converter) =>
            new ConverterPhysicsMaterial(converter);
        
        private sealed class ConverterMesh : GenericFuncConverter<Mesh?, Mesh?>, IConverterMesh
        {
            public ConverterMesh(IConverter<Mesh?, Mesh?> converter) 
                : base(converter) { }

            public ConverterMesh(Func<Mesh?, Mesh?> converter)
                : base(converter) { }
        }
        
        private sealed class ConverterMaterial : GenericFuncConverter<Material?, Material?>, IConverterMaterial
        {
            public ConverterMaterial(IConverter<Material?, Material?> converter) 
                : base(converter) { }

            public ConverterMaterial(Func<Material?, Material?> converter) 
                : base(converter) { }
        }
        
        private sealed class ConverterQuaternion : GenericFuncConverter<Quaternion, Quaternion>, IConverterQuaternion
        {
            public ConverterQuaternion(IConverter<Quaternion, Quaternion> converter)
                : base(converter) { }

            public ConverterQuaternion(Func<Quaternion, Quaternion> converter)
                : base(converter) { }
        }
        
        private sealed class ConverterPhysicsMaterial : GenericFuncConverter<PhysicsMaterial?, PhysicsMaterial?>, IConverterPhysicsMaterial
        {
            public ConverterPhysicsMaterial(IConverter<PhysicsMaterial?, PhysicsMaterial?> converter) 
                : base(converter) { }

            public ConverterPhysicsMaterial(Func<PhysicsMaterial?, PhysicsMaterial?> converter) 
                : base(converter) { }
        }
    }
}