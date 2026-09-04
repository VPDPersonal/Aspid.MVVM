// @ts-check
/**
 * Doc ids are `<folder>/readme`, the folder name run through `samplePrefixParser` in docusaurus.config.js.
 * `README.md` is the folder's index document, so the route is /tutorials/<folder>.
 * @type {import('@docusaurus/plugin-content-docs').SidebarsConfig}
 */
export default {
  tutorials: [
    {
      type: 'category',
      label: 'Path',
      collapsed: false,
      items: ['counter/readme', 'greeter/readme', 'bind-modes/readme', 'stats/readme', 'todo-list/readme', 'custom-binder/readme'],
    },
    {
      type: 'category',
      label: 'Features',
      collapsed: false,
      items: ['virtualized-list/readme', 'dynamic-view-model/readme', 'di-integration/readme', 'example-scripts/readme'],
    },
  ],
};
