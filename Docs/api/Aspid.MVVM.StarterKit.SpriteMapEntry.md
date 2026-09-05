---
title: "Struct SpriteMapEntry"
sidebar_label: "SpriteMapEntry"
description: "Struct SpriteMapEntry — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct SpriteMapEntry {#Aspid_MVVM_StarterKit_SpriteMapEntry}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

One key of a [`StringToSpriteConverter`](Aspid.MVVM.StarterKit.StringToSpriteConverter.md) map, with the sprite it names.

```csharp
[Serializable]
public struct SpriteMapEntry
```



## Constructors

### SpriteMapEntry\(string, Sprite?\) {#Aspid_MVVM_StarterKit_SpriteMapEntry__ctor_System_String_UnityEngine_Sprite_}

```csharp
public SpriteMapEntry(string key, Sprite? sprite)
```

#### Parameters

`key` [string](https://learn.microsoft.com/dotnet/api/system.string)

The key the sprite is looked up by.

`sprite` Sprite?

The sprite that key names, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to map the key to nothing.

## Properties

### Key {#Aspid_MVVM_StarterKit_SpriteMapEntry_Key}

Gets the key the sprite is looked up by.

```csharp
public readonly string Key { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### Sprite {#Aspid_MVVM_StarterKit_SpriteMapEntry_Sprite}

Gets the sprite [`SpriteMapEntry.Key`](Aspid.MVVM.StarterKit.SpriteMapEntry.md#Aspid_MVVM_StarterKit_SpriteMapEntry_Key) names, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the key maps to nothing.

```csharp
public readonly Sprite? Sprite { get; }
```

#### Property Value

 Sprite?

