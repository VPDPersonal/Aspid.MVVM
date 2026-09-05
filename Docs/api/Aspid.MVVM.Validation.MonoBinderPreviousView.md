---
title: "Struct MonoBinderPreviousView"
sidebar_label: "MonoBinderPreviousView"
description: "Struct MonoBinderPreviousView — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct MonoBinderPreviousView {#Aspid_MVVM_Validation_MonoBinderPreviousView}

Namespace: [Aspid.MVVM.Validation](Aspid.MVVM.Validation.md)  
Assembly: Aspid.MVVM.Unity.dll  

The last non-empty View of a [`MonoBinder`](Aspid.MVVM.MonoBinder.md), kept with its name to detect a lost reference.

```csharp
[Serializable]
public struct MonoBinderPreviousView
```



## Constructors

### MonoBinderPreviousView\(Component\) {#Aspid_MVVM_Validation_MonoBinderPreviousView__ctor_UnityEngine_Component_}

```csharp
public MonoBinderPreviousView(Component view)
```

#### Parameters

`view` Component

The View to keep.

## Properties

### Name {#Aspid_MVVM_Validation_MonoBinderPreviousView_Name}

Gets the name the View had when it was set.

```csharp
public string Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### View {#Aspid_MVVM_Validation_MonoBinderPreviousView_View}

Gets the View.

```csharp
public Component View { get; }
```

#### Property Value

 Component

