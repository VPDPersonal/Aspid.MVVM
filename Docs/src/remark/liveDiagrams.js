/**
 * Replaces a static diagram with its animated site component. The Markdown keeps the SVG, so GitHub and Unity
 * still show the picture; only the site renders the live version. The caption paragraph that repeats the alt
 * text stays in the document and keeps its caption style.
 * A table can have a live version too: Markdown keeps the table, and the component gets its rows as children.
 * So can an ordered list: Markdown keeps the steps, and the component gets each item's text as a child.
 */
// File name → component, or [component, props] when one component draws several pictures.
const COMPONENTS = {};

// First body cell → component that draws the table. Each row becomes `<RowComponent call="…">second cell</RowComponent>`.
const TABLES = {};

// First bold text of the first item → component that draws the ordered list; each item becomes a `<span>` child.
const LISTS = {};

// `<code lang="csharp">` is already `InlineCode` in the docs instance; elsewhere it is still the portable tag.
function cellCode(cell) {
  const [part] = cell.children;
  if (cell.children.length !== 1 || part.type !== 'mdxJsxTextElement') return undefined;
  if (part.name === 'InlineCode') return part.attributes.find((attribute) => attribute.name === 'code')?.value;
  if (part.name === 'code' && part.children.every((child) => child.type === 'text')) {
    return part.children.map((child) => child.value).join('');
  }
  return undefined;
}

function liveTable(node) {
  const [header, ...rows] = node.children;
  if (header.children.length !== 2 || rows.length === 0) return undefined;
  const entry = TABLES[cellCode(rows[0].children[0])];
  const calls = rows.map((row) => cellCode(row.children[0]));
  if (!entry || calls.includes(undefined)) return undefined;
  const [name, rowName] = entry;
  const attribute = (key, value) => ({type: 'mdxJsxAttribute', name: key, value});
  const alt = header.children[0].children.map((part) => part.value ?? '').join('');
  return {
    type: 'mdxJsxFlowElement',
    name,
    attributes: [attribute('alt', alt)],
    children: rows.map((row, index) => ({
      type: 'mdxJsxFlowElement',
      name: rowName,
      attributes: [attribute('call', calls[index])],
      children: row.children[1].children,
    })),
  };
}

function liveList(node) {
  if (!node.ordered) return undefined;
  const items = node.children.map((item) => (item.children.length === 1 && item.children[0].type === 'paragraph'
    ? item.children[0].children : undefined));
  if (items.length === 0 || items.includes(undefined)) return undefined;
  const strong = items[0].find((part) => part.type === 'strong');
  const name = strong && LISTS[strong.children.map((part) => part.value ?? '').join('')];
  if (!name) return undefined;
  return {
    type: 'mdxJsxFlowElement',
    name,
    attributes: [],
    children: items.map((children) => ({type: 'mdxJsxFlowElement', name: 'span', attributes: [], children})),
  };
}

export default function remarkLiveDiagrams() {
  return (tree) => {
    tree.children.forEach((node, index, siblings) => {
      if (node.type === 'table' || node.type === 'list') {
        const live = node.type === 'table' ? liveTable(node) : liveList(node);
        if (live) siblings[index] = live;
        return;
      }
      const image = node.type === 'paragraph' && node.children?.length === 1 && node.children[0];
      const entry = image?.type === 'image' && COMPONENTS[image.url.split('/').pop()];
      if (!entry) return;
      const [name, props = {}] = Array.isArray(entry) ? entry : [entry];
      siblings[index] = {
        type: 'mdxJsxFlowElement',
        name,
        attributes: [{alt: image.alt || '', ...props}].flatMap((all) => Object.entries(all))
          .map(([key, value]) => ({type: 'mdxJsxAttribute', name: key, value})),
        children: [],
      };
      const caption = siblings[index + 1];
      if (caption?.type === 'paragraph' && caption.children?.every((part) => part.type === 'text')
        && caption.children.map((part) => part.value).join('') === image.alt) {
        caption.data = {...caption.data, hProperties: {...caption.data?.hProperties, className: 'doc-media-caption'}};
      }
    });
  };
}
