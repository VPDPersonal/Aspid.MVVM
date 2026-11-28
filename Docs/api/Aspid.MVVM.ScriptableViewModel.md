---
title: "Class ScriptableViewModel"
sidebar_label: "ScriptableViewModel"
description: "Class ScriptableViewModel — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ScriptableViewModel {#Aspid_MVVM_ScriptableViewModel}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

[`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) for ViewModels that refresh bindings after Inspector changes.

```csharp
public abstract class ScriptableViewModel : ScriptableObject, IDisposable, IViewModel
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
ScriptableObject ←
[ScriptableViewModel](Aspid.MVVM.ScriptableViewModel.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable),
[IViewModel](Aspid.MVVM.IViewModel.md)


#### Extension Methods

[ViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DestroyViewModelAndGameObject\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DestroyViewModelAndGameObjectSafely\(IViewModel?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObjectSafely_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DestroyViewModelSafely\(IViewModel?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DestroyViewModelSafely_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DisposeViewModel\<ScriptableViewModel\>\(ScriptableViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel__1___0_),
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DisposeViewModelSafely\<ScriptableViewModel\>\(ScriptableViewModel?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely__1___0_),
[ViewModelExtensions.DisposeViewModelSafely\(IViewModel?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.InitializeView\(IViewModel, IView\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeView_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.InitializeViewSafely\(IViewModel?, IView?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewSafely_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, params T\[\]\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel___0___),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, Span\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Span___0__),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, ReadOnlySpan\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, List\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, IEnumerable\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, params T\[\]?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel___0___),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, Span\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Span___0__),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, ReadOnlySpan\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, List\<T\>?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.InitializeViewsSafely\<T\>\(IViewModel?, IEnumerable\<T\>?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__),
[ViewModelExtensions.ReinitializeView\(IViewModel?, IView\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeView_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.ReinitializeViewSafely\(IViewModel?, IView?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewSafely_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.ReinitializeViews\<T\>\(IViewModel?, T\[\]\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel___0___),
[ViewModelExtensions.ReinitializeViews\<T\>\(IViewModel?, Span\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_Span___0__),
[ViewModelExtensions.ReinitializeViews\<T\>\(IViewModel?, ReadOnlySpan\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__),
[ViewModelExtensions.ReinitializeViews\<T\>\(IViewModel?, List\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.ReinitializeViews\<T\>\(IViewModel?, IEnumerable\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__),
[ViewModelExtensions.ReinitializeViewsSafely\<T\>\(IViewModel?, T\[\]?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel___0___),
[ViewModelExtensions.ReinitializeViewsSafely\<T\>\(IViewModel?, Span\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Span___0__),
[ViewModelExtensions.ReinitializeViewsSafely\<T\>\(IViewModel?, ReadOnlySpan\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_ReadOnlySpan___0__),
[ViewModelExtensions.ReinitializeViewsSafely\<T\>\(IViewModel?, List\<T\>?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.ReinitializeViewsSafely\<T\>\(IViewModel?, IEnumerable\<T\>?\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_ReinitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable___0__)

## Methods

### Dispose\(\) {#Aspid_MVVM_ScriptableViewModel_Dispose}

Releases owned resources once without destroying the Unity object.

```csharp
public void Dispose()
```

#### Remarks

Cleanup is attempted once, even if [`ScriptableViewModel.OnDispose`](Aspid.MVVM.ScriptableViewModel.md#Aspid_MVVM_ScriptableViewModel_OnDispose) throws.

### FindBindableMember\(in FindBindableMemberParameters\) {#Aspid_MVVM_ScriptableViewModel_FindBindableMember_Aspid_MVVM_FindBindableMemberParameters__}

Finds the bindable member with the identifier in <code class="paramref">parameters</code>.

```csharp
public virtual FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
```

#### Parameters

`parameters` [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

The query with a nonblank binding identifier.

#### Returns

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

A result containing the matching member, or an empty result if the identifier is unknown.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The query identifier is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

The query identifier is empty or whitespace.

### NotifyAll\(\) {#Aspid_MVVM_ScriptableViewModel_NotifyAll}

```csharp
public virtual void NotifyAll()
```

### NotifyCanExecuteChangedAll\(\) {#Aspid_MVVM_ScriptableViewModel_NotifyCanExecuteChangedAll}

```csharp
public virtual void NotifyCanExecuteChangedAll()
```

### OnDestroy\(\) {#Aspid_MVVM_ScriptableViewModel_OnDestroy}

Called when Unity destroys the object to release its resources.

```csharp
protected virtual void OnDestroy()
```

#### Remarks

Overrides must call the base implementation.

### OnDispose\(\) {#Aspid_MVVM_ScriptableViewModel_OnDispose}

Called on the first disposal attempt. Override to release owned resources.

```csharp
protected virtual void OnDispose()
```

### OnValidate\(\) {#Aspid_MVVM_ScriptableViewModel_OnValidate}

Called when Unity validates serialized data. Override to validate additional fields.

```csharp
protected virtual void OnValidate()
```

#### Remarks

Call the base implementation to queue binding notifications on the Editor's main thread.
