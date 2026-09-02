using System;
using System.Linq;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for shared converter assets: the asset itself, the reference that lets an ordinary
    /// converter field point at one, and the reuse that is the whole reason they exist.
    /// </summary>
    [TestFixture]
    public sealed class ConverterAssetTests : SceneFixture
    {
        private StringConverterAsset _asset;

        [SetUp]
        public void CreateAsset()
        {
            _asset = Track(ScriptableObject.CreateInstance<StringConverterAsset>());
            SetConverter(_asset, new StringFormatConverter("HP: {0}"));
        }

        [Test]
        public void Asset_ForwardsToItsConverter() =>
            Assert.AreEqual("HP: 42", _asset.Convert("42"));

        [Test]
        public void Asset_WithoutAConverter_ReturnsTheDefaultAndReportsEveryTime()
        {
            LogAssert.Expect(LogType.Error, new Regex("no converter assigned"));
            LogAssert.Expect(LogType.Error, new Regex("no converter assigned"));

            var empty = Track(ScriptableObject.CreateInstance<StringConverterAsset>());

            Assert.IsNull(empty.Convert("42"));
            empty.Convert("43");
        }

        // An asset whose converter points back at the asset would recurse until the process dies, and
        // a stack overflow takes the Editor with it — the cycle has to be refused, not survived.
        [Test]
        public void Asset_WhoseConverterLeadsBackToIt_ReturnsTheDefaultAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("leads back to this asset"));

            SetConverter(_asset, new ConverterAssetReference<string, string>(_asset));

            Assert.IsNull(_asset.Convert("42"));
        }

        // The guard is per conversion, not per asset: refusing a cycle must not leave the asset dead.
        [Test]
        public void Asset_KeepsConverting_AfterACycleWasRefused()
        {
            LogAssert.Expect(LogType.Error, new Regex("leads back to this asset"));

            SetConverter(_asset, new ConverterAssetReference<string, string>(_asset));
            _asset.Convert("42");

            SetConverter(_asset, new StringFormatConverter("HP: {0}"));

            Assert.AreEqual("HP: 42", _asset.Convert("42"));
        }

        [Test]
        public void Reference_ForwardsToTheAsset() =>
            Assert.AreEqual("HP: 42", new ConverterAssetReference<string, string>(_asset).Convert("42"));

        // The point of an asset: one authored converter, many fields, one place to correct it.
        [Test]
        public void Reference_SharesOneAuthoredConverterAcrossFields()
        {
            var first = new ConverterAssetReference<string, string>(_asset);
            var second = new ConverterAssetReference<string, string>(_asset);

            SetConverter(_asset, new StringFormatConverter("MP: {0}"));

            Assert.AreEqual("MP: 42", first.Convert("42"));
            Assert.AreEqual("MP: 42", second.Convert("42"));
        }

        [Test]
        public void Reference_WithoutAnAsset_ReturnsTheFallbackAndReportsEveryTime()
        {
            LogAssert.Expect(LogType.Error, new Regex("no asset assigned"));
            LogAssert.Expect(LogType.Error, new Regex("no asset assigned"));
            LogAssert.Expect(LogType.Error, new Regex("no asset assigned"));

            var reference = (ConverterAssetReference<string, string>)Activator.CreateInstance(
                typeof(ConverterAssetReference<string, string>), nonPublic: true);

            Assert.IsNull(reference.Convert("42"));
            reference.Convert("43");
            reference.Convert("44");
        }

        // Unity cannot create an asset of an open generic type, so every asset the user can make is a
        // sealed subclass that closes the arguments. Shipping the base alone would be unusable.
        [Test]
        public void EveryShippedAssetTypeIsConcreteAndCreatable()
        {
            var assets = typeof(Vector3CombineConverter).Assembly
                .GetTypes()
                .Where(type => !type.IsAbstract)
                .Where(type => typeof(ScriptableObject).IsAssignableFrom(type))
                .Where(type => typeof(IConverter).IsAssignableFrom(type))
                .ToArray();

            Assert.That(assets.Length, Is.GreaterThan(5), "shipped converter asset types");
            Assert.IsEmpty(
                assets.Where(type => type.GetCustomAttributes(typeof(CreateAssetMenuAttribute), false).Length == 0),
                "every concrete converter asset needs a CreateAssetMenu entry to be creatable");
        }

        private static void SetConverter(ScriptableObject asset, IConverter<string, string> converter)
        {
            var serialized = new UnityEditor.SerializedObject(asset);
            serialized.FindProperty("_converter").managedReferenceValue = converter;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
