#!/usr/bin/env bash
# Creates a throwaway Unity project that compiles the package, its tests and every sample with the packages a user
# installs next to it, for running the EditMode and PlayMode tests on any Unity version (the dev project itself is
# pinned to one).
# CI (ci.yml) and local runs use it.
#
# Usage: scripts/make-unity-test-project.sh <project-dir> [unity-version | project]
#   unity-version  e.g. 6000.0.53f1; "project" (the default) takes the dev project's version.
# Prints the resolved Unity version to stdout.
#
# Local run:
#   v=$(scripts/make-unity-test-project.sh /tmp/aspid-mvvm-ci 6000.0.53f1)
#   "/Applications/Unity/Hub/Editor/$v/Unity.app/Contents/MacOS/Unity" -batchmode -nographics \
#     -projectPath /tmp/aspid-mvvm-ci -runTests -testPlatform EditMode -testResults /tmp/aspid-mvvm-ci/results.xml
set -euo pipefail

if [ $# -lt 1 ] || [ $# -gt 2 ]; then
  echo "usage: $0 <project-dir> [unity-version | project]" >&2
  exit 2
fi

ROOT="$(cd "$(dirname "$0")/.." && pwd)"
DEV="$ROOT/Aspid.MVVM"
PACKAGE="$DEV/Packages/tech.aspid.mvvm"
PROJECT="$1"
VERSION="${2:-project}"

if [ "$VERSION" = "project" ]; then
  VERSION="$(sed -n 's/^m_EditorVersion: //p' "$DEV/ProjectSettings/ProjectVersion.txt")"
fi
if [ -z "$VERSION" ]; then
  echo "error: could not resolve the Unity version" >&2
  exit 1
fi

if [ -e "$PROJECT" ] && [ -n "$(ls -A "$PROJECT")" ]; then
  echo "error: $PROJECT exists and is not empty" >&2
  exit 1
fi

mkdir -p "$PROJECT/Assets" "$PROJECT/Packages" "$PROJECT/ProjectSettings"

# Embedded, as in the dev project: the contract tests read the package sources from Packages/tech.aspid.mvvm of the
# project they run in.
cp -R "$PACKAGE" "$PROJECT/Packages/tech.aspid.mvvm"

# The git packages come from the dev project's manifest, at the commit its packages-lock.json holds: a branch such as
# `#upm` moves, and the tests must not change under a commit. FastTools and Collections are required; VContainer turns
# on the VContainer integration (ASPID_MVVM_VCONTAINER_INTEGRATION). Localization turns on the Localization and
# Addressables integrations and takes the dev project's version. Zenject is not a package (the dev project keeps it in
# Assets/Plugins), so the Zenject integration is not compiled here.
# test-framework 1.4.6 is the newest one on the registry, which early 6000.0 releases resolve from; later Editors
# bundle 1.6.0 (not on the registry) and raise the version to it. ugui 2.0.0 is the uGUI and TextMeshPro of 6000.0;
# later Editors raise it to their own. The modules cover what the binders, the tests and the samples use.
python3 - "$DEV/Packages/manifest.json" "$DEV/Packages/packages-lock.json" \
  >"$PROJECT/Packages/manifest.json" <<'PY'
import json
import sys

manifest_path, lock_path = sys.argv[1:]
with open(manifest_path, encoding="utf-8") as file:
    manifest = json.load(file)["dependencies"]
with open(lock_path, encoding="utf-8") as file:
    lock = json.load(file)["dependencies"]

GIT = ["tech.aspid.fasttools", "tech.aspid.collections", "jp.hadashikick.vcontainer"]
REGISTRY = ["com.unity.localization"]
MODULES = [
    "ai", "animation", "assetbundle", "audio", "imageconversion", "imgui", "jsonserialize", "particlesystem",
    "physics", "physics2d", "ui", "uielements", "unitywebrequest", "unitywebrequestassetbundle",
    "unitywebrequestaudio", "unitywebrequesttexture", "video",
]

dependencies = {}
for name in GIT:
    locked = lock.get(name, {})
    if locked.get("source") != "git" or not locked.get("hash"):
        sys.exit(f"error: {lock_path} has no git commit for {name}")
    dependencies[name] = f"{manifest[name].split('#')[0]}#{locked['hash']}"
for name in REGISTRY:
    dependencies[name] = manifest[name]
dependencies["com.unity.test-framework"] = "1.4.6"
dependencies["com.unity.ugui"] = "2.0.0"
for module in MODULES:
    dependencies[f"com.unity.modules.{module}"] = "1.0.0"

json.dump({"dependencies": dependencies, "testables": ["tech.aspid.mvvm"]}, sys.stdout, indent=2)
print()
PY

printf 'm_EditorVersion: %s\n' "$VERSION" > "$PROJECT/ProjectSettings/ProjectVersion.txt"

# Samples are imported the way Package Manager imports them: copied into Assets/ with their .meta files.
mkdir -p "$PROJECT/Assets/Samples"
cp -R "$PACKAGE/Samples~/." "$PROJECT/Assets/Samples/"

# The TMP Essential Resources of the dev project: the default font asset and TMP Settings that a TextMeshPro component
# looks for, as in a user project after the TMP importer ran.
TMP="$DEV/Assets/_Assets/TextMesh Pro"
mkdir -p "$PROJECT/Assets/TextMesh Pro"
cp "$TMP.meta" "$PROJECT/Assets/TextMesh Pro.meta"
for folder in Fonts Resources Shaders Sprites; do
  cp -R "$TMP/$folder" "$TMP/$folder.meta" "$PROJECT/Assets/TextMesh Pro/"
done

echo "$VERSION"
