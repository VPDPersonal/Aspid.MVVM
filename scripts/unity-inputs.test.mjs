// Tests of scripts/unity-inputs.mjs: which files and which part of ci.yml change the digest. ci.yml runs it:
//   node --test scripts/*.test.mjs
import assert from 'node:assert/strict';
import { test } from 'node:test';
import { readFileSync } from 'node:fs';
import { fileURLToPath } from 'node:url';
import { DEV_PACKAGES, PACKAGE, TMP_ESSENTIALS, digest, inputs, isIgnored, workflowInputs } from './unity-inputs.mjs';

const WORKFLOW = `name: CI
# A comment above the jobs.
on:
  pull_request:
permissions:
  contents: read

jobs:
  checks:
    runs-on: ubuntu-latest
    steps:
      - run: echo checks

  unity-plan:
    steps:
      - id: plan
        env:
          # The versions.
          UNITY_VERSIONS: 6000.0.53f1 6000.6.5f1
        run: echo plan

  # A comment above the job.
  unity-tests:
    runs-on: ubuntu-latest
    steps:
      # A comment in the job.
      - env: &account
          EMAIL: email
        run: &pass |
          echo pass
      - env: *account
        run: *pass

  # A comment above the next job.
  unity:
    needs: [unity-plan, unity-tests]
    steps:
      - run: echo unity
`;

const replace = (text, from, to) => {
  assert.ok(text.includes(from), `the fixture has no "${from}"`);
  return text.replace(from, to);
};

const TMP_SETTINGS = 'Aspid.MVVM/Assets/TextMesh Pro/Resources/TMP Settings.asset';
const FILES = [
  ['100644', 'a1', `${PACKAGE}/package.json`],
  ['100644', 'a2', `${PACKAGE}/Source/Item.cs`],
  ['100644', 'a3', `${PACKAGE}/Documentation/01-getting-started.md`],
  ['100644', 'a4', DEV_PACKAGES[0]],
  ['100644', 'a5', DEV_PACKAGES[1]],
  ['100644', 'a6', TMP_SETTINGS],
  ['100755', 'a7', 'scripts/make-unity-test-project.sh'],
];

// A fake `git`: ls-tree lists the files, show prints the workflow.
const reader = (files = FILES, workflow = WORKFLOW) => (command) =>
  command === 'ls-tree' ? files.map(([mode, id, path]) => `${mode} blob ${id}\t${path}\0`).join('') : workflow;

const digestOf = (files, workflow) => digest(inputs('HEAD', reader(files, workflow)));

test('Markdown, the license and the Documentation folder are ignored with their .meta', () => {
  for (const path of [
    `${PACKAGE}/README.md`,
    `${PACKAGE}/README.md.meta`,
    `${PACKAGE}/Samples~/01. Counter/README.md`,
    `${PACKAGE}/Samples~/01. Counter/README.ru.md`,
    `${PACKAGE}/LICENSE.md`,
    `${PACKAGE}/LICENSE.md.meta`,
    `${PACKAGE}/Documentation/01-getting-started.md`,
    `${PACKAGE}/Documentation/Images/diagram.png`,
    `${PACKAGE}/Documentation/Images/diagram.png.meta`,
    `${PACKAGE}/Documentation.meta`,
  ]) {
    assert.ok(isIgnored(path), path);
  }
});

test('code, assets, the DLLs and the dev project files that the test project reads are inputs', () => {
  for (const path of [
    `${PACKAGE}/package.json`,
    `${PACKAGE}/Source/Item.cs`,
    `${PACKAGE}/Source/Item.cs.meta`,
    `${PACKAGE}/Source/Aspid.MVVM.asmdef`,
    `${PACKAGE}/Aspid.MVVM.Generators.dll`,
    `${PACKAGE}/Unity/Aspid.MVVM.Unity.Generators.dll.meta`,
    `${PACKAGE}/Unity/Editor/Resources/Styles/Aspid-MVVM-MonoBinder.uss`,
    `${PACKAGE}/Samples~/01. Counter/Scenes/Counter.unity`,
    `${PACKAGE}/Tests/EditMode/Aspid.MVVM.Tests.EditMode.asmdef`,
    `${PACKAGE}/DocumentationTools/Page.cs`,
    ...DEV_PACKAGES,
    TMP_SETTINGS,
  ]) {
    assert.ok(!isIgnored(path), path);
  }
});

test('the TMP Essential Resources are the three folders and their .meta', () => {
  assert.equal(TMP_ESSENTIALS.length, 7);
  assert.ok(TMP_ESSENTIALS.every((path) => path.startsWith('Aspid.MVVM/Assets/TextMesh Pro')));
  assert.ok(!TMP_ESSENTIALS.some((path) => /Examples|Documentation/.test(path)));
});

test('an ignored file does not change the digest, an input does', () => {
  const base = digestOf();
  assert.equal(digestOf([...FILES, ['100644', 'b1', `${PACKAGE}/Documentation/Images/diagram.png`]]), base);
  assert.equal(digestOf(FILES.map(([mode, id, path]) => [mode, path.endsWith('.md') ? 'b2' : id, path])), base);
  assert.notEqual(digestOf([...FILES, ['100644', 'b3', `${PACKAGE}/Source/Other.cs`]]), base);
  assert.notEqual(digestOf(FILES.map(([mode, id, path]) => [mode, path === DEV_PACKAGES[1] ? 'b5' : id, path])), base);
  assert.notEqual(digestOf(FILES.map(([mode, id, path]) => [mode, path === TMP_SETTINGS ? 'b6' : id, path])), base);
  assert.notEqual(digestOf(FILES.map(([mode, id, path]) => [mode, path.endsWith('.cs') ? 'b4' : id, path])), base);
  assert.notEqual(digestOf(FILES.map(([mode, id, path]) => [path.endsWith('.cs') ? '100755' : mode, id, path])), base);
  assert.notEqual(digestOf(FILES.filter(([, , path]) => !path.endsWith('.cs'))), base);
});

test('comments, blank lines and the other jobs of ci.yml do not change the digest', () => {
  const base = digestOf();
  for (const [from, to] of [
    ['# A comment above the jobs.', '# Another comment.'],
    ['      # A comment in the job.\n', ''],
    ['  # A comment above the next job.', '  # Another comment.'],
    ['echo checks', 'echo other checks'],
    ['echo plan', 'echo other plan'],
    ['echo unity', 'echo other unity'],
    ['    runs-on: ubuntu-latest\n    steps:\n      # A', '    runs-on: ubuntu-latest\n\n    steps:\n      # A'],
  ]) {
    assert.equal(digestOf(FILES, replace(WORKFLOW, from, to)), base, `${from} -> ${to}`);
  }
});

test('the unity-tests job, the part above the jobs and UNITY_VERSIONS change the digest', () => {
  const base = digestOf();
  for (const [from, to] of [
    ['echo pass', 'echo other pass'],
    ['EMAIL: email', 'EMAIL: other'],
    ['  contents: read', '  contents: write'],
    ['6000.0.53f1 6000.6.5f1', '6000.0.53f1 6000.7.1f1'],
  ]) {
    assert.notEqual(digestOf(FILES, replace(WORKFLOW, from, to)), base, `${from} -> ${to}`);
  }
});

test('the job ends at the next job, not at a comment', () => {
  const job = workflowInputs(WORKFLOW);
  assert.match(job, /echo pass/);
  assert.match(job, /run: \*pass/);
  assert.doesNotMatch(job, /echo unity/);
  assert.doesNotMatch(job, /echo checks/);
});

test('an alias to an anchor outside the job fails', () => {
  const outside = replace(replace(WORKFLOW, 'EMAIL: email', 'EMAIL: *email'), 'echo checks', '&email echo checks');
  assert.throws(() => workflowInputs(outside), /anchor "email"/);
  assert.throws(() => workflowInputs(replace(WORKFLOW, 'run: *pass', 'run: *checks')), /anchor "checks"/);
});

test('a workflow without the job or with a second UNITY_VERSIONS fails', () => {
  assert.throws(() => workflowInputs(replace(WORKFLOW, '  unity-tests:', '  unity-other:')), /"unity-tests:" job/);
  assert.throws(() => workflowInputs(replace(WORKFLOW, 'echo pass', 'UNITY_VERSIONS: 6000.0.53f1')), /2 UNITY_VERSIONS/);
});

test('a commit without the package, the dev manifest or the script fails instead of giving a digest', () => {
  assert.throws(() => digestOf(FILES.filter(([, , path]) => !path.endsWith('package.json'))), /package\.json/);
  assert.throws(() => digestOf(FILES.filter(([, , path]) => path !== DEV_PACKAGES[0])), /manifest\.json/);
  assert.throws(() => digestOf(FILES.filter(([, , path]) => !path.startsWith('scripts/'))), /make-unity-test-project/);
  assert.throws(() => digestOf([]), /package\.json/);
});

test('the ci.yml of the repository parses', () => {
  const text = readFileSync(fileURLToPath(new URL('../.github/workflows/ci.yml', import.meta.url)), 'utf8');
  const job = workflowInputs(text);
  assert.match(job, /^ {2}unity-tests:$/m);
  assert.match(job, /^\s+UNITY_VERSIONS: /m);
  assert.match(job, /Return the Unity seat/);
  assert.doesNotMatch(job, /^ {2}unity:$/m);
});

test('the inputs of this commit list the package, the dev manifest and the TMP settings', () => {
  const text = inputs('HEAD');
  assert.match(text, /^unity-inputs 1$/m);
  assert.match(text, new RegExp(` ${PACKAGE}/package\\.json$`, 'm'));
  assert.match(text, / Aspid\.MVVM\/Packages\/packages-lock\.json$/m);
  assert.match(text, / Aspid\.MVVM\/Assets\/TextMesh Pro\/Resources\/TMP Settings\.asset$/m);
  assert.doesNotMatch(text, /\/Documentation\//);
});
