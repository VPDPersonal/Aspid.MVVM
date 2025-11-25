using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    public static partial class SerializePropertyExtensions
    {
        #region Int
        public static T SetValue<T>(this T property, int value)
            where T : SerializedProperty
        {
            return property.SetInt(value);
        }
        
        public static T SetValueAndApply<T>(this T property, int value)
            where T : SerializedProperty
        {
            return property.SetIntAndApply(value);
        }

        public static T SetInt<T>(this T property, int value)
            where T : SerializedProperty
        {
            property.intValue = value;
            return property;
        }
        
        public static T SetIntAndApply<T>(this T property, int value)
            where T : SerializedProperty
        {
            return property.SetInt(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Uint
        public static T SetValue<T>(this T property, uint value)
            where T : SerializedProperty
        {
            return property.SetUint(value);
        }
        
        public static T SetValueAndApply<T>(this T property, uint value)
            where T : SerializedProperty
        {
            return property.SetUintAndApply(value);
        }

        public static T SetUint<T>(this T property, uint value)
            where T : SerializedProperty
        {
            property.uintValue = value;
            return property;
        }
        
        public static T SetUintAndApply<T>(this T property, uint value)
            where T : SerializedProperty
        {
            return property.SetUint(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Long
        public static T SetValue<T>(this T property, long value)
            where T : SerializedProperty
        {
            return property.SetLong(value);
        }
        
        public static T SetValueAndApply<T>(this T property, long value)
            where T : SerializedProperty
        {
            return property.SetLongAndApply(value);
        }

        public static T SetLong<T>(this T property, long value)
            where T : SerializedProperty
        {
            property.longValue = value;
            return property;
        }
        
        public static T SetLongAndApply<T>(this T property, long value)
            where T : SerializedProperty
        {
            return property.SetLong(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Ulong
        public static T SetValue<T>(this T property, ulong value)
            where T : SerializedProperty
        {
            return property.SetUlong(value);
        }
        
        public static T SetValueAndApply<T>(this T property, ulong value)
            where T : SerializedProperty
        {
            return property.SetUlongAndApply(value);
        }

        public static T SetUlong<T>(this T property, ulong value)
            where T : SerializedProperty
        {
            property.ulongValue = value;
            return property;
        }
        
        public static T SetUlongAndApply<T>(this T property, ulong value)
            where T : SerializedProperty
        {
            return property.SetUlong(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Float
        public static T SetValue<T>(this T property, float value)
            where T : SerializedProperty
        {
            return property.SetFloat(value);
        }
        
        public static T SetValueAndApply<T>(this T property, float value)
            where T : SerializedProperty
        {
            return property.SetFloatAndApply(value);
        }

        public static T SetFloat<T>(this T property, float value)
            where T : SerializedProperty
        {
            property.floatValue = value;
            return property;
        }
        
        public static T SetFloatAndApply<T>(this T property, float value)
            where T : SerializedProperty
        {
            return property.SetFloat(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Double
        public static T SetValue<T>(this T property, double value)
            where T : SerializedProperty
        {
            return property.SetDouble(value);
        }
        
        public static T SetValueAndApply<T>(this T property, double value)
            where T : SerializedProperty
        {
            return property.SetDoubleAndApply(value);
        }

        public static T SetDouble<T>(this T property, double value)
            where T : SerializedProperty
        {
            property.doubleValue = value;
            return property;
        }
        
        public static T SetDoubleAndApply<T>(this T property, double value)
            where T : SerializedProperty
        {
            return property.SetDouble(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region EnumIndex
        public static T SetEnumFlag<T>(this T property, int value)
            where T : SerializedProperty
        {
            property.enumValueFlag = value;
            return property;
        }
        
        public static T SetEnumFlagAndApply<T>(this T property, int value)
            where T : SerializedProperty
        {
            return property.SetEnumFlag(value).ApplyModifiedProperties();
        }

        public static T SetEnumIndex<T>(this T property, int value)
            where T : SerializedProperty
        {
            property.enumValueIndex = value;
            return property;
        }
        
        public static T SetEnumIndexAndApply<T>(this T property, int value)
            where T : SerializedProperty
        {
            return property.SetEnumIndex(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Bool
        public static T SetValue<T>(this T property, bool value)
            where T : SerializedProperty
        {
            return property.SetBool(value);
        }
        
        public static T SetValueAndApply<T>(this T property, bool value)
            where T : SerializedProperty
        {
            return property.SetBoolAndApply(value);
        }

        public static T SetBool<T>(this T property, bool value)
            where T : SerializedProperty
        {
            property.boolValue = value;
            return property;
        }
        
        public static T SetBoolAndApply<T>(this T property, bool value)
            where T : SerializedProperty
        {
            return property.SetBool(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Rect
        public static T SetValue<T>(this T property, Rect value)
            where T : SerializedProperty
        {
            return property.SetRect(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Rect value)
            where T : SerializedProperty
        {
            return property.SetRectAndApply(value);
        }

        public static T SetRect<T>(this T property, Rect value)
            where T : SerializedProperty
        {
            property.rectValue = value;
            return property;
        }
        
        public static T SetRectAndApply<T>(this T property, Rect value)
            where T : SerializedProperty
        {
            return property.SetRect(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region RectInt
        public static T SetValue<T>(this T property, RectInt value)
            where T : SerializedProperty
        {
            return property.SetRectInt(value);
        }
        
        public static T SetValueAndApply<T>(this T property, RectInt value)
            where T : SerializedProperty
        {
            return property.SetRectIntAndApply(value);
        }

        public static T SetRectInt<T>(this T property, RectInt value)
            where T : SerializedProperty
        {
            property.rectIntValue = value;
            return property;
        }
        
        public static T SetRectIntAndApply<T>(this T property, RectInt value)
            where T : SerializedProperty
        {
            return property.SetRectInt(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Bounds
        public static T SetValue<T>(this T property, Bounds value)
            where T : SerializedProperty
        {
            return property.SetBounds(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Bounds value)
            where T : SerializedProperty
        {
            return property.SetBoundsAndApply(value);
        }
        
        public static T SetBounds<T>(this T property, Bounds value)
            where T : SerializedProperty
        {
            property.boundsValue = value;
            return property;
        }
        
        public static T SetBoundsAndApply<T>(this T property, Bounds value)
            where T : SerializedProperty
        {
            return property.SetBounds(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region BoundsInt
        public static T SetValue<T>(this T property, BoundsInt value)
            where T : SerializedProperty
        {
            return property.SetBoundsInt(value);
        }
        
        public static T SetValueAndApply<T>(this T property, BoundsInt value)
            where T : SerializedProperty
        {
            return property.SetBoundsIntAndApply(value);
        }

        public static T SetBoundsInt<T>(this T property, BoundsInt value)
            where T : SerializedProperty
        {
            property.boundsIntValue = value;
            return property;
        }
        
        public static T SetBoundsIntAndApply<T>(this T property, BoundsInt value)
            where T : SerializedProperty
        {
            return property.SetBoundsInt(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Color
        public static T SetValue<T>(this T property, Color value)
            where T : SerializedProperty
        {
            return property.SetColor(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Color value)
            where T : SerializedProperty
        {
            return property.SetColorAndApply(value);
        }

        public static T SetColor<T>(this T property, Color value)
            where T : SerializedProperty
        {
            property.colorValue = value;
            return property;
        }
        
        public static T SetColorAndApply<T>(this T property, Color value)
            where T : SerializedProperty
        {
            return property.SetColor(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Gradient
        public static T SetValue<T>(this T property, Gradient value)
            where T : SerializedProperty
        {
            return property.SetGradient(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Gradient value)
            where T : SerializedProperty
        {
            return property.SetGradientAndApply(value);
        }

        public static T SetGradient<T>(this T property, Gradient value)
            where T : SerializedProperty
        {
            property.gradientValue = value;
            return property;
        }
        
        public static T SetGradientAndApply<T>(this T property, Gradient value)
            where T : SerializedProperty
        {
            return property.SetGradient(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Hash128
        public static T SetValue<T>(this T property, Hash128 value)
            where T : SerializedProperty
        {
            return property.SetHash128(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Hash128 value)
            where T : SerializedProperty
        {
            return property.SetHash128AndApply(value);
        }

        public static T SetHash128<T>(this T property, Hash128 value)
            where T : SerializedProperty
        {
            property.hash128Value = value;
            return property;
        }
        
        public static T SetHash128AndApply<T>(this T property, Hash128 value)
            where T : SerializedProperty
        {
            return property.SetHash128(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Vector4
        public static T SetValue<T>(this T property, Vector4 value)
            where T : SerializedProperty
        {
            return property.SetVector4(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Vector4 value)
            where T : SerializedProperty
        {
            return property.SetVector4AndApply(value);
        }

        public static T SetVector4<T>(this T property, Vector4 value)
            where T : SerializedProperty
        {
            property.vector4Value = value;
            return property;
        }
        
        public static T SetVector4AndApply<T>(this T property, Vector4 value)
            where T : SerializedProperty
        {
            return property.SetVector4(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Vector3
        public static T SetValue<T>(this T property, Vector3 value)
            where T : SerializedProperty
        {
            return property.SetVector3(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Vector3 value)
            where T : SerializedProperty
        {
            return property.SetVector3AndApply(value);
        }

        public static T SetVector3<T>(this T property, Vector3 value)
            where T : SerializedProperty
        {
            property.vector3Value = value;
            return property;
        }
        
        public static T SetVector3AndApply<T>(this T property, Vector3 value)
            where T : SerializedProperty
        {
            return property.SetVector3(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Vector3Int
        public static T SetValue<T>(this T property, Vector3Int value)
            where T : SerializedProperty
        {
            return property.SetVector3Int(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Vector3Int value)
            where T : SerializedProperty
        {
            return property.SetVector3IntAndApply(value);
        }

        public static T SetVector3Int<T>(this T property, Vector3Int value)
            where T : SerializedProperty
        {
            property.vector3Value = value;
            return property;
        }
        
        public static T SetVector3IntAndApply<T>(this T property, Vector3Int value)
            where T : SerializedProperty
        {
            return property.SetVector3Int(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Vector2
        public static T SetValue<T>(this T property, Vector2 value)
            where T : SerializedProperty
        {
            return property.SetVector2(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Vector2 value)
            where T : SerializedProperty
        {
            return property.SetVector2AndApply(value);
        }

        public static T SetVector2<T>(this T property, Vector2 value)
            where T : SerializedProperty
        {
            property.vector2Value = value;
            return property;
        }
        
        public static T SetVector2AndApply<T>(this T property, Vector2 value)
            where T : SerializedProperty
        {
            return property.SetVector2(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Vector2Int
        public static T SetValue<T>(this T property, Vector2Int value)
            where T : SerializedProperty
        {
            return property.SetVector2Int(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Vector2Int value)
            where T : SerializedProperty
        {
            return property.SetVector2IntAndApply(value);
        }

        public static T SetVector2Int<T>(this T property, Vector2Int value)
            where T : SerializedProperty
        {
            property.vector2Value = value;
            return property;
        }
        
        public static T SetVector2IntAndApply<T>(this T property, Vector2Int value)
            where T : SerializedProperty
        {
            return property.SetVector2Int(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Quaternion
        public static T SetValue<T>(this T property, Quaternion value)
            where T : SerializedProperty
        {
            return property.SetQuaternion(value);
        }
        
        public static T SetValueAndApply<T>(this T property, Quaternion value)
            where T : SerializedProperty
        {
            return property.SetQuaternionAndApply(value);
        }

        public static T SetQuaternion<T>(this T property, Quaternion value)
            where T : SerializedProperty
        {
            property.quaternionValue = value;
            return property;
        }
        
        public static T SetQuaternionAndApply<T>(this T property, Quaternion value)
            where T : SerializedProperty
        {
            return property.SetQuaternion(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region String
        public static T SetValue<T>(this T property, string value)
            where T : SerializedProperty
        {
            return property.SetString(value);
        }
        
        public static T SetValueAndApply<T>(this T property, string value)
            where T : SerializedProperty
        {
            return property.SetStringAndApply(value);
        }
        
        public static T SetString<T>(this T property, string value)
            where T : SerializedProperty
        {
            property.stringValue = value;
            return property;
        }
        
        public static T SetStringAndApply<T>(this T property, string value)
            where T : SerializedProperty
        {
            return property.SetString(value).ApplyModifiedProperties();
        }
        #endregion

        #region AnimationCurve
        public static T SetValue<T>(this T property, AnimationCurve value)
            where T : SerializedProperty
        {
            return property.SetAnimationCurve(value);
        }

        public static T SetValueAndApply<T>(this T property, AnimationCurve value)
            where T : SerializedProperty
        {
            return property.SetAnimationCurveAndApply(value);
        }
        
        public static T SetAnimationCurve<T>(this T property, AnimationCurve value)
            where T : SerializedProperty
        {
            property.animationCurveValue = value;
            return property;
        }
        
        public static T SetAnimationCurveAndApply<T>(this T property, AnimationCurve value)
            where T : SerializedProperty
        {
            return property.SetAnimationCurve(value).ApplyModifiedProperties();
        }
        #endregion

        #region ArraySize
        public static T SetArraySize<T>(this T property, int size)
            where T : SerializedProperty
        {
            property.arraySize = size;
            return property;
        }
        
        public static T SetArraySizeAndApply<T>(this T property, int size)
            where T : SerializedProperty
        {
            return property.SetArraySize(size).ApplyModifiedProperties();
        }
        #endregion

        #region ManagedReference
        public static T SetManagedReference<T>(this T property, object value)
            where T : SerializedProperty
        {
            property.managedReferenceValue = value;
            return property;
        }
        
        public static T SetManagedReferenceAndApply<T>(this T property, object value)
            where T : SerializedProperty
        {
            return property.SetManagedReference(value).ApplyModifiedProperties();
        }
        #endregion

        #region ObjectReference
        public static T SetObjectReference<T>(this T property, Object value)
            where T : SerializedProperty
        {
            property.objectReferenceValue = value;
            return property;
        }
        
        public static T SetObjectReferenceAndApply<T>(this T property, Object value)
            where T : SerializedProperty
        {
            return property.SetObjectReference(value).ApplyModifiedProperties();
        }
        #endregion

        #region ExposedReference
        public static T SetExposedReference<T>(this T property, Object value)
            where T : SerializedProperty
        {
            property.exposedReferenceValue = value;
            return property;
        }
        
        public static T SetExposedReferenceAndApply<T>(this T property, Object value)
            where T : SerializedProperty
        {
            return property.SetExposedReference(value).ApplyModifiedProperties();
        }
        #endregion
        
#if UNITY_6000_0_OR_NEWER
        #region Boxed
        public static T SetBoxed<T>(this T property, object value) 
            where T : SerializedProperty
        {
            property.boxedValue = value;
            return property;
        }
        
        public static T SetBoxedAndApply<T>(this T property, object value) 
            where T : SerializedProperty
        {
            return property.SetBoxed(value).ApplyModifiedProperties();
        }
        #endregion
#endif

#if UNITY_6000_2_OR_NEWER
        #region EntityId
        public static T SetValue<T>(this T property, EntityId value) 
            where T : SerializedProperty
        {
            return property.SetEntityId(value);
        }
        
        public static T SetValueAndApply<T>(this T property, EntityId value) 
            where T : SerializedProperty
        {
            return property.SetEntityId(value);
        }
        
        public static T SetEntityId<T>(this T property, EntityId value) 
            where T : SerializedProperty
        {
            property.entityIdValue = value;
            return property;
        }
        
        public static T SetEntityIdApply<T>(this T property, EntityId value) 
            where T : SerializedProperty
        {
            return property.SetEntityId(value).ApplyModifiedProperties();
        }
        #endregion
#endif
    }
}