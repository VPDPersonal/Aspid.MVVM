using UnityEngine;
using NUnit.Framework;
using UnityEngine.Video;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="VideoPlayer"/> binders: which clip plays, its playback speed, and looping.
    /// </summary>
    [TestFixture]
    public sealed class VideoPlayerBinderTests : SceneFixture
    {
        [Test]
        public void PlaybackSpeed_IsClampedToTheDocumentedRange()
        {
            var player = Spawn<VideoPlayer>("VideoPlayer");
            var binder = player.gameObject.AddComponent<VideoPlayerPlaybackSpeedMonoBinder>();

            ((IBinder<float>)binder).SetValue(2f);
            Assert.AreEqual(2f, player.playbackSpeed, 0.001f, "The playback speed did not reach the player");

            ((IBinder<float>)binder).SetValue(100f);
            Assert.AreEqual(10f, player.playbackSpeed, 0.001f, "A speed outside 0..10 was not clamped");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(player.playbackSpeed), "NaN reached the player");
        }

        [Test]
        public void IsLooping_ReachesThePlayer()
        {
            var player = Spawn<VideoPlayer>("VideoPlayer");
            var binder = player.gameObject.AddComponent<VideoPlayerIsLoopingMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);

            Assert.IsTrue(player.isLooping, "Looping did not reach the player");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var player = Spawn<VideoPlayer>("VideoPlayer");

            Assert.IsTrue(new VideoPlayerPlaybackSpeedBinder(player).CanBind);
            Assert.IsTrue(new VideoPlayerIsLoopingBinder(player).CanBind);
            Assert.IsTrue(new VideoPlayerClipBinder(player).CanBind);
        }
    }
}
