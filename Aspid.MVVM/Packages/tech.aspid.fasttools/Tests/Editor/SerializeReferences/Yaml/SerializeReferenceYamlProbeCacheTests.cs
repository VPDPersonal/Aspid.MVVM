using System.IO;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Behavioural coverage for <see cref="SerializeReferenceYamlProbeCache"/>: it serves a cached copy while the file's
    /// last-write-time is unchanged, re-reads when the timestamp moves, and drops everything on <see cref="ClearCache"/>.
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceYamlProbeCacheTests
    {
        private string _path;

        [SetUp]
        public void SetUp()
        {
            SerializeReferenceYamlProbeCache.ClearCache();
            _path = YamlFixtures.WriteTemp("alpha\n");
        }

        [TearDown]
        public void TearDown()
        {
            SerializeReferenceYamlProbeCache.ClearCache();
            YamlFixtures.Delete(_path);
        }

        [Test]
        public void ReadAllLines_ReturnsFileContent()
        {
            var lines = SerializeReferenceYamlProbeCache.ReadAllLines(_path);
            Assert.AreEqual(1, lines.Length);
            Assert.AreEqual("alpha", lines[0]);
        }

        [Test]
        public void ReadAllLines_SameTimestamp_ServesCachedContent()
        {
            // Pin an explicit write time on BOTH the warm read and the post-edit read. Capturing the OS-written stamp
            // instead would be unreliable: Mono's SetLastWriteTimeUtc can store a lower-precision value than
            // GetLastWriteTimeUtc returned, so the cached key would never match. Setting the same value twice is
            // deterministic regardless of the platform's timestamp resolution.
            var pinned = new System.DateTime(2020, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

            File.SetLastWriteTimeUtc(_path, pinned);
            var first = SerializeReferenceYamlProbeCache.ReadAllLines(_path);
            Assert.AreEqual("alpha", first[0]);

            // Change the content but restore the same pinned write time: the cache key is (path, write-time), so the
            // stale copy is served by design until the timestamp moves or the cache is cleared.
            File.WriteAllText(_path, "beta\n");
            File.SetLastWriteTimeUtc(_path, pinned);

            var cached = SerializeReferenceYamlProbeCache.ReadAllLines(_path);
            Assert.AreEqual("alpha", cached[0], "Unchanged write-time must serve the cached copy.");
        }

        [Test]
        public void ReadAllLines_NewerTimestamp_ReReads()
        {
            SerializeReferenceYamlProbeCache.ReadAllLines(_path); // warm

            File.WriteAllText(_path, "beta\n");
            File.SetLastWriteTimeUtc(_path, File.GetLastWriteTimeUtc(_path).AddSeconds(5));

            var reread = SerializeReferenceYamlProbeCache.ReadAllLines(_path);
            Assert.AreEqual("beta", reread[0], "A newer write-time must bust the cache.");
        }

        [Test]
        public void ClearCache_ForcesReRead()
        {
            var stamp = File.GetLastWriteTimeUtc(_path);
            SerializeReferenceYamlProbeCache.ReadAllLines(_path); // warm

            File.WriteAllText(_path, "beta\n");
            File.SetLastWriteTimeUtc(_path, stamp); // keep timestamp so only ClearCache can bust it

            SerializeReferenceYamlProbeCache.ClearCache();

            var reread = SerializeReferenceYamlProbeCache.ReadAllLines(_path);
            Assert.AreEqual("beta", reread[0], "ClearCache must force a fresh read.");
        }
    }
}
