// Portable <pre><code> cells remain readable on GitHub. On the site, use the
// same code renderer as fenced blocks, including highlighting and copying.
function enhanceTableCode(node) {
  if (node.type === 'mdxJsxTextElement' && node.name === 'pre') {
    const language = node.attributes.find((attribute) => attribute.name === 'lang')?.value;
    const read = (part) => {
      if (part.type === 'text') return part.value;
      if (part.type !== 'mdxJsxTextElement') return null;
      if (part.name === 'br') return '\n';
      if (part.name !== 'pre' && part.name !== 'code') return null;
      const children = part.children.map(read);
      return children.includes(null) ? null : children.join('');
    };
    const value = read(node);
    if (typeof language === 'string' && value !== null) {
      delete node.name;
      delete node.attributes;
      delete node.children;
      Object.assign(node, {type: 'code', lang: language, value});
    }
  }
  node.children?.forEach(enhanceTableCode);
}

// A portable `<code lang="csharp">` stays plain inline code on GitHub; on the site it is highlighted in place.
function highlightInlineCode(node) {
  if (node.type === 'mdxJsxTextElement' && node.name === 'code') {
    const language = node.attributes.find((attribute) => attribute.name === 'lang')?.value;
    if (typeof language === 'string' && node.children.every((part) => part.type === 'text')) {
      const code = node.children.map((part) => part.value).join('');
      Object.assign(node, {
        name: 'InlineCode',
        attributes: [
          {type: 'mdxJsxAttribute', name: 'language', value: language},
          {type: 'mdxJsxAttribute', name: 'code', value: code},
        ],
        children: [],
      });
      return;
    }
  }
  node.children?.forEach(highlightInlineCode);
}

/** Highlights `<code lang="…">` on pages that do not run the introduction plugin (the sample pages). */
export function remarkInlineCode() {
  return (tree) => highlightInlineCode(tree);
}

/** Keep the package README unchanged while placing the site's TOC after its banner. */
export function remarkStatusBadges() {
  return (tree) => {
    function visit(node) {
      if (node.type === 'mdxJsxTextElement' && node.name === 'img'
        && node.attributes.some((attribute) => attribute.name === 'src'
          && attribute.value?.value?.includes('status-badge-'))) {
        node.attributes.push({type: 'mdxJsxAttribute', name: 'className', value: 'readme-status-badge'});
      }
      node.children?.forEach(visit);
    }
    visit(tree);
  };
}

/** The README banner's file name; the site draws the banner itself (src/components/IntroBanner). */
export const BANNER = 'Aspid.MVVMHeaderImage.png';

export default function remarkIntroBanner({baseUrl, siteUrl, title}) {
  return (tree) => {
    // Two-column tables made entirely of code are before/after comparisons.
    // Keep portable HTML in package Markdown and use native code blocks on the site.
    const tables = [];
    (function collect(node) {
      if (node.type === 'table') tables.push(node);
      else node.children?.forEach(collect);
    })(tree);
    for (const table of tables) {
      const [header, ...rows] = table.children;
      if (header.children.length !== 2 || rows.length === 0
        || !header.children.every((cell) => cell.children.every((part) => part.type === 'text'))
        || !rows.every((row) => row.children.length === 2 && row.children.every((cell) =>
          cell.children.length === 1 && cell.children[0].type === 'mdxJsxTextElement'
          && cell.children[0].name === 'pre'))) continue;
      enhanceTableCode(table);
      if (!rows.every((row) => row.children.every((cell) => cell.children[0].type === 'code'))) continue;
      table.data = {...table.data, hProperties: {...table.data?.hProperties, className: 'doc-code-comparison'}};
      const labels = header.children.map((cell) => cell.children.map((part) => part.value).join(''));
      rows.forEach((row) => row.children.forEach((cell, index) => {
        cell.data = {...cell.data, hProperties: {...cell.data?.hProperties, 'data-label': labels[index]}};
      }));
    }
    highlightInlineCode(tree);
    // Translated copies can start with generated front matter. The banner is the README's `<img>`, alone or inside the
    // `<picture>` that gives GitHub an animated source per theme; the site renders its own.
    const isBannerImage = (node) => node?.type === 'mdxJsxFlowElement' && node.name === 'img'
      && node.attributes.some((attribute) => attribute.name === 'src'
        && typeof attribute.value === 'string'
        && attribute.value.endsWith(`/${BANNER}`));
    // A Markdown banner (`![…](Images/Aspid.MVVMHeaderImage.png)`) leads the first paragraph, ahead of the badges that
    // share its line; it moves out into its own `<img>` with the site title as its alt text.
    const lead = tree.children.find((node) => node.type === 'paragraph');
    const markdownBanner = lead?.children[0];
    if (markdownBanner?.type === 'image' && markdownBanner.url.endsWith(`/${BANNER}`)) {
      lead.children.shift();
      const at = tree.children.indexOf(lead);
      tree.children.splice(at, lead.children.some((part) => part.type !== 'text' || part.value.trim()) ? 0 : 1, {
        type: 'mdxJsxFlowElement', name: 'img', children: [], attributes: [
          {type: 'mdxJsxAttribute', name: 'src', value: markdownBanner.url},
          {type: 'mdxJsxAttribute', name: 'alt', value: title},
        ]});
    }
    const banner = tree.children.find((node) => node.type === 'mdxJsxFlowElement' && ['img', 'picture'].includes(node.name));
    const image = banner?.name === 'picture' ? banner.children.find(isBannerImage) : banner;
    if (isBannerImage(image)) {
      banner.name = 'IntroBanner';
      banner.attributes = image.attributes;
      banner.children = [];
      // The paragraph after the badges is the project lede; the next one is the link row.
      const badges = tree.children.findIndex((node) => node.type === 'paragraph'
        && node.children.some((part) => part.type === 'link'
          && part.children.some((child) => child.type === 'image' && child.url.includes('status-badge-'))));
      const addClass = (node, className) => {
        if (node?.type !== 'paragraph') return;
        node.data = {...node.data, hProperties: {...node.data?.hProperties, className}};
      };
      if (badges !== -1) {
        // The badge images become chips whose icons can move; `status-badge-<kind>.svg` names the icon.
        const row = tree.children[badges];
        row.children = row.children.map((part) => {
          const image = part.type === 'link' ? part.children.find((child) => child.type === 'image') : part.type === 'image' ? part : undefined;
          const kind = image?.url.match(/status-badge-(\w+)\.svg$/)?.[1];
          if (!kind) return part;
          return {type: 'mdxJsxTextElement', name: 'StatusBadge', children: [], attributes: [
            {type: 'mdxJsxAttribute', name: 'kind', value: kind},
            {type: 'mdxJsxAttribute', name: 'label', value: image.alt ?? ''},
            ...(part.type === 'link' ? [{type: 'mdxJsxAttribute', name: 'href', value: part.url}] : []),
          ]};
        });
        addClass(tree.children[badges + 1], 'readme-lede');
        // The link row repeats the navigation the site already has, and points at this very page.
        const links = tree.children[badges + 2];
        if (links?.type === 'paragraph' && links.children.every((part) => part.type === 'link'
          || (part.type === 'text' && /^[\s·|\-–—]*$/.test(part.value)))) {
          tree.children.splice(badges + 2, 1);
        }
      }
      // The install section — the steps, the URL block and the note on versions — becomes one interactive panel.
      const install = tree.children.findIndex((node, index) => node.type === 'heading' && node.depth === 2
        && tree.children[index + 1]?.type === 'paragraph'
        && tree.children[index + 2]?.type === 'code' && /\.git#upm/.test(tree.children[index + 2].value)
        && tree.children[index + 3]?.type === 'paragraph');
      if (install !== -1) {
        tree.children.splice(install + 1, 3, {type: 'mdxJsxFlowElement', name: 'InstallPanel',
          attributes: [{type: 'mdxJsxAttribute', name: 'url', value: tree.children[install + 2].value.trim()}], children: []});
      }
      // On GitHub the features are grouped sections: a linked heading, a sentence and a preview.
      // The site keeps the group headings and turns each group's features into its own card grid.
      const features = tree.children.findIndex((node, index) => node.type === 'heading' && node.depth === 2
        && tree.children[index + 1]?.type === 'heading' && tree.children[index + 1].depth === 3
        && tree.children[index + 2]?.type === 'heading' && tree.children[index + 2].depth === 4);
      if (features !== -1) {
        let end = features + 1;
        while (end < tree.children.length && !(tree.children[end].type === 'heading' && tree.children[end].depth === 2)) end++;
        const section = tree.children.slice(features + 1, end);
        const jsx = (name, className, children) => ({
          type: 'mdxJsxFlowElement',
          name,
          attributes: [{type: 'mdxJsxAttribute', name: 'className', value: className}],
          children,
        });
        const paragraph = (className, children) => ({type: 'paragraph', data: {hProperties: {className}}, children});
        const result = [];
        let list;
        for (let i = 0; i < section.length; i++) {
          const node = section[i];
          if (node.type === 'heading' && node.depth === 3) {
            list = jsx('div', 'feature-cards', []);
            result.push({...node, data: {...node.data, hProperties: {...node.data?.hProperties, className: 'feature-group'}}}, list);
            continue;
          }
          if (!list || node.type !== 'heading' || node.depth !== 4) continue;
          const summary = section[i + 1];
          let preview = section[i + 2];
          // GitHub reads a sized <img>; the site lays the same capture out as a Markdown image.
          if (preview?.type === 'mdxJsxFlowElement' && preview.name === 'img') {
            const attribute = (name) => preview.attributes.find((item) => item.name === name)?.value;
            preview = {type: 'paragraph', children: [{type: 'image', url: attribute('src'), alt: attribute('alt') ?? ''}]};
          }
          if (summary?.type !== 'paragraph' || !preview
            || !(preview.type === 'code' || (preview.type === 'paragraph' && preview.children[0]?.type === 'image'))) continue;
          // `09-profiler-markers.md` → `profiler-markers`: the site component picks a live preview by page.
          const link = node.children.find((part) => part.type === 'link');
          const url = link?.url ?? '';
          const doc = url.replace(/^.*\//, '').replace(/^\d+-/, '').replace(/\.md$/, '');
          const livePreview = {...jsx('FeaturePreview', undefined, [preview]),
            attributes: [{type: 'mdxJsxAttribute', name: 'doc', value: doc}]};
          list.children.push(jsx('article', 'feature-card', [
            jsx('div', 'feature-card__preview', [livePreview]),
            jsx('div', 'feature-card__body', [
              paragraph('feature-card__title', node.children),
              paragraph('feature-card__text', summary.children),
              ...(link ? [paragraph('feature-card__more', [{...structuredClone(link),
                children: [{type: 'mdxJsxTextElement', name: 'FeatureCardMore', attributes: [], children: []}]}])] : []),
            ]),
          ]));
          i += 2;
        }
        if (result.some((node) => node.children?.length && node.name === 'div')) {
          tree.children.splice(features + 1, end - features - 1, ...result);
        }
      }
      // A section that is only a list of `[Link](…) — summary` items becomes a grid of link tiles.
      tree.children.forEach((node, index) => {
        const list = tree.children[index + 1];
        if (node.type !== 'heading' || node.depth !== 2 || list?.type !== 'list' || list.ordered) return;
        const items = list.children.map((item) => item.children.length === 1 && item.children[0].type === 'paragraph'
          ? item.children[0].children : null);
        if (!items.every((parts) => parts?.[0]?.type === 'link'
          && parts.slice(1).every((part, at) => at > 0 || (part.type === 'text' && /^\s*—\s*/.test(part.value))))) return;
        const block = (className, children) => ({type: 'paragraph', data: {hProperties: {className}}, children});
        tree.children[index + 1] = {
          type: 'mdxJsxFlowElement',
          name: 'div',
          attributes: [{type: 'mdxJsxAttribute', name: 'className', value: 'resource-cards'}],
          children: items.map(([link, ...rest]) => {
            // The text after the dash starts lower-case; on its own line in a tile it starts a sentence.
            const lead = rest[0]?.value.replace(/^\s*—\s*/, '');
            const summary = rest.length ? [{...rest[0], value: lead.charAt(0).toUpperCase() + lead.slice(1)}, ...rest.slice(1)] : [];
            return {
              type: 'mdxJsxFlowElement',
              name: 'div',
              attributes: [{type: 'mdxJsxAttribute', name: 'className', value: 'resource-card'}],
              children: [block('resource-card__title', [link]), ...(summary.length ? [block('resource-card__text', summary)] : [])],
            };
          }),
        };
      });
      // The help section — the issues paragraph and the star paragraph — becomes one call-to-action card; the licence
      // line goes, since the status badges and the footer already name the licence.
      const linkIn = (node, test) => node?.type === 'paragraph' && node.children.find((part) => part.type === 'link' && test(part.url));
      const help = tree.children.findIndex((node, index) => node.type === 'heading' && node.depth === 2
        && linkIn(tree.children[index + 1], (url) => /\/issues\/?$/.test(url))
        && linkIn(tree.children[index + 2], (url) => /^https:\/\/github\.com\/[^/]+\/[^/]+\/?$/.test(url)));
      if (help !== -1) {
        const issues = linkIn(tree.children[help + 1], (url) => /\/issues\/?$/.test(url)).url;
        const repo = linkIn(tree.children[help + 2], (url) => /^https:\/\/github\.com\/[^/]+\/[^/]+\/?$/.test(url)).url;
        const licence = linkIn(tree.children[help + 3], (url) => /\/LICENSE$/.test(url));
        tree.children.splice(help + 1, licence ? 3 : 2, {type: 'mdxJsxFlowElement', name: 'SupportPanel', children: [], attributes: [
          {type: 'mdxJsxAttribute', name: 'issues', value: issues},
          {type: 'mdxJsxAttribute', name: 'repo', value: repo},
        ]});
      }
      // The donation section — one sentence linking the Asset Store — becomes a card with a button to it; the link in
      // the sentence turns into plain text, since the button carries it.
      const isStore = (url) => /^https:\/\/assetstore\.unity\.com\//.test(url);
      const donate = tree.children.findIndex((node, index) => node.type === 'heading' && node.depth === 2
        && tree.children[index + 1]?.type === 'paragraph'
        && tree.children[index + 1].children.some((part) => part.type === 'link' && isStore(part.url))
        && (!tree.children[index + 2] || tree.children[index + 2].type === 'heading'));
      if (donate !== -1) {
        const sentence = tree.children[donate + 1].children;
        const store = sentence.find((part) => part.type === 'link' && isStore(part.url));
        tree.children[donate + 1] = {type: 'mdxJsxFlowElement', name: 'DonatePanel',
          attributes: [{type: 'mdxJsxAttribute', name: 'href', value: store.url}],
          children: sentence.flatMap((part) => (part === store ? part.children : [part]))};
      }
      // Keep absolute site links on GitHub, and native local links on the site. A link without a locale segment stays in
      // the page's locale: it becomes a site route, which Docusaurus prefixes with the current locale's base URL.
      function visit(node) {
        if (node.type === 'link') {
          const href = node.url.startsWith(`${siteUrl}${baseUrl}`) ? node.url.slice(siteUrl.length) : undefined;
          const route = href?.slice(baseUrl.length - 1);
          if (route && !/^\/[a-z]{2}(?:-[A-Za-z]{2,4})?(?:[/#?]|$)/.test(route)) {
            node.url = route;
          } else if (href) {
            node.type = 'mdxJsxTextElement';
            node.name = 'ReadmeLink';
            node.attributes = [{type: 'mdxJsxAttribute', name: 'href', value: href}];
            delete node.url;
            delete node.title;
          }
        }
        node.children?.forEach(visit);
      }
      visit(tree);
    }
  };
}
