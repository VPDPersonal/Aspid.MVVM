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

Represents the query passed to [`IViewModel.FindBindableMember`](Aspid.MVVM.IViewModel.md).

```csharp
public readonly ref struct FindBindableMemberParameters
```


## Constructors

### FindBindableMemberParameters\(string\) {#Aspid_MVVM_FindBindableMemberParameters__ctor_System_String_}

Creates a query for the member with identifier <code class="paramref">id</code>.

```csharp
public FindBindableMemberParameters(string id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The identifier of the bindable member.

## Fields

### Id {#Aspid_MVVM_FindBindableMemberParameters_Id}

The identifier of the bindable member, as generated or set by [`BindIdAttribute`](Aspid.MVVM.BindIdAttribute.md).

```csharp
public readonly string Id
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

