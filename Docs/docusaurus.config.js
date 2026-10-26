// @ts-check
import { execFileSync } from 'node:child_process';
import { readFileSync } from 'node:fs';
import venom from './src/prism/venom.js';
import remarkGithubAdmonitionsToDirectives from 'remark-github-admonitions-to-directives';
import remarkCrossInstanceLinks from './src/remark/crossInstanceLinks.js';
import remarkThemedImages from './src/remark/themedImages.js';
import remarkAgentPrompt from './src/remark/agentPrompt.js';
import remarkLiveDiagrams from './src/remark/liveDiagrams.js';
import remarkIntroBanner, {BANNER, remarkInlineCode, remarkStatusBadges} from './src/remark/introBanner.js';
import {ACCENT_BOOT_SCRIPT} from './src/accents.js';
import {INTRO_BOOT_SCRIPT} from './src/intro.js';

const TITLE = 'Aspid.MVVM';
const PACKAGE = '../Aspid.MVVM/Packages/tech.aspid.mvvm';
const LOCALES = ['en', 'ru'];
// Translations live in `Documentation/<locale>/`; they must not be picked up as English pages.
const TRANSLATION_FOLDERS = LOCALES.filter((locale) => locale !== 'en').map((locale) => `${locale}/**`);
const REPO = 'https://github.com/VPDPersonal/Aspid.MVVM';
const ASSET_STORE = 'https://assetstore.unity.com/packages/slug/298463';
const SITE_URL = 'https://vpdpersonal.github.io';
const BASE_URL = '/Aspid.MVVM/';
/** The docs in the working tree describe the package version in the working tree. */
const PACKAGE_VERSION = JSON.parse(readFileSync(new URL(`${PACKAGE}/package.json`, import.meta.url), 'utf8')).version;
/**
 * The UPM branch the install URL points at; each release tags it `<branch>/<version>`. A prerelease publishes to
 * `upm-preview`, a stable version to `upm`.
 */
const UPM_BRANCH = PACKAGE_VERSION.includes('-') ? 'upm-preview' : 'upm';

/** The translated sidebar label of the introduction, for a page under `i18n/<locale>/`. */
function introductionLabel(filePath) {
  const locale = filePath.match(/[\\/]i18n[\\/]([^\\/]+)[\\/]/)?.[1];
  if (!locale) return undefined;
  const file = new URL(`./i18n/${locale}/docusaurus-plugin-content-docs/current.json`, import.meta.url);
  try {
    return JSON.parse(readFileSync(file, 'utf8'))['sidebar.docs.doc.Introduction']?.message;
  } catch {
    return undefined;
  }
}

/** Orders `1.0.0-rc.10` after `1.0.0-rc.9`, and a release after its prereleases. */
function compareVersions(a, b) {
  const parse = (value) => value.split('-');
  const [coreA, preA] = parse(a);
  const [coreB, preB] = parse(b);
  const core = coreA.localeCompare(coreB, 'en', {numeric: true});
  if (core !== 0) return core;
  if (!preA || !preB) return preA ? -1 : preB ? 1 : 0;
  return preA.localeCompare(preB, 'en', {numeric: true});
}

/**
 * The versions the install panel can pin, per UPM branch and newest first: each branch's tags on GitHub, or the local
 * ones when offline. The working-tree version is always offered on its branch, since its tag may be pushed after
 * the docs are built.
 */
function readPackageVersions() {
  const read = (args) => {
    try {
      return execFileSync('git', args, {encoding: 'utf8', timeout: 15000, stdio: ['ignore', 'pipe', 'ignore']});
    } catch {
      return '';
    }
  };
  const branches = ['upm', 'upm-preview'];
  const patterns = branches.map((branch) => `${branch}/*`);
  const tags = read(['ls-remote', '--tags', '--refs', `${REPO}.git`, ...patterns]) || read(['tag', '-l', ...patterns]);
  const refs = tags.split('\n').map((line) => line.trim().split(/\s/).pop().replace(/^refs\/tags\//, ''));
  return Object.fromEntries(branches.map((branch) => {
    // A prerelease tagged on upm must not enable the Stable tab.
    const versions = refs.filter((ref) => ref.startsWith(`${branch}/`))
      .map((ref) => ref.slice(branch.length + 1))
      .filter((version) => branch !== 'upm' || !version.split('+')[0].includes('-'));
    if (branch === UPM_BRANCH) versions.push(PACKAGE_VERSION);
    return [branch, [...new Set(versions)].sort(compareVersions).reverse()];
  }));
}

/**
 * Turns a sample folder name such as `01. Counter` into `{ numberPrefix: 1, filename: 'counter' }`.
 * Folders without a number (`VirtualizedList`) keep their name in kebab-case and get no position.
 */
function samplePrefixParser(filename) {
  const match = filename.match(/^(\d+)\.\s*(.+)$/);
  const name = (match ? match[2] : filename)
    .replace(/([a-z])([A-Z])/g, '$1-$2')
    .replace(/\s+/g, '-')
    .toLowerCase();
  return { filename: name, numberPrefix: match ? Number(match[1]) : undefined };
}

/**
 * Shared options: GitHub-style `> [!NOTE]` alerts become Docusaurus admonitions, and file links
 * between the two plugin instances become site routes.
 */
const markdownOptions = {
  beforeDefaultRemarkPlugins: [remarkGithubAdmonitionsToDirectives, remarkCrossInstanceLinks, remarkAgentPrompt, remarkLiveDiagrams, remarkThemedImages],
  showLastUpdateTime: true,
  // A translation is edited where it lives in the package: `Documentation/<locale>/…`, `README.<locale>.md`,
  // `index.<locale>.mdx` (scripts/sync-i18n.mjs copies them into i18n/).
  editUrl: ({ versionDocsDirPath, docPath, locale }) => {
    const dir = versionDocsDirPath.replace(/^\.\.\//, '');
    if (locale === 'en') return `${REPO}/edit/main/${dir}/${docPath}`;
    if (dir.endsWith('/Documentation')) return `${REPO}/edit/main/${dir}/${locale}/${docPath}`;
    return `${REPO}/edit/main/${dir}/${docPath.replace(/\.(mdx?)$/, `.${locale}.$1`)}`;
  },
};

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: TITLE,
  tagline: 'Source Generator-based MVVM framework for Unity',
  // The scripts apply the stored accent colour (src/accents.js) and arm the introduction's entrance (src/intro.js) before
  // the first paint, so they come before the stylesheet, which would hold them back until it has loaded.
  // The icon is a plain tag, not the `favicon` field: React Helmet owns the tag that field renders and restores the default
  // icon on every navigation, undoing the accent's icon (src/accents.js).
  headTags: [
    { tagName: 'script', attributes: {}, innerHTML: ACCENT_BOOT_SCRIPT },
    { tagName: 'script', attributes: {}, innerHTML: INTRO_BOOT_SCRIPT },
    { tagName: 'link', attributes: { rel: 'icon', href: `${BASE_URL}img/logo.png` } },
  ],
  clientModules: ['./src/clientModules/accent.js'],

  url: SITE_URL,
  baseUrl: BASE_URL,
  customFields: {
    assetStore: ASSET_STORE, packageVersion: PACKAGE_VERSION, packageVersions: readPackageVersions(),
    // Captures framed in their file (a margin that continues the Inspector) get padded to the cards' inset on doc pages
    // (MDXComponents/Img). List their file names here, without the extension and the `-light` suffix.
    framedCaptures: {margin: 8, names: []},
  },
  organizationName: 'VPDPersonal',
  projectName: 'Aspid.MVVM',
  trailingSlash: false,

  onBrokenLinks: 'throw',
  // CI fails on a mistyped #anchor; a local build only warns, so a draft page can still be previewed.
  onBrokenAnchors: process.env.CI ? 'throw' : 'warn',
  markdown: {
    // The introduction uses a banner instead of a heading; retain its page metadata.
    async parseFrontMatter({filePath, fileContent, defaultParseFrontMatter}) {
      const result = await defaultParseFrontMatter({filePath, fileContent});
      if (fileContent.includes(`/${BANNER}`)) {
        result.frontMatter.title ??= TITLE;
        // Pagination reuses the untranslated sidebar label of a page without a title; give it the translated one.
        result.frontMatter.pagination_label ??= introductionLabel(filePath);
        result.frontMatter.hide_title = true;
        result.frontMatter.description ??= fileContent.match(/^\**Aspid\.MVVM (is|—) .*$/m)?.[0].replace(/\*\*|\\$/g, '');
      }
      return result;
    },
    hooks: { onBrokenMarkdownLinks: 'throw' },
  },

  i18n: {
    defaultLocale: 'en',
    locales: LOCALES,
    localeConfigs: {
      en: { label: 'English' },
      ru: { label: 'Русский' },
    },
  },

  presets: [
    [
      'classic',
      /** @type {import('@docusaurus/preset-classic').Options} */
      ({
        // Main documentation lives inside the UPM package so it ships to Unity users as-is.
        // Translations sit next to it in `Documentation/<locale>/` and are wired in by scripts/sync-i18n.mjs.
        docs: {
          path: `${PACKAGE}/Documentation`,
          routeBasePath: 'docs',
          breadcrumbs: false,
          sidebarPath: './sidebars.js',
          exclude: ['**/SUMMARY.md', '**/*.meta', ...TRANSLATION_FOLDERS],
          versions: { current: { label: PACKAGE_VERSION } },
          ...markdownOptions,
          beforeDefaultRemarkPlugins: [[remarkIntroBanner, {baseUrl: BASE_URL, siteUrl: SITE_URL, title: TITLE}], ...markdownOptions.beforeDefaultRemarkPlugins],
          remarkPlugins: [remarkStatusBadges],
        },
        blog: false,
        theme: { customCss: ['./src/css/fonts.css', './src/css/custom.css', './src/css/accents.css'] },
      }),
    ],
  ],

  plugins: [
    './src/plugins/search/index.js',
    './src/plugins/fonts/index.js',
    [
      // Tutorials are the README.md of each sample: one source for GitHub, Unity and the site.
      // Translations are `README.<locale>.md` beside it, wired in by scripts/sync-i18n.mjs.
      '@docusaurus/plugin-content-docs',
      /** @type {import('@docusaurus/plugin-content-docs').Options} */
      ({
        id: 'tutorials',
        path: `${PACKAGE}/Samples~`,
        routeBasePath: 'tutorials',
        breadcrumbs: false,
        sidebarPath: './sidebarsTutorials.js',
        // `index.mdx` is the site's overview of the samples (src/components/SamplesGallery); Unity skips `Samples~`.
        include: ['index.mdx', '*/README.md'],
        numberPrefixParser: samplePrefixParser,
        ...markdownOptions,
        beforeDefaultRemarkPlugins: [remarkInlineCode, ...markdownOptions.beforeDefaultRemarkPlugins],
      }),
    ],
    [
      // The root CHANGELOG.md (and CHANGELOG.<locale>.md), copied in by scripts/sync-changelog.mjs.
      '@docusaurus/plugin-content-docs',
      /** @type {import('@docusaurus/plugin-content-docs').Options} */
      ({
        id: 'changelog',
        path: 'changelog',
        routeBasePath: 'changelog',
        breadcrumbs: false,
        sidebarPath: './changelog/sidebars.json',
        showLastUpdateTime: true,
        editUrl: ({ locale }) => `${REPO}/edit/main/CHANGELOG${locale === 'en' ? '' : `.${locale}`}.md`,
        beforeDefaultRemarkPlugins: [remarkGithubAdmonitionsToDirectives],
      }),
    ],
    [
      // API reference generated by DocFX from the XML doc comments (`npm run api`), see scripts/docfx-*.mjs.
      '@docusaurus/plugin-content-docs',
      /** @type {import('@docusaurus/plugin-content-docs').Options} */
      ({
        id: 'api',
        path: 'api',
        routeBasePath: 'api',
        breadcrumbs: false,
        sidebarPath: './sidebarsApi.js',
        showLastUpdateTime: false,
        editUrl: undefined,
      }),
    ],
  ],

  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      colorMode: { defaultMode: 'dark', respectPrefersColorScheme: false },
      navbar: {
        title: TITLE,
        logo: { alt: TITLE, src: 'img/logo.png', width: 28, height: 28 },
        hideOnScroll: false,
        items: [
          { type: 'docSidebar', sidebarId: 'docs', position: 'left', label: 'Docs' },
          { type: 'docSidebar', docsPluginId: 'tutorials', sidebarId: 'tutorials', position: 'left', label: 'Tutorials' },
          { type: 'docSidebar', docsPluginId: 'api', sidebarId: 'api', position: 'left', label: 'API' },
          { to: '/changelog', label: 'Changelog', position: 'left' },
          { type: 'localeDropdown', position: 'right', className: 'navbar-locale' },
          { href: REPO, label: 'GitHub', position: 'right', className: 'navbar-github', 'aria-label': 'GitHub' },
        ],
      },
      footer: {
        style: 'dark',
        // One row: the author's contacts. Docs, GitHub, Asset Store and the changelog already live in the sidebar panel.
        links: [
          { label: 'Vladislav Panin', href: 'https://github.com/VPDPersonal' },
          { label: 'LinkedIn', href: 'https://www.linkedin.com/in/vpdaspid/' },
          { label: 'X', href: 'https://x.com/VPDInc' },
          { label: 'vpd.aspid@gmail.com', href: 'mailto:vpd.aspid@gmail.com' },
        ],
        copyright: `Copyright © ${new Date().getFullYear()} Vladislav Panin. MIT License.`,
      },
      prism: {
        theme: venom.light,
        darkTheme: venom.dark,
        additionalLanguages: ['csharp', 'json', 'bash'],
      },
    }),
};

export default config;
