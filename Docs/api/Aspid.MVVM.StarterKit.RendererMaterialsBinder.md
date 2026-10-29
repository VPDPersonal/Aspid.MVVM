---
title: "Class RendererMaterialsBinder"
sidebar_label: "RendererMaterialsBinder"
description: "Class RendererMaterialsBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RendererMaterialsBinder {#Aspid_MVVM_StarterKit_RendererMaterialsBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds [`material`](https://docs.unity3d.com/ScriptReference/Renderer-material.html) or
[`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html).

```csharp
[Serializable]
public sealed class RendererMaterialsBinder : TargetBinder<Renderer>, IRebindableBinder, IBinder<Material>, IBinder<IReadOnlyCollection<Material>>, IReverseBinder<Material>, IReverseBinder<Material[]>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<Renderer\>](Aspid.MVVM.TargetBinder-1.md) ← 
[RendererMaterialsBinder](Aspid.MVVM.StarterKit.RendererMaterialsBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<Material\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IReadOnlyCollection\<Material\>\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<Material\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<Material\[\]\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<RendererMaterialsBinder\>\(RendererMaterialsBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<RendererMaterialsBinder\>\(RendererMaterialsBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<RendererMaterialsBinder\>\(RendererMaterialsBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

In [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) the shared materials are reported, so no instance copies are
created.

## Constructors

### RendererMaterialsBinder\(Renderer, IConverter\<Material, Material\>, BindMode\) {#Aspid_MVVM_StarterKit_RendererMaterialsBinder__ctor_UnityEngine_Renderer_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Material_UnityEngine_Material__Aspid_MVVM_BindMode_}

```csharp
public RendererMaterialsBinder(Renderer target, IConverter<Material, Material> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Renderer

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Material, Material\>

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_RendererMaterialsBinder_OnBound}

Called after the binder attaches and receives the first value. Override to subscribe to the component.

```csharp
protected override void OnBound()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_RendererMaterialsBinder_OnUnbound}

Called after the binder detaches. Override to release what [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) subscribed to.

```csharp
protected override void OnUnbound()
```

### SetValue\(Material\) {#Aspid_MVVM_StarterKit_RendererMaterialsBinder_SetValue_UnityEngine_Material_}

Applies <code class="paramref">value</code> to the View.

```csharp
public void SetValue(Material value)
```

#### Parameters

`value` Material

The value received from the ViewModel.

### SetValue\(IReadOnlyCollection\<Material\>\) {#Aspid_MVVM_StarterKit_RendererMaterialsBinder_SetValue_System_Collections_Generic_IReadOnlyCollection_UnityEngine_Material__}

Applies <code class="paramref">value</code> to the View.

```csharp
public void SetValue(IReadOnlyCollection<Material> values)
```

#### Parameters

`values` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<Material\>

