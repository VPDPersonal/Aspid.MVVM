---
title: "Class BindModeExtensions"
sidebar_label: "BindModeExtensions"
description: "Class BindModeExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindModeExtensions {#Aspid_MVVM_BindModeExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Provides extension methods for [`BindMode`](Aspid.MVVM.BindMode.md).

```csharp
public static class BindModeExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[BindModeExtensions](Aspid.MVVM.BindModeExtensions.md)



## Methods

### EnsureForward\(BindMode\) {#Aspid_MVVM_BindModeExtensions_EnsureForward_Aspid_MVVM_BindMode_}

Ensures the mode sends values from the ViewModel to the View.

```csharp
public static void EnsureForward(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The mode is not [`BindMode.OneWay`](Aspid.MVVM.BindMode.md), [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

### EnsureForwardOnly\(BindMode\) {#Aspid_MVVM_BindModeExtensions_EnsureForwardOnly_Aspid_MVVM_BindMode_}

Ensures the mode sends values only from the ViewModel to the View.

```csharp
public static void EnsureForwardOnly(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The mode is neither [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) nor [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

### EnsureNot\(BindMode, BindMode\) {#Aspid_MVVM_BindModeExtensions_EnsureNot_Aspid_MVVM_BindMode_Aspid_MVVM_BindMode_}

Ensures the mode is valid and differs from <code class="paramref">excludedMode</code>.

```csharp
public static void EnsureNot(this BindMode mode, BindMode excludedMode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

`excludedMode` [BindMode](Aspid.MVVM.BindMode.md)

The mode to exclude.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Either argument is [`BindMode.None`](Aspid.MVVM.BindMode.md) or undefined.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">mode</code> equals <code class="paramref">excludedMode</code>.

### EnsureReverse\(BindMode\) {#Aspid_MVVM_BindModeExtensions_EnsureReverse_Aspid_MVVM_BindMode_}

Ensures the mode sends values from the View to the ViewModel.

```csharp
public static void EnsureReverse(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The mode is neither [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) nor [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

### EnsureValid\(BindMode\) {#Aspid_MVVM_BindModeExtensions_EnsureValid_Aspid_MVVM_BindMode_}

Ensures the mode is declared and is not [`BindMode.None`](Aspid.MVVM.BindMode.md).

```csharp
public static void EnsureValid(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

The mode is [`BindMode.None`](Aspid.MVVM.BindMode.md) or undefined.

### IncludesForward\(BindMode\) {#Aspid_MVVM_BindModeExtensions_IncludesForward_Aspid_MVVM_BindMode_}

Determines whether the mode sends values from the ViewModel to the View.

```csharp
public static bool IncludesForward(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for [`BindMode.OneWay`](Aspid.MVVM.BindMode.md), [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### IncludesReverse\(BindMode\) {#Aspid_MVVM_BindModeExtensions_IncludesReverse_Aspid_MVVM_BindMode_}

Determines whether the mode sends values from the View to the ViewModel.

```csharp
public static bool IncludesReverse(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### IsForwardOnly\(BindMode\) {#Aspid_MVVM_BindModeExtensions_IsForwardOnly_Aspid_MVVM_BindMode_}

Determines whether the mode sends values only from the ViewModel to the View.

```csharp
public static bool IsForwardOnly(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### IsValid\(BindMode\) {#Aspid_MVVM_BindModeExtensions_IsValid_Aspid_MVVM_BindMode_}

Determines whether the mode can be used for binding.

```csharp
public static bool IsValid(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a declared mode other than [`BindMode.None`](Aspid.MVVM.BindMode.md); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
