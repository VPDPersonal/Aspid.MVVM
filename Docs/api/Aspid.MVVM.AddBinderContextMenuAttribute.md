---
title: "Class AddBinderContextMenuAttribute"
sidebar_label: "AddBinderContextMenuAttribute"
description: "Class AddBinderContextMenuAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AddBinderContextMenuAttribute {#Aspid_MVVM_AddBinderContextMenuAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

Offers the decorated component in a target component menu and in the menus of its named serialized properties.

```csharp
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Class)]
public class AddBinderContextMenuAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ←
[AddBinderContextMenuAttribute](Aspid.MVVM.AddBinderContextMenuAttribute.md)



## Examples


```csharp
[AddBinderContextMenu(typeof(UnityEngine.Renderer), "m_Enabled", SubPath = "Enabled")]
public sealed class EnabledBinder : MonoBinder { }
```


## Remarks

Adds the component to the same GameObject without populating its fields. Applications are Editor-only.

## Constructors

### AddBinderContextMenuAttribute\(Type, params string\[\]\) {#Aspid_MVVM_AddBinderContextMenuAttribute__ctor_System_Type_System_String___}

Creates an entry restricted to the supplied component type and property names.

```csharp
public AddBinderContextMenuAttribute(Type type, params string[] serializePropertyNames)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The target component type.

`serializePropertyNames` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]

The serialized property leaf names; an empty array offers only the component menu entry.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

An argument is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Path {#Aspid_MVVM_AddBinderContextMenuAttribute_Path}

Gets or sets the complete menu entry path; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> uses the generated path.

```csharp
public string? Path { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### SerializePropertyNames {#Aspid_MVVM_AddBinderContextMenuAttribute_SerializePropertyNames}

Gets the leaf names of serialized properties whose context menus offer the entry.

```csharp
public string[] SerializePropertyNames { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]

### SubPath {#Aspid_MVVM_AddBinderContextMenuAttribute_SubPath}

Gets or sets the group below the generated menu root; ignored when [`AddBinderContextMenuAttribute.Path`](Aspid.MVVM.AddBinderContextMenuAttribute.md#Aspid_MVVM_AddBinderContextMenuAttribute_Path) is specified.

```csharp
public string? SubPath { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### Type {#Aspid_MVVM_AddBinderContextMenuAttribute_Type}

Gets the component type whose serialized properties offer this entry.

```csharp
public Type Type { get; }
```

#### Property Value

 [Type](https://learn.microsoft.com/dotnet/api/system.type)
