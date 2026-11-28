---
title: "Class ScrollRectCommandMonoBinder<T1, T2>"
sidebar_label: "ScrollRectCommandMonoBinder<T1, T2>"
description: "Class ScrollRectCommandMonoBinder<T1, T2> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ScrollRectCommandMonoBinder\<T1, T2\> {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with the normalized position and [`ScrollRectCommandMonoBinder<T1, T2>.Param1`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_Param1),
[`ScrollRectCommandMonoBinder<T1, T2>.Param2`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-2.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_Param2).

```csharp
public abstract class ScrollRectCommandMonoBinder<T1, T2> : ComponentMonoBinder<ScrollRect>, IMonoBinderValidatable, IBinder<IRelayCommand<Vector2, T1, T2>>, IBinder<IRelayCommand<Vector3, T1, T2>>, IBinder
```

#### Type Parameters

`T1`

The type of the first extra parameter.

`T2`

The type of the second extra parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[ComponentMonoBinder\<ScrollRect\>](Aspid.MVVM.ComponentMonoBinder-1.md) ←
[ScrollRectCommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-2.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder\<IRelayCommand\<Vector2, T1, T2\>\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<IRelayCommand\<Vector3, T1, T2\>\>](Aspid.MVVM.IBinder-1.md),
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

Accepts [`IRelayCommand<T1, T2, T3>`](Aspid.MVVM.IRelayCommand-3.md) with a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) or [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) position.

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### IsDebug {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Param1 {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_Param1}

Gets or sets the extra parameter passed after the position.

```csharp
public virtual T1 Param1 { get; set; }
```

#### Property Value

 T1

### Param2 {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_Param2}

Gets or sets the extra parameter passed after the position.

```csharp
public virtual T2 Param2 { get; set; }
```

#### Property Value

 T2

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### OnAttach\(\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### OnValidate\(\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_OnValidate}

Called when Unity validates serialized data to refresh the command state.

```csharp
protected virtual void OnValidate()
```

### SetValue\(IRelayCommand\<Vector2, T1, T2\>\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_Vector2__0__1__}

```csharp
public void SetValue(IRelayCommand<Vector2, T1, T2> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<Vector2, T1, T2\>

### SetValue\(IRelayCommand\<Vector3, T1, T2\>\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_2_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_Vector3__0__1__}

```csharp
public void SetValue(IRelayCommand<Vector3, T1, T2> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<Vector3, T1, T2\>
