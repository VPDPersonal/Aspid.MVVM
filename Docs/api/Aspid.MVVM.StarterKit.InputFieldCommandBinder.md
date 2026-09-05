---
title: "Class InputFieldCommandBinder"
sidebar_label: "InputFieldCommandBinder"
description: "Class InputFieldCommandBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class InputFieldCommandBinder {#Aspid_MVVM_StarterKit_InputFieldCommandBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on the selected field event with the text.

```csharp
[Serializable]
public sealed class InputFieldCommandBinder : TargetBinder<TMP_InputField>, IRebindableBinder, IBinder<IRelayCommand>, IBinder<IRelayCommand<string>>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<TMP\_InputField\>](Aspid.MVVM.TargetBinder-1.md) ← 
[InputFieldCommandBinder](Aspid.MVVM.StarterKit.InputFieldCommandBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IRelayCommand\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<string\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<InputFieldCommandBinder\>\(InputFieldCommandBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<InputFieldCommandBinder\>\(InputFieldCommandBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<InputFieldCommandBinder\>\(InputFieldCommandBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Accepts [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) and [`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) with a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">string</a>.

## Constructors

### InputFieldCommandBinder\(TMP\_InputField, BindMode\) {#Aspid_MVVM_StarterKit_InputFieldCommandBinder__ctor_TMPro_TMP_InputField_Aspid_MVVM_BindMode_}

```csharp
public InputFieldCommandBinder(TMP_InputField target, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TMP\_InputField

The field to bind.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

### InputFieldCommandBinder\(TMP\_InputField, UpdateInputFieldEvent, BindMode\) {#Aspid_MVVM_StarterKit_InputFieldCommandBinder__ctor_TMPro_TMP_InputField_Aspid_MVVM_StarterKit_UpdateInputFieldEvent_Aspid_MVVM_BindMode_}

```csharp
public InputFieldCommandBinder(TMP_InputField target, UpdateInputFieldEvent updateEvent, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TMP\_InputField

The field to bind.

`updateEvent` [UpdateInputFieldEvent](Aspid.MVVM.StarterKit.UpdateInputFieldEvent.md)

The field event that executes the command.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

### InputFieldCommandBinder\(TMP\_InputField, ICanExecuteHandler, UpdateInputFieldEvent, BindMode\) {#Aspid_MVVM_StarterKit_InputFieldCommandBinder__ctor_TMPro_TMP_InputField_Aspid_MVVM_StarterKit_ICanExecuteHandler_Aspid_MVVM_StarterKit_UpdateInputFieldEvent_Aspid_MVVM_BindMode_}

```csharp
public InputFieldCommandBinder(TMP_InputField target, ICanExecuteHandler customInteractable, UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TMP\_InputField

The field to bind.

`customInteractable` [ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)

The handler that reflects the command's CanExecute.

`updateEvent` [UpdateInputFieldEvent](Aspid.MVVM.StarterKit.UpdateInputFieldEvent.md)

The field event that executes the command.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">customInteractable</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InputFieldCommandBinder\(TMP\_InputField, InteractableMode, UpdateInputFieldEvent, BindMode\) {#Aspid_MVVM_StarterKit_InputFieldCommandBinder__ctor_TMPro_TMP_InputField_Aspid_MVVM_StarterKit_InteractableMode_Aspid_MVVM_StarterKit_UpdateInputFieldEvent_Aspid_MVVM_BindMode_}

```csharp
public InputFieldCommandBinder(TMP_InputField target, InteractableMode interactableMode, UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TMP\_InputField

The field to bind.

`interactableMode` [InteractableMode](Aspid.MVVM.StarterKit.InteractableMode.md)

How the command's CanExecute is reflected on the field; not [`InteractableMode.Custom`](Aspid.MVVM.StarterKit.InteractableMode.md).

`updateEvent` [UpdateInputFieldEvent](Aspid.MVVM.StarterKit.UpdateInputFieldEvent.md)

The field event that executes the command.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">interactableMode</code> is [`InteractableMode.Custom`](Aspid.MVVM.StarterKit.InteractableMode.md).

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_InputFieldCommandBinder_OnBound}

Called after binding is established. Override to add post-binding logic.

```csharp
protected override void OnBound()
```

#### Remarks

Runs after the ViewModel's first value has been applied and after [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. This is where a binder subscribes to its component — see
[`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding) for why the earlier hook is the wrong place.

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_InputFieldCommandBinder_OnUnbound}

Called after unbinding. Override to add post-unbinding logic.

```csharp
protected override void OnUnbound()
```

#### Remarks

Runs once the binder is detached and [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
This is where a subscription taken in [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) is released.

### SetValue\(IRelayCommand\) {#Aspid_MVVM_StarterKit_InputFieldCommandBinder_SetValue_Aspid_MVVM_IRelayCommand_}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The value received from the ViewModel.

### SetValue\(IRelayCommand\<string\>\) {#Aspid_MVVM_StarterKit_InputFieldCommandBinder_SetValue_Aspid_MVVM_IRelayCommand_System_String__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<string> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The value received from the ViewModel.

