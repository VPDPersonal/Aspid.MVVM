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

Provides extension methods for [`BindMode`](Aspid.MVVM.BindMode.md).

```csharp
public static class BindModeExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BindModeExtensions](Aspid.MVVM.BindModeExtensions.md)



## Methods

### IsNone\(BindMode\) {#Aspid_MVVM_BindModeExtensions_IsNone_Aspid_MVVM_BindMode_}

Determines whether <code class="paramref">mode</code> is [`BindMode.None`](Aspid.MVVM.BindMode.md).

```csharp
[Obsolete("Compare with BindMode.None directly. Nothing in the package calls this, and the name reads as a question about validity rather than about one enum member. Will be removed in the next major version.")]
public static bool IsNone(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for [`BindMode.None`](Aspid.MVVM.BindMode.md); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### IsOne\(BindMode\) {#Aspid_MVVM_BindModeExtensions_IsOne_Aspid_MVVM_BindMode_}

Determines whether <code class="paramref">mode</code> pushes values only from the ViewModel to the View.

```csharp
public static bool IsOne(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### IsTwo\(BindMode\) {#Aspid_MVVM_BindModeExtensions_IsTwo_Aspid_MVVM_BindMode_}

Determines whether <code class="paramref">mode</code> pushes values from the View to the ViewModel.

```csharp
public static bool IsTwo(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md); otherwise,
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### ThrowExceptionIfMatches\(BindMode, BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfMatches_Aspid_MVVM_BindMode_Aspid_MVVM_BindMode_}

Throws when <code class="paramref">mode</code> equals <code class="paramref">invalidMode</code>.

```csharp
public static void ThrowExceptionIfMatches(this BindMode mode, BindMode invalidMode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

`invalidMode` [BindMode](Aspid.MVVM.BindMode.md)

The disallowed mode.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">mode</code> equals <code class="paramref">invalidMode</code>.

### ThrowExceptionIfNone\(BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNone_Aspid_MVVM_BindMode_}

Throws when <code class="paramref">mode</code> is [`BindMode.None`](Aspid.MVVM.BindMode.md).

```csharp
public static void ThrowExceptionIfNone(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">mode</code> is [`BindMode.None`](Aspid.MVVM.BindMode.md).

### ThrowExceptionIfNotMatches\(BindMode, BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNotMatches_Aspid_MVVM_BindMode_Aspid_MVVM_BindMode_}

Throws unless <code class="paramref">mode</code> equals <code class="paramref">validMode</code>.

```csharp
public static void ThrowExceptionIfNotMatches(this BindMode mode, BindMode validMode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

`validMode` [BindMode](Aspid.MVVM.BindMode.md)

The only allowed mode.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">mode</code> differs from <code class="paramref">validMode</code>.

### ThrowExceptionIfNotOne\(BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNotOne_Aspid_MVVM_BindMode_}

Throws unless <code class="paramref">mode</code> is [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
public static void ThrowExceptionIfNotOne(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is neither [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) nor [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

### ThrowExceptionIfNotTwo\(BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNotTwo_Aspid_MVVM_BindMode_}

Throws unless <code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

```csharp
public static void ThrowExceptionIfNotTwo(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is neither [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) nor [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

### ThrowExceptionIfOne\(BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfOne_Aspid_MVVM_BindMode_}

Throws when <code class="paramref">mode</code> is [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
[Obsolete("Nothing in the package throws for the one-way modes: a binder that supports only the reverse direction uses ThrowExceptionIfNotTwo, and one that supports only the forward direction has nothing to reject. Will be removed in the next major version.")]
public static void ThrowExceptionIfOne(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

### ThrowExceptionIfTwo\(BindMode\) {#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfTwo_Aspid_MVVM_BindMode_}

Throws when <code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

```csharp
public static void ThrowExceptionIfTwo(this BindMode mode)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to check.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

