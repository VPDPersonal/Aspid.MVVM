---
title: "Class VisualElementMonoBinder<TElement>"
sidebar_label: "VisualElementMonoBinder<TElement>"
description: "Class VisualElementMonoBinder<TElement> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VisualElementMonoBinder\<TElement\> {#Aspid_MVVM_StarterKit_VisualElementMonoBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

Abstract [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that resolves one <code class="typeparamref">TElement</code> in a
[`UIDocument`](https://docs.unity3d.com/ScriptReference/UIElements-UIDocument.html) by name or USS class.

```csharp
public abstract class VisualElementMonoBinder<TElement> : MonoBinder, IMonoBinderValidatable, IBinder where TElement : VisualElement
```

#### Type Parameters

`TElement`

The type of element this binder drives.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[VisualElementMonoBinder\<TElement\>](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<VisualElementMonoBinder\<TElement\>\>\(VisualElementMonoBinder\<TElement\>?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_),
[BinderExtensions.BindSafely\<VisualElementMonoBinder\<TElement\>\>\(VisualElementMonoBinder\<TElement\>?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_),
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
[BinderExtensions.UnbindSafely\<VisualElementMonoBinder\<TElement\>\>\(VisualElementMonoBinder\<TElement\>?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Remarks

The element is resolved lazily, since the document builds its tree in <code>OnEnable</code>; a failed lookup is
reported and retried on the next access.

## Properties

### Element {#Aspid_MVVM_StarterKit_VisualElementMonoBinder_1_Element}

Gets the resolved element, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it cannot be found.

```csharp
protected TElement Element { get; }
```

#### Property Value

 TElement

## Methods

### ClearValue\(\) {#Aspid_MVVM_StarterKit_VisualElementMonoBinder_1_ClearValue}

Called when a binding ends or starts to release its retained value.

```csharp
protected virtual void ClearValue()
```

### OnAttach\(\) {#Aspid_MVVM_StarterKit_VisualElementMonoBinder_1_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnBinding\(\) {#Aspid_MVVM_StarterKit_VisualElementMonoBinder_1_OnBinding}

Called before attachment and the first value. Override to prepare for that value.

```csharp
protected override void OnBinding()
```

#### Remarks

Subscribe to the component in [`MonoBinder.OnAttach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnAttach) so the first value is not reported back as user input.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_VisualElementMonoBinder_1_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### OnElementAttached\(TElement\) {#Aspid_MVVM_StarterKit_VisualElementMonoBinder_1_OnElementAttached__0_}

Called when an element joins the active binding. Override to subscribe to input.

```csharp
protected virtual void OnElementAttached(TElement element)
```

#### Parameters

`element` TElement

The resolved element.

### OnElementDetached\(TElement\) {#Aspid_MVVM_StarterKit_VisualElementMonoBinder_1_OnElementDetached__0_}

Called before an element leaves the binding. Override to release its callbacks.

```csharp
protected virtual void OnElementDetached(TElement element)
```

#### Parameters

`element` TElement

The previous element.

### ResolveForWrite\(\) {#Aspid_MVVM_StarterKit_VisualElementMonoBinder_1_ResolveForWrite}

Resolves the current element before a new value is applied, without replaying the previous value.

```csharp
protected TElement ResolveForWrite()
```

#### Returns

 TElement

The current element, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> before its tree exists.

### RestoreElement\(\) {#Aspid_MVVM_StarterKit_VisualElementMonoBinder_1_RestoreElement}

Called when a replacement element needs the last bound value.

```csharp
protected virtual void RestoreElement()
```
