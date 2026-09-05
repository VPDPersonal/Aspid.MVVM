---
title: "Class AnimatorSetParameterBinder<T>"
sidebar_label: "AnimatorSetParameterBinder<T>"
description: "Class AnimatorSetParameterBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AnimatorSetParameterBinder\<T\> {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract [`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that sets a typed [`Animator`](https://docs.unity3d.com/ScriptReference/Animator.html) parameter.

```csharp
[Serializable]
[BindModeOverride(new BindMode[] { BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource })]
public abstract class AnimatorSetParameterBinder<T> : TargetBinder<Animator>, IRebindableBinder, IBinder<T>, IReverseBinder<Action<T>?>, IReverseBinder<IRelayCommand<T>?>, IBinder
```

#### Type Parameters

`T` 

The parameter value type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<Animator\>](Aspid.MVVM.TargetBinder-1.md) ← 
[AnimatorSetParameterBinder\<T\>](Aspid.MVVM.StarterKit.AnimatorSetParameterBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<T\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<Action\<T\>?\>](Aspid.MVVM.IReverseBinder-1.md), 
[IReverseBinder\<IRelayCommand\<T\>?\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<AnimatorSetParameterBinder\<T\>\>\(AnimatorSetParameterBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<AnimatorSetParameterBinder\<T\>\>\(AnimatorSetParameterBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<AnimatorSetParameterBinder\<T\>\>\(AnimatorSetParameterBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

In [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) the setter is handed to the ViewModel as an
[`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) or an [`Action<T>`](https://learn.microsoft.com/dotnet/api/system.action-1).

## Constructors

### AnimatorSetParameterBinder\(\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1__ctor}

```csharp
protected AnimatorSetParameterBinder()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### AnimatorSetParameterBinder\(Animator, string, BindMode\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1__ctor_UnityEngine_Animator_System_String_Aspid_MVVM_BindMode_}

```csharp
protected AnimatorSetParameterBinder(Animator target, string parameterName, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` Animator

The animator to bind.

`parameterName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The parameter to set.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">parameterName</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md).

## Properties

### ParameterName {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1_ParameterName}

```csharp
protected string ParameterName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### ParameterType {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1_ParameterType}

The parameter type inferred from <code class="typeparamref">T</code>, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to match by name only.

```csharp
protected virtual AnimatorControllerParameterType? ParameterType { get; }
```

#### Property Value

 AnimatorControllerParameterType?

## Methods

### CanExecute\(T?\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1_CanExecute__0_}

Whether the parameter may be set: the animator is active and its controller has the parameter.

```csharp
protected virtual bool CanExecute(T? value)
```

#### Parameters

`value` T?

The value that would be written.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the parameter may be set.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1_NotifyCanExecuteChanged}

Notifies the command handed to the ViewModel that [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute) may have changed.

```csharp
public void NotifyCanExecuteChanged()
```

### OnBound\(\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1_OnBound}

Called after binding is established. Override to add post-binding logic.

```csharp
protected override sealed void OnBound()
```

#### Remarks

Runs after the ViewModel's first value has been applied and after [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. This is where a binder subscribes to its component — see
[`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding) for why the earlier hook is the wrong place.

### OnUnbinding\(\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1_OnUnbinding}

Called before unbinding. Override to add pre-unbinding logic.

```csharp
protected override sealed void OnUnbinding()
```

#### Remarks

Runs while [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is still <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> and the binder is still
attached to the ViewModel, so anything sent from here still arrives.

### SetParameter\(T?\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1_SetParameter__0_}

Writes <code class="paramref">value</code> to the parameter named [`AnimatorSetParameterBinder<T>.ParameterName`](Aspid.MVVM.StarterKit.AnimatorSetParameterBinder-1.md#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1_ParameterName).

```csharp
protected abstract void SetParameter(T? value)
```

#### Parameters

`value` T?

The value to write.

### SetValue\(T?\) {#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1_SetValue__0_}

Sets the parameter when [`AnimatorSetParameterBinder<T>.CanExecute`](Aspid.MVVM.StarterKit.AnimatorSetParameterBinder-1.md#Aspid_MVVM_StarterKit_AnimatorSetParameterBinder_1_CanExecute__0_) allows it.

```csharp
public void SetValue(T? value)
```

#### Parameters

`value` T?

The value received from the ViewModel.

