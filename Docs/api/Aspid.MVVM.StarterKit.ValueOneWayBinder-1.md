---
title: "Class ValueOneWayBinder<T>"
sidebar_label: "ValueOneWayBinder<T>"
description: "Class ValueOneWayBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ValueOneWayBinder\<T\> {#Aspid_MVVM_StarterKit_ValueOneWayBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) that stores the latest ViewModel value and raises [`ValueOneWayBinder<T>.Changed`](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md#Aspid_MVVM_StarterKit_ValueOneWayBinder_1_Changed).

```csharp
[Serializable]
public class ValueOneWayBinder<T> : Binder, IRebindableBinder, IBinder<T>, IBinder
```

#### Type Parameters

`T` 

The type of the stored value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[ValueOneWayBinder\<T\>](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<T\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ValueOneWayBinder\<T\>\>\(ValueOneWayBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ValueOneWayBinder\<T\>\>\(ValueOneWayBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ValueOneWayBinder\<T\>\>\(ValueOneWayBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### ValueOneWayBinder\(T?, BindMode\) {#Aspid_MVVM_StarterKit_ValueOneWayBinder_1__ctor__0_Aspid_MVVM_BindMode_}

```csharp
public ValueOneWayBinder(T? value = default, BindMode mode = BindMode.OneWay)
```

#### Parameters

`value` T?

The initial value.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. Must not be [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when <code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

### ValueOneWayBinder\(T?, IConverter\<T?, T?\>?, BindMode\) {#Aspid_MVVM_StarterKit_ValueOneWayBinder_1__ctor__0_Aspid_MVVM_StarterKit_IConverter__0__0__Aspid_MVVM_BindMode_}

```csharp
public ValueOneWayBinder(T? value, IConverter<T?, T?>? converter, BindMode mode = BindMode.OneWay)
```

#### Parameters

`value` T?

The initial value.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, T?\>?

The converter applied to each incoming value, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to store it unchanged.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. Must not be [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when <code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

## Properties

### Value {#Aspid_MVVM_StarterKit_ValueOneWayBinder_1_Value}

Gets the latest, converted value.

```csharp
public T? Value { get; }
```

#### Property Value

 T?

### Changed {#Aspid_MVVM_StarterKit_ValueOneWayBinder_1_Changed}

Raised with the unconverted ViewModel value when [`ValueOneWayBinder<T>.Value`](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md#Aspid_MVVM_StarterKit_ValueOneWayBinder_1_Value) is updated.

```csharp
public event Action<T?>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

## Operators

### implicit operator T?\(ValueOneWayBinder\<T\>\) {#Aspid_MVVM_StarterKit_ValueOneWayBinder_1_op_Implicit_Aspid_MVVM_StarterKit_ValueOneWayBinder__0____0}

Returns [`ValueOneWayBinder<T>.Value`](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md#Aspid_MVVM_StarterKit_ValueOneWayBinder_1_Value).

```csharp
public static implicit operator T?(ValueOneWayBinder<T> binder)
```

#### Parameters

`binder` [ValueOneWayBinder](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md)\<T\>

The binder to read.

#### Returns

 T?

The current [`ValueOneWayBinder<T>.Value`](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md#Aspid_MVVM_StarterKit_ValueOneWayBinder_1_Value).

