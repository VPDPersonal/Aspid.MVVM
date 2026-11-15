---
title: "Class RendererPropertyBlockMonoBinder<TValue>"
sidebar_label: "RendererPropertyBlockMonoBinder<TValue>"
description: "Class RendererPropertyBlockMonoBinder<TValue> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RendererPropertyBlockMonoBinder\<TValue\> {#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that writes one shader property through a
[`MaterialPropertyBlock`](https://docs.unity3d.com/ScriptReference/MaterialPropertyBlock.html).

```csharp
public abstract class RendererPropertyBlockMonoBinder<TValue> : ComponentMonoBinder<Renderer>, IMonoBinderValidatable, IBinder<TValue>, IBinder
```

#### Type Parameters

`TValue` 

The type of value written to the shader property.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Renderer\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[RendererPropertyBlockMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IBinder\<TValue\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<RendererPropertyBlockMonoBinder\<TValue\>\>\(RendererPropertyBlockMonoBinder\<TValue\>?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<RendererPropertyBlockMonoBinder\<TValue\>\>\(RendererPropertyBlockMonoBinder\<TValue\>?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_), 
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
[BinderExtensions.UnbindSafely\<RendererPropertyBlockMonoBinder\<TValue\>\>\(RendererPropertyBlockMonoBinder\<TValue\>?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Remarks

The property name is resolved once, on binding. A blank name is reported and disables writes until the
next bind.

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### Block {#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_Block}

Gets the block values are written into.

```csharp
protected MaterialPropertyBlock Block { get; }
```

#### Property Value

 MaterialPropertyBlock

### IsDebug {#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### PropertyId {#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_PropertyId}

Gets the id the property name resolved to.

```csharp
protected int PropertyId { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### OnAttach\(\) {#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected override void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### SetValue\(TValue\) {#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_SetValue__0_}

Writes <code class="paramref">value</code> into the block and applies the block to the renderer.

```csharp
public void SetValue(TValue value)
```

#### Parameters

`value` TValue

The value received from the ViewModel.

### Write\(TValue\) {#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_Write__0_}

Writes <code class="paramref">value</code> into [`RendererPropertyBlockMonoBinder<T>.Block`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_Block) under [`RendererPropertyBlockMonoBinder<T>.PropertyId`](Aspid.MVVM.StarterKit.RendererPropertyBlockMonoBinder-1.md#Aspid_MVVM_StarterKit_RendererPropertyBlockMonoBinder_1_PropertyId).

```csharp
protected abstract void Write(TValue value)
```

#### Parameters

`value` TValue

The value to write.

