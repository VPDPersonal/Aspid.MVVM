using System.Runtime.CompilerServices;

// The YAML asset-editing engine ships as internal infrastructure: its types are an implementation detail shared with
// the SerializeReference editor feature (currently the sole consumer) and exercised by its own test assembly. Kept
// internal — rather than promoted to a public API surface — until a second consumer justifies it.
[assembly: InternalsVisibleTo("Aspid.FastTools.Unity.Editor")]
[assembly: InternalsVisibleTo("Aspid.FastTools.Unity.Editor.SerializeReferences.Tests")]
[assembly: InternalsVisibleTo("Aspid.FastTools.Unity.Editor.Tests")]
[assembly: InternalsVisibleTo("Aspid.FastTools.DevTests.CliCommands.Editor")]
