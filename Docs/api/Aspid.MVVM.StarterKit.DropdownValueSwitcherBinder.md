---
title: "Class DropdownValueSwitcherBinder"
sidebar_label: "DropdownValueSwitcherBinder"
description: "Class DropdownValueSwitcherBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DropdownValueSwitcherBinder {#Aspid_MVVM_StarterKit_DropdownValueSwitcherBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

[`SwitcherBinder<T1, T2>`](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) that switches `value`.

```csharp
[Serializable]
public sealed class DropdownValueSwitcherBinder : SwitcherBinder<TMP_Dropdown, int>, IBinder<bool>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Binder](Aspid.MVVM.Binder.md) ←
[TargetBinder\<TMP\_Dropdown\>](Aspid.MVVM.TargetBinder-1.md) ←
[SwitcherBinder\<TMP\_Dropdown, int\>](Aspid.MVVM.StarterKit.SwitcherBinder-2.md) ←
[DropdownValueSwitcherBinder](Aspid.MVVM.StarterKit.DropdownValueSwitcherBinder.md)

#### Implements

[IBinder\<bool\>](Aspid.MVVM.IBinder-1.md),
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

## Constructors

### DropdownValueSwitcherBinder\(TMP\_Dropdown, NotifyMode, int, int, IConverter\<int, int\>, BindMode\) {#Aspid_MVVM_StarterKit_DropdownValueSwitcherBinder__ctor_TMPro_TMP_Dropdown_Aspid_MVVM_StarterKit_NotifyMode_System_Int32_System_Int32_Aspid_MVVM_StarterKit_IConverter_System_Int32_System_Int32__Aspid_MVVM_BindMode_}

```csharp
public DropdownValueSwitcherBinder(TMP_Dropdown target, NotifyMode notifyMode, int trueValue, int falseValue, IConverter<int, int> converter = null, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TMP\_Dropdown

`notifyMode` [NotifyMode](Aspid.MVVM.StarterKit.NotifyMode.md)

`trueValue` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`falseValue` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<[int](https://learn.microsoft.com/dotnet/api/system.int32), [int](https://learn.microsoft.com/dotnet/api/system.int32)\>

`mode` [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### SetValue\(int\) {#Aspid_MVVM_StarterKit_DropdownValueSwitcherBinder_SetValue_System_Int32_}

Applies the chosen, converted <code class="paramref">value</code> to the target.

```csharp
protected override void SetValue(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The value to apply.
