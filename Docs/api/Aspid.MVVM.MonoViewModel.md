---
title: "Class MonoViewModel"
sidebar_label: "MonoViewModel"
description: "Class MonoViewModel — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MonoViewModel {#Aspid_MVVM_MonoViewModel}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

[`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) for ViewModels that refresh bindings after Inspector changes.

```csharp
public abstract class MonoViewModel : MonoBehaviour, IDisposable, IViewModel
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoViewModel](Aspid.MVVM.MonoViewModel.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable),
[IViewModel](Aspid.MVVM.IViewModel.md)


#### Extension Methods

[MonoViewModelExtensions.DestroyViewModel\<MonoViewModel\>\(MonoViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel__1___0_),
[MonoViewModelExtensions.DestroyViewModel\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_),
[MonoViewModelExtensions.DestroyViewModelAndGameObject\<MonoViewModel\>\(MonoViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModelAndGameObject__1___0_),
[MonoViewModelExtensions.DestroyViewModelAndGameObject\(IViewModel\)](Aspid.MVVM.MonoViewModelExtensions.md#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.DisposeViewModel\<MonoViewModel\>\(MonoViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel__1___0_),
[ViewModelExtensions.DisposeViewModel\(IViewModel\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_),
[ViewModelExtensions.InitializeView\(IViewModel, IView\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeView_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_),
[ViewModelExtensions.InitializeViews\(IViewModel, params IView\[\]\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews_Aspid_MVVM_IViewModel_Aspid_MVVM_IView___),
[ViewModelExtensions.InitializeViews\<T\>\(IViewModel, List\<T\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__),
[ViewModelExtensions.InitializeViews\(IViewModel, IEnumerable\<IView\>\)](Aspid.MVVM.ViewModelExtensions.md#Aspid_MVVM_ViewModelExtensions_InitializeViews_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable_Aspid_MVVM_IView__)

## Methods

### Dispose\(\) {#Aspid_MVVM_MonoViewModel_Dispose}

Releases owned resources once without destroying the Unity object.

```csharp
public void Dispose()
```

#### Remarks

Repeated and reentrant calls do nothing, including after a cleanup failure.
Override [`MonoViewModel.OnDispose`](Aspid.MVVM.MonoViewModel.md#Aspid_MVVM_MonoViewModel_OnDispose) to release resources.

### FindBindableMember\(in FindBindableMemberParameters\) {#Aspid_MVVM_MonoViewModel_FindBindableMember_Aspid_MVVM_FindBindableMemberParameters__}

Finds the bindable member with the identifier in <code class="paramref">parameters</code>.

```csharp
public virtual FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
```

#### Parameters

`parameters` [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

The lookup query with a non-null identifier that is not empty or whitespace.

#### Returns

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

The lookup result; [`FindBindableMemberResult.IsFound`](Aspid.MVVM.FindBindableMemberResult.md#Aspid_MVVM_FindBindableMemberResult_IsFound) indicates whether it contains a member.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The query identifier is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

The query identifier is empty or whitespace.

### NotifyAll\(\) {#Aspid_MVVM_MonoViewModel_NotifyAll}

```csharp
public virtual void NotifyAll()
```

### NotifyCanExecuteChangedAll\(\) {#Aspid_MVVM_MonoViewModel_NotifyCanExecuteChangedAll}

```csharp
public virtual void NotifyCanExecuteChangedAll()
```

### OnDestroy\(\) {#Aspid_MVVM_MonoViewModel_OnDestroy}

Called when Unity destroys the object to release its resources.

```csharp
protected virtual void OnDestroy()
```

#### Remarks

Overrides must call the base implementation. Use the destruction helpers when Unity does not issue this callback,
including for components that have never been active in Play Mode.

### OnDispose\(\) {#Aspid_MVVM_MonoViewModel_OnDispose}

Called on the first disposal attempt. Override to release owned resources and subscriptions.

```csharp
protected virtual void OnDispose()
```

#### Remarks

Cleanup exceptions propagate; cleanup is not retried.

### OnValidate\(\) {#Aspid_MVVM_MonoViewModel_OnValidate}

Called when Unity validates serialized data. Override to validate additional fields.

```csharp
protected virtual void OnValidate()
```

#### Remarks

Call the base implementation to queue binding notifications for the next Editor update on the main thread.
