---
title: "Class BinderLogAttribute"
sidebar_label: "BinderLogAttribute"
description: "Class BinderLogAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BinderLogAttribute {#Aspid_MVVM_BinderLogAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Instructs the Source Generator to generate an explicit [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) [`IBinder<T>.SetValue`](Aspid.MVVM.IBinder-1.md#Aspid_MVVM_IBinder_1_SetValue__0_) implementation
with added logging, wrapping the annotated method.

```csharp
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Method)]
public sealed class BinderLogAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BinderLogAttribute](Aspid.MVVM.BinderLogAttribute.md)



## Remarks

Must be applied only to [`IBinder<T>.SetValue`](Aspid.MVVM.IBinder-1.md#Aspid_MVVM_IBinder_1_SetValue__0_) methods that implicitly implement [`IBinder<T>`](Aspid.MVVM.IBinder-1.md)
in a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/partial-method">partial</a> class.
The attribute is stripped in non-<code>UNITY_EDITOR</code> builds, so no logging is generated outside the Editor.

