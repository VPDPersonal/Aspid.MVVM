---
title: "Class ScrollRectCommandBinder<T>"
sidebar_label: "ScrollRectCommandBinder<T>"
description: "Class ScrollRectCommandBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ScrollRectCommandBinder\<T\> {#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect-onValueChanged.html) with
the normalized position and [`ScrollRectCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-1.md#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_1_Param).

```csharp
[Serializable]
public class ScrollRectCommandBinder<T> : TargetBinder<ScrollRect>, IBinder<IRelayCommand<Vector2, T>>, IBinder<IRelayCommand<Vector3, T>>, IBinder
```

#### Type Parameters

`T`

The type of the extra parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Binder](Aspid.MVVM.Binder.md) ←
[TargetBinder\<ScrollRect\>](Aspid.MVVM.TargetBinder-1.md) ←
[ScrollRectCommandBinder\<T\>](Aspid.MVVM.StarterKit.ScrollRectCommandBinder-1.md)

#### Implements

[IBinder\<IRelayCommand\<Vector2, T\>\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<IRelayCommand\<Vector3, T\>\>](Aspid.MVVM.IBinder-1.md),
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

Accepts [`IRelayCommand<T1, T2>`](Aspid.MVVM.IRelayCommand-2.md) with a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) or [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) position.

## Constructors

### ScrollRectCommandBinder\(ScrollRect, T, BindMode\) {#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_1__ctor_UnityEngine_UI_ScrollRect__0_Aspid_MVVM_BindMode_}

```csharp
public ScrollRectCommandBinder(ScrollRect target, T param, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` ScrollRect

The scroll rect to bind.

`param` T

The extra parameter passed after the position.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

### ScrollRectCommandBinder\(ScrollRect, T, ICanExecuteHandler, BindMode\) {#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_1__ctor_UnityEngine_UI_ScrollRect__0_Aspid_MVVM_StarterKit_ICanExecuteHandler_Aspid_MVVM_BindMode_}

```csharp
public ScrollRectCommandBinder(ScrollRect target, T param, ICanExecuteHandler interactable, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` ScrollRect

The scroll rect to bind.

`param` T

The extra parameter passed after the position.

`interactable` [ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)

The handler that reflects the command's CanExecute.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">interactable</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Param {#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_1_Param}

Gets or sets the extra parameter passed after the position.

```csharp
public virtual T Param { get; set; }
```

#### Property Value

 T

## Methods

### OnAttach\(\) {#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_1_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`Binder.OnDetach`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnDetach) releases partially acquired resources.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_1_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### SetValue\(IRelayCommand\<Vector2, T\>\) {#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_1_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_Vector2__0__}

Applies <code class="paramref">value</code> to the View.

```csharp
public void SetValue(IRelayCommand<Vector2, T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<Vector2, T\>

The value received from the ViewModel.

### SetValue\(IRelayCommand\<Vector3, T\>\) {#Aspid_MVVM_StarterKit_ScrollRectCommandBinder_1_SetValue_Aspid_MVVM_IRelayCommand_UnityEngine_Vector3__0__}

Applies <code class="paramref">value</code> to the View.

```csharp
public void SetValue(IRelayCommand<Vector3, T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<Vector3, T\>

The value received from the ViewModel.
