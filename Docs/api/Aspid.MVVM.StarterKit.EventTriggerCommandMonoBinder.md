---
title: "Class EventTriggerCommandMonoBinder"
sidebar_label: "EventTriggerCommandMonoBinder"
description: "Class EventTriggerCommandMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EventTriggerCommandMonoBinder {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command when the selected
[`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event fires.

```csharp
[AddBinderContextMenu(typeof(EventTrigger), new string[] { })]
[AddComponentMenu("Aspid/MVVM/Binders/UI/EventTrigger/EventTrigger Binder – Command")]
public sealed class EventTriggerCommandMonoBinder : ComponentMonoBinder<EventTrigger>, IMonoBinderValidatable, IBinder<IRelayCommand>, IBinder<IRelayCommand<BaseEventData>>, IBinder<IRelayCommand<EventTriggerType>>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<EventTrigger\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[EventTriggerCommandMonoBinder](Aspid.MVVM.StarterKit.EventTriggerCommandMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IBinder\<IRelayCommand\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<BaseEventData\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<EventTriggerType\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<EventTriggerCommandMonoBinder\>\(EventTriggerCommandMonoBinder?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_),
[BinderExtensions.BindSafely\<EventTriggerCommandMonoBinder\>\(EventTriggerCommandMonoBinder?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_),
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
[BinderExtensions.UnbindSafely\<EventTriggerCommandMonoBinder\>\(EventTriggerCommandMonoBinder?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Remarks

Accepts a plain command, one that receives the [`BaseEventData`](https://docs.unity3d.com/ScriptReference/EventSystems-BaseEventData.html), or one that receives the
[`EventTriggerType`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTriggerType.html) as its first argument.

## Methods

### OnAttach\(\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### OnValidate\(\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_OnValidate}

Called by Unity in the Editor when a serialized value changes. Fills the empty component field outside Play mode.

```csharp
protected override void OnValidate()
```

#### Remarks

When overriding, always call <code>base.OnValidate()</code>.

### SetValue\(IRelayCommand\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_SetValue_Aspid_MVVM_IRelayCommand_}

```csharp
public void SetValue(IRelayCommand value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

### SetValue\(IRelayCommand\<BaseEventData\>\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_EventSystems_BaseEventData__}

```csharp
public void SetValue(IRelayCommand<BaseEventData> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<BaseEventData\>

### SetValue\(IRelayCommand\<EventTriggerType\>\) {#Aspid_MVVM_StarterKit_EventTriggerCommandMonoBinder_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_EventSystems_EventTriggerType__}

```csharp
public void SetValue(IRelayCommand<EventTriggerType> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<EventTriggerType\>

