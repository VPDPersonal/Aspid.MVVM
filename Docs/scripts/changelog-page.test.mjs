import assert from 'node:assert/strict';
import {test} from 'node:test';
import {changelogBody, changelogPage, changelogSidebars, versionAnchor} from './changelog-page.mjs';

const english = '# Changelog\n\n> Русская версия: [Website/i18n/ru/CHANGELOG.md](Website/i18n/ru/CHANGELOG.md).\n\nIntro.\n\n## [Unreleased]\n\n- One.\n\n## [1.0.0-rc.8] — 2026-09-06\n\n- Two.\n\n[1.0.0-rc.8]: https://example.test\n';
const russian = '# Журнал изменений (RU)\n\n> English version: [CHANGELOG.md](CHANGELOG.md).\n\n## [1.0.0-rc.8] — 2026-09-06\n';

test('a version becomes an anchor of lower-case words joined by dashes', () => {
  assert.equal(versionAnchor('Unreleased'), 'vunreleased');
  assert.equal(versionAnchor('1.0.0-rc.8'), 'v1-0-0-rc-8');
});

test('the page drops the language switch and anchors every version heading', () => {
  const body = changelogBody(english);
  assert.ok(!body.includes('Русская версия'));
  assert.match(body, /^## \[Unreleased\] \{#vunreleased\}$/m);
  assert.match(body, /^## \[1\.0\.0-rc\.8\] — 2026-09-06 \{#v1-0-0-rc-8\}$/m);
  assert.match(body, /^\[1\.0\.0-rc\.8\]: https:\/\/example\.test$/m);
});

test('a translation loses its language suffix and takes the title it is given', () => {
  assert.match(changelogBody(russian), /^# Журнал изменений$/m);
  assert.match(changelogBody(russian, 'История изменений'), /^# История изменений$/m);
  assert.ok(!changelogBody(russian).includes('English version'));
});

test('the front matter carries the date only when git has one', () => {
  assert.match(changelogPage(english, undefined, '2026-10-01T10:00:00Z'), /^---\nslug: \/\ndisplayed_sidebar: changelog\nlast_update:\n {2}date: 2026-10-01T10:00:00Z\n---\n\n# Changelog/);
  assert.match(changelogPage(english, undefined, null), /^---\nslug: \/\ndisplayed_sidebar: changelog\n---\n\n# Changelog/);
});

test('the sidebar links every version', () => {
  const [{items}] = changelogSidebars(english).changelog;
  assert.deepEqual(items, [
    {type: 'link', label: 'Unreleased', href: '/changelog#vunreleased'},
    {type: 'link', label: '1.0.0-rc.8', href: '/changelog#v1-0-0-rc-8'},
  ]);
});
