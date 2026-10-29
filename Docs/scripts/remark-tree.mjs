import {unified} from 'unified';
import remarkGfm from 'remark-gfm';
import remarkMdx from 'remark-mdx';
import remarkParse from 'remark-parse';

// Docusaurus parses a page as MDX, so JSX elements such as `<picture>` or `<pre lang="csharp">` are `mdxJsx*` nodes.
const processor = unified().use(remarkParse).use(remarkMdx).use(remarkGfm);

/** Parses a page the way Docusaurus does and runs a remark plugin on it, as `(tree, file)`. */
export function transform(plugin, markdown, file = undefined) {
  const tree = processor.parse(markdown);
  plugin(tree, {path: file});
  return tree;
}

/** Every node of the tree that `test` accepts, in document order. */
export function find(tree, test) {
  const found = [];
  (function visit(node) {
    if (test(node)) found.push(node);
    node.children?.forEach(visit);
  })(tree);
  return found;
}

export const element = (name, className) => node => node.name === name && (className === undefined || attribute(node, 'className') === className);

/** The value of a JSX attribute of a node, or undefined. */
export function attribute(node, name) {
  return node.attributes?.find(item => item.name === name)?.value;
}
