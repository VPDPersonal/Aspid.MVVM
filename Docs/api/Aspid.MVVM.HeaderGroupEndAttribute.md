---
title: "Class HeaderGroupEndAttribute"
sidebar_label: "HeaderGroupEndAttribute"
description: "Class HeaderGroupEndAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class HeaderGroupEndAttribute {#Aspid_MVVM_HeaderGroupEndAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Editor-only marker that closes the currently open foldout group before the decorated
binder field is processed. The decorated field itself is rendered outside of the closed group.
Stripped from builds outside of <code>DEBUG</code> and <code>UNITY_EDITOR</code> configurations.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Field)]
public sealed class HeaderGroupEndAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[HeaderGroupEndAttribute](Aspid.MVVM.HeaderGroupEndAttribute.md)


