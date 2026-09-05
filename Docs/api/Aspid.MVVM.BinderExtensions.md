---
title: "Class BinderExtensions"
sidebar_label: "BinderExtensions"
description: "Class BinderExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BinderExtensions {#Aspid_MVVM_BinderExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Provides extension methods for safely binding and unbinding [`IBinder`](Aspid.MVVM.IBinder.md) instances to [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) targets.
Null-safe variants guard against <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binders or collections.

```csharp
public static class BinderExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BinderExtensions](Aspid.MVVM.BinderExtensions.md)



## Methods

### BindSafely\<T\>\(T?, in FindBindableMemberResult, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds a single binder to the provided [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) if the bindable member was found.

```csharp
public static void BindSafely<T>(this T? binder, in FindBindableMemberResult result, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binder` T?

The binder instance to bind.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The result of a bindable member lookup.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Unused; accepted for signature parity with the collection overloads.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Unused; accepted for signature parity with the collection overloads.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

### BindSafely\<T\>\(T?, IBinderAdder, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_}

Safely binds a single binder to the specified binder adder.

```csharp
public static void BindSafely<T>(this T? binder, IBinderAdder binderAdder, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binder` T?

The binder instance to bind.

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The binder adder to bind to.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Unused; accepted for signature parity with the collection overloads.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Unused; accepted for signature parity with the collection overloads.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

### BindSafely\<T\>\(T\[\]?, in FindBindableMemberResult, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0___Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds an array of binders to the provided [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) if the bindable member was found.

```csharp
public static void BindSafely<T>(this T[]? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` T\[\]?

The array of binders to bind.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The result of a bindable member lookup.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional name of the field that holds <code class="paramref">binders</code>, used in diagnostics.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

#### Exceptions

 [BindSafelyNullReferenceException](Aspid.MVVM.BindSafelyNullReferenceException.md)

Thrown if any individual binder in the array is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
In Unity (<code>UNITY_2020_3_OR_NEWER</code>), skips the <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder instead of throwing.
When <code>DEBUG</code> is also defined, additionally logs an error via <code>UnityEngine.Debug.LogError</code>.

### BindSafely\<T\>\(T\[\]?, IBinderAdder, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0___Aspid_MVVM_IBinderAdder_System_Object_System_String_}

Safely binds an array of binders to the specified binder adder.

```csharp
public static void BindSafely<T>(this T[]? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` T\[\]?

The array of binders.

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The binder adder to bind to.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional name of the field that holds <code class="paramref">binders</code>, used in diagnostics.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

#### Exceptions

 [BindSafelyNullReferenceException](Aspid.MVVM.BindSafelyNullReferenceException.md)

Thrown if any individual binder in the array is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
In Unity (<code>UNITY_2020_3_OR_NEWER</code>), skips the <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder instead of throwing.
When <code>DEBUG</code> is also defined, additionally logs an error via <code>UnityEngine.Debug.LogError</code>.

### BindSafely\<T\>\(List\<T\>?, in FindBindableMemberResult, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds a list of binders to the provided [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) if the bindable member was found.

```csharp
public static void BindSafely<T>(this List<T>? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The list of binders.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The result of a bindable member lookup.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional name of the field that holds <code class="paramref">binders</code>, used in diagnostics.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

#### Exceptions

 [BindSafelyNullReferenceException](Aspid.MVVM.BindSafelyNullReferenceException.md)

Thrown if any individual binder in the list is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
In Unity (<code>UNITY_2020_3_OR_NEWER</code>), skips the <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder instead of throwing.
When <code>DEBUG</code> is also defined, additionally logs an error via <code>UnityEngine.Debug.LogError</code>.

### BindSafely\<T\>\(List\<T\>?, IBinderAdder, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_IBinderAdder_System_Object_System_String_}

Safely binds a list of binders to the specified binder adder.

```csharp
public static void BindSafely<T>(this List<T>? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The list of binders.

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The binder adder to bind to.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional name of the field that holds <code class="paramref">binders</code>, used in diagnostics.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

#### Exceptions

 [BindSafelyNullReferenceException](Aspid.MVVM.BindSafelyNullReferenceException.md)

Thrown if any individual binder in the list is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
In Unity (<code>UNITY_2020_3_OR_NEWER</code>), skips the <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder instead of throwing.
When <code>DEBUG</code> is also defined, additionally logs an error via <code>UnityEngine.Debug.LogError</code>.

### BindSafely\<T\>\(IEnumerable\<T\>?, in FindBindableMemberResult, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds an enumerable of binders to the provided [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) if the bindable member was found.

```csharp
public static void BindSafely<T>(this IEnumerable<T>? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The enumerable of binders.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The result of a bindable member lookup.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional name of the field that holds <code class="paramref">binders</code>, used in diagnostics.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

#### Exceptions

 [BindSafelyNullReferenceException](Aspid.MVVM.BindSafelyNullReferenceException.md)

Thrown if any individual binder in the sequence is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
In Unity (<code>UNITY_2020_3_OR_NEWER</code>), skips the <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder instead of throwing.
When <code>DEBUG</code> is also defined, additionally logs an error via <code>UnityEngine.Debug.LogError</code>.

### BindSafely\<T\>\(IEnumerable\<T\>?, IBinderAdder, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_IBinderAdder_System_Object_System_String_}

Safely binds an enumerable of binders to the specified binder adder.

```csharp
public static void BindSafely<T>(this IEnumerable<T>? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The enumerable of binders.

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The binder adder to bind to.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional name of the field that holds <code class="paramref">binders</code>, used in diagnostics.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

#### Exceptions

 [BindSafelyNullReferenceException](Aspid.MVVM.BindSafelyNullReferenceException.md)

Thrown if any individual binder in the sequence is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
In Unity (<code>UNITY_2020_3_OR_NEWER</code>), skips the <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder instead of throwing.
When <code>DEBUG</code> is also defined, additionally logs an error via <code>UnityEngine.Debug.LogError</code>.

### UnbindSafely\<T\>\(T?, object?, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_}

Safely unbinds a single binder.

```csharp
public static void UnbindSafely<T>(this T? binder, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binder` T?

The binder instance to unbind.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Unused; accepted for signature parity with the collection overloads.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Unused; accepted for signature parity with the collection overloads.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

### UnbindSafely\<T\>\(T\[\]?, object?, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0___System_Object_System_String_}

Safely unbinds an array of binders.

```csharp
public static void UnbindSafely<T>(this T[]? binders, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` T\[\]?

The array of binders to unbind.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional name of the field that holds <code class="paramref">binders</code>, used in diagnostics.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

#### Exceptions

 [UnbindSafelyNullReferenceException](Aspid.MVVM.UnbindSafelyNullReferenceException.md)

Thrown if any element in the sequence is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
In Unity builds (<code>UNITY_2020_3_OR_NEWER</code>), skips the <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder instead of throwing.
When <code>DEBUG</code> is also defined, additionally logs an error via <code>UnityEngine.Debug.LogError</code>.

### UnbindSafely\<T\>\(List\<T\>?, object?, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1_System_Collections_Generic_List___0__System_Object_System_String_}

Safely unbinds a list of binders.

```csharp
public static void UnbindSafely<T>(this List<T>? binders, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The list of binders to unbind.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional name of the field that holds <code class="paramref">binders</code>, used in diagnostics.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

#### Exceptions

 [UnbindSafelyNullReferenceException](Aspid.MVVM.UnbindSafelyNullReferenceException.md)

Thrown if any element in the sequence is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
In Unity builds (<code>UNITY_2020_3_OR_NEWER</code>), skips the <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder instead of throwing.
When <code>DEBUG</code> is also defined, additionally logs an error via <code>UnityEngine.Debug.LogError</code>.

### UnbindSafely\<T\>\(IEnumerable\<T\>?, object?, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1_System_Collections_Generic_IEnumerable___0__System_Object_System_String_}

Safely unbinds a sequence of binders.

```csharp
public static void UnbindSafely<T>(this IEnumerable<T>? binders, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The enumerable of binders to unbind.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional name of the field that holds <code class="paramref">binders</code>, used in diagnostics.

#### Type Parameters

`T` 

The binder type that implements [`IBinder`](Aspid.MVVM.IBinder.md).

#### Exceptions

 [UnbindSafelyNullReferenceException](Aspid.MVVM.UnbindSafelyNullReferenceException.md)

Thrown if any element in the sequence is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
In Unity builds (<code>UNITY_2020_3_OR_NEWER</code>), skips the <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder instead of throwing.
When <code>DEBUG</code> is also defined, additionally logs an error via <code>UnityEngine.Debug.LogError</code>.

