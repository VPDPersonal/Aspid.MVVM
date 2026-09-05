---
title: "Class MonoView"
sidebar_label: "MonoView"
description: "Class MonoView — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MonoView {#Aspid_MVVM_MonoView}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Represents a base class for views in a Unity context that are derived from [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html).
Implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) to allow cleanup of resources, including the destruction of the associated GameObject.

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

[ViewExtensions.DeinitializeView\<MonoView\>\(MonoView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DeinitializeView__1___0_), 
[MonoViewExtensions.DestroyView\<MonoView\>\(MonoView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyView__1___0_), 
[MonoViewExtensions.DestroyView\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyView_Aspid_MVVM_IView_), 
[MonoViewExtensions.DestroyViewAndGameObject\<MonoView\>\(MonoView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject__1___0_), 
[MonoViewExtensions.DestroyViewAndGameObject\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_), 
[ViewExtensions.DisposeView\<MonoView\>\(MonoView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView__1___0_), 
[ViewExtensions.DisposeView\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_), 
[ViewExtensions.Reinitialize\(IView?, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_)

## Properties

### ViewModel {#Aspid_MVVM_MonoView_ViewModel}

Gets the associated ViewModel.
If the view is not initialized, it may return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public IViewModel ViewModel { get; protected set; }
```

#### Property Value

 [IViewModel](Aspid.MVVM.IViewModel.md)

## Methods

### Deinitialize\(\) {#Aspid_MVVM_MonoView_Deinitialize}

Deinitializes the view, resetting the ViewModel property to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public void Deinitialize()
```

### DeinitializeInternal\(\) {#Aspid_MVVM_MonoView_DeinitializeInternal}

```csharp
protected virtual void DeinitializeInternal()
```

### Dispose\(\) {#Aspid_MVVM_MonoView_Dispose}

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public virtual void Dispose()
```

### Initialize\(IViewModel\) {#Aspid_MVVM_MonoView_Initialize_Aspid_MVVM_IViewModel_}

Initializes the view with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md) for binding.

```csharp
public void Initialize(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The [`IViewModel`](Aspid.MVVM.IViewModel.md) object used to initialize the View.

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

### Instantiate\<T\>\(T, Transform, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Transform_Aspid_MVVM_IViewModel_}

Creates an instance of the View with the specified parent and initializes it with the given [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static T Instantiate<T>(T original, Transform parent, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`parent` Transform

The parent object for the new View instance.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

#### Returns

 T

The created instance of the View.

#### Type Parameters

`T` 

The type of the View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

### Instantiate\<T\>\(T, Transform, bool, IViewModel\) {#Aspid_MVVM_MonoView_Instantiate__1___0_UnityEngine_Transform_System_Boolean_Aspid_MVVM_IViewModel_}

Creates an instance of the View with the specified parent and initializes it with the given [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`parent` Transform

The parent object for the new View instance.

`worldPositionStays` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the new instance should retain its world position.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

#### Returns

 T

The created instance of the View.

#### Type Parameters

`T` 

The type of the View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

The parent object for the new View instance.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

#### Returns

 T

The created instance of the View.

#### Type Parameters

`T` 

The type of the View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

### InstantiateAsync\<T\>\(T, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_Aspid_MVVM_IViewModel_}

Asynchronously creates an instance of the View and initializes it with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instance.

#### Type Parameters

`T` 

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

The ViewModel for initialization.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instance.

#### Type Parameters

`T` 

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

The ViewModel for initialization.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instance.

#### Type Parameters

`T` 

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

The ViewModel for initialization.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instance.

#### Type Parameters

`T` 

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

The ViewModel for initialization.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T` 

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

The ViewModel for initialization.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T` 

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

The ViewModel for initialization.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T` 

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

An array of positions for the new View instances.

`rotations` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Quaternion\>

An array of rotations for the new View instances.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for the View.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T` 

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

### InstantiateAsync\<T\>\(T, int, Transform, Vector3, Quaternion, IViewModel\) {#Aspid_MVVM_MonoView_InstantiateAsync__1___0_System_Int32_UnityEngine_Transform_UnityEngine_Vector3_UnityEngine_Quaternion_Aspid_MVVM_IViewModel_}

Asynchronously creates an instance of the View with specified parent, position, and rotation,
and initializes it with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static AsyncInstantiateOperation<T> InstantiateAsync<T>(T original, int count, Transform parent, Vector3 position, Quaternion rotation, IViewModel viewModel) where T : Object, IView
```

#### Parameters

`original` T

The original View object to be instantiated.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of instances to create.

`parent` Transform

The parent object for the new View instance.

`position` Vector3

The position for the new View instance.

`rotation` Quaternion

The rotation for the new View instance.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instance.

#### Type Parameters

`T` 

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

An array of positions for the new View instances.

`rotations` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Quaternion\>

An array of rotations for the new View instances.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for the View.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating the View instances.

#### Type Parameters

`T` 

The type of View, which must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

The ViewModel for initialization.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

The cancellation token for stopping the creation process.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating View instances.

#### Type Parameters

`T` 

The type of View that must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

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

An array of positions for the new View instances.

`rotations` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1)\<Quaternion\>

An array of rotations for the new View instances.

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel for initialization.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

The cancellation token for stopping the creation process.

#### Returns

 AsyncInstantiateOperation\<T\>

An operation representing the asynchronous process of creating View instances.

#### Type Parameters

`T` 

The type of View that must inherit from [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) and implement [`IView`](Aspid.MVVM.IView.md).

### OnDestroy\(\) {#Aspid_MVVM_MonoView_OnDestroy}

```csharp
protected virtual void OnDestroy()
```

