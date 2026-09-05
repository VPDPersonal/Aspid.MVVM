---
title: "Class StringToSpriteConverter"
sidebar_label: "StringToSpriteConverter"
description: "Class StringToSpriteConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToSpriteConverter {#Aspid_MVVM_StarterKit_StringToSpriteConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Looks a sprite up by name.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Sprite", Name = "To Sprite", Tooltip = "Looks a sprite up by name")]
public sealed class StringToSpriteConverter : IConverter<string?, Sprite?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToSpriteConverter](Aspid.MVVM.StarterKit.StringToSpriteConverter.md)

#### Implements

[IConverter\<string?, Sprite?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A <code>SpriteAtlas</code> is deliberately not a source: <code>SpriteAtlas.GetSprite</code> returns a fresh
[`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html) on every call, so a binder pushing per notification would leak one per push.

## Constructors

### StringToSpriteConverter\(\) {#Aspid_MVVM_StarterKit_StringToSpriteConverter__ctor}

```csharp
public StringToSpriteConverter()
```

#### Remarks

Default: an empty map, so every key falls back to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### StringToSpriteConverter\(SpriteMapEntry\[\]?, Sprite?, bool\) {#Aspid_MVVM_StarterKit_StringToSpriteConverter__ctor_Aspid_MVVM_StarterKit_SpriteMapEntry___UnityEngine_Sprite_System_Boolean_}

```csharp
public StringToSpriteConverter(SpriteMapEntry[]? map, Sprite? fallback = null, bool ignoreCase = false)
```

#### Parameters

`map` [SpriteMapEntry](Aspid.MVVM.StarterKit.SpriteMapEntry.md)\[\]?

The keys and the sprites they name.

`fallback` Sprite?

Used when the key is blank, spaces included, or matches nothing.
When omitted, returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`ignoreCase` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to match keys without regard to case.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringToSpriteConverter_Convert_System_String_}

Looks up the sprite the specified key names.

```csharp
public Sprite? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The key to look up.

#### Returns

 Sprite?

The sprite mapped to the key, or the fallback. A blank key, spaces included, is treated as
no value rather than as a failed lookup and returns the fallback silently.

