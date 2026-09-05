---
title: "Class EventTriggerCommandMonoBinder<T1, T2, T3>"
sidebar_label: "EventTriggerCommandMonoBinder<T1, T2, T3>"
description: "Class EventTriggerCommandMonoBinder<T1, T2, T3> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EventTriggerCommandMonoBinder\<T1, T2, T3\> {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command when the selected
[`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event fires with [`EventTriggerCommandMonoBinder<T1, T2, T3>.Param1`](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_Param1), [`EventTriggerCommandMonoBinder<T1, T2, T3>.Param2`](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_Param2), [`EventTriggerCommandMonoBinder<T1, T2, T3>.Param3`](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-3.md#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_Param3).

```csharp
public abstract class EventTriggerCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<EventTrigger>, IMonoBinderValidatable, IRebindableBinder, IBinder<IRelayCommand<T1, T2, T3>>, IBinder<IRelayCommand<BaseEventData, T1, T2, T3>>, IBinder<IRelayCommand<EventTriggerType, T1, T2, T3>>, IBinder
```

#### Type Parameters

`T1` 

The type of the first parameter.

`T2` 

The type of the second parameter.

`T3` 

The type of the third parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<EventTrigger\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[EventTriggerCommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder-3.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IRelayCommand\<T1, T2, T3\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<BaseEventData, T1, T2, T3\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<EventTriggerType, T1, T2, T3\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<EventTriggerCommandMonoBinder\<T1, T2, T3\>\>\(EventTriggerCommandMonoBinder\<T1, T2, T3\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<EventTriggerCommandMonoBinder\<T1, T2, T3\>\>\(EventTriggerCommandMonoBinder\<T1, T2, T3\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<EventTriggerCommandMonoBinder\<T1, T2, T3\>\>\(EventTriggerCommandMonoBinder\<T1, T2, T3\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Accepts a plain command, one that receives the [`BaseEventData`](https://docs.unity3d.com/ScriptReference/EventSystems-BaseEventData.html), or one that receives the
[`EventTriggerType`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTriggerType.html) as its first argument.

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### IsDebug {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Param1 {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_Param1}

Gets or sets the parameter passed to the command.

```csharp
public virtual T1 Param1 { get; set; }
```

#### Property Value

 T1

### Param2 {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_Param2}

Gets or sets the parameter passed to the command.

```csharp
public virtual T2 Param2 { get; set; }
```

#### Property Value

 T2

### Param3 {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_Param3}

Gets or sets the parameter passed to the command.

```csharp
public virtual T3 Param3 { get; set; }
```

#### Property Value

 T3

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### OnBound\(\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_OnBound}

Called after binding is established and the first value is applied. Override to subscribe to the component.

```csharp
protected override void OnBound()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_OnUnbound}

Called after unbinding. Override to release a subscription taken in [`MonoBinder.OnBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBound).

```csharp
protected override void OnUnbound()
```

### OnValidate\(\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_OnValidate}

Called by Unity in the Editor when a serialized value changes. Fills the empty component field outside Play mode.

```csharp
protected override void OnValidate()
```

#### Remarks

When overriding, always call <code>base.OnValidate()</code>.

### SetValue\(IRelayCommand\<T1, T2, T3\>\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_SetValue_Aspid_MVVM_IRelayCommand__0__1__2__}

```csharp
public void SetValue(IRelayCommand<T1, T2, T3> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1, T2, T3\>

### SetValue\(IRelayCommand\<BaseEventData, T1, T2, T3\>\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_EventSystems_BaseEventData__0__1__2__}

```csharp
public void SetValue(IRelayCommand<BaseEventData, T1, T2, T3> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<BaseEventData, T1, T2, T3\>

### SetValue\(IRelayCommand\<EventTriggerType, T1, T2, T3\>\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_3_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_EventSystems_EventTriggerType__0__1__2__}

```csharp
public void SetValue(IRelayCommand<EventTriggerType, T1, T2, T3> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<EventTriggerType, T1, T2, T3\>

