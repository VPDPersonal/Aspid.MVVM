/**
 * Joins an agent request to the comparison table under it: a ```text prompt block directly followed by a table becomes
 * `<AgentPrompt prompt="…">table</AgentPrompt>`, one frame with the request on top. GitHub and Unity ignore the
 * `prompt` meta and show a plain text block above the table.
 */
export default function remarkAgentPrompt() {
  return (tree) => {
    tree.children.forEach((node, index, siblings) => {
      const table = siblings[index + 1];
      if (node.type !== 'code' || node.lang !== 'text' || node.meta !== 'prompt' || table?.type !== 'table') return;
      siblings.splice(index, 2, {
        type: 'mdxJsxFlowElement',
        name: 'AgentPrompt',
        attributes: [{type: 'mdxJsxAttribute', name: 'prompt', value: node.value}],
        children: [table],
      });
    });
  };
}
