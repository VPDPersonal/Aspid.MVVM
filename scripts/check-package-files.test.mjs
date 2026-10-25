// Tests of scripts/check-package-files.mjs. The script finds the repository from its own location, so each test copies
// it into a small package of fixture files, breaks that package in one way and reads the verdict. ci.yml runs it:
//   node --test scripts/*.test.mjs
import assert from 'node:assert/strict';
import { test } from 'node:test';
import { tmpdir } from 'node:os';
import { dirname, join } from 'node:path';
import { spawnSync } from 'node:child_process';
import { fileURLToPath } from 'node:url';
import { cpSync, mkdirSync, mkdtempSync, rmSync, writeFileSync } from 'node:fs';

const script = fileURLToPath(new URL('check-package-files.mjs', import.meta.url));
const inputs = fileURLToPath(new URL('unity-inputs.mjs', import.meta.url));
const PACKAGE = 'Aspid.MVVM/Packages/tech.aspid.mvvm';

const guid = (number) => number.toString(16).padStart(32, '0');
const meta = (number, importer = '') => `fileFormatVersion: 2\nguid: ${guid(number)}\n${importer}`;
const MONO = 'MonoImporter:\n  externalObjects: {}\n  serializedVersion: 2\n';
const FOLDER = 'folderAsset: yes\nDefaultImporter:\n  externalObjects: {}\n';
const asmdef = (name, references = []) => JSON.stringify({ name, references });

const LICENSE = 'MIT License\n\nCopyright (c) 2025 Vladislav Panin\n';

// A package that passes: a runtime and an editor assembly, a docs page, a sample, a hidden file and the license copy.
const valid = () => ({
  LICENSE,
  [`${PACKAGE}/LICENSE.md`]: LICENSE,
  [`${PACKAGE}/LICENSE.md.meta`]: meta(11),
  [`${PACKAGE}/package.json`]: JSON.stringify({ name: 'tech.aspid.mvvm', samples: [{ displayName: 'Demo', path: 'Samples~/Demo' }] }),
  [`${PACKAGE}/package.json.meta`]: meta(1),
  [`${PACKAGE}/.DS_Store`]: '',
  [`${PACKAGE}/Source.meta`]: meta(2, FOLDER),
  [`${PACKAGE}/Source/Aspid.MVVM.asmdef`]: asmdef('Aspid.MVVM', ['Aspid.FastTools']),
  [`${PACKAGE}/Source/Aspid.MVVM.asmdef.meta`]: meta(3),
  [`${PACKAGE}/Source/Item.cs`]: 'class Item { }',
  [`${PACKAGE}/Source/Item.cs.meta`]: meta(4, MONO),
  [`${PACKAGE}/Unity.meta`]: meta(5, FOLDER),
  [`${PACKAGE}/Unity/Aspid.MVVM.Unity.asmdef`]: asmdef('Aspid.MVVM.Unity', ['Aspid.MVVM', `GUID:${guid(3)}`, 'VContainer']),
  [`${PACKAGE}/Unity/Aspid.MVVM.Unity.asmdef.meta`]: meta(6),
  [`${PACKAGE}/Documentation.meta`]: meta(12, FOLDER),
  [`${PACKAGE}/Documentation/01-intro.md`]: '# Intro',
  [`${PACKAGE}/Documentation/01-intro.md.meta`]: meta(13),
  [`${PACKAGE}/Samples~/Demo.meta`]: meta(7, FOLDER),
  [`${PACKAGE}/Samples~/Demo/README.md`]: '# Demo',
  [`${PACKAGE}/Samples~/Demo/README.md.meta`]: meta(8),
});

// Run the script on the fixture files; a value of null removes a file of the valid package.
function check(t, changes = {}) {
  const dir = mkdtempSync(join(tmpdir(), 'aspid-package-files-'));
  t.after(() => rmSync(dir, { recursive: true, force: true }));
  mkdirSync(join(dir, 'scripts'));
  cpSync(script, join(dir, 'scripts/check-package-files.mjs'));
  cpSync(inputs, join(dir, 'scripts/unity-inputs.mjs'));
  for (const [file, text] of Object.entries({ ...valid(), ...changes })) {
    if (text === null) continue;
    mkdirSync(dirname(join(dir, file)), { recursive: true });
    writeFileSync(join(dir, file), text);
  }
  return spawnSync('node', ['scripts/check-package-files.mjs'], { cwd: dir, encoding: 'utf8' });
}

function assertFails(result, file, message) {
  assert.equal(result.status, 1, result.stdout + result.stderr);
  assert.ok(result.stdout.includes(`::error file=${file}::`), result.stdout);
  assert.match(result.stdout, message);
}

test('a package with a .meta for every file, unique guids and resolving references passes', (t) => {
  const result = check(t);
  assert.equal(result.status, 0, result.stdout + result.stderr);
  assert.match(result.stdout, /Package files OK/);
});

test('a file without a .meta is reported, also in Samples~ and in a folder', (t) => {
  const metas = [`${PACKAGE}/Source/Item.cs.meta`, `${PACKAGE}/Samples~/Demo/README.md.meta`, `${PACKAGE}/Source.meta`];
  for (const file of metas) assertFails(check(t, { [file]: null }), file.slice(0, -'.meta'.length), /no \.meta;/);
});

test('a .meta without its asset is reported', (t) => {
  assertFails(check(t, { [`${PACKAGE}/Source/Gone.cs.meta`]: meta(10, MONO) }), `${PACKAGE}/Source/Gone.cs.meta`, /no asset for this \.meta/);
});

test('a .meta without a valid guid is reported', (t) => {
  for (const text of ['fileFormatVersion: 2\n', `fileFormatVersion: 2\nguid: ${'A'.repeat(32)}\n`, 'fileFormatVersion: 2\nguid: 1234\n']) {
    assertFails(check(t, { [`${PACKAGE}/Source/Item.cs.meta`]: text + MONO }), `${PACKAGE}/Source/Item.cs.meta`, /no "guid:" line/);
  }
});

// The folders are read in name order, so the later file of a pair (Unity/ after Source/, Source/ after Samples~/) is the
// one reported.
test('a guid used twice is reported, also between the package and a sample', (t) => {
  assertFails(check(t, { [`${PACKAGE}/Unity/Aspid.MVVM.Unity.asmdef.meta`]: meta(4) }), `${PACKAGE}/Unity/Aspid.MVVM.Unity.asmdef.meta`,
    /guid 0+4 is also the guid of .*Source\/Item\.cs\.meta/);
  const sample = `${PACKAGE}/Samples~/Demo/Sample.cs`;
  assertFails(check(t, { [sample]: 'class Sample { }', [`${sample}.meta`]: meta(4) }), `${PACKAGE}/Source/Item.cs.meta`,
    /guid 0+4 is also the guid of .*Samples~\/Demo\/Sample\.cs\.meta/);
});

test('a sample path that is no folder of the package is reported', (t) => {
  for (const path of ['Samples~/Missing', 'package.json']) {
    const manifest = JSON.stringify({ name: 'tech.aspid.mvvm', samples: [{ displayName: 'Demo', path }] });
    assertFails(check(t, { [`${PACKAGE}/package.json`]: manifest }), `${PACKAGE}/package.json`, new RegExp(`the path "${path}" of the sample "Demo" is not a folder`));
  }
});

test('a sample without a path is reported and does not crash the script', (t) => {
  const manifest = JSON.stringify({ name: 'tech.aspid.mvvm', samples: [{ displayName: 'Demo' }] });
  assertFails(check(t, { [`${PACKAGE}/package.json`]: manifest }), `${PACKAGE}/package.json`, /the sample "Demo" has no "path"/);
});

test('a folder that Unity ignores needs no .meta for its content, except Samples~', (t) => {
  const doc = `${PACKAGE}/Documentation~/Guide.md`;
  assert.equal(check(t, { [doc]: '# Guide' }).status, 0);
  // Unity writes no .meta into Samples~ of the dev project, so the hint names the import instead.
  const readme = `${PACKAGE}/Samples~/Demo/README.ru.md`;
  assertFails(check(t, { [readme]: '# Демо' }), readme, /no \.meta; import the sample into a project/);
});

test('an asmdef reference that is no assembly of the package is reported', (t) => {
  const unity = `${PACKAGE}/Unity/Aspid.MVVM.Unity.asmdef`;
  assertFails(check(t, { [unity]: asmdef('Aspid.MVVM.Unity', ['Aspid.MVVM.Typo']) }), unity, /the reference "Aspid\.MVVM\.Typo" is no asmdef/);
  assertFails(check(t, { [unity]: asmdef('Aspid.MVVM.Unity', [`GUID:${guid(99)}`]) }), unity, /the reference "GUID:0+63" is no asmdef/);
  // A GUID reference to the .meta of a file that is no asmdef is no assembly either.
  assertFails(check(t, { [unity]: asmdef('Aspid.MVVM.Unity', [`GUID:${guid(4)}`]) }), unity, /the reference "GUID:0+4" is no asmdef/);
});

test('an asmdef that is not JSON is reported', (t) => {
  const source = `${PACKAGE}/Source/Aspid.MVVM.asmdef`;
  assertFails(check(t, { [source]: '{ "name": ' }), source, /not valid JSON/);
});

test('a path of 140 characters from the install folder is reported, but not in Samples~', (t) => {
  const name = `${'a'.repeat(125)}.txt`;
  const tooLong = `${PACKAGE}/Source/${name}`;
  assertFails(check(t, { [tooLong]: '', [`${tooLong}.meta`]: meta(10) }), tooLong, /path is \d+ characters/);
  const sample = `${PACKAGE}/Samples~/Demo/${name}`;
  assert.equal(check(t, { [sample]: '', [`${sample}.meta`]: meta(10) }).status, 0);
});

test('a .cs.meta without a MonoImporter block is reported, but not in Samples~', (t) => {
  assertFails(check(t, { [`${PACKAGE}/Source/Item.cs.meta`]: meta(4) }), `${PACKAGE}/Source/Item.cs.meta`, /no MonoImporter block/);
  const sample = `${PACKAGE}/Samples~/Demo/Sample.cs`;
  assert.equal(check(t, { [sample]: 'class Sample { }', [`${sample}.meta`]: meta(10) }).status, 0);
});

test('a .uss.meta without a ScriptedImporter block or with the ThemeStyleSheetImporter is reported', (t) => {
  const uss = `${PACKAGE}/Unity/Style.uss`;
  const importer = (id) => `ScriptedImporter:\n  script: {fileID: ${id}, guid: 0000000000000000e000000000000000, type: 0}\n`;
  assert.equal(check(t, { [uss]: '', [`${uss}.meta`]: meta(10, importer(12385)) }).status, 0);
  assertFails(check(t, { [uss]: '', [`${uss}.meta`]: meta(10, importer(12388)) }), `${uss}.meta`, /importer fileID 12388/);
  assertFails(check(t, { [uss]: '', [`${uss}.meta`]: meta(10, 'timeCreated: 1772978190\n') }), `${uss}.meta`, /no ScriptedImporter block/);
});

test('a package LICENSE.md that is missing or differs from the root LICENSE is reported', (t) => {
  const copy = `${PACKAGE}/LICENSE.md`;
  assertFails(check(t, { [copy]: LICENSE.replace('2025', '2026') }), copy, /differs from LICENSE/);
  // A different line ending is a different file too.
  assertFails(check(t, { [copy]: LICENSE.replaceAll('\n', '\r\n') }), copy, /differs from LICENSE/);
  assertFails(check(t, { [copy]: null, [`${copy}.meta`]: null }), copy, /no LICENSE\.md in the package/);
  assertFails(check(t, { LICENSE: null }), 'LICENSE', /no LICENSE at the repository root/);
});

test('a package file that refers to a file the Unity CI ignores is reported', (t) => {
  const scene = `${PACKAGE}/Samples~/Demo/Demo.unity`;
  const sceneFiles = (text) => ({ [scene]: text, [`${scene}.meta`]: meta(14) });
  assertFails(check(t, sceneFiles(`m_Text: {guid: ${guid(8)}, type: 3}`)), scene, /refers to .*README\.md by its guid/);
  assertFails(check(t, { [`${PACKAGE}/Source/Item.cs`]: 'class Item { string A = "Documentation/01-intro.md"; }' }),
    `${PACKAGE}/Source/Item.cs`, /names "01-intro\.md" in quotes/);
  // A mention in prose and a guid of an input are no references.
  const result = check(t, { ...sceneFiles(`m_Script: {guid: ${guid(4)}}`), [`${PACKAGE}/Source/Item.cs`]: '// See README.md\nclass Item { }' });
  assert.equal(result.status, 0, result.stdout + result.stderr);
});
