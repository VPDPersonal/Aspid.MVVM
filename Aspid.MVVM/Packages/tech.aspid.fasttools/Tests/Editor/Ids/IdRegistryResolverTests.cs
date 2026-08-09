using System;
using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.FastTools.Ids.Editors;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aspid.FastTools.Ids.Tests
{
    // Two distinct generator-backed IId struct types so the binding tests can prove per-type isolation and retargeting.
    internal partial struct ResolverTestIdA : IId { }
    internal partial struct ResolverTestIdB : IId { }

    /// <summary>
    /// Locks the <see cref="IdRegistryResolver"/> binding invariants (see <c>Ids/CLAUDE.md</c>): <c>Find</c> is the
    /// only sanctioned registry lookup, it indexes assets by the struct's assembly-qualified name stored in
    /// <c>_targetStructType</c>, and the one-registry-per-type rule is enforced at lookup time — a second asset bound
    /// to the same struct logs an error instead of silently shadowing the first. The tests drive real <c>.asset</c>
    /// files through AssetDatabase because the resolver's cache is patched by an AssetPostprocessor on import.
    /// </summary>
    [TestFixture]
    internal sealed class IdRegistryResolverTests
    {
        private readonly List<string> _assetPaths = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var path in _assetPaths)
                AssetDatabase.DeleteAsset(path);
            _assetPaths.Clear();

            // The resolver cache is process-wide static state; reset it so no binding leaks into other fixtures.
            IdRegistryResolver.ClearCache();
        }

        private IdRegistry Track(IdRegistry registry)
        {
            _assetPaths.Add(AssetDatabase.GetAssetPath(registry));
            return registry;
        }

        // A second asset bound to the same AQN, created the way a user duplicating an asset would end up with one —
        // bypassing Create() so the resolver only discovers it on the next cache rebuild.
        private string CreateDuplicateBoundTo(Type structType)
        {
            var path = AssetDatabase.GenerateUniqueAssetPath($"Assets/IdRegistry_{structType.Name}_dup.asset");
            var registry = ScriptableObject.CreateInstance<IdRegistry>();
            AssetDatabase.CreateAsset(registry, path);

            var serialized = new SerializedObject(registry);
            serialized.FindProperty("_targetStructType").stringValue = structType.AssemblyQualifiedName;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            AssetDatabase.SaveAssets();

            _assetPaths.Add(path);
            return path;
        }

        [Test]
        public void Create_BindsTheStructAqn_FindReturnsThatAsset()
        {
            var registry = Track(IdRegistryResolver.Create(typeof(ResolverTestIdA)));

            Assert.AreSame(registry, IdRegistryResolver.Find(typeof(ResolverTestIdA)),
                "Find must resolve the struct type to the asset Create just bound.");
        }

        [Test]
        public void Find_TypeWithoutRegistry_ReturnsNull_AndNullTypeIsSafe()
        {
            Assert.IsNull(IdRegistryResolver.Find(typeof(ResolverTestIdB)),
                "A struct type no asset is bound to has no registry.");
            Assert.IsNull(IdRegistryResolver.Find(null));
        }

        [Test]
        public void GetOrCreate_ReusesTheExistingBinding()
        {
            var created = Track(IdRegistryResolver.GetOrCreate(typeof(ResolverTestIdA)));
            var again = IdRegistryResolver.GetOrCreate(typeof(ResolverTestIdA));

            Assert.AreSame(created, again, "GetOrCreate must never create a second asset for an already-bound type.");
        }

        [Test]
        public void Find_TwoRegistriesBoundToOneType_LogsTheDuplicateError_StillReturnsARegistry()
        {
            var duplicateError = new Regex("Multiple registries found for type AQN=");
            Track(IdRegistryResolver.Create(typeof(ResolverTestIdA)));

            // The rule is enforced at two points, each logging once: the import of the second asset hits the warm
            // cache (the AssetPostprocessor routes it through OnAssetImported)...
            LogAssert.Expect(LogType.Error, duplicateError);
            CreateDuplicateBoundTo(typeof(ResolverTestIdA));

            // ...and the full rescan a fresh domain / editor session performs with both assets already on disk.
            LogAssert.Expect(LogType.Error, duplicateError);
            IdRegistryResolver.ClearCache();

            Assert.IsNotNull(IdRegistryResolver.Find(typeof(ResolverTestIdA)),
                "The one-per-type violation is reported, but lookup still degrades to a single winner.");
        }

        [Test]
        public void OnAssetImported_RetargetedAsset_MovesTheBindingToTheNewType()
        {
            // Rebinding an existing asset to another struct (via its Type field) must atomically move the cache
            // entry: the new type resolves, the old one no longer does.
            var registry = Track(IdRegistryResolver.Create(typeof(ResolverTestIdA)));
            var path = AssetDatabase.GetAssetPath(registry);

            var serialized = new SerializedObject(registry);
            serialized.FindProperty("_targetStructType").stringValue = typeof(ResolverTestIdB).AssemblyQualifiedName;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            IdRegistryResolver.OnAssetImported(path);

            Assert.AreSame(registry, IdRegistryResolver.Find(typeof(ResolverTestIdB)),
                "The retargeted asset must resolve for its new struct type.");
            Assert.IsNull(IdRegistryResolver.Find(typeof(ResolverTestIdA)),
                "The old binding must be removed in the same import pass.");
        }
    }
}
