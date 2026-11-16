import fs from 'node:fs';
import path from 'node:path';
import crypto from 'node:crypto';
import { fileURLToPath } from 'node:url';

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '../..');
const manifestPath = path.join(root, 'Docs/docfx/api-manifest.json');
const packagePath = 'Aspid.MVVM/Packages/tech.aspid.mvvm';

export function digestFiles(directory, select) {
  const entries = {};
  function walk(relative) {
    for (const entry of fs.readdirSync(path.join(directory, relative), { withFileTypes: true })) {
      const name = path.posix.join(relative, entry.name);
      if (entry.isDirectory()) walk(name);
      else if (select(name)) entries[name] = crypto.createHash('sha256').update(fs.readFileSync(path.join(directory, name))).digest('hex');
    }
  }
  walk('');
  return Object.fromEntries(Object.entries(entries).sort(([a], [b]) => a.localeCompare(b, 'en')));
}

export function apiSnapshot() {
  const inputs = {};
  for (const relative of [`${packagePath}/Source`, `${packagePath}/Unity/Runtime`, `${packagePath}/StarterKit/Runtime`]) {
    for (const [file, hash] of Object.entries(digestFiles(path.join(root, relative), name => name.endsWith('.cs') || name.endsWith('.asmdef'))))
      inputs[`${relative}/${file}`] = hash;
  }
  const paths = [
    `${packagePath}/Aspid.MVVM.Generators.dll`, `${packagePath}/Unity/Aspid.MVVM.Unity.Generators.dll`,
    'Directory.Build.props', 'global.json', 'Docs/docfx/docfx.json', 'Docs/scripts/docfx-projects.mjs', 'Docs/scripts/docfx-postprocess.mjs',
    'Docs/scripts/api-freshness.mjs', 'Docs/scripts/generate-api.mjs',
    'Aspid.MVVM/Packages/manifest.json', 'Aspid.MVVM/Packages/packages-lock.json',
  ];
  for (const file of paths)
    inputs[file] = crypto.createHash('sha256').update(fs.readFileSync(path.join(root, file))).digest('hex');
  return {
    schema: 1,
    inputs: Object.fromEntries(Object.entries(inputs).sort(([a], [b]) => a.localeCompare(b, 'en'))),
    outputs: digestFiles(path.join(root, 'Docs/api'), () => true),
  };
}

export function checkApiContracts() {
  const read = name => fs.readFileSync(path.join(root, 'Docs/api', name), 'utf8');
  const oneTime = read('Aspid.MVVM.OneTimeBindableMember-1.md');
  if (!oneTime.includes('public OneTimeBindableMember(T? value)') || !oneTime.includes('public T? Value { get; set; }')
      || /public static .*\bGet\(/.test(oneTime)) throw new Error('OneTimeBindableMember API contract is stale');
  if (!read('Aspid.MVVM.StarterKit.SliderValueBinder.md').includes('NotifyMode notifyMode'))
    throw new Error('SliderValueBinder NotifyMode is missing');
  const dynamic = read('Aspid.MVVM.StarterKit.DynamicViewModel.md');
  if (!dynamic.includes('AddEnum<T>') || !dynamic.includes('AddStruct<T>'))
    throw new Error('DynamicViewModel specialized factories are missing');
}

export function recordApiSnapshot() {
  checkApiContracts();
  fs.writeFileSync(manifestPath, JSON.stringify(apiSnapshot(), null, 2) + '\n');
}

if (process.argv[1] && path.resolve(process.argv[1]) === fileURLToPath(import.meta.url)) {
  const expected = JSON.parse(fs.readFileSync(manifestPath, 'utf8'));
  if (JSON.stringify(expected) !== JSON.stringify(apiSnapshot()))
    throw new Error('API inputs or generated pages changed. Recompile Unity and run npm run api.');
  checkApiContracts();
  console.log('API inputs, generated pages, and signature checks match.');
}
