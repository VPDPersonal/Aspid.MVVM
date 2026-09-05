---
title: "Class ValueTwoWayBinder<T>"
sidebar_label: "ValueTwoWayBinder<T>"
description: "Class ValueTwoWayBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ValueTwoWayBinder\<T\> {#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) and [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) that stores a value
and synchronizes it in both directions. Supports every binding mode; in [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md),
the current value is pushed to the ViewModel on binding.

```csharp
[Serializable]
[BindModeOverride(new BindMode[] { }, IsAll = true)]
public class ValueTwoWayBinder<T> : Binder, IRebindableBinder, IBinder<T>, IReverseBinder<T>, IBinder
```

#### Type Parameters

`T` 

The type of the stored value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[ValueTwoWayBinder\<T\>](Aspid.MVVM.StarterKit.ValueTwoWayBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<T\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<T\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ValueTwoWayBinder\<T\>\>\(ValueTwoWayBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ValueTwoWayBinder\<T\>\>\(ValueTwoWayBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ValueTwoWayBinder\<T\>\>\(ValueTwoWayBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### ValueTwoWayBinder\(T?, BindMode\) {#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1__ctor__0_Aspid_MVVM_BindMode_}

```csharp
public ValueTwoWayBinder(T? value = default, BindMode mode = BindMode.TwoWay)
```

#### Parameters

`value` T?

The initial value.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when <code class="paramref">mode</code> is [`BindMode.None`](Aspid.MVVM.BindMode.md).

### ValueTwoWayBinder\(T?, IConverter\<T?, T?\>?, BindMode\) {#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1__ctor__0_Aspid_MVVM_StarterKit_IConverter__0__0__Aspid_MVVM_BindMode_}

```csharp
public ValueTwoWayBinder(T? value, IConverter<T?, T?>? converter, BindMode mode = BindMode.TwoWay)
```

#### Parameters

`value` T?

The initial value.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, T?\>?

The converter applied to each ViewModel value, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to store it unchanged.
Runs in reverse only if it implements [`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when <code class="paramref">mode</code> is [`BindMode.None`](Aspid.MVVM.BindMode.md).

## Properties

### Value {#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1_Value}

Gets or sets the current value. Setting it notifies the ViewModel through [`IReverseBinder<T>.ValueChanged`](Aspid.MVVM.IReverseBinder-1.md#Aspid_MVVM_IReverseBinder_1_ValueChanged).

```csharp
public T? Value { get; set; }
```

#### Property Value

 T?

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1_OnBound}

Pushes the current [`ValueTwoWayBinder<T>.Value`](Aspid.MVVM.StarterKit.ValueTwoWayBinder-1.md#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1_Value) to the ViewModel in [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

```csharp
protected override void OnBound()
```

### Changed {#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1_Changed}

Raised with the unconverted ViewModel value when it updates [`ValueTwoWayBinder<T>.Value`](Aspid.MVVM.StarterKit.ValueTwoWayBinder-1.md#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1_Value).

```csharp
public event Action<T?>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

## Operators

### implicit operator T?\(ValueTwoWayBinder\<T\>\) {#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1_op_Implicit_Aspid_MVVM_StarterKit_ValueTwoWayBinder__0____0}

Returns [`ValueTwoWayBinder<T>.Value`](Aspid.MVVM.StarterKit.ValueTwoWayBinder-1.md#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1_Value).

```csharp
public static implicit operator T?(ValueTwoWayBinder<T> binder)
```

#### Parameters

`binder` [ValueTwoWayBinder](Aspid.MVVM.StarterKit.ValueTwoWayBinder-1.md)\<T\>

The binder to read.

#### Returns

 T?

The current [`ValueTwoWayBinder<T>.Value`](Aspid.MVVM.StarterKit.ValueTwoWayBinder-1.md#Aspid_MVVM_StarterKit_ValueTwoWayBinder_1_Value).

