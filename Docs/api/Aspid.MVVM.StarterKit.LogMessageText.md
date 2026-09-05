---
title: "Class LogMessageText"
sidebar_label: "LogMessageText"
description: "Class LogMessageText — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LogMessageText {#Aspid_MVVM_StarterKit_LogMessageText}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes types and values the way they read inside a logged message; shared by binders and converters.

```csharp
public static class LogMessageText
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LogMessageText](Aspid.MVVM.StarterKit.LogMessageText.md)



## Methods

### Describe\(object?\) {#Aspid_MVVM_StarterKit_LogMessageText_Describe_System_Object_}

Writes a value unambiguously: <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> as the word "null", a string in
double quotes, a char in single quotes, and everything else as it prints itself.

```csharp
public static string Describe(this object? value)
```

#### Parameters

`value` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The value to describe.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The readable description.

### Expected\(object?, string\) {#Aspid_MVVM_StarterKit_LogMessageText_Expected_System_Object_System_String_}

Writes what was needed and what came instead:
<code>expected a whole number but got "abc"</code>.

```csharp
public static string Expected(this object? value, string expected)
```

#### Parameters

`value` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The value that would not convert.

`expected` [string](https://learn.microsoft.com/dotnet/api/system.string)

What was needed, as a noun phrase: "a whole number".

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The problem, as a sentence without the trailing period.

### GetTypeName\(Type\) {#Aspid_MVVM_StarterKit_LogMessageText_GetTypeName_System_Type_}

Writes a type name the way it reads in code: <code>BoolToValueConverter&lt;float&gt;</code>, not <code>BoolToValueConverter`1</code>.

```csharp
public static string GetTypeName(this Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The type to name.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The readable name.

