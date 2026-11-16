---
title: "Class MonoViewModelExtensions"
sidebar_label: "MonoViewModelExtensions"
description: "Class MonoViewModelExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MonoViewModelExtensions {#Aspid_MVVM_MonoViewModelExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

Provides extension methods for [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static class MonoViewModelExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[MonoViewModelExtensions](Aspid.MVVM.MonoViewModelExtensions.md)



## Methods

### DestroyViewModel\<T\>\(T\) {#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel__1___0_}

Disposes the ViewModel if supported and destroys its Unity object while preserving any GameObject.

```csharp
public static GameObject? DestroyViewModel<T>(this T viewModel) where T : Object, IViewModel
```

#### Parameters

`viewModel` T

The ViewModel to dispose and destroy.

#### Returns

 GameObject?

The associated GameObject if still alive after the call; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T`

The Unity ViewModel object.

#### Remarks

<p>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</p>
<p>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller.</p>

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### DestroyViewModel\(IViewModel\) {#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_}

Disposes the ViewModel if supported and destroys its Unity object while preserving any GameObject.

```csharp
public static GameObject? DestroyViewModel(this IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to dispose and destroy.

#### Returns

 GameObject?

The associated GameObject if still alive after the call; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Remarks

<p>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</p>
<p>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller. Managed ViewModels are only disposed.</p>

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### DestroyViewModelAndGameObject\<T\>\(T\) {#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModelAndGameObject__1___0_}

Disposes the ViewModel if supported and destroys its GameObject or the Unity object itself.

```csharp
public static GameObject? DestroyViewModelAndGameObject<T>(this T viewModel) where T : Object, IViewModel
```

#### Parameters

`viewModel` T

The ViewModel to dispose and destroy.

#### Returns

 GameObject?

The associated GameObject if still alive after the call; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T`

The Unity ViewModel object.

#### Remarks

<p>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</p>
<p>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller.</p>

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### DestroyViewModelAndGameObject\(IViewModel\) {#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_}

Disposes the ViewModel if supported and destroys its GameObject or the Unity object itself.

```csharp
public static GameObject? DestroyViewModelAndGameObject(this IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to dispose and destroy.

#### Returns

 GameObject?

The associated GameObject if still alive after the call; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Remarks

<p>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</p>
<p>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller. Managed ViewModels are only disposed.</p>

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
