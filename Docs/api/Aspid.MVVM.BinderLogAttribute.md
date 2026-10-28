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

Generates an explicit [`IBinder<T>.SetValue`](Aspid.MVVM.IBinder-1.md#Aspid_MVVM_IBinder_1_SetValue__0_) implementation that records each value in an Inspector log
before calling the marked method.

```csharp
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Method)]
public sealed class BinderLogAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BinderLogAttribute](Aspid.MVVM.BinderLogAttribute.md)



## Examples


```csharp
[BinderLog]
public void SetValue(string value) => _text.text = value;
```


## Remarks

<p>
Mark a <code>SetValue</code> method with one parameter that implicitly implements [`IBinder<T>`](Aspid.MVVM.IBinder-1.md), or
[`IAnyBinder`](Aspid.MVVM.IAnyBinder.md) when it is generic, in a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/partial-method">partial</a> type.
</p>
<p>
The log records values while its Inspector toggle is on, in the Editor only; <code>ASPID_MVVM_BINDER_LOG_DISABLED</code>
removes it.
</p>

