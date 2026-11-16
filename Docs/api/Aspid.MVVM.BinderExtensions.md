---
title: "Class BinderExtensions"
sidebar_label: "BinderExtensions"
description: "Class BinderExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BinderExtensions {#Aspid_MVVM_BinderExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Provides extension methods for binding and unbinding [`IBinder`](Aspid.MVVM.IBinder.md) instances and their collections.

```csharp
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
public static class BinderExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[BinderExtensions](Aspid.MVVM.BinderExtensions.md)



## Remarks

Binder failures are logged once with context and the original exception details.
A live Unity object owner is used as the Console context.

## Methods

### BindSafely\<T\>\(T?, in FindBindableMemberResult, object, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds <code class="paramref">binder</code> to the found member.

```csharp
public static void BindSafely<T>(this T? binder, in FindBindableMemberResult result, object owner, string? memberName = null) where T : IBinder
```

#### Parameters

`binder` T?

The binder to bind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The member lookup result; an unsuccessful lookup does nothing.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)

The owner acquiring the binding, also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binders and logs binder exceptions.

### BindSafely\<T\>\(T?, in BindContext, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_}

Binds <code class="paramref">binder</code> using the given context.

```csharp
public static void BindSafely<T>(this T? binder, in BindContext context, string? memberName = null) where T : IBinder
```

#### Parameters

`binder` T?

The binder to bind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and owner; the owner is also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binders and logs binder exceptions.

### BindSafely\<T\>\(T\[\]?, in FindBindableMemberResult, object, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0___Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds each binder to the found member.

```csharp
public static void BindSafely<T>(this T[]? binders, in FindBindableMemberResult result, object owner, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` T\[\]?

The binders to bind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The member lookup result; an unsuccessful lookup does nothing.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)

The owner acquiring the binding, also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries, reporting them under <code>DEBUG</code>.</p>
<p>Logs binder exceptions and continues with the remaining entries.</p>

### BindSafely\<T\>\(T\[\]?, in BindContext, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0___Aspid_MVVM_BindContext__System_String_}

Binds each binder using the given context.

```csharp
public static void BindSafely<T>(this T[]? binders, in BindContext context, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` T\[\]?

The binders to bind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and owner; the owner is also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries, reporting them under <code>DEBUG</code>.</p>
<p>Logs binder exceptions and continues with the remaining entries.</p>

### BindSafely\<T\>\(List\<T\>?, in FindBindableMemberResult, object, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds each binder to the found member.

```csharp
public static void BindSafely<T>(this List<T>? binders, in FindBindableMemberResult result, object owner, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The binders to bind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The member lookup result; an unsuccessful lookup does nothing.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)

The owner acquiring the binding, also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries, reporting them under <code>DEBUG</code>.</p>
<p>Logs binder exceptions and continues with the remaining entries.</p>

### BindSafely\<T\>\(List\<T\>?, in BindContext, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_BindContext__System_String_}

Binds each binder using the given context.

```csharp
public static void BindSafely<T>(this List<T>? binders, in BindContext context, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The binders to bind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and owner; the owner is also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries, reporting them under <code>DEBUG</code>.</p>
<p>Logs binder exceptions and continues with the remaining entries.</p>

### BindSafely\<T\>\(IEnumerable\<T\>?, in FindBindableMemberResult, object, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds each binder to the found member.

```csharp
public static void BindSafely<T>(this IEnumerable<T>? binders, in FindBindableMemberResult result, object owner, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The binders to bind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The member lookup result; an unsuccessful lookup does nothing.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)

The owner acquiring the binding, also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries, reporting them under <code>DEBUG</code>.</p>
<p>Logs binder exceptions and continues with the remaining entries.</p>
<p>Enumeration and enumerator disposal failures propagate; a single failure retains its original stack trace.</p>

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Both enumeration and enumerator disposal fail.

### BindSafely\<T\>\(IEnumerable\<T\>?, in BindContext, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_BindContext__System_String_}

Binds each binder using the given context.

```csharp
public static void BindSafely<T>(this IEnumerable<T>? binders, in BindContext context, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The binders to bind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and owner; the owner is also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries, reporting them under <code>DEBUG</code>.</p>
<p>Logs binder exceptions and continues with the remaining entries.</p>
<p>Enumeration and enumerator disposal failures propagate; a single failure retains its original stack trace.</p>

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Both enumeration and enumerator disposal fail.

### Unbind\<T\>\(T\[\]?, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_Unbind__1___0___Aspid_MVVM_UnbindContext__}

Releases each binder whose owner matches the context.

```csharp
public static void Unbind<T>(this T[]? binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` T\[\]?

The binders to release; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner releasing the bindings.

#### Type Parameters

`T`

The binder type.

#### Remarks

Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> entries and finishes cleanup before reporting failures; a single failure retains its original stack trace.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> is uninitialized.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple cleanup or enumeration operations fail.

### Unbind\<T\>\(List\<T\>?, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_Unbind__1_System_Collections_Generic_List___0__Aspid_MVVM_UnbindContext__}

Releases each binder whose owner matches the context.

```csharp
public static void Unbind<T>(this List<T>? binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The binders to release; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner releasing the bindings.

#### Type Parameters

`T`

The binder type.

#### Remarks

Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> entries and finishes cleanup before reporting failures; a single failure retains its original stack trace.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> is uninitialized.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple cleanup or enumeration operations fail.

### Unbind\<T\>\(IEnumerable\<T\>?, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_Unbind__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_UnbindContext__}

Releases each binder whose owner matches the context.

```csharp
public static void Unbind<T>(this IEnumerable<T>? binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The binders to release; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner releasing the bindings.

#### Type Parameters

`T`

The binder type.

#### Remarks

Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> entries and reports cleanup and enumeration failures after processing reachable entries.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> is uninitialized.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple cleanup or enumeration operations fail.

### UnbindSafely\<T\>\(T?, in UnbindContext, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_}

Unbinds <code class="paramref">binder</code>.

```csharp
public static void UnbindSafely<T>(this T? binder, in UnbindContext context, string? memberName = null) where T : IBinder
```

#### Parameters

`binder` T?

The binder to unbind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner releasing the binding, also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binders and logs binder exceptions.

### UnbindSafely\<T\>\(T\[\]?, in UnbindContext, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0___Aspid_MVVM_UnbindContext__System_String_}

Unbinds each binder.

```csharp
public static void UnbindSafely<T>(this T[]? binders, in UnbindContext context, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` T\[\]?

The binders to unbind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner releasing the bindings, also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries, reporting them under <code>DEBUG</code>.</p>
<p>Logs binder exceptions and continues with the remaining entries.</p>

### UnbindSafely\<T\>\(List\<T\>?, in UnbindContext, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_UnbindContext__System_String_}

Unbinds each binder.

```csharp
public static void UnbindSafely<T>(this List<T>? binders, in UnbindContext context, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The binders to unbind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner releasing the bindings, also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries, reporting them under <code>DEBUG</code>.</p>
<p>Logs binder exceptions and continues with the remaining entries.</p>

### UnbindSafely\<T\>\(IEnumerable\<T\>?, in UnbindContext, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_UnbindContext__System_String_}

Unbinds each binder.

```csharp
public static void UnbindSafely<T>(this IEnumerable<T>? binders, in UnbindContext context, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The binders to unbind; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner releasing the bindings, also used for diagnostics.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The binder field or property name; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries, reporting them under <code>DEBUG</code>.</p>
<p>Logs binder exceptions and continues with the remaining entries.</p>
<p>Enumeration and enumerator disposal failures propagate; a single failure retains its original stack trace.</p>

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Both enumeration and enumerator disposal fail.
