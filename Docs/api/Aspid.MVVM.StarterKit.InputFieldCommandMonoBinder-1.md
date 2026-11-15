---
title: "Class InputFieldCommandMonoBinder<T>"
sidebar_label: "InputFieldCommandMonoBinder<T>"
description: "Class InputFieldCommandMonoBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class InputFieldCommandMonoBinder\<T\> {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on the selected field event with the text
and [`InputFieldCommandMonoBinder<T>.Param`](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-1.md#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1_Param).

```csharp
public abstract class InputFieldCommandMonoBinder<T> : ComponentMonoBinder<TMP_InputField>, IMonoBinderValidatable, IBinder<IRelayCommand<string, T>>, IBinder
```

#### Type Parameters

`T` 

The type of the extra parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<TMP\_InputField\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[InputFieldCommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IBinder\<IRelayCommand\<string, T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<InputFieldCommandMonoBinder\<T\>\>\(InputFieldCommandMonoBinder\<T\>?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<InputFieldCommandMonoBinder\<T\>\>\(InputFieldCommandMonoBinder\<T\>?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_), 
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
[BinderExtensions.UnbindSafely\<InputFieldCommandMonoBinder\<T\>\>\(InputFieldCommandMonoBinder\<T\>?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### IsDebug {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Param {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1_Param}

Gets or sets the extra parameter passed after the text.

```csharp
public virtual T Param { get; set; }
```

#### Property Value

 T

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### OnAttach\(\) {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### OnValidate\(\) {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1_OnValidate}

Re-subscribes to the selected event after an Inspector change in Play mode.

```csharp
protected override void OnValidate()
```

#### Remarks

Runs only while bound, so the listener is never stacked or left behind.

### SetValue\(IRelayCommand\<string, T\>\) {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_1_SetValue_Aspid_MVVM_IRelayCommand_System_String__0__}

```csharp
public void SetValue(IRelayCommand<string, T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<[string](https://learn.microsoft.com/dotnet/api/system.string), T\>

