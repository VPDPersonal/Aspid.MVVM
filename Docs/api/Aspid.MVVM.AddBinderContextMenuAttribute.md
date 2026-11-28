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

Adds an Inspector context menu entry to the named serialized properties of a component.

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

Selecting the entry adds the decorated binder to the inspected component's GameObject.

## Constructors

### AddBinderContextMenuAttribute\(Type, params string\[\]\) {#Aspid_MVVM_AddBinderContextMenuAttribute__ctor_System_Type_System_String___}

Creates an entry matched by component type and property name.

```csharp
public AddBinderContextMenuAttribute(Type type, params string[] serializePropertyNames)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The target component type.

`serializePropertyNames` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]

The serialized property leaf names. An empty array matches no properties.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">type</code> or <code class="paramref">serializePropertyNames</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Path {#Aspid_MVVM_AddBinderContextMenuAttribute_Path}

Gets or sets the full menu path. A blank path uses the generated path.

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

Gets or sets the submenu under <code>Add Binder</code>. A nonblank [`AddBinderContextMenuAttribute.Path`](Aspid.MVVM.AddBinderContextMenuAttribute.md#Aspid_MVVM_AddBinderContextMenuAttribute_Path) takes precedence.

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
