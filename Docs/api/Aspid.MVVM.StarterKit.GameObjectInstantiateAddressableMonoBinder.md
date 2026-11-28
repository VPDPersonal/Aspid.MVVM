---
title: "Class GameObjectInstantiateAddressableMonoBinder"
sidebar_label: "GameObjectInstantiateAddressableMonoBinder"
description: "Class GameObjectInstantiateAddressableMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class GameObjectInstantiateAddressableMonoBinder {#Aspid_MVVM_StarterKit_GameObjectInstantiateAddressableMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`AddressableMonoBinder<T>`](Aspid.MVVM.StarterKit.AddressableMonoBinder-1.md) that instantiates the loaded prefab into a container, replacing
the previous instance.

```csharp
[AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Instantiate Addressable")]
[AddBinderContextMenu(typeof(Component), new string[] { }, Path = "Add General Binder/GameObject/GameObject Binder – Instantiate Addressable")]
public sealed class GameObjectInstantiateAddressableMonoBinder : AddressableMonoBinder<GameObject>, IMonoBinderValidatable, IBinder<string>, IBinder<IKeyEvaluator>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[AddressableMonoBinder\<GameObject\>](Aspid.MVVM.StarterKit.AddressableMonoBinder-1.md) ←
[GameObjectInstantiateAddressableMonoBinder](Aspid.MVVM.StarterKit.GameObjectInstantiateAddressableMonoBinder.md)

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

## Methods

### GetDefaultAsset\(\) {#Aspid_MVVM_StarterKit_GameObjectInstantiateAddressableMonoBinder_GetDefaultAsset}

Returns the asset applied when no key is bound. The default is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/default">default</a>.

```csharp
protected override GameObject GetDefaultAsset()
```

#### Returns

 GameObject

The default asset.

### ReleaseAsset\(\) {#Aspid_MVVM_StarterKit_GameObjectInstantiateAddressableMonoBinder_ReleaseAsset}

Called during destruction before loaded handles are released. Override to release owned instances.

```csharp
protected override void ReleaseAsset()
```

### Reset\(\) {#Aspid_MVVM_StarterKit_GameObjectInstantiateAddressableMonoBinder_Reset}

Called when Unity adds or resets the component to apply [`MonoBinder.DefaultMode`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_DefaultMode).

```csharp
protected override void Reset()
```

#### Remarks

Overrides must call the base implementation.

### SetAsset\(GameObject\) {#Aspid_MVVM_StarterKit_GameObjectInstantiateAddressableMonoBinder_SetAsset_UnityEngine_GameObject_}

Applies <code class="paramref">asset</code> to the target.

```csharp
protected override void SetAsset(GameObject prefab)
```

#### Parameters

`prefab` GameObject
