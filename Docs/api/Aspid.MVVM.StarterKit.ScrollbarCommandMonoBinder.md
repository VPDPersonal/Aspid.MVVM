---
title: "Class ScrollbarCommandMonoBinder"
sidebar_label: "ScrollbarCommandMonoBinder"
description: "Class ScrollbarCommandMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ScrollbarCommandMonoBinder {#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on
[`onValueChanged`](https://docs.unity3d.com/ScriptReference/UI-Scrollbar-onValueChanged.html) with the scrollbar value.

```csharp
[AddBinderContextMenu(typeof(Scrollbar), new string[] { "m_Calls" })]
[AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Command")]
public sealed class ScrollbarCommandMonoBinder : ComponentMonoBinder<Scrollbar>, IMonoBinderValidatable, IBinder<IRelayCommand<int>>, IBinder<IRelayCommand<long>>, IBinder<IRelayCommand<float>>, IBinder<IRelayCommand<double>>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Scrollbar\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[ScrollbarCommandMonoBinder](Aspid.MVVM.StarterKit.ScrollbarCommandMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IBinder\<IRelayCommand\<int\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<long\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<float\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<double\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ScrollbarCommandMonoBinder\>\(ScrollbarCommandMonoBinder?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ScrollbarCommandMonoBinder\>\(ScrollbarCommandMonoBinder?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_), 
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
[BinderExtensions.UnbindSafely\<ScrollbarCommandMonoBinder\>\(ScrollbarCommandMonoBinder?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Remarks

Accepts [`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) with an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a>, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">long</a>,
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a> or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">double</a> value; integers are truncated.

## Methods

### OnAttach\(\) {#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### OnValidate\(\) {#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_OnValidate}

Called by Unity in the Editor when a serialized value changes. Fills the empty component field outside Play mode.

```csharp
protected override void OnValidate()
```

#### Remarks

When overriding, always call <code>base.OnValidate()</code>.

### SetValue\(IRelayCommand\<int\>\) {#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_SetValue_Aspid_MVVM_IRelayCommand_System_Int32__}

```csharp
public void SetValue(IRelayCommand<int> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<[int](https://learn.microsoft.com/dotnet/api/system.int32)\>

### SetValue\(IRelayCommand\<long\>\) {#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_SetValue_Aspid_MVVM_IRelayCommand_System_Int64__}

```csharp
public void SetValue(IRelayCommand<long> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<[long](https://learn.microsoft.com/dotnet/api/system.int64)\>

### SetValue\(IRelayCommand\<float\>\) {#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_SetValue_Aspid_MVVM_IRelayCommand_System_Single__}

```csharp
public void SetValue(IRelayCommand<float> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<[float](https://learn.microsoft.com/dotnet/api/system.single)\>

### SetValue\(IRelayCommand\<double\>\) {#Aspid_MVVM_StarterKit_ScrollbarCommandMonoBinder_SetValue_Aspid_MVVM_IRelayCommand_System_Double__}

```csharp
public void SetValue(IRelayCommand<double> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<[double](https://learn.microsoft.com/dotnet/api/system.double)\>

