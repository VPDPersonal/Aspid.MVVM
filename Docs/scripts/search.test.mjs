import assert from 'node:assert/strict';
import path from 'node:path';
import {test} from 'node:test';
import {readdirSync, readFileSync} from 'node:fs';
import {fileURLToPath} from 'node:url';
import {isSearchShortcut, prepareIndex, searchEntries} from '../src/components/search.js';
import searchPlugin, {plainText} from '../src/plugins/search/index.js';

const entries = prepareIndex([
  {title: 'Types Sample', section: 'Tutorials', url: '/tutorials/counter', text: 'Choose Enemy Type in the Inspector.'},
  {title: 'Serializable Type System', section: 'Docs', url: '/docs/serializable-types', text: 'SerializableType<T> stores a type. Choose Enemy Type in a sample.'},
  {title: 'EnumValues<T>', section: 'API', url: '/api/enum', text: 'Maps enum values to data.'},
  {title: 'Выбор типа', section: 'Docs', url: '/ru/docs/types', text: 'Сохраняет объекты и настраивает поведение.'},
]);

test('exact titles rank first and multiple terms can match content', () => {
  assert.equal(searchEntries(entries, 'Types Sample')[0].url, '/tutorials/counter');
  assert.equal(searchEntries(entries, 'enemy inspector')[0].url, '/tutorials/counter');
  assert.deepEqual(searchEntries(entries, 'not-a-feature'), []);
});
test('search supports Cyrillic and generic type names', () => {
  assert.equal(searchEntries(entries, 'ОБЪЕКТЫ')[0].url, '/ru/docs/types');
  assert.equal(searchEntries(entries, 'SerializableType<T>')[0].url, '/docs/serializable-types');
  assert.equal(searchEntries(entries, 'EnumValues<T>')[0].url, '/api/enum');
});
test('index strips Markdown markup while preserving searchable code', () => {
  assert.equal(plainText('---\ntitle: Hidden\n---\n# Types\n![Screenshot](image.png)\n[Guide](guide.md)\n```csharp\nSerializableType<T> field;\n```'), 'Types Guide SerializableType<T> field;');
});
test('index decodes entities and drops table rules and MDX machinery', () => {
  const table = '| До — Unity API | После — FastTools |\n| --- | --- |\n| <pre>private static readonly&#10;ProfilerMarker _m;</pre> | <pre>if (x) &#123; y(); &#125;</pre> |';
  assert.equal(plainText(table), 'До — Unity API После — FastTools private static readonly ProfilerMarker _m; if (x) { y(); }');
  assert.equal(plainText('Intro\n\n---\n\nNext &lt;T&gt; &amp; more'), 'Intro Next <T> & more');
  assert.equal(plainText("import SamplesGallery from '@site/src/components/SamplesGallery';\n\n<SamplesGallery />\n\nUse <None> or List<float>."), 'Use <None> or List<float>.');
  assert.equal(plainText('<ol className="enum-lookup-flow"><li><b>Exact</b> match</li></ol>'), 'Exact match');
});
test('index drops the banner picture and the empty links of image badges', () => {
  const banner = [
    '<picture>',
    '  <source media="(prefers-color-scheme: dark)" srcset="banner_dark.webp" />',
    '  <img src="banner.png" alt="Aspid.FastTools" />',
    '</picture>',
    '',
    '[![Unity 6.0+](Images/badge.svg)](https://example.com/unity)',
    '[![MIT License](Images/license.svg)](https://example.com/license)',
    '',
    'Aspid.FastTools is a Unity package.',
    '',
    '[Documentation](https://example.com/docs)',
  ].join('\n');
  assert.equal(plainText(banner), 'Aspid.FastTools is a Unity package. Documentation');
});
test('index keeps the text of a link whose text holds brackets', () => {
  assert.equal(
    plainText('Display settings from [<code lang="csharp">[TypeSelectorDisplay]</code>](03-type-selector.md#typeselectordisplay) apply here.'),
    'Display settings from [TypeSelectorDisplay] apply here.');
  assert.equal(plainText('Use [`list[0]`](guide.md) and [Guide](guide.md).'), 'Use list[0] and Guide.');
  assert.equal(plainText('A [lone] bracket and an escaped \\[bracket\\](not-a-link).'), 'A [lone] bracket and an escaped [bracket](not-a-link).');
});
test('index unescapes the characters DocFX escapes in generated API pages', () => {
  const link = '[VisualElementExtensions.AddChildren\\<TypeField\\>\\(TypeField, params VisualElement?\\[\\]?\\)](Aspid.FastTools.UIElements.VisualElementExtensions.md#Aspid_AddChildren__1___0_UnityEngine_UIElements_VisualElement___)';
  assert.equal(plainText(link), 'VisualElementExtensions.AddChildren<TypeField>(TypeField, params VisualElement?[]?)');
  assert.equal(plainText('### CreateField\\(SerializedProperty, params Type\\[\\]\\) {#Aspid_CreateField}'), 'CreateField(SerializedProperty, params Type[])');
});
test('the search text of every page has no link or escape leftovers', () => {
  const siteDir = path.dirname(path.dirname(fileURLToPath(import.meta.url)));
  // A plain walk: `recursive` and `Dirent.parentPath` need Node 20.1 and 20.12, and engines allows 20.0.
  const pages = (directory) => readdirSync(directory, {withFileTypes: true}).flatMap((entry) => {
    const file = path.join(directory, entry.name);
    if (entry.isDirectory()) return pages(file);
    return entry.isFile() && /\.mdx?$/.test(entry.name) ? [file] : [];
  });
  const leftover = /\]\([^)\s]*\.md[^)]*\)|\[ \[|\] \]|\\[()[\]<>]/;
  const packageDir = '../Aspid.MVVM/Packages/tech.aspid.mvvm';
  const bad = [`${packageDir}/Documentation`, `${packageDir}/Samples~`, 'api', 'i18n']
    .flatMap((directory) => pages(path.join(siteDir, directory)))
    .filter((file) => leftover.test(plainText(readFileSync(file, 'utf8'))));
  assert.deepEqual(bad.map((file) => path.relative(siteDir, file)), []);
});
test('empty queries suggest documentation and tutorials', () => {
  assert.equal(searchEntries(entries, '  ').length, 3);
  assert.ok(searchEntries(entries, '').every((entry) => entry.section !== 'API'));
});
test('index URLs follow trailingSlash: false except the locale root', async () => {
  const siteDir = path.dirname(path.dirname(fileURLToPath(import.meta.url)));
  const doc = (permalink) => ({permalink, title: 'Counter', source: '@site/../Aspid.MVVM/Packages/tech.aspid.mvvm/Samples~/01. Counter/README.md'});
  const allContent = {'docusaurus-plugin-content-docs': {tutorials: {loadedVersions: [{docs: [
    doc('/Aspid.MVVM/ru/'), doc('/Aspid.MVVM/ru/tutorials/'), doc('/Aspid.MVVM/ru/tutorials/counter/'),
    doc('/Aspid.MVVM/ru/docs/binding-modes'),
  ]}]}}};
  let index;
  const context = {siteDir, baseUrl: '/Aspid.MVVM/ru/', siteConfig: {trailingSlash: false, baseUrl: '/Aspid.MVVM/'}};
  await searchPlugin(context).allContentLoaded({allContent, actions: {createData: async (name, data) => { index = JSON.parse(data); }}});
  assert.deepEqual(index.map((entry) => entry.url),
    ['/Aspid.MVVM/ru/', '/Aspid.MVVM/ru/tutorials', '/Aspid.MVVM/ru/tutorials/counter', '/Aspid.MVVM/ru/docs/binding-modes']);
});
test('Ctrl or Cmd with K opens search on Latin and non-Latin layouts', () => {
  assert.ok(isSearchShortcut({ctrlKey: true, key: 'k', code: 'KeyK'}));
  assert.ok(isSearchShortcut({metaKey: true, key: 'K', code: 'KeyK'}));
  assert.ok(isSearchShortcut({ctrlKey: true, key: 'л', code: 'KeyK'}));
  assert.ok(isSearchShortcut({ctrlKey: true, key: 'k', code: 'KeyV'}));
  assert.ok(!isSearchShortcut({key: 'k', code: 'KeyK'}));
  assert.ok(!isSearchShortcut({ctrlKey: true, key: 'л', code: 'KeyL'}));
  assert.ok(!isSearchShortcut({ctrlKey: true, key: 't', code: 'KeyK'}));
  assert.ok(!isSearchShortcut({ctrlKey: true, code: 'KeyJ'}));
});
