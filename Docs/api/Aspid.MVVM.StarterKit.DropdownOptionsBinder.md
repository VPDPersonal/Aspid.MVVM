---
title: "Class DropdownOptionsBinder"
sidebar_label: "DropdownOptionsBinder"
description: "Class DropdownOptionsBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DropdownOptionsBinder {#Aspid_MVVM_StarterKit_DropdownOptionsBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds `options` from labels, sprites or option
data.

```csharp
[Serializable]
[BindModeOverride(new BindMode[] { BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource })]
public class DropdownOptionsBinder : TargetBinder<TMP_Dropdown>, IRebindableBinder, IBinder<List<string>>, IBinder<List<Sprite>>, IBinder<IEnumerable<TMP_Dropdown.OptionData>>, IReverseBinder<List<TMP_Dropdown.OptionData>>, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[TargetBinder\<TMP\_Dropdown\>](Aspid.MVVM.TargetBinder-1.md) ← 
[DropdownOptionsBinder](Aspid.MVVM.StarterKit.DropdownOptionsBinder.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<List\<string\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<List\<Sprite\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IEnumerable\<TMP\_Dropdown.OptionData\>\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<List\<TMP\_Dropdown.OptionData\>\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<DropdownOptionsBinder\>\(DropdownOptionsBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<DropdownOptionsBinder\>\(DropdownOptionsBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<DropdownOptionsBinder\>\(DropdownOptionsBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Remarks

The selection is kept where the new list still has room for it; a selection that no longer fits is clamped
without being reported, that channel belongs to the Value binder.

## Constructors

### DropdownOptionsBinder\(TMP\_Dropdown, BindMode\) {#Aspid_MVVM_StarterKit_DropdownOptionsBinder__ctor_TMPro_TMP_Dropdown_Aspid_MVVM_BindMode_}

```csharp
public DropdownOptionsBinder(TMP_Dropdown target, BindMode mode = BindMode.OneWay)
```

#### Parameters

`target` TMP\_Dropdown

The dropdown to bind.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md).

## Methods

### OnBound\(\) {#Aspid_MVVM_StarterKit_DropdownOptionsBinder_OnBound}

Called after binding is established. Override to add post-binding logic.

```csharp
protected override void OnBound()
```

#### Remarks

Runs after the ViewModel's first value has been applied and after [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. This is where a binder subscribes to its component — see
[`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding) for why the earlier hook is the wrong place.

### SetValue\(List\<string\>?\) {#Aspid_MVVM_StarterKit_DropdownOptionsBinder_SetValue_System_Collections_Generic_List_System_String__}

Replaces the options with labels; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> clears them.

```csharp
public void SetValue(List<string>? values)
```

#### Parameters

`values` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

The values received from the ViewModel.

### SetValue\(List\<Sprite\>?\) {#Aspid_MVVM_StarterKit_DropdownOptionsBinder_SetValue_System_Collections_Generic_List_UnityEngine_Sprite__}

Replaces the options with sprites; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> clears them.

```csharp
public void SetValue(List<Sprite>? values)
```

#### Parameters

`values` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<Sprite\>?

The values received from the ViewModel.

### SetValue\(IEnumerable\<OptionData\>?\) {#Aspid_MVVM_StarterKit_DropdownOptionsBinder_SetValue_System_Collections_Generic_IEnumerable_TMPro_TMP_Dropdown_OptionData__}

Replaces the options; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> clears them.

```csharp
public void SetValue(IEnumerable<TMP_Dropdown.OptionData>? values)
```

#### Parameters

`values` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<TMP\_Dropdown.OptionData\>?

The values received from the ViewModel.

### ValueChanged {#Aspid_MVVM_StarterKit_DropdownOptionsBinder_ValueChanged}

Raised when the View's value changes and needs to be propagated back to the ViewModel.

```csharp
public event Action<List<TMP_Dropdown.OptionData>>? ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<TMP\_Dropdown.OptionData\>\>?

