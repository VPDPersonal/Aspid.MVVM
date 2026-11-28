---
title: "Class BinderLogAttribute"
sidebar_label: "BinderLogAttribute"
description: "Class BinderLogAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BinderLogAttribute {#Aspid_MVVM_BinderLogAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Records incoming values in the Inspector before calling the marked <code>SetValue</code> method.

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
The method must implicitly implement [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) or [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md) in a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/partial-method">partial</a> type.
</p>
<p>
Recording runs only in the Editor while enabled in the Inspector. Define <code>ASPID_MVVM_BINDER_LOG_DISABLED</code> to omit the log.
</p>
