---
title: "Class CommandMonoBinder<T1, T2>"
sidebar_label: "CommandMonoBinder<T1, T2>"
description: "Class CommandMonoBinder<T1, T2> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CommandMonoBinder\<T1, T2\> {#Aspid_MVVM_StarterKit_CommandMonoBinder_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that holds a bound [`IRelayCommand<T1, T2>`](Aspid.MVVM.IRelayCommand-2.md) and exposes [`CommandMonoBinder<T1, T2>.CanExecute`](Aspid.MVVM.StarterKit.CommandMonoBinder-2.md) and [`CommandMonoBinder<T1, T2>.Execute`](Aspid.MVVM.StarterKit.CommandMonoBinder-2.md) for it.

```csharp
public abstract class CommandMonoBinder<T1, T2> : MonoBinder, IMonoBinderValidatable, IRebindableBinder, IBinder<IRelayCommand<T1, T2>>, IBinder
```

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[CommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.CommandMonoBinder-2.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IRelayCommand\<T1, T2\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<CommandMonoBinder\<T1, T2\>\>\(CommandMonoBinder\<T1, T2\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<CommandMonoBinder\<T1, T2\>\>\(CommandMonoBinder\<T1, T2\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<CommandMonoBinder\<T1, T2\>\>\(CommandMonoBinder\<T1, T2\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_CommandMonoBinder_2_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### Command {#Aspid_MVVM_StarterKit_CommandMonoBinder_2_Command}

Gets the bound command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when unbound.

```csharp
protected IRelayCommand<T1, T2> Command { get; }
```

#### Property Value

 [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>

### IsDebug {#Aspid_MVVM_StarterKit_CommandMonoBinder_2_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_2_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### CanExecute\(T1, T2\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_2_CanExecute__0__1_}

Returns whether the bound command can execute with the given parameters.

```csharp
public bool CanExecute(T1 param1, T2 param2)
```

#### Parameters

`param1` T1

The first command parameter.

`param2` T2

The second command parameter.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if a command is bound and can execute; otherwise <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(T1, T2\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_2_Execute__0__1_}

Executes the bound command, if any, with the given parameters.

```csharp
public void Execute(T1 param1, T2 param2)
```

#### Parameters

`param1` T1

The first command parameter.

`param2` T2

The second command parameter.

### OnCanExecuteChanged\(IRelayCommand\<T1, T2\>\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_2_OnCanExecuteChanged_Aspid_MVVM_IRelayCommand__0__1__}

Called when the bound command's [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged) fires and right after binding.

```csharp
protected virtual void OnCanExecuteChanged(IRelayCommand<T1, T2> command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>

The bound command.

### OnSetValue\(IRelayCommand\<T1, T2\>\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_2_OnSetValue_Aspid_MVVM_IRelayCommand__0__1__}

Called after a command is bound. Override to react to the change.

```csharp
protected virtual void OnSetValue(IRelayCommand<T1, T2> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>

The bound command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when unbound.

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_2_OnUnbound}

Called after unbinding. Override to release a subscription taken in [`MonoBinder.OnBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBound).

```csharp
protected override void OnUnbound()
```

### SetValue\(IRelayCommand\<T1, T2\>\) {#Aspid_MVVM_StarterKit_CommandMonoBinder_2_SetValue_Aspid_MVVM_IRelayCommand__0__1__}

Binds <code class="paramref">value</code> and calls [`CommandMonoBinder<T1, T2>.OnSetValue`](Aspid.MVVM.StarterKit.CommandMonoBinder-2.md).

```csharp
public void SetValue(IRelayCommand<T1, T2> value)
```

#### Parameters

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>

The value received from the ViewModel.

