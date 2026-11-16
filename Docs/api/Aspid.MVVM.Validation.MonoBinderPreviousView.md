---
title: "Struct MonoBinderPreviousView"
sidebar_label: "MonoBinderPreviousView"
description: "Struct MonoBinderPreviousView — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct MonoBinderPreviousView {#Aspid_MVVM_Validation_MonoBinderPreviousView}

Namespace: [Aspid.MVVM.Validation](Aspid.MVVM.Validation.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

The last non-empty View of a [`MonoBinder`](Aspid.MVVM.MonoBinder.md), kept with its name to detect a lost reference.

```csharp
[Serializable]
public struct MonoBinderPreviousView
```



## Constructors

### MonoBinderPreviousView\(Component?\) {#Aspid_MVVM_Validation_MonoBinderPreviousView__ctor_UnityEngine_Component_}

Creates a record of the supplied View and its current name.

```csharp
public MonoBinderPreviousView(Component? view)
```

#### Parameters

`view` Component?

The View to keep; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> records an empty name.

## Properties

### Name {#Aspid_MVVM_Validation_MonoBinderPreviousView_Name}

Gets the recorded name, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for a default record.

```csharp
public string? Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### View {#Aspid_MVVM_Validation_MonoBinderPreviousView_View}

Gets the recorded View, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none; the recorded object may have been destroyed.

```csharp
public Component? View { get; }
```

#### Property Value

 Component?
