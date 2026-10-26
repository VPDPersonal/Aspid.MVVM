---
name: docs-site
description: How Aspid.MVVM documentation is authored and published — Docusaurus site in `Docs/` with the pages in `Docs/docs`, the sample tutorials in `Docs/tutorials` and translations in `Docs/i18n`, GitHub Pages deploy, `en`/`ru` locales, versioning. Use when writing or editing any page under `Docs/`, adding a tutorial or sample, translating a page, cutting a docs version, changing `Docs/` config, or when the user asks "where does this doc go", "how do I link to", "add a page", "run the docs site".
---

# Documentation site

All documentation lives in `Docs/`, laid out as on the Aspid.FastTools site. The UPM package holds no docs or media:
only code, assets, sample scenes and license texts. Pages are GitHub Flavored Markdown, so GitHub renders them too;
the site adapts to them, never the other way round.

## Layout

| What | Where | Site route |
|---|---|---|
| Main docs (guide, StarterKit reference) | `Docs/docs/**/*.md`, images in `Docs/docs/Images/` | `/docs/...` |
| Tutorials | `Docs/tutorials/<Sample>/README.md` (folder named like `Samples~/<Sample>`), overview `Docs/tutorials/index.mdx` | `/tutorials/<slug>` |
| Translations | `Docs/i18n/<locale>/docusaurus-plugin-content-docs/current/**`, `…-docs-tutorials/current/<Sample>/README.md` | `/<locale>/...` |
| UI strings | `Docs/i18n/<locale>/code.json`, `*/current.json`, `docusaurus-theme-classic/navbar.json` | |
| Changelog | `CHANGELOG.md`, `CHANGELOG.<locale>.md` (repo root) | `/changelog` |
| Site config | `Docs/docusaurus.config.js`, `Docs/sidebars.js`, `Docs/sidebarsTutorials.js`, `Docs/sidebarsApi.js` | |
| CI | `.github/workflows/docs.yml` → GitHub Pages `https://vpdpersonal.github.io/Aspid.MVVM/` | |

Two docs plugin instances: the default one (`docs`) reads `Docs/docs` (`SUMMARY.md` excluded), the second
(`id: 'tutorials'`) reads `Docs/tutorials` with `include: ['index.mdx', '*/README.md']`.

Every page and translation is tracked where Docusaurus reads it; "Edit" links open that file
(`editLocalizedFiles`). Only the changelog is generated: `Docs/scripts/sync-i18n.mjs` copies the root
`CHANGELOG.<locale>.md` to `Docs/i18n/<locale>/CHANGELOG.md`, and `Docs/scripts/sync-changelog.mjs` turns the changelogs
into the `/changelog` page (`Docs/changelog/`, a version list as its sidebar). Both run before `start` and `build`; their
output is gitignored.

`SUMMARY.md` is a GitBook leftover kept as the GitHub table of contents. It is excluded from
the site; `Docs/sidebars.js` groups the guide pages (Basics, Core, Advanced) and autogenerates StarterKit.

## Writing rules (so GitHub and the site agree)

- **Front matter** only where a page needs it (`index.mdx`); a guide page takes its title from the first `# H1`,
  slug and order from the file/folder name.
- **Admonitions**: GitHub style only.
  ```md
  > [!NOTE]
  > Text.
  ```
  Allowed: `NOTE`, `TIP`, `IMPORTANT`, `WARNING`, `CAUTION`. Never `:::note`.
- **Links** are relative paths to the `.md` file, spaces as `%20`:
  `[Greeter](../02.%20Greeter/README.md)`, `[Binders](../../docs/06-binders.md)`, `[Counter](../tutorials/01.%20Counter/README.md)`.
  GitHub follows them as files. Docusaurus resolves links inside one plugin instance itself; links between
  `docs/` and `tutorials/` (and their i18n twins) are rewritten to site routes by `Docs/src/remark/crossInstanceLinks.js`.
  Never link by site URL, except from the introduction, whose absolute site links become routes on the site.
- **One `# H1` per file.** Extra H1s in the body get no anchor id on the site; use `##`.
- **Anchors** to headings must match a heading that exists; CI fails on stale ones.
- **Images** live in `Docs/docs/Images/` (or a tutorial's own `Images/`), referenced relatively; a `-light` twin
  next to an image is shown in the light theme.
- **Sample tutorials**: keep the sample facts at the top (scene table, files) and the fonts note at the bottom; the
  tutorial body goes between. The sample's scenes and code stay in `Samples~/<Sample>/` in the package.
- A translation mirrors its English page under `Docs/i18n/<locale>/…/current/` with the same path and links
  translated targets with relative paths inside `i18n/` (`../../docusaurus-plugin-content-docs-tutorials/current/01.%20Counter/README.md`);
  shared images are linked from `Docs/docs/Images/`. Missing pages fall back to English.
- Adding a language: add the locale to `LOCALES` in `docusaurus.config.js` and its folders under `Docs/i18n/`.

## Slugs and ids (tutorials)

`samplePrefixParser` in `docusaurus.config.js` turns the folder name into a slug:
`01. Counter` → `counter` (position 1), `VirtualizedList` → `virtualized-list`, `DynamicViewModel` →
`dynamic-view-model`. `README.md` is the folder's index document, so the route is `/tutorials/counter`
and the doc id used in `sidebarsTutorials.js` is `counter/readme`. `crossInstanceLinks.js` has a copy
of the same slug rules; change both together.

Main docs use the default parser: `03-binding-modes.md` → `/docs/binding-modes`,
`StarterKit/README.md` → `/docs/StarterKit`, `docs/README.md` → `/docs`. The site root `/`
redirects to `/docs` (`Docs/src/pages/index.js`).

## Adding a tutorial / sample

1. Create the sample in `Samples~/<NN. Name>/` (scenes, scripts, `.meta` files) and register it in the package
   `package.json` → `samples`.
2. Write the tutorial at `Docs/tutorials/<NN. Name>/README.md`; optionally its translation at
   `Docs/i18n/ru/docusaurus-plugin-content-docs-tutorials/current/<NN. Name>/README.md`.
3. Add `<slug>/readme` to `Docs/sidebarsTutorials.js` under `Path` or `Features`, and a card to
   `Docs/src/components/SamplesGallery/index.js`.

## Adding a main doc page

Drop the file into `Docs/docs/` (numbered prefix `NN-` orders it), add it to `Docs/sidebars.js` and to `SUMMARY.md`.
Optionally add the translation at `Docs/i18n/ru/docusaurus-plugin-content-docs/current/<same name>`.

## Versioning

Cut a version when the package version is released:

```bash
cd Docs
npm run version 1.1.0          # main docs → Docs/versioned_docs/version-1.1.0
```

Tutorials are not versioned: a tutorial always describes the sample of the package version in the working tree.
The site has no version dropdown; a docs snapshot (`npm run version`) keeps its own pages.

## Local run / check

```bash
cd Docs
npm ci
npm start                # http://localhost:3000/Aspid.MVVM/ (prestart syncs the changelogs)
npm run start:ru         # Russian locale (dev server serves one locale at a time)
npm test                 # unit tests of the site scripts and remark plugins (CI runs them)
npm run build            # what CI runs; fails on broken links
npm run check-selectors  # after a build: hashed Docusaurus classes the CSS selects still exist
Docs/scripts/serve-all.sh  # build + serve detached, one port per checkout (main: 3002)
```

`onBrokenLinks` and `onBrokenMarkdownLinks` are set to `throw`: a bad relative link breaks the build on purpose.

## API reference (`/api`)

The **API** tab is generated from the XML doc comments by DocFX, not written by hand. `Docs/api/` holds the
generated Markdown plus `sidebar.js` and **is committed**: DocFX has to compile the assemblies against the
local Unity installation, which CI does not have.

```bash
dotnet tool install -g docfx        # once
cd Docs && npm run api              # regenerate after public API or XML doc changes
```

`npm run api` runs three steps (`Docs/scripts/docfx-*.mjs`, `Docs/docfx/docfx.json`):

1. `docfx-projects.mjs` rewrites the legacy `.csproj` files Unity generates (`Aspid.MVVM`, `Aspid.MVVM.Unity`,
   `Aspid.MVVM.StarterKit`) into SDK-style projects under `Docs/docfx/projects/` (gitignored, absolute paths)
   with the same files, references, defines and the source generators as analyzers. Unity must have regenerated
   the `.csproj` files and `Library/ScriptAssemblies` first (open the project once).
2. `docfx metadata` writes one Markdown page per type into `Docs/api/`.
3. `docfx-postprocess.mjs` makes it MDX-safe: `<xref>` → links (own pages, learn.microsoft.com, Unity Scripting
   Reference), `<pre><code>` → fenced code, heading anchors as `{#id}`, escaped `<T`/`{}`, no "Inherited Members",
   front matter with a short `sidebar_label`, and `toc.yml` → `sidebar.js` (namespace → Classes/Interfaces/… groups).

The pages are served by a third docs plugin instance (`id: 'api'`) in `docusaurus.config.js`. Never edit files in
`Docs/api/` by hand; fix the XML comment or the postprocess script and regenerate. Translations are not
generated; the `ru` locale falls back to the English pages.

## Design

The site is the Aspid.FastTools site (`Aspid.FastTools/Website`) with MVVM content: when one of them changes its
theme or components, port the change to the other. Theme "Venom": acid green on near-black, light mode is neutral
paper; the accent can be switched to red, blue, yellow or mono (`Docs/src/accents.js`, `src/css/accents.css`, the
logos in `Docs/static/img/logo*.png` are the package icons from `Unity/Editor/Resources/Icons`). Fonts: IBM Plex
(self-hosted from `@fontsource`, `src/css/fonts.css`) and iA Writer Quattro (`src/fonts/`).

- No navbar on desktop: the doc sidebar is the whole chrome (`src/components/NavigationPanel`): brand, section
  switcher, search (`src/theme/SearchBar` + `src/plugins/search`, a local index built at build time), the doc list,
  and a footer with GitHub, Asset Store, language and appearance. Below 997px the same panel is the mobile drawer.
- `src/theme/Root`: the dot-grid background with its ambient glow, spotlight and click ripples (`src/components/Dot*`).
- `src/theme/DocItem/Layout`: the article panel, a floating TOC below 1400px (`FloatingToc`), the footer inside the
  article column. `src/theme/MDXComponents/Img`: zoomable images.
- The introduction (`Docs/docs/README.md`) is rebuilt by `src/remark/introBanner.js`: the
  `Images/Aspid.MVVMHeaderImage.png` banner becomes the animated `IntroBanner` (`src/intro.js` arms its entrance), and,
  when the README follows the FastTools layout, `status-badge-*.svg` badges become chips, the install section with
  a `.git#upm…` URL the `InstallPanel`, `####` feature headings cards, a `[Link](…) — summary` list tiles, the help
  section a `SupportPanel` and the donation sentence a `DonatePanel`. Absolute links to the site become routes.
- A feature card can play a live preview instead of its README code block: register a component under the doc slug
  in `PREVIEWS` of `src/components/FeaturePreview` (ViewModels, Binding Modes and Commands have one).
- `Docs/tutorials/index.mdx` is the tutorials overview (`/tutorials`): a card per sample from
  `src/components/SamplesGallery`. Add a new sample there too; a card shows its step until it gets a capture.
  The captures are dark/light GIF pairs in `Docs/static/img/samples/`, recorded in a hidden batch-mode Editor by
  `Docs/media/samples-gallery/` (its README has the steps); `capture: true` on a card shows them.
- `src/prism/venom.js`: Prism themes in the Ayu palette.
- The site root `/` redirects to `/docs` (`src/pages/index.js`); there is no landing page.

Check both themes after a change: build, `npx docusaurus serve`, and look at it in a browser (set
`localStorage.theme = 'light'`, and `localStorage['aspid-accent']` for an accent; `defaultMode` is dark). Do not run
`npm run build` while a dev server is running from the same folder: they share `.docusaurus/`, `i18n/` and
`node_modules/.cache`, and the dev server starts crashing with `Cannot read properties of undefined (reading 'id')`.
Build a copy of `Docs/` (with a real copy of `node_modules`, not a symlink) instead, or stop the dev server.

## Deploy

`.github/workflows/docs.yml` builds on every push to `main` touching `Docs/` or a root changelog, and on PRs
(build only). Pages source must be set to "GitHub Actions" once in the
repository settings.
