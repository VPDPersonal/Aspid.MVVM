using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
using Store = Aspid.FastTools.SerializeReferences.Editors.SerializeReferencePendingAssignment;
using Entry = Aspid.FastTools.SerializeReferences.Editors.SerializeReferencePendingAssignment.Entry;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Coverage for the persistence contract of <see cref="SerializeReferencePendingAssignment"/> — the deferred
    /// "Create new script" assignment that must survive every domain reload until the new type compiles. The bug being
    /// pinned here is the old up-front erase that dropped any entry unresolvable in the first post-reload pass; these
    /// tests fix the wire format (so re-persisted entries round-trip), the legacy back-compat decode, malformed-line
    /// rejection, the supersede-on-re-pick merge, and the cross-reload give-up boundary.
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferencePendingAssignmentTests
    {
        private const string GlobalId = "GlobalObjectId_V1-2-0000000000000000f000000000000000-12345678901234567-0";
        private const string Path = "_weapons.Array.data[3]._primary";
        private const string TypeName = "Game.Weapons.Pistol";

        [Test]
        public void Entry_RoundTrips_AllFieldsThroughEncodeDecode()
        {
            var entry = new Entry(GlobalId, Path, TypeName, attempts: 7);

            Assert.IsTrue(Entry.TryDecode(entry.Encode(), out var decoded));
            Assert.AreEqual(entry, decoded, "Encode/TryDecode must be a lossless round-trip including the attempt count.");
        }

        [Test]
        public void Entry_GenericAqnTypeName_SurvivesRoundTrip()
        {
            // An assembly-qualified closed-generic name carries '[', ']', ',' and '.' — but never '|' or '\n', so the
            // pipe/newline split must not corrupt it.
            const string generic = "Game.Inventory.Slot`1[[Game.Weapons.Pistol, Game.Runtime]]";
            var entry = new Entry(GlobalId, Path, generic, attempts: 0);

            Assert.IsTrue(Entry.TryDecode(entry.Encode(), out var decoded));
            Assert.AreEqual(generic, decoded.FullTypeName);
        }

        [Test]
        public void TryDecode_LegacyThreeFieldLine_DecodesWithZeroAttempts()
        {
            // Entries written before retry tracking have no attempt field; they must decode (attempts = 0), not be dropped.
            var legacy = $"{GlobalId}|{Path}|{TypeName}";

            Assert.IsTrue(Entry.TryDecode(legacy, out var decoded));
            Assert.AreEqual(0, decoded.Attempts);
            Assert.AreEqual(TypeName, decoded.FullTypeName);
        }

        [Test]
        public void TryDecode_NonPositiveOrGarbageAttempts_ClampToZero()
        {
            Assert.IsTrue(Entry.TryDecode($"{GlobalId}|{Path}|{TypeName}|-5", out var negative));
            Assert.AreEqual(0, negative.Attempts);

            Assert.IsTrue(Entry.TryDecode($"{GlobalId}|{Path}|{TypeName}|oops", out var garbage));
            Assert.AreEqual(0, garbage.Attempts);
        }

        [Test]
        public void TryDecode_MalformedLines_AreRejected()
        {
            Assert.IsFalse(Entry.TryDecode(null, out _), "null line");
            Assert.IsFalse(Entry.TryDecode("", out _), "empty line");
            Assert.IsFalse(Entry.TryDecode($"{GlobalId}|{Path}", out _), "fewer than three fields");
            Assert.IsFalse(Entry.TryDecode($"|{Path}|{TypeName}", out _), "empty global id");
            Assert.IsFalse(Entry.TryDecode($"{GlobalId}||{TypeName}", out _), "empty property path");
            Assert.IsFalse(Entry.TryDecode($"{GlobalId}|{Path}|", out _), "empty type name");
        }

        [Test]
        public void Decode_SkipsMalformedLines_KeepsValidOnes()
        {
            var raw = string.Join("\n",
                $"{GlobalId}|{Path}|{TypeName}|1",
                "garbage-without-separators",
                $"{GlobalId}|{Path}|Game.Weapons.Rifle|0");

            var entries = Store.Decode(raw);

            Assert.AreEqual(2, entries.Count, "The malformed middle line must be skipped, not abort the whole decode.");
            Assert.AreEqual(TypeName, entries[0].FullTypeName);
            Assert.AreEqual("Game.Weapons.Rifle", entries[1].FullTypeName);
        }

        [Test]
        public void Decode_EmptyOrNull_ReturnsEmptyList()
        {
            Assert.IsEmpty(Store.Decode(null));
            Assert.IsEmpty(Store.Decode(string.Empty));
        }

        [Test]
        public void EncodeDecode_MultipleEntries_PreserveOrderAndContent()
        {
            var original = new List<Entry>
            {
                new(GlobalId, "_a", "Game.A", 0),
                new(GlobalId, "_b", "Game.B", 4),
                new(GlobalId, "_c", "Game.C", 31),
            };

            var roundTripped = Store.Decode(Store.Encode(original));

            CollectionAssert.AreEqual(original, roundTripped, "A multi-entry queue must survive encode/decode in order.");
        }

        [Test]
        public void WithIncrementedAttempt_BumpsCount_PreservesIdentity()
        {
            var entry = new Entry(GlobalId, Path, TypeName, attempts: 2);
            var next = entry.WithIncrementedAttempt();

            Assert.AreEqual(3, next.Attempts);
            Assert.IsTrue(entry.SameTarget(next), "Incrementing the attempt must not change the entry's target.");
            Assert.AreEqual(TypeName, next.FullTypeName);
        }

        [Test]
        public void Merge_RePickSameField_SupersedesPreviousEntry()
        {
            var queue = new List<Entry> { new(GlobalId, Path, "Game.OldPick", 5) };

            Store.Merge(queue, new Entry(GlobalId, Path, "Game.NewPick", 0));

            Assert.AreEqual(1, queue.Count, "Re-picking the same field must replace, not append, the pending assignment.");
            Assert.AreEqual("Game.NewPick", queue[0].FullTypeName);
            Assert.AreEqual(0, queue[0].Attempts, "The superseding pick starts its own attempt budget.");
        }

        [Test]
        public void Merge_DifferentField_AppendsAndKeepsExisting()
        {
            var queue = new List<Entry> { new(GlobalId, "_a", "Game.A", 1) };

            Store.Merge(queue, new Entry(GlobalId, "_b", "Game.B", 0));

            Assert.AreEqual(2, queue.Count);
            Assert.AreEqual("Game.A", queue[0].FullTypeName, "An unrelated pending field must be untouched.");
            Assert.AreEqual("Game.B", queue[1].FullTypeName);
        }

    }

    // A persistable target for the resolve-pass tests: saved as an asset so its GlobalObjectId round-trips back to the
    // live object (an in-memory ScriptableObject's id does not). No fields are needed — an unresolved type short-circuits
    // before the property is ever read.
    internal sealed class PendingAssignmentProbeObject : ScriptableObject { }

    /// <summary>
    /// Integration coverage that drives <see cref="SerializeReferencePendingAssignment.ResolvePass"/> against a real
    /// <see cref="SessionState"/>. This is the assertion that actually pins ASP-29: an entry that cannot be applied this
    /// pass must be <b>re-persisted, not erased</b>. It also pins the budget split — a not-yet-loaded target must not
    /// spend the cross-reload give-up budget, while a loaded target whose type is unresolved spends exactly one attempt.
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferencePendingAssignmentResolveTests
    {
        // A well-formed GlobalObjectId that parses but resolves to no live object (its owner is "not loaded").
        private const string UnloadedGlobalId =
            "GlobalObjectId_V1-2-0000000000000000f000000000000000-12345678901234567-0";
        private const string UnresolvableType = "Aspid.FastTools.Tests.NoSuchPendingType";
        private const string ProbeAssetPath = "Assets/__AspidPendingAssignmentProbe__.asset";

        [SetUp]
        public void SetUp() => SessionState.EraseString(Store.Key);

        [TearDown]
        public void TearDown() => SessionState.EraseString(Store.Key);

        [Test]
        public void ResolvePass_UnapplicableEntry_IsRePersisted_NotErased()
        {
            // The ASP-29 regression guard: the old code erased the key up front and dropped any entry it could not
            // resolve in the first pass. A re-introduction of that bug must fail HERE, not slip past the wire-model tests.
            Seed(new Entry(UnloadedGlobalId, "_field", UnresolvableType, attempts: 0));

            var stillPending = Store.ResolvePass(countAttempt: true);

            Assert.IsTrue(stillPending, "An entry that cannot be applied yet must keep the queue pending.");
            var survivors = Store.Decode(SessionState.GetString(Store.Key, string.Empty));
            Assert.AreEqual(1, survivors.Count, "The unapplied entry must be re-persisted, not erased (the ASP-29 bug).");
            Assert.AreEqual(UnresolvableType, survivors[0].FullTypeName);
        }

        [Test]
        public void ResolvePass_TargetNotLoaded_DoesNotSpendTheGiveUpBudget()
        {
            // A closed scene/asset can never be fixed by a domain reload, so its attempt counter must stay put — otherwise
            // unrelated reloads burn the budget and silently drop a still-valid assignment.
            Seed(new Entry(UnloadedGlobalId, "_field", UnresolvableType, attempts: 5));

            Store.ResolvePass(countAttempt: true);

            var survivors = Store.Decode(SessionState.GetString(Store.Key, string.Empty));
            Assert.AreEqual(1, survivors.Count);
            Assert.AreEqual(5, survivors[0].Attempts, "A not-yet-loaded target must not increment the give-up budget.");
        }

        [Test]
        public void ResolvePass_LoadedTargetUnresolvedType_SpendsExactlyOneAttempt()
        {
            var probe = ScriptableObject.CreateInstance<PendingAssignmentProbeObject>();
            try
            {
                AssetDatabase.CreateAsset(probe, ProbeAssetPath);
                var globalId = GlobalObjectId.GetGlobalObjectIdSlow(probe).ToString();
                Seed(new Entry(globalId, "_field", UnresolvableType, attempts: 0));

                Store.ResolvePass(countAttempt: true);

                var survivors = Store.Decode(SessionState.GetString(Store.Key, string.Empty));
                Assert.AreEqual(1, survivors.Count, "A resolvable target with an unresolved type stays pending.");
                Assert.AreEqual(1, survivors[0].Attempts,
                    "A loaded target whose type has not compiled yet spends exactly one attempt per reload pass.");
            }
            finally
            {
                AssetDatabase.DeleteAsset(ProbeAssetPath);
            }
        }

        private static void Seed(Entry entry) =>
            SessionState.SetString(Store.Key, Store.Encode(new List<Entry> { entry }));
    }
}
