---
title: "Struct VersionStamp"
sidebar_label: "VersionStamp"
description: "Struct VersionStamp — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct VersionStamp {#Aspid_MVVM_VersionStamp}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Marks a revision of an operation so earlier work can detect when it is stale.

```csharp
public readonly struct VersionStamp
```



## Remarks

Use the default value as the initial stamp and compare stamps from the same sequence.

## Methods

### Matches\(VersionStamp\) {#Aspid_MVVM_VersionStamp_Matches_Aspid_MVVM_VersionStamp_}

Checks whether two stamps mark the same revision.

```csharp
public bool Matches(VersionStamp other)
```

#### Parameters

`other` [VersionStamp](Aspid.MVVM.VersionStamp.md)

The stamp from the same sequence to compare.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the revisions match, otherwise <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Next\(\) {#Aspid_MVVM_VersionStamp_Next}

Returns the next stamp, wrapping back to the default value after the last revision.

```csharp
public VersionStamp Next()
```

#### Returns

 [VersionStamp](Aspid.MVVM.VersionStamp.md)

The next stamp in the sequence.
