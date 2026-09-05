---
title: "Class EventTriggerCommandBinder<T>"
sidebar_label: "EventTriggerCommandBinder<T>"
description: "Class EventTriggerCommandBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EventTriggerCommandBinder\<T\> {#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command when the selected [`EventTrigger`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTrigger.html) event
fires with [`EventTriggerCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-1.md#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_1_Param).

```csharp
[Serializable]
public class EventTriggerCommandBinder<T> : TargetBinder<EventTrigger>, IRebindableBinder, IBinder<IRelayCommand<T>>, IBinder<IRelayCommand<BaseEventData, T>>, IBinder<IRelayCommand<EventTriggerType, T>>, IBinder
```

#### Type Parameters

`T` 

The type of the parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<EventTrigger\>](Aspid.MVVM.TargetBinder-1.md) ← 
[EventTriggerCommandBinder\<T\>](Aspid.MVVM.StarterKit.EventTriggerCommandBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IRelayCommand\<T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<BaseEventData, T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<EventTriggerType, T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<EventTriggerCommandBinder\<T\>\>\(EventTriggerCommandBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<EventTriggerCommandBinder\<T\>\>\(EventTriggerCommandBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<EventTriggerCommandBinder\<T\>\>\(EventTriggerCommandBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Accepts a plain command, one that receives the [`BaseEventData`](https://docs.unity3d.com/ScriptReference/EventSystems-BaseEventData.html), or one that receives the
[`EventTriggerType`](https://docs.unity3d.com/ScriptReference/EventSystems-EventTriggerType.html) as its first argument.

## Constructors

### EventTriggerCommandBinder\(EventTrigger, EventTriggerType, T, ICanExecuteHandler, BindMode\) {#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_1__ctor_UnityEngine_EventSystems_EventTrigger_UnityEngine_EventSystems_EventTriggerType__0_Aspid_MVVM_StarterKit_ICanExecuteHandler_Aspid_MVVM_BindMode_}

```csharp
public EventTriggerCommandBinder(EventTrigger target, EventTriggerType eventType, T param, ICanExecuteHandler customInteractable = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` EventTrigger

The event trigger to bind.

`eventType` EventTriggerType

The event that executes the command.

`param` T

The parameter passed to the command.

`customInteractable` [ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)

The handler that reflects the command's CanExecute, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

## Properties

### Param {#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_1_Param}

Gets or sets the parameter passed to the command.

```csharp
public virtual T Param { get; set; }
```

#### Property Value

 T

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_1_OnBound}

Called after binding is established. Override to add post-binding logic.

```csharp
protected override void OnBound()
```

#### Remarks

Runs after the ViewModel's first value has been applied and after [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. This is where a binder subscribes to its component — see
[`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding) for why the earlier hook is the wrong place.

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_1_OnUnbound}

Called after unbinding. Override to add post-unbinding logic.

```csharp
protected override void OnUnbound()
```

#### Remarks

Runs once the binder is detached and [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
This is where a subscription taken in [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) is released.

### SetValue\(IRelayCommand\<T\>\) {#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_1_SetValue_Aspid_MVVM_IRelayCommand__0__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>

The value received from the ViewModel.

### SetValue\(IRelayCommand\<BaseEventData, T\>\) {#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_1_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_EventSystems_BaseEventData__0__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<BaseEventData, T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<BaseEventData, T\>

The value received from the ViewModel.

### SetValue\(IRelayCommand\<EventTriggerType, T\>\) {#Aspid_MVVM_StarterKit_EventTriggerCommandBinder_1_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_EventSystems_EventTriggerType__0__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<EventTriggerType, T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<EventTriggerType, T\>

The value received from the ViewModel.

