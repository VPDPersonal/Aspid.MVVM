---
title: "Class MeshColliderMeshSwitcherBinder"
sidebar_label: "MeshColliderMeshSwitcherBinder"
description: "Class MeshColliderMeshSwitcherBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MeshColliderMeshSwitcherBinder {#Aspid_MVVM_StarterKit_MeshColliderMeshSwitcherBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches [`sharedMesh`](https://docs.unity3d.com/ScriptReference/MeshCollider-sharedMesh.html).

```csharp
[Serializable]
public sealed class MeshColliderMeshSwitcherBinder : SwitcherBinder<MeshCollider, Mesh>, IBinder<bool>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Binder](Aspid.MVVM.Binder.md) ←
[TargetBinder\<MeshCollider\>](Aspid.MVVM.TargetBinder-1.md) ←
[SwitcherBinder\<MeshCollider, Mesh\>](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) ←
[MeshColliderMeshSwitcherBinder](Aspid.MVVM.StarterKit.MeshColliderMeshSwitcherBinder.md)

#### Implements

[IBinder\<bool\>](Aspid.MVVM.IBinder-1.md),
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

## Constructors

### MeshColliderMeshSwitcherBinder\(MeshCollider, Mesh, Mesh, IConverter\<Mesh, Mesh\>, BindMode\) {#Aspid_MVVM_StarterKit_MeshColliderMeshSwitcherBinder__ctor_UnityEngine_MeshCollider_UnityEngine_Mesh_UnityEngine_Mesh_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Mesh_UnityEngine_Mesh__Aspid_MVVM_BindMode_}

```csharp
public MeshColliderMeshSwitcherBinder(MeshCollider target, Mesh trueValue = null, Mesh falseValue = null, IConverter<Mesh, Mesh> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` MeshCollider

The target object that receives the chosen value.

`trueValue` Mesh

The value applied when the bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

`falseValue` Mesh

The value applied when the bound <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">bool</a> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Mesh, Mesh\>

The converter applied to the chosen value, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it unchanged.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. Must be [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">target</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The mode is neither [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) nor [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

## Methods

### SetValue\(Mesh\) {#Aspid_MVVM_StarterKit_MeshColliderMeshSwitcherBinder_SetValue_UnityEngine_Mesh_}

Applies the chosen, converted <code class="paramref">value</code> to the target.

```csharp
protected override void SetValue(Mesh value)
```

#### Parameters

`value` Mesh

The value to apply.
