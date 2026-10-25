// What the Unity tests of ci.yml read, and a digest of it for a commit. Two commits with the same digest give the Unity
// tests the same input, so the Unity plan job of ci.yml skips the tests when the digest of the commit under test equals
// the one of its base, or when a run of this repository already passed on a commit with this digest.
// The digest covers:
//  - every file of the package (Samples~ included: the test project copies it into Assets/) except IGNORED, by path,
//    mode and blob id;
//  - the manifest and packages-lock.json of the dev project, which give the test project its git packages and their
//    commits, and the TMP Essential Resources of the dev project, which the test project copies into Assets/;
//  - scripts/make-unity-test-project.sh, which builds the test project;
//  - the unity-tests job of ci.yml, the part of the file above `jobs:` (env, permissions, defaults reach every job) and
//    the UNITY_VERSIONS line, without blank and comment lines.
// The Unity tests read nothing else. When the unity-tests job starts to read another file, add it to PATHS.
// Usage, in a clone that has the commits (a partial clone fetches the blobs it reads):
//   node scripts/unity-inputs.mjs <commit>...       one digest per commit, in order
//   node scripts/unity-inputs.mjs --list <commit>   the text that the digest is taken of
import { execFileSync } from 'node:child_process';
import { createHash } from 'node:crypto';
import { fileURLToPath } from 'node:url';

export const PACKAGE = 'Aspid.MVVM/Packages/tech.aspid.mvvm';
export const DEV_PACKAGES = ['Aspid.MVVM/Packages/manifest.json', 'Aspid.MVVM/Packages/packages-lock.json'];
const TMP = 'Aspid.MVVM/Assets/_Assets/TextMesh Pro';
export const TMP_ESSENTIALS = [`${TMP}.meta`, ...['Fonts', 'Resources', 'Shaders', 'Sprites'].flatMap((folder) => [
  `${TMP}/${folder}`,
  `${TMP}/${folder}.meta`,
])];
const SCRIPT = 'scripts/make-unity-test-project.sh';
const PATHS = [PACKAGE, ...DEV_PACKAGES, ...TMP_ESSENTIALS, SCRIPT];
const WORKFLOW = '.github/workflows/ci.yml';
const JOB = 'unity-tests';
// A new format of the digest text must not match a digest of the old one.
const FORMAT = 'unity-inputs 1';

// Package files that Unity imports as plain text or not at all and that no Unity test reads: Markdown (the sample
// tutorials included), the license text and the Documentation folder, which holds the pages of the docs site. With
// their .meta. check-package-files.mjs fails when an asset refers to one of them by its guid or a package file names one
// in quotes; then the file is an input and must leave this list.
export const isIgnored = (path) =>
  /\.md(\.meta)?$/.test(path) ||
  /(^|\/)LICEN[CS]E[^/]*$/.test(path) ||
  path.startsWith(`${PACKAGE}/Documentation/`) ||
  path === `${PACKAGE}/Documentation.meta`;

const isComment = (line) => line.trim() === '' || /^\s*#/.test(line);

// The part of ci.yml that the Unity tests depend on. A YAML alias in the job must point to an anchor in the job: the
// part above would not show a change to an anchor elsewhere.
export function workflowInputs(text) {
  const lines = text.split(/\r?\n/);
  const jobs = lines.findIndex((line) => /^jobs:\s*$/.test(line));
  const start = lines.findIndex((line) => new RegExp(`^  ${JOB}:\\s*$`).test(line));
  if (jobs < 0 || start < jobs) throw new Error(`${WORKFLOW} has no "jobs:" with a "${JOB}:" job`);
  // The job ends at the next job or top-level key. A comment line does not end it.
  let end = start + 1;
  while (end < lines.length && !/^ {0,2}[^\s#]/.test(lines[end])) end++;
  const job = lines.slice(start, end);

  const anchors = new Set(job.flatMap((line) => [...line.matchAll(/(?:^|\s)&([\w-]+)/g)].map((match) => match[1])));
  for (const line of job) {
    const alias = /(?::|-)\s+\*([\w-]+)\s*$/.exec(line)?.[1];
    if (alias && !anchors.has(alias)) throw new Error(`the ${JOB} job uses the anchor "${alias}" from outside the job`);
  }
  const versions = lines.filter((line) => /^\s+UNITY_VERSIONS:/.test(line));
  if (versions.length !== 1) throw new Error(`${WORKFLOW} has ${versions.length} UNITY_VERSIONS lines instead of one`);

  return [...lines.slice(0, jobs), ...job, ...versions].filter((line) => !isComment(line)).join('\n');
}

const git = (...args) => execFileSync('git', args, { encoding: 'utf8', maxBuffer: 64 * 1024 * 1024 });

export function inputs(commit, read = git) {
  // -z: a path with a space or a non-ASCII letter comes unquoted.
  const files = read('ls-tree', '-r', '-z', '--full-tree', commit, '--', ...PATHS)
    .split('\0')
    .filter(Boolean)
    .map((entry) => {
      const [info, path] = entry.split('\t');
      const [mode, , id] = info.split(' ');
      return { mode, id, path };
    })
    .filter(({ path }) => !isIgnored(path));
  // An empty listing (a wrong commit, a moved package) must not match another empty one.
  for (const required of [`${PACKAGE}/package.json`, ...DEV_PACKAGES, SCRIPT]) {
    if (!files.some(({ path }) => path === required)) throw new Error(`${commit} has no ${required}`);
  }
  return [
    FORMAT,
    ...files.map(({ mode, id, path }) => `${mode} ${id} ${path}`),
    workflowInputs(read('show', `${commit}:${WORKFLOW}`)),
  ].join('\n');
}

export const digest = (text) => createHash('sha256').update(text).digest('hex');

if (process.argv[1] === fileURLToPath(import.meta.url)) {
  const args = process.argv.slice(2);
  try {
    if (args[0] === '--list' && args.length === 2) {
      console.log(inputs(args[1]));
    } else if (args.length > 0 && !args[0].startsWith('-')) {
      for (const commit of args) console.log(digest(inputs(commit)));
    } else {
      console.error('usage: node scripts/unity-inputs.mjs <commit>... | --list <commit>');
      process.exit(2);
    }
  } catch (error) {
    console.error(`::error::${error.message}`);
    process.exit(1);
  }
}
