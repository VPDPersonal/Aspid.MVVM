// Tests of scripts/set-version.sh. The script finds the repository from its own location and changes its files, so each
// test runs it on a fixture repository: a package.json, a README with the install URLs and both CHANGELOGs, plus
// check-version.mjs, which the script runs last. ci.yml runs it:
//   node --test scripts/*.test.mjs
import assert from 'node:assert/strict';
import { test } from 'node:test';
import { tmpdir } from 'node:os';
import { dirname, join } from 'node:path';
import { spawnSync } from 'node:child_process';
import { fileURLToPath } from 'node:url';
import { cpSync, mkdirSync, mkdtempSync, readFileSync, rmSync, writeFileSync } from 'node:fs';

const scripts = fileURLToPath(new URL('.', import.meta.url));
const PACKAGE_JSON = 'Aspid.MVVM/Packages/tech.aspid.mvvm/package.json';
const REPO = 'https://github.com/VPDPersonal/Aspid.MVVM';
const CHANGELOGS = ['CHANGELOG.md', 'Docs/i18n/ru/CHANGELOG.md'];
const FILES = [PACKAGE_JSON, 'README.md', ...CHANGELOGS];

const packageJson = (version) =>
  `{\n  "name": "tech.aspid.mvvm",\n  "version": "${version}",\n  "dependencies": {\n    "tech.aspid.fasttools": "${version}"\n  }\n}\n`;

const readme = (channel, pin) => [
  '```', 'https://github.com/VPDPersonal/Aspid.FastTools.git#upm-preview', '```',
  '```', 'https://github.com/VPDPersonal/Aspid.Collections.git#upm', '```',
  '```', `${REPO}.git#${channel}`, '```',
  '```', `${REPO}.git#upm/${pin}`, '```', '',
].join('\n');

// A CHANGELOG with the unreleased notes under `heading` and one released version below.
const changelog = (heading, links = `[1.0.5]: ${REPO}/releases/tag/v1.0.5\n`) =>
  `# Changelog\n\n---\n\n${heading}\n\n### Fixed\n\n- A note for the release.\n\n## [1.0.5] — 2025-10-17\n\n- Old.\n\n${links}`;

const run = (command, args, cwd) => spawnSync(command, args, { cwd, encoding: 'utf8' });
const output = (result) => `${result.stdout}${result.stderr}`;
const read = (dir, file) => readFileSync(join(dir, file), 'utf8');
const write = (dir, file, text) => {
  mkdirSync(dirname(join(dir, file)), { recursive: true });
  writeFileSync(join(dir, file), text);
};
const snapshot = (dir) => FILES.map((file) => read(dir, file));

function sandbox(t, { version = '1.1.0-beta.1', en = '## [Unreleased]', ru = '## [Unreleased]' } = {}) {
  const dir = mkdtempSync(join(tmpdir(), 'aspid-set-version-'));
  t.after(() => rmSync(dir, { recursive: true, force: true }));
  for (const file of ['set-version.sh', 'check-version.mjs']) cpSync(join(scripts, file), join(dir, 'scripts', file));
  write(dir, PACKAGE_JSON, packageJson(version));
  write(dir, 'README.md', readme(version.includes('-') ? 'upm-preview' : 'upm', version));
  write(dir, 'CHANGELOG.md', changelog(en));
  write(dir, 'Docs/i18n/ru/CHANGELOG.md', changelog(ru));
  run('git', ['init', '--quiet'], dir);
  return dir;
}

const setVersion = (dir, version) => run('sh', ['scripts/set-version.sh', version], dir);
const version = (dir) => JSON.parse(read(dir, PACKAGE_JSON)).version;
const dated = (version) => new RegExp(`^## \\[${version.replaceAll('.', '\\.')}\\] — \\d{4}-\\d{2}-\\d{2}\\n\\n### Fixed\\n\\n- A note`, 'm');

test('a prerelease moves the files to the preview channel', (t) => {
  const dir = sandbox(t, { version: '1.0.5' });
  const result = setVersion(dir, '1.1.0-rc.1');
  assert.equal(result.status, 0, output(result));
  assert.equal(version(dir), '1.1.0-rc.1');
  // A dependency at the same number keeps it.
  assert.match(read(dir, PACKAGE_JSON), /"tech\.aspid\.fasttools": "1\.0\.5"/);

  const text = read(dir, 'README.md');
  assert.ok(text.includes(`${REPO}.git#upm-preview\n`), text);
  assert.ok(text.includes(`${REPO}.git#upm/1.1.0-rc.1\n`), text);
  assert.ok(text.includes('Aspid.Collections.git#upm\n'), text);

  for (const file of CHANGELOGS) {
    const notes = read(dir, file);
    assert.match(notes, /^## \[Unreleased\]\n\n## \[1\.1\.0-rc\.1\] — /m, file);
    assert.match(notes, dated('1.1.0-rc.1'), file);
    assert.ok(notes.includes(`\n[Unreleased]: ${REPO}/compare/v1.1.0-rc.1...HEAD\n[1.1.0-rc.1]: ${REPO}/releases/tag/v1.1.0-rc.1\n[1.0.5]: `), file);
  }
});

test('a stable version releases the notes under the line heading marked unreleased', (t) => {
  const links = `[1.1.0]: ${REPO}/compare/v1.0.5...HEAD\n[1.0.5]: ${REPO}/releases/tag/v1.0.5\n`;
  const dir = sandbox(t, { en: '## [1.1.0] — Unreleased', ru: '## [1.1.0] — Не выпущено' });
  for (const file of CHANGELOGS) write(dir, file, read(dir, file).replace(/\[1\.0\.5\]: .*\n/, links));
  const result = setVersion(dir, '1.1.0');
  assert.equal(result.status, 0, output(result));
  assert.match(result.stdout, /1\.1\.0-beta\.1 -> 1\.1\.0 \(upm\)/);

  const text = read(dir, 'README.md');
  assert.ok(text.includes(`${REPO}.git#upm\n`), text);
  assert.ok(text.includes(`${REPO}.git#upm/1.1.0\n`), text);
  for (const file of CHANGELOGS) {
    const notes = read(dir, file);
    assert.match(notes, dated('1.1.0'), file);
    assert.doesNotMatch(notes, /— (Unreleased|Не выпущено)/, file);
    // The compare link of the unreleased line becomes the release link, defined once.
    assert.equal(notes.match(/^\[1\.1\.0\]: /gm).length, 1, file);
    assert.ok(notes.includes(`[1.1.0]: ${REPO}/releases/tag/v1.1.0\n`), file);
  }
});

test('a second run with the same version repairs files and changes nothing else', (t) => {
  const dir = sandbox(t);
  assert.equal(setVersion(dir, '1.1.0').status, 0);
  const done = snapshot(dir);
  const again = setVersion(dir, '1.1.0');
  assert.equal(again.status, 0, output(again));
  assert.deepEqual(snapshot(dir), done);

  write(dir, PACKAGE_JSON, packageJson('1.1.1'));
  write(dir, 'README.md', readme('upm-preview', '1.1.0-beta.1'));
  assert.equal(setVersion(dir, '1.1.0').status, 0);
  assert.deepEqual(snapshot(dir).slice(1), done.slice(1));
  assert.equal(version(dir), '1.1.0');
});

test('a prerelease of a line whose notes sit under the line heading needs its own section', (t) => {
  const dir = sandbox(t, { en: '## [1.1.0] — Unreleased', ru: '## [1.1.0] — Не выпущено' });
  const before = snapshot(dir);
  const result = setVersion(dir, '1.1.0-rc.1');
  assert.notEqual(result.status, 0);
  assert.match(result.stderr, /no dated "## \[1\.1\.0-rc\.1\] — YYYY-MM-DD" section/);
  assert.deepEqual(snapshot(dir), before);

  // A dated section written by hand is kept, and gets its release link.
  for (const file of CHANGELOGS) {
    write(dir, file, read(dir, file).replace(/^(?=## \[1\.1\.0\] — )/m, '## [1.1.0-rc.1] — 2026-10-01\n\n- The preview.\n\n'));
  }
  const again = setVersion(dir, '1.1.0-rc.1');
  assert.equal(again.status, 0, output(again));
  for (const file of CHANGELOGS) {
    const notes = read(dir, file);
    assert.match(notes, /^## \[1\.1\.0-rc\.1\] — 2026-10-01$/m, file);
    assert.match(notes, /^## \[1\.1\.0\] — (Unreleased|Не выпущено)$/m, file);
    assert.ok(notes.includes(`[1.1.0-rc.1]: ${REPO}/releases/tag/v1.1.0-rc.1\n`), file);
  }

  // The stable release that follows puts the new [Unreleased] at the top, above the prerelease section.
  const stable = setVersion(dir, '1.1.0');
  assert.equal(stable.status, 0, output(stable));
  for (const file of CHANGELOGS) {
    assert.match(read(dir, file), /---\n\n## \[Unreleased\]\n\n## \[1\.1\.0-rc\.1\] — 2026-10-01\n[\s\S]*\n## \[1\.1\.0\] — \d{4}-\d{2}-\d{2}\n/, file);
  }
});

test('a version that is not SemVer is refused before any file changes', (t) => {
  const dir = sandbox(t);
  const before = snapshot(dir);
  for (const bad of ['1.0', '1.0.0.0', 'v1.0.0', '1.0.0-', '1.0.0 ', '1.0.0\nx', 'latest', '01.0.0', '1.0.0-rc.01', '1.0.0-rc..1']) {
    const result = setVersion(dir, bad);
    assert.notEqual(result.status, 0, JSON.stringify(bad));
    assert.match(result.stderr, /is not a SemVer version/, JSON.stringify(bad));
  }
  assert.deepEqual(snapshot(dir), before);
});

test('a CHANGELOG with nothing to release is refused before any file changes', (t) => {
  const dir = sandbox(t);
  write(dir, 'Docs/i18n/ru/CHANGELOG.md', read(dir, 'Docs/i18n/ru/CHANGELOG.md').replace(/### Fixed\n\n- A note for the release\.\n\n/, ''));
  const before = snapshot(dir);
  const result = setVersion(dir, '1.1.0');
  assert.notEqual(result.status, 0);
  assert.match(result.stderr, /^Docs\/i18n\/ru\/CHANGELOG\.md: no "## \[Unreleased\]"/m);
  assert.deepEqual(snapshot(dir), before);
});

test('a release link of the version to another URL is replaced, not defined twice', (t) => {
  const dir = sandbox(t);
  assert.equal(setVersion(dir, '1.1.0').status, 0);
  write(dir, 'CHANGELOG.md', read(dir, 'CHANGELOG.md').replace(`${REPO}/releases/tag/v1.1.0`, 'https://example.com/old'));
  const result = setVersion(dir, '1.1.0');
  assert.equal(result.status, 0, output(result));
  assert.equal(read(dir, 'CHANGELOG.md').match(/^\[1\.1\.0\]: /gm).length, 1);
  assert.ok(read(dir, 'CHANGELOG.md').includes(`[1.1.0]: ${REPO}/releases/tag/v1.1.0\n`));
});
