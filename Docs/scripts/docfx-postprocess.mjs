/**
 * Adapts the Markdown that `docfx metadata` writes into `Docs/api/` for Docusaurus:
 *
 * - `<xref href="uid">` tags become Markdown links (to our own pages, learn.microsoft.com or the Unity
 *   Scripting Reference), since MDX does not know the element;
 * - the "Inherited Members" list is dropped (dozens of `object`/`Attribute` members on every page);
 * - `{` and `}` outside code are escaped, MDX would read them as expressions;
 * - every page gets front matter with a short title for the sidebar;
 * - `toc.yml` becomes `Docs/api/sidebar.js`, the sidebar of the `api` docs plugin instance. Core namespaces keep
 *   DocFX's Classes/Structs/… groups; `Aspid.MVVM.StarterKit` is grouped by the package folders instead
 *   (Binders → Text, Converters → Colors, …), since a flat list of a thousand binders is unreadable.
 *
 * Run after `docfx metadata` (see `npm run api`).
 */
import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const siteDir = path.dirname(path.dirname(fileURLToPath(import.meta.url)));
const apiDir = path.join(siteDir, 'api');

const files = new Set(fs.readdirSync(apiDir).filter((f) => f.endsWith('.md')));

/** Anchors DocFX emitted per file (`<a id="…">`), so member links only carry anchors that exist. */
const anchors = new Map(
  [...files].map((f) => [f, new Set([...fs.readFileSync(path.join(apiDir, f), 'utf8').matchAll(/<a id="([^"]+)"/g)].map((m) => m[1]))]),
);

/** `Aspid.MVVM.IBinder%601` → `Aspid.MVVM.IBinder-1` (the file name DocFX uses for generic types). */
const uidToFile = (uid) => uid.replace(/%60(\d+)/g, '-$1');
const uidToAnchor = (uid) => uid.replace(/%60/g, '_').replace(/[^A-Za-z0-9]/g, '_');

/** `Aspid.MVVM.IBinder%602` → `IBinder<T1, T2>`, `Aspid.MVVM.BindMode.TwoWay` → `BindMode.TwoWay`. */
function shortName(uid, keepParent) {
  const withoutParams = uid.replace(/\(.*$/, '');
  const parts = withoutParams.split('.');
  const name = keepParent ? parts.slice(-2).join('.') : parts[parts.length - 1];
  return name.replace(/%60(\d+)/g, (_, n) => {
    const count = Number(n);
    return count === 1 ? '<T>' : `<${Array.from({ length: count }, (_, i) => `T${i + 1}`).join(', ')}>`;
  });
}

function resolveXref(uid) {
  const file = uidToFile(uid);
  if (files.has(`${file}.md`)) return { href: `${file}.md`, text: shortName(uid, false) };

  // A member: link to the declaring type's page with the member anchor.
  const typeUid = uid.replace(/\(.*$/, '').replace(/\.[^.]+$/, '');
  const typeFile = uidToFile(typeUid);
  if (files.has(`${typeFile}.md`)) {
    const anchor = uidToAnchor(uid);
    const hash = anchors.get(`${typeFile}.md`).has(anchor) ? `#${anchor}` : '';
    return { href: `${typeFile}.md${hash}`, text: shortName(uid, true) };
  }

  if (uid.startsWith('System.')) {
    const apiPath = uid.replace(/\(.*$/, '').replace(/%60(\d+)/g, '-$1').toLowerCase();
    return { href: `https://learn.microsoft.com/dotnet/api/${apiPath}`, text: shortName(uid, false) };
  }
  if (uid.startsWith('UnityEngine.') || uid.startsWith('UnityEditor.')) {
    const page = uid.replace(/\(.*$/, '').replace(/%60\d+/g, '').split('.').slice(1).join('-');
    return { href: `https://docs.unity3d.com/ScriptReference/${page}.html`, text: shortName(uid, false) };
  }
  return { href: null, text: shortName(uid, false) };
}

function convertXrefs(markdown) {
  return markdown.replace(/<xref href="([^"]+)"[^>]*><\/xref>/g, (_, rawUid) => {
    const uid = rawUid.replace(/%7B/gi, '{').replace(/%7D/gi, '}');
    const { href, text } = resolveXref(uid);
    const label = `\`${text}\``;
    return href ? `[${label}](${href})` : label;
  });
}

/** `<pre><code class="lang-csharp">…</code></pre>` from `<example>` XML tags → fenced code, which MDX leaves alone and Prism highlights. */
function preToFence(markdown) {
  const decode = (html) => html.replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&quot;/g, '"').replace(/&#39;/g, "'").replace(/&amp;/g, '&');
  return markdown.replace(/<pre><code(?: class="lang-(\w+)")?>([\s\S]*?)<\/code><\/pre>/g, (_, lang, code) => `\n\`\`\`${lang ?? ''}\n${decode(code).trimEnd()}\n\`\`\`\n`);
}

/** DocFX backslash-escapes `#`, `_` and `-` inside link destinations; Docusaurus resolves them literally. */
function unescapeLinkTargets(markdown) {
  return markdown.replace(/\]\(([^)\s]+)\)/g, (_, target) => `](${target.replace(/\\([#_\-])/g, '$1')})`);
}

/** Extension methods declared on `object` (the logging helpers) show up on every page; drop those entries. */
function dropObjectExtensions(markdown) {
  return markdown
    .replace(/^\[LogMessageText\.[^\n]*\n/gm, '')
    .replace(/\n#### Extension Methods\n\s*(?=\n#{1,4} |$)/g, '\n');
}

/** Removes a `#### Heading` block up to the next heading. */
function dropSection(markdown, heading) {
  return markdown.replace(new RegExp(`\\n#### ${heading}\\n[\\s\\S]*?(?=\\n#{1,4} |$)`, 'g'), '\n');
}

/**
 * Escapes what MDX would read as JSX outside code: `{`/`}` expressions and `<T`-style generic brackets
 * (DocFX escapes only the closing `>`). Fenced and inline code are left alone, and so is the real HTML DocFX
 * emits from XML doc tags (`<p>`, `<a id>`, `<code class="paramref">`, lists).
 */
const HTML_TAGS = 'a|p|pre|code|br|ul|ol|li|em|strong|b|i|table|thead|tbody|tr|td|th|blockquote|span|div|h[1-6]';
const JSX_BRACKET = new RegExp(`(?<!\\\\)<(?!/|(?:${HTML_TAGS})[\\s>/])(?=[A-Za-z\\[])`, 'g');

function escapeMdx(markdown) {
  const parts = markdown.split(/(```[\s\S]*?```|`[^`\n]*`)/);
  return parts
    .map((part, i) => (i % 2 ? part : part.replace(/(?<!\\)([{}])/g, '\\$1').replace(JSX_BRACKET, '\\<')))
    .join('');
}

/** `### <a id="X"></a> Title` → `### Title {#X}`: Docusaurus only knows heading ids, not inline anchors. */
function headingAnchors(markdown) {
  return markdown.replace(/^(#+) <a id="([^"]+)"><\/a> (.+)$/gm, '$1 $3 {#$2}');
}

function titleOf(markdown, file) {
  const h1 = markdown.match(/^# (.+?)(?: \{#[^}]+\})?$/m)?.[1] ?? file;
  return h1.replace(/\\([<>()])/g, '$1');
}

for (const file of files) {
  const filePath = path.join(apiDir, file);
  let markdown = fs.readFileSync(filePath, 'utf8');
  if (markdown.startsWith('---\n')) continue; // already processed

  markdown = convertXrefs(markdown);
  markdown = preToFence(markdown);
  markdown = unescapeLinkTargets(markdown);
  markdown = dropSection(markdown, 'Inherited Members');
  markdown = dropObjectExtensions(markdown);
  markdown = escapeMdx(markdown);
  markdown = headingAnchors(markdown);

  const fullTitle = titleOf(markdown, file);
  const kind = fullTitle.split(' ')[0];
  const label = fullTitle.replace(/^(Namespace|Class|Interface|Struct|Enum|Delegate) /, '');
  const frontMatter = [
    '---',
    `title: ${JSON.stringify(fullTitle)}`,
    `sidebar_label: ${JSON.stringify(label)}`,
    `description: ${JSON.stringify(`${kind} ${label} — Aspid.MVVM API reference`)}`,
    'hide_title: true',
    'pagination_prev: null',
    'pagination_next: null',
    '---',
    '',
  ].join('\n');
  fs.writeFileSync(filePath, frontMatter + markdown);
}

// ---- toc.yml → sidebar.js ------------------------------------------------------------------------

function parseToc(yaml) {
  const root = { items: [] };
  const stack = [{ indent: -1, node: root }];
  for (const line of yaml.split('\n')) {
    const match = line.match(/^(\s*)- name: (.*)$/);
    const hrefMatch = line.match(/^\s*href: (.*)$/);
    if (match) {
      const indent = match[1].length;
      const node = { name: match[2].trim(), href: null, items: [] };
      while (stack[stack.length - 1].indent >= indent) stack.pop();
      stack[stack.length - 1].node.items.push(node);
      stack.push({ indent, node });
    } else if (hrefMatch) {
      stack[stack.length - 1].node.href = hrefMatch[1].trim();
    }
  }
  return root.items;
}

const docId = (href) => href.replace(/\.md$/, '');

// ---- StarterKit: folder-based grouping -----------------------------------------------------------

const starterKitDir = path.resolve(siteDir, '../Aspid.MVVM/Packages/tech.aspid.mvvm/StarterKit/Runtime');

/** Type name → folder segments under StarterKit/Runtime, from the declarations in the sources. */
function indexStarterKit(dir, segments = [], index = new Map()) {
  for (const entry of fs.readdirSync(dir, { withFileTypes: true })) {
    if (entry.isDirectory()) indexStarterKit(path.join(dir, entry.name), [...segments, entry.name], index);
    else if (entry.name.endsWith('.cs')) {
      const source = fs.readFileSync(path.join(dir, entry.name), 'utf8');
      for (const m of source.matchAll(/\b(?:class|struct|interface|enum|delegate)\s+(\w+)/g)) {
        if (!index.has(m[1])) index.set(m[1], segments);
      }
    }
  }
  return index;
}

const starterKitIndex = fs.existsSync(starterKitDir) ? indexStarterKit(starterKitDir) : new Map();

function starterKitFolder(label) {
  const name = label.replace(/<.*$/, '').split('.')[0]; // nested types (`Outer.Inner`) follow the outer type
  // Serializable twins (`TextBinder`) are generated from the MonoBehaviour (`TextMonoBinder`) and live in its folder.
  return starterKitIndex.get(name) ?? starterKitIndex.get(name.replace(/Binder$/, 'MonoBinder')) ?? ['Other'];
}

function starterKitCategory(node) {
  const tree = new Map(); // top folder → (sub folder | '' → docs)
  for (const item of node.items) {
    if (!item.href) continue;
    const [top = 'Other', sub = ''] = starterKitFolder(item.name);
    if (!tree.has(top)) tree.set(top, new Map());
    const subs = tree.get(top);
    if (!subs.has(sub)) subs.set(sub, []);
    subs.get(sub).push({ type: 'doc', id: docId(item.href), label: item.name });
  }
  const byLabel = (a, b) => a.label.localeCompare(b.label);
  const groups = [...tree.entries()].sort(([a], [b]) => a.localeCompare(b)).map(([top, subs]) => {
    const direct = (subs.get('') ?? []).sort(byLabel);
    const nested = [...subs.entries()]
      .filter(([sub]) => sub !== '')
      .sort(([a], [b]) => a.localeCompare(b))
      .map(([sub, docs]) => ({ type: 'category', label: sub, collapsed: true, items: docs.sort(byLabel) }));
    return { type: 'category', label: top, collapsed: true, items: [...nested, ...direct] };
  });
  return {
    type: 'category',
    label: node.name,
    collapsed: true,
    link: { type: 'doc', id: docId(node.href) },
    items: groups,
  };
}

/** Namespace → category linked to the namespace page; "Classes"/"Interfaces"/… headers → collapsed sub-categories. */
function namespaceCategory(node) {
  const groups = [];
  let current = null;
  for (const item of node.items) {
    if (!item.href) {
      current = { type: 'category', label: item.name, collapsed: true, items: [] };
      groups.push(current);
    } else if (current) {
      current.items.push({ type: 'doc', id: docId(item.href), label: item.name });
    } else {
      groups.push({ type: 'doc', id: docId(item.href), label: item.name });
    }
  }
  return {
    type: 'category',
    label: node.name,
    collapsed: true,
    link: { type: 'doc', id: docId(node.href) },
    items: groups.filter((g) => g.type === 'doc' || g.items.length > 0),
  };
}

const toc = parseToc(fs.readFileSync(path.join(apiDir, 'toc.yml'), 'utf8'));
const sidebar = toc.map((node) => (node.name === 'Aspid.MVVM.StarterKit' ? starterKitCategory(node) : namespaceCategory(node)));
fs.writeFileSync(
  path.join(apiDir, 'sidebar.js'),
  `// Generated by scripts/docfx-postprocess.mjs from toc.yml. Do not edit.\nexport default { api: ${JSON.stringify(sidebar, null, 2)} };\n`,
);
fs.rmSync(path.join(apiDir, 'toc.yml'));
console.log(`[docfx-postprocess] ${files.size} pages, ${sidebar.length} namespaces`);
