---
title: "Class Binder<TProperty>"
sidebar_label: "Binder<TProperty>"
description: "Class Binder<TProperty> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Binder\<TProperty\> {#Aspid_MVVM_StarterKit_Binder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`Binder`](Aspid.MVVM.Binder.md) that binds a single property through its accessors, applying an optional
converter in both directions. In [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md), the current property value is sent to the ViewModel on binding.

```csharp
[Serializable]
[BindModeOverride(new BindMode[] { BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource })]
public abstract class Binder<TProperty> : Binder, IRebindableBinder, IBinder<TProperty>, IReverseBinder<TProperty>, IBinder
```

#### Type Parameters

`TProperty` 

The type of the bound property.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[Binder\<TProperty\>](Aspid.MVVM.StarterKit.Binder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<TProperty\>](Aspid.MVVM.IBinder-1.md), 
[IReverseBinder\<TProperty\>](Aspid.MVVM.IReverseBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<Binder\<TProperty\>\>\(Binder\<TProperty\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<Binder\<TProperty\>\>\(Binder\<TProperty\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<Binder\<TProperty\>\>\(Binder\<TProperty\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### Binder\(\) {#Aspid_MVVM_StarterKit_Binder_1__ctor}

```csharp
protected Binder()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### Binder\(IConverter\<TProperty?, TProperty?\>?, BindMode\) {#Aspid_MVVM_StarterKit_Binder_1__ctor_Aspid_MVVM_StarterKit_IConverter__0__0__Aspid_MVVM_BindMode_}

```csharp
protected Binder(IConverter<TProperty?, TProperty?>? converter, BindMode mode = BindMode.OneWay)
```

#### Parameters

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<TProperty?, TProperty?\>?

The converter applied before the value is written, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it unchanged.
Runs in reverse only if it implements [`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

## Properties

### Property {#Aspid_MVVM_StarterKit_Binder_1_Property}

Gets or sets the bound property.

```csharp
protected abstract TProperty? Property { get; set; }
```

#### Property Value

 TProperty?

## Methods

### GetConvertedBackValue\(TProperty?\) {#Aspid_MVVM_StarterKit_Binder_1_GetConvertedBackValue__0_}

Converts <code class="paramref">value</code> for the ViewModel; unchanged unless the converter implements [`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).

```csharp
protected virtual TProperty? GetConvertedBackValue(TProperty? value)
```

#### Parameters

`value` TProperty?

The value to convert.

#### Returns

 TProperty?

The converted value.

### GetConvertedValue\(TProperty?\) {#Aspid_MVVM_StarterKit_Binder_1_GetConvertedValue__0_}

Converts <code class="paramref">value</code> with the serialized converter, or returns it unchanged when none is set.

```csharp
protected virtual TProperty? GetConvertedValue(TProperty? value)
```

#### Parameters

`value` TProperty?

The value to convert.

#### Returns

 TProperty?

The converted value.

### OnBound\(\) {#Aspid_MVVM_StarterKit_Binder_1_OnBound}

Sends the initial property value to the ViewModel in [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

```csharp
protected override void OnBound()
```

#### Remarks

When overriding, always call <code>base.OnBound()</code>. To change what is sent, override [`Binder<T>.SendInitialValueToSource`](Aspid.MVVM.StarterKit.Binder-1.md#Aspid_MVVM_StarterKit_Binder_1_SendInitialValueToSource) instead.

### RaiseValueChanged\(\) {#Aspid_MVVM_StarterKit_Binder_1_RaiseValueChanged}

Raises [`Binder<T>.ValueChanged`](Aspid.MVVM.StarterKit.Binder-1.md#Aspid_MVVM_StarterKit_Binder_1_ValueChanged) with the current [`Binder<T>.Property`](Aspid.MVVM.StarterKit.Binder-1.md#Aspid_MVVM_StarterKit_Binder_1_Property), after [`Binder<T>.GetConvertedBackValue`](Aspid.MVVM.StarterKit.Binder-1.md#Aspid_MVVM_StarterKit_Binder_1_GetConvertedBackValue__0_).

```csharp
protected void RaiseValueChanged()
```

### RaiseValueChanged\(TProperty?\) {#Aspid_MVVM_StarterKit_Binder_1_RaiseValueChanged__0_}

Raises [`Binder<T>.ValueChanged`](Aspid.MVVM.StarterKit.Binder-1.md#Aspid_MVVM_StarterKit_Binder_1_ValueChanged) with <code class="paramref">value</code>, after [`Binder<T>.GetConvertedBackValue`](Aspid.MVVM.StarterKit.Binder-1.md#Aspid_MVVM_StarterKit_Binder_1_GetConvertedBackValue__0_).

```csharp
protected void RaiseValueChanged(TProperty? value)
```

#### Parameters

`value` TProperty?

The value to send to the ViewModel.

### SendInitialValueToSource\(\) {#Aspid_MVVM_StarterKit_Binder_1_SendInitialValueToSource}

Called on binding in [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) to send the current [`Binder<T>.Property`](Aspid.MVVM.StarterKit.Binder-1.md#Aspid_MVVM_StarterKit_Binder_1_Property) to the ViewModel.
Override to broadcast through additional channels.

```csharp
protected virtual void SendInitialValueToSource()
```

#### Remarks

An override must route the value through [`Binder<T>.GetConvertedBackValue`](Aspid.MVVM.StarterKit.Binder-1.md#Aspid_MVVM_StarterKit_Binder_1_GetConvertedBackValue__0_).

### SetValue\(TProperty?\) {#Aspid_MVVM_StarterKit_Binder_1_SetValue__0_}

Writes <code class="paramref">value</code> to [`Binder<T>.Property`](Aspid.MVVM.StarterKit.Binder-1.md#Aspid_MVVM_StarterKit_Binder_1_Property) after [`Binder<T>.GetConvertedValue`](Aspid.MVVM.StarterKit.Binder-1.md#Aspid_MVVM_StarterKit_Binder_1_GetConvertedValue__0_).

```csharp
public void SetValue(TProperty? value)
```

#### Parameters

`value` TProperty?

The value received from the ViewModel.

### ValueChanged {#Aspid_MVVM_StarterKit_Binder_1_ValueChanged}

Raised when the View's value changes and needs to be propagated back to the ViewModel.

```csharp
public event Action<TProperty?>? ValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<TProperty?\>?

