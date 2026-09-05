---
title: "Class ObservableDictionaryViewModelBinder<TKey, TViewModel>"
sidebar_label: "ObservableDictionaryViewModelBinder<TKey, TViewModel>"
description: "Class ObservableDictionaryViewModelBinder<TKey, TViewModel> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableDictionaryViewModelBinder\<TKey, TViewModel\> {#Aspid_MVVM_StarterKit_ObservableDictionaryViewModelBinder_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ObservableDictionaryViewModelBinder<T1, T2, T3>`](Aspid.MVVM.StarterKit.ObservableDictionaryViewModelBinder-3.md) over [`MonoView`](Aspid.MVVM.MonoView.md).

```csharp
[Serializable]
public class ObservableDictionaryViewModelBinder<TKey, TViewModel> : ObservableDictionaryViewModelBinder<TKey, TViewModel, MonoView>, IRebindableBinder, IBinder<IReadOnlyObservableDictionary<TKey, TViewModel?>>, IBinder where TViewModel : IViewModel
```

#### Type Parameters

`TKey` 

The type of the dictionary keys.

`TViewModel` 

The type of ViewModel stored as values.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[ObservableDictionaryBinder\<TKey, TViewModel\>](Aspid.MVVM.StarterKit.ObservableDictionaryBinder-2.md) ← 
[ObservableDictionaryViewModelBinder\<TKey, TViewModel, MonoView\>](Aspid.MVVM.StarterKit.ObservableDictionaryViewModelBinder-3.md) ← 
[ObservableDictionaryViewModelBinder\<TKey, TViewModel\>](Aspid.MVVM.StarterKit.ObservableDictionaryViewModelBinder-2.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IReadOnlyObservableDictionary\<TKey, TViewModel?\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ObservableDictionaryViewModelBinder\<TKey, TViewModel\>\>\(ObservableDictionaryViewModelBinder\<TKey, TViewModel\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ObservableDictionaryViewModelBinder\<TKey, TViewModel\>\>\(ObservableDictionaryViewModelBinder\<TKey, TViewModel\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ObservableDictionaryViewModelBinder\<TKey, TViewModel\>\>\(ObservableDictionaryViewModelBinder\<TKey, TViewModel\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### ObservableDictionaryViewModelBinder\(\) {#Aspid_MVVM_StarterKit_ObservableDictionaryViewModelBinder_2__ctor}

```csharp
protected ObservableDictionaryViewModelBinder()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### ObservableDictionaryViewModelBinder\(IViewFactoryWithKey\<MonoView\>, BindMode\) {#Aspid_MVVM_StarterKit_ObservableDictionaryViewModelBinder_2__ctor_Aspid_MVVM_StarterKit_IViewFactoryWithKey_Aspid_MVVM_MonoView__Aspid_MVVM_BindMode_}

```csharp
public ObservableDictionaryViewModelBinder(IViewFactoryWithKey<MonoView> viewFactory, BindMode mode = BindMode.OneWay)
```

#### Parameters

`viewFactory` [IViewFactoryWithKey](Aspid.MVVM.StarterKit.IViewFactoryWithKey-1.md)\<[MonoView](Aspid.MVVM.MonoView.md)\>

The factory that creates and releases views by key.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. Must not be [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">viewFactory</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when <code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

