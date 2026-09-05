---
title: "Struct ShaderPropertyId"
sidebar_label: "ShaderPropertyId"
description: "Struct ShaderPropertyId — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct ShaderPropertyId {#Aspid_MVVM_StarterKit_ShaderPropertyId}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Caches the id a shader property name resolves to.

```csharp
public struct ShaderPropertyId
```



## Methods

### Resolve\(string\) {#Aspid_MVVM_StarterKit_ShaderPropertyId_Resolve_System_String_}

Returns the id <code class="paramref">name</code> resolves to, resolving it once per name.

```csharp
public int Resolve(string name)
```

#### Parameters

`name` [string](https://learn.microsoft.com/dotnet/api/system.string)

The shader property name, as the shader declares it.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

The id the name resolves to.

#### Remarks

Re-resolves when the name changes, which the Inspector allows at any time.

