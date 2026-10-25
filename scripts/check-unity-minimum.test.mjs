// Tests of scripts/check-unity-minimum.mjs. The script finds the repository from its own location, so each test copies
// it next to fixture files, changes one of them and reads the verdict. ci.yml runs it:
//   node --test scripts/*.test.mjs
import assert from 'node:assert/strict';
import { test } from 'node:test';
import { tmpdir } from 'node:os';
import { dirname, join } from 'node:path';
import { spawnSync } from 'node:child_process';
import { fileURLToPath } from 'node:url';
import { cpSync, mkdirSync, mkdtempSync, rmSync, writeFileSync } from 'node:fs';

const script = fileURLToPath(new URL('check-unity-minimum.mjs', import.meta.url));
const PACKAGE_JSON = 'Aspid.MVVM/Packages/tech.aspid.mvvm/package.json';
const MATRIX = '.github/workflows/ci.yml';
const PROSE = ['MIGRATION.md', 'MIGRATION.ru.md', '.claude/skills/aspid-code-style/SKILL.md'];

const matrix = (versions) => `jobs:\n  unity-plan:\n    steps:\n      - env:\n          UNITY_VERSIONS: ${versions}\n`;
const valid = () => ({
  [PACKAGE_JSON]: JSON.stringify({ unity: '6000.0', unityRelease: '0f1' }),
  [MATRIX]: matrix('6000.0.53f1 6000.6.5f1'),
  ...Object.fromEntries(PROSE.map((file) => [file, 'Unity `6000.0` or newer; the Editor project is on `6000.6.5f1`.\n'])),
});

function check(t, changes = {}) {
  const dir = mkdtempSync(join(tmpdir(), 'aspid-unity-minimum-'));
  t.after(() => rmSync(dir, { recursive: true, force: true }));
  mkdirSync(join(dir, 'scripts'));
  cpSync(script, join(dir, 'scripts/check-unity-minimum.mjs'));
  for (const [file, text] of Object.entries({ ...valid(), ...changes })) {
    mkdirSync(dirname(join(dir, file)), { recursive: true });
    writeFileSync(join(dir, file), text);
  }
  return spawnSync('node', ['scripts/check-unity-minimum.mjs'], { cwd: dir, encoding: 'utf8' });
}

function assertFails(result, file, message) {
  assert.equal(result.status, 1, result.stdout + result.stderr);
  assert.ok(result.stdout.includes(`::error file=${file}::`), result.stdout);
  assert.match(result.stdout, message);
}

test('the same minimum line everywhere and a matrix that starts on it pass', (t) => {
  const result = check(t);
  assert.equal(result.status, 0, result.stdout + result.stderr);
  assert.match(result.stdout, /The minimum Unity 6000\.0 .* on 6000\.0\.53f1/);
});

test('a prose file without the minimum line is reported, and 6000.01 is no mention of 6000.0', (t) => {
  assertFails(check(t, { 'MIGRATION.md': 'Unity `6000.3` or newer.\n' }), 'MIGRATION.md', /no mention of the minimum Unity 6000\.0/);
  assertFails(check(t, { 'MIGRATION.md': 'Unity `6000.01`.\n' }), 'MIGRATION.md', /no mention/);
});

test('a matrix that starts on another line or below the minimum release is reported', (t) => {
  assertFails(check(t, { [MATRIX]: matrix('6000.1.17f1 6000.6.5f1') }), MATRIX, /not with a 6000\.0 release/);
  const raised = JSON.stringify({ unity: '6000.0', unityRelease: '60f1' });
  assertFails(check(t, { [PACKAGE_JSON]: raised }), MATRIX, /older than the minimum/);
  assert.equal(check(t, { [PACKAGE_JSON]: JSON.stringify({ unity: '6000.0', unityRelease: '53f1' }) }).status, 0);
});

test('a release is ordered by patch, then by stage, then by build', (t) => {
  assertFails(check(t, { [PACKAGE_JSON]: JSON.stringify({ unity: '6000.0', unityRelease: '53p1' }) }), MATRIX, /older than the minimum/);
  assert.equal(check(t, { [PACKAGE_JSON]: JSON.stringify({ unity: '6000.0', unityRelease: '53b9' }) }).status, 0);
  assert.equal(check(t, { [PACKAGE_JSON]: JSON.stringify({ unity: '6000.0', unityRelease: '9f1' }) }).status, 0);
});

test('a package.json without the fields or a matrix without UNITY_VERSIONS is reported', (t) => {
  assertFails(check(t, { [PACKAGE_JSON]: '{}' }), PACKAGE_JSON, /no "unity" field/);
  assertFails(check(t, { [MATRIX]: 'jobs:\n' }), MATRIX, /no "UNITY_VERSIONS: …" line/);
});
