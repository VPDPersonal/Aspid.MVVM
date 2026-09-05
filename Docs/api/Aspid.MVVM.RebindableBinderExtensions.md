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

Provides extension methods for [`IBinder`](Aspid.MVVM.IBinder.md) instances that implement [`IRebindableBinder`](Aspid.MVVM.IRebindableBinder.md).

```csharp
public static class RebindableBinderExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RebindableBinderExtensions](Aspid.MVVM.RebindableBinderExtensions.md)



## Remarks

Only active in <code>DEBUG</code> or <code>UNITY_EDITOR</code> builds.

## Methods

### Rebind\(IBinder\) {#Aspid_MVVM_RebindableBinderExtensions_Rebind_Aspid_MVVM_IBinder_}

Rebinds the binder if it implements [`IRebindableBinder`](Aspid.MVVM.IRebindableBinder.md); otherwise, does nothing.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
public static void Rebind(this IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder instance to attempt to rebind.

#### Remarks

Call sites are stripped by the compiler in builds where neither <code>DEBUG</code> nor <code>UNITY_EDITOR</code> is defined.

