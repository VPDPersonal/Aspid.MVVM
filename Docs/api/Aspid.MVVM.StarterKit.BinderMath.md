---
title: "Class BinderMath"
sidebar_label: "BinderMath"
description: "Class BinderMath — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BinderMath {#Aspid_MVVM_StarterKit_BinderMath}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Sanitizing helpers that report the value they had to replace.

```csharp
public static class BinderMath
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BinderMath](Aspid.MVVM.StarterKit.BinderMath.md)



## Remarks

A value outside the target's range saturates at the bound without reporting. A non-finite value has
not bound to saturate at, so it is replaced and reported through [`BinderLogger`](Aspid.MVVM.StarterKit.BinderLogger.md). The
[`Type`](https://learn.microsoft.com/dotnet/api/system.type) overloads let a helper report on another binder's behalf.

## Methods

### IsFinite\(float\) {#Aspid_MVVM_StarterKit_BinderMath_IsFinite_System_Single_}

Indicates whether <code class="paramref">value</code> is a finite number.

```csharp
public static bool IsFinite(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite number; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Remarks

[`Clamp`](https://docs.unity3d.com/ScriptReference/Mathf-Clamp.html) is two comparisons that are both false for
[`NaN`](https://learn.microsoft.com/dotnet/api/system.single.nan), so NaN passes through untouched.

### NonNegative\(IBinder, float, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_}

Returns <code class="paramref">value</code> with anything below zero raised to <code>0</code>,
reporting non-finite input and mapping it to <code>0</code> as well.

```csharp
public static float NonNegative(this IBinder binder, float value, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The sanitizing binder; a scene or asset object is pinged by default.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The extent to sanitize.

`context` Object?

The object to ping instead of the binder.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

<code class="paramref">value</code> when it is finite and positive; otherwise, <code>0</code>.

#### Remarks

Unity rejects a non-finite extent with an error but accepts a negative one silently.

### NonNegative\(Type, float, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_NonNegative_System_Type_System_Single_UnityEngine_Object_}

Raises anything below zero, and anything not finite, to <code>0</code>
on behalf of the specified binder type.

```csharp
public static float NonNegative(Type binderType, float value, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The sanitizing binder's type.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The extent to sanitize.

`context` Object?

The object to ping, when one is known.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

<code class="paramref">value</code> when it is finite and positive; otherwise, <code>0</code>.

### NonNegative\(IBinder, Vector2, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_}

Returns <code class="paramref">value</code> with anything below zero raised to <code>0</code>,
reporting non-finite input and mapping it to <code>0</code> as well.

```csharp
public static Vector2 NonNegative(this IBinder binder, Vector2 value, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The sanitizing binder; a scene or asset object is pinged by default.

`value` Vector2

The extent to sanitize.

`context` Object?

The object to ping instead of the binder.

#### Returns

 Vector2

<code class="paramref">value</code> when it is finite and positive; otherwise, <code>0</code>.

#### Remarks

Unity rejects a non-finite extent with an error but accepts a negative one silently.

### NonNegative\(Type, Vector2, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_NonNegative_System_Type_UnityEngine_Vector2_UnityEngine_Object_}

Raises anything below zero, and anything not finite, to <code>0</code>
on behalf of the specified binder type.

```csharp
public static Vector2 NonNegative(Type binderType, Vector2 value, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The sanitizing binder's type.

`value` Vector2

The extent to sanitize.

`context` Object?

The object to ping, when one is known.

#### Returns

 Vector2

<code class="paramref">value</code> when it is finite and positive; otherwise, <code>0</code>.

### NonNegative\(IBinder, Vector3, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_}

Returns <code class="paramref">value</code> with anything below zero raised to <code>0</code>,
reporting non-finite input and mapping it to <code>0</code> as well.

```csharp
public static Vector3 NonNegative(this IBinder binder, Vector3 value, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The sanitizing binder; a scene or asset object is pinged by default.

`value` Vector3

The extent to sanitize.

`context` Object?

The object to ping instead of the binder.

#### Returns

 Vector3

<code class="paramref">value</code> when it is finite and positive; otherwise, <code>0</code>.

#### Remarks

Unity rejects a non-finite extent with an error but accepts a negative one silently.

### NonNegative\(Type, Vector3, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_NonNegative_System_Type_UnityEngine_Vector3_UnityEngine_Object_}

Raises anything below zero, and anything not finite, to <code>0</code>
on behalf of the specified binder type.

```csharp
public static Vector3 NonNegative(Type binderType, Vector3 value, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The sanitizing binder's type.

`value` Vector3

The extent to sanitize.

`context` Object?

The object to ping, when one is known.

#### Returns

 Vector3

<code class="paramref">value</code> when it is finite and positive; otherwise, <code>0</code>.

### RequireFinite\(IBinder, float, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_}

Reports <code class="paramref">value</code> when it is not finite, so the caller can skip to write.

```csharp
public static bool RequireFinite(this IBinder binder, float value, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The checking binder; a scene or asset object is pinged by default.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to test.

`context` Object?

The object to ping instead of the binder.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### RequireFinite\(Type, float, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_System_Type_System_Single_UnityEngine_Object_}

Reports <code class="paramref">value</code> when it is not finite, on behalf of the specified binder type.

```csharp
public static bool RequireFinite(Type binderType, float value, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The checking binder's type.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to test.

`context` Object?

The object to ping, when one is known.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### RequireFinite\(IBinder, Vector2, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_}

Reports <code class="paramref">value</code> when it is not finite, so the caller can skip to write.

```csharp
public static bool RequireFinite(this IBinder binder, Vector2 value, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The checking binder; a scene or asset object is pinged by default.

`value` Vector2

The value to test.

`context` Object?

The object to ping instead of the binder.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### RequireFinite\(Type, Vector2, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_System_Type_UnityEngine_Vector2_UnityEngine_Object_}

Reports <code class="paramref">value</code> when it is not finite, on behalf of the specified binder type.

```csharp
public static bool RequireFinite(Type binderType, Vector2 value, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The checking binder's type.

`value` Vector2

The value to test.

`context` Object?

The object to ping, when one is known.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### RequireFinite\(IBinder, Vector3, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_}

Reports <code class="paramref">value</code> when it is not finite, so the caller can skip to write.

```csharp
public static bool RequireFinite(this IBinder binder, Vector3 value, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The checking binder; a scene or asset object is pinged by default.

`value` Vector3

The value to test.

`context` Object?

The object to ping instead of the binder.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### RequireFinite\(Type, Vector3, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_System_Type_UnityEngine_Vector3_UnityEngine_Object_}

Reports <code class="paramref">value</code> when it is not finite, on behalf of the specified binder type.

```csharp
public static bool RequireFinite(Type binderType, Vector3 value, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The checking binder's type.

`value` Vector3

The value to test.

`context` Object?

The object to ping, when one is known.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### RequireFinite\(IBinder, Vector4, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_}

Reports <code class="paramref">value</code> when it is not finite, so the caller can skip to write.

```csharp
public static bool RequireFinite(this IBinder binder, Vector4 value, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The checking binder; a scene or asset object is pinged by default.

`value` Vector4

The value to test.

`context` Object?

The object to ping instead of the binder.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### RequireFinite\(Type, Vector4, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_System_Type_UnityEngine_Vector4_UnityEngine_Object_}

Reports <code class="paramref">value</code> when it is not finite, on behalf of the specified binder type.

```csharp
public static bool RequireFinite(Type binderType, Vector4 value, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The checking binder's type.

`value` Vector4

The value to test.

`context` Object?

The object to ping, when one is known.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### RequireFinite\(IBinder, Rect, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_}

Reports <code class="paramref">value</code> when it is not finite, so the caller can skip to write.

```csharp
public static bool RequireFinite(this IBinder binder, Rect value, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The checking binder; a scene or asset object is pinged by default.

`value` Rect

The value to test.

`context` Object?

The object to ping instead of the binder.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### RequireFinite\(Type, Rect, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_System_Type_UnityEngine_Rect_UnityEngine_Object_}

Reports <code class="paramref">value</code> when it is not finite, on behalf of the specified binder type.

```csharp
public static bool RequireFinite(Type binderType, Rect value, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The checking binder's type.

`value` Rect

The value to test.

`context` Object?

The object to ping, when one is known.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a finite value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### SafeClamp\(IBinder, float, float, float, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_}

Clamps <code class="paramref">value</code> between <code class="paramref">min</code> and <code class="paramref">max</code>,
reporting non-finite input and mapping it to <code class="paramref">min</code>.

```csharp
public static float SafeClamp(this IBinder binder, float value, float min, float max, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The clamping binder; a scene or asset object is pinged by default.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to clamp.

`min` [float](https://learn.microsoft.com/dotnet/api/system.single)

The lower bound, returned for [`NaN`](https://learn.microsoft.com/dotnet/api/system.single.nan) and infinities.

`max` [float](https://learn.microsoft.com/dotnet/api/system.single)

The upper bound.

`context` Object?

The object to ping instead of the binder.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The clamped value, or <code class="paramref">min</code> when <code class="paramref">value</code> is not finite.

### SafeClamp\(Type, float, float, float, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_System_Type_System_Single_System_Single_System_Single_UnityEngine_Object_}

Clamps <code class="paramref">value</code> between <code class="paramref">min</code> and <code class="paramref">max</code>
on behalf of the specified binder type.

```csharp
public static float SafeClamp(Type binderType, float value, float min, float max, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The clamping binder's type.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to clamp.

`min` [float](https://learn.microsoft.com/dotnet/api/system.single)

The lower bound, returned for [`NaN`](https://learn.microsoft.com/dotnet/api/system.single.nan) and infinities.

`max` [float](https://learn.microsoft.com/dotnet/api/system.single)

The upper bound.

`context` Object?

The object to ping, when one is known.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The clamped value, or <code class="paramref">min</code> when <code class="paramref">value</code> is not finite.

### SafeClamp01\(IBinder, float, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_}

Clamps <code class="paramref">value</code> to the 0..1 range, reporting non-finite input and mapping it to <code>0</code>.

```csharp
public static float SafeClamp01(this IBinder binder, float value, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The clamping binder; a scene or asset object is pinged by default.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to clamp.

`context` Object?

The object to ping instead of the binder.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The clamped value, or <code>0</code> when <code class="paramref">value</code> is not finite.

### SafeClamp01\(Type, float, Object?\) {#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_System_Type_System_Single_UnityEngine_Object_}

Clamps <code class="paramref">value</code> to the 0..1 range on behalf of the specified binder type.

```csharp
public static float SafeClamp01(Type binderType, float value, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The clamping binder's type.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to clamp.

`context` Object?

The object to ping, when one is known.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The clamped value, or <code>0</code> when <code class="paramref">value</code> is not finite.

