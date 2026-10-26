import path from 'node:path';
import {mkdirSync, mkdtempSync, rmSync, writeFileSync} from 'node:fs';
import {tmpdir} from 'node:os';
import assert from 'node:assert/strict';
import {test} from 'node:test';
import {fileURLToPath} from 'node:url';
import remarkAgentPrompt from '../src/remark/agentPrompt.js';
import remarkCrossInstanceLinks from '../src/remark/crossInstanceLinks.js';
import remarkLiveDiagrams from '../src/remark/liveDiagrams.js';
import remarkThemedImages from '../src/remark/themedImages.js';
import {attribute, element, find, transform} from './remark-tree.mjs';

const site = fileURLToPath(new URL('..', import.meta.url));
const page = (...segments) => path.join(site, ...segments);
const ruDocs = ['i18n', 'ru', 'docusaurus-plugin-content-docs', 'current'];
const ruTutorials = ['i18n', 'ru', 'docusaurus-plugin-content-docs-tutorials', 'current'];
const urls = tree => find(tree, node => node.type === 'link' || node.type === 'definition').map(node => node.url);
const links = (file, ...targets) => urls(transform(remarkCrossInstanceLinks(), targets.map(url => `[x](${url})`).join('\n\n'), file));

test('a tutorial links a doc and a doc links a tutorial as routes', () => {
  assert.deepEqual(links(page('tutorials', '01. Counter', 'README.md'), '../../docs/03-binding-modes.md#oneway', '../../docs/README.md'),
    ['/docs/binding-modes#oneway', '/docs']);
  assert.deepEqual(links(page('docs', '01-getting-started.md'), '../tutorials/01.%20Counter/README.md', '../tutorials/VirtualizedList/README.md#setup'),
    ['/tutorials/counter', '/tutorials/virtualized-list#setup']);
});

test('translated pages link translated targets and get the same routes', () => {
  assert.deepEqual(links(page(...ruDocs, '01-getting-started.md'), '../../docusaurus-plugin-content-docs-tutorials/current/01.%20Counter/README.md'), ['/tutorials/counter']);
  assert.deepEqual(links(page(...ruTutorials, '01. Counter', 'README.md'), '../../../docusaurus-plugin-content-docs/current/03-binding-modes.md'), ['/docs/binding-modes']);
});

test('the tutorials index goes to the gallery', () => {
  assert.deepEqual(links(page('docs', 'README.md'), '../tutorials/index.mdx'), ['/tutorials']);
});

test('links inside one instance, external links, anchors and images stay as they are', () => {
  const same = ['03-binding-modes.md', 'StarterKit/README.md', 'https://example.test/a.md', '#oneway', 'Images/demo.gif'];
  assert.deepEqual(links(page('docs', '02-architecture.md'), ...same), same);
});

test('a link definition is rewritten like a link', () => {
  const tree = transform(remarkCrossInstanceLinks(), '[x][counter]\n\n[counter]: ../tutorials/01.%20Counter/README.md\n', page('docs', '01-getting-started.md'));
  assert.deepEqual(urls(tree), ['/tutorials/counter']);
});

test('a text prompt followed by a table becomes one agent prompt', () => {
  const table = '| A | B |\n|---|---|\n| 1 | 2 |\n';
  const [prompt] = find(transform(remarkAgentPrompt(), `\`\`\`text prompt\nAdd a marker.\n\`\`\`\n\n${table}`), element('AgentPrompt'));
  assert.equal(attribute(prompt, 'prompt'), 'Add a marker.');
  assert.equal(prompt.children[0].type, 'table');
  const plain = transform(remarkAgentPrompt(), `\`\`\`text\nAdd a marker.\n\`\`\`\n\n${table}`);
  assert.deepEqual(plain.children.map(node => node.type), ['code', 'table']);
});

test('a diagram without a registered live component stays a static image', () => {
  const markdown = '![Marker tree](Images/hierarchy.svg)\n\nMarker tree\n';
  assert.deepEqual(transform(remarkLiveDiagrams(), markdown), transform(() => {}, markdown));
});

test('an image with a light sibling is shown per theme, and its caption keeps the caption style', () => {
  const dir = mkdtempSync(path.join(tmpdir(), 'themed-images-'));
  try {
    mkdirSync(path.join(dir, 'Images'));
    writeFileSync(path.join(dir, 'Images', 'demo.gif'), '');
    writeFileSync(path.join(dir, 'Images', 'demo-light.gif'), '');
    const tree = transform(remarkThemedImages(), '![Demo](Images/demo.gif)\n\nDemo\n\n![Other](Images/no-such-image.png)\n', path.join(dir, 'README.md'));
    const [dark, light] = find(tree, element('span'));
    assert.equal(attribute(dark, 'className'), 'theme-image--dark');
    assert.equal(attribute(light, 'className'), 'theme-image--light');
    assert.equal(light.children[0].url, 'Images/demo-light.gif');
    assert.equal(tree.children[1].data.hProperties.className, 'doc-media-caption');
    assert.equal(find(tree, node => node.type === 'image' && node.url === 'Images/no-such-image.png').length, 1);
    assert.equal(find(tree, element('span')).length, 2);
  } finally {
    rmSync(dir, {recursive: true, force: true});
  }
});
