#!/usr/bin/env bash
# Builds the three Roslyn DLLs that the package ships from the submodules, in Release, and copies them into the
# package. The Roslyn DLLs job of ci.yml runs it and fails when the result differs from the committed DLLs, so after a
# change to a generator or the analyzer (or a submodule bump) run it and commit the DLLs it rewrites.
# Directory.Build.props makes the build reproducible; global.json pins the SDK that builds it.
#
# Usage: scripts/build-roslyn-dlls.sh
#
# IsRoslynComponent=false, which only adds an IDE capability, switches off the CopyToUnity targets of the submodules,
# so that only this script writes into the package.
set -euo pipefail

ROOT="$(cd "$(dirname "$0")/.." && pwd)"
PACKAGE="$ROOT/Aspid.MVVM/Packages/tech.aspid.mvvm"

# <submodule> <destination folder in the package>
DLLS=(
  "Aspid.MVVM.Generators:."
  "Aspid.MVVM.Analyzers:."
  "Aspid.MVVM.Unity.Generators:Unity"
)

cd "$ROOT"
for entry in "${DLLS[@]}"; do
  name="${entry%%:*}"
  destination="$PACKAGE/${entry#*:}"
  project="$name/$name/$name"
  if [ ! -f "$project/$name.csproj" ]; then
    echo "error: $project/$name.csproj not found; run 'git submodule update --init'" >&2
    exit 1
  fi
  dotnet build "$project" -c Release --nologo -p:IsRoslynComponent=false
  cp "$project/bin/Release/netstandard2.0/$name.dll" "$destination/$name.dll"
done
