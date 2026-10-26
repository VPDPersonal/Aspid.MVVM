// Check the package files that Unity or the Asset Store Validator complain about later:
//  1. Path length: the Validator warns when a path is 140 characters or longer, counted from "Aspid/MVVM/" (the install
//     folder under Assets/) without the .meta files.
//  2. Every .cs.meta of the package has a MonoImporter block, so all of them stay in the format Unity writes for a new
//     file. Samples~ is skipped: it is not imported in the dev project.
//  3. A .uss.meta (samples included) has a ScriptedImporter block, and its importer is the StyleSheetImporter (12385), not
//     the ThemeStyleSheetImporter (12388). Unity 6000.6 warns on every .uss.meta without the block (an IDE writes only the
//     guid), and it rewrites 12388 in the working tree when it imports the sample.
//  4. Every file and folder of the package has a .meta, and every .meta has its asset. Unity cannot write a .meta into an
//     installed package: it ignores the file there and warns about a .meta without an asset. Samples~ is copied into the
//     project on import, and its .meta files keep the GUIDs that its scenes refer to. Another folder that ends with "~"
//     is ignored with its content.
//  5. Every .meta has a guid, and no guid is used twice in the package.
//  6. Every "path" in the "samples" of package.json is a folder of the package.
//  7. Every reference of an asmdef of the package is an asmdef of the package, or an assembly of EXTERNAL_ASSEMBLIES.
//  8. LICENSE.md of the package is a byte-for-byte copy of the root LICENSE, which GitHub shows and "licensesUrl" links.
//  9. No package file refers to a file that scripts/unity-inputs.mjs ignores, by its guid or by its name in quotes.
//     ci.yml skips the Unity tests for a change to an ignored file, so a file that an asset or a test reads must not be
//     one.
// ci.yml runs it. It has no dependencies:
//   node scripts/check-package-files.mjs
import { existsSync, readdirSync, readFileSync, statSync } from 'node:fs';
import { basename, join, relative } from 'node:path';
import { fileURLToPath } from 'node:url';
import { PACKAGE, isIgnored } from './unity-inputs.mjs';

process.chdir(fileURLToPath(new URL('..', import.meta.url)));

const INSTALL_PREFIX = 'Aspid/MVVM/';
const PATH_LIMIT = 140;
const LICENSE = 'LICENSE';
const PACKAGE_LICENSE = `${PACKAGE}/LICENSE.md`;
// Assemblies that the asmdefs of the package use and the package does not contain: its git dependencies, the optional
// integrations (a missing one is skipped by Unity) and Unity packages. A reference by GUID cannot name one.
const EXTERNAL_ASSEMBLIES = new Set([
  'Aspid.Collections.Observable',
  'Aspid.FastTools',
  'Aspid.FastTools.Editor',
  'Aspid.Internal.Unity.Editor',
  'Unity.Addressables',
  'Unity.Collections',
  'Unity.Localization',
  'Unity.Mathematics',
  'Unity.ResourceManager',
  'Unity.TextMeshPro',
  'UnityEditor.TestRunner',
  'UnityEngine.TestRunner',
  'VContainer',
  'Zenject',
]);

let errors = 0;
const fail = (file, message) => {
  console.log(`::error file=${file}::${message}`);
  errors++;
};

const files = (dir) =>
  readdirSync(dir, { withFileTypes: true }).flatMap((entry) => {
    const path = join(dir, entry.name);
    return entry.isDirectory() ? files(path) : [path];
  });

// Unity imports no entry with these names, so it needs no .meta. Samples~ is one of them, but its content needs one:
// Unity copies Samples~ into the project on import, and the scenes there refer to the GUIDs of its .meta files.
const unityIgnores = (name) => name.endsWith('~') || name.endsWith('.tmp') || name.toLowerCase() === 'cvs';
const isFolder = (path) => statSync(path, { throwIfNoEntry: false })?.isDirectory() ?? false;

// Files and folders. A hidden entry (a name that starts with a dot) is skipped with its content, and so is a folder that
// Unity ignores, except Samples~. Every folder it lists goes into `folders`.
const folders = new Set();
const entries = (dir) =>
  readdirSync(dir, { withFileTypes: true }).flatMap((entry) => {
    if (entry.name.startsWith('.')) return [];
    const path = join(dir, entry.name);
    if (!entry.isDirectory()) return [path];
    folders.add(path);
    return unityIgnores(entry.name) && entry.name !== 'Samples~' ? [path] : [path, ...entries(path)];
  });

// Each tree is read once.
const packageFiles = files(PACKAGE);
const visible = entries(PACKAGE);
const visibleFiles = visible.filter((path) => !folders.has(path));

const samples = `${PACKAGE}/Samples~/`;
// Unity does not import Samples~ in the dev project, so it writes no .meta there.
const NO_META = 'no .meta; open the project in Unity once and commit the .meta it writes';
const NO_SAMPLE_META = 'no .meta; import the sample into a project and copy the .meta it writes back, or write it by hand';

// Samples~ is installed under Assets/Samples/, so the install-folder limit does not apply to it.
for (const file of packageFiles) {
  if (file.endsWith('.meta') || file.startsWith(samples)) continue;
  const length = (INSTALL_PREFIX + relative(PACKAGE, file)).length;
  if (length >= PATH_LIMIT) fail(file, `path is ${length} characters from "${INSTALL_PREFIX}"; the limit is ${PATH_LIMIT - 1}`);
}

for (const file of packageFiles) {
  if (file.endsWith('.cs.meta') && !file.startsWith(samples) && !readFileSync(file, 'utf8').includes('\nMonoImporter:')) {
    fail(file, 'no MonoImporter block; open the project in Unity once and commit the .meta it writes');
  }
  if (file.endsWith('.uss.meta')) {
    const text = readFileSync(file, 'utf8');
    if (!text.includes('\nScriptedImporter:')) {
      fail(file, 'no ScriptedImporter block; open the project in Unity once and commit the .meta it writes');
    } else if (/^\s+script: \{fileID: 12388,/m.test(text)) {
      fail(file, 'importer fileID 12388 (ThemeStyleSheetImporter) on a .uss; use 12385 (StyleSheetImporter)');
    }
  }
}

const guidOf = new Map(); // .meta path -> guid
const ownerOf = new Map(); // guid -> .meta path
const present = new Set(visible);
for (const path of visible) {
  if (!path.endsWith('.meta')) {
    if (!unityIgnores(basename(path)) && !present.has(`${path}.meta`)) {
      fail(path, path.startsWith(samples) ? NO_SAMPLE_META : NO_META);
    }
    continue;
  }
  if (!present.has(path.slice(0, -'.meta'.length))) {
    fail(path, 'no asset for this .meta; delete the .meta or restore the asset');
  }
  const guid = /^guid: ([0-9a-f]{32})\s*$/m.exec(readFileSync(path, 'utf8'))?.[1];
  if (!guid) {
    fail(path, 'no "guid:" line with 32 hexadecimal digits');
    continue;
  }
  if (ownerOf.has(guid)) fail(path, `guid ${guid} is also the guid of ${ownerOf.get(guid)}`);
  guidOf.set(path, guid);
  ownerOf.set(guid, path);
}

const manifestPath = `${PACKAGE}/package.json`;
for (const sample of JSON.parse(readFileSync(manifestPath, 'utf8')).samples ?? []) {
  if (typeof sample.path !== 'string') {
    fail(manifestPath, `the sample "${sample.displayName}" has no "path"`);
  } else if (!isFolder(join(PACKAGE, sample.path))) {
    fail(manifestPath, `the path "${sample.path}" of the sample "${sample.displayName}" is not a folder of the package`);
  }
}

const asmdefs = [];
for (const path of visible) {
  if (!path.endsWith('.asmdef')) continue;
  try {
    asmdefs.push({ path, json: JSON.parse(readFileSync(path, 'utf8')) });
  } catch (error) {
    fail(path, `not valid JSON: ${error.message}`);
  }
}
const assemblyNames = new Set(asmdefs.map(({ json }) => json.name));
const assemblyGuids = new Set(asmdefs.map(({ path }) => guidOf.get(`${path}.meta`)));
const resolves = (reference) =>
  reference.startsWith('GUID:')
    ? assemblyGuids.has(reference.slice('GUID:'.length))
    : assemblyNames.has(reference) || EXTERNAL_ASSEMBLIES.has(reference);

for (const { path, json } of asmdefs) {
  for (const reference of json.references ?? []) {
    if (!resolves(reference)) {
      fail(path, `the reference "${reference}" is no asmdef of the package; fix it, or add the assembly to EXTERNAL_ASSEMBLIES`);
    }
  }
}

// The Unity tests read the package as the test project imports it, Samples~ included.
const ignored = visibleFiles.filter((path) => isIgnored(path) && !path.endsWith('.meta'));
const ignoredGuids = new Map(ignored.map((path) => [guidOf.get(`${path}.meta`), path]).filter(([guid]) => guid));
const escapeRegExp = (text) => text.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
const ignoredNames = [...new Set(ignored.map((path) => basename(path)))].map((name) => ({
  name,
  quoted: new RegExp(`["'/]${escapeRegExp(name)}["']`),
}));
const SKIPPED = 'but ci.yml skips the Unity tests for a change to it; drop it, or make the file an input in scripts/unity-inputs.mjs';
for (const path of visibleFiles) {
  if (isIgnored(path)) continue;
  const text = readFileSync(path, 'latin1');
  for (const [guid, target] of ignoredGuids) {
    if (text.includes(guid)) fail(path, `refers to ${target} by its guid, ${SKIPPED}`);
  }
  for (const { name, quoted } of ignoredNames) {
    if (quoted.test(text)) fail(path, `names "${name}" in quotes, ${SKIPPED}`);
  }
}

if (!existsSync(LICENSE)) {
  fail(LICENSE, 'no LICENSE at the repository root');
} else if (!existsSync(PACKAGE_LICENSE)) {
  fail(PACKAGE_LICENSE, `no LICENSE.md in the package; copy ${LICENSE} there`);
} else if (!readFileSync(LICENSE).equals(readFileSync(PACKAGE_LICENSE))) {
  fail(PACKAGE_LICENSE, `differs from ${LICENSE}; change both files the same way`);
}

if (errors) process.exit(1);
console.log('Package files OK');
