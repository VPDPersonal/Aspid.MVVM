---
title: "Class AnimatorResetTriggerBinder"
sidebar_label: "AnimatorResetTriggerBinder"
description: "Class AnimatorResetTriggerBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AnimatorResetTriggerBinder {#Aspid_MVVM_StarterKit_AnimatorResetTriggerBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`AnimatorTriggerBinder`](Aspid.MVVM.StarterKit.AnimatorTriggerBinder.md) that calls [`ResetTrigger`](https://docs.unity3d.com/ScriptReference/Animator-ResetTrigger.html).

```csharp
[Serializable]
public class AnimatorResetTriggerBinder : AnimatorTriggerBinder, IReverseBinder<Action?>, IReverseBinder<IRelayCommand?>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Binder](Aspid.MVVM.Binder.md) ←
[TargetBinder\<Animator\>](Aspid.MVVM.TargetBinder-1.md) ←
[AnimatorTriggerBinder](Aspid.MVVM.StarterKit.AnimatorTriggerBinder.md) ←
[AnimatorResetTriggerBinder](Aspid.MVVM.StarterKit.AnimatorResetTriggerBinder.md)

#### Implements

[IReverseBinder\<Action?\>](Aspid.MVVM.IReverseBinder-1.md),
[IReverseBinder\<IRelayCommand?\>](Aspid.MVVM.IReverseBinder-1.md),
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\(IBinder?, in BindContext\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_BindContext__),
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_),
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_),
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_),
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderExtensions.UnbindSafely\(IBinder?, in UnbindContext\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_UnbindContext__)

## Remarks

A trigger that was set and never consumed stays armed until reset.

## Constructors

### AnimatorResetTriggerBinder\(\) {#Aspid_MVVM_StarterKit_AnimatorResetTriggerBinder__ctor}

```csharp
protected AnimatorResetTriggerBinder()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### AnimatorResetTriggerBinder\(Animator, string\) {#Aspid_MVVM_StarterKit_AnimatorResetTriggerBinder__ctor_UnityEngine_Animator_System_String_}

```csharp
public AnimatorResetTriggerBinder(Animator target, string triggerName)
```

#### Parameters

`target` Animator

The animator to bind.

`triggerName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The trigger parameter.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">triggerName</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Apply\(string\) {#Aspid_MVVM_StarterKit_AnimatorResetTriggerBinder_Apply_System_String_}

Performs the operation on the trigger named <code class="paramref">triggerName</code>.

```csharp
protected override void Apply(string triggerName)
```

#### Parameters

`triggerName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The trigger, already checked to exist.
