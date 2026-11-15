---
title: "Class InputFieldCommandMonoBinder"
sidebar_label: "InputFieldCommandMonoBinder"
description: "Class InputFieldCommandMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class InputFieldCommandMonoBinder {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that executes a command on the selected field event with the text.

```csharp
[AddBinderContextMenu(typeof(TMP_InputField), new string[] { "m_Calls" })]
[AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – Command")]
public sealed class InputFieldCommandMonoBinder : ComponentMonoBinder<TMP_InputField>, IMonoBinderValidatable, IBinder<IRelayCommand>, IBinder<IRelayCommand<string>>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<TMP\_InputField\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[InputFieldCommandMonoBinder](Aspid.MVVM.StarterKit.InputFieldCommandMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IBinder\<IRelayCommand\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IRelayCommand\<string\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<InputFieldCommandMonoBinder\>\(InputFieldCommandMonoBinder?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<InputFieldCommandMonoBinder\>\(InputFieldCommandMonoBinder?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_), 
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
[BinderExtensions.UnbindSafely\<InputFieldCommandMonoBinder\>\(InputFieldCommandMonoBinder?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Remarks

Accepts [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) and [`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) with a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types">string</a>.

## Methods

### OnAttach\(\) {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### OnValidate\(\) {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_OnValidate}

Re-subscribes to the selected event after an Inspector change in Play mode.

```csharp
protected override void OnValidate()
```

#### Remarks

Runs only while bound, so the listener is never stacked or left behind.

### SetValue\(IRelayCommand\) {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_SetValue_Aspid_MVVM_IRelayCommand_}

```csharp
public void SetValue(IRelayCommand value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

### SetValue\(IRelayCommand\<string\>\) {#Aspid_MVVM_StarterKit_InputFieldCommandMonoBinder_SetValue_Aspid_MVVM_IRelayCommand_System_String__}

```csharp
public void SetValue(IRelayCommand<string> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

