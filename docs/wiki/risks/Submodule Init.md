---
title: Submodule Init
type: risk
status: active
source_paths:
  - .gitmodules
  - CLAUDE.md
tags:
  - risk
  - git
  - setup
  - submodules
  - build
updated_at: 2026-05-31
---

# Submodule Init

> Cloning without submodules leaves the generator/analyzer repos empty, so Unity has nothing to consume and the project fails to compile. Always recurse submodules.

## Symptom

After a fresh `git clone`, Unity reports compile errors: the `[ViewModel]`, `[Bind]`, and `[RelayCommand]` attributes resolve but no generated members appear, and types from the generator/analyzer projects are missing. The directories `Aspid.MVVM.Generators/`, `Aspid.MVVM.Analyzers/`, and `Aspid.MVVM.Unity.Generators/` exist but are empty.

## Cause

The project pulls in three git submodules (see `.gitmodules` facts below), declared in `.gitmodules`:

- `Aspid.MVVM.Generators`
- `Aspid.MVVM.Analyzers`
- `Aspid.MVVM.Unity.Generators`

A plain `git clone` records the submodule pointers but does **not** fetch their contents. Until they are checked out, the source of the [[Source Generator]], [[Analyzer]], and [[Unity Generators]] is absent.

Note: this is the *source* side. Unity itself consumes the [[Committed DLLs|committed DLLs]] checked into `Aspid.MVVM/Assets/Aspid/MVVM/`, not the submodule source — so an empty submodule does not always break the editor immediately. It does break rebuilding the generators (see [[Source Generation Pipeline]]) and any work on generator/analyzer code. The "Unity won't compile" framing in CLAUDE.md is the worst-case outcome; the reliable failure is that you cannot regenerate the DLLs.

[[External Dependencies|Aspid.Collections]] is **not** a submodule — it is consumed as a UPM git package (`tech.aspid.collections`), so it is unaffected by this issue.

## Fix

If you have not cloned yet:

```bash
git clone --recurse-submodules <repo-url>
```

If you already cloned without submodules:

```bash
git submodule update --init --recursive
```

Then rebuild the generator solution and re-commit the refreshed DLL if you changed generator code (see [[Source Generator]] and [[Committed DLLs]]).

## Prevention

- Make `--recurse-submodules` your default clone habit; you can also set `git config --global submodule.recurse true` so `pull`/`checkout` follow submodules automatically.
- After switching branches that move submodule pointers, re-run `git submodule update --init --recursive`.
- See also [[NET 9 SDK Pin]] and [[Getting Started]] for the rest of the first-run setup, and [[Architecture]] for how the submodules fit the build.
