---
title: "Class ConverterLogger"
sidebar_label: "ConverterLogger"
description: "Class ConverterLogger — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ConverterLogger {#Aspid_MVVM_StarterKit_ConverterLogger}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes converter messages in one shape shared by all converters.

```csharp
public static class ConverterLogger
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ConverterLogger](Aspid.MVVM.StarterKit.ConverterLogger.md)



## Remarks

The [`Type`](https://learn.microsoft.com/dotnet/api/system.type) overloads are for helpers reporting on another converter's behalf.

## Methods

### Log\(IConverter, string, Object?\) {#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_}

Logs an informational message.

```csharp
[HideInCallstack]
public static void Log(this IConverter converter, string message, Object? context = null)
```

#### Parameters

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter.md)

The logging converter; a scene or asset object is pinged by default.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The message, as full sentences.

`context` Object?

The object to ping instead of the converter.

### Log\(Type, string, Object?\) {#Aspid_MVVM_StarterKit_ConverterLogger_Log_System_Type_System_String_UnityEngine_Object_}

Logs an informational message on behalf of the specified converter type.

```csharp
[HideInCallstack]
public static void Log(Type converterType, string message, Object? context = null)
```

#### Parameters

`converterType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The logging converter's type.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The message, as full sentences.

`context` Object?

The object to ping, when one is known.

### LogError\(IConverter, string, string, Object?\) {#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_}

Reports a problem: a value that would not convert, a bad setting, an impossible reverse conversion.

```csharp
[HideInCallstack]
public static void LogError(this IConverter converter, string problem, string consequence, Object? context = null)
```

#### Parameters

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter.md)

The reporting converter; a scene or asset object is pinged by default.

`problem` [string](https://learn.microsoft.com/dotnet/api/system.string)

What is wrong, as a sentence without the trailing period.

`consequence` [string](https://learn.microsoft.com/dotnet/api/system.string)

What the converter does instead, as a full sentence.

`context` Object?

The object to ping instead of the converter.

### LogError\(Type, string, string, Object?\) {#Aspid_MVVM_StarterKit_ConverterLogger_LogError_System_Type_System_String_System_String_UnityEngine_Object_}

Reports a problem on behalf of the specified converter type.

```csharp
[HideInCallstack]
public static void LogError(Type converterType, string problem, string consequence, Object? context = null)
```

#### Parameters

`converterType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The reporting converter's type.

`problem` [string](https://learn.microsoft.com/dotnet/api/system.string)

What is wrong, as a sentence without the trailing period.

`consequence` [string](https://learn.microsoft.com/dotnet/api/system.string)

What the converter does instead, as a full sentence.

`context` Object?

The object to ping, when one is known.

### LogError\(IConverter, Exception, string, Object?\) {#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_}

Reports an exception the converter caught.

```csharp
[HideInCallstack]
public static void LogError(this IConverter converter, Exception exception, string consequence, Object? context = null)
```

#### Parameters

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter.md)

The throwing converter; a scene or asset object is pinged by default.

`exception` [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

The exception caught.

`consequence` [string](https://learn.microsoft.com/dotnet/api/system.string)

What the converter does instead, as a full sentence.

`context` Object?

The object to ping instead of the converter.

### LogError\(Type, Exception, string, Object?\) {#Aspid_MVVM_StarterKit_ConverterLogger_LogError_System_Type_System_Exception_System_String_UnityEngine_Object_}

Reports an exception on behalf of the specified converter type.

```csharp
[HideInCallstack]
public static void LogError(Type converterType, Exception exception, string consequence, Object? context = null)
```

#### Parameters

`converterType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The throwing converter's type.

`exception` [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

The exception caught.

`consequence` [string](https://learn.microsoft.com/dotnet/api/system.string)

What the converter does instead, as a full sentence.

`context` Object?

The object to ping, when one is known.

