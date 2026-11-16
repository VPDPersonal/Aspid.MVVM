---
title: "Struct MonoBinderPreviousId"
sidebar_label: "MonoBinderPreviousId"
description: "Struct MonoBinderPreviousId — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct MonoBinderPreviousId {#Aspid_MVVM_Validation_MonoBinderPreviousId}

Namespace: [Aspid.MVVM.Validation](Aspid.MVVM.Validation.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

The last non-empty ID of a [`MonoBinder`](Aspid.MVVM.MonoBinder.md), kept to detect a renamed View field.

```csharp
[Serializable]
public struct MonoBinderPreviousId
```



## Constructors

### MonoBinderPreviousId\(string?\) {#Aspid_MVVM_Validation_MonoBinderPreviousId__ctor_System_String_}

Creates a record of the supplied ID.

```csharp
public MonoBinderPreviousId(string? id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The ID to keep; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for none.

## Properties

### Id {#Aspid_MVVM_Validation_MonoBinderPreviousId_Id}

Gets the ID, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when no ID has been recorded.

```csharp
public string? Id { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?
