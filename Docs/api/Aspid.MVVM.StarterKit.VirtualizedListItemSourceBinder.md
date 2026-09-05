---
title: "Class VirtualizedListItemSourceBinder"
sidebar_label: "VirtualizedListItemSourceBinder"
description: "Class VirtualizedListItemSourceBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VirtualizedListItemSourceBinder {#Aspid_MVVM_StarterKit_VirtualizedListItemSourceBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that sets [`VirtualizedList.ItemsSource`](Aspid.MVVM.StarterKit.VirtualizedList.md#Aspid_MVVM_StarterKit_VirtualizedList_ItemsSource)
to the bound list, optionally filtered and sorted.

```csharp
[Serializable]
public sealed class VirtualizedListItemSourceBinder : TargetBinder<VirtualizedList>, IRebindableBinder, IBinder<IReadOnlyList<IViewModel>>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<VirtualizedList\>](Aspid.MVVM.TargetBinder-1.md) ← 
[VirtualizedListItemSourceBinder](Aspid.MVVM.StarterKit.VirtualizedListItemSourceBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IReadOnlyList\<IViewModel\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<VirtualizedListItemSourceBinder\>\(VirtualizedListItemSourceBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<VirtualizedListItemSourceBinder\>\(VirtualizedListItemSourceBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<VirtualizedListItemSourceBinder\>\(VirtualizedListItemSourceBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### VirtualizedListItemSourceBinder\(VirtualizedList, ICollectionFilter\<IViewModel\>, ICollectionOrder\<IViewModel\>, BindMode\) {#Aspid_MVVM_StarterKit_VirtualizedListItemSourceBinder__ctor_Aspid_MVVM_StarterKit_VirtualizedList_Aspid_MVVM_StarterKit_ICollectionFilter_Aspid_MVVM_IViewModel__Aspid_MVVM_StarterKit_ICollectionOrder_Aspid_MVVM_IViewModel__Aspid_MVVM_BindMode_}

```csharp
public VirtualizedListItemSourceBinder(VirtualizedList target, ICollectionFilter<IViewModel> filter = null, ICollectionOrder<IViewModel> order = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` [VirtualizedList](Aspid.MVVM.StarterKit.VirtualizedList.md)

`filter` [ICollectionFilter](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)\<[IViewModel](Aspid.MVVM.IViewModel.md)\>

`order` [ICollectionOrder](Aspid.MVVM.StarterKit.ICollectionOrder-1.md)\<[IViewModel](Aspid.MVVM.IViewModel.md)\>

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_VirtualizedListItemSourceBinder_OnUnbound}

Called after unbinding. Override to add post-unbinding logic.

```csharp
protected override void OnUnbound()
```

#### Remarks

Runs once the binder is detached and [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
This is where a subscription taken in [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) is released.

### SetValue\(IReadOnlyList\<IViewModel\>\) {#Aspid_MVVM_StarterKit_VirtualizedListItemSourceBinder_SetValue_System_Collections_Generic_IReadOnlyList_Aspid_MVVM_IViewModel__}

Sets the bound property to <code class="paramref">value</code>.

```csharp
public void SetValue(IReadOnlyList<IViewModel> list)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[IViewModel](Aspid.MVVM.IViewModel.md)\>

