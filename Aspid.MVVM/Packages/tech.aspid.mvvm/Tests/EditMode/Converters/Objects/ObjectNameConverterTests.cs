using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ObjectNameConverter"/> — the stripped "(Clone)" suffix and the
    /// missing-object fallback.
    /// </summary>
    [TestFixture]
    public sealed class ObjectNameConverterTests : SceneFixture
    {
        [Test]
        public void ObjectName_StripsTheCloneSuffix() =>
            Assert.AreEqual(
                "Enemy",
                new ObjectNameConverter(fallback: string.Empty).Convert(Spawn("Enemy(Clone)")));

        [Test]
        public void ObjectName_MissingObjectGivesTheFallback() =>
            Assert.AreEqual("—", new ObjectNameConverter(fallback: "—").Convert(null));
    }
}
