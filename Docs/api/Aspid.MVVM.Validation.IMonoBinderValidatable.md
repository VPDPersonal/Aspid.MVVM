---
title: "Interface IMonoBinderValidatable"
sidebar_label: "IMonoBinderValidatable"
description: "Interface IMonoBinderValidatable — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IMonoBinderValidatable {#Aspid_MVVM_Validation_IMonoBinderValidatable}

Namespace: [Aspid.MVVM.Validation](Aspid.MVVM.Validation.md)  
Assembly: Aspid.MVVM.Unity.dll  

Editor-side view of a [`MonoBinder`](Aspid.MVVM.MonoBinder.md): the View and field ID it is wired to, with their last known values.

```csharp
public interface IMonoBinderValidatable : IBinder
```

#### Implements

[IBinder](Aspid.MVVM.IBinder.md)

#### Extension Methods

[BinderExtensions.BindSafely\<IMonoBinderValidatable\>\(IMonoBinderValidatable?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<IMonoBinderValidatable\>\(IMonoBinderValidatable?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<IMonoBinderValidatable\>\(IMonoBinderValidatable?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Properties

### Id {#Aspid_MVVM_Validation_IMonoBinderValidatable_Id}

Gets the ID of the View field this binder is bound through.

```csharp
string Id { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### IsMonoAlive {#Aspid_MVVM_Validation_IMonoBinderValidatable_IsMonoAlive}

Indicates whether the underlying component still exists.

```csharp
bool IsMonoAlive { get; }
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

Gets the last non-empty View.

```csharp
MonoBinderPreviousView PreviousView { get; }
```

#### Property Value

 [MonoBinderPreviousView](Aspid.MVVM.Validation.MonoBinderPreviousView.md)

### View {#Aspid_MVVM_Validation_IMonoBinderValidatable_View}

Gets the View this binder belongs to, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

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

Whether the previous values are cleared as well.

### ResetId\(MonoBinderResetMode\) {#Aspid_MVVM_Validation_IMonoBinderValidatable_ResetId_Aspid_MVVM_Validation_MonoBinderResetMode_}

Clears [`IMonoBinderValidatable.Id`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_Id).

```csharp
void ResetId(MonoBinderResetMode mode = MonoBinderResetMode.Hard)
```

#### Parameters

`mode` [MonoBinderResetMode](Aspid.MVVM.Validation.MonoBinderResetMode.md)

Whether [`IMonoBinderValidatable.PreviousId`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_PreviousId) is cleared as well.

### ResetView\(MonoBinderResetMode\) {#Aspid_MVVM_Validation_IMonoBinderValidatable_ResetView_Aspid_MVVM_Validation_MonoBinderResetMode_}

Clears [`IMonoBinderValidatable.View`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_View).

```csharp
void ResetView(MonoBinderResetMode mode = MonoBinderResetMode.Hard)
```

#### Parameters

`mode` [MonoBinderResetMode](Aspid.MVVM.Validation.MonoBinderResetMode.md)

Whether [`IMonoBinderValidatable.PreviousView`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_PreviousView) is cleared as well.

### SetId\(string?\) {#Aspid_MVVM_Validation_IMonoBinderValidatable_SetId_System_String_}

Sets [`IMonoBinderValidatable.Id`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_Id); a blank value resets it.

```csharp
void SetId(string? id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The ID, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### SetView\(IView?\) {#Aspid_MVVM_Validation_IMonoBinderValidatable_SetView_Aspid_MVVM_IView_}

Sets [`IMonoBinderValidatable.View`](Aspid.MVVM.Validation.IMonoBinderValidatable.md#Aspid_MVVM_Validation_IMonoBinderValidatable_View); <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> resets it.

```csharp
void SetView(IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The View, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

