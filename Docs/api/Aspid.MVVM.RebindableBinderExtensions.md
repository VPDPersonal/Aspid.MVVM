---
title: "Class RebindableBinderExtensions"
sidebar_label: "RebindableBinderExtensions"
description: "Class RebindableBinderExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RebindableBinderExtensions {#Aspid_MVVM_RebindableBinderExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Provides extension methods for rebinding an [`IBinder`](Aspid.MVVM.IBinder.md).

```csharp
public static class RebindableBinderExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RebindableBinderExtensions](Aspid.MVVM.RebindableBinderExtensions.md)



## Methods

### Rebind\(IBinder\) {#Aspid_MVVM_RebindableBinderExtensions_Rebind_Aspid_MVVM_IBinder_}

Rebinds <code class="paramref">binder</code> when it implements [`IRebindableBinder`](Aspid.MVVM.IRebindableBinder.md); otherwise, does nothing.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
public static void Rebind(this IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder to rebind.

#### Remarks

Calls are removed from builds that define neither <code>DEBUG</code> nor <code>UNITY_EDITOR</code>.

