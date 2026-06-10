---
title: Analyzer
type: generation
status: active
source_paths:
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/TypeAnalyzers/PartialAnalyzer.cs
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/TypeAnalyzers/PartialCodeFixProvider.cs
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/FieldAnalyzers/BindAttributeAnalyzer.cs
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/FieldAnalyzers/BindAttributeCodeFixProvider.cs
tags: [analyzer, roslyn, diagnostics, codefix, generation]
updated_at: 2026-05-31
---

# Analyzer

> Roslyn diagnostics + one-click code fixes that catch the two most common Aspid mistakes at edit time: forgetting `partial`, and touching a `[Bind]` field directly instead of its generated property.

## Why it exists

Aspid is source-generated, so the members you *should* use are invisible in hand-written source. Two errors follow naturally: declaring a `[ViewModel]`/`[View]` type without `partial` (the generator can't add its half), and reading or writing a `[Bind]` field directly instead of the generated property (which bypasses change notification). The analyzer surfaces both as IDE squiggles with fixes, long before a build fails. Ships as a committed DLL — see [[Committed DLLs]]; Unity consumes the DLL, not this submodule source. See also [[Must Be Partial]].

## How it works

Two `DiagnosticAnalyzer` + `CodeFixProvider` pairs. Both analyzers skip generated code and run concurrently.

**PartialAnalyzer** registers on class/struct declarations. For a type carrying `Aspid.MVVM.ViewModelAttribute` or `Aspid.MVVM.ViewAttribute` but missing the `partial` keyword, it reports one of four error IDs (class vs struct × ViewModel vs View):

| ID | Trigger | Severity |
|----|---------|----------|
| AM0003 | class + `[View]` not partial | Error |
| AM0004 | struct + `[View]` not partial | Error |
| AM0005 | class + `[ViewModel]` not partial | Error |
| AM0006 | struct + `[ViewModel]` not partial | Error |

`PartialCodeFixProvider` offers **"Make partial"** — it just `AddModifiers(partial)` to the declaration.

**BindAttributeAnalyzer** registers on every `IdentifierName`, resolves it to an `IFieldSymbol`, and checks whether the field carries `[Bind]` or a mode-specific variant (`OneWayBind`, `TwoWayBind`, `OneTimeBind`, `OneWayToSourceBind`). If so it reports:

| ID | Context | Severity |
|----|---------|----------|
| AM0001 | field on the **left** of an assignment (write) | Error |
| AM0002 | any other read | Warning |

`BindAttributeCodeFixProvider` offers **"Use property '<Name>'"**, replacing the identifier with the PascalCase property name (`_count` → `Count`, stripping `_` / `m_` prefixes). Writes inside a constructor are intentionally exempt (`IsWithinConstructor`), since the generated property may not yet be wired up there.

> Note (inferred): the fix reads a `PropertyName` diagnostic property if present, otherwise re-derives PascalCase. The analyzer source above does not appear to set that property, so the fallback path is what runs today.

## Key relationships

- Enforces the [[Must Be Partial]] contract that the [[Source Generator]] depends on.
- AM0001/AM0002 protect the generated [[Bindable Members]] property over its backing `[Bind]` field; see [[BindMode]] for the mode variants.
- Complements, but is separate from, the [[Source Generation Pipeline]] and [[Unity Generators]].

## Source

`Aspid.MVVM.Analyzers/.../TypeAnalyzers/` (Partial pair) and `.../FieldAnalyzers/` (Bind pair). Built independently of Unity into the committed `Aspid.MVVM.Analyzers.dll`. See [[Submodule Init]] and [[NET 9 SDK Pin]].
