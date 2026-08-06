using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using Aspid.FastTools.Ids.Editors;
using System.Collections.Generic;

namespace Aspid.FastTools.Ids.Tests
{
    // A generator-backed IId struct (IdStructGenerator emits _id / Id into the other partial part); the extra
    // constructor lets tests build a value for a specific id.
    internal partial struct TestWeaponId : IId
    {
        public TestWeaponId(int id) : this() => _id = id;
    }

    // IdRegistry<T> is generic, so Unity cannot instantiate it directly — the concrete subclass is the shape users declare.
    internal sealed class TestWeaponIdRegistry : IdRegistry<TestWeaponId> { }

    /// <summary>
    /// Locks the <see cref="IdRegistry"/> lookup contract the drawers and user code rely on: name↔id resolution over
    /// the serialized parallel arrays, the explicit cache-invalidation protocol (<see cref="IdRegistry.InvalidateCache"/>
    /// → <see cref="IdRegistry.EnsureCache"/> — the reason RegistryEditorCore's Commit exists), the documented
    /// last-entry-wins semantics for duplicate names, and order-independence of the mapping (reordering rows must never
    /// change what a stored id resolves to — that is the "stable IDs" promise).
    /// </summary>
    [TestFixture]
    internal sealed class IdRegistryTests
    {
        private readonly List<UnityEngine.Object> _created = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var obj in _created)
                if (obj != null) UnityEngine.Object.DestroyImmediate(obj);
            _created.Clear();
        }

        // Populates the private parallel arrays the way the registry inspector does — through SerializedProperty —
        // and invalidates the cache explicitly, mirroring RegistryEditorCore.Commit.
        private T NewRegistry<T>(params (int id, string name)[] entries) where T : IdRegistry
        {
            var registry = ScriptableObject.CreateInstance<T>();
            _created.Add(registry);

            var serialized = new SerializedObject(registry);
            var ids = serialized.FindProperty("_ids");
            var names = serialized.FindProperty("_names");

            ids.arraySize = entries.Length;
            names.arraySize = entries.Length;

            for (var i = 0; i < entries.Length; i++)
            {
                ids.GetArrayElementAtIndex(i).intValue = entries[i].id;
                names.GetArrayElementAtIndex(i).stringValue = entries[i].name;
            }

            serialized.ApplyModifiedPropertiesWithoutUndo();
            registry.InvalidateCache();
            return registry;
        }

        [Test]
        public void TryGetId_TryGetName_RoundTripBothDirections()
        {
            var registry = NewRegistry<IdRegistry>((1, "Sword"), (2, "Bow"), (7, "Shield"));

            Assert.IsTrue(registry.TryGetId("Bow", out var id));
            Assert.AreEqual(2, id);

            Assert.IsTrue(registry.TryGetName(7, out var name));
            Assert.AreEqual("Shield", name);
        }

        [Test]
        public void TryGetName_UnknownId_ReturnsFalse_WithEmptyName()
        {
            var registry = NewRegistry<IdRegistry>((1, "Sword"));

            Assert.IsFalse(registry.TryGetName(42, out var name));
            Assert.AreEqual(string.Empty, name, "A failed name lookup must yield string.Empty, not null.");
        }

        [Test]
        public void Contains_ChecksBothIdAndName()
        {
            var registry = NewRegistry<IdRegistry>((1, "Sword"), (2, "Bow"));

            Assert.IsTrue(registry.Contains(1));
            Assert.IsTrue(registry.Contains("Bow"));
            Assert.IsFalse(registry.Contains(3));
            Assert.IsFalse(registry.Contains("Axe"));
        }

        [Test]
        public void Reorder_DoesNotChangeWhatAStoredIdResolvesTo()
        {
            // The "stable IDs" promise: a stored int survives any row reordering in the registry asset.
            var original = NewRegistry<IdRegistry>((1, "Sword"), (2, "Bow"), (7, "Shield"));
            var reordered = NewRegistry<IdRegistry>((7, "Shield"), (1, "Sword"), (2, "Bow"));

            foreach (var pair in original)
            {
                Assert.IsTrue(reordered.TryGetName(pair.Key, out var name));
                Assert.AreEqual(pair.Value, name, $"Id {pair.Key} must resolve identically after reordering.");
            }
        }

        [Test]
        public void DuplicateNames_LastEntryWinsForNameLookup_EveryIdStillNamed()
        {
            // Duplicate rows are invalid data the Clean-up flow reports; until cleaned, the cache resolves the name to
            // the LAST row (dictionary overwrite) while both ids keep resolving to the shared name.
            var registry = NewRegistry<IdRegistry>((1, "Sword"), (2, "Sword"));

            Assert.IsTrue(registry.TryGetId("Sword", out var id));
            Assert.AreEqual(2, id, "The last duplicate row wins the name→id lookup.");

            Assert.IsTrue(registry.TryGetName(1, out var first));
            Assert.IsTrue(registry.TryGetName(2, out var second));
            Assert.AreEqual("Sword", first);
            Assert.AreEqual("Sword", second);
        }

        [Test]
        public void WhitespaceName_ExcludedFromNameLookup_IdStillNamed()
        {
            var registry = NewRegistry<IdRegistry>((1, "  "), (2, "Bow"));

            Assert.IsFalse(registry.Contains("  "), "A whitespace name never enters the name→id lookup.");
            Assert.IsTrue(registry.TryGetName(1, out _), "The id itself still resolves (to its raw stored name).");
        }

        [Test]
        public void Enumerator_YieldsPairsForTheShorterArray()
        {
            // Mismatched array lengths are a corrupt-asset shape (the inspector always writes both); enumeration
            // degrades to the shorter length instead of throwing.
            var registry = NewRegistry<IdRegistry>((1, "Sword"), (2, "Bow"));
            var serialized = new SerializedObject(registry);
            serialized.FindProperty("_names").arraySize = 1;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            registry.InvalidateCache();

            var pairs = registry.ToList();

            Assert.AreEqual(1, pairs.Count);
            Assert.AreEqual(new KeyValuePair<int, string>(1, "Sword"), pairs[0]);
        }

        [Test]
        public void CacheProtocol_InvalidateMarksDirty_EnsureCacheClears()
        {
            // RegistryEditorCore.Commit relies on exactly this flag protocol; a lookup transparently rebuilds.
            var registry = NewRegistry<IdRegistry>((1, "Sword"));

            registry.EnsureCache();
            Assert.IsFalse(registry.IsCacheDirty, "EnsureCache must clear the dirty flag.");

            registry.InvalidateCache();
            Assert.IsTrue(registry.IsCacheDirty, "InvalidateCache must mark the cache dirty.");

            Assert.IsTrue(registry.Contains(1), "A lookup on a dirty cache rebuilds it transparently.");
            Assert.IsFalse(registry.IsCacheDirty);
        }

        [Test]
        public void TypedRegistry_ResolvesThroughTheStructsIdValue()
        {
            var registry = NewRegistry<TestWeaponIdRegistry>((1, "Sword"), (2, "Bow"));

            Assert.IsTrue(registry.Contains(new TestWeaponId(2)));
            Assert.IsFalse(registry.Contains(new TestWeaponId(3)));

            Assert.IsTrue(registry.TryGetName(new TestWeaponId(1), out var name));
            Assert.AreEqual("Sword", name);
        }
    }

    /// <summary>
    /// Locks <see cref="IdRegistryValidator"/> — the single name check every entry point (Add row, rename, Clean-up)
    /// funnels through, so one rule change propagates everywhere. The rules are ordered: whitespace → identifier
    /// grammar → length cap → taken.
    /// </summary>
    [TestFixture]
    internal sealed class IdRegistryValidatorTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void IsValidName_RejectsEmptyOrWhitespace(string input)
        {
            Assert.IsFalse(IdRegistryValidator.IsValidName(input, isTaken: null, out var error));
            Assert.IsNotEmpty(error);
        }

        [TestCase("1Sword", Description = "must not start with a digit")]
        [TestCase("-Sword", Description = "must not start with a hyphen")]
        [TestCase("Sword Axe", Description = "no spaces")]
        [TestCase("Меч", Description = "ASCII identifier grammar only")]
        [TestCase("Sword.Fire", Description = "no dots")]
        public void IsValidName_RejectsNonIdentifierGrammar(string input)
        {
            Assert.IsFalse(IdRegistryValidator.IsValidName(input, isTaken: null, out _));
        }

        [TestCase("Sword")]
        [TestCase("_hidden")]
        [TestCase("Sword-2")]
        [TestCase("fire_bolt3")]
        public void IsValidName_AcceptsIdentifierGrammar(string input)
        {
            Assert.IsTrue(IdRegistryValidator.IsValidName(input, isTaken: null, out var error));
            Assert.IsNull(error);
        }

        [Test]
        public void IsValidName_CapsLengthAt255()
        {
            Assert.IsTrue(IdRegistryValidator.IsValidName(new string('a', 255), isTaken: null, out _));
            Assert.IsFalse(IdRegistryValidator.IsValidName(new string('a', 256), isTaken: null, out _));
        }

        [Test]
        public void IsValidName_RejectsTakenNames_NullDelegateSkipsTheCheck()
        {
            Assert.IsFalse(IdRegistryValidator.IsValidName("Sword", isTaken: _ => true, out var error));
            StringAssert.Contains("Sword", error, "The duplicate error names the offending input.");

            Assert.IsTrue(IdRegistryValidator.IsValidName("Sword", isTaken: null, out _));
        }

        [Test]
        public void Summarize_CountsEmptyAndDuplicateRowsSeparately()
        {
            // Rows: valid, empty, duplicate-of-first, empty — 2 empty + 1 duplicate, matching EnumerateInvalidIndices.
            var names = new[] { "Sword", "", "Sword", null };
            var summary = IdRegistryValidator.Summarize(names.Length, i => names[i]);

            Assert.AreEqual(2, summary.EmptyCount);
            Assert.AreEqual(1, summary.DuplicateCount);
        }
    }
}
