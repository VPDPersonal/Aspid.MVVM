# Analyzers

Roslyn analyzers check MVVM code at compile time and offer automatic fixes.

## Contents

- [Overview](#overview)
- [Diagnostics](#diagnostics)
- [Installation](#installation)
- [Configuration](#configuration)

---

## Overview

Aspid.MVVM ships two analyzer sets:

| Package | Purpose |
|-------|-----------|
| `Aspid.MVVM.Analyzers` | Checks ViewModel and View code |
| `Aspid.MVVM.Unity.Generators` | Unity-specific checks |

The analyzers run in the IDE (Rider, Visual Studio, VS Code) and during Unity compilation.

---

## Diagnostics

### ViewModel

| ID | Severity | Description |
|----|----------|----------|
| `AMVVM001` | Warning | A class with `[ViewModel]` must be `partial` |
| `AMVVM002` | Warning | A field with `[Bind]` must be inside a `[ViewModel]` class |
| `AMVVM003` | Warning | A method with `[RelayCommand]` must be inside a `[ViewModel]` class |
| `AMVVM004` | Info | Prefer the generated property over the backing field |
| `AMVVM005` | Warning | The `CanExecute` method/property was not found |
| `AMVVM006` | Warning | `CanExecute` parameters do not match the command |

### View

| ID | Severity | Description |
|----|----------|----------|
| `AMVVM010` | Warning | A class with `[View]` must be `partial` |
| `AMVVM011` | Warning | A class with `[View]` must inherit `MonoView` |
| `AMVVM012` | Info | A binder field has no matching ViewModel property |

### Code fixes

Most diagnostics come with an automatic fix:

| Diagnostic | Code fix |
|-------------|----------|
| `AMVVM001` | Add `partial` to the class |
| `AMVVM004` | Replace `_field` with `Property` |
| `AMVVM010` | Add `partial` to the class |

---

## Installation

The analyzers ship with the Aspid.MVVM package. If you cloned from source:

```bash
git submodule update --init --recursive
```

Make sure the `Aspid.MVVM.Analyzers` submodule is initialized.

---

## Configuration

### Disabling specific diagnostics

In `.editorconfig`:

```ini
[*.cs]
# Turn off the property-over-field suggestion
dotnet_diagnostic.AMVVM004.severity = none

# Lower to a suggestion
dotnet_diagnostic.AMVVM001.severity = suggestion
```

### In code (for single places)

```csharp
#pragma warning disable AMVVM004
var text = _text; // Using the backing field on purpose
#pragma warning restore AMVVM004
```

---

## See also

- [ViewModels](04-viewmodels.md), the attributes the analyzers check
- [Views](05-views.md), View rules
- [Best Practices](14-best-practices.md), code recommendations
