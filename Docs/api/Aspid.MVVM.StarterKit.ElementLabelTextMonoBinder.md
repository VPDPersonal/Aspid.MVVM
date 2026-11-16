---
title: "Class ElementLabelTextMonoBinder"
sidebar_label: "ElementLabelTextMonoBinder"
description: "Class ElementLabelTextMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ElementLabelTextMonoBinder {#Aspid_MVVM_StarterKit_ElementLabelTextMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`VisualElementMonoBinder<T>`](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) that binds [`text`](https://docs.unity3d.com/ScriptReference/UIElements-TextElement-text.html) of a
[`Label`](https://docs.unity3d.com/ScriptReference/UIElements-Label.html) from any value.

```csharp
[BindModeOverride(new BindMode[] { BindMode.OneWay, BindMode.OneTime })]
[AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Label Text")]
[AddBinderContextMenu(typeof(Component), new string[] { }, Path = "Add General Binder/UIToolkit/Element Binder – Label Text")]
public sealed class ElementLabelTextMonoBinder : VisualElementMonoBinder<Label>, IMonoBinderValidatable, IAnyBinder, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[VisualElementMonoBinder\<Label\>](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md) ←
[ElementLabelTextMonoBinder](Aspid.MVVM.StarterKit.ElementLabelTextMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IAnyBinder](Aspid.MVVM.IAnyBinder.md),
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ElementLabelTextMonoBinder\>\(ElementLabelTextMonoBinder?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_),
[BinderExtensions.BindSafely\<ElementLabelTextMonoBinder\>\(ElementLabelTextMonoBinder?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_),
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
[BinderExtensions.UnbindSafely\<ElementLabelTextMonoBinder\>\(ElementLabelTextMonoBinder?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Remarks

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is written as an empty string.

## Methods

### ClearValue\(\) {#Aspid_MVVM_StarterKit_ElementLabelTextMonoBinder_ClearValue}

Called when a binding ends or starts to release its retained value.

```csharp
protected override void ClearValue()
```

### RestoreElement\(\) {#Aspid_MVVM_StarterKit_ElementLabelTextMonoBinder_RestoreElement}

Called when a replacement element needs the last bound value.

```csharp
protected override void RestoreElement()
```

### SetValue\<T\>\(T\) {#Aspid_MVVM_StarterKit_ElementLabelTextMonoBinder_SetValue__1___0_}

```csharp
public void SetValue<T>(T value)
```

#### Parameters

`value` T

#### Type Parameters

`T`
