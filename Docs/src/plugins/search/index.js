import {readFile} from 'node:fs/promises';
import path from 'node:path';

const ENTITIES = {amp: '&', lt: '<', gt: '>', quot: '"', apos: "'", nbsp: ' '};

/** Decodes the HTML entities Markdown sources use inside raw HTML (`&#10;` in table `<pre>`, `&#123;` for braces). */
function decodeEntities(text) {
  return text.replace(/&(?:#(\d+)|#x([\da-f]+)|(\w+));/gi, (match, dec, hex, name) =>
    dec ? String.fromCodePoint(Number(dec)) : hex ? String.fromCodePoint(parseInt(hex, 16)) : ENTITIES[name.toLowerCase()] ?? match);
}

// Use Docusaurus' resolved sources and permalinks so translations and versions stay aligned.
export function plainText(markdown) {
  const text = markdown
    .replace(/^---\r?\n[\s\S]*?\r?\n---\r?\n/, '')
    // MDX imports/exports, self-closing components and components with props are page machinery, not prose.
    .replace(/^(?:import|export)\s.*$/gm, ' ')
    .replace(/<[A-Z][\w.]*(?:\s[^>]*)?\/>|<\/?[A-Z][\w.]*\s+[\w-]+=[^>]*>/g, ' ')
    // Table separator rows and thematic breaks.
    .replace(/^\s*\|?(?:\s*:?-{3,}:?\s*\|)+\s*(?::?-{3,}:?\s*)?$/gm, ' ')
    .replace(/^\s*(?:-{3,}|\*{3,})\s*$/gm, ' ')
    .replace(/```[^\n]*\n/g, ' ').replace(/```/g, ' ')
    .replace(/!\[[^\]]*\]\([^)]*\)/g, '')
    // A badge is a link around an image: the image is gone above, the empty link is dropped here.
    .replace(/\[\]\([^)]*\)/g, '')
    // A link keeps its text. The text may hold an escaped bracket (`Type\[\]` in a generated API signature) or a
    // bracket pair of its own (`[<code lang="csharp">[TypeSelector]</code>](…)`), so a bare `[^\]]+` would skip it.
    .replace(/(?<!\\)\[((?:[^[\]\\]|\\.|\[[^\]]*\])+)\]\([^)]*\)/g, '$1')
    .replace(/<\/?(?:img|picture|source|a|div|span|p|br|details|summary|table|thead|tbody|tr|td|th|pre|code|ol|ul|li|b|i|em|strong|small|kbd|sup|sub)\b[^>]*>/gi, ' ')
    .replace(/\\([<>_{}()[\]])/g, '$1')
    .replace(/\{#[^}]+\}/g, '')
    .replace(/^\s*(?:#{1,6}|>)\s+/gm, ' ')
    .replace(/[*`|]/g, ' ');
  // Entities are decoded last, so an escaped `&lt;T&gt;` stays text instead of being taken for a tag above.
  return decodeEntities(text).replace(/\s+/g, ' ').trim();
}

/**
 * Docusaurus' `Link` applies `trailingSlash`, but the search bar pushes index URLs as they are: an index page's
 * permalink (`/tutorials/types/`) would open a URL that the static host does not serve when trailingSlash is false.
 */
export function indexUrl(permalink, {trailingSlash, baseUrl}) {
  return trailingSlash === false && permalink !== baseUrl ? permalink.replace(/\/$/, '') : permalink;
}

export default function searchPlugin(context) {
  let indexPath;
  return {
    name: 'aspid-search',
    async allContentLoaded({allContent, actions}) {
      const entries = [];
      for (const [id, content] of Object.entries(allContent['docusaurus-plugin-content-docs'] ?? {})) {
        for (const version of content.loadedVersions) {
          for (const doc of version.docs) {
            if (doc.draft || doc.unlisted) continue;
            const source = path.resolve(context.siteDir, doc.source.replace(/^@site\//, ''));
            const markdown = await readFile(source, 'utf8');
            const heading = markdown.match(/^# (.+)$/m)?.[1];
            entries.push({
              title: plainText(heading || doc.title),
              url: indexUrl(doc.permalink, {trailingSlash: context.siteConfig.trailingSlash, baseUrl: context.baseUrl}),
              section: {default: 'Docs', tutorials: 'Tutorials', api: 'API', changelog: 'Changelog'}[id] ?? id,
              description: plainText(doc.description || ''),
              text: plainText(markdown),
            });
          }
        }
      }
      indexPath = await actions.createData('index.json', JSON.stringify(entries));
    },
    configureWebpack() {
      return {resolve: {alias: {'@aspid-search-index': indexPath}}};
    },
  };
}
