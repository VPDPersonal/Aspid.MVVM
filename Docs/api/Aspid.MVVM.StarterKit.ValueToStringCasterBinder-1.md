---
title: "Class ValueToStringCasterBinder<T>"
sidebar_label: "ValueToStringCasterBinder<T>"
description: "Class ValueToStringCasterBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ValueToStringCasterBinder\<T\> {#Aspid_MVVM_StarterKit_ValueToStringCasterBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) that converts a <code class="typeparamref">T</code> value to a [`String`](https://learn.microsoft.com/dotnet/api/system.string)
and forwards it to a target setter.

```csharp
public sealed class ValueToStringCasterBinder<T> : Binder, IBinder<T>, IBinder
```

#### Type Parameters

`T`

The source value type produced by the ViewModel binding.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Binder](Aspid.MVVM.Binder.md) ←
[ValueToStringCasterBinder\<T\>](Aspid.MVVM.StarterKit.ValueToStringCasterBinder-1.md)

#### Implements

[IBinder\<T\>](Aspid.MVVM.IBinder-1.md),
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\(IBinder?, in BindContext\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_BindContext__),
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_),
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_),
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_),
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderExtensions.UnbindSafely\(IBinder?, in UnbindContext\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_UnbindContext__)

## Remarks

By default, uses [`ValueToStringConverter<T>`](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md) with the given format string.

## Constructors

### ValueToStringCasterBinder\(Action\<string?\>, string, BindMode\) {#Aspid_MVVM_StarterKit_ValueToStringCasterBinder_1__ctor_System_Action_System_String__System_String_Aspid_MVVM_BindMode_}

```csharp
public ValueToStringCasterBinder(Action<string?> setValue, string format, BindMode mode = BindMode.OneWay)
```

#### Parameters

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[string](https://learn.microsoft.com/dotnet/api/system.string)?\>

The action invoked with the converted [`String`](https://learn.microsoft.com/dotnet/api/system.string) value.

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

A composite format string passed to the default converter.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. Must be [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The mode is neither [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) nor [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

### ValueToStringCasterBinder\(Action\<string?\>, IConverter\<T?, string?\>, BindMode\) {#Aspid_MVVM_StarterKit_ValueToStringCasterBinder_1__ctor_System_Action_System_String__Aspid_MVVM_StarterKit_IConverter__0_System_String__Aspid_MVVM_BindMode_}

```csharp
public ValueToStringCasterBinder(Action<string?> setValue, IConverter<T?, string?> converter, BindMode mode = BindMode.OneWay)
```

#### Parameters

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[string](https://learn.microsoft.com/dotnet/api/system.string)?\>

The action invoked with the converted [`String`](https://learn.microsoft.com/dotnet/api/system.string) value.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, [string](https://learn.microsoft.com/dotnet/api/system.string)?\>

The converter used to transform a <code class="typeparamref">T</code> value to a [`String`](https://learn.microsoft.com/dotnet/api/system.string).

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. Must be [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">setValue</code> or <code class="paramref">converter</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The mode is neither [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) nor [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

## Methods

### SetValue\(T?\) {#Aspid_MVVM_StarterKit_ValueToStringCasterBinder_1_SetValue__0_}

Converts <code class="paramref">value</code> to a [`String`](https://learn.microsoft.com/dotnet/api/system.string) and forwards it to the target setter.

```csharp
public void SetValue(T? value)
```

#### Parameters

`value` T?

The value received from the ViewModel.
