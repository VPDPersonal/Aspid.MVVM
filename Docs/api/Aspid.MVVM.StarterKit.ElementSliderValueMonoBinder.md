---
title: "Class ElementSliderValueMonoBinder"
sidebar_label: "ElementSliderValueMonoBinder"
description: "Class ElementSliderValueMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ElementSliderValueMonoBinder {#Aspid_MVVM_StarterKit_ElementSliderValueMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that binds [`value`](https://docs.unity3d.com/ScriptReference/UIElements-BaseSlider-value.html) of a
[`Slider`](https://docs.unity3d.com/ScriptReference/UIElements-Slider.html) and reports user changes back.

```csharp
[BindModeOverride(new BindMode[] { }, IsAll = true)]
[AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Slider Value")]
[AddBinderContextMenuByType(typeof(float))]
public sealed class ElementSliderValueMonoBinder : VisualElementMonoBinder<Slider>, IMonoBinderValidatable, IFloatBinder, INumberBinder, IBinder<int>, IBinder<uint>, IBinder<long>, IBinder<ulong>, IBinder<byte>, IBinder<sbyte>, IBinder<short>, IBinder<ushort>, IBinder<float>, IBinder<double>, INumberReverseBinder, IReverseBinder<int>, IReverseBinder<long>, IReverseBinder<float>, IReverseBinder<double>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[VisualElementMonoBinder\<Slider\>](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) ←
[ElementSliderValueMonoBinder](Aspid.MVVM.StarterKit.ElementSliderValueMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IFloatBinder](Aspid.MVVM.StarterKit.IFloatBinder.md),
[INumberBinder](Aspid.MVVM.StarterKit.INumberBinder.md),
[IBinder\<int\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<uint\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<long\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<ulong\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<byte\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<sbyte\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<short\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<ushort\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<float\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<double\>](Aspid.MVVM.IBinder-1.md),
[INumberReverseBinder](Aspid.MVVM.StarterKit.INumberReverseBinder.md),
[IReverseBinder\<int\>](Aspid.MVVM.IReverseBinder-1.md),
[IReverseBinder\<long\>](Aspid.MVVM.IReverseBinder-1.md),
[IReverseBinder\<float\>](Aspid.MVVM.IReverseBinder-1.md),
[IReverseBinder\<double\>](Aspid.MVVM.IReverseBinder-1.md),
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

Writes raise the change event for other listeners; only the binder's own echo is suppressed. A non-finite
value is refused.

## Methods

### ClearValue\(\) {#Aspid_MVVM_StarterKit_ElementSliderValueMonoBinder_ClearValue}

Called when a binding ends or starts to release its retained value.

```csharp
protected override void ClearValue()
```

### OnBound\(\) {#Aspid_MVVM_StarterKit_ElementSliderValueMonoBinder_OnBound}

Called with the binding complete and [`MonoBinder.IsBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_IsBound) set. Override to notify or send an initial value.

```csharp
protected override void OnBound()
```

#### Remarks

Exceptions do not roll back the binding. This hook may unbind and bind again; finish resource changes before invoking external code.

### OnElementAttached\(Slider\) {#Aspid_MVVM_StarterKit_ElementSliderValueMonoBinder_OnElementAttached_UnityEngine_UIElements_Slider_}

Called when an element joins the active binding. Override to subscribe to input.

```csharp
protected override void OnElementAttached(Slider element)
```

#### Parameters

`element` Slider

The resolved element.

### OnElementDetached\(Slider\) {#Aspid_MVVM_StarterKit_ElementSliderValueMonoBinder_OnElementDetached_UnityEngine_UIElements_Slider_}

Called before an element leaves the binding. Override to release its callbacks.

```csharp
protected override void OnElementDetached(Slider element)
```

#### Parameters

`element` Slider

The previous element.

### RestoreElement\(\) {#Aspid_MVVM_StarterKit_ElementSliderValueMonoBinder_RestoreElement}

Called when a replacement element needs the last bound value.

```csharp
protected override void RestoreElement()
```

### SetValue\(float\) {#Aspid_MVVM_StarterKit_ElementSliderValueMonoBinder_SetValue_System_Single_}

Sets the value without reporting the write back to the ViewModel.

```csharp
public void SetValue(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value received from the ViewModel.
