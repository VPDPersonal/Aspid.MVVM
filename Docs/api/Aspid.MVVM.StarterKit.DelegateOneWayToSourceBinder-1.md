---
title: "Class DelegateOneWayToSourceBinder<T>"
sidebar_label: "DelegateOneWayToSourceBinder<T>"
description: "Class DelegateOneWayToSourceBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DelegateOneWayToSourceBinder\<T\> {#Aspid_MVVM_StarterKit_DelegateOneWayToSourceBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) that propagates View values back to the ViewModel.

```csharp
public class DelegateOneWayToSourceBinder<T> : Binder, IRebindableBinder, IReverseBinder<T>, IBinder
```

#### Type Parameters

`T` 

The type of the value reported to the ViewModel.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[DelegateOneWayToSourceBinder\<T\>](Aspid.MVVM.StarterKit.DelegateOneWayToSourceBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IReverseBinder\<T\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<DelegateOneWayToSourceBinder\<T\>\>\(DelegateOneWayToSourceBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<DelegateOneWayToSourceBinder\<T\>\>\(DelegateOneWayToSourceBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<DelegateOneWayToSourceBinder\<T\>\>\(DelegateOneWayToSourceBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### DelegateOneWayToSourceBinder\(Action\<Action\<T\>\>, Func\<T?\>?, Func\<T?\>?\) {#Aspid_MVVM_StarterKit_DelegateOneWayToSourceBinder_1__ctor_System_Action_System_Action__0___System_Func__0__System_Func__0__}

```csharp
public DelegateOneWayToSourceBinder(Action<Action<T>> subscribe, Func<T?>? getValueOnBound = null, Func<T?>? getValueOnUnbinding = null)
```

#### Parameters

`subscribe` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>\>

Receives the callback that raises [`DelegateOneWayToSourceBinder<T>.ValueChanged`](Aspid.MVVM.StarterKit.DelegateOneWayToSourceBinder-1.md#Aspid_MVVM_StarterKit_DelegateOneWayToSourceBinder_1_ValueChanged); subscribe it to the View event.

`getValueOnBound` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<T?\>?

Optional factory whose result is pushed to the ViewModel on binding.

`getValueOnUnbinding` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<T?\>?

Optional factory whose result is pushed to the ViewModel just before unbinding.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">subscribe</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### DelegateOneWayToSourceBinder\(Func\<T?\>?, Func\<T?\>?\) {#Aspid_MVVM_StarterKit_DelegateOneWayToSourceBinder_1__ctor_System_Func__0__System_Func__0__}

```csharp
public DelegateOneWayToSourceBinder(Func<T?>? getValueOnBound = null, Func<T?>? getValueOnUnbinding = null)
```

#### Parameters

`getValueOnBound` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<T?\>?

Optional factory whose result is pushed to the ViewModel on binding.

`getValueOnUnbinding` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<T?\>?

Optional factory whose result is pushed to the ViewModel just before unbinding.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when both <code class="paramref">getValueOnBound</code> and <code class="paramref">getValueOnUnbinding</code> are <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_DelegateOneWayToSourceBinder_1_OnBound}

Pushes the <code>getValueOnBound</code> result to the ViewModel, when that factory was provided.

```csharp
protected override void OnBound()
```

### OnUnbinding\(\) {#Aspid_MVVM_StarterKit_DelegateOneWayToSourceBinder_1_OnUnbinding}

Pushes the <code>getValueOnUnbinding</code> result to the ViewModel, when that factory was provided.

```csharp
protected override void OnUnbinding()
```

### ValueChanged {#Aspid_MVVM_StarterKit_DelegateOneWayToSourceBinder_1_ValueChanged}

Raised when the View's value changes and needs to be propagated back to the ViewModel.

```csharp
public event Action<T?>? ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

