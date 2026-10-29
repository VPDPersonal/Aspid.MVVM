import generated from './api/sidebar.js';

/**
 * Reshapes the generated API sidebar for navigation without touching `api/sidebar.js`:
 * - a namespace is a section whose title links to the namespace page, with a separate caret that folds it;
 * - the Classes/Structs/… layer is dropped; types keep DocFX's order (classes, structs, interfaces, enums, delegates);
 * - classes listed in TYPED_GROUPS fold into a collapsed group right after their anchor class;
 * - types outside any namespace gather under "Global".
 */
const PREFIX = /^Aspid\.MVVM\./;
// Each group follows its `anchor` class; `match` picks the per-value-type classes that repeat the anchor's members.
const TYPED_GROUPS = [];

function foldTypedGroups(items) {
  return TYPED_GROUPS.reduce((list, { anchor, match, label }) => {
    const typed = list.filter((item) => match.test(item.label ?? ''));
    if (typed.length < 2) return list;
    const rest = list.filter((item) => !match.test(item.label ?? ''));
    const at = Math.max(0, rest.findIndex((item) => item.label === anchor) + 1);
    rest.splice(at, 0, {
      type: 'category',
      label: `${label} (${typed.length})`,
      collapsed: true,
      className: 'api-overloads',
      items: typed,
    });
    return rest;
  }, items);
}

const docsIn = (item) => item.type === 'category' ? item.items.flatMap(docsIn) : [item];

/**
 * DocFX groups the StarterKit types by source folder (`Binders` › `Aggregator`) under the kind layer. Those folders
 * become the same collapsed groups as TYPED_GROUPS, one level deep, so a namespace reads like the others.
 */
function folderGroup(item) {
  if (item.type !== 'category') return item;
  const items = docsIn(item);
  return { type: 'category', key: item.key, label: `${item.label} (${items.length})`, collapsed: true, className: 'api-overloads', items };
}

function namespaceSection(category) {
  const label = category.label.replace(PREFIX, '');
  const kinds = category.items.filter((item) => item.type === 'category');
  const items = foldTypedGroups(kinds.flatMap((kind) => kind.items).map(folderGroup));
  return { type: 'category', label, collapsed: true, className: 'doc-menu-group api-namespace', link: category.link, items };
}

// DocFX emits a type outside any namespace as a childless category linked to its page.
const isNamespace = (item) => item.type === 'category' && item.items?.length > 0;
const asDoc = (item) => item.type === 'category' ? { type: 'doc', id: item.link.id, label: item.label } : item;

const namespaces = generated.api.filter(isNamespace).map(namespaceSection);
const globals = generated.api.filter((item) => !isNamespace(item)).map(asDoc);

const globalSection = { type: 'category', label: 'Global', collapsed: true, className: 'doc-menu-group api-namespace', items: globals };

export default {
  api: globals.length ? [...namespaces, globalSection] : namespaces,
};
