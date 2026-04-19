using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    public static partial class SerializePropertyExtensions
    {
        #region Int
        /// <inheritdoc cref="SetInt{T}"/>
        public static T SetValue<T>(this T property, int value)
            where T : SerializedProperty
        {
            return property.SetInt(value);
        }

        /// <inheritdoc cref="SetIntAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, int value)
            where T : SerializedProperty
        {
            return property.SetIntAndApply(value);
        }

        /// <summary>Sets <see cref="SerializedProperty.intValue"/> and returns the property for chaining.</summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetInt<T>(this T property, int value)
            where T : SerializedProperty
        {
            property.intValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.intValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetInt{T}"/>
        public static T SetIntAndApply<T>(this T property, int value)
            where T : SerializedProperty
        {
            return property.SetInt(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Uint
        /// <inheritdoc cref="SetUint{T}"/>
        public static T SetValue<T>(this T property, uint value)
            where T : SerializedProperty
        {
            return property.SetUint(value);
        }

        /// <inheritdoc cref="SetUintAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, uint value)
            where T : SerializedProperty
        {
            return property.SetUintAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.uintValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetUint<T>(this T property, uint value)
            where T : SerializedProperty
        {
            property.uintValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.uintValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetUint{T}"/>
        public static T SetUintAndApply<T>(this T property, uint value)
            where T : SerializedProperty
        {
            return property.SetUint(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Long
        /// <inheritdoc cref="SetLong{T}"/>
        public static T SetValue<T>(this T property, long value)
            where T : SerializedProperty
        {
            return property.SetLong(value);
        }

        /// <inheritdoc cref="SetLongAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, long value)
            where T : SerializedProperty
        {
            return property.SetLongAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.longValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetLong<T>(this T property, long value)
            where T : SerializedProperty
        {
            property.longValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.longValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetLong{T}"/>
        public static T SetLongAndApply<T>(this T property, long value)
            where T : SerializedProperty
        {
            return property.SetLong(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Ulong
        /// <inheritdoc cref="SetUlong{T}"/>
        public static T SetValue<T>(this T property, ulong value)
            where T : SerializedProperty
        {
            return property.SetUlong(value);
        }

        /// <inheritdoc cref="SetUlongAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, ulong value)
            where T : SerializedProperty
        {
            return property.SetUlongAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.ulongValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetUlong<T>(this T property, ulong value)
            where T : SerializedProperty
        {
            property.ulongValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.ulongValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetUlong{T}"/>
        public static T SetUlongAndApply<T>(this T property, ulong value)
            where T : SerializedProperty
        {
            return property.SetUlong(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Float
        /// <inheritdoc cref="SetFloat{T}"/>
        public static T SetValue<T>(this T property, float value)
            where T : SerializedProperty
        {
            return property.SetFloat(value);
        }

        /// <inheritdoc cref="SetFloatAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, float value)
            where T : SerializedProperty
        {
            return property.SetFloatAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.floatValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetFloat<T>(this T property, float value)
            where T : SerializedProperty
        {
            property.floatValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.floatValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetFloat{T}"/>
        public static T SetFloatAndApply<T>(this T property, float value)
            where T : SerializedProperty
        {
            return property.SetFloat(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Double
        /// <inheritdoc cref="SetDouble{T}"/>
        public static T SetValue<T>(this T property, double value)
            where T : SerializedProperty
        {
            return property.SetDouble(value);
        }

        /// <inheritdoc cref="SetDoubleAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, double value)
            where T : SerializedProperty
        {
            return property.SetDoubleAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.doubleValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetDouble<T>(this T property, double value)
            where T : SerializedProperty
        {
            property.doubleValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.doubleValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetDouble{T}"/>
        public static T SetDoubleAndApply<T>(this T property, double value)
            where T : SerializedProperty
        {
            return property.SetDouble(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region EnumIndex
        /// <summary>
        /// Sets <see cref="SerializedProperty.enumValueFlag"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Flag value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetEnumFlag<T>(this T property, int value)
            where T : SerializedProperty
        {
            property.enumValueFlag = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.enumValueFlag"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetEnumFlag{T}"/>
        public static T SetEnumFlagAndApply<T>(this T property, int value)
            where T : SerializedProperty
        {
            return property.SetEnumFlag(value).ApplyModifiedProperties();
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.enumValueIndex"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Index value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetEnumIndex<T>(this T property, int value)
            where T : SerializedProperty
        {
            property.enumValueIndex = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.enumValueIndex"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetEnumIndex{T}"/>
        public static T SetEnumIndexAndApply<T>(this T property, int value)
            where T : SerializedProperty
        {
            return property.SetEnumIndex(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Bool
        /// <inheritdoc cref="SetBool{T}"/>
        public static T SetValue<T>(this T property, bool value)
            where T : SerializedProperty
        {
            return property.SetBool(value);
        }

        /// <inheritdoc cref="SetBoolAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, bool value)
            where T : SerializedProperty
        {
            return property.SetBoolAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.boolValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetBool<T>(this T property, bool value)
            where T : SerializedProperty
        {
            property.boolValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.boolValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetBool{T}"/>
        public static T SetBoolAndApply<T>(this T property, bool value)
            where T : SerializedProperty
        {
            return property.SetBool(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Rect
        /// <inheritdoc cref="SetRect{T}"/>
        public static T SetValue<T>(this T property, Rect value)
            where T : SerializedProperty
        {
            return property.SetRect(value);
        }

        /// <inheritdoc cref="SetRectAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Rect value)
            where T : SerializedProperty
        {
            return property.SetRectAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.rectValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetRect<T>(this T property, Rect value)
            where T : SerializedProperty
        {
            property.rectValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.rectValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetRect{T}"/>
        public static T SetRectAndApply<T>(this T property, Rect value)
            where T : SerializedProperty
        {
            return property.SetRect(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region RectInt
        /// <inheritdoc cref="SetRectInt{T}"/>
        public static T SetValue<T>(this T property, RectInt value)
            where T : SerializedProperty
        {
            return property.SetRectInt(value);
        }

        /// <inheritdoc cref="SetRectIntAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, RectInt value)
            where T : SerializedProperty
        {
            return property.SetRectIntAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.rectIntValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetRectInt<T>(this T property, RectInt value)
            where T : SerializedProperty
        {
            property.rectIntValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.rectIntValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetRectInt{T}"/>
        public static T SetRectIntAndApply<T>(this T property, RectInt value)
            where T : SerializedProperty
        {
            return property.SetRectInt(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Bounds
        /// <inheritdoc cref="SetBounds{T}"/>
        public static T SetValue<T>(this T property, Bounds value)
            where T : SerializedProperty
        {
            return property.SetBounds(value);
        }

        /// <inheritdoc cref="SetBoundsAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Bounds value)
            where T : SerializedProperty
        {
            return property.SetBoundsAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.boundsValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetBounds<T>(this T property, Bounds value)
            where T : SerializedProperty
        {
            property.boundsValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.boundsValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetBounds{T}"/>
        public static T SetBoundsAndApply<T>(this T property, Bounds value)
            where T : SerializedProperty
        {
            return property.SetBounds(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region BoundsInt
        /// <inheritdoc cref="SetBoundsInt{T}"/>
        public static T SetValue<T>(this T property, BoundsInt value)
            where T : SerializedProperty
        {
            return property.SetBoundsInt(value);
        }

        /// <inheritdoc cref="SetBoundsIntAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, BoundsInt value)
            where T : SerializedProperty
        {
            return property.SetBoundsIntAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.boundsIntValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetBoundsInt<T>(this T property, BoundsInt value)
            where T : SerializedProperty
        {
            property.boundsIntValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.boundsIntValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetBoundsInt{T}"/>
        public static T SetBoundsIntAndApply<T>(this T property, BoundsInt value)
            where T : SerializedProperty
        {
            return property.SetBoundsInt(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Color
        /// <inheritdoc cref="SetColor{T}"/>
        public static T SetValue<T>(this T property, Color value)
            where T : SerializedProperty
        {
            return property.SetColor(value);
        }

        /// <inheritdoc cref="SetColorAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Color value)
            where T : SerializedProperty
        {
            return property.SetColorAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.colorValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetColor<T>(this T property, Color value)
            where T : SerializedProperty
        {
            property.colorValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.colorValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetColor{T}"/>
        public static T SetColorAndApply<T>(this T property, Color value)
            where T : SerializedProperty
        {
            return property.SetColor(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Gradient
        /// <inheritdoc cref="SetGradient{T}"/>
        public static T SetValue<T>(this T property, Gradient value)
            where T : SerializedProperty
        {
            return property.SetGradient(value);
        }

        /// <inheritdoc cref="SetGradientAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Gradient value)
            where T : SerializedProperty
        {
            return property.SetGradientAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.gradientValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetGradient<T>(this T property, Gradient value)
            where T : SerializedProperty
        {
            property.gradientValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.gradientValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetGradient{T}"/>
        public static T SetGradientAndApply<T>(this T property, Gradient value)
            where T : SerializedProperty
        {
            return property.SetGradient(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Hash128
        /// <inheritdoc cref="SetHash128{T}"/>
        public static T SetValue<T>(this T property, Hash128 value)
            where T : SerializedProperty
        {
            return property.SetHash128(value);
        }

        /// <inheritdoc cref="SetHash128AndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Hash128 value)
            where T : SerializedProperty
        {
            return property.SetHash128AndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.hash128Value"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetHash128<T>(this T property, Hash128 value)
            where T : SerializedProperty
        {
            property.hash128Value = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.hash128Value"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetHash128{T}"/>
        public static T SetHash128AndApply<T>(this T property, Hash128 value)
            where T : SerializedProperty
        {
            return property.SetHash128(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Vector4
        /// <inheritdoc cref="SetVector4{T}"/>
        public static T SetValue<T>(this T property, Vector4 value)
            where T : SerializedProperty
        {
            return property.SetVector4(value);
        }

        /// <inheritdoc cref="SetVector4AndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Vector4 value)
            where T : SerializedProperty
        {
            return property.SetVector4AndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.vector4Value"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetVector4<T>(this T property, Vector4 value)
            where T : SerializedProperty
        {
            property.vector4Value = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.vector4Value"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetVector4{T}"/>
        public static T SetVector4AndApply<T>(this T property, Vector4 value)
            where T : SerializedProperty
        {
            return property.SetVector4(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Vector3
        /// <inheritdoc cref="SetVector3{T}"/>
        public static T SetValue<T>(this T property, Vector3 value)
            where T : SerializedProperty
        {
            return property.SetVector3(value);
        }

        /// <inheritdoc cref="SetVector3AndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Vector3 value)
            where T : SerializedProperty
        {
            return property.SetVector3AndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.vector3Value"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetVector3<T>(this T property, Vector3 value)
            where T : SerializedProperty
        {
            property.vector3Value = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.vector3Value"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetVector3{T}"/>
        public static T SetVector3AndApply<T>(this T property, Vector3 value)
            where T : SerializedProperty
        {
            return property.SetVector3(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Vector3Int
        /// <inheritdoc cref="SetVector3Int{T}"/>
        public static T SetValue<T>(this T property, Vector3Int value)
            where T : SerializedProperty
        {
            return property.SetVector3Int(value);
        }

        /// <inheritdoc cref="SetVector3IntAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Vector3Int value)
            where T : SerializedProperty
        {
            return property.SetVector3IntAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.vector3IntValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetVector3Int<T>(this T property, Vector3Int value)
            where T : SerializedProperty
        {
            property.vector3IntValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.vector3IntValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetVector3Int{T}"/>
        public static T SetVector3IntAndApply<T>(this T property, Vector3Int value)
            where T : SerializedProperty
        {
            return property.SetVector3Int(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Vector2
        /// <inheritdoc cref="SetVector2{T}"/>
        public static T SetValue<T>(this T property, Vector2 value)
            where T : SerializedProperty
        {
            return property.SetVector2(value);
        }

        /// <inheritdoc cref="SetVector2AndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Vector2 value)
            where T : SerializedProperty
        {
            return property.SetVector2AndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.vector2Value"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetVector2<T>(this T property, Vector2 value)
            where T : SerializedProperty
        {
            property.vector2Value = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.vector2Value"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetVector2{T}"/>
        public static T SetVector2AndApply<T>(this T property, Vector2 value)
            where T : SerializedProperty
        {
            return property.SetVector2(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Vector2Int
        /// <inheritdoc cref="SetVector2Int{T}"/>
        public static T SetValue<T>(this T property, Vector2Int value)
            where T : SerializedProperty
        {
            return property.SetVector2Int(value);
        }

        /// <inheritdoc cref="SetVector2IntAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Vector2Int value)
            where T : SerializedProperty
        {
            return property.SetVector2IntAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.vector2IntValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetVector2Int<T>(this T property, Vector2Int value)
            where T : SerializedProperty
        {
            property.vector2Value = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.vector2IntValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetVector2Int{T}"/>
        public static T SetVector2IntAndApply<T>(this T property, Vector2Int value)
            where T : SerializedProperty
        {
            return property.SetVector2Int(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region Quaternion
        /// <inheritdoc cref="SetQuaternion{T}"/>
        public static T SetValue<T>(this T property, Quaternion value)
            where T : SerializedProperty
        {
            return property.SetQuaternion(value);
        }

        /// <inheritdoc cref="SetQuaternionAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, Quaternion value)
            where T : SerializedProperty
        {
            return property.SetQuaternionAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.quaternionValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetQuaternion<T>(this T property, Quaternion value)
            where T : SerializedProperty
        {
            property.quaternionValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.quaternionValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetQuaternion{T}"/>
        public static T SetQuaternionAndApply<T>(this T property, Quaternion value)
            where T : SerializedProperty
        {
            return property.SetQuaternion(value).ApplyModifiedProperties();
        }
        #endregion
        
        #region String
        /// <inheritdoc cref="SetString{T}"/>
        public static T SetValue<T>(this T property, string value)
            where T : SerializedProperty
        {
            return property.SetString(value);
        }

        /// <inheritdoc cref="SetStringAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, string value)
            where T : SerializedProperty
        {
            return property.SetStringAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.stringValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetString<T>(this T property, string value)
            where T : SerializedProperty
        {
            property.stringValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.stringValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetString{T}"/>
        public static T SetStringAndApply<T>(this T property, string value)
            where T : SerializedProperty
        {
            return property.SetString(value).ApplyModifiedProperties();
        }
        #endregion

        #region AnimationCurve
        /// <inheritdoc cref="SetAnimationCurve{T}"/>
        public static T SetValue<T>(this T property, AnimationCurve value)
            where T : SerializedProperty
        {
            return property.SetAnimationCurve(value);
        }

        /// <inheritdoc cref="SetAnimationCurveAndApply{T}"/>
        public static T SetValueAndApply<T>(this T property, AnimationCurve value)
            where T : SerializedProperty
        {
            return property.SetAnimationCurveAndApply(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.animationCurveValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetAnimationCurve<T>(this T property, AnimationCurve value)
            where T : SerializedProperty
        {
            property.animationCurveValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.animationCurveValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetAnimationCurve{T}"/>
        public static T SetAnimationCurveAndApply<T>(this T property, AnimationCurve value)
            where T : SerializedProperty
        {
            return property.SetAnimationCurve(value).ApplyModifiedProperties();
        }
        #endregion

        #region ArraySize
        /// <summary>
        /// Sets <see cref="SerializedProperty.arraySize"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target array property.</param>
        /// <param name="size">New array size.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetArraySize<T>(this T property, int size)
            where T : SerializedProperty
        {
            property.arraySize = size;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.arraySize"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetArraySize{T}"/>
        public static T SetArraySizeAndApply<T>(this T property, int size)
            where T : SerializedProperty
        {
            return property.SetArraySize(size).ApplyModifiedProperties();
        }

        /// <summary>
        /// Increases <see cref="SerializedProperty.arraySize"/> by <paramref name="value"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target array property.</param>
        /// <param name="value">Amount to add to the current array size.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T AddArraySize<T>(this T property, int value = 1)
            where T : SerializedProperty
        {
            return SetArraySize(property, size: property.arraySize + value);
        }

        /// <summary>
        /// Increases <see cref="SerializedProperty.arraySize"/> by <paramref name="value"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="AddArraySize{T}"/>
        public static T AddArraySizeAndApply<T>(this T property, int value = 1)
            where T : SerializedProperty
        {
            return SetArraySizeAndApply(property, size: property.arraySize + value);
        }

        /// <summary>
        /// Decreases <see cref="SerializedProperty.arraySize"/> by <paramref name="value"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target array property.</param>
        /// <param name="value">Amount to subtract from the current array size.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T RemoveArraySize<T>(this T property, int value = 1)
            where T : SerializedProperty
        {
            return SetArraySize(property, size: property.arraySize - value);
        }

        /// <summary>
        /// Decreases <see cref="SerializedProperty.arraySize"/> by <paramref name="value"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="RemoveArraySize{T}"/>
        public static T RemoveArraySizeAndApply<T>(this T property, int value = 1)
            where T : SerializedProperty
        {
            return SetArraySizeAndApply(property, size: property.arraySize - value);
        }
        #endregion

        #region ManagedReference
        /// <summary>
        /// Sets <see cref="SerializedProperty.managedReferenceValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property (must be a <c>[SerializeReference]</c> field).</param>
        /// <param name="value">Managed reference value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetManagedReference<T>(this T property, object value)
            where T : SerializedProperty
        {
            property.managedReferenceValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.managedReferenceValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetManagedReference{T}"/>
        public static T SetManagedReferenceAndApply<T>(this T property, object value)
            where T : SerializedProperty
        {
            return property.SetManagedReference(value).ApplyModifiedProperties();
        }
        #endregion

        #region ObjectReference
        /// <summary>
        /// Sets <see cref="SerializedProperty.objectReferenceValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value"><see cref="UnityEngine.Object"/> reference to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetObjectReference<T>(this T property, Object value)
            where T : SerializedProperty
        {
            property.objectReferenceValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.objectReferenceValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetObjectReference{T}"/>
        public static T SetObjectReferenceAndApply<T>(this T property, Object value)
            where T : SerializedProperty
        {
            return property.SetObjectReference(value).ApplyModifiedProperties();
        }
        #endregion

        #region ExposedReference
        /// <summary>
        /// Sets <see cref="SerializedProperty.exposedReferenceValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value"><see cref="UnityEngine.Object"/> exposed reference to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetExposedReference<T>(this T property, Object value)
            where T : SerializedProperty
        {
            property.exposedReferenceValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.exposedReferenceValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetExposedReference{T}"/>
        public static T SetExposedReferenceAndApply<T>(this T property, Object value)
            where T : SerializedProperty
        {
            return property.SetExposedReference(value).ApplyModifiedProperties();
        }
        #endregion
        
#if UNITY_6000_0_OR_NEWER
        #region Boxed
        /// <summary>
        /// Sets <see cref="SerializedProperty.boxedValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Boxed value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetBoxed<T>(this T property, object value)
            where T : SerializedProperty
        {
            property.boxedValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.boxedValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetBoxed{T}"/>
        public static T SetBoxedAndApply<T>(this T property, object value)
            where T : SerializedProperty
        {
            return property.SetBoxed(value).ApplyModifiedProperties();
        }
        #endregion
#endif

#if UNITY_6000_2_OR_NEWER
        #region EntityId
        /// <inheritdoc cref="SetEntityId{T}"/>
        public static T SetValue<T>(this T property, EntityId value)
            where T : SerializedProperty
        {
            return property.SetEntityId(value);
        }

        /// <inheritdoc cref="SetEntityIdApply{T}"/>
        public static T SetValueAndApply<T>(this T property, EntityId value)
            where T : SerializedProperty
        {
            return property.SetEntityId(value);
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.entityIdValue"/> and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">Target property.</param>
        /// <param name="value">Value to assign.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T SetEntityId<T>(this T property, EntityId value)
            where T : SerializedProperty
        {
            property.entityIdValue = value;
            return property;
        }

        /// <summary>
        /// Sets <see cref="SerializedProperty.entityIdValue"/> then applies modified properties.
        /// </summary>
        /// <inheritdoc cref="SetEntityId{T}"/>
        public static T SetEntityIdApply<T>(this T property, EntityId value)
            where T : SerializedProperty
        {
            return property.SetEntityId(value).ApplyModifiedProperties();
        }
        #endregion
#endif
    }
}
