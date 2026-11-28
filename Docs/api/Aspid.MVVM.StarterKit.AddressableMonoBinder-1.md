---
title: "Class AddressableMonoBinder<TAsset>"
sidebar_label: "AddressableMonoBinder<TAsset>"
description: "Class AddressableMonoBinder<TAsset> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AddressableMonoBinder\<TAsset\> {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that loads an Addressable asset by key or [`IKeyEvaluator`](https://docs.unity3d.com/ScriptReference/AddressableAssets-IKeyEvaluator.html)
and applies it once loaded. An empty key applies [`AddressableMonoBinder<T>.GetDefaultAsset`](Aspid.MVVM.StarterKit.AddressableMonoBinder-1.md#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_GetDefaultAsset).

```csharp
public abstract class AddressableMonoBinder<TAsset> : MonoBinder, IMonoBinderValidatable, IBinder<string>, IBinder<IKeyEvaluator>, IBinder
```

#### Type Parameters

`TAsset`

The type of asset to load.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[AddressableMonoBinder\<TAsset\>](Aspid.MVVM.StarterKit.AddressableMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder\<string\>](Aspid.MVVM.IBinder-1.md),
[IBinder\<IKeyEvaluator\>](Aspid.MVVM.IBinder-1.md),
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

Available only with <code>ASPID_MVVM_ADDRESSABLES_INTEGRATION</code>.

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### IsDebug {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### GetDefaultAsset\(\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_GetDefaultAsset}

Returns the asset applied when no key is bound. The default is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/default">default</a>.

```csharp
protected virtual TAsset GetDefaultAsset()
```

#### Returns

 TAsset

The default asset.

### OnDestroy\(\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_OnDestroy}

Called when Unity destroys the component to release its binding.

```csharp
protected override void OnDestroy()
```

#### Remarks

Overrides must call the base implementation.

### OnDetach\(\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected override void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### ReleaseAsset\(\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_ReleaseAsset}

Called during destruction before loaded handles are released. Override to release owned instances.

```csharp
protected virtual void ReleaseAsset()
```

### SetAsset\(TAsset\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_SetAsset__0_}

Applies <code class="paramref">asset</code> to the target.

```csharp
protected abstract void SetAsset(TAsset asset)
```

#### Parameters

`asset` TAsset

The loaded asset, or the default one.

### SetValue\(string\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_SetValue_System_String_}

Loads the asset at <code class="paramref">value</code>, or applies the default asset when the key is empty.

```csharp
public void SetValue(string value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

The Addressable address.

### SetValue\(IKeyEvaluator\) {#Aspid_MVVM_StarterKit_AddressableMonoBinder_1_SetValue_UnityEngine_AddressableAssets_IKeyEvaluator_}

Loads the asset behind <code class="paramref">value</code>, or applies the default asset when the key is empty.

```csharp
public void SetValue(IKeyEvaluator value)
```

#### Parameters

`value` IKeyEvaluator

The evaluator providing the Addressable runtime key.
