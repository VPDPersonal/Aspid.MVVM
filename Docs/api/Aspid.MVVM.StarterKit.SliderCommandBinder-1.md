---
title: "Class SliderCommandBinder<T>"
sidebar_label: "SliderCommandBinder<T>"
description: "Class SliderCommandBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SliderCommandBinder\<T\> {#Aspid_MVVM_StarterKit_SliderCommandBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that executes a command on [`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Slider-onValueChanged.html) with
the slider value and [`SliderCommandBinder<T>.Param`](Aspid.MVVM.StarterKit.SliderCommandBinder-1.md#Aspid_MVVM_StarterKit_SliderCommandBinder_1_Param).

```csharp
[Serializable]
public class SliderCommandBinder<T> : TargetBinder<Slider>, IRebindableBinder, IBinder<IRelayCommand<int, T>>, IBinder<IRelayCommand<long, T>>, IBinder<IRelayCommand<float, T>>, IBinder<IRelayCommand<double, T>>, IBinder
```

#### Type Parameters

`T` 

The type of the extra parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<Slider\>](Aspid.MVVM.TargetBinder-1.md) ← 
[SliderCommandBinder\<T\>](Aspid.MVVM.StarterKit.SliderCommandBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IRelayCommand\<int, T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<long, T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<float, T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<double, T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<SliderCommandBinder\<T\>\>\(SliderCommandBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<SliderCommandBinder\<T\>\>\(SliderCommandBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<SliderCommandBinder\<T\>\>\(SliderCommandBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

Accepts [`IRelayCommand<T1, T2>`](Aspid.MVVM.IRelayCommand-2.md) with an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a>, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">long</a>,
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">double</a> value; integers are truncated.

## Constructors

### SliderCommandBinder\(Slider, T, BindMode\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_1__ctor_UnityEngine_UI_Slider__0_Aspid_MVVM_BindMode_}

```csharp
public SliderCommandBinder(Slider target, T param, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Slider

The slider to bind.

`param` T

The extra parameter passed after the slider value.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

### SliderCommandBinder\(Slider, T, ICanExecuteHandler, BindMode\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_1__ctor_UnityEngine_UI_Slider__0_Aspid_MVVM_StarterKit_ICanExecuteHandler_Aspid_MVVM_BindMode_}

```csharp
public SliderCommandBinder(Slider target, T param, ICanExecuteHandler customInteractable, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Slider

The slider to bind.

`param` T

The extra parameter passed after the slider value.

`customInteractable` [ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)

The handler that reflects the command's CanExecute.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">customInteractable</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### SliderCommandBinder\(Slider, T, InteractableMode, BindMode\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_1__ctor_UnityEngine_UI_Slider__0_Aspid_MVVM_StarterKit_InteractableMode_Aspid_MVVM_BindMode_}

```csharp
public SliderCommandBinder(Slider target, T param, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Slider

The slider to bind.

`param` T

The extra parameter passed after the slider value.

`interactableMode` [InteractableMode](Aspid.MVVM.StarterKit.InteractableMode.md)

How the command's CanExecute is reflected on the slider; not [`InteractableMode.Custom`](Aspid.MVVM.StarterKit.InteractableMode.md).

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">interactableMode</code> is [`InteractableMode.Custom`](Aspid.MVVM.StarterKit.InteractableMode.md).

## Properties

### Param {#Aspid_MVVM_StarterKit_SliderCommandBinder_1_Param}

Gets or sets the extra parameter passed after the slider value.

```csharp
public virtual T Param { get; set; }
```

#### Property Value

 T

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_1_OnBound}

Called after binding is established. Override to add post-binding logic.

```csharp
protected override void OnBound()
```

#### Remarks

Runs after the ViewModel's first value has been applied and after [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. This is where a binder subscribes to its component — see
[`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding) for why the earlier hook is the wrong place.

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_1_OnUnbound}

Called after unbinding. Override to add post-unbinding logic.

```csharp
protected override void OnUnbound()
```

#### Remarks

Runs once the binder is detached and [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
This is where a subscription taken in [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) is released.

### SetValue\(IRelayCommand\<int, T\>\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_1_SetValue_Aspid_MVVM_IRelayCommand_System_Int32__0__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<int, T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<[int](https://learn.microsoft.com/dotnet/api/system.int32), T\>

The value received from the ViewModel.

### SetValue\(IRelayCommand\<long, T\>\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_1_SetValue_Aspid_MVVM_IRelayCommand_System_Int64__0__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<long, T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<[long](https://learn.microsoft.com/dotnet/api/system.int64), T\>

The value received from the ViewModel.

### SetValue\(IRelayCommand\<float, T\>\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_1_SetValue_Aspid_MVVM_IRelayCommand_System_Single__0__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<float, T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<[float](https://learn.microsoft.com/dotnet/api/system.single), T\>

The value received from the ViewModel.

### SetValue\(IRelayCommand\<double, T\>\) {#Aspid_MVVM_StarterKit_SliderCommandBinder_1_SetValue_Aspid_MVVM_IRelayCommand_System_Double__0__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IRelayCommand<double, T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<[double](https://learn.microsoft.com/dotnet/api/system.double), T\>

The value received from the ViewModel.

