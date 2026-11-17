---
title: "Class AddressableMonoBinder<TAsset, TComponent>"
sidebar_label: "AddressableMonoBinder<TAsset, TComponent>"
description: "Class AddressableMonoBinder<TAsset, TComponent> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AddressableMonoBinder\<TAsset, TComponent\> {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that loads an Addressable asset by key or [`IKeyEvaluator`](https://docs.unity3d.com/ScriptReference/AddressableAssets-IKeyEvaluator.html)
and applies it to the component once loaded. An empty key applies [`AddressableMonoBinder<T1, T2>.GetDefaultAsset`](Aspid.MVVM.StarterKit.AddressableMonoBinder-2.md#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_GetDefaultAsset).

```csharp
public abstract class AddressableMonoBinder<TAsset, TComponent> : ComponentMonoBinder<TComponent>, IMonoBinderValidatable, IBinder<string>, IBinder<IKeyEvaluator>, IBinder where TComponent : Component
```

#### Type Parameters

`TAsset`

The type of asset to load.

`TComponent`

The type of [`Component`](https://docs.unity3d.com/ScriptReference/Component.html) that receives the asset.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[ComponentMonoBinder\<TComponent\>](Aspid.MVVM.ComponentMonoBinder-1.md) ←
[AddressableMonoBinder\<TAsset, TComponent\>](Aspid.MVVM.StarterKit.AddressableMonoBinder-2.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder\<string\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<IKeyEvaluator\>](Aspid.MVVM.IBinder-1.md),
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<AddressableMonoBinder\<TAsset, TComponent\>\>\(AddressableMonoBinder\<TAsset, TComponent\>?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_),
[BinderExtensions.BindSafely\<AddressableMonoBinder\<TAsset, TComponent\>\>\(AddressableMonoBinder\<TAsset, TComponent\>?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_),
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
[BinderExtensions.UnbindSafely\<AddressableMonoBinder\<TAsset, TComponent\>\>\(AddressableMonoBinder\<TAsset, TComponent\>?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

## Remarks

Available only with <code>ASPID_MVVM_ADDRESSABLES_INTEGRATION</code>.

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### IsDebug {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### GetDefaultAsset\(\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_GetDefaultAsset}

Returns the asset applied when no key is bound. The default is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/default">default</a>.

```csharp
protected virtual TAsset GetDefaultAsset()
```

#### Returns

 TAsset

The default asset.

### OnDestroy\(\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_OnDestroy}

Called when Unity destroys the component to release its binding.

```csharp
protected override void OnDestroy()
```

#### Remarks

Overrides must call <code>base.OnDestroy()</code>.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### ReleaseAsset\(\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_ReleaseAsset}

Called during destruction before loaded handles are released. Override to release owned instances.

```csharp
protected virtual void ReleaseAsset()
```

### SetAsset\(TAsset\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_SetAsset__0_}

Applies <code class="paramref">asset</code> to the component.

```csharp
protected abstract void SetAsset(TAsset asset)
```

#### Parameters

`asset` TAsset

The loaded asset, or the default one.

### SetValue\(string\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_SetValue_System_String_}

Loads the asset at <code class="paramref">value</code>, or applies the default asset when the key is empty.

```csharp
public void SetValue(string value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

The Addressable address.

### SetValue\(IKeyEvaluator\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_2_SetValue_UnityEngine_AddressableAssets_IKeyEvaluator_}

Loads the asset behind <code class="paramref">value</code>, or applies the default asset when the key is empty.

```csharp
public void SetValue(IKeyEvaluator value)
```

#### Parameters

`value` IKeyEvaluator

The evaluator providing the Addressable runtime key.
