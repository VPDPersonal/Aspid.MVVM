---
title: "Class ViewModelExtensions"
sidebar_label: "ViewModelExtensions"
description: "Class ViewModelExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewModelExtensions {#Aspid_MVVM_ViewModelExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Provides extension methods for [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class ViewModelExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[ViewModelExtensions](Aspid.MVVM.ViewModelExtensions.md)



## Methods

### DestroyViewModel\(IViewModel\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_}

Cleans up the ViewModel and destroys its Unity object, if any, preserving the owning GameObject.

```csharp
public static GameObject? DestroyViewModel(this IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to destroy.

#### Returns

 GameObject?

The surviving GameObject for a component, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> otherwise.

#### Remarks

Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
It is immediate in Edit Mode and deferred in Play Mode.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Cleanup and destruction both fail.

### DestroyViewModelAndGameObject\(IViewModel\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_}

Cleans up the ViewModel and destroys its GameObject for components or the object itself for other Unity objects.

```csharp
public static void DestroyViewModelAndGameObject(this IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to destroy.

#### Remarks

Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
It is immediate in Edit Mode and deferred in Play Mode.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Cleanup and destruction both fail.

### DestroyViewModelAndGameObjectSafely\(IViewModel?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObjectSafely_Aspid_MVVM_IViewModel_}

Cleans up the ViewModel and destroys its GameObject for components or the object itself for other Unity objects.

```csharp
public static void DestroyViewModelAndGameObjectSafely(this IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to destroy. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

#### Remarks

Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
It is immediate in Edit Mode and deferred in Play Mode.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Cleanup and destruction both fail.

### DestroyViewModelSafely\(IViewModel?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelSafely_Aspid_MVVM_IViewModel_}

Cleans up the ViewModel and destroys its Unity object, if any, preserving the owning GameObject.

```csharp
public static GameObject? DestroyViewModelSafely(this IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to destroy. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

#### Returns

 GameObject?

The surviving GameObject for a component, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> otherwise.

#### Remarks

Call on the main thread for Unity objects. Destruction is attempted even if cleanup fails.
It is immediate in Edit Mode and deferred in Play Mode.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Cleanup and destruction both fail.

### DestroyViewModels\<T\>\(T\[\]\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModels__1___0___}

Calls [`ViewModelExtensions.DestroyViewModel`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows failures after cleanup.

```csharp
public static void DestroyViewModels<T>(this T[] viewModels) where T : IViewModel
```

#### Parameters

`viewModels` T\[\]

The ViewModels to destroy.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModels</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewModels\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModels__1_System_Span___0__}

Calls [`ViewModelExtensions.DestroyViewModel`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows failures after cleanup.

```csharp
public static void DestroyViewModels<T>(this Span<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The ViewModels to destroy.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewModels\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModels__1_System_ReadOnlySpan___0__}

Calls [`ViewModelExtensions.DestroyViewModel`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows failures after cleanup.

```csharp
public static void DestroyViewModels<T>(this ReadOnlySpan<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The ViewModels to destroy.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewModels\<T\>\(List\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModels__1_System_Collections_Generic_List___0__}

Calls [`ViewModelExtensions.DestroyViewModel`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows failures after cleanup.

```csharp
public static void DestroyViewModels<T>(this List<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The ViewModels to destroy.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModels</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewModels\<T\>\(IEnumerable\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModels__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewModelExtensions.DestroyViewModel`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows failures after cleanup.

```csharp
public static void DestroyViewModels<T>(this IEnumerable<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The ViewModels to destroy.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModels</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewModelsAndGameObjects\<T\>\(T\[\]\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsAndGameObjects__1___0___}

Calls [`ViewModelExtensions.DestroyViewModelAndGameObject`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows failures after cleanup.

```csharp
public static void DestroyViewModelsAndGameObjects<T>(this T[] viewModels) where T : IViewModel
```

#### Parameters

`viewModels` T\[\]

The ViewModels to destroy.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModels</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewModelsAndGameObjects\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsAndGameObjects__1_System_Span___0__}

Calls [`ViewModelExtensions.DestroyViewModelAndGameObject`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows failures after cleanup.

```csharp
public static void DestroyViewModelsAndGameObjects<T>(this Span<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The ViewModels to destroy.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewModelsAndGameObjects\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsAndGameObjects__1_System_ReadOnlySpan___0__}

Calls [`ViewModelExtensions.DestroyViewModelAndGameObject`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows failures after cleanup.

```csharp
public static void DestroyViewModelsAndGameObjects<T>(this ReadOnlySpan<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The ViewModels to destroy.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewModelsAndGameObjects\<T\>\(List\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsAndGameObjects__1_System_Collections_Generic_List___0__}

Calls [`ViewModelExtensions.DestroyViewModelAndGameObject`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows failures after cleanup.

```csharp
public static void DestroyViewModelsAndGameObjects<T>(this List<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The ViewModels to destroy.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModels</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewModelsAndGameObjects\<T\>\(IEnumerable\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsAndGameObjects__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewModelExtensions.DestroyViewModelAndGameObject`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows failures after cleanup.

```csharp
public static void DestroyViewModelsAndGameObjects<T>(this IEnumerable<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The ViewModels to destroy.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModels</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur during cleanup, destruction, or enumeration.

### DestroyViewModelsAndGameObjectsSafely\<T\>\(T\[\]?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsAndGameObjectsSafely__1___0___}

Calls [`ViewModelExtensions.DestroyViewModelAndGameObjectSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObjectSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DestroyViewModelsAndGameObjectsSafely<T>(this T[]? viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` T\[\]?

The ViewModels. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DestroyViewModelsAndGameObjectsSafely\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsAndGameObjectsSafely__1_System_Span___0__}

Calls [`ViewModelExtensions.DestroyViewModelAndGameObjectSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObjectSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DestroyViewModelsAndGameObjectsSafely<T>(this Span<T> viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The ViewModels. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DestroyViewModelsAndGameObjectsSafely\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsAndGameObjectsSafely__1_System_ReadOnlySpan___0__}

Calls [`ViewModelExtensions.DestroyViewModelAndGameObjectSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObjectSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DestroyViewModelsAndGameObjectsSafely<T>(this ReadOnlySpan<T> viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The ViewModels. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DestroyViewModelsAndGameObjectsSafely\<T\>\(List\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsAndGameObjectsSafely__1_System_Collections_Generic_List___0__}

Calls [`ViewModelExtensions.DestroyViewModelAndGameObjectSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObjectSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DestroyViewModelsAndGameObjectsSafely<T>(this List<T>? viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The ViewModels. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DestroyViewModelsAndGameObjectsSafely\<T\>\(IEnumerable\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsAndGameObjectsSafely__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewModelExtensions.DestroyViewModelAndGameObjectSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObjectSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DestroyViewModelsAndGameObjectsSafely<T>(this IEnumerable<T>? viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The ViewModels. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DestroyViewModelsSafely\<T\>\(T\[\]?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsSafely__1___0___}

Calls [`ViewModelExtensions.DestroyViewModelSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DestroyViewModelsSafely<T>(this T[]? viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` T\[\]?

The ViewModels. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DestroyViewModelsSafely\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsSafely__1_System_Span___0__}

Calls [`ViewModelExtensions.DestroyViewModelSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DestroyViewModelsSafely<T>(this Span<T> viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The ViewModels. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DestroyViewModelsSafely\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsSafely__1_System_ReadOnlySpan___0__}

Calls [`ViewModelExtensions.DestroyViewModelSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DestroyViewModelsSafely<T>(this ReadOnlySpan<T> viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The ViewModels. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DestroyViewModelsSafely\<T\>\(List\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsSafely__1_System_Collections_Generic_List___0__}

Calls [`ViewModelExtensions.DestroyViewModelSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DestroyViewModelsSafely<T>(this List<T>? viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The ViewModels. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DestroyViewModelsSafely\<T\>\(IEnumerable\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelsSafely__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewModelExtensions.DestroyViewModelSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DestroyViewModelsSafely<T>(this IEnumerable<T>? viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The ViewModels. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Remarks

Call on the main thread for Unity objects.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DisposeViewModel\<T\>\(T\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModel__1___0_}

Calls [`Dispose`](https://learn.microsoft.com/dotnet/api/system.idisposable.dispose) on the ViewModel.

```csharp
public static void DisposeViewModel<T>(this T viewModel) where T : class, IViewModel, IDisposable
```

#### Parameters

`viewModel` T

The ViewModel to clean up.

#### Type Parameters

`T`

The disposable ViewModel type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### DisposeViewModel\(IViewModel\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_}

Disposes the ViewModel if it implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable).

```csharp
public static void DisposeViewModel(this IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to clean up.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### DisposeViewModelSafely\<T\>\(T?\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely__1___0_}

Calls [`Dispose`](https://learn.microsoft.com/dotnet/api/system.idisposable.dispose) on the ViewModel.

```csharp
public static void DisposeViewModelSafely<T>(this T? viewModel) where T : class, IViewModel, IDisposable
```

#### Parameters

`viewModel` T?

The ViewModel to clean up. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

#### Type Parameters

`T`

The disposable ViewModel type.

### DisposeViewModelSafely\(IViewModel?\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely_Aspid_MVVM_IViewModel_}

Disposes the ViewModel if it implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable).

```csharp
public static void DisposeViewModelSafely(this IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to clean up. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### DisposeViewModels\<T\>\(T\[\]\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModels__1___0___}

Calls [`ViewModelExtensions.DisposeViewModel`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DisposeViewModels<T>(this T[] viewModels) where T : IViewModel
```

#### Parameters

`viewModels` T\[\]

The ViewModels to clean up.

#### Type Parameters

`T`

The ViewModel type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModels</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DisposeViewModels\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModels__1_System_Span___0__}

Calls [`ViewModelExtensions.DisposeViewModel`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DisposeViewModels<T>(this Span<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The ViewModels to clean up.

#### Type Parameters

`T`

The ViewModel type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DisposeViewModels\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModels__1_System_ReadOnlySpan___0__}

Calls [`ViewModelExtensions.DisposeViewModel`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DisposeViewModels<T>(this ReadOnlySpan<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The ViewModels to clean up.

#### Type Parameters

`T`

The ViewModel type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The only failure is a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DisposeViewModels\<T\>\(List\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModels__1_System_Collections_Generic_List___0__}

Calls [`ViewModelExtensions.DisposeViewModel`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DisposeViewModels<T>(this List<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The ViewModels to clean up.

#### Type Parameters

`T`

The ViewModel type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModels</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DisposeViewModels\<T\>\(IEnumerable\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModels__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewModelExtensions.DisposeViewModel`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DisposeViewModels<T>(this IEnumerable<T> viewModels) where T : IViewModel
```

#### Parameters

`viewModels` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The ViewModels to clean up.

#### Type Parameters

`T`

The ViewModel type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModels</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or the only failure is a missing or destroyed ViewModel.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DisposeViewModelsSafely\<T\>\(T\[\]?\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModelsSafely__1___0___}

Calls [`ViewModelExtensions.DisposeViewModelSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DisposeViewModelsSafely<T>(this T[]? viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` T\[\]?

The ViewModels. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DisposeViewModelsSafely\<T\>\(Span\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModelsSafely__1_System_Span___0__}

Calls [`ViewModelExtensions.DisposeViewModelSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DisposeViewModelsSafely<T>(this Span<T> viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The ViewModels. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DisposeViewModelsSafely\<T\>\(ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModelsSafely__1_System_ReadOnlySpan___0__}

Calls [`ViewModelExtensions.DisposeViewModelSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DisposeViewModelsSafely<T>(this ReadOnlySpan<T> viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The ViewModels. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DisposeViewModelsSafely\<T\>\(List\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModelsSafely__1_System_Collections_Generic_List___0__}

Calls [`ViewModelExtensions.DisposeViewModelSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DisposeViewModelsSafely<T>(this List<T>? viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The ViewModels. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### DisposeViewModelsSafely\<T\>\(IEnumerable\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModelsSafely__1_System_Collections_Generic_IEnumerable___0__}

Calls [`ViewModelExtensions.DisposeViewModelSafely`](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely_Aspid_MVVM_IViewModel_) on each ViewModel and rethrows any failures after cleanup.

```csharp
public static void DisposeViewModelsSafely<T>(this IEnumerable<T>? viewModels) where T : IViewModel?
```

#### Parameters

`viewModels` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The ViewModels. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The ViewModel type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple failures occur while processing the ViewModels.

### InitializeView\(IViewModel, IView\) {#Aspid_MVVM_ViewModelExtensions_InitializeView_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_}

Calls [`IView.Initialize`](Aspid.MVVM.IView.md#Aspid_MVVM_IView_Initialize_Aspid_MVVM_IViewModel_) with <code class="paramref">viewModel</code>.

```csharp
public static void InitializeView(this IViewModel viewModel, IView view)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to the View.

`view` [IView](Aspid.MVVM.IView.md)

The View to initialize.

#### Exceptions

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViewSafely\(IViewModel?, IView?\) {#Aspid_MVVM_ViewModelExtensions_InitializeViewSafely_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_}

Initializes the View with <code class="paramref">viewModel</code>.

```csharp
public static void InitializeViewSafely(this IViewModel? viewModel, IView? view)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

`view` [IView](Aspid.MVVM.IView.md)?

The View to initialize. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### InitializeViews\<T\>\(IViewModel, params T\[\]\) {#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel___0___}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViews<T>(this IViewModel viewModel, params T[] views) where T : IView
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to each View.

`views` T\[\]

The Views to initialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\<T\>\(IViewModel, Span\<T\>\) {#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Span___0__}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViews<T>(this IViewModel viewModel, Span<T> views) where T : IView
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to each View.

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views to initialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\<T\>\(IViewModel, ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViews<T>(this IViewModel viewModel, ReadOnlySpan<T> views) where T : IView
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to each View.

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views to initialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\<T\>\(IViewModel, List\<T\>\) {#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViews<T>(this IViewModel viewModel, List<T> views) where T : IView
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to each View.

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The Views to initialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\<T\>\(IViewModel, IEnumerable\<T\>\) {#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViews<T>(this IViewModel viewModel, IEnumerable<T> views) where T : IView
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel passed to each View.

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The Views to initialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViewsSafely\<T\>\(IViewModel?, params T\[\]?\) {#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel___0___}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViewsSafely<T>(this IViewModel? viewModel, params T[]? views) where T : IView?
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

`views` T\[\]?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

### InitializeViewsSafely\<T\>\(IViewModel?, Span\<T\>\) {#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Span___0__}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViewsSafely<T>(this IViewModel? viewModel, Span<T> views) where T : IView?
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

### InitializeViewsSafely\<T\>\(IViewModel?, ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViewsSafely<T>(this IViewModel? viewModel, ReadOnlySpan<T> views) where T : IView?
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

### InitializeViewsSafely\<T\>\(IViewModel?, List\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViewsSafely<T>(this IViewModel? viewModel, List<T>? views) where T : IView?
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

### InitializeViewsSafely\<T\>\(IViewModel?, IEnumerable\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__}

Initializes the Views in order until one fails.

```csharp
public static void InitializeViewsSafely<T>(this IViewModel? viewModel, IEnumerable<T>? views) where T : IView?
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Initialization is skipped for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed ViewModel.

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Processing the sequence and disposing its enumerator both fail.

### ReinitializeView\(IViewModel?, IView\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeView_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_}

Deinitializes the View before binding the replacement ViewModel.

```csharp
public static IViewModel? ReinitializeView(this IViewModel? newViewModel, IView view)
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves the View unbound.

`view` [IView](Aspid.MVVM.IView.md)

The View to reinitialize.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View had none.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViewSafely\(IViewModel?, IView?\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViewSafely_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_}

Deinitializes the View before binding the replacement ViewModel.

```csharp
public static IViewModel? ReinitializeViewSafely(this IViewModel? newViewModel, IView? view)
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves the View unbound.

`view` [IView](Aspid.MVVM.IView.md)?

The View to reinitialize. Skipped if <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the View was skipped or had none.

### ReinitializeViews\<T\>\(IViewModel?, T\[\]\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel___0___}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViews<T>(this IViewModel? newViewModel, T[] views) where T : IView
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

`views` T\[\]

The Views to reinitialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or a View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViews\<T\>\(IViewModel?, Span\<T\>\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_Span___0__}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViews<T>(this IViewModel? newViewModel, Span<T> views) where T : IView
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views to reinitialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViews\<T\>\(IViewModel?, ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViews<T>(this IViewModel? newViewModel, ReadOnlySpan<T> views) where T : IView
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views to reinitialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

A View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViews\<T\>\(IViewModel?, List\<T\>\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViews<T>(this IViewModel? newViewModel, List<T> views) where T : IView
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The Views to reinitialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or a View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViews\<T\>\(IViewModel?, IEnumerable\<T\>\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViews<T>(this IViewModel? newViewModel, IEnumerable<T> views) where T : IView
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

The Views to reinitialize.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or a View is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

### ReinitializeViewsSafely\<T\>\(IViewModel?, T\[\]?\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel___0___}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViewsSafely<T>(this IViewModel? newViewModel, T[]? views) where T : IView?
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

`views` T\[\]?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

### ReinitializeViewsSafely\<T\>\(IViewModel?, Span\<T\>\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Span___0__}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViewsSafely<T>(this IViewModel? newViewModel, Span<T> views) where T : IView?
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

`views` [Span](https://learn.microsoft.com/dotnet/api/system.span-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

### ReinitializeViewsSafely\<T\>\(IViewModel?, ReadOnlySpan\<T\>\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViewsSafely<T>(this IViewModel? newViewModel, ReadOnlySpan<T> views) where T : IView?
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

`views` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<T\>

The Views. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

### ReinitializeViewsSafely\<T\>\(IViewModel?, List\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViewsSafely<T>(this IViewModel? newViewModel, List<T>? views) where T : IView?
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

### ReinitializeViewsSafely\<T\>\(IViewModel?, IEnumerable\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__}

Reinitializes the Views in order until one fails.

```csharp
public static void ReinitializeViewsSafely<T>(this IViewModel? newViewModel, IEnumerable<T>? views) where T : IView?
```

#### Parameters

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed replacement leaves each View unbound.

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>?

The Views. Skips a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> collection and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed entries.

#### Type Parameters

`T`

The View type.

#### Exceptions

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Processing the sequence and disposing its enumerator both fail.
