import {existsSync} from 'node:fs';
import path from 'node:path';

/**
 * Samples whose `demo`/`scene` captures show a scene rather than an editor window: the `tutorials/<Folder>/` names
 * (`01. Counter`). None has such captures yet; an empty list matches nothing.
 */
const SCENE_SAMPLES = [];
const escape = (name) => name.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
const never = /(?!)/;
// A tutorial page lives in `tutorials/<Folder>/` or its i18n twin.
const sceneSampleFolder = SCENE_SAMPLES.length
  ? new RegExp(`[/\\\\](?:${SCENE_SAMPLES.map(escape).join('|')})[/\\\\]`, 'i') : never;
// A doc page links the sample's own `tutorials/<Folder>/Images/`.
const sceneSampleUrl = SCENE_SAMPLES.length
  ? new RegExp(`tutorials/(?:${SCENE_SAMPLES.map((name) => escape(encodeURI(name))).join('|')})/`) : never;

/** A sibling `image-light.png` supplies the light theme; Markdown stays usable outside the site. */
export default function remarkThemedImages() {
  return (tree, file) => {
    if (!file.path) return;
    function visit(node) {
      // Existing sample captions repeat the image alt text in a following paragraph.
      node.children?.forEach((child, index, siblings) => {
        const image = child.type === 'paragraph' && child.children?.length === 1 && child.children[0];
        const caption = siblings[index + 1];
        if (image?.type === 'image' && caption?.type === 'paragraph'
          && caption.children?.every((part) => part.type === 'text')
          && caption.children.map((part) => part.value).join('') === image.alt) {
          caption.data = {...caption.data, hProperties: {...caption.data?.hProperties, className: 'doc-media-caption'}};
        }
      });
      if ((node.type === 'paragraph' || node.type === 'tableCell') && node.children?.length === 1) {
        const image = node.children[0];
        if (image.type === 'image' && !/^(?:[a-z]+:|\/|#)/i.test(image.url)) {
          const lightUrl = image.url.replace(/(?<!-light)(\.(?:png|gif|jpe?g|webp|svg))$/i, '-light$1');
          if (lightUrl !== image.url && existsSync(path.resolve(path.dirname(file.path), decodeURIComponent(lightUrl)))) {
            const sceneCapture = /(?:^|\/)(?:demo|scene)\.(?:gif|png)$/i.test(image.url);
            const sceneSample = sceneCapture && sceneSampleFolder.test(file.path);
            // A scene sample's footage linked from a doc page keeps its own background, which matches the article.
            const sceneFootage = sceneCapture && sceneSampleUrl.test(image.url);
            const sceneClass = sceneSample ? ' sample-scene' : sceneFootage ? ' scene-footage' : '';
            // Docusaurus replaces image nodes; put the theme class on a stable wrapper.
            const wrap = (child, theme) => ({
              type: 'mdxJsxTextElement',
              name: 'span',
              attributes: [{type: 'mdxJsxAttribute', name: 'className', value: `theme-image--${theme}${sceneClass}`}],
              children: [child],
            });
            node.children = [wrap(image, 'dark'), wrap({...image, url: lightUrl}, 'light')];
          }
        }
      }
      node.children?.forEach(visit);
    }
    visit(tree);
  };
}
