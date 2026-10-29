// Check that every hand-written copy of the minimum Unity version agrees with package.json, where it is split into
// "unity" (6000.0) and "unityRelease" (0f1). A raised minimum that misses one place leaves an agent, a reader or the CI
// matrix on the old version. ci.yml runs it:
//   node scripts/check-unity-minimum.mjs
// The prose names the minimum line (6000.0). The CI matrix must test that line: the first version of UNITY_VERSIONS
// (the list the Unity plan job turns into the matrix) is a release of it, not older than package.json. It may be newer:
// the git dependencies set their own minimum (Aspid.FastTools needs 6000.0.53f1), and GameCI has no image for 0f1.
import { readFileSync } from 'node:fs';
import { fileURLToPath } from 'node:url';

process.chdir(fileURLToPath(new URL('..', import.meta.url)));

const PACKAGE_JSON = 'Aspid.MVVM/Packages/tech.aspid.mvvm/package.json';
// Files that name the minimum in prose. The CHANGELOGs are not here: their old sections keep the old minimum. Nor are the
// older migration guides: only the newest one (and its translation) names the current minimum.
export const MENTIONS = [
  'Docs/migration/1.1.md',
  'Docs/i18n/ru/docusaurus-plugin-content-docs-migration/current/1.1.md',
  '.claude/skills/aspid-code-style/SKILL.md',
];
const MATRIX = '.github/workflows/ci.yml';

const RELEASE = /^(\d+)([abfp])(\d+)$/;
// a < b < f < p within a patch, as Unity orders its releases.
const releaseKey = (release) => {
  const [, patch, stage, build] = RELEASE.exec(release);
  return [Number(patch), 'abfp'.indexOf(stage), Number(build)];
};
const compare = (a, b) => {
  const [x, y] = [releaseKey(a), releaseKey(b)];
  return x[0] - y[0] || x[1] - y[1] || x[2] - y[2];
};

const { unity, unityRelease = '0f1' } = JSON.parse(readFileSync(PACKAGE_JSON, 'utf8'));
if (!unity || !RELEASE.test(unityRelease)) {
  console.log(`::error file=${PACKAGE_JSON}::no "unity" field, or an "unityRelease" that is not like 53f1`);
  process.exit(1);
}

const escape = (text) => text.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
let errors = 0;
const fail = (file, message) => {
  console.log(`::error file=${file}::${message}; ${PACKAGE_JSON} says ${unity}.${unityRelease}. Update the file by hand.`);
  errors++;
};

for (const file of MENTIONS) {
  if (!new RegExp(`(^|[^0-9.])${escape(unity)}([^0-9]|$)`).test(readFileSync(file, 'utf8'))) {
    fail(file, `has no mention of the minimum Unity ${unity}`);
  }
}

const first = /^\s+UNITY_VERSIONS: (\S+)/m.exec(readFileSync(MATRIX, 'utf8'))?.[1];
if (!first) {
  fail(MATRIX, 'has no "UNITY_VERSIONS: …" line');
} else if (!first.startsWith(`${unity}.`) || !RELEASE.test(first.slice(unity.length + 1))) {
  fail(MATRIX, `starts UNITY_VERSIONS with ${first}, not with a ${unity} release`);
} else if (compare(first.slice(unity.length + 1), unityRelease) < 0) {
  fail(MATRIX, `starts UNITY_VERSIONS with ${first}, older than the minimum`);
}

if (errors) process.exit(1);
console.log(`The minimum Unity ${unity} is the same in ${MENTIONS.length} files, and CI tests it on ${first}`);
