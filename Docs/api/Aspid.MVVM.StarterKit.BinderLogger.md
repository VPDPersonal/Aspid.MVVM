---
title: "Class BinderLogger"
sidebar_label: "BinderLogger"
description: "Class BinderLogger — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BinderLogger {#Aspid_MVVM_StarterKit_BinderLogger}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes binder messages in one shape shared by all binders.

```csharp
public static class BinderLogger
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BinderLogger](Aspid.MVVM.StarterKit.BinderLogger.md)



## Remarks

The [`Type`](https://learn.microsoft.com/dotnet/api/system.type) overloads are for helpers reporting on another binder's behalf.

## Methods

### Log\(IBinder, string, Object?\) {#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_}

Logs an informational message.

```csharp
[HideInCallstack]
public static void Log(this IBinder binder, string message, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The logging binder; pinged when it is a scene or asset object.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The message, as full sentences.

`context` Object?

The object to ping instead of the binder.

### Log\(Type, string, Object?\) {#Aspid_MVVM_StarterKit_BinderLogger_Log_System_Type_System_String_UnityEngine_Object_}

Logs an informational message on behalf of <code class="paramref">binderType</code>.

```csharp
[HideInCallstack]
public static void Log(Type binderType, string message, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The logging binder's type.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The message, as full sentences.

`context` Object?

The object to ping, when one is known.

### LogError\(IBinder, string, string, Object?\) {#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_}

Reports a problem: a value the target will not take, a missing reference, a bad setting.

```csharp
[HideInCallstack]
public static void LogError(this IBinder binder, string problem, string consequence, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The reporting binder; pinged when it is a scene or asset object.

`problem` [string](https://learn.microsoft.com/dotnet/api/system.string)

What is wrong, as a sentence without the trailing period.

`consequence` [string](https://learn.microsoft.com/dotnet/api/system.string)

What the binder does instead, as a full sentence.

`context` Object?

The object to ping instead of the binder.

### LogError\(Type, string, string, Object?\) {#Aspid_MVVM_StarterKit_BinderLogger_LogError_System_Type_System_String_System_String_UnityEngine_Object_}

Reports a problem on behalf of <code class="paramref">binderType</code>.

```csharp
[HideInCallstack]
public static void LogError(Type binderType, string problem, string consequence, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The reporting binder's type.

`problem` [string](https://learn.microsoft.com/dotnet/api/system.string)

What is wrong, as a sentence without the trailing period.

`consequence` [string](https://learn.microsoft.com/dotnet/api/system.string)

What the binder does instead, as a full sentence.

`context` Object?

The object to ping, when one is known.

### LogError\(IBinder, Exception, string, Object?\) {#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_}

Reports an exception the binder caught.

```csharp
[HideInCallstack]
public static void LogError(this IBinder binder, Exception exception, string consequence, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The throwing binder; pinged when it is a scene or asset object.

`exception` [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

The exception caught.

`consequence` [string](https://learn.microsoft.com/dotnet/api/system.string)

What the binder does instead, as a full sentence.

`context` Object?

The object to ping instead of the binder.

### LogError\(Type, Exception, string, Object?\) {#Aspid_MVVM_StarterKit_BinderLogger_LogError_System_Type_System_Exception_System_String_UnityEngine_Object_}

Reports an exception on behalf of <code class="paramref">binderType</code>.

```csharp
[HideInCallstack]
public static void LogError(Type binderType, Exception exception, string consequence, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The throwing binder's type.

`exception` [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

The exception caught.

`consequence` [string](https://learn.microsoft.com/dotnet/api/system.string)

What the binder does instead, as a full sentence.

`context` Object?

The object to ping, when one is known.

### LogWarning\(IBinder, string, string, Object?\) {#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_}

Reports a setup the binder still works with, but not the way it reads.

```csharp
[HideInCallstack]
public static void LogWarning(this IBinder binder, string problem, string consequence, Object? context = null)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The reporting binder; pinged when it is a scene or asset object.

`problem` [string](https://learn.microsoft.com/dotnet/api/system.string)

What is wrong, as a sentence without the trailing period.

`consequence` [string](https://learn.microsoft.com/dotnet/api/system.string)

What the binder does instead, as a full sentence.

`context` Object?

The object to ping instead of the binder.

### LogWarning\(Type, string, string, Object?\) {#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_System_Type_System_String_System_String_UnityEngine_Object_}

Reports a questionable setup on behalf of <code class="paramref">binderType</code>.

```csharp
[HideInCallstack]
public static void LogWarning(Type binderType, string problem, string consequence, Object? context = null)
```

#### Parameters

`binderType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The reporting binder's type.

`problem` [string](https://learn.microsoft.com/dotnet/api/system.string)

What is wrong, as a sentence without the trailing period.

`consequence` [string](https://learn.microsoft.com/dotnet/api/system.string)

What the binder does instead, as a full sentence.

`context` Object?

The object to ping, when one is known.

