/**
 * Turns the legacy-format .csproj files Unity generates into SDK-style projects that DocFX can load.
 *
 * MSBuild from the .NET SDK does not evaluate Unity's `ToolsVersion="4.0"` projects properly (defines and
 * nullable settings are lost), so the API reference cannot be built from them directly. This script copies the
 * compile items, assembly references and compiler settings of the runtime assemblies into
 * `Docs/docfx/projects/<Assembly>.csproj`. Unity project references become references to the compiled
 * assemblies in `Library/ScriptAssemblies`, except the ones we document, which stay project references.
 *
 * The output contains absolute paths into the local Unity installation and is gitignored; run it (and the Unity
 * Editor, so `Library/ScriptAssemblies` is fresh) before `docfx metadata`.
 */
import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const siteDir = path.dirname(path.dirname(fileURLToPath(import.meta.url)));
const unityDir = path.resolve(siteDir, '../Aspid.MVVM');
const outDir = path.join(siteDir, 'docfx', 'projects');

/** Assemblies that get an API reference page tree. */
const ASSEMBLIES = ['Aspid.MVVM', 'Aspid.MVVM.Unity', 'Aspid.MVVM.StarterKit'];

function attr(xml, tag, name) {
  return [...xml.matchAll(new RegExp(`<${tag}\\s+${name}="([^"]*)"`, 'g'))].map((m) => m[1]);
}

function element(xml, tag) {
  return xml.match(new RegExp(`<${tag}>([^<]*)</${tag}>`))?.[1] ?? '';
}

function hintPaths(xml) {
  return [...xml.matchAll(/<Reference Include="[^"]*">\s*<HintPath>([^<]*)<\/HintPath>/g)].map((m) => m[1]);
}

fs.rmSync(outDir, { recursive: true, force: true });
fs.mkdirSync(outDir, { recursive: true });

for (const assembly of ASSEMBLIES) {
  const xml = fs.readFileSync(path.join(unityDir, `${assembly}.csproj`), 'utf8');

  const compile = attr(xml, 'Compile', 'Include').map((file) => path.join(unityDir, file));
  const references = hintPaths(xml).map((hint) => (path.isAbsolute(hint) ? hint : path.join(unityDir, hint)));
  const projectRefs = attr(xml, 'ProjectReference', 'Include').map((file) => path.basename(file, '.csproj'));
  // Source generators complete the partial types ([ViewModel], [View], binders); without them the code does not compile.
  const analyzers = attr(xml, 'Analyzer', 'Include').filter((file) => /Generators?\.dll$|SourceGenerators?\.dll$/.test(file));
  const defines = element(xml, 'DefineConstants');
  const langVersion = element(xml, 'LangVersion') || 'latest';
  const nullable = element(xml, 'Nullable') || 'disable';
  const unsafeBlocks = element(xml, 'AllowUnsafeBlocks') || 'false';

  const items = [
    ...compile.map((file) => `    <Compile Include="${file}" />`),
    ...references.map((file) => `    <Reference Include="${path.basename(file, '.dll')}"><HintPath>${file}</HintPath></Reference>`),
    ...analyzers.map((file) => `    <Analyzer Include="${file}" />`),
    ...projectRefs.map((name) =>
      ASSEMBLIES.includes(name)
        ? `    <ProjectReference Include="${name}.csproj" />`
        : `    <Reference Include="${name}"><HintPath>${path.join(unityDir, 'Library/ScriptAssemblies', `${name}.dll`)}</HintPath></Reference>`,
    ),
  ];

  const project = `<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <AssemblyName>${assembly}</AssemblyName>
    <RootNamespace>${assembly}</RootNamespace>
    <LangVersion>${langVersion}</LangVersion>
    <Nullable>${nullable}</Nullable>
    <AllowUnsafeBlocks>${unsafeBlocks}</AllowUnsafeBlocks>
    <DefineConstants>${defines}</DefineConstants>
    <EnableDefaultItems>false</EnableDefaultItems>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>CS1591;CS1573;CS8632</NoWarn>
    <DisableImplicitFrameworkReferences>false</DisableImplicitFrameworkReferences>
  </PropertyGroup>
  <ItemGroup>
${items.join('\n')}
  </ItemGroup>
</Project>
`;
  fs.writeFileSync(path.join(outDir, `${assembly}.csproj`), project);
  console.log(`[docfx-projects] ${assembly}: ${compile.length} files, ${references.length + projectRefs.length} references, ${analyzers.length} generators`);
}
