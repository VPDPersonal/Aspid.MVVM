---
title: "Class RendererMaterialsMonoBinder"
sidebar_label: "RendererMaterialsMonoBinder"
description: "Class RendererMaterialsMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RendererMaterialsMonoBinder {#Aspid_MVVM_StarterKit_RendererMaterialsMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds [`material`](https://docs.unity3d.com/ScriptReference/Renderer-material.html) or
[`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html).

```csharp
[BindModeOverride(new BindMode[] { BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource })]
[AddBinderContextMenu(typeof(Renderer), new string[] { "m_Materials" })]
[AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials")]
public sealed class RendererMaterialsMonoBinder : ComponentMonoBinder<Renderer>, IMonoBinderValidatable, IRebindableBinder, IBinder<Material>, IBinder<IReadOnlyCollection<Material>>, IReverseBinder<Material>, IReverseBinder<Material[]>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ComponentMonoBinder\<Renderer\>](Aspid.MVVM.ComponentMonoBinder-1.md) ← 
[RendererMaterialsMonoBinder](Aspid.MVVM.StarterKit.RendererMaterialsMonoBinder.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<Material\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IReadOnlyCollection\<Material\>\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<Material\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<Material\[\]\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<RendererMaterialsMonoBinder\>\(RendererMaterialsMonoBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<RendererMaterialsMonoBinder\>\(RendererMaterialsMonoBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_), 
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[RebindableBinderExtensions.Rebind\(IBinder\)](Aspid.MVVM.RebindableBinderExtensions.md#Aspid_MVVM_RebindableBinderExtensions_Rebind_Aspid_MVVM_IBinder_), 
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_), 
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_), 
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderExtensions.UnbindSafely\<RendererMaterialsMonoBinder\>\(RendererMaterialsMonoBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

In [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) the shared materials are reported, so no instance copies are
created.

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_RendererMaterialsMonoBinder_OnBound}

Called after binding is established and the first value is applied. Override to subscribe to the component.

```csharp
protected override void OnBound()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_RendererMaterialsMonoBinder_OnUnbound}

Called after unbinding. Override to release a subscription taken in [`MonoBinder.OnBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBound).

```csharp
protected override void OnUnbound()
```

### SetValue\(Material\) {#Aspid_MVVM_StarterKit_RendererMaterialsMonoBinder_SetValue_UnityEngine_Material_}

Sets [`material`](https://docs.unity3d.com/ScriptReference/Renderer-material.html).

```csharp
public void SetValue(Material value)
```

#### Parameters

`value` Material

The material received from the ViewModel.

### SetValue\(IReadOnlyCollection\<Material\>\) {#Aspid_MVVM_StarterKit_RendererMaterialsMonoBinder_SetValue_System_Collections_Generic_IReadOnlyCollection_UnityEngine_Material__}

Sets [`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html); <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or empty clears the array.

```csharp
public void SetValue(IReadOnlyCollection<Material> values)
```

#### Parameters

`values` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<Material\>

The materials received from the ViewModel.

