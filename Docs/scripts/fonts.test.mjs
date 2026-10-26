import assert from 'node:assert/strict';
import {readdirSync, readFileSync, statSync} from 'node:fs';
import {createRequire} from 'node:module';
import path from 'node:path';
import {test} from 'node:test';
import {fileURLToPath} from 'node:url';
import {getFileLoaderUtils} from '@docusaurus/utils';
import fontsPlugin from '../src/plugins/fonts/index.js';

const site = fileURLToPath(new URL('..', import.meta.url));
const require = createRequire(import.meta.url);
const fontsCss = readFileSync(path.join(site, 'src/css/fonts.css'), 'utf8');

/** Every file under `dir` whose name ends with one of `extensions`. */
function sourceFiles(dir, extensions) {
  return readdirSync(dir).flatMap((name) => {
    const file = path.join(dir, name);
    if (statSync(file).isDirectory()) return sourceFiles(file, extensions);
    return extensions.some((extension) => name.endsWith(extension)) ? [file] : [];
  });
}

/** The `@font-face` rules of fonts.css as `{family, style, weight, file, unicodeRange}`. */
function fontFaces() {
  return [...fontsCss.matchAll(/@font-face\s*\{([^}]*)\}/g)].map(([, body]) => {
    const get = (property) => body.match(new RegExp(`${property}:\\s*([^;]+);`))?.[1].trim();
    return {
      family: get('font-family').replace(/['"]/g, ''),
      style: get('font-style'),
      weight: Number(get('font-weight')),
      display: get('font-display'),
      file: get('src').match(/^url\('([^']+)'\)\s*format\('woff2'\)$/)?.[1],
      unicodeRange: get('unicode-range'),
    };
  });
}

test('the site requests no font from a third-party host', () => {
  const files = [path.join(site, 'docusaurus.config.js'), ...sourceFiles(path.join(site, 'src'), ['.js', '.css'])];
  for (const file of files) {
    assert.doesNotMatch(readFileSync(file, 'utf8'), /fonts\.(?:googleapis|gstatic)\.com/, `${path.relative(site, file)} links Google Fonts`);
  }
});

test('every IBM Plex face is a woff2 of the pinned package, with its own unicode range, for Latin and Cyrillic', () => {
  const faces = fontFaces();
  assert.ok(faces.length > 0, 'fonts.css declares no face');
  for (const face of faces) {
    const [, id, subset, weight, style] = face.file.match(/^@fontsource\/(ibm-plex-[a-z]+)\/files\/\1-([a-z-]+)-(\d+)-(normal|italic)\.woff2$/) ?? [];
    assert.ok(id, `${face.file} is not a woff2 file of an @fontsource/ibm-plex-* package`);
    assert.equal(face.style, style, `${face.file} is declared with font-style ${face.style}`);
    assert.equal(face.weight, Number(weight), `${face.file} is declared with font-weight ${face.weight}`);
    assert.equal(face.display, 'swap', `${face.file} has no font-display: swap`);
    assert.ok(statSync(require.resolve(face.file)).size > 0, `${face.file} is missing from the package`);
    const ranges = JSON.parse(readFileSync(require.resolve(`@fontsource/${id}/unicode.json`), 'utf8'));
    assert.equal(face.unicodeRange, ranges[subset], `${face.file} has a unicode-range that differs from the package's ${subset} subset`);
  }
  // A face needs both subsets: without Cyrillic the Russian pages fall back to a system font, without Latin the English ones.
  for (const family of new Set(faces.map((face) => face.family))) {
    for (const key of new Set(faces.filter((face) => face.family === family).map((face) => `${face.style} ${face.weight}`))) {
      const subsets = faces.filter((face) => face.family === family && `${face.style} ${face.weight}` === key).map((face) => face.file.match(/-(cyrillic|latin)-\d+-/)?.[1]);
      assert.deepEqual(subsets.sort(), ['cyrillic', 'latin'], `${family} ${key} needs a Latin and a Cyrillic rule`);
    }
  }
});

test('fonts.css declares every IBM Plex family the theme names', () => {
  const css = readFileSync(path.join(site, 'src/css/custom.css'), 'utf8');
  const named = new Set([...css.matchAll(/'(IBM Plex [A-Za-z]+)'/g)].map((match) => match[1]));
  assert.deepEqual([...named].sort(), ['IBM Plex Mono', 'IBM Plex Sans', 'IBM Plex Serif']);
  const declared = new Set(fontFaces().map((face) => face.family));
  for (const family of named) assert.ok(declared.has(family), `${family} has no @font-face rule in fonts.css`);
});

test('the fonts plugin makes the fonts rule emit files and leaves the other asset rules alone', () => {
  const {rules} = getFileLoaderUtils(false);
  const config = {module: {rules: [rules.images(), rules.fonts(), rules.svgs()]}};
  const imageLimit = config.module.rules[0].use[0].options.limit;
  assert.ok(imageLimit > 0, 'the images rule is expected to inline small files');
  assert.deepEqual(fontsPlugin().configureWebpack(config), {});
  assert.equal(config.module.rules[1].use[0].options.limit, 0);
  assert.equal(config.module.rules[0].use[0].options.limit, imageLimit);
  assert.equal(config.module.rules[2].use[0].options.limit, imageLimit);
  assert.deepEqual(fontsPlugin().configureWebpack({}), {});
});
