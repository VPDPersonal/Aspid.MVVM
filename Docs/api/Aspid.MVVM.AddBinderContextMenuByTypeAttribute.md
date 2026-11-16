---
title: "Class AddBinderContextMenuByTypeAttribute"
sidebar_label: "AddBinderContextMenuByTypeAttribute"
description: "Class AddBinderContextMenuByTypeAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AddBinderContextMenuByTypeAttribute {#Aspid_MVVM_AddBinderContextMenuByTypeAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

Offers the decorated component in serialized property menus matching the property's value type.

```csharp
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class AddBinderContextMenuByTypeAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ←
[AddBinderContextMenuByTypeAttribute](Aspid.MVVM.AddBinderContextMenuByTypeAttribute.md)



## Examples


```csharp
[AddBinderContextMenuByType(typeof(float))]
public sealed class FloatBinder : MonoBinder { }
```


## Remarks

Multiple applications support different value types. Applications are Editor-only.

## Constructors

### AddBinderContextMenuByTypeAttribute\(Type\) {#Aspid_MVVM_AddBinderContextMenuByTypeAttribute__ctor_System_Type_}

Creates an entry matched by serialized property value type.

```csharp
public AddBinderContextMenuByTypeAttribute(Type propertyType)
```

#### Parameters

`propertyType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The type assignable from the serialized property's value type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">propertyType</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Type {#Aspid_MVVM_AddBinderContextMenuByTypeAttribute_Type}

Gets the value type accepted by the property menu entry.

```csharp
public Type Type { get; }
```

#### Property Value

 [Type](https://learn.microsoft.com/dotnet/api/system.type)
