---
title: "Class ScrollRectCommandMonoBinder<T1, T2, T3>"
sidebar_label: "ScrollRectCommandMonoBinder<T1, T2, T3>"
description: "Class ScrollRectCommandMonoBinder<T1, T2, T3> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ScrollRectCommandMonoBinder\<T1, T2, T3\> {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with the normalized position and [`ScrollRectCommandMonoBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_Param1),
[`ScrollRectCommandMonoBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_Param2), [`ScrollRectCommandMonoBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_Param3).

```csharp
public abstract class ScrollRectCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<ScrollRect>, IMonoBinderValidatable, IBinder<IRelayCommand<Vector2, T1, T2, T3>>, IBinder<IRelayCommand<Vector3, T1, T2, T3>>, IBinder
```

#### Type Parameters

`T1`

The type of the first extra parameter.

`T2`

The type of the second extra parameter.

`T3`

The type of the third extra parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[ComponentMonoBinder\<ScrollRect\>](Aspid.MVVM.ComponentMonoBinder-1.md) ←
[ScrollRectCommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.ScrollRectCommandMonoBinder-3.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder\<IRelayCommand\<Vector2, T1, T2, T3\>\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<IRelayCommand\<Vector3, T1, T2, T3\>\>](Aspid.MVVM.IBinder-1.md),
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ScrollRectCommandMonoBinder\<T1, T2, T3\>\>\(ScrollRectCommandMonoBinder\<T1, T2, T3\>?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_),
[BinderExtensions.BindSafely\<ScrollRectCommandMonoBinder\<T1, T2, T3\>\>\(ScrollRectCommandMonoBinder\<T1, T2, T3\>?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_),
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
[BinderExtensions.UnbindSafely\<ScrollRectCommandMonoBinder\<T1, T2, T3\>\>\(ScrollRectCommandMonoBinder\<T1, T2, T3\>?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Remarks

Accepts [`IRelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.IRelayCommand-4.md) with a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) or [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html)
position.

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### IsDebug {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Param1 {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_Param1}

Gets or sets the extra parameter passed after the position.

```csharp
public virtual T1 Param1 { get; set; }
```

#### Property Value

 T1

### Param2 {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_Param2}

Gets or sets the extra parameter passed after the position.

```csharp
public virtual T2 Param2 { get; set; }
```

#### Property Value

 T2

### Param3 {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_Param3}

Gets or sets the extra parameter passed after the position.

```csharp
public virtual T3 Param3 { get; set; }
```

#### Property Value

 T3

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### OnAttach\(\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### OnValidate\(\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_OnValidate}

Called when Unity validates serialized data to fill an empty target field outside Play Mode.

```csharp
protected override void OnValidate()
```

#### Remarks

Overrides must call <code>base.OnValidate()</code>.

### SetValue\(IRelayCommand\<Vector2, T1, T2, T3\>\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_Vector2__0__1__2__}

```csharp
public void SetValue(IRelayCommand<Vector2, T1, T2, T3> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<Vector2, T1, T2, T3\>

### SetValue\(IRelayCommand\<Vector3, T1, T2, T3\>\) {#Aspid_MVVM_StarterKit_ScrollRectCommandMonoBinder_3_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_Vector3__0__1__2__}

```csharp
public void SetValue(IRelayCommand<Vector3, T1, T2, T3> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<Vector3, T1, T2, T3\>
