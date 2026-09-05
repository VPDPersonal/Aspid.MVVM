---
title: "Struct LookupEntry<TKey, TValue>"
sidebar_label: "LookupEntry<TKey, TValue>"
description: "Struct LookupEntry<TKey, TValue> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct LookupEntry\<TKey, TValue\> {#Aspid_MVVM_StarterKit_LookupEntry_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

One row of a [`DictionaryLookupConverter<T1, T2>`](Aspid.MVVM.StarterKit.DictionaryLookupConverter-2.md) table.

```csharp
[Serializable]
public struct LookupEntry<TKey, TValue>
```

#### Type Parameters

`TKey` 

The type of the key being looked up.

`TValue` 

The type of the value the key names.



## Constructors

### LookupEntry\(TKey, TValue?\) {#Aspid_MVVM_StarterKit_LookupEntry_2__ctor__0__1_}

```csharp
public LookupEntry(TKey key, TValue? value)
```

#### Parameters

`key` TKey

The key this row matches.

`value` TValue?

The value returned for the key.

## Properties

### Key {#Aspid_MVVM_StarterKit_LookupEntry_2_Key}

Gets the key this row matches.

```csharp
public readonly TKey Key { get; }
```

#### Property Value

 TKey

### Value {#Aspid_MVVM_StarterKit_LookupEntry_2_Value}

Gets the value returned for [`LookupEntry<T1, T2>.Key`](Aspid.MVVM.StarterKit.LookupEntry-2.md#Aspid_MVVM_StarterKit_LookupEntry_2_Key).

```csharp
public readonly TValue? Value { get; }
```

#### Property Value

 TValue?

