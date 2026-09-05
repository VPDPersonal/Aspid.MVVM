---
title: "Class AddBinderContextMenuAttribute"
sidebar_label: "AddBinderContextMenuAttribute"
description: "Class AddBinderContextMenuAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AddBinderContextMenuAttribute {#Aspid_MVVM_AddBinderContextMenuAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Editor-only attribute that offers a [`MonoBinder`](Aspid.MVVM.MonoBinder.md) in the "Add Binder" context menu of a
component, and of the specific serialized properties it names.

```csharp
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Class)]
public class AddBinderContextMenuAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[AddBinderContextMenuAttribute](Aspid.MVVM.AddBinderContextMenuAttribute.md)



## Remarks

Choosing the entry adds the binder component to the same GameObject. It does not fill any of the binder's
own fields — the property names decide <em>where the entry appears</em>, not what happens afterwards.

## Constructors

### AddBinderContextMenuAttribute\(Type, params string\[\]\) {#Aspid_MVVM_AddBinderContextMenuAttribute__ctor_System_Type_System_String___}

Initializes the attribute for the given component type.

```csharp
public AddBinderContextMenuAttribute(Type type, params string[] serializePropertyNames)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The component type this binder targets.

`serializePropertyNames` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]

Names of serialized properties to auto-populate when the binder is added via the context menu.

## Properties

### Path {#Aspid_MVVM_AddBinderContextMenuAttribute_Path}

Intended override for the root menu path. Nothing reads it yet.

```csharp
public string Path { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Set on a number of binders, but no code consults it, so the entry appears under the path derived from
the binder type name regardless. Kept because the binders that set it describe a hierarchy worth
having; documented as unimplemented rather than removed silently.

### SerializePropertyNames {#Aspid_MVVM_AddBinderContextMenuAttribute_SerializePropertyNames}

Names of serialized properties on the target component whose context menu should offer this binder.

```csharp
public string[] SerializePropertyNames { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]

#### Remarks

Matched against the leaf name of the right-clicked property, so a nested one such as
<code>m_OnClick.m_PersistentCalls.m_Calls</code> is matched as <code>m_Calls</code>. A name the component does not
have simply never matches, and the entry never appears — which is why a contract test checks them.

### SubPath {#Aspid_MVVM_AddBinderContextMenuAttribute_SubPath}

Intended override for the root menu path. Nothing reads it yet.

```csharp
public string SubPath { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Set on a number of binders, but no code consults it, so the entry appears under the path derived from
the binder type name regardless. Kept because the binders that set it describe a hierarchy worth
having; documented as unimplemented rather than removed silently.

### Type {#Aspid_MVVM_AddBinderContextMenuAttribute_Type}

The component type this binder is associated with.
Used to determine which context menus display this binder entry.

```csharp
public Type Type { get; }
```

#### Property Value

 [Type](https://learn.microsoft.com/dotnet/api/system.type)

