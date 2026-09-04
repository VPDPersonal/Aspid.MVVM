/**
 * Rewrites Markdown links that cross the boundary between the two docs plugin instances.
 *
 * A sample README links to the main docs as `../../Documentation/03-binding-modes.md`, and a doc
 * links to a tutorial as `../Samples~/01.%20Counter/README.md`. GitHub follows these as files,
 * but Docusaurus resolves `.md` links only inside the current plugin. This plugin turns them into
 * site routes before Docusaurus sees them, so the file links stay the single source of truth.
 *
 * Translated sources link translated targets (`Documentation/ru/...`, `README.ru.md`) so GitHub lands on
 * the same language. The locale segment is dropped here: Docusaurus prefixes absolute routes with the
 * current locale itself.
 */
const DOCS_RE = /^(?:\.\.\/)+Documentation\/(?:([a-z]{2}(?:-[A-Za-z]{2,4})?)\/)?(.+?)(?:\.md)?(#.*)?$/;
// `Samples~/` is optional so links between sibling tutorials (`../02.%20Greeter/README.ru.md`) are covered too:
// the translated copies are all named README.md, so the `.ru` file link only exists in the package.
const SAMPLES_RE = /^(?:\.\.\/)+(?:Samples~\/)?([^/]+)\/README(?:\.([a-z]{2}(?:-[A-Za-z]{2,4})?))?\.md(#.*)?$/i;

function kebab(name) {
  return name
    .replace(/([a-z])([A-Z])/g, '$1-$2')
    .replace(/\s+/g, '-')
    .toLowerCase();
}

/** `01. Counter` → `counter`, `VirtualizedList` → `virtualized-list`. Must match samplePrefixParser. */
function sampleSlug(folder) {
  const match = folder.match(/^(\d+)\.\s*(.+)$/);
  return kebab(match ? match[2] : folder);
}

/** `03-binding-modes` → `binding-modes`; `StarterKit/README` → `StarterKit`; `README` → ``. */
function docSlug(path) {
  return path
    .split('/')
    .map((segment) => segment.replace(/^\d+-/, ''))
    .filter((segment) => !/^readme$/i.test(segment))
    .join('/');
}

function rewrite(url) {
  const decoded = decodeURIComponent(url);

  const samples = decoded.match(SAMPLES_RE);
  if (samples) return `/tutorials/${sampleSlug(samples[1])}${samples[3] ?? ''}`;

  const docs = decoded.match(DOCS_RE);
  if (docs) {
    const slug = docSlug(docs[2]);
    return `/docs${slug ? `/${slug}` : ''}${docs[3] ?? ''}`;
  }

  return null;
}

function visit(node, callback) {
  callback(node);
  if (node.children) node.children.forEach((child) => visit(child, callback));
}

export default function remarkCrossInstanceLinks() {
  return (tree, file) => {
    visit(tree, (node) => {
      if ((node.type === 'link' || node.type === 'definition') && typeof node.url === 'string') {
        const target = rewrite(node.url);
        if (target) node.url = target;
      }
    });
  };
}
