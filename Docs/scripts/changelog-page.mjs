/**
 * The text transforms of `sync-changelog.mjs`, split out so that `changelog-page.test.mjs` can run them on small inputs.
 */

// `## [1.0.0] — 2026-01-01` → anchor `#v1-0-0`, so the generated sidebar can link every version in every locale.
const versionHeading = /^## \[([^\]]+)\](.*)$/gm;
export const versionAnchor = (version) => `v${version.toLowerCase().replace(/[^a-z0-9]+/g, '-')}`;

/**
 * The changelog is served at /changelog. The language-switch line at its top (`> Русская версия: …`)
 * exists for GitHub readers; the site has a locale dropdown, so it is dropped. A translation's H1 carries a
 * language suffix for GitHub (`# Changelog (RU)`); on the site it takes the navbar's translated label instead.
 */
export function changelogBody(markdown, title) {
  return markdown
    .replace(/^> .*CHANGELOG(?:\.[a-z]{2})?\.md.*\n\n/m, '')
    .replace(/^# (.+?)(?: \([A-Z]{2}\))?$/m, (line, heading) => `# ${title ?? heading}`)
    .replace(versionHeading, (line, version) => `${line} {#${versionAnchor(version)}}`);
}

/**
 * A relative link to another repository file (`[MIGRATION.md](MIGRATION.md)`) works on GitHub only; on the site it
 * opens the file on GitHub. Links to the other changelog are left alone: `changelogBody` drops their line.
 */
export function repositoryLinks(markdown, blobUrl) {
  return markdown.replace(/\]\((?![a-z][a-z\d+.-]*:|#|\/)([^)\s]+)\)/gi, (link, target) =>
    /^CHANGELOG(?:\.[a-z]{2})?\.md$/.test(target) ? link : `](${blobUrl}${target})`);
}

/** The page file: front matter with the date of the source's last commit (when git has one) and the body. */
export function changelogPage(markdown, title, date) {
  return `---\nslug: /\ndisplayed_sidebar: changelog\n${date ? `last_update:\n  date: ${date}\n` : ''}---\n\n${changelogBody(markdown, title)}`;
}

/** The changelog is a single page; its sidebar lists the versions so the left panel is never empty. */
export function changelogSidebars(markdown) {
  const versions = [...markdown.matchAll(versionHeading)].map(([, version]) => ({
    type: 'link', label: version, href: `/changelog#${versionAnchor(version)}`,
  }));
  return { changelog: [{ type: 'category', label: 'Versions', className: 'doc-menu-group', collapsible: false, items: versions }] };
}
