---
title: "Struct PoolSettings"
sidebar_label: "PoolSettings"
description: "Struct PoolSettings — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct PoolSettings {#Aspid_MVVM_StarterKit_PoolSettings}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Size limits of a [`PrefabViewPool<T>`](Aspid.MVVM.StarterKit.PrefabViewPool-1.md).

```csharp
public readonly struct PoolSettings
```



## Constructors

### PoolSettings\(int, int\) {#Aspid_MVVM_StarterKit_PoolSettings__ctor_System_Int32_System_Int32_}

```csharp
public PoolSettings(int initialCount, int maxCount = 2147483647)
```

#### Parameters

`initialCount` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of views instantiated up front.

`maxCount` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The maximum number of inactive views kept in the pool.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">initialCount</code> is negative or <code class="paramref">maxCount</code> is less than one.

## Fields

### InitialCount {#Aspid_MVVM_StarterKit_PoolSettings_InitialCount}

The number of views instantiated up front.

```csharp
public readonly int InitialCount
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### MaxCount {#Aspid_MVVM_StarterKit_PoolSettings_MaxCount}

The maximum number of inactive views kept in the pool.

```csharp
public readonly int MaxCount
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

