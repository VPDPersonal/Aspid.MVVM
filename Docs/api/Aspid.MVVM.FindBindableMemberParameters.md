---
title: "Struct FindBindableMemberParameters"
sidebar_label: "FindBindableMemberParameters"
description: "Struct FindBindableMemberParameters — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct FindBindableMemberParameters {#Aspid_MVVM_FindBindableMemberParameters}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Represents the parameters used to search for a bindable member in a ViewModel.

```csharp
public readonly ref struct FindBindableMemberParameters
```


## Constructors

### FindBindableMemberParameters\(string\) {#Aspid_MVVM_FindBindableMemberParameters__ctor_System_String_}

Initializes a new instance of the [`FindBindableMemberParameters`](Aspid.MVVM.FindBindableMemberParameters.md) struct with the specified identifier.

```csharp
public FindBindableMemberParameters(string id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The identifier of the bindable member.

## Fields

### Id {#Aspid_MVVM_FindBindableMemberParameters_Id}

Gets the identifier of the bindable member to search for.

```csharp
public readonly string Id
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

