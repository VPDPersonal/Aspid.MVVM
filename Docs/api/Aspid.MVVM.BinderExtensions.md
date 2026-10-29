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

Provides extension methods for binding and unbinding [`IBinder`](Aspid.MVVM.IBinder.md) instances and their collections.

```csharp
public static class BinderExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BinderExtensions](Aspid.MVVM.BinderExtensions.md)



## Methods

### BindSafely\<T\>\(T?, in FindBindableMemberResult, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds <code class="paramref">binder</code> to the found bindable member; does nothing when <code class="paramref">binder</code> is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or no member was found.

```csharp
public static void BindSafely<T>(this T? binder, in FindBindableMemberResult result, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binder` T?

The binder to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The result of [`IViewModel.FindBindableMember`](Aspid.MVVM.IViewModel.md).

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Unused; matches the collection overloads.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Unused; matches the collection overloads.

#### Type Parameters

`T` 

The binder type.

### BindSafely\<T\>\(T?, IBinderAdder, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_}

Binds <code class="paramref">binder</code> to <code class="paramref">binderAdder</code>; does nothing when <code class="paramref">binder</code> is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static void BindSafely<T>(this T? binder, IBinderAdder binderAdder, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binder` T?

The binder to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The bindable member to bind to.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Unused; matches the collection overloads.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Unused; matches the collection overloads.

#### Type Parameters

`T` 

The binder type.

### BindSafely\<T\>\(T\[\]?, in FindBindableMemberResult, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0___Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds each binder in <code class="paramref">binders</code> to the found bindable member; does nothing when no member was found.

```csharp
public static void BindSafely<T>(this T[]? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` T\[\]?

The binders to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The result of [`IViewModel.FindBindableMember`](Aspid.MVVM.IViewModel.md).

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The View that holds <code class="paramref">binders</code>, named in log messages and used as their context when it is a Unity object; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The name of the member that holds <code class="paramref">binders</code>, named in log messages; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T` 

The binder type.

#### Remarks

<p>Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binder; under <code>DEBUG</code>, also logs an error.</p>
<p>Logs an exception thrown by a binder and goes on to bind the rest.</p>

### BindSafely\<T\>\(T\[\]?, IBinderAdder, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1___0___Aspid_MVVM_IBinderAdder_System_Object_System_String_}

Binds each binder in <code class="paramref">binders</code> to <code class="paramref">binderAdder</code>.

```csharp
public static void BindSafely<T>(this T[]? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` T\[\]?

The binders to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The bindable member to bind to.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The View that holds <code class="paramref">binders</code>, named in log messages and used as their context when it is a Unity object; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The name of the member that holds <code class="paramref">binders</code>, named in log messages; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T` 

The binder type.

#### Remarks

<p>Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binder; under <code>DEBUG</code>, also logs an error.</p>
<p>Logs an exception thrown by a binder and goes on to bind the rest.</p>

### BindSafely\<T\>\(List\<T\>?, in FindBindableMemberResult, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds each binder in <code class="paramref">binders</code> to the found bindable member; does nothing when no member was found.

```csharp
public static void BindSafely<T>(this List<T>? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The binders to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The result of [`IViewModel.FindBindableMember`](Aspid.MVVM.IViewModel.md).

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The View that holds <code class="paramref">binders</code>, named in log messages and used as their context when it is a Unity object; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The name of the member that holds <code class="paramref">binders</code>, named in log messages; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T` 

The binder type.

#### Remarks

<p>Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binder; under <code>DEBUG</code>, also logs an error.</p>
<p>Logs an exception thrown by a binder and goes on to bind the rest.</p>

### BindSafely\<T\>\(List\<T\>?, IBinderAdder, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_IBinderAdder_System_Object_System_String_}

Binds each binder in <code class="paramref">binders</code> to <code class="paramref">binderAdder</code>.

```csharp
public static void BindSafely<T>(this List<T>? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The binders to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The bindable member to bind to.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The View that holds <code class="paramref">binders</code>, named in log messages and used as their context when it is a Unity object; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The name of the member that holds <code class="paramref">binders</code>, named in log messages; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T` 

The binder type.

#### Remarks

<p>Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binder; under <code>DEBUG</code>, also logs an error.</p>
<p>Logs an exception thrown by a binder and goes on to bind the rest.</p>

### BindSafely\<T\>\(IEnumerable\<T\>?, in FindBindableMemberResult, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_}

Binds each binder in <code class="paramref">binders</code> to the found bindable member; does nothing when no member was found.

```csharp
public static void BindSafely<T>(this IEnumerable<T>? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The binders to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`result` [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The result of [`IViewModel.FindBindableMember`](Aspid.MVVM.IViewModel.md).

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The View that holds <code class="paramref">binders</code>, named in log messages and used as their context when it is a Unity object; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The name of the member that holds <code class="paramref">binders</code>, named in log messages; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T` 

The binder type.

#### Remarks

<p>Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binder; under <code>DEBUG</code>, also logs an error.</p>
<p>Logs an exception thrown by a binder and goes on to bind the rest.</p>

### BindSafely\<T\>\(IEnumerable\<T\>?, IBinderAdder, object?, string?\) {#Aspid_MVVM_BinderExtensions_BindSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_IBinderAdder_System_Object_System_String_}

Binds each binder in <code class="paramref">binders</code> to <code class="paramref">binderAdder</code>.

```csharp
public static void BindSafely<T>(this IEnumerable<T>? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The binders to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The bindable member to bind to.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The View that holds <code class="paramref">binders</code>, named in log messages and used as their context when it is a Unity object; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The name of the member that holds <code class="paramref">binders</code>, named in log messages; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T` 

The binder type.

#### Remarks

<p>Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binder; under <code>DEBUG</code>, also logs an error.</p>
<p>Logs an exception thrown by a binder and goes on to bind the rest.</p>

### UnbindSafely\<T\>\(T?, object?, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_}

Unbinds <code class="paramref">binder</code>; does nothing when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a destroyed Unity object.

```csharp
public static void UnbindSafely<T>(this T? binder, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binder` T?

The binder to unbind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

Unused; matches the collection overloads.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Unused; matches the collection overloads.

#### Type Parameters

`T` 

The binder type.

### UnbindSafely\<T\>\(T\[\]?, object?, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0___System_Object_System_String_}

Unbinds each binder in <code class="paramref">binders</code>.

```csharp
public static void UnbindSafely<T>(this T[]? binders, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` T\[\]?

The binders to unbind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The View that holds <code class="paramref">binders</code>, named in log messages and used as their context when it is a Unity object; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The name of the member that holds <code class="paramref">binders</code>, named in log messages; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T` 

The binder type.

#### Remarks

<p>Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binder; under <code>DEBUG</code>, also logs an error.</p>
<p>Logs an exception thrown by a binder and goes on to unbind the rest.</p>

### UnbindSafely\<T\>\(List\<T\>?, object?, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1_System_Collections_Generic_List___0__System_Object_System_String_}

Unbinds each binder in <code class="paramref">binders</code>.

```csharp
public static void UnbindSafely<T>(this List<T>? binders, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The binders to unbind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The View that holds <code class="paramref">binders</code>, named in log messages and used as their context when it is a Unity object; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The name of the member that holds <code class="paramref">binders</code>, named in log messages; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T` 

The binder type.

#### Remarks

<p>Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binder; under <code>DEBUG</code>, also logs an error.</p>
<p>Logs an exception thrown by a binder and goes on to unbind the rest.</p>

### UnbindSafely\<T\>\(IEnumerable\<T\>?, object?, string?\) {#Aspid_MVVM_BinderExtensions_UnbindSafely__1_System_Collections_Generic_IEnumerable___0__System_Object_System_String_}

Unbinds each binder in <code class="paramref">binders</code>.

```csharp
public static void UnbindSafely<T>(this IEnumerable<T>? binders, object? owner = null, string? memberName = null) where T : IBinder
```

#### Parameters

`binders` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The binders to unbind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The View that holds <code class="paramref">binders</code>, named in log messages and used as their context when it is a Unity object; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

`memberName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The name of the member that holds <code class="paramref">binders</code>, named in log messages; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

#### Type Parameters

`T` 

The binder type.

#### Remarks

<p>Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed binder; under <code>DEBUG</code>, also logs an error.</p>
<p>Logs an exception thrown by a binder and goes on to unbind the rest.</p>

