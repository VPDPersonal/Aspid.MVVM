/**
 * Docusaurus inlines every font under 10 kB into the stylesheet as a base64 data URI. Three Cyrillic faces of Plex Mono
 * are that small, so they would ride in the stylesheet of every page, English ones included, instead of loading only
 * where a page shows Cyrillic code. This plugin makes the fonts rule emit every font as a file.
 */
export default function fontsPlugin() {
  return {
    name: 'aspid-fonts',
    configureWebpack(config) {
      for (const rule of config.module?.rules ?? []) {
        if (!(rule.test instanceof RegExp) || !rule.test.test('font.woff2')) continue;
        // Under the limit a font is inlined (url-loader), above it, or at 0, a file is emitted (file-loader).
        for (const loader of Array.isArray(rule.use) ? rule.use : []) {
          if (loader?.options?.limit !== undefined) loader.options.limit = 0;
        }
      }
      return {};
    },
  };
}
