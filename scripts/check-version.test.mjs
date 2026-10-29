// Tests of scripts/check-version.mjs. The script finds the repository from its own location, so each test copies it
// next to a package.json and a README and reads the verdict. ci.yml runs it:
//   node --test scripts/*.test.mjs
import assert from 'node:assert/strict';
import { test } from 'node:test';
import { tmpdir } from 'node:os';
import { dirname, join } from 'node:path';
import { spawnSync } from 'node:child_process';
import { fileURLToPath } from 'node:url';
import { cpSync, mkdirSync, mkdtempSync, rmSync, writeFileSync } from 'node:fs';

const script = fileURLToPath(new URL('check-version.mjs', import.meta.url));
const PACKAGE_JSON = 'Aspid.MVVM/Packages/tech.aspid.mvvm/package.json';
const URL_BASE = 'https://github.com/VPDPersonal/Aspid.MVVM.git';

const readme = (channel, pin) => [
  '```', 'https://github.com/VPDPersonal/Aspid.FastTools.git#upm-preview', '```',
  '```', `${URL_BASE}#${channel}`, '```',
  '```', `${URL_BASE}#${pin}`, '```', '',
].join('\n');

function check(t, version, text) {
  const dir = mkdtempSync(join(tmpdir(), 'aspid-version-'));
  t.after(() => rmSync(dir, { recursive: true, force: true }));
  mkdirSync(join(dir, 'scripts'));
  cpSync(script, join(dir, 'scripts/check-version.mjs'));
  mkdirSync(dirname(join(dir, PACKAGE_JSON)), { recursive: true });
  writeFileSync(join(dir, PACKAGE_JSON), JSON.stringify({ version }));
  writeFileSync(join(dir, 'README.md'), text);
  return spawnSync('node', ['scripts/check-version.mjs'], { cwd: dir, encoding: 'utf8' });
}

test('a prerelease installs from upm-preview and pins its upm/ tag', (t) => {
  const result = check(t, '1.1.0-beta.1', readme('upm-preview', 'upm/1.1.0-beta.1'));
  assert.equal(result.status, 0, result.stdout + result.stderr);
});

test('a stable version installs from upm', (t) => {
  assert.equal(check(t, '1.1.0', readme('upm', 'upm/1.1.0')).status, 0);
  const result = check(t, '1.1.0', readme('upm-preview', 'upm/1.1.0'));
  assert.equal(result.status, 1);
  assert.match(result.stdout, /must use #upm for 1\.1\.0, not #upm-preview/);
});

test('the pinned URL names the whole version', (t) => {
  const result = check(t, '1.1.0', readme('upm', 'upm/1.1.0-beta.1'));
  assert.equal(result.status, 1);
  assert.match(result.stdout, /must use #upm\/1\.1\.0, not #upm\/1\.1\.0-beta\.1/);
});

test('a README without an install URL or a pinned one is reported', (t) => {
  const result = check(t, '1.1.0', '# Aspid.MVVM\n');
  assert.equal(result.status, 1);
  assert.match(result.stdout, /#\(none\)/);
});
