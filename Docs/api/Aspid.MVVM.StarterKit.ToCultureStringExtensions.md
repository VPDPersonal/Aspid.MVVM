---
title: "Class ToCultureStringExtensions"
sidebar_label: "ToCultureStringExtensions"
description: "Class ToCultureStringExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ToCultureStringExtensions {#Aspid_MVVM_StarterKit_ToCultureStringExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Provides extension methods for [`CultureInfoMode`](Aspid.MVVM.StarterKit.CultureInfoMode.md).

```csharp
public static class ToCultureStringExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ToCultureStringExtensions](Aspid.MVVM.StarterKit.ToCultureStringExtensions.md)



## Methods

### ToCultureInfo\(CultureInfoMode\) {#Aspid_MVVM_StarterKit_ToCultureStringExtensions_ToCultureInfo_Aspid_MVVM_StarterKit_CultureInfoMode_}

Resolves the culture a [`CultureInfoMode`](Aspid.MVVM.StarterKit.CultureInfoMode.md) names.

```csharp
public static CultureInfo ToCultureInfo(this CultureInfoMode mode)
```

#### Parameters

`mode` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The mode to resolve.

#### Returns

 [CultureInfo](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo)

The named culture; an undeclared mode is reported and reads as the current culture.

#### Remarks

Both [`DefaultThreadCurrentCulture`](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo.defaultthreadcurrentculture) and
[`DefaultThreadCurrentUICulture`](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo.defaultthreadcurrentuiculture) are <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> until an
application sets them, so those modes read as the corresponding current culture.

### ToCultureString\(int, CultureInfoMode\) {#Aspid_MVVM_StarterKit_ToCultureStringExtensions_ToCultureString_System_Int32_Aspid_MVVM_StarterKit_CultureInfoMode_}

Writes the specified number in the culture the mode names.

```csharp
public static string ToCultureString(this int number, CultureInfoMode mode)
```

#### Parameters

`number` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number to write.

`mode` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture to write it in.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The number as text.

### ToCultureString\(uint, CultureInfoMode\) {#Aspid_MVVM_StarterKit_ToCultureStringExtensions_ToCultureString_System_UInt32_Aspid_MVVM_StarterKit_CultureInfoMode_}

Writes the specified number in the culture the mode names.

```csharp
public static string ToCultureString(this uint number, CultureInfoMode mode)
```

#### Parameters

`number` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The number to write.

`mode` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture to write it in.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The number as text.

### ToCultureString\(long, CultureInfoMode\) {#Aspid_MVVM_StarterKit_ToCultureStringExtensions_ToCultureString_System_Int64_Aspid_MVVM_StarterKit_CultureInfoMode_}

Writes the specified number in the culture the mode names.

```csharp
public static string ToCultureString(this long number, CultureInfoMode mode)
```

#### Parameters

`number` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The number to write.

`mode` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture to write it in.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The number as text.

### ToCultureString\(double, CultureInfoMode\) {#Aspid_MVVM_StarterKit_ToCultureStringExtensions_ToCultureString_System_Double_Aspid_MVVM_StarterKit_CultureInfoMode_}

Writes the specified number in the culture the mode names.

```csharp
public static string ToCultureString(this double number, CultureInfoMode mode)
```

#### Parameters

`number` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to write.

`mode` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture to write it in.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The number as text.

### ToCultureString\(float, CultureInfoMode\) {#Aspid_MVVM_StarterKit_ToCultureStringExtensions_ToCultureString_System_Single_Aspid_MVVM_StarterKit_CultureInfoMode_}

Writes the specified number in the culture the mode names.

```csharp
public static string ToCultureString(this float number, CultureInfoMode mode)
```

#### Parameters

`number` [float](https://learn.microsoft.com/dotnet/api/system.single)

The number to write.

`mode` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture to write it in.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The number as text.

### ToCultureString\(decimal, CultureInfoMode\) {#Aspid_MVVM_StarterKit_ToCultureStringExtensions_ToCultureString_System_Decimal_Aspid_MVVM_StarterKit_CultureInfoMode_}

Writes the specified number in the culture the mode names.

```csharp
public static string ToCultureString(this decimal number, CultureInfoMode mode)
```

#### Parameters

`number` [decimal](https://learn.microsoft.com/dotnet/api/system.decimal)

The number to write.

`mode` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture to write it in.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The number as text.

