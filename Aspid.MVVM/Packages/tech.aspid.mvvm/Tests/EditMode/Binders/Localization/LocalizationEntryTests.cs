#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for reading a localization table entry back as a string.
    /// </summary>
    /// <remarks>
    /// <see cref="TableEntryReference"/> converts to <see cref="string"/> through its <c>Key</c>, which is filled
    /// only when the entry is referenced <em>by name</em>. An id-based reference — the common case in the
    /// Localization inspector — converts to <see langword="null"/>, and resolving an id to its name needs the
    /// shared table data loaded, which a binder cannot assume.
    /// </remarks>
    [TestFixture]
    public sealed class LocalizationEntryTests : SceneFixture
    {
        /// <summary>
        /// Assigning any entry reference to a <see cref="LocalizeStringEvent"/> makes the package reach for an
        /// active <c>LocalizationSettings</c>. A throwaway instance with no table loaded is what the guard exists
        /// for.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var previousSettings = LocalizationSettings.Instance;
            LocalizationSettings.Instance = Track(ScriptableObject.CreateInstance<LocalizationSettings>());

            RestoreOnTearDown(() => LocalizationSettings.Instance = previousSettings);
        }

        [Test]
        public void UnityTableEntryReference_ById_ConvertsToNull()
        {
            TableEntryReference byId = 4242L;

            Assert.AreEqual(TableEntryReference.Type.Id, byId.ReferenceType);
            Assert.IsNull((string)byId, "Unity started returning a key name for an id-based reference");
        }

        [Test]
        public void EntryBinder_WithAnIdReference_SaysWhyTheViewModelGetsNothing()
        {
            var binder = NewBinder(4242L);

            LogAssert.Expect(LogType.Error, new Regex("referenced by id"));
            var received = ReadProperty(binder);

            Assert.IsNull(received, "Expected null — resolving an id without a table is not possible");
        }

        [Test]
        public void UnityTableEntryReference_ByName_ConvertsToTheKey()
        {
            TableEntryReference byName = "Greeting";

            Assert.AreEqual(TableEntryReference.Type.Name, byName.ReferenceType);
            Assert.AreEqual("Greeting", (string)byName, "Unity stopped returning the key name for a name-based reference");
        }

        private static string ReadProperty(LocalizeStringEventEntryMonoBinder binder)
        {
            var property = typeof(LocalizeStringEventEntryMonoBinder)
                .GetProperty("Property", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.IsNotNull(property, "Property was renamed — this test no longer checks anything");
            return (string)property.GetValue(binder);
        }

        private LocalizeStringEventEntryMonoBinder NewBinder(TableEntryReference reference)
        {
            var component = Spawn<LocalizeStringEvent>("Localize");
            component.StringReference.TableEntryReference = reference;

            return component.gameObject.AddComponent<LocalizeStringEventEntryMonoBinder>();
        }
    }
}
#endif
