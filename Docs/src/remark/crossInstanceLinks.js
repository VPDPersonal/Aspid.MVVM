/**
 * Rewrites Markdown links that cross the boundary between the docs and tutorials plugin instances.
 *
 * A tutorial links to the main docs as `../../docs/03-binding-modes.md`, and a doc links to a tutorial as
 * `../tutorials/01.%20Counter/README.md`. GitHub follows these as files, but Docusaurus resolves `.md` links only inside the
 * current plugin. This plugin resolves each link against the page's own file and turns a target in the other instance
 * into a site route, so the file links stay the single source of truth.
 *
 * Translations live in `i18n/<locale>/docusaurus-plugin-content-docs[-tutorials]/current/` and link translated
 * targets, so GitHub stays in the same language. Routes carry no locale: Docusaurus prefixes absolute routes with the
 * current locale itself.
 */
import path from 'node:path';
import {fileURLToPath} from 'node:url';

const siteDir = fileURLToPath(new URL('../..', import.meta.url));
const LOCALE = '[a-z]{2}(?:-[A-Za-z]{2,4})?';
const INSTANCES = [
  {route: '/docs', root: new RegExp(`^(?:docs|i18n/${LOCALE}/docusaurus-plugin-content-docs/current)/(.+)$`), slug: docSlug},
  {route: '/tutorials', root: new RegExp(`^(?:tutorials|i18n/${LOCALE}/docusaurus-plugin-content-docs-tutorials/current)/(.+)$`), slug: tutorialSlug},
];

function kebab(name) {
  return name
    .replace(/([a-z])([A-Z])/g, '$1-$2')
    .replace(/\s+/g, '-')
    .toLowerCase();
}

/** `03-type-selector.md` → `type-selector`; `README.md` → ``. */
function docSlug(file) {
  return file
    .replace(/\.mdx?$/, '')
    .split('/')
    .map((segment) => segment.replace(/^\d+-/, ''))
    .filter((segment) => !/^readme$/i.test(segment))
    .join('/');
}

/** `01. Counter/README.md` → `counter`, `index.mdx` → ``. Must match samplePrefixParser. */
function tutorialSlug(file) {
  const folder = file.split('/')[0];
  if (/^index\.mdx?$/.test(folder)) return '';
  const match = folder.match(/^(\d+)\.\s*(.+)$/);
  return kebab(match ? match[2] : folder);
}

/** The plugin instance a site file belongs to, with its path inside that instance. */
function locate(file) {
  const relative = path.relative(siteDir, file).split(path.sep).join('/');
  for (const instance of INSTANCES) {
    const match = relative.match(instance.root);
    if (match) return {instance, file: match[1]};
  }
  return null;
}

function rewrite(url, page) {
  if (/^(?:[a-z][a-z\d+.-]*:|\/|#)/i.test(url)) return null;
  const [, file, hash = ''] = /^([^?#]*)(?:\?[^#]*)?(#.*)?$/.exec(url);
  if (!/\.mdx?$/i.test(file)) return null;
  const source = locate(page);
  const targetFile = path.resolve(path.dirname(page), decodeURIComponent(file));
  const target = locate(targetFile);
  if (!source || !target || source.instance === target.instance) return null;
  const slug = target.instance.slug(target.file);
  return `${target.instance.route}${slug ? `/${slug}` : ''}${hash}`;
}

function visit(node, callback) {
  callback(node);
  if (node.children) node.children.forEach((child) => visit(child, callback));
}

export default function remarkCrossInstanceLinks() {
  return (tree, file) => {
    if (!file.path) return;
    visit(tree, (node) => {
      if ((node.type === 'link' || node.type === 'definition') && typeof node.url === 'string') {
        const target = rewrite(node.url, file.path);
        if (target) node.url = target;
      }
    });
  };
}
