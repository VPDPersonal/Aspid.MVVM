import fs from 'node:fs';
import assert from 'node:assert/strict';
import {test} from 'node:test';
import remarkIntroBanner, {BANNER, remarkInlineCode} from '../src/remark/introBanner.js';
import {attribute, element, find, transform} from './remark-tree.mjs';

const options = {baseUrl: '/Aspid.MVVM/', siteUrl: 'https://vpdpersonal.github.io', title: 'Aspid.MVVM'};
const banner = (markdown) => transform(remarkIntroBanner(options), markdown, 'README.md');

test('the package introduction: its Markdown banner becomes the site banner, the badges stay in the lede', () => {
  const markdown = fs.readFileSync(new URL('../docs/README.md', import.meta.url), 'utf8');
  const tree = banner(markdown);
  assert.equal(tree.children[0].name, 'IntroBanner');
  assert.equal(attribute(tree.children[0], 'src'), `Images/${BANNER}`);
  assert.equal(attribute(tree.children[0], 'alt'), 'Aspid.MVVM');
  assert.equal(find(tree, node => node.type === 'image' && node.url.endsWith(BANNER)).length, 0);
  // Every site block of the introduction is in use.
  for (const name of ['StatusBadge', 'InstallPanel', 'FeaturePreview', 'SupportPanel', 'DonatePanel']) {
    assert.ok(find(tree, element(name)).length > 0, `${name} is rendered`);
  }
  assert.ok(find(tree, element('div', 'resource-card')).length > 0, 'the resources are tiles');
});

// The introduction in the layout the site components expect (the Aspid.FastTools README layout).
const introduction = [
  `<img src="Images/${BANNER}" alt="Aspid.MVVM" />`,
  '',
  '[![Unity 6000.0+](Images/status-badge-unity.svg)](https://assetstore.unity.com/packages/slug/298463)',
  '[![MIT License](Images/status-badge-license.svg)](https://github.com/VPDPersonal/Aspid.MVVM/blob/main/LICENSE)',
  '',
  'Aspid.MVVM is an MVVM framework for Unity.',
  '',
  '[Documentation](https://vpdpersonal.github.io/Aspid.MVVM/docs)',
  '',
  '## Installation',
  '',
  'In **Window → Package Manager**, paste this URL:',
  '',
  '```text',
  'https://github.com/VPDPersonal/Aspid.MVVM.git#upm-preview',
  '```',
  '',
  'The URL installs the latest preview.',
  '',
  '## Features',
  '',
  '### Core',
  '',
  '#### [ViewModels](04-viewmodels.md)',
  '',
  'Turns a field into a bindable property.',
  '',
  '```csharp',
  '[Bind] private int _count;',
  '```',
  '',
  '#### [Binding Modes](03-binding-modes.md)',
  '',
  'Four directions between the View and the ViewModel.',
  '',
  '<img src="Images/modes.gif" alt="Binding modes" width="640" />',
  '',
  '## Resources',
  '',
  '- [Changelog](https://vpdpersonal.github.io/Aspid.MVVM/changelog) — changes and fixes by version.',
  '- [API reference](https://vpdpersonal.github.io/Aspid.MVVM/api/Aspid.MVVM) — package types.',
  '',
  '## Help and support',
  '',
  'Report bugs in [GitHub Issues](https://github.com/VPDPersonal/Aspid.MVVM/issues).',
  '',
  'If the package helps you, star it on [GitHub](https://github.com/VPDPersonal/Aspid.MVVM).',
  '',
  'Distributed under the [MIT License](https://github.com/VPDPersonal/Aspid.MVVM/blob/main/LICENSE).',
  '',
  '## Support the project',
  '',
  'Aspid.MVVM is developed in spare time. Support it on the [Unity Asset Store](https://assetstore.unity.com/packages/slug/298463).',
  '',
].join('\n');
const tree = banner(introduction);

test('introduction: the banner, the status badges and the install panel', () => {
  assert.equal(tree.children[0].name, 'IntroBanner');
  assert.deepEqual(find(tree, element('StatusBadge')).map(node => attribute(node, 'kind')), ['unity', 'license']);
  const [panel, ...more] = find(tree, element('InstallPanel'));
  assert.equal(more.length, 0);
  assert.equal(attribute(panel, 'url'), 'https://github.com/VPDPersonal/Aspid.MVVM.git#upm-preview');
});

test('introduction: every feature heading becomes a card with a preview and a link', () => {
  assert.deepEqual(find(tree, element('FeaturePreview')).map(node => attribute(node, 'doc')), ['viewmodels', 'binding-modes']);
  assert.equal(find(tree, element('article', 'feature-card')).length, 2);
  assert.equal(find(tree, element('FeatureCardMore')).length, 2);
  assert.equal(find(tree, node => node.data?.hProperties?.className === 'feature-group').length, 1);
});

test('introduction: the resources become tiles and the help section a support panel', () => {
  const tiles = find(tree, element('div', 'resource-card'));
  assert.equal(tiles.length, 2);
  for (const tile of tiles) assert.equal(tile.children[0].data.hProperties.className, 'resource-card__title');
  const [support] = find(tree, element('SupportPanel'));
  assert.equal(attribute(support, 'issues'), 'https://github.com/VPDPersonal/Aspid.MVVM/issues');
  assert.equal(attribute(support, 'repo'), 'https://github.com/VPDPersonal/Aspid.MVVM');
});

test('introduction: the donation section becomes a card with a button to the Asset Store', () => {
  const [donate, ...more] = find(tree, element('DonatePanel'));
  assert.equal(more.length, 0);
  assert.match(attribute(donate, 'href'), /^https:\/\/assetstore\.unity\.com\//);
  assert.equal(find(donate, node => node.type === 'link').length, 0, 'the button carries the link');
});

test('introduction: absolute links to the site become routes in the page locale', () => {
  const site = `${options.siteUrl}${options.baseUrl}`;
  assert.equal(find(tree, node => node.type === 'link' && node.url.startsWith(site)).length, 0);
  const routes = find(tree, node => node.type === 'link' && node.url.startsWith('/')).map(node => node.url);
  assert.deepEqual(routes, ['/changelog', '/api/Aspid.MVVM']);
  const localized = banner('<img src="Images/' + BANNER + '" alt="Aspid.MVVM" />\n\n[RU](https://vpdpersonal.github.io/Aspid.MVVM/ru/docs)\n');
  assert.deepEqual(find(localized, element('ReadmeLink')).map(node => attribute(node, 'href')), ['/Aspid.MVVM/ru/docs']);
});

test('a page without the banner image is left as it is', () => {
  const page = '# Title\n\n## Installation\n\nPaste the URL:\n\n```text\nhttps://example.test/repo.git#upm\n```\n\nThat is all.\n';
  assert.deepEqual(banner(page), transform(() => {}, page));
});

const comparison = (header, cell) => `| ${header} | After |\n|---|---|\n| <pre lang="csharp">${cell}</pre> | <pre lang="csharp">b();</pre> |\n`;

test('a two-column table of <pre> cells becomes a code comparison with code blocks', () => {
  const tree = banner(comparison('Before', 'a();'));
  const [table] = find(tree, node => node.type === 'table');
  assert.equal(table.data.hProperties.className, 'doc-code-comparison');
  assert.deepEqual(find(table, node => node.type === 'code').map(node => [node.lang, node.value]), [['csharp', 'a();'], ['csharp', 'b();']]);
  assert.deepEqual(table.children[1].children.map(cell => cell.data.hProperties['data-label']), ['Before', 'After']);
});

test('a <code> in the header leaves the table as it is', () => {
  const [table] = find(banner(comparison('`Before`', 'a();')), node => node.type === 'table');
  assert.equal(table.data, undefined);
  assert.equal(find(table, node => node.type === 'code').length, 0);
});

test('<code lang> becomes highlighted inline code', () => {
  const tree = transform(remarkInlineCode(), 'Call <code lang="csharp">Run&lt;T&gt;()</code> first.\n');
  const [code] = find(tree, element('InlineCode'));
  assert.equal(attribute(code, 'language'), 'csharp');
  assert.equal(attribute(code, 'code'), 'Run<T>()');
});
