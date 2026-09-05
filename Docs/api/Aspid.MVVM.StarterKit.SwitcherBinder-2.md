---
title: "Class SwitcherBinder<TTarget, T>"
sidebar_label: "SwitcherBinder<TTarget, T>"
description: "Class SwitcherBinder<TTarget, T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SwitcherBinder\<TTarget, T\> {#Aspid_MVVM_StarterKit_SwitcherBinder_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that applies one of two preset values depending on a bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a>,
passing the chosen value through an optional converter first.

```csharp
[Serializable]
public abstract class SwitcherBinder<TTarget, T> : TargetBinder<TTarget>, IRebindableBinder, IBinder<bool>, IBinder
```

#### Type Parameters

`TTarget` 

The type of the target object whose property is switched.

`T` 

The type of the values switched between.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<TTarget\>](Aspid.MVVM.TargetBinder-1.md) ← 
[SwitcherBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.SwitcherBinder-2.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<bool\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<SwitcherBinder\<TTarget, T\>\>\(SwitcherBinder\<TTarget, T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<SwitcherBinder\<TTarget, T\>\>\(SwitcherBinder\<TTarget, T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_), 
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[RebindableBinderExtensions.Rebind\(IBinder\)](Aspid.MVVM.RebindableBinderExtensions.md#Aspid_MVVM_RebindableBinderExtensions_Rebind_Aspid_MVVM_IBinder_), 
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_), 
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_), 
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderExtensions.UnbindSafely\<SwitcherBinder\<TTarget, T\>\>\(SwitcherBinder\<TTarget, T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### SwitcherBinder\(\) {#Aspid_MVVM_StarterKit_SwitcherBinder_2__ctor}

```csharp
protected SwitcherBinder()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### SwitcherBinder\(TTarget, T, T, IConverter\<T?, T?\>?, BindMode\) {#Aspid_MVVM_StarterKit_SwitcherBinder_2__ctor__0__1__1_Aspid_MVVM_StarterKit_IConverter__1__1__Aspid_MVVM_BindMode_}

```csharp
protected SwitcherBinder(TTarget target, T trueValue, T falseValue, IConverter<T?, T?>? converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TTarget

The target object that receives the chosen value.

`trueValue` T

The value applied when the bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

`falseValue` T

The value applied when the bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, T?\>?

The converter applied to the chosen value, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it unchanged.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. Must not be [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">target</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when <code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

## Methods

### GetConvertedValue\(T?\) {#Aspid_MVVM_StarterKit_SwitcherBinder_2_GetConvertedValue__1_}

Converts <code class="paramref">value</code> with the serialized converter, or returns it unchanged when none is set.

```csharp
protected virtual T? GetConvertedValue(T? value)
```

#### Parameters

`value` T?

The value to convert.

#### Returns

 T?

The converted value.

### SetValue\(bool\) {#Aspid_MVVM_StarterKit_SwitcherBinder_2_SetValue_System_Boolean_}

Chooses the true or false value, converts it via [`SwitcherBinder<T1, T2>.GetConvertedValue`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md#Aspid_MVVM_StarterKit_SwitcherBinder_2_GetConvertedValue__1_) and forwards it to [`SwitcherBinder<T1, T2>.SetValue`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md#Aspid_MVVM_StarterKit_SwitcherBinder_2_SetValue__1_).

```csharp
public void SetValue(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The value received from the ViewModel.

### SetValue\(T?\) {#Aspid_MVVM_StarterKit_SwitcherBinder_2_SetValue__1_}

Applies the chosen, converted <code class="paramref">value</code> to the target.

```csharp
protected abstract void SetValue(T? value)
```

#### Parameters

`value` T?

The value to apply.

