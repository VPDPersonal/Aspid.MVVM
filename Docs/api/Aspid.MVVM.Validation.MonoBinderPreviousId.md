---
title: "Struct MonoBinderPreviousId"
sidebar_label: "MonoBinderPreviousId"
description: "Struct MonoBinderPreviousId — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct MonoBinderPreviousId {#Aspid_MVVM_Validation_MonoBinderPreviousId}

Namespace: [Aspid.MVVM.Validation](Aspid.MVVM.Validation.md)  
Assembly: Aspid.MVVM.Unity.dll  

The last non-empty ID of a [`MonoBinder`](Aspid.MVVM.MonoBinder.md), kept to detect a renamed View field.

```csharp
[Serializable]
public struct MonoBinderPreviousId
```



## Constructors

### MonoBinderPreviousId\(string\) {#Aspid_MVVM_Validation_MonoBinderPreviousId__ctor_System_String_}

```csharp
public MonoBinderPreviousId(string id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The ID to keep.

## Properties

### Id {#Aspid_MVVM_Validation_MonoBinderPreviousId_Id}

Gets the ID.

```csharp
public string Id { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

