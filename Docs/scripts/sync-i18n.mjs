/**
 * Copies the root translated changelogs into the layout scripts/sync-changelog.mjs reads:
 *
 *   CHANGELOG.<locale>.md (repo root) → i18n/<locale>/CHANGELOG.md
 *
 * The root files stay where GitHub and the package's changelog link expect them; the copy is a build artifact and is
 * gitignored. The pages themselves are tracked in place: English in `docs/` and `tutorials/`, translations in
 * `i18n/<locale>/docusaurus-plugin-content-docs[-tutorials]/current/`. Runs before `start` and `build`.
 */
import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const siteDir = path.dirname(path.dirname(fileURLToPath(import.meta.url)));
const repoDir = path.resolve(siteDir, '..');
const i18nDir = path.join(siteDir, 'i18n');

const locales = fs
  .readdirSync(repoDir)
  .map((file) => file.match(/^CHANGELOG\.([a-z]{2}(?:-[A-Za-z]{2,4})?)\.md$/)?.[1])
  .filter(Boolean);

for (const locale of locales) {
  const destination = path.join(i18nDir, locale, 'CHANGELOG.md');
  fs.mkdirSync(path.dirname(destination), { recursive: true });
  fs.copyFileSync(path.join(repoDir, `CHANGELOG.${locale}.md`), destination);
}

console.log(`[sync-i18n] changelogs: ${locales.join(', ') || 'none'}`);
