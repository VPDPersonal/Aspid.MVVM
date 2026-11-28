---
title: "Class ComponentObjectMonoBinder<TComponent, TObject>"
sidebar_label: "ComponentObjectMonoBinder<TComponent, TObject>"
description: "Class ComponentObjectMonoBinder<TComponent, TObject> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ComponentObjectMonoBinder\<TComponent, TObject\> {#Aspid_MVVM_StarterKit_ComponentObjectMonoBinder_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

Abstract base [`ComponentMonoBinder<TComponent, TObject>`](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) that binds
a [`UnityEngine.Object`](https://docs.unity3d.com/ScriptReference/Object.html) reference, normalizing destroyed references to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> in both directions.

```csharp
public abstract class ComponentObjectMonoBinder<TComponent, TObject> : ComponentMonoBinder<TComponent, TObject>, IMonoBinderValidatable, IBinder<TObject>, IReverseBinder<TObject>, IBinder where TComponent : Component where TObject : Object
```

#### Type Parameters

`TComponent`

The type of [`Component`](https://docs.unity3d.com/ScriptReference/Component.html) that exposes the bound property.

`TObject`

The type of [`UnityEngine.Object`](https://docs.unity3d.com/ScriptReference/Object.html) the property holds.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md) ←
[ComponentMonoBinder\<TComponent\>](Aspid.MVVM.ComponentMonoBinder-1.md) ←
[ComponentMonoBinder\<TComponent, TObject\>](Aspid.MVVM.StarterKit.ComponentMonoBinder-2.md) ←
[ComponentObjectMonoBinder\<TComponent, TObject\>](Aspid.MVVM.StarterKit.ComponentObjectMonoBinder-2.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder\<TObject\>](Aspid.MVVM.IBinder-1.md),
[IReverseBinder\<TObject\>](Aspid.MVVM.IReverseBinder-1.md),
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

### GetConvertedBackValue\(TObject\) {#Aspid_MVVM_StarterKit_ComponentObjectMonoBinder_2_GetConvertedBackValue__1_}

Converts <code class="paramref">value</code> for the ViewModel; unchanged unless the converter implements [`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).

```csharp
protected override TObject GetConvertedBackValue(TObject value)
```

#### Parameters

`value` TObject

The value to convert.

#### Returns

 TObject

The converted value.

### GetConvertedValue\(TObject\) {#Aspid_MVVM_StarterKit_ComponentObjectMonoBinder_2_GetConvertedValue__1_}

Converts <code class="paramref">value</code> with the serialized converter, or returns it unchanged when none is set.

```csharp
protected override TObject GetConvertedValue(TObject value)
```

#### Parameters

`value` TObject

The value to convert.

#### Returns

 TObject

The converted value.
