---
title: "Interface IMonoBinderValidatable"
sidebar_label: "IMonoBinderValidatable"
description: "Interface IMonoBinderValidatable — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IMonoBinderValidatable {#Aspid_MVVM_Validation_IMonoBinderValidatable}

Namespace: [Aspid.MVVM.Validation](Aspid.MVVM.Validation.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

[`IBinder`](Aspid.MVVM.IBinder.md) with Editor validation of its View, field identifier and previous assignments.

```csharp
public interface IMonoBinderValidatable : IBinder
```

#### Implements

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

## Properties

### Id {#Aspid_MVVM_Validation_IMonoBinderValidatable_Id}

Gets the binder field ID. Returns an empty string when unassigned.

```csharp
string Id { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### IsAlive {#Aspid_MVVM_Validation_IMonoBinderValidatable_IsAlive}

Gets a value indicating whether the underlying component exists and has not begun destruction.

```csharp
bool IsAlive { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### PreviousId {#Aspid_MVVM_Validation_IMonoBinderValidatable_PreviousId}

Gets the last non-empty ID.

```csharp
MonoBinderPreviousId PreviousId { get; }
```

#### Property Value

 [MonoBinderPreviousId](Aspid.MVVM.Validation.MonoBinderPreviousId.md)

### PreviousView {#Aspid_MVVM_Validation_IMonoBinderValidatable_PreviousView}

Gets the last assigned view.

```csharp
MonoBinderPreviousView PreviousView { get; }
```

#### Property Value

 [MonoBinderPreviousView](Aspid.MVVM.Validation.MonoBinderPreviousView.md)

### View {#Aspid_MVVM_Validation_IMonoBinderValidatable_View}

Gets the assigned component view, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when missing or destroyed.

```csharp
IView? View { get; }
```

#### Property Value

 [IView](Aspid.MVVM.IView.md)?

## Methods

### Reset\(MonoBinderResetMode\) {#Aspid_MVVM_Validation_IMonoBinderValidatable_Reset_Aspid_MVVM_Validation_MonoBinderResetMode_}

Clears both [`IMonoBinderValidatable.Id`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_Id) and [`IMonoBinderValidatable.View`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_View).

```csharp
void Reset(MonoBinderResetMode mode = MonoBinderResetMode.Hard)
```

#### Parameters

`mode` [MonoBinderResetMode](Aspid.MVVM.Validation.MonoBinderResetMode.md)

Use [`MonoBinderResetMode.Hard`](Aspid.MVVM.Validation.MonoBinderResetMode.md) to also clear previous values.

### ResetId\(MonoBinderResetMode\) {#Aspid_MVVM_Validation_IMonoBinderValidatable_ResetId_Aspid_MVVM_Validation_MonoBinderResetMode_}

Clears [`IMonoBinderValidatable.Id`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_Id).

```csharp
void ResetId(MonoBinderResetMode mode = MonoBinderResetMode.Hard)
```

#### Parameters

`mode` [MonoBinderResetMode](Aspid.MVVM.Validation.MonoBinderResetMode.md)

Use [`MonoBinderResetMode.Hard`](Aspid.MVVM.Validation.MonoBinderResetMode.md) to also clear [`IMonoBinderValidatable.PreviousId`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_PreviousId).

### ResetView\(MonoBinderResetMode\) {#Aspid_MVVM_Validation_IMonoBinderValidatable_ResetView_Aspid_MVVM_Validation_MonoBinderResetMode_}

Clears [`IMonoBinderValidatable.View`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_View).

```csharp
void ResetView(MonoBinderResetMode mode = MonoBinderResetMode.Hard)
```

#### Parameters

`mode` [MonoBinderResetMode](Aspid.MVVM.Validation.MonoBinderResetMode.md)

Use [`MonoBinderResetMode.Hard`](Aspid.MVVM.Validation.MonoBinderResetMode.md) to also clear [`IMonoBinderValidatable.PreviousView`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_PreviousView).

### SetId\(string?\) {#Aspid_MVVM_Validation_IMonoBinderValidatable_SetId_System_String_}

Sets the binder field ID used for validation.

```csharp
void SetId(string? id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The ID to assign, or a blank value to reset it.

### SetView\(IView?\) {#Aspid_MVVM_Validation_IMonoBinderValidatable_SetView_Aspid_MVVM_IView_}

Sets the view used for validation.

```csharp
void SetView(IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The component view to assign. A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed view clears the assignment and its history.
