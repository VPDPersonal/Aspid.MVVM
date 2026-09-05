---
title: "Class ViewInitializer"
sidebar_label: "ViewInitializer"
description: "Class ViewInitializer — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewInitializer {#Aspid_MVVM_StarterKit_ViewInitializer}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ViewInitializerBase`](Aspid.MVVM.StarterKit.ViewInitializerBase.md) that resolves its ViewModel from a serialized slot and
initializes the views at the chosen lifecycle stage.

```csharp
[AddComponentMenu("Aspid/MVVM/View Initializers/View Initializer")]
[AddBinderContextMenu(typeof(MonoView), new string[] { }, Path = "Add View Initializers/View Initializer")]
public sealed class ViewInitializer : ViewInitializerBase
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[ViewInitializerBase](Aspid.MVVM.StarterKit.ViewInitializerBase.md) ← 
[ViewInitializer](Aspid.MVVM.StarterKit.ViewInitializer.md)



## Properties

### ViewModel {#Aspid_MVVM_StarterKit_ViewInitializer_ViewModel}

Gets the ViewModel. Resolved once in play mode and on every call in edit mode.

```csharp
public override IViewModel ViewModel { get; }
```

#### Property Value

 [IViewModel](Aspid.MVVM.IViewModel.md)

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when the ViewModel slot is empty.

## Methods

### Deinitialize\(\) {#Aspid_MVVM_StarterKit_ViewInitializer_Deinitialize}

Deinitializes the views. Allowed only in the <code>Manual</code> stage.

```csharp
public void Deinitialize()
```

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when the stage is not <code>Manual</code>.

### Initialize\(\) {#Aspid_MVVM_StarterKit_ViewInitializer_Initialize}

Initializes the views. Allowed only in the <code>Manual</code> stage.

```csharp
public void Initialize()
```

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when the stage is not <code>Manual</code>.

### OnDestroy\(\) {#Aspid_MVVM_StarterKit_ViewInitializer_OnDestroy}

Disposes the already resolved views if [`ViewInitializerBase.IsDisposeViewOnDestroy`](Aspid.MVVM.StarterKit.ViewInitializerBase.md#Aspid_MVVM_StarterKit_ViewInitializerBase_IsDisposeViewOnDestroy) is set.

```csharp
protected override void OnDestroy()
```

### OnValidate\(\) {#Aspid_MVVM_StarterKit_ViewInitializer_OnValidate}

Keeps the serialized view slots consistent with their resolve mode.

```csharp
protected override void OnValidate()
```

