/**
 * Builds Docs/i18n from the translations that live inside the UPM package, so the package stays the
 * single source of truth and contributors never touch Docusaurus' plugin-named folders by hand.
 *
 *   Documentation/<locale>/**            → i18n/<locale>/docusaurus-plugin-content-docs/current/**
 *   Documentation/Images/**              → i18n/<locale>/docusaurus-plugin-content-docs/Images/** (`../Images/` links)
 *   Samples~/<Sample>/README.<locale>.md → i18n/<locale>/docusaurus-plugin-content-docs-tutorials/current/<Sample>/README.md
 *   Samples~/index.<locale>.mdx          → i18n/<locale>/docusaurus-plugin-content-docs-tutorials/current/index.mdx
 *   CHANGELOG.<locale>.md (repo root)    → i18n/<locale>/CHANGELOG.md, which scripts/sync-changelog.mjs turns into a page
 *
 * Files are copied, not symlinked: webpack resolves symlinks to their real path, which breaks the
 * relative Markdown links inside a translation. The copies are build artifacts and are gitignored; the site's own UI
 * strings (`i18n/<locale>/*.json`) are tracked and left in place. Runs before `start` and `build`.
 */
import { execFileSync } from 'node:child_process';
import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const siteDir = path.dirname(path.dirname(fileURLToPath(import.meta.url)));
const packageDir = path.resolve(siteDir, '../Aspid.MVVM/Packages/tech.aspid.mvvm');
const docsDir = path.join(packageDir, 'Documentation');
const samplesDir = path.join(packageDir, 'Samples~');
const i18nDir = path.join(siteDir, 'i18n');

const locales = fs
  .readdirSync(docsDir, { withFileTypes: true })
  .filter((entry) => entry.isDirectory() && /^[a-z]{2}(-[A-Za-z]{2,4})?$/.test(entry.name))
  .map((entry) => entry.name);

const repoDir = path.resolve(siteDir, '..');
const GENERATED = ['docusaurus-plugin-content-docs/current', 'docusaurus-plugin-content-docs/Images', 'docusaurus-plugin-content-docs-tutorials/current', 'CHANGELOG.md'];
for (const locale of fs.existsSync(i18nDir) ? fs.readdirSync(i18nDir) : []) {
  for (const generated of GENERATED) fs.rmSync(path.join(i18nDir, locale, generated), { recursive: true, force: true });
}

/** ISO date of the last commit touching `file`, or null when git has no history for it (uncommitted). */
function lastCommitDate(file) {
  try {
    return execFileSync('git', ['log', '-1', '--format=%cI', '--', file], { cwd: path.dirname(file), stdio: ['ignore', 'pipe', 'ignore'] })
      .toString().trim() || null;
  } catch {
    return null;
  }
}

/**
 * A copy is not tracked by git, so Docusaurus cannot read its history: each copied page gets `last_update.date` from
 * its source's last commit, as the English pages show it.
 */
function stampLastUpdate(source, destination) {
  const date = lastCommitDate(source);
  if (!date) return;
  const markdown = fs.readFileSync(destination, 'utf8');
  const frontMatter = markdown.match(/^---\r?\n([\s\S]*?)\r?\n---\r?\n/);
  const field = `last_update:\n  date: ${date}`;
  fs.writeFileSync(destination, frontMatter && !/^last_update:/m.test(frontMatter[1])
    ? markdown.replace(frontMatter[0], `---\n${frontMatter[1]}\n${field}\n---\n`)
    : frontMatter ? markdown : `---\n${field}\n---\n\n${markdown}`);
}

function copy(source, destination, { stamp = true } = {}) {
  fs.mkdirSync(path.dirname(destination), { recursive: true });
  fs.cpSync(source, destination, { recursive: true, filter: (file) => !file.endsWith('.meta') });
  const pages = fs.statSync(source).isDirectory()
    ? fs.readdirSync(source, { recursive: true }).filter((file) => /\.mdx?$/.test(file))
      .map((file) => [path.join(source, file), path.join(destination, file)])
    : /\.mdx?$/.test(source) ? [[source, destination]] : [];
  if (stamp) for (const [from, to] of pages) stampLastUpdate(from, to);
}

for (const locale of locales) {
  copy(path.join(docsDir, locale), path.join(i18nDir, locale, 'docusaurus-plugin-content-docs', 'current'));
  // A translation links the shared images as `../Images/…`; give the copy the same neighbour.
  copy(path.join(docsDir, 'Images'), path.join(i18nDir, locale, 'docusaurus-plugin-content-docs', 'Images'));

  for (const sample of fs.readdirSync(samplesDir, { withFileTypes: true })) {
    if (!sample.isDirectory()) continue;
    const readme = path.join(samplesDir, sample.name, `README.${locale}.md`);
    if (!fs.existsSync(readme)) continue;
    copy(readme, path.join(i18nDir, locale, 'docusaurus-plugin-content-docs-tutorials', 'current', sample.name, 'README.md'));
  }

  const overview = path.join(samplesDir, `index.${locale}.mdx`);
  if (fs.existsSync(overview)) copy(overview, path.join(i18nDir, locale, 'docusaurus-plugin-content-docs-tutorials', 'current', 'index.mdx'));

  const changelog = path.join(repoDir, `CHANGELOG.${locale}.md`);
  // scripts/sync-changelog.mjs writes the page's front matter and date itself.
  if (fs.existsSync(changelog)) copy(changelog, path.join(i18nDir, locale, 'CHANGELOG.md'), { stamp: false });
}

console.log(`[sync-i18n] locales: ${locales.join(', ') || 'none'}`);
