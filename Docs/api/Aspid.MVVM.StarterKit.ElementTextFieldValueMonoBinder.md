---
title: "Class ElementTextFieldValueMonoBinder"
sidebar_label: "ElementTextFieldValueMonoBinder"
description: "Class ElementTextFieldValueMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ElementTextFieldValueMonoBinder {#Aspid_MVVM_StarterKit_ElementTextFieldValueMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that binds the text of a [`TextField`](https://docs.unity3d.com/ScriptReference/UIElements-TextField.html) and reports
user changes back.

```csharp
[BindModeOverride(new BindMode[] { }, IsAll = true)]
[AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – TextField Value")]
[AddBinderContextMenuByType(typeof(string))]
public sealed class ElementTextFieldValueMonoBinder : VisualElementMonoBinder<TextField>, IMonoBinderValidatable, IBinder<string>, IReverseBinder<string>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[VisualElementMonoBinder\<TextField\>](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) ←
[ElementTextFieldValueMonoBinder](Aspid.MVVM.StarterKit.ElementTextFieldValueMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder\<string\>](Aspid.MVVM.IBinder-1.md),
[IReverseBinder\<string\>](Aspid.MVVM.IReverseBinder-1.md),
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

Writes raise the change event for other listeners; only the binder's own echo is suppressed.
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is written as an empty string.

## Methods

### ClearValue\(\) {#Aspid_MVVM_StarterKit_ElementTextFieldValueMonoBinder_ClearValue}

Called when a binding ends or starts to release its retained value.

```csharp
protected override void ClearValue()
```

### OnBound\(\) {#Aspid_MVVM_StarterKit_ElementTextFieldValueMonoBinder_OnBound}

Called with the binding complete and [`MonoBinder.IsBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_IsBound) set. Override to notify or send an initial value.

```csharp
protected override void OnBound()
```

#### Remarks

Exceptions do not roll back the binding. This hook may unbind and bind again; finish resource changes before invoking external code.

### OnElementAttached\(TextField\) {#Aspid_MVVM_StarterKit_ElementTextFieldValueMonoBinder_OnElementAttached_UnityEngine_UIElements_TextField_}

Called when an element joins the active binding. Override to subscribe to input.

```csharp
protected override void OnElementAttached(TextField element)
```

#### Parameters

`element` TextField

The resolved element.

### OnElementDetached\(TextField\) {#Aspid_MVVM_StarterKit_ElementTextFieldValueMonoBinder_OnElementDetached_UnityEngine_UIElements_TextField_}

Called before an element leaves the binding. Override to release its callbacks.

```csharp
protected override void OnElementDetached(TextField element)
```

#### Parameters

`element` TextField

The previous element.

### RestoreElement\(\) {#Aspid_MVVM_StarterKit_ElementTextFieldValueMonoBinder_RestoreElement}

Called when a replacement element needs the last bound value.

```csharp
protected override void RestoreElement()
```

### SetValue\(string\) {#Aspid_MVVM_StarterKit_ElementTextFieldValueMonoBinder_SetValue_System_String_}

Sets the value without reporting the write back to the ViewModel.

```csharp
public void SetValue(string value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

The value received from the ViewModel.

### ValueChanged {#Aspid_MVVM_StarterKit_ElementTextFieldValueMonoBinder_ValueChanged}

Occurs when the View produces a value for the ViewModel.

```csharp
public event Action<string> ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[string](https://learn.microsoft.com/dotnet/api/system.string)\>
