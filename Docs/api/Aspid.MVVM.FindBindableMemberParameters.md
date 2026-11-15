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


## Remarks

The default instance is invalid. Built-in ViewModels validate the query before lookup.

## Constructors

### FindBindableMemberParameters\(string\) {#Aspid_MVVM_FindBindableMemberParameters__ctor_System_String_}

Creates a query for the member with identifier <code class="paramref">id</code>.

```csharp
public FindBindableMemberParameters(string id)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

The identifier of the bindable member.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">id</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">id</code> is empty or whitespace.

## Fields

### Id {#Aspid_MVVM_FindBindableMemberParameters_Id}

The identifier of the bindable member, as generated or set by [`BindIdAttribute`](Aspid.MVVM.BindIdAttribute.md).

```csharp
public readonly string Id
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### EnsureValid\(\) {#Aspid_MVVM_FindBindableMemberParameters_EnsureValid}

Ensures the query has a non-empty identifier.

```csharp
public void EnsureValid()
```

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

The identifier is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, including a default query.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

The identifier is empty or whitespace.

