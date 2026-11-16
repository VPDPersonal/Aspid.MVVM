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

[ViewExtensions.DeinitializeView\<MonoView\>\(MonoView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DeinitializeView__1___0_),
[MonoViewExtensions.DestroyView\<MonoView\>\(MonoView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyView__1___0_),
[MonoViewExtensions.DestroyView\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyView_Aspid_MVVM_IView_),
[MonoViewExtensions.DestroyViewAndGameObject\<MonoView\>\(MonoView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject__1___0_),
[MonoViewExtensions.DestroyViewAndGameObject\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_),
[ViewExtensions.DisposeView\<MonoView\>\(MonoView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView__1___0_),
[ViewExtensions.DisposeView\(IView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_),
[ViewExtensions.Reinitialize\(IView, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_)

## Properties

### ViewModel {#Aspid_MVVM_MonoView_ViewModel}

Gets the ViewModel the view is initialized with; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it is not initialized.

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

Deinitializes the view without destroying its component, GameObject or ViewModel.

```csharp
public virtual void Dispose()
```

#### Remarks

Overrides must call the base implementation to release bindings.

### Initialize\(IViewModel\) {#Aspid_MVVM_MonoView_Initialize_Aspid_MVVM_IViewModel_}

Initializes the view with <code class="paramref">viewModel</code>, binding its binders to the ViewModel's members.

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

Creates an instance of the View and initializes it with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static T Instantiate<T>(T original, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

#### Returns

 T

The created instance of the View.

#### Type Parameters

`T`

The type of the View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Instantiate\<T\>\(T, Transform, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Transform_Aspid_MVVM_IViewModel_}

Creates an instance of the View with the specified parent and initializes it with the given [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static T Instantiate<T>(T original, Transform parent, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`parent` Transform

The parent for the clone; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> creates it at the root.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

#### Returns

 T

The created instance of the View.

#### Type Parameters

`T`

The type of the View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Instantiate\<T\>\(T, Transform, bool, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Transform_System_Boolean_Aspid_MVVM_IViewModel_}

Creates an instance of the View with the specified parent and initializes it with the given [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`parent` Transform

The parent for the clone; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> creates it at the root.

`worldPositionStays` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, retains the original world position.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

#### Returns

 T

The created instance of the View.

#### Type Parameters

`T`

The type of the View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Instantiate\<T\>\(T, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Creates an instance of the View with the specified position and rotation, and initializes it with the given [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`position` Vector3

The position for the new View instance.

`rotation` Quaternion

The rotation for the new View instance.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

#### Returns

 T

The created instance of the View.

#### Type Parameters

`T`

The type of the View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Instantiate\<T\>\(T, Vector3, Quaternion, Transform, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Vector3_UnityEngine_Quaternion_UnityEngine_Transform_Aspid_MVVM_IViewModel_}

Creates an instance of the View with the specified position, rotation, and parent, and initializes it with the given [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`position` Vector3

The position for the new View instance.

`rotation` Quaternion

The rotation for the new View instance.

`parent` Transform

The parent for the clone; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> creates it at the root.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

#### Returns

 T

The created instance of the View.

#### Type Parameters

`T`

The type of the View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. Failed initialization destroys the clone, including its GameObject for components.
Cleanup failures during rollback do not replace the initialization exception. The ViewModel is not disposed.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_Aspid_MVVM_IViewModel_}

Asynchronously creates an instance of the View and initializes it with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instance.

#### Type Parameters

`T`

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, Transform, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_UnityEngine_Transform_Aspid_MVVM_IViewModel_}

Asynchronously creates an instance of the View with the specified parent
and initializes it with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Transform parent, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`parent` Transform

The parent object for the new View instance.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instance.

#### Type Parameters

`T`

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Asynchronously creates an instance of the View with specified position and rotation,
and initializes it with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`position` Vector3

The position for the new View instance.

`rotation` Quaternion

The rotation for the new View instance.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instance.

#### Type Parameters

`T`

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, Transform, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_UnityEngine_Transform_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Asynchronously creates an instance of the View with specified parent, position, and rotation,
and initializes it with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, Transform parent, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`parent` Transform

The parent object for the new View instance.

`position` Vector3

The position for the new View instance.

`rotation` Quaternion

The rotation for the new View instance.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instance.

#### Type Parameters

`T`

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, int, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_Aspid_MVVM_IViewModel_}

Asynchronously creates multiple instances of the View and initializes them with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T`

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, int, Transform, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_Aspid_MVVM_IViewModel_}

Asynchronously creates multiple instances of the View with the specified parent
and initializes them with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent object for the new View instances.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T`

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, int, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Asynchronously creates multiple instances of the View with specified position and rotation,
and initializes them with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`position` Vector3

The position for the new View instances.

`rotation` Quaternion

The rotation for the new View instances.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T`

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, int, ReadOnlySpan\<Vector3\>, ReadOnlySpan\<Quaternion\>, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_System_ReadOnlySpan_UnityEngine_Vector3__System_ReadOnlySpan_UnityEngine_Quaternion__Aspid_MVVM_IViewModel_}

Asynchronously creates multiple View instances with specified positions and rotations,
and initializes them with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`positions` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Vector3\>

A span of positions for the new View instances.

`rotations` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Quaternion\>

A span of rotations for the new View instances.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T`

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, int, Transform, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Asynchronously creates multiple instances of the View with specified parent, position, and rotation,
and initializes them with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent object for the new View instances.

`position` Vector3

The position for the new View instances.

`rotation` Quaternion

The rotation for the new View instances.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T`

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, int, Transform, ReadOnlySpan\<Vector3\>, ReadOnlySpan\<Quaternion\>, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_System_ReadOnlySpan_UnityEngine_Vector3__System_ReadOnlySpan_UnityEngine_Quaternion__Aspid_MVVM_IViewModel_}

Asynchronously creates multiple View instances with the specified parent
and initializes them with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent object for the new View instances.

`positions` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Vector3\>

A span of positions for the new View instances.

`rotations` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Quaternion\>

A span of rotations for the new View instances.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T`

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, int, Transform, Vector3, Quaternion, IViewModel, CancellationToken\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_System_Threading_CancellationToken_}

Asynchronously creates multiple instances of View with the specified parent, position, and rotation,
and initializes them with the given [`IViewModel`](Aspid.MVVM.IViewModel.md). Supports cancellation via [`CancellationToken`](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, Vector3 position, Quaternion rotation, IViewModel viewModel, CancellationToken cancellationToken) where T : Object, IView
```

#### Parameters

`original` T

The original View object that will be instantiated.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent object for the new View instances.

`position` Vector3

The position for the new View instances.

`rotation` Quaternion

The rotation for the new View instances.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

The cancellation token for stopping the creation process.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating View instances.

#### Type Parameters

`T`

The type of View that must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InstantiateAsync\<T\>\(T, int, Transform, ReadOnlySpan\<Vector3\>, ReadOnlySpan\<Quaternion\>, IViewModel, CancellationToken\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_System_ReadOnlySpan_UnityEngine_Vector3__System_ReadOnlySpan_UnityEngine_Quaternion__Aspid_MVVM_IViewModel_System_Threading_CancellationToken_}

Asynchronously creates multiple instances of View with the specified parent, positions, and rotations,
and initializes them with the given [`IViewModel`](Aspid.MVVM.IViewModel.md). Supports cancellation via [`CancellationToken`](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, ReadOnlySpan<Vector3> positions, ReadOnlySpan<Quaternion> rotations, IViewModel viewModel, CancellationToken cancellationToken) where T : Object, IView
```

#### Parameters

`original` T

The original View object that will be instantiated.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent object for the new View instances.

`positions` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Vector3\>

A span of positions for the new View instances.

`rotations` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Quaternion\>

A span of rotations for the new View instances.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The shared ViewModel used to initialize every created view.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

The cancellation token for stopping the creation process.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating View instances.

#### Type Parameters

`T`

The type of View that must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

#### Remarks

Call on the main thread. The returned Unity operation represents object creation, not initialization success.
Initialization runs in the completion callback, which may run after the operation becomes done.
Failures escape that callback after all clones are rolled back.
Rollback preserves the initialization exception and does not dispose the ViewModel. Canceled empty results are ignored.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### OnDestroy\(\) {#Aspid_MVVM_MonoView_OnDestroy}

Called when Unity destroys the component to dispose the view.

```csharp
protected virtual void OnDestroy()
```

#### Remarks

Overrides must call the base implementation.
