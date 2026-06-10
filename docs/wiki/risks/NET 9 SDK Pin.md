---
title: NET 9 SDK Pin
type: risk
status: active
source_paths:
  - CLAUDE.md
  - Aspid.MVVM.Generators/global.json
tags:
  - risk
  - build
  - source-generator
  - toolchain
updated_at: 2026-05-31
---

# NET 9 SDK Pin

> The Roslyn generator/analyzer projects require the .NET 9 SDK, pinned by a `global.json`. Build them with an older SDK and the build fails before any code compiles.

## Symptom

`dotnet build`/`dotnet test` on the generator or analyzer solutions fails immediately with an SDK-resolution error (e.g. "A compatible .NET SDK was not found" / requested version `9.0.0`), even though the C# itself is fine. The [[Committed DLLs]] then never get refreshed, so Unity keeps consuming a stale [[Source Generator]] / [[Analyzer]].

## Cause

`Aspid.MVVM.Generators/global.json` pins the toolchain:

```json
{ "sdk": { "version": "9.0.0", "rollForward": "latestMajor", "allowPrerelease": true } }
```

`rollForward: latestMajor` means any SDK **9.0.0 or newer** is accepted; anything older is rejected outright. These are standalone .NET projects (not Unity-compiled C#), so they need a real SDK on `PATH`. `allowPrerelease` lets preview SDKs satisfy the pin.

Note: only the Generators submodule actually carries a `global.json` at its root in this checkout. CLAUDE.md says "each generator/analyzer root," but the Analyzers and [[Unity Generators]] submodule roots have none — they likely fall back to the global SDK / shared toolchain. The pin you must satisfy is the Generators one.

## Fix

1. Install the .NET 9 SDK (or newer): `dotnet --list-sdks` should show a `9.x` entry.
2. Re-run the build, e.g. `dotnet build Aspid.MVVM.Generators/Aspid.MVVM.Generators.sln`.
3. Commit the rebuilt DLL — see [[Committed DLLs]] for which artifacts to refresh.

If submodules are empty (build can't even find the project), that's a different problem — see [[Submodule Init]].

## Prevention

- Keep a 9.x SDK installed on every machine and CI runner that touches the generators.
- Treat the SDK requirement as part of build setup alongside [[Submodule Init]] and the DLL-commit step from [[Committed DLLs]].
- Do not edit `global.json` to relax the floor casually; the [[Source Generation Pipeline]] is built against a known SDK and lowering it can silently change generated output.

Unrelated trap: this is a build-time concern only. It does not affect Unity runtime code, which compiles against .NET Standard 2.0 and never sees this SDK.

## Source

- `Aspid.MVVM.Generators/global.json` — the actual pin.
- `CLAUDE.md` (Key Technologies, Gotchas) — states `.NET 9.0 SDK` is required for generator/analyzer projects.
