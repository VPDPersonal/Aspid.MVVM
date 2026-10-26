// Check that the install URLs of the README follow the package.json version: a stable version installs from `upm`, a
// prerelease from `upm-preview`, and the pinned URL names the version's tag (`upm/<version>`, the tag release.yml
// creates). The version is matched whole, so 1.1.0 does not pass on a URL that still says 1.1.0-beta.1.
// ci.yml runs it:
//   node scripts/check-version.mjs
import { readFileSync } from 'node:fs';
import { fileURLToPath } from 'node:url';

process.chdir(fileURLToPath(new URL('..', import.meta.url)));

const PACKAGE_JSON = 'Aspid.MVVM/Packages/tech.aspid.mvvm/package.json';
const FILES = ['README.md'];

const version = JSON.parse(readFileSync(PACKAGE_JSON, 'utf8')).version ?? '';
const branch = version.includes('-') ? 'upm-preview' : 'upm';

let errors = 0;
const fail = (file, message) => {
  console.log(`::error file=${file}::${message}`);
  errors++;
};

for (const file of FILES) {
  const refs = [...readFileSync(file, 'utf8').matchAll(/Aspid\.MVVM\.git#([^\s`)"'<]+)/g)].map((match) => match[1]);
  const channels = refs.filter((ref) => !ref.startsWith('upm/'));
  const pins = refs.filter((ref) => ref.startsWith('upm/'));
  if (!channels.length || channels.some((ref) => ref !== branch)) {
    fail(file, `the install URLs must use #${branch} for ${version}, not #${channels.join(', #') || '(none)'}`);
  }
  if (!pins.length || pins.some((ref) => ref !== `upm/${version}`)) {
    fail(file, `the pinned install URL must use #upm/${version}, not #${pins.join(', #') || '(none)'}`);
  }
}

if (errors) process.exit(1);
console.log(`The install URLs follow ${version} (#${branch}, #upm/${version})`);
