---
title: "Class AsBinderAttribute"
sidebar_label: "AsBinderAttribute"
description: "Class AsBinderAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AsBinderAttribute {#Aspid_MVVM_AsBinderAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Wraps the field or property of a [`[View]`](Aspid.MVVM.ViewAttribute.md) class in a binder of [`AsBinderAttribute.Type`](Aspid.MVVM.AsBinderAttribute.md#Aspid_MVVM_AsBinderAttribute_Type), created with the member as its first
constructor argument.

```csharp
[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
public sealed class AsBinderAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[AsBinderAttribute](Aspid.MVVM.AsBinderAttribute.md)



## Examples


```csharp
[AsBinder(typeof(ImageSpriteBinder))]
[SerializeField] private Image _icon;
```


```csharp
[AsBinder(typeof(AnimatorSetFloatBinder), "Speed")]
[SerializeField] private Animator _speed;
```


## Remarks

For an array, creates one binder per element.

## Constructors

### AsBinderAttribute\(Type, params object\[\]\) {#Aspid_MVVM_AsBinderAttribute__ctor_System_Type_System_Object___}

Creates an attribute that wraps the member in a binder of <code class="paramref">type</code>.

```csharp
public AsBinderAttribute(Type type, params object[] arguments)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The binder type: a non-abstract [`IBinder`](Aspid.MVVM.IBinder.md); any other type skips the member.

`arguments` [object](https://learn.microsoft.com/dotnet/api/system.object)\[\]

The binder constructor arguments that follow the member; a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/operators/nameof">nameof</a> expression passes the named member
of the View.

## Fields

### Type {#Aspid_MVVM_AsBinderAttribute_Type}

Gets the binder type.

```csharp
public readonly Type Type
```

#### Field Value

 [Type](https://learn.microsoft.com/dotnet/api/system.type)

#### Remarks

Exists only when <code>UNITY_EDITOR</code> or <code>DEBUG</code> is defined.

