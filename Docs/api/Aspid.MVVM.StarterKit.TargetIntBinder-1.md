---
title: "Class TargetIntBinder<TTarget>"
sidebar_label: "TargetIntBinder<TTarget>"
description: "Class TargetIntBinder<TTarget> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TargetIntBinder\<TTarget\> {#Aspid_MVVM_StarterKit_TargetIntBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`StarterKit.TargetBinder<T1, T2>?text=TargetBinder%3cTTarget%2c+int%3e`](Aspid.MVVM.StarterKit.md) that binds an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a> property,
accepting every numeric type via [`IIntBinder`](Aspid.MVVM.StarterKit.IIntBinder.md) and reporting to every numeric type via [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md).

```csharp
[Serializable]
public abstract class TargetIntBinder<TTarget> : TargetBinder<TTarget, int>, IRebindableBinder, IIntBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, INumberReverseBinder, IReverseBinder<int>, IReverseBinder<long>, IReverseBinder<float>, IReverseBinder<double>, IBinder
```

#### Type Parameters

`TTarget` 

The type of the target object that exposes the bound property.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<TTarget\>](Aspid.MVVM.TargetBinder-1.md) ← 
[TargetBinder\<TTarget, int\>](Aspid.MVVM.StarterKit.TargetBinder-2.md) ← 
[TargetIntBinder\<TTarget\>](Aspid.MVVM.StarterKit.TargetIntBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IIntBinder](Aspid.MVVM.StarterKit.IIntBinder.md), 
[INumberBinder](Aspid.MVVM.StarterKit.INumberBinder.md), 
[IBinder\<int\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<uint\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<long\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<ulong\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<byte\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<sbyte\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<short\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<ushort\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<float\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<double\>](Aspid.MVVM.IBinder-1.md), 
[INumberReverseBinder](Aspid.MVVM.StarterKit.INumberReverseBinder.md), 
[IReverseBinder\<int\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<long\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<float\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<double\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<TargetIntBinder\<TTarget\>\>\(TargetIntBinder\<TTarget\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<TargetIntBinder\<TTarget\>\>\(TargetIntBinder\<TTarget\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<TargetIntBinder\<TTarget\>\>\(TargetIntBinder\<TTarget\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### TargetIntBinder\(\) {#Aspid_MVVM_StarterKit_TargetIntBinder_1__ctor}

```csharp
protected TargetIntBinder()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### TargetIntBinder\(TTarget, IConverter\<int, int\>?, BindMode\) {#Aspid_MVVM_StarterKit_TargetIntBinder_1__ctor__0_Aspid_MVVM_StarterKit_IConverter_System_Int32_System_Int32__Aspid_MVVM_BindMode_}

```csharp
protected TargetIntBinder(TTarget target, IConverter<int, int>? converter, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TTarget

The target object that exposes the property.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<[int](https://learn.microsoft.com/dotnet/api/system.int32), [int](https://learn.microsoft.com/dotnet/api/system.int32)\>?

The converter applied before the value is written, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it unchanged.
Runs in reverse only if it implements [`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">target</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### SendInitialValueToSource\(\) {#Aspid_MVVM_StarterKit_TargetIntBinder_1_SendInitialValueToSource}

Sends the current value on every numeric channel.

```csharp
protected override void SendInitialValueToSource()
```

#### Remarks

Also calls the base implementation: [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) for the property's own type resolves to
the class-level [`TargetBinder<T1, T2>.ValueChanged`](Aspid.MVVM.StarterKit.TargetBinder-2.md#Aspid_MVVM_StarterKit_TargetBinder_2_ValueChanged), not to the channel.

