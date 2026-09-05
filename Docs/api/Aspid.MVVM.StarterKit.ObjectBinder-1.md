---
title: "Class ObjectBinder<TObject>"
sidebar_label: "ObjectBinder<TObject>"
description: "Class ObjectBinder<TObject> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObjectBinder\<TObject\> {#Aspid_MVVM_StarterKit_ObjectBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`StarterKit.Binder<T>?text=Binder%3cTObject%3e`](Aspid.MVVM.StarterKit.md) that binds a [`Object`](https://docs.unity3d.com/ScriptReference/Object?text=UnityEngine-Object.html)
reference, normalizing destroyed references to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> in both directions.

```csharp
[Serializable]
public abstract class ObjectBinder<TObject> : Binder<TObject>, IRebindableBinder, IBinder<TObject>, IReverseBinder<TObject>, IBinder where TObject : Object
```

#### Type Parameters

`TObject` 

The type of [`Object`](https://docs.unity3d.com/ScriptReference/Object?text=UnityEngine-Object.html) the property holds.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[Binder\<TObject\>](Aspid.MVVM.StarterKit.Binder-1.md) ← 
[ObjectBinder\<TObject\>](Aspid.MVVM.StarterKit.ObjectBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<TObject\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<TObject\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ObjectBinder\<TObject\>\>\(ObjectBinder\<TObject\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ObjectBinder\<TObject\>\>\(ObjectBinder\<TObject\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ObjectBinder\<TObject\>\>\(ObjectBinder\<TObject\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### ObjectBinder\(\) {#Aspid_MVVM_StarterKit_ObjectBinder_1__ctor}

```csharp
protected ObjectBinder()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### ObjectBinder\(IConverter\<TObject?, TObject?\>?, BindMode\) {#Aspid_MVVM_StarterKit_ObjectBinder_1__ctor_Aspid_MVVM_StarterKit_IConverter__0__0__Aspid_MVVM_BindMode_}

```csharp
protected ObjectBinder(IConverter<TObject?, TObject?>? converter, BindMode mode = BindMode.OneWay)
```

#### Parameters

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<TObject?, TObject?\>?

The converter applied before the value is written, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it unchanged.
Runs in reverse only if it implements [`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

## Methods

### GetConvertedBackValue\(TObject?\) {#Aspid_MVVM_StarterKit_ObjectBinder_1_GetConvertedBackValue__0_}

Converts <code class="paramref">value</code> for the ViewModel; unchanged unless the converter implements [`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).

```csharp
protected override TObject? GetConvertedBackValue(TObject? value)
```

#### Parameters

`value` TObject?

The value to convert.

#### Returns

 TObject?

The converted value.

### GetConvertedValue\(TObject?\) {#Aspid_MVVM_StarterKit_ObjectBinder_1_GetConvertedValue__0_}

Converts <code class="paramref">value</code> with the serialized converter, or returns it unchanged when none is set.

```csharp
protected override TObject? GetConvertedValue(TObject? value)
```

#### Parameters

`value` TObject?

The value to convert.

#### Returns

 TObject?

The converted value.

