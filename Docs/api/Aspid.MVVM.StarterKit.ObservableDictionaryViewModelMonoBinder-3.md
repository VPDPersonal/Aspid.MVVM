---
title: "Class ObservableDictionaryViewModelMonoBinder<TKey, TViewModel, TView>"
sidebar_label: "ObservableDictionaryViewModelMonoBinder<TKey, TViewModel, TView>"
description: "Class ObservableDictionaryViewModelMonoBinder<TKey, TViewModel, TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableDictionaryViewModelMonoBinder\<TKey, TViewModel, TView\> {#Aspid_MVVM_StarterKit_ObservableDictionaryViewModelMonoBinder_3}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ObservableDictionaryMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.ObservableDictionaryMonoBinder-2.md) that creates a view per entry through a keyed factory
and releases it when the entry leaves. A replacement releases the old view and creates a new one.

```csharp
public abstract class ObservableDictionaryViewModelMonoBinder<TKey, TViewModel, TView> : ObservableDictionaryMonoBinder<TKey, TViewModel>, IMonoBinderValidatable, IRebindableBinder, IBinder<IReadOnlyObservableDictionary<TKey, TViewModel>>, IBinder where TViewModel : IViewModel where TView : MonoBehaviour, IView
```

#### Type Parameters

`TKey` 

The type of the dictionary keys.

`TViewModel` 

The type of ViewModel stored as values.

`TView` 

The type of view created per entry.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ObservableDictionaryMonoBinder\<TKey, TViewModel\>](Aspid.MVVM.StarterKit.ObservableDictionaryMonoBinder-2.md) ← 
[ObservableDictionaryViewModelMonoBinder\<TKey, TViewModel, TView\>](Aspid.MVVM.StarterKit.ObservableDictionaryViewModelMonoBinder-3.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IReadOnlyObservableDictionary\<TKey, TViewModel\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ObservableDictionaryViewModelMonoBinder\<TKey, TViewModel, TView\>\>\(ObservableDictionaryViewModelMonoBinder\<TKey, TViewModel, TView\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ObservableDictionaryViewModelMonoBinder\<TKey, TViewModel, TView\>\>\(ObservableDictionaryViewModelMonoBinder\<TKey, TViewModel, TView\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ObservableDictionaryViewModelMonoBinder\<TKey, TViewModel, TView\>\>\(ObservableDictionaryViewModelMonoBinder\<TKey, TViewModel, TView\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Methods

### OnAdded\(KeyValuePair\<TKey, TViewModel\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryViewModelMonoBinder_3_OnAdded_System_Collections_Generic_KeyValuePair__0__1__}

Called when one entry was added.

```csharp
protected override void OnAdded(KeyValuePair<TKey, TViewModel> newItem)
```

#### Parameters

`newItem` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TViewModel\>

The added entry.

### OnAdded\(IReadOnlyList\<KeyValuePair\<TKey, TViewModel\>\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryViewModelMonoBinder_3_OnAdded_System_Collections_Generic_IReadOnlyList_System_Collections_Generic_KeyValuePair__0__1___}

Called when several entries were added at once.

```csharp
protected override void OnAdded(IReadOnlyList<KeyValuePair<TKey, TViewModel>> newItems)
```

#### Parameters

`newItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TViewModel\>\>

The added entries.

### OnRemoved\(KeyValuePair\<TKey, TViewModel\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryViewModelMonoBinder_3_OnRemoved_System_Collections_Generic_KeyValuePair__0__1__}

Called when one entry was removed.

```csharp
protected override void OnRemoved(KeyValuePair<TKey, TViewModel> oldItem)
```

#### Parameters

`oldItem` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TViewModel\>

The removed entry.

### OnRemoved\(IReadOnlyList\<KeyValuePair\<TKey, TViewModel\>\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryViewModelMonoBinder_3_OnRemoved_System_Collections_Generic_IReadOnlyList_System_Collections_Generic_KeyValuePair__0__1___}

Called when several entries were removed at once.

```csharp
protected override void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TViewModel>> oldItems)
```

#### Parameters

`oldItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TViewModel\>\>

The removed entries.

### OnReplaced\(KeyValuePair\<TKey, TViewModel\>, KeyValuePair\<TKey, TViewModel\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryViewModelMonoBinder_3_OnReplaced_System_Collections_Generic_KeyValuePair__0__1__System_Collections_Generic_KeyValuePair__0__1__}

Called when an entry was replaced.

```csharp
protected override void OnReplaced(KeyValuePair<TKey, TViewModel> oldItem, KeyValuePair<TKey, TViewModel> newItem)
```

#### Parameters

`oldItem` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TViewModel\>

The entry before replacement.

`newItem` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TViewModel\>

The entry after replacement.

### OnReset\(\) {#Aspid_MVVM_StarterKit_ObservableDictionaryViewModelMonoBinder_3_OnReset}

Called when the dictionary was cleared; the View should drop every entry.

```csharp
protected override void OnReset()
```

