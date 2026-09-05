---
title: "Class ObservableDictionaryMonoBinder<TKey, TValue>"
sidebar_label: "ObservableDictionaryMonoBinder<TKey, TValue>"
description: "Class ObservableDictionaryMonoBinder<TKey, TValue> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableDictionaryMonoBinder\<TKey, TValue\> {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that follows an `IReadOnlyObservableDictionary<T1, T2>`
and reflects its changes onto a View.

```csharp
public abstract class ObservableDictionaryMonoBinder<TKey, TValue> : MonoBinder, IMonoBinderValidatable, IRebindableBinder, IBinder<IReadOnlyObservableDictionary<TKey, TValue>>, IBinder
```

#### Type Parameters

`TKey` 

The type of the dictionary keys.

`TValue` 

The type of the dictionary values.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ObservableDictionaryMonoBinder\<TKey, TValue\>](Aspid.MVVM.StarterKit.ObservableDictionaryMonoBinder-2.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IReadOnlyObservableDictionary\<TKey, TValue\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ObservableDictionaryMonoBinder\<TKey, TValue\>\>\(ObservableDictionaryMonoBinder\<TKey, TValue\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ObservableDictionaryMonoBinder\<TKey, TValue\>\>\(ObservableDictionaryMonoBinder\<TKey, TValue\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ObservableDictionaryMonoBinder\<TKey, TValue\>\>\(ObservableDictionaryMonoBinder\<TKey, TValue\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

[`Move`](https://learn.microsoft.com/dotnet/api/system.collections.specialized.notifycollectionchangedaction.move) throws [`NotImplementedException`](https://learn.microsoft.com/dotnet/api/system.notimplementedexception): a dictionary has no order.

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### Dictionary {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_Dictionary}

Gets the bound dictionary, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none is set.

```csharp
protected IReadOnlyObservableDictionary<TKey, TValue> Dictionary { get; }
```

#### Property Value

 IReadOnlyObservableDictionary\<TKey, TValue\>

### IsDebug {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### OnAdded\(KeyValuePair\<TKey, TValue\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_OnAdded_System_Collections_Generic_KeyValuePair__0__1__}

Called when one entry was added.

```csharp
protected abstract void OnAdded(KeyValuePair<TKey, TValue> newItem)
```

#### Parameters

`newItem` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>

The added entry.

### OnAdded\(IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_OnAdded_System_Collections_Generic_IReadOnlyList_System_Collections_Generic_KeyValuePair__0__1___}

Called when several entries were added at once.

```csharp
protected abstract void OnAdded(IReadOnlyList<KeyValuePair<TKey, TValue>> newItems)
```

#### Parameters

`newItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>\>

The added entries.

### OnRemoved\(KeyValuePair\<TKey, TValue\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_OnRemoved_System_Collections_Generic_KeyValuePair__0__1__}

Called when one entry was removed.

```csharp
protected abstract void OnRemoved(KeyValuePair<TKey, TValue> oldItem)
```

#### Parameters

`oldItem` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>

The removed entry.

### OnRemoved\(IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_OnRemoved_System_Collections_Generic_IReadOnlyList_System_Collections_Generic_KeyValuePair__0__1___}

Called when several entries were removed at once.

```csharp
protected abstract void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TValue>> oldItems)
```

#### Parameters

`oldItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>\>

The removed entries.

### OnReplaced\(KeyValuePair\<TKey, TValue\>, KeyValuePair\<TKey, TValue\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_OnReplaced_System_Collections_Generic_KeyValuePair__0__1__System_Collections_Generic_KeyValuePair__0__1__}

Called when an entry was replaced.

```csharp
protected abstract void OnReplaced(KeyValuePair<TKey, TValue> oldItem, KeyValuePair<TKey, TValue> newItem)
```

#### Parameters

`oldItem` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>

The entry before replacement.

`newItem` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>

The entry after replacement.

### OnReset\(\) {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_OnReset}

Called when the dictionary was cleared; the View should drop every entry.

```csharp
protected abstract void OnReset()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_OnUnbound}

Resets the View and unsubscribes from the bound dictionary.

```csharp
protected override void OnUnbound()
```

### SetValue\(IReadOnlyObservableDictionary\<TKey, TValue\>\) {#Aspid_MVVM_StarterKit_ObservableDictionaryMonoBinder_2_SetValue_Aspid_Collections_Observable_IReadOnlyObservableDictionary__0__1__}

Binds to <code class="paramref">dictionary</code>: resets the previous one, then forwards the existing entries to [`ObservableDictionaryMonoBinder<T1, T2>.OnAdded`](Aspid.MVVM.StarterKit.ObservableDictionaryMonoBinder-2.md).

```csharp
public void SetValue(IReadOnlyObservableDictionary<TKey, TValue> dictionary)
```

#### Parameters

`dictionary` IReadOnlyObservableDictionary\<TKey, TValue\>

The dictionary to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear the binding.

