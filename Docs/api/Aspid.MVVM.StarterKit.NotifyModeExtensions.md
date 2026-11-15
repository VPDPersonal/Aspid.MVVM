---
title: "Class NotifyModeExtensions"
sidebar_label: "NotifyModeExtensions"
description: "Class NotifyModeExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NotifyModeExtensions {#Aspid_MVVM_StarterKit_NotifyModeExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

Provides extension methods for [`NotifyMode`](Aspid.MVVM.StarterKit.NotifyMode.md).

```csharp
public static class NotifyModeExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[NotifyModeExtensions](Aspid.MVVM.StarterKit.NotifyModeExtensions.md)



## Methods

### ShouldNotify\(NotifyMode, bool\) {#Aspid_MVVM_StarterKit_NotifyModeExtensions_ShouldNotify_Aspid_MVVM_StarterKit_NotifyMode_System_Boolean_}

Returns whether a write should raise the change event of the bound component.

```csharp
public static bool ShouldNotify(this NotifyMode mode, bool isBound)
```

#### Parameters

`mode` [NotifyMode](Aspid.MVVM.StarterKit.NotifyMode.md)

The notify mode of the binder.

`isBound` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The binder's <code>IsBound</code> at the time of the write.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the write should raise the event; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Remarks

The ViewModel pushes its first value before the binder is marked bound, so a write made while
<code class="paramref">isBound</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> counts as the write made while binding.
