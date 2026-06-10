---
title: Committed DLLs
type: risk
status: active
source_paths:
  - CLAUDE.md
  - Aspid.MVVM.Generators/Directory.Build.targets
  - Aspid.MVVM/Assets/Aspid/MVVM/Aspid.MVVM.Generators.dll
  - Aspid.MVVM/Assets/Aspid/MVVM/Aspid.MVVM.Generators.dll.meta
tags: [risk, generators, build, unity, submodule]
updated_at: 2026-05-31
---

# Committed DLLs

> The Roslyn generator/analyzer DLLs that Unity actually runs are checked into `Assets/`, not built by Unity. Edit a generator's source and forget to rebuild + commit the DLL, and your change silently never reaches Unity.

## Symptom

You change generator or analyzer code (e.g. how [[ViewModel to Generated Code]] members are emitted), reopen Unity, and nothing changes — the generated `partial`, the diagnostic, or the fix behaves exactly as before. No build error points at the cause. CI or a teammate may also see stale generated members because the committed DLL predates your source edit.

## Cause

Unity does not compile the generator/analyzer projects. It loads three prebuilt DLLs committed under the Unity tree:

- `Aspid.MVVM/Assets/Aspid/MVVM/Aspid.MVVM.Generators.dll`
- `Aspid.MVVM/Assets/Aspid/MVVM/Aspid.MVVM.Analyzers.dll`
- `Aspid.MVVM/Assets/Aspid/MVVM/Unity/Aspid.MVVM.Unity.Generators.dll`

Each `.dll.meta` carries the `RoslynAnalyzer` label, which is how Unity treats the assembly as a compile-time source generator/analyzer rather than a runtime plugin. The actual source lives in git submodules ([[Source Generator]], [[Analyzer]], [[Unity Generators]]) that Unity never reads — see [[Submodule Init]]. So the DLL and its source can drift apart: the DLL is the source of truth at runtime.

## Fix

Rebuild the changed solution and commit the refreshed DLL:

```bash
dotnet build Aspid.MVVM.Generators/Aspid.MVVM.Generators.sln
```

A build helper does the copy for you. `Aspid.MVVM.Generators/Directory.Build.targets` defines a `CopyToUnity` target that runs `AfterTargets="Build"` only when `IsRoslynComponent == true`, copying `$(TargetPath)` into `Assets/Aspid/MVVM/` (with `SkipUnchangedFiles`). So a successful build refreshes the in-Assets DLL automatically — but git won't stage it for you. You must `git add` and commit the updated `.dll`. Then let Unity reimport. (The analyzer and Unity-generator submodules appear to use the same `Directory.Build.targets` convention.)

## Prevention

- After any generator/analyzer source change, treat "build the `.sln` and commit the DLL" as one inseparable step.
- Verify the working tree shows the `.dll` as modified before committing; an unchanged DLL means your build did not actually update it (wrong project, build error, or `IsRoslynComponent` not set).
- Requires the [[NET 9 SDK Pin]] to build at all, and initialized [[Submodule Init|submodules]] to have source to build.
- Review the binary diff size, not the content: a missing or stale DLL is the failure mode, so confirm the timestamp/size moved.

See [[Source Generation Pipeline]] for what these DLLs produce, and [[Architecture]] for the overall build flow.

## Source

- `Aspid.MVVM.Generators/Directory.Build.targets` — `CopyToUnity` auto-copy target
- `Aspid.MVVM/Assets/Aspid/MVVM/*.dll` + `*.dll.meta` — committed artifacts, `RoslynAnalyzer` label
- `CLAUDE.md` — "Generator artifacts are committed" gotcha
