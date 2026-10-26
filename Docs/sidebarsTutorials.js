// @ts-check
/**
 * Doc ids are `<folder>/readme`, the folder name run through `samplePrefixParser` in docusaurus.config.js.
 * `README.md` is the folder's index document, so the route is /tutorials/<folder>.
 * @type {import('@docusaurus/plugin-content-docs').SidebarsConfig}
 */
export default {
  tutorials: [
    { type: 'doc', id: 'index', label: 'Overview' },
    {
      type: 'category',
      label: 'Path',
      className: 'doc-menu-group',
      collapsible: false,
      items: ['counter/readme', 'greeter/readme', 'bind-modes/readme', 'stats/readme', 'todo-list/readme', 'custom-binder/readme'],
    },
    {
      type: 'category',
      label: 'Features',
      className: 'doc-menu-group',
      collapsible: false,
      items: ['virtualized-list/readme', 'dynamic-view-model/readme', 'di-integration/readme', 'example-scripts/readme'],
    },
  ],
};
