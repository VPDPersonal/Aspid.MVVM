---
title: "Class BindModeExtensions"
sidebar_label: "BindModeExtensions"
description: "Class BindModeExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindModeExtensions {#Aspid_MVVM_BindModeExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Provides extension methods for [`BindMode`](Aspid.MVVM.BindMode.md) providing mode classification checks and validation helpers.

```csharp
public static class BindModeExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BindModeExtensions](Aspid.MVVM.BindModeExtensions.md)



## Methods

### IsNone\(BindMode\) {#Aspid_MVVM_BindModeExtensions_IsNone_Aspid_MVVM_BindMode_}

Returns true when the mode is [`BindMode.None`](Aspid.MVVM.BindMode.md).

```csharp
[Obsolete("Compare with BindMode.None directly. Nothing in the package calls this, and the name reads as a question about validity rather than about one enum member. Will be removed in the next major version.")]
public static bool IsNone(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the mode is [`BindMode.None`](Aspid.MVVM.BindMode.md); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### IsOne\(BindMode\) {#Aspid_MVVM_BindModeExtensions_IsOne_Aspid_MVVM_BindMode_}

Returns true when the mode represents one-way styles: [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
public static bool IsOne(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the mode is [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### IsTwo\(BindMode\) {#Aspid_MVVM_BindModeExtensions_IsTwo_Aspid_MVVM_BindMode_}

Returns true when the mode represents two-way styles: [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

```csharp
public static bool IsTwo(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the mode is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### ThrowExceptionIfMatches\(BindMode, BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfMatches_Aspid_MVVM_BindMode_Aspid_MVVM_BindMode_}

Throws an [`ArgumentException`](https://learn.microsoft.com/dotnet/api/system.argumentexception) if the binding mode matches the specified invalid mode.

```csharp
public static void ThrowExceptionIfMatches(this BindMode mode, BindMode invalidMode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The current binding mode to check.

`invalidMode` [BindMode](Aspid.MVVM.BindMode.md)

The invalid binding mode to compare against.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when the mode matches the invalid mode.

### ThrowExceptionIfNone\(BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNone_Aspid_MVVM_BindMode_}

Throws an [`ArgumentException`](https://learn.microsoft.com/dotnet/api/system.argumentexception) if the binding mode is [`BindMode.None`](Aspid.MVVM.BindMode.md).

```csharp
public static void ThrowExceptionIfNone(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when the mode is [`BindMode.None`](Aspid.MVVM.BindMode.md).

### ThrowExceptionIfNotMatches\(BindMode, BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNotMatches_Aspid_MVVM_BindMode_Aspid_MVVM_BindMode_}

Throws an [`ArgumentException`](https://learn.microsoft.com/dotnet/api/system.argumentexception) if the binding mode does not match the specified valid mode.

```csharp
public static void ThrowExceptionIfNotMatches(this BindMode mode, BindMode validMode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The current binding mode to check.

`validMode` [BindMode](Aspid.MVVM.BindMode.md)

The valid binding mode to compare against.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when the mode is not equal to the valid mode.

### ThrowExceptionIfNotOne\(BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNotOne_Aspid_MVVM_BindMode_}

Throws if the binding mode is not one-way style ([`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md)).

```csharp
public static void ThrowExceptionIfNotOne(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to validate.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when the mode is not [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

### ThrowExceptionIfNotTwo\(BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNotTwo_Aspid_MVVM_BindMode_}

Throws if the binding mode is not two-way style ([`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md)).

```csharp
public static void ThrowExceptionIfNotTwo(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to validate.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when the mode is not [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

### ThrowExceptionIfOne\(BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfOne_Aspid_MVVM_BindMode_}

Throws an [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) if the binding mode is either [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
[Obsolete("Nothing in the package throws for the one-way modes: a binder that supports only the reverse direction uses ThrowExceptionIfNotTwo, and one that supports only the forward direction has nothing to reject. Will be removed in the next major version.")]
public static void ThrowExceptionIfOne(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when the mode is [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

### ThrowExceptionIfTwo\(BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfTwo_Aspid_MVVM_BindMode_}

Throws an [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) if the binding mode is either [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

```csharp
public static void ThrowExceptionIfTwo(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when the mode is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

