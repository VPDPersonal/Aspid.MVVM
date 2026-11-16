import { cpSync, mkdirSync, readFileSync, writeFileSync, existsSync, rmSync } from 'node:fs';
import { resolve } from 'node:path';
import { fileURLToPath } from 'node:url';

const root = fileURLToPath(new URL('..', import.meta.url));
const [destination, profile = 'minimal', reuse] = process.argv.slice(2);
const modules = ['ai', 'animation', 'audio', 'particlesystem', 'video', 'physics', 'physics2d', 'ui', 'uielements', 'imgui'];
const profiles = ['minimal', 'all', 'counter', 'ugui', 'without-fasttools', 'collections', ...modules];
if (!destination || !profiles.includes(profile)) throw new Error(`Usage: node scripts/asp-make-dependency-profile.mjs <new directory> <${profiles.join('|')}>`);
const output = resolve(destination);
if (existsSync(output) && (reuse !== "--reuse" || !existsSync(resolve(output, "profile.json"))))
  throw new Error(`Destination already exists and is not an opted-in dependency profile: ${output}`);
const dev = resolve(root, 'Aspid.MVVM');
const manifest = JSON.parse(readFileSync(resolve(dev, 'Packages/manifest.json'))).dependencies;
const lock = JSON.parse(readFileSync(resolve(dev, 'Packages/packages-lock.json'))).dependencies;
const dependencies = {};
const allIntegrations = profile === 'all' || profile === 'without-fasttools';
for (const module of modules)
  if (profile === module || allIntegrations) dependencies[`com.unity.modules.${module}`] = '1.0.0';
if (profile === 'ugui' || profile === 'counter' || allIntegrations) dependencies['com.unity.ugui'] = '2.0.0';
if (profile === 'counter' || allIntegrations) dependencies['com.unity.inputsystem'] = manifest['com.unity.inputsystem'];
for (const name of ['fasttools', 'collections']) {
  if (name === 'fasttools' ? profile === 'without-fasttools' : profile !== name && !allIntegrations) continue;
  const id = `tech.aspid.${name}`;
  dependencies[id] = `${manifest[id].split('#')[0]}#${lock[id].hash}`;
}
for (const name of ['Assets/Editor', 'Packages', 'ProjectSettings']) mkdirSync(resolve(output, name), { recursive: true });
cpSync(resolve(dev, 'Packages/tech.aspid.mvvm'), resolve(output, 'Packages/tech.aspid.mvvm'), { recursive: true });
writeFileSync(resolve(output, 'ProjectSettings/ProjectVersion.txt'), process.env.UNITY_VERSION
  ? `m_EditorVersion: ${process.env.UNITY_VERSION}\n`
  : readFileSync(resolve(dev, 'ProjectSettings/ProjectVersion.txt')));
writeFileSync(resolve(output, 'Packages/manifest.json'), JSON.stringify({ dependencies }, null, 2) + '\n');
cpSync(resolve(root, 'scripts/dependency-profile/AspDependencyProfile.cs'), resolve(output, 'Assets/Editor/AspDependencyProfile.cs'));
const smoke = profile === 'without-fasttools' ? 'AspMissingFastToolsSmoke' : 'AspDependencySmoke';
for (const name of ['AspDependencySmoke', 'AspMissingFastToolsSmoke'])
  rmSync(resolve(output, `Assets/${name}.cs`), { force: true });
cpSync(resolve(root, `scripts/dependency-profile/${smoke}.cs`), resolve(output, `Assets/${smoke}.cs`));
if (reuse === '--reuse' && profile !== 'counter' && profile !== 'all') {
  rmSync(resolve(output, 'Assets/Samples/Counter'), { recursive: true, force: true });
  rmSync(resolve(output, 'Assets/TextMesh Pro'), { recursive: true, force: true });
}
if (profile === 'counter' || allIntegrations) {
  cpSync(resolve(dev, 'Packages/tech.aspid.mvvm/Samples~/01. Counter'), resolve(output, 'Assets/Samples/Counter'), { recursive: true });
  for (const folder of ['Fonts', 'Resources', 'Shaders'])
    cpSync(resolve(dev, `Assets/TextMesh Pro/${folder}`), resolve(output, `Assets/TextMesh Pro/${folder}`), { recursive: true });
}
writeFileSync(resolve(output, 'profile.json'), JSON.stringify({ profile, dependencies }, null, 2) + '\n');
console.log(output);
