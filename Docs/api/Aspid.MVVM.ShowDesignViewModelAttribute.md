---
title: "Class ShowDesignViewModelAttribute"
sidebar_label: "ShowDesignViewModelAttribute"
description: "Class ShowDesignViewModelAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ShowDesignViewModelAttribute {#Aspid_MVVM_ShowDesignViewModelAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Specifies which ViewModel types are available as design-time ViewModels for a View in the Unity Editor.
Apply this attribute to a [`MonoView`](Aspid.MVVM.MonoView.md) or [`ScriptableView`](Aspid.MVVM.ScriptableView.md) class to restrict
or extend the list of types shown in the design ViewModel selector.

```csharp
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Class)]
public sealed class ShowDesignViewModelAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[ShowDesignViewModelAttribute](Aspid.MVVM.ShowDesignViewModelAttribute.md)



## Constructors

### ShowDesignViewModelAttribute\(\) {#Aspid_MVVM_ShowDesignViewModelAttribute__ctor}

Initializes the attribute allowing any [`IViewModel`](Aspid.MVVM.IViewModel.md) implementation
to be used as a design ViewModel.

```csharp
public ShowDesignViewModelAttribute()
```

### ShowDesignViewModelAttribute\(params Type\[\]\) {#Aspid_MVVM_ShowDesignViewModelAttribute__ctor_System_Type___}

Initializes the attribute with multiple allowed ViewModel types.
[`IViewModel`](Aspid.MVVM.IViewModel.md) is appended automatically if none of the provided types implement it.

```csharp
public ShowDesignViewModelAttribute(params Type[] types)
```

#### Parameters

`types` [Type](https://learn.microsoft.com/dotnet/api/system.type)\[\]

The ViewModel types to include in the design ViewModel selector.

### ShowDesignViewModelAttribute\(Type, bool\) {#Aspid_MVVM_ShowDesignViewModelAttribute__ctor_System_Type_System_Boolean_}

Initializes the attribute with a single ViewModel type.

```csharp
public ShowDesignViewModelAttribute(Type type, bool strictType = false)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The ViewModel type to show in the design ViewModel selector.

`strictType` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, only the exact <code class="paramref">type</code> is shown.
The type must implement [`IViewModel`](Aspid.MVVM.IViewModel.md); otherwise an [`ArgumentException`](https://learn.microsoft.com/dotnet/api/system.argumentexception) is thrown.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

Thrown when <code class="paramref">strictType</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> and <code class="paramref">type</code>
does not implement [`IViewModel`](Aspid.MVVM.IViewModel.md).

## Fields

### StrictType {#Aspid_MVVM_ShowDesignViewModelAttribute_StrictType}

Indicates whether the type filter is strict.
When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, only the exact specified type is shown, and it must implement [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public readonly bool StrictType
```

#### Field Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Types {#Aspid_MVVM_ShowDesignViewModelAttribute_Types}

The ViewModel types available for selection in the design ViewModel dropdown.
Always includes [`IViewModel`](Aspid.MVVM.IViewModel.md) unless [`ShowDesignViewModelAttribute.StrictType`](Aspid.MVVM.ShowDesignViewModelAttribute.md#Aspid_MVVM_ShowDesignViewModelAttribute_StrictType) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>
and the provided type already implements it.

```csharp
public readonly Type[] Types
```

#### Field Value

 [Type](https://learn.microsoft.com/dotnet/api/system.type)\[\]

