---
title: "Class MonoView"
sidebar_label: "MonoView"
description: "Class MonoView — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MonoView {#Aspid_MVVM_MonoView}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

[`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) for views with serialized binder groups.

```csharp
[AddComponentMenu("Aspid/MVVM/Views/Mono View")]
[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
public class MonoView : MonoBehaviour, IDisposable, IView
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoView](Aspid.MVVM.MonoView.md)

#### Derived

[EventMonoView](Aspid.MVVM.StarterKit.EventMonoView.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable),
[IView](Aspid.MVVM.IView.md)


#### Extension Methods

[ViewExtensions.DeinitializeView\(IView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DeinitializeView_Aspid_MVVM_IView_),
[ViewExtensions.DeinitializeViewSafely\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DeinitializeViewSafely_Aspid_MVVM_IView_),
[ViewExtensions.DestroyView\(IView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyView_Aspid_MVVM_IView_),
[ViewExtensions.DestroyViewAndGameObject\(IView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_),
[ViewExtensions.DestroyViewAndGameObjectSafely\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObjectSafely_Aspid_MVVM_IView_),
[ViewExtensions.DestroyViewSafely\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewSafely_Aspid_MVVM_IView_),
[ViewExtensions.DisposeView\<MonoView\>\(MonoView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView__1___0_),
[ViewExtensions.DisposeView\(IView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_),
[ViewExtensions.DisposeViewSafely\<MonoView\>\(MonoView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeViewSafely__1___0_),
[ViewExtensions.DisposeViewSafely\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeViewSafely_Aspid_MVVM_IView_),
[ViewExtensions.InitializeSafely\(IView?, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_InitializeSafely_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_),
[ViewExtensions.Reinitialize\(IView, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_),
[ViewExtensions.ReinitializeSafely\(IView?, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_ReinitializeSafely_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_)

## Properties

### ViewModel {#Aspid_MVVM_MonoView_ViewModel}

Gets the bound ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> before initialization and after deinitialization.

```csharp
public IViewModel ViewModel { get; protected set; }
```

#### Property Value

 [IViewModel](Aspid.MVVM.IViewModel.md)

## Methods

### Deinitialize\(\) {#Aspid_MVVM_MonoView_Deinitialize}

Unbinds the binders and clears [`IView.ViewModel`](Aspid.MVVM.IView.md#Aspid_MVVM_IView_ViewModel).

```csharp
public void Deinitialize()
```

#### Remarks

Does not dispose the ViewModel.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Initialization is in progress.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple cleanup operations fail.

### DeinitializeInternal\(\) {#Aspid_MVVM_MonoView_DeinitializeInternal}

```csharp
protected virtual void DeinitializeInternal()
```

### Dispose\(\) {#Aspid_MVVM_MonoView_Dispose}

Deinitializes the view and releases its bindings without destroying any Unity objects.

```csharp
public virtual void Dispose()
```

#### Remarks

Overrides must call the base implementation.

### Initialize\(IViewModel\) {#Aspid_MVVM_MonoView_Initialize_Aspid_MVVM_IViewModel_}

Binds the View to the members of <code class="paramref">viewModel</code>.

```csharp
public void Initialize(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The View is already initialized.

### InitializeInternal\(IViewModel\) {#Aspid_MVVM_MonoView_InitializeInternal_Aspid_MVVM_IViewModel_}

```csharp
protected virtual void InitializeInternal(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

### Instantiate\<T\>\(T, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_Aspid_MVVM_IViewModel_}

Clones and initializes the view.

```csharp
public static T Instantiate<T>(T original, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Returns

 T

The initialized clone.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

Failed initialization destroys the clone without disposing its ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Instantiate\<T\>\(T, Transform, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Transform_Aspid_MVVM_IViewModel_}

Clones and initializes the view using its local transform under the parent.

```csharp
public static T Instantiate<T>(T original, Transform parent, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`parent` Transform

The parent for the clone, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Returns

 T

The initialized clone.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

Failed initialization destroys the clone without disposing its ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Instantiate\<T\>\(T, Transform, bool, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Transform_System_Boolean_Aspid_MVVM_IViewModel_}

Clones and initializes the view.

```csharp
public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`parent` Transform

The parent for the clone, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`worldPositionStays` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, keeps the original world position and rotation.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Returns

 T

The initialized clone.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

Failed initialization destroys the clone without disposing its ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Instantiate\<T\>\(T, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Clones and initializes the view.

```csharp
public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`position` Vector3

The world-space position.

`rotation` Quaternion

The world-space rotation.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Returns

 T

The initialized clone.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

Failed initialization destroys the clone without disposing its ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html).

### Instantiate\<T\>\(T, Vector3, Quaternion, Transform, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Vector3_UnityEngine_Quaternion_UnityEngine_Transform_Aspid_MVVM_IViewModel_}

Clones and initializes the view.

```csharp
public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`position` Vector3

The world-space position.

`rotation` Quaternion

The world-space rotation.

`parent` Transform

The parent for the clone, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Returns

 T

The initialized clone.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

Failed initialization destroys the clone without disposing its ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, or it is a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) and <code class="paramref">parent</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Instantiate\<T\>\(T, IViewModel, InstantiateParameters\) {#Aspid_MVVM_MonoView_Instantiate__1___0_Aspid_MVVM_IViewModel_UnityEngine_InstantiateParameters_}

Clones and initializes the view.

```csharp
public static T Instantiate<T>(T original, IViewModel viewModel, InstantiateParameters parameters) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

`parameters` InstantiateParameters

The Unity cloning options.

#### Returns

 T

The initialized clone.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

Failed initialization destroys the clone without disposing its ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Instantiate\<T\>\(T, Vector3, Quaternion, IViewModel, InstantiateParameters\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_UnityEngine_InstantiateParameters_}

Clones and initializes the view.

```csharp
public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, IViewModel viewModel, InstantiateParameters parameters) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`position` Vector3

The position in the configured coordinate space.

`rotation` Quaternion

The rotation in the configured coordinate space.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

`parameters` InstantiateParameters

The Unity cloning options.

#### Returns

 T

The initialized clone.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

Failed initialization destroys the clone without disposing its ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_Aspid_MVVM_IViewModel_}

Clones a view asynchronously and initializes it on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html).

### InstantiateAsync\<T\>\(T, Transform, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_UnityEngine_Transform_Aspid_MVVM_IViewModel_}

Clones a view asynchronously and initializes it on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Transform parent, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`parent` Transform

The parent of the clone, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html).

### InstantiateAsync\<T\>\(T, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Clones a view asynchronously and initializes it on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`position` Vector3

The world-space position.

`rotation` Quaternion

The world-space rotation.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html).

### InstantiateAsync\<T\>\(T, Transform, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_UnityEngine_Transform_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Clones a view asynchronously and initializes it on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Transform parent, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`parent` Transform

The parent of the clone, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`position` Vector3

The world-space position.

`rotation` Quaternion

The world-space rotation.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html).

### InstantiateAsync\<T\>\(T, int, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_Aspid_MVVM_IViewModel_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### InstantiateAsync\<T\>\(T, int, Transform, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_Aspid_MVVM_IViewModel_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent of the clones, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### InstantiateAsync\<T\>\(T, int, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`position` Vector3

The world-space position.

`rotation` Quaternion

The world-space rotation.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### InstantiateAsync\<T\>\(T, int, ReadOnlySpan\<Vector3\>, ReadOnlySpan\<Quaternion\>, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_System_ReadOnlySpan_UnityEngine_Vector3__System_ReadOnlySpan_UnityEngine_Quaternion__Aspid_MVVM_IViewModel_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`positions` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Vector3\>

World-space positions, repeated when shorter than <code class="paramref">count</code>.

`rotations` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Quaternion\>

World-space rotations, repeated when shorter than <code class="paramref">count</code>.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### InstantiateAsync\<T\>\(T, int, Transform, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent of the clones, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`position` Vector3

The world-space position.

`rotation` Quaternion

The world-space rotation.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### InstantiateAsync\<T\>\(T, int, Transform, ReadOnlySpan\<Vector3\>, ReadOnlySpan\<Quaternion\>, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_System_ReadOnlySpan_UnityEngine_Vector3__System_ReadOnlySpan_UnityEngine_Quaternion__Aspid_MVVM_IViewModel_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent of the clones, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`positions` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Vector3\>

World-space positions, repeated when shorter than <code class="paramref">count</code>.

`rotations` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Quaternion\>

World-space rotations, repeated when shorter than <code class="paramref">count</code>.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### InstantiateAsync\<T\>\(T, int, Transform, Vector3, Quaternion, IViewModel, CancellationToken\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_System_Threading_CancellationToken_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, Vector3 position, Quaternion rotation, IViewModel viewModel, CancellationToken cancellationToken) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent of the clones, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`position` Vector3

The world-space position.

`rotation` Quaternion

The world-space rotation.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

The token that cancels cloning or destroys clones if initialization is canceled.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### InstantiateAsync\<T\>\(T, int, Transform, ReadOnlySpan\<Vector3\>, ReadOnlySpan\<Quaternion\>, IViewModel, CancellationToken\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_System_ReadOnlySpan_UnityEngine_Vector3__System_ReadOnlySpan_UnityEngine_Quaternion__Aspid_MVVM_IViewModel_System_Threading_CancellationToken_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, IViewModel viewModel, CancellationToken cancellationToken) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent of the clones, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`positions` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Vector3\>

World-space positions, repeated when shorter than <code class="paramref">count</code>.

`rotations` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Quaternion\>

World-space rotations, repeated when shorter than <code class="paramref">count</code>.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

The token that cancels cloning or destroys clones if initialization is canceled.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### InstantiateAsync\<T\>\(T, int, IViewModel, InstantiateParameters, CancellationToken\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_Aspid_MVVM_IViewModel_UnityEngine_InstantiateParameters_System_Threading_CancellationToken_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, IViewModel viewModel, InstantiateParameters parameters, CancellationToken cancellationToken = default) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

`parameters` InstantiateParameters

The Unity cloning options.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

The token that cancels cloning or destroys clones if initialization is canceled.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### InstantiateAsync\<T\>\(T, int, Vector3, Quaternion, IViewModel, InstantiateParameters, CancellationToken\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_UnityEngine_InstantiateParameters_System_Threading_CancellationToken_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Vector3 position, Quaternion rotation, IViewModel viewModel, InstantiateParameters parameters, CancellationToken cancellationToken = default) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`position` Vector3

The position in the configured coordinate space.

`rotation` Quaternion

The rotation in the configured coordinate space.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

`parameters` InstantiateParameters

The Unity cloning options.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

The token that cancels cloning or destroys clones if initialization is canceled.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### InstantiateAsync\<T\>\(T, int, ReadOnlySpan\<Vector3\>, ReadOnlySpan\<Quaternion\>, IViewModel, InstantiateParameters, CancellationToken\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_System_ReadOnlySpan_UnityEngine_Vector3__System_ReadOnlySpan_UnityEngine_Quaternion__Aspid_MVVM_IViewModel_UnityEngine_InstantiateParameters_System_Threading_CancellationToken_}

Clones a view asynchronously and initializes each clone on completion.

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, IViewModel viewModel, InstantiateParameters parameters, CancellationToken cancellationToken = default) where T : Object, IView
```

#### Parameters

`original` T

The view to clone.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`positions` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Vector3\>

Positions, repeated when shorter than <code class="paramref">count</code>.

`rotations` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Quaternion\>

Rotations, repeated when shorter than <code class="paramref">count</code>.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel shared by all clones.

`parameters` InstantiateParameters

The Unity cloning options.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

The token that cancels cloning or destroys clones if initialization is canceled.

#### Returns

 AsyncInstantiateOperation\<T\>

The Unity cloning operation.

#### Type Parameters

`T`

The Unity view type.

#### Remarks

If initialization fails or the ViewModel is destroyed before initialization, the completion callback
destroys all clones and rethrows the exception.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or destroyed.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">original</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html), or <code class="paramref">count</code> is less than one.

### OnDestroy\(\) {#Aspid_MVVM_MonoView_OnDestroy}

Called when Unity destroys the component to dispose the view.

```csharp
protected virtual void OnDestroy()
```

#### Remarks

Overrides must call the base implementation.
