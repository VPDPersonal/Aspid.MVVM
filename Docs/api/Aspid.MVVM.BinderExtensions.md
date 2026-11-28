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

Provides extension methods for [`IBinder`](Aspid.MVVM.IBinder.md) and binder collections.

```csharp
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class BinderExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[BinderExtensions](Aspid.MVVM.BinderExtensions.md)



## Methods

### Bind\<T\>\(T\[\], in BindContext\) {#Aspid_MVVM_BinderExtensions_Bind__1___0___Aspid_MVVM_BindContext__}

Attaches each binder to the specified member.

```csharp
public static void Bind<T>(this T[] binders, in BindContext context) where T : IBinder
```

#### Parameters

`binders` T\[\]

The binders to bind.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

#### Type Parameters

`T`

The binder type.

#### Remarks

Stops at the first failure without rolling back earlier bindings.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">binders</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

An entry is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Bind\<T\>\(in Span\<T\>, in BindContext\) {#Aspid_MVVM_BinderExtensions_Bind__1_System_Span___0___Aspid_MVVM_BindContext__}

Attaches each binder to the specified member.

```csharp
public static void Bind<T>(this in Span<T> binders, in BindContext context) where T : IBinder
```

#### Parameters

`binders` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The binders to bind.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

#### Type Parameters

`T`

The binder type.

#### Remarks

Stops at the first failure without rolling back earlier bindings.

#### Exceptions

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

An entry is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Bind\<T\>\(in ReadOnlySpan\<T\>, in BindContext\) {#Aspid_MVVM_BinderExtensions_Bind__1_System_ReadOnlySpan___0___Aspid_MVVM_BindContext__}

Attaches each binder to the specified member.

```csharp
public static void Bind<T>(this in ReadOnlySpan<T> binders, in BindContext context) where T : IBinder
```

#### Parameters

`binders` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The binders to bind.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

#### Type Parameters

`T`

The binder type.

#### Remarks

Stops at the first failure without rolling back earlier bindings.

#### Exceptions

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

An entry is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Bind\<T\>\(List\<T\>, in BindContext\) {#Aspid_MVVM_BinderExtensions_Bind__1_System_Collections_Generic_List___0__Aspid_MVVM_BindContext__}

Attaches each binder to the specified member.

```csharp
public static void Bind<T>(this List<T> binders, in BindContext context) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The binders to bind.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

#### Type Parameters

`T`

The binder type.

#### Remarks

Stops at the first failure without rolling back earlier bindings.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">binders</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

An entry is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Bind\<T\>\(IEnumerable\<T\>, in BindContext\) {#Aspid_MVVM_BinderExtensions_Bind__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_BindContext__}

Attaches each binder to the specified member.

```csharp
public static void Bind<T>(this IEnumerable<T> binders, in BindContext context) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The binders to bind.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

#### Type Parameters

`T`

The binder type.

#### Remarks

Stops at the first failure without rolling back earlier bindings.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">binders</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

An entry is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### BindSafely\(IBinder?, in BindContext\) {#Aspid_MVVM_BinderExtensions_BindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_BindContext__}

Attaches the binder to the specified member.

```csharp
public static void BindSafely(this IBinder? binder, in BindContext context)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)?

The binder to bind. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

### BindSafely\<T\>\(T?\[\]?, in BindContext\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0___Aspid_MVVM_BindContext__}

Attaches each binder to the specified member, skipping <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

```csharp
public static void BindSafely<T>(this T?[]? binders, in BindContext context) where T : IBinder
```

#### Parameters

`binders` T?\[\]?

The binders to bind. Pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip the operation.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

#### Type Parameters

`T`

The binder type.

#### Remarks

Stops at the first failure without rolling back earlier bindings.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no member or owner.

### BindSafely\<T\>\(in Span\<T?\>, in BindContext\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Span___0___Aspid_MVVM_BindContext__}

Attaches each binder to the specified member, skipping <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

```csharp
public static void BindSafely<T>(this in Span<T?> binders, in BindContext context) where T : IBinder
```

#### Parameters

`binders` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T?\>

The binders to bind.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

#### Type Parameters

`T`

The binder type.

#### Remarks

Stops at the first failure without rolling back earlier bindings.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no member or owner.

### BindSafely\<T\>\(in ReadOnlySpan\<T?\>, in BindContext\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_ReadOnlySpan___0___Aspid_MVVM_BindContext__}

Attaches each binder to the specified member, skipping <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

```csharp
public static void BindSafely<T>(this in ReadOnlySpan<T?> binders, in BindContext context) where T : IBinder
```

#### Parameters

`binders` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T?\>

The binders to bind.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

#### Type Parameters

`T`

The binder type.

#### Remarks

Stops at the first failure without rolling back earlier bindings.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no member or owner.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no member or owner.

### BindSafely\<T\>\(List\<T?\>?, in BindContext\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_BindContext__}

Attaches each binder to the specified member, skipping <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

```csharp
public static void BindSafely<T>(this List<T?>? binders, in BindContext context) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T?\>?

The binders to bind. Pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip the operation.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

#### Type Parameters

`T`

The binder type.

#### Remarks

Stops at the first failure without rolling back earlier bindings.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no member or owner.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no member or owner.

### BindSafely\<T\>\(IEnumerable\<T?\>?, in BindContext\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_BindContext__}

Attaches each binder to the specified member, skipping <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

```csharp
public static void BindSafely<T>(this IEnumerable<T?>? binders, in BindContext context) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T?\>?

The binders to bind. Pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip the operation.

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and binding owner.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Stops at the first failure without rolling back earlier bindings.</p>
<p>An exception during enumerator disposal can mask the binding failure.</p>

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no member or owner.

### Unbind\<T\>\(T\[\], in UnbindContext\) {#Aspid_MVVM_BinderExtensions_Unbind__1___0___Aspid_MVVM_UnbindContext__}

Releases each binding held by the specified owner.

```csharp
public static void Unbind<T>(this T[] binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` T\[\]

The binders to unbind.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

#### Type Parameters

`T`

The binder type.

#### Remarks

Continues after binder failures and rethrows a single failure with its original stack trace.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">binders</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> entry.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple binders fail during cleanup.

### Unbind\<T\>\(Span\<T\>, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_Unbind__1_System_Span___0__Aspid_MVVM_UnbindContext__}

Releases each binding held by the specified owner.

```csharp
public static void Unbind<T>(this Span<T> binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The binders to unbind.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

#### Type Parameters

`T`

The binder type.

#### Remarks

Continues after binder failures and rethrows a single failure with its original stack trace.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> entry.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple binders fail during cleanup.

### Unbind\<T\>\(ReadOnlySpan\<T\>, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_Unbind__1_System_ReadOnlySpan___0__Aspid_MVVM_UnbindContext__}

Releases each binding held by the specified owner.

```csharp
public static void Unbind<T>(this ReadOnlySpan<T> binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The binders to unbind.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

#### Type Parameters

`T`

The binder type.

#### Remarks

Continues after binder failures and rethrows a single failure with its original stack trace.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> entry.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple binders fail during cleanup.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

### Unbind\<T\>\(List\<T\>, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_Unbind__1_System_Collections_Generic_List___0__Aspid_MVVM_UnbindContext__}

Releases each binding held by the specified owner.

```csharp
public static void Unbind<T>(this List<T> binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The binders to unbind.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Continues after binder failures and rethrows a single failure with its original stack trace.</p>
<p>Modifying the list stops enumeration.</p>

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">binders</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> entry.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple cleanup or enumeration failures occur.

### Unbind\<T\>\(IEnumerable\<T\>, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_Unbind__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_UnbindContext__}

Releases each binding held by the specified owner.

```csharp
public static void Unbind<T>(this IEnumerable<T> binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The binders to unbind.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Continues after binder failures and rethrows a single failure with its original stack trace.</p>
<p>Enumeration errors can leave later binders unprocessed.</p>

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">binders</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> entry.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple cleanup or enumeration failures occur.

### UnbindSafely\(IBinder?, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_UnbindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_UnbindContext__}

Releases the binding held by the specified owner.

```csharp
public static void UnbindSafely(this IBinder? binder, in UnbindContext context)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)?

The binder to unbind. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

### UnbindSafely\<T\>\(T?\[\]?, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0___Aspid_MVVM_UnbindContext__}

Releases each binding held by the specified owner, skipping <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

```csharp
public static void UnbindSafely<T>(this T?[]? binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` T?\[\]?

The binders to unbind. Pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip the operation.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

#### Type Parameters

`T`

The binder type.

#### Remarks

Continues after binder failures and rethrows a single failure with its original stack trace.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple binders fail during cleanup.

### UnbindSafely\<T\>\(in Span\<T?\>, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1_System_Span___0___Aspid_MVVM_UnbindContext__}

Releases each binding held by the specified owner, skipping <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

```csharp
public static void UnbindSafely<T>(this in Span<T?> binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T?\>

The binders to unbind.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

#### Type Parameters

`T`

The binder type.

#### Remarks

Continues after binder failures and rethrows a single failure with its original stack trace.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple binders fail during cleanup.

### UnbindSafely\<T\>\(in ReadOnlySpan\<T?\>, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1_System_ReadOnlySpan___0___Aspid_MVVM_UnbindContext__}

Releases each binding held by the specified owner, skipping <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

```csharp
public static void UnbindSafely<T>(this in ReadOnlySpan<T?> binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T?\>

The binders to unbind.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

#### Type Parameters

`T`

The binder type.

#### Remarks

Continues after binder failures and rethrows a single failure with its original stack trace.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple binders fail during cleanup.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

### UnbindSafely\<T\>\(List\<T?\>?, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_UnbindContext__}

Releases each binding held by the specified owner, skipping <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

```csharp
public static void UnbindSafely<T>(this List<T?>? binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T?\>?

The binders to unbind. Pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip the operation.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Continues after binder failures and rethrows a single failure with its original stack trace.</p>
<p>Modifying the list stops enumeration.</p>

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple cleanup or enumeration failures occur.

### UnbindSafely\<T\>\(IEnumerable\<T?\>?, in UnbindContext\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_UnbindContext__}

Releases each binding held by the specified owner, skipping <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

```csharp
public static void UnbindSafely<T>(this IEnumerable<T?>? binders, in UnbindContext context) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T?\>?

The binders to unbind. Pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip the operation.

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner that acquired the binding.

#### Type Parameters

`T`

The binder type.

#### Remarks

<p>Continues after binder failures and rethrows a single failure with its original stack trace.</p>
<p>Enumeration errors can leave later binders unprocessed.</p>

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> has no owner.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple cleanup or enumeration failures occur.
