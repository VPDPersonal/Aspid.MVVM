---
title: "Class ViewExtensions"
sidebar_label: "ViewExtensions"
description: "Class ViewExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewExtensions {#Aspid_MVVM_ViewExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Provides extension methods for [`IView`](Aspid.MVVM.IView.md).

```csharp
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class ViewExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[ViewExtensions](Aspid.MVVM.ViewExtensions.md)



## Methods

### DeinitializeView\(IView\) {#Aspid_MVVM_ViewExtensions_DeinitializeView_Aspid_MVVM_IView_}

Unbinds the View from its current ViewModel.

```csharp
public static IViewModel? DeinitializeView(this IView view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)

The View to deinitialize.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View had none.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### DeinitializeViewSafely\(IView?\) {#Aspid_MVVM_ViewExtensions_DeinitializeViewSafely_Aspid_MVVM_IView_}

Unbinds the View from its current ViewModel.

```csharp
public static IViewModel? DeinitializeViewSafely(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The View to deinitialize. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View was skipped or had none.

### DeinitializeViews\<T\>\(T\[\]\) {#Aspid_MVVM_ViewExtensions_DeinitializeViews__1___0___}

Deinitializes each View and rethrows any failures after cleanup.

```csharp
public static void DeinitializeViews<T>(this T[] views) where T : IView
```

#### Parameters

`views` T\[\]

The Views to deinitialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a destroyed View.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DeinitializeViews\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewExtensions_DeinitializeViews__1_System_Span___0__}

Deinitializes each View and rethrows any failures after cleanup.

```csharp
public static void DeinitializeViews<T>(this Span<T> views) where T : IView
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views to deinitialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a destroyed View.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DeinitializeViews\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewExtensions_DeinitializeViews__1_System_ReadOnlySpan___0__}

Deinitializes each View and rethrows any failures after cleanup.

```csharp
public static void DeinitializeViews<T>(this ReadOnlySpan<T> views) where T : IView
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views to deinitialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a destroyed View.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DeinitializeViews\<T\>\(List\<T\>\) {#Aspid_MVVM_ViewExtensions_DeinitializeViews__1_System_Collections_Generic_List___0__}

Deinitializes each View and rethrows any failures after cleanup.

```csharp
public static void DeinitializeViews<T>(this List<T> views) where T : IView
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The Views to deinitialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a destroyed View.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DeinitializeViews\<T\>\(IEnumerable\<T\>\) {#Aspid_MVVM_ViewExtensions_DeinitializeViews__1_System_Collections_Generic_IEnumerable___0__}

Deinitializes each View and rethrows any failures after cleanup.

```csharp
public static void DeinitializeViews<T>(this IEnumerable<T> views) where T : IView
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The Views to deinitialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a destroyed View.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DeinitializeViewsSafely\<T\>\(T\[\]?\) {#Aspid_MVVM_ViewExtensions_DeinitializeViewsSafely__1___0___}

Deinitializes each View and rethrows any failures after cleanup.

```csharp
public static void DeinitializeViewsSafely<T>(this T[]? views) where T : IView?
```

#### Parameters

`views` T\[\]?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DeinitializeViewsSafely\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewExtensions_DeinitializeViewsSafely__1_System_Span___0__}

Deinitializes each View and rethrows any failures after cleanup.

```csharp
public static void DeinitializeViewsSafely<T>(this Span<T> views) where T : IView?
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DeinitializeViewsSafely\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewExtensions_DeinitializeViewsSafely__1_System_ReadOnlySpan___0__}

Deinitializes each View and rethrows any failures after cleanup.

```csharp
public static void DeinitializeViewsSafely<T>(this ReadOnlySpan<T> views) where T : IView?
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DeinitializeViewsSafely\<T\>\(List\<T\>?\) {#Aspid_MVVM_ViewExtensions_DeinitializeViewsSafely__1_System_Collections_Generic_List___0__}

Deinitializes each View and rethrows any failures after cleanup.

```csharp
public static void DeinitializeViewsSafely<T>(this List<T>? views) where T : IView?
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DeinitializeViewsSafely\<T\>\(IEnumerable\<T\>?\) {#Aspid_MVVM_ViewExtensions_DeinitializeViewsSafely__1_System_Collections_Generic_IEnumerable___0__}

Deinitializes each View and rethrows any failures after cleanup.

```csharp
public static void DeinitializeViewsSafely<T>(this IEnumerable<T>? views) where T : IView?
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DestroyView\(IView\) {#Aspid_MVVM_ViewExtensions_DestroyView_Aspid_MVVM_IView_}

Cleans up the View and destroys its Unity object, if any, preserving the owning GameObject.

```csharp
public static IViewModel? DestroyView(this IView view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)

The View to destroy.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View had none.

#### Remarks

Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
It is immediate in Edit Mode and deferred in Play Mode.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Cleanup and destruction both fail.

### DestroyViewAndGameObject\(IView\) {#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_}

Cleans up the View and destroys its GameObject for components or the object itself for other Unity objects.

```csharp
public static IViewModel? DestroyViewAndGameObject(this IView view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)

The View to destroy.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View had none.

#### Remarks

Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
It is immediate in Edit Mode and deferred in Play Mode.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Cleanup and destruction both fail.

### DestroyViewAndGameObjectSafely\(IView?\) {#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObjectSafely_Aspid_MVVM_IView_}

Cleans up the View and destroys its GameObject for components or the object itself for other Unity objects.

```csharp
public static IViewModel? DestroyViewAndGameObjectSafely(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The View to destroy. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View was skipped or had none.

#### Remarks

Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
It is immediate in Edit Mode and deferred in Play Mode.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Cleanup and destruction both fail.

### DestroyViewSafely\(IView?\) {#Aspid_MVVM_ViewExtensions_DestroyViewSafely_Aspid_MVVM_IView_}

Cleans up the View and destroys its Unity object, if any, preserving the owning GameObject.

```csharp
public static IViewModel? DestroyViewSafely(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The View to destroy. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View was skipped or had none.

#### Remarks

Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
It is immediate in Edit Mode and deferred in Play Mode.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Cleanup and destruction both fail.

### DestroyViews\<T\>\(T\[\]\) {#Aspid_MVVM_ViewExtensions_DestroyViews__1___0___}

Calls [`ViewExtensions.DestroyView`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyView_Aspid_MVVM_IView_) on each View and rethrows failures after cleanup.

```csharp
public static void DestroyViews<T>(this T[] views) where T : IView
```

#### Parameters

`views` T\[\]

The Views to destroy.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViews\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViews__1_System_Span___0__}

Calls [`ViewExtensions.DestroyView`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyView_Aspid_MVVM_IView_) on each View and rethrows failures after cleanup.

```csharp
public static void DestroyViews<T>(this Span<T> views) where T : IView
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views to destroy.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViews\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViews__1_System_ReadOnlySpan___0__}

Calls [`ViewExtensions.DestroyView`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyView_Aspid_MVVM_IView_) on each View and rethrows failures after cleanup.

```csharp
public static void DestroyViews<T>(this ReadOnlySpan<T> views) where T : IView
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views to destroy.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViews\<T\>\(List\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViews__1_System_Collections_Generic_List___0__}

Calls [`ViewExtensions.DestroyView`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyView_Aspid_MVVM_IView_) on each View and rethrows failures after cleanup.

```csharp
public static void DestroyViews<T>(this List<T> views) where T : IView
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The Views to destroy.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViews\<T\>\(IEnumerable\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViews__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewExtensions.DestroyView`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyView_Aspid_MVVM_IView_) on each View and rethrows failures after cleanup.

```csharp
public static void DestroyViews<T>(this IEnumerable<T> views) where T : IView
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The Views to destroy.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewsAndGameObjects\<T\>\(T\[\]\) {#Aspid_MVVM_ViewExtensions_DestroyViewsAndGameObjects__1___0___}

Calls [`ViewExtensions.DestroyViewAndGameObject`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_) on each View and rethrows failures after cleanup.

```csharp
public static void DestroyViewsAndGameObjects<T>(this T[] views) where T : IView
```

#### Parameters

`views` T\[\]

The Views to destroy.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewsAndGameObjects\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViewsAndGameObjects__1_System_Span___0__}

Calls [`ViewExtensions.DestroyViewAndGameObject`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_) on each View and rethrows failures after cleanup.

```csharp
public static void DestroyViewsAndGameObjects<T>(this Span<T> views) where T : IView
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views to destroy.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewsAndGameObjects\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViewsAndGameObjects__1_System_ReadOnlySpan___0__}

Calls [`ViewExtensions.DestroyViewAndGameObject`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_) on each View and rethrows failures after cleanup.

```csharp
public static void DestroyViewsAndGameObjects<T>(this ReadOnlySpan<T> views) where T : IView
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views to destroy.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewsAndGameObjects\<T\>\(List\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViewsAndGameObjects__1_System_Collections_Generic_List___0__}

Calls [`ViewExtensions.DestroyViewAndGameObject`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_) on each View and rethrows failures after cleanup.

```csharp
public static void DestroyViewsAndGameObjects<T>(this List<T> views) where T : IView
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The Views to destroy.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewsAndGameObjects\<T\>\(IEnumerable\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViewsAndGameObjects__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewExtensions.DestroyViewAndGameObject`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_) on each View and rethrows failures after cleanup.

```csharp
public static void DestroyViewsAndGameObjects<T>(this IEnumerable<T> views) where T : IView
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The Views to destroy.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewsAndGameObjectsSafely\<T\>\(T\[\]?\) {#Aspid_MVVM_ViewExtensions_DestroyViewsAndGameObjectsSafely__1___0___}

Calls [`ViewExtensions.DestroyViewAndGameObjectSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObjectSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DestroyViewsAndGameObjectsSafely<T>(this T[]? views) where T : IView?
```

#### Parameters

`views` T\[\]?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DestroyViewsAndGameObjectsSafely\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViewsAndGameObjectsSafely__1_System_Span___0__}

Calls [`ViewExtensions.DestroyViewAndGameObjectSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObjectSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DestroyViewsAndGameObjectsSafely<T>(this Span<T> views) where T : IView?
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DestroyViewsAndGameObjectsSafely\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViewsAndGameObjectsSafely__1_System_ReadOnlySpan___0__}

Calls [`ViewExtensions.DestroyViewAndGameObjectSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObjectSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DestroyViewsAndGameObjectsSafely<T>(this ReadOnlySpan<T> views) where T : IView?
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DestroyViewsAndGameObjectsSafely\<T\>\(List\<T\>?\) {#Aspid_MVVM_ViewExtensions_DestroyViewsAndGameObjectsSafely__1_System_Collections_Generic_List___0__}

Calls [`ViewExtensions.DestroyViewAndGameObjectSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObjectSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DestroyViewsAndGameObjectsSafely<T>(this List<T>? views) where T : IView?
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DestroyViewsAndGameObjectsSafely\<T\>\(IEnumerable\<T\>?\) {#Aspid_MVVM_ViewExtensions_DestroyViewsAndGameObjectsSafely__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewExtensions.DestroyViewAndGameObjectSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObjectSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DestroyViewsAndGameObjectsSafely<T>(this IEnumerable<T>? views) where T : IView?
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DestroyViewsSafely\<T\>\(T\[\]?\) {#Aspid_MVVM_ViewExtensions_DestroyViewsSafely__1___0___}

Calls [`ViewExtensions.DestroyViewSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DestroyViewsSafely<T>(this T[]? views) where T : IView?
```

#### Parameters

`views` T\[\]?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DestroyViewsSafely\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViewsSafely__1_System_Span___0__}

Calls [`ViewExtensions.DestroyViewSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DestroyViewsSafely<T>(this Span<T> views) where T : IView?
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DestroyViewsSafely\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewExtensions_DestroyViewsSafely__1_System_ReadOnlySpan___0__}

Calls [`ViewExtensions.DestroyViewSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DestroyViewsSafely<T>(this ReadOnlySpan<T> views) where T : IView?
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DestroyViewsSafely\<T\>\(List\<T\>?\) {#Aspid_MVVM_ViewExtensions_DestroyViewsSafely__1_System_Collections_Generic_List___0__}

Calls [`ViewExtensions.DestroyViewSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DestroyViewsSafely<T>(this List<T>? views) where T : IView?
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DestroyViewsSafely\<T\>\(IEnumerable\<T\>?\) {#Aspid_MVVM_ViewExtensions_DestroyViewsSafely__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewExtensions.DestroyViewSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DestroyViewsSafely<T>(this IEnumerable<T>? views) where T : IView?
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DisposeView\<T\>\(T\) {#Aspid_MVVM_ViewExtensions_DisposeView__1___0_}

Calls [`Dispose`](https://learn.microsoft.com/dotnet/api/system.idisposable.dispose) on the View.

```csharp
public static IViewModel? DisposeView<T>(this T view) where T : class, IView, IDisposable
```

#### Parameters

`view` T

The View to clean up.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View had none.

#### Type Parameters

`T`

The disposable View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### DisposeView\(IView\) {#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_}

Disposes the View if it implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), or deinitializes it otherwise.

```csharp
public static IViewModel? DisposeView(this IView view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)

The View to clean up.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View had none.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### DisposeViewSafely\<T\>\(T?\) {#Aspid_MVVM_ViewExtensions_DisposeViewSafely__1___0_}

Calls [`Dispose`](https://learn.microsoft.com/dotnet/api/system.idisposable.dispose) on the View.

```csharp
public static IViewModel? DisposeViewSafely<T>(this T? view) where T : class, IView, IDisposable
```

#### Parameters

`view` T?

The View to clean up. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View was skipped or had none.

#### Type Parameters

`T`

The disposable View type.

### DisposeViewSafely\(IView?\) {#Aspid_MVVM_ViewExtensions_DisposeViewSafely_Aspid_MVVM_IView_}

Disposes the View if it implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), or deinitializes it otherwise.

```csharp
public static IViewModel? DisposeViewSafely(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The View to clean up. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View was skipped or had none.

### DisposeViews\<T\>\(T\[\]\) {#Aspid_MVVM_ViewExtensions_DisposeViews__1___0___}

Calls [`ViewExtensions.DisposeView`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DisposeViews<T>(this T[] views) where T : IView
```

#### Parameters

`views` T\[\]

The Views to clean up.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DisposeViews\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewExtensions_DisposeViews__1_System_Span___0__}

Calls [`ViewExtensions.DisposeView`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DisposeViews<T>(this Span<T> views) where T : IView
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views to clean up.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DisposeViews\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewExtensions_DisposeViews__1_System_ReadOnlySpan___0__}

Calls [`ViewExtensions.DisposeView`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DisposeViews<T>(this ReadOnlySpan<T> views) where T : IView
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views to clean up.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DisposeViews\<T\>\(List\<T\>\) {#Aspid_MVVM_ViewExtensions_DisposeViews__1_System_Collections_Generic_List___0__}

Calls [`ViewExtensions.DisposeView`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DisposeViews<T>(this List<T> views) where T : IView
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The Views to clean up.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DisposeViews\<T\>\(IEnumerable\<T\>\) {#Aspid_MVVM_ViewExtensions_DisposeViews__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewExtensions.DisposeView`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DisposeViews<T>(this IEnumerable<T> views) where T : IView
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The Views to clean up.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed View.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DisposeViewsSafely\<T\>\(T\[\]?\) {#Aspid_MVVM_ViewExtensions_DisposeViewsSafely__1___0___}

Calls [`ViewExtensions.DisposeViewSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeViewSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DisposeViewsSafely<T>(this T[]? views) where T : IView?
```

#### Parameters

`views` T\[\]?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DisposeViewsSafely\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewExtensions_DisposeViewsSafely__1_System_Span___0__}

Calls [`ViewExtensions.DisposeViewSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeViewSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DisposeViewsSafely<T>(this Span<T> views) where T : IView?
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DisposeViewsSafely\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewExtensions_DisposeViewsSafely__1_System_ReadOnlySpan___0__}

Calls [`ViewExtensions.DisposeViewSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeViewSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DisposeViewsSafely<T>(this ReadOnlySpan<T> views) where T : IView?
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DisposeViewsSafely\<T\>\(List\<T\>?\) {#Aspid_MVVM_ViewExtensions_DisposeViewsSafely__1_System_Collections_Generic_List___0__}

Calls [`ViewExtensions.DisposeViewSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeViewSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DisposeViewsSafely<T>(this List<T>? views) where T : IView?
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### DisposeViewsSafely\<T\>\(IEnumerable\<T\>?\) {#Aspid_MVVM_ViewExtensions_DisposeViewsSafely__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewExtensions.DisposeViewSafely`](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeViewSafely_Aspid_MVVM_IView_) on each View and rethrows any failures after cleanup.

```csharp
public static void DisposeViewsSafely<T>(this IEnumerable<T>? views) where T : IView?
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the Views.

### InitializeSafely\(IView?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_InitializeSafely_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_}

Initializes the View with <code class="paramref">viewModel</code>.

```csharp
public static void InitializeSafely(this IView? view, IViewModel? viewModel)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The View to initialize. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

### InitializeViews\<T\>\(T\[\], IViewModel\) {#Aspid_MVVM_ViewExtensions_InitializeViews__1___0___Aspid_MVVM_IViewModel_}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViews<T>(this T[] views, IViewModel viewModel) where T : IView
```

#### Parameters

`views` T\[\]

The Views to initialize.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to each View.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\<T\>\(Span\<T\>, IViewModel\) {#Aspid_MVVM_ViewExtensions_InitializeViews__1_System_Span___0__Aspid_MVVM_IViewModel_}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViews<T>(this Span<T> views, IViewModel viewModel) where T : IView
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views to initialize.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to each View.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\<T\>\(ReadOnlySpan\<T\>, IViewModel\) {#Aspid_MVVM_ViewExtensions_InitializeViews__1_System_ReadOnlySpan___0__Aspid_MVVM_IViewModel_}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViews<T>(this ReadOnlySpan<T> views, IViewModel viewModel) where T : IView
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views to initialize.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to each View.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\<T\>\(List\<T\>, IViewModel\) {#Aspid_MVVM_ViewExtensions_InitializeViews__1_System_Collections_Generic_List___0__Aspid_MVVM_IViewModel_}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViews<T>(this List<T> views, IViewModel viewModel) where T : IView
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The Views to initialize.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to each View.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\<T\>\(IEnumerable\<T\>, IViewModel\) {#Aspid_MVVM_ViewExtensions_InitializeViews__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_IViewModel_}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViews<T>(this IEnumerable<T> views, IViewModel viewModel) where T : IView
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The Views to initialize.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to each View.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViewsSafely\<T\>\(T\[\]?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_InitializeViewsSafely__1___0___Aspid_MVVM_IViewModel_}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViewsSafely<T>(this T[]? views, IViewModel? viewModel) where T : IView?
```

#### Parameters

`views` T\[\]?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

#### Type Parameters

`T`

The View type.

### InitializeViewsSafely\<T\>\(Span\<T\>, IViewModel?\) {#Aspid_MVVM_ViewExtensions_InitializeViewsSafely__1_System_Span___0__Aspid_MVVM_IViewModel_}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViewsSafely<T>(this Span<T> views, IViewModel? viewModel) where T : IView?
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

#### Type Parameters

`T`

The View type.

### InitializeViewsSafely\<T\>\(ReadOnlySpan\<T\>, IViewModel?\) {#Aspid_MVVM_ViewExtensions_InitializeViewsSafely__1_System_ReadOnlySpan___0__Aspid_MVVM_IViewModel_}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViewsSafely<T>(this ReadOnlySpan<T> views, IViewModel? viewModel) where T : IView?
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

#### Type Parameters

`T`

The View type.

### InitializeViewsSafely\<T\>\(List\<T\>?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_InitializeViewsSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_IViewModel_}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViewsSafely<T>(this List<T>? views, IViewModel? viewModel) where T : IView?
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

#### Type Parameters

`T`

The View type.

### InitializeViewsSafely\<T\>\(IEnumerable\<T\>?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_InitializeViewsSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_IViewModel_}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViewsSafely<T>(this IEnumerable<T>? views, IViewModel? viewModel) where T : IView?
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Processing the sequence and disposing its enumerator both fail.

### Reinitialize\(IView, IViewModel?\) {#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_}

Deinitializes the View before binding the replacement ViewModel.

```csharp
public static IViewModel? Reinitialize(this IView view, IViewModel? newViewModel)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)

The View to reinitialize.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves the View unbound.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View had none.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeSafely\(IView?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeSafely_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_}

Deinitializes the View before binding the replacement ViewModel.

```csharp
public static IViewModel? ReinitializeSafely(this IView? view, IViewModel? newViewModel)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The View to reinitialize. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves the View unbound.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View was skipped or had none.

### ReinitializeViews\<T\>\(T\[\], IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeViews__1___0___Aspid_MVVM_IViewModel_}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViews<T>(this T[] views, IViewModel? newViewModel) where T : IView
```

#### Parameters

`views` T\[\]

The Views to reinitialize.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or a View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViews\<T\>\(Span\<T\>, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeViews__1_System_Span___0__Aspid_MVVM_IViewModel_}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViews<T>(this Span<T> views, IViewModel? newViewModel) where T : IView
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views to reinitialize.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViews\<T\>\(ReadOnlySpan\<T\>, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeViews__1_System_ReadOnlySpan___0__Aspid_MVVM_IViewModel_}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViews<T>(this ReadOnlySpan<T> views, IViewModel? newViewModel) where T : IView
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views to reinitialize.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViews\<T\>\(List\<T\>, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeViews__1_System_Collections_Generic_List___0__Aspid_MVVM_IViewModel_}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViews<T>(this List<T> views, IViewModel? newViewModel) where T : IView
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The Views to reinitialize.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or a View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViews\<T\>\(IEnumerable\<T\>, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeViews__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_IViewModel_}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViews<T>(this IEnumerable<T> views, IViewModel? newViewModel) where T : IView
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The Views to reinitialize.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or a View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViewsSafely\<T\>\(T\[\]?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeViewsSafely__1___0___Aspid_MVVM_IViewModel_}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViewsSafely<T>(this T[]? views, IViewModel? newViewModel) where T : IView?
```

#### Parameters

`views` T\[\]?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

#### Type Parameters

`T`

The View type.

### ReinitializeViewsSafely\<T\>\(Span\<T\>, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeViewsSafely__1_System_Span___0__Aspid_MVVM_IViewModel_}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViewsSafely<T>(this Span<T> views, IViewModel? newViewModel) where T : IView?
```

#### Parameters

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

#### Type Parameters

`T`

The View type.

### ReinitializeViewsSafely\<T\>\(ReadOnlySpan\<T\>, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeViewsSafely__1_System_ReadOnlySpan___0__Aspid_MVVM_IViewModel_}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViewsSafely<T>(this ReadOnlySpan<T> views, IViewModel? newViewModel) where T : IView?
```

#### Parameters

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

#### Type Parameters

`T`

The View type.

### ReinitializeViewsSafely\<T\>\(List\<T\>?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeViewsSafely__1_System_Collections_Generic_List___0__Aspid_MVVM_IViewModel_}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViewsSafely<T>(this List<T>? views, IViewModel? newViewModel) where T : IView?
```

#### Parameters

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

#### Type Parameters

`T`

The View type.

### ReinitializeViewsSafely\<T\>\(IEnumerable\<T\>?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeViewsSafely__1_System_Collections_Generic_IEnumerable___0__Aspid_MVVM_IViewModel_}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViewsSafely<T>(this IEnumerable<T>? views, IViewModel? newViewModel) where T : IView?
```

#### Parameters

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Processing the sequence and disposing its enumerator both fail.
