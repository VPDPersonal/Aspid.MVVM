using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="RichTextSizeConverter"/> — the percent and point tags, and the
    /// non-positive size guards on the constructor and on the serialized field.
    /// </summary>
    [TestFixture]
    public sealed class RichTextSizeConverterTests
    {
        [TestCase("")]
        [TestCase("   ")]
        public void Convert_LeavesBlankUntagged(string value) =>
            Assert.AreSame(value, new RichTextSizeConverter(200f).Convert(value));

        [Test]
        public void Convert_TagsAsPercentByDefault() =>
            Assert.AreEqual("<size=150%>hp</size>", new RichTextSizeConverter(150f).Convert("hp"));

        [Test]
        public void Convert_TagsAsPointsWhenAsked() =>
            Assert.AreEqual("<size=32>hp</size>", new RichTextSizeConverter(32f, isPercent: false).Convert("hp"));

        [TestCase(0f)]
        [TestCase(-50f)]
        [TestCase(float.NaN)]
        public void Constructor_NotAboveZero_IsRefused(float size) =>
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new RichTextSizeConverter(size));

        // A size the Inspector can still hold — an animated or copied value — has to show the text at
        // its own size rather than emit <size=0%>, which draws nothing at all.
        [TestCase(0f)]
        [TestCase(-50f)]
        [TestCase(float.NaN)]
        public void Convert_SerializedSizeNotAboveZero_IsReportedAndLeavesTheStringUntagged(float size)
        {
            LogAssert.Expect(LogType.Error, new Regex("RichTextSizeConverter.*no text can be drawn at"));

            var converter = new RichTextSizeConverter(100f);
            SetField(converter, "_size", size);

            Assert.AreEqual("hp", converter.Convert("hp"));
        }

        private static void SetField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");
            field.SetValue(target, value);

            if (target is ISerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();
        }
    }
}
