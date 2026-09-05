---
title: "Class CommandMonoBinder<T>"
sidebar_label: "CommandMonoBinder<T>"
description: "Class CommandMonoBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CommandMonoBinder\<T\> {#Aspid_MVVM_StarterKit_CommandMonoBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that holds a bound [`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) and exposes [`CommandMonoBinder<T>.CanExecute`](Aspid.MVVM.StarterKit.CommandMonoBinder-1.md#Aspid_MVVM_StarterKit_CommandMonoBinder_1_CanExecute__0_) and [`CommandMonoBinder<T>.Execute`](Aspid.MVVM.StarterKit.CommandMonoBinder-1.md#Aspid_MVVM_StarterKit_CommandMonoBinder_1_Execute__0_) for it.

```csharp
public abstract class CommandMonoBinder<T> : MonoBinder, IMonoBinderValidatable, IRebindableBinder, IBinder<IRelayCommand<T>>, IBinder
```

#### Type Parameters

`T` 

The type of the command parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[CommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.CommandMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IRelayCommand\<T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<CommandMonoBinder\<T\>\>\(CommandMonoBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<CommandMonoBinder\<T\>\>\(CommandMonoBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<CommandMonoBinder\<T\>\>\(CommandMonoBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_CommandMonoBinder_1_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### Command {#Aspid_MVVM_StarterKit_CommandMonoBinder_1_Command}

Gets the bound command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when unbound.

```csharp
protected IRelayCommand<T> Command { get; }
```

#### Property Value

 [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>

### IsDebug {#Aspid_MVVM_StarterKit_CommandMonoBinder_1_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_1_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### CanExecute\(T\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_1_CanExecute__0_}

Returns whether the bound command can execute with the given parameter.

```csharp
public bool CanExecute(T param1)
```

#### Parameters

`param1` T

The command parameter.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if a command is bound and can execute; otherwise <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(T\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_1_Execute__0_}

Executes the bound command, if any, with the given parameter.

```csharp
public void Execute(T param1)
```

#### Parameters

`param1` T

The command parameter.

### OnCanExecuteChanged\(IRelayCommand\<T\>\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_1_OnCanExecuteChanged_Aspid_MVVM_IRelayCommand__0__}

Called when the bound command's [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged) fires and right after binding.

```csharp
protected virtual void OnCanExecuteChanged(IRelayCommand<T> command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>

The bound command.

### OnSetValue\(IRelayCommand\<T\>\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_1_OnSetValue_Aspid_MVVM_IRelayCommand__0__}

Called after a command is bound. Override to react to the change.

```csharp
protected virtual void OnSetValue(IRelayCommand<T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>

The bound command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when unbound.

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_1_OnUnbound}

Called after unbinding. Override to release a subscription taken in [`MonoBinder.OnBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBound).

```csharp
protected override void OnUnbound()
```

### SetValue\(IRelayCommand\<T\>\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_1_SetValue_Aspid_MVVM_IRelayCommand__0__}

Binds <code class="paramref">value</code> and calls [`CommandMonoBinder<T>.OnSetValue`](Aspid.MVVM.StarterKit.CommandMonoBinder-1.md#Aspid_MVVM_StarterKit_CommandMonoBinder_1_OnSetValue_Aspid_MVVM_IRelayCommand__0__).

```csharp
public void SetValue(IRelayCommand<T> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>

The value received from the ViewModel.

