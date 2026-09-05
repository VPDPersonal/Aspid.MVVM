---
title: "Class DelegateTwoWayBinder<T>"
sidebar_label: "DelegateTwoWayBinder<T>"
description: "Class DelegateTwoWayBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DelegateTwoWayBinder\<T\> {#Aspid_MVVM_StarterKit_DelegateTwoWayBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`Binder`](Aspid.MVVM.Binder.md) implementing [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) and [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) that synchronises
a value in both directions between the ViewModel and the View.

```csharp
public class DelegateTwoWayBinder<T> : Binder, IRebindableBinder, IBinder<T>, IReverseBinder<T>, IBinder
```

#### Type Parameters

`T` 

The type of the value exchanged between View and ViewModel.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[DelegateTwoWayBinder\<T\>](Aspid.MVVM.StarterKit.DelegateTwoWayBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<T\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<T\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<DelegateTwoWayBinder\<T\>\>\(DelegateTwoWayBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<DelegateTwoWayBinder\<T\>\>\(DelegateTwoWayBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<DelegateTwoWayBinder\<T\>\>\(DelegateTwoWayBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### DelegateTwoWayBinder\(Action\<Action\<T\>\>, Action\<T?\>, Func\<T?\>?, Func\<T?\>?\) {#Aspid_MVVM_StarterKit_DelegateTwoWayBinder_1__ctor_System_Action_System_Action__0___System_Action__0__System_Func__0__System_Func__0__}

```csharp
public DelegateTwoWayBinder(Action<Action<T>> subscribe, Action<T?> setValue, Func<T?>? getValueOnBound = null, Func<T?>? getValueOnUnbinding = null)
```

#### Parameters

`subscribe` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>\>

Receives the callback that raises [`DelegateTwoWayBinder<T>.ValueChanged`](Aspid.MVVM.StarterKit.DelegateTwoWayBinder-1.md#Aspid_MVVM_StarterKit_DelegateTwoWayBinder_1_ValueChanged); subscribe it to the View event.

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

The action invoked with each value received from the ViewModel.

`getValueOnBound` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<T?\>?

Optional factory whose result is pushed to the ViewModel on binding.

`getValueOnUnbinding` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<T?\>?

Optional factory whose result is pushed to the ViewModel just before unbinding.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">subscribe</code> or <code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### DelegateTwoWayBinder\(Action\<T?\>, Func\<T?\>?, Func\<T?\>?\) {#Aspid_MVVM_StarterKit_DelegateTwoWayBinder_1__ctor_System_Action__0__System_Func__0__System_Func__0__}

```csharp
public DelegateTwoWayBinder(Action<T?> setValue, Func<T?>? getValueOnBound = null, Func<T?>? getValueOnUnbinding = null)
```

#### Parameters

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

The action invoked with each value received from the ViewModel.

`getValueOnBound` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<T?\>?

Optional factory whose result is pushed to the ViewModel on binding.

`getValueOnUnbinding` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<T?\>?

Optional factory whose result is pushed to the ViewModel just before unbinding.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_DelegateTwoWayBinder_1_OnBound}

Pushes the <code>getValueOnBound</code> result to the ViewModel, when that factory was provided.

```csharp
protected override void OnBound()
```

### OnUnbinding\(\) {#Aspid_MVVM_StarterKit_DelegateTwoWayBinder_1_OnUnbinding}

Pushes the <code>getValueOnUnbinding</code> result to the ViewModel, when that factory was provided.

```csharp
protected override void OnUnbinding()
```

### SetValue\(T?\) {#Aspid_MVVM_StarterKit_DelegateTwoWayBinder_1_SetValue__0_}

Forwards <code class="paramref">value</code> to the setter action.

```csharp
public void SetValue(T? value)
```

#### Parameters

`value` T?

The value received from the ViewModel.

### ValueChanged {#Aspid_MVVM_StarterKit_DelegateTwoWayBinder_1_ValueChanged}

Raised when the View's value changes and needs to be propagated back to the ViewModel.

```csharp
public event Action<T?>? ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

