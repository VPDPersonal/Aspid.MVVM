/**
 * Builds Docs/i18n from the translations that live inside the UPM package, so the package stays the
 * single source of truth and contributors never touch Docusaurus' plugin-named folders by hand.
 *
 *   Documentation/<locale>/**            → i18n/<locale>/docusaurus-plugin-content-docs/current/**
 *   Samples~/<Sample>/README.<locale>.md → i18n/<locale>/docusaurus-plugin-content-docs-tutorials/current/<Sample>/README.md
 *
 * Files are copied, not symlinked: webpack resolves symlinks to their real path, which breaks the
 * relative Markdown links inside a translation. `Docs/i18n` is a build artifact and is gitignored.
 * Runs before `start` and `build`.
 */
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

fs.rmSync(i18nDir, { recursive: true, force: true });

function copy(source, destination) {
  fs.mkdirSync(path.dirname(destination), { recursive: true });
  fs.cpSync(source, destination, { recursive: true, filter: (file) => !file.endsWith('.meta') });
}

for (const locale of locales) {
  copy(path.join(docsDir, locale), path.join(i18nDir, locale, 'docusaurus-plugin-content-docs', 'current'));

  for (const sample of fs.readdirSync(samplesDir, { withFileTypes: true })) {
    if (!sample.isDirectory()) continue;
    const readme = path.join(samplesDir, sample.name, `README.${locale}.md`);
    if (!fs.existsSync(readme)) continue;
    copy(readme, path.join(i18nDir, locale, 'docusaurus-plugin-content-docs-tutorials', 'current', sample.name, 'README.md'));
  }
}

console.log(`[sync-i18n] locales: ${locales.join(', ') || 'none'}`);
