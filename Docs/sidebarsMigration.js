// @ts-check
/**
 * Doc ids are the file names in `migration/` (`1.1.md` → `1.1`); the newest guide goes first, so the navbar opens it.
 * @type {import('@docusaurus/plugin-content-docs').SidebarsConfig}
 */
export default {
  migration: [
    {
      type: 'category',
      label: 'Versions',
      className: 'doc-menu-group',
      collapsible: false,
      items: [{ type: 'doc', id: '1.1', label: '1.0 → 1.1' }],
    },
  ],
};
