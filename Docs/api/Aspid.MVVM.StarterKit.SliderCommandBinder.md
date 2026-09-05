---
title: "Class SliderCommandBinder"
sidebar_label: "SliderCommandBinder"
description: "Class SliderCommandBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SliderCommandBinder {#Aspid_MVVM_StarterKit_SliderCommandBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with
the slider value.

```csharp
[Serializable]
public sealed class SliderCommandBinder : TargetBinder<Slider>, IRebindableBinder, IBinder<IRelayCommand<int>>, IBinder<IRelayCommand<long>>, IBinder<IRelayCommand<float>>, IBinder<IRelayCommand<double>>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<Slider\>](Aspid.MVVM.TargetBinder-1.md) ← 
[SliderCommandBinder](Aspid.MVVM.StarterKit.SliderCommandBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IRelayCommand\<int\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<long\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<float\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<double\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<SliderCommandBinder\>\(SliderCommandBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<SliderCommandBinder\>\(SliderCommandBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<SliderCommandBinder\>\(SliderCommandBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Accepts [`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) with an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a>, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">long</a>,
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">double</a> value; integers are truncated.

## Constructors

### SliderCommandBinder\(Slider, BindMode\) {#Aspid_MVVM_StarterKit_SliderCommandBinder__ctor_UnityEngine_UI_Slider_Aspid_MVVM_BindMode_}

```csharp
public SliderCommandBinder(Slider target, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Slider

The slider to bind.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

### SliderCommandBinder\(Slider, ICanExecuteHandler, BindMode\) {#Aspid_MVVM_StarterKit_SliderCommandBinder__ctor_UnityEngine_UI_Slider_Aspid_MVVM_StarterKit_ICanExecuteHandler_Aspid_MVVM_BindMode_}

```csharp
public SliderCommandBinder(Slider target, ICanExecuteHandler customInteractable, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Slider

The slider to bind.

`customInteractable` [ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)

The handler that reflects the command's CanExecute.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">customInteractable</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### SliderCommandBinder\(Slider, InteractableMode, BindMode\) {#Aspid_MVVM_StarterKit_SliderCommandBinder__ctor_UnityEngine_UI_Slider_Aspid_MVVM_StarterKit_InteractableMode_Aspid_MVVM_BindMode_}

```csharp
public SliderCommandBinder(Slider target, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Slider

The slider to bind.

`interactableMode` [InteractableMode](Aspid.MVVM.StarterKit.InteractableMode.md)

How the command's CanExecute is reflected on the slider; not [`InteractableMode.Custom`](Aspid.MVVM.StarterKit.InteractableMode.md).

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">interactableMode</code> is [`InteractableMode.Custom`](Aspid.MVVM.StarterKit.InteractableMode.md).

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_OnBound}

Called after binding is established. Override to add post-binding logic.

```csharp
protected override void OnBound()
```

#### Remarks

Runs after the ViewModel's first value has been applied and after [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. This is where a binder subscribes to its component — see
[`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding) for why the earlier hook is the wrong place.

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_OnUnbound}

Called after unbinding. Override to add post-unbinding logic.

```csharp
protected override void OnUnbound()
```

#### Remarks

Runs once the binder is detached and [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
This is where a subscription taken in [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) is released.

### SetValue\(IRelayCommand\<int\>\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_SetValue_Aspid_MVVM_IRelayCommand_System_Int32__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<int> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<[int](https://learn.microsoft.com/dotnet/api/system.int32)\>

The value received from the ViewModel.

### SetValue\(IRelayCommand\<long\>\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_SetValue_Aspid_MVVM_IRelayCommand_System_Int64__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<long> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<[long](https://learn.microsoft.com/dotnet/api/system.int64)\>

The value received from the ViewModel.

### SetValue\(IRelayCommand\<float\>\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_SetValue_Aspid_MVVM_IRelayCommand_System_Single__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<float> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<[float](https://learn.microsoft.com/dotnet/api/system.single)\>

The value received from the ViewModel.

### SetValue\(IRelayCommand\<double\>\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_SetValue_Aspid_MVVM_IRelayCommand_System_Double__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<double> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<[double](https://learn.microsoft.com/dotnet/api/system.double)\>

The value received from the ViewModel.

