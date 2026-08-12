#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for reading a localization table entry back as a string.
    /// </summary>
    /// <remarks>
    /// <see cref="TableEntryReference"/> converts to <see cref="string"/> through its <c>Key</c>, which is filled
    /// only when the entry is referenced <em>by name</em>. Picking an entry in the Localization inspector stores it
    /// by id instead — the common case — so the conversion produced <see langword="null"/> and the binders handed
    /// that to the ViewModel in <see cref="BindMode.OneWayToSource"/> as if it were the entry.
    /// <para/>
    /// Resolving an id to its name needs the shared table data loaded, which a binder cannot assume. The value is
    /// therefore still <see langword="null"/>; what changed is that it says so.
    /// </remarks>
    [TestFixture]
    public sealed class LocalizationEntryTests
    {
        private readonly List<Object> _spawned = new();

        private LocalizationSettings _previousSettings;

        /// <summary>
        /// Assigning any entry reference to a <see cref="LocalizeStringEvent"/> makes the package reach for an
        /// active <c>LocalizationSettings</c>, and this project ships none. A throwaway instance is enough — no
        /// table is loaded, which is precisely the situation the guard exists for.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _previousSettings = LocalizationSettings.Instance;

            var settings = ScriptableObject.CreateInstance<LocalizationSettings>();
            _spawned.Add(settings);

            LocalizationSettings.Instance = settings;
        }

        [TearDown]
        public void TearDown()
        {
            LocalizationSettings.Instance = _previousSettings;

            foreach (var spawned in _spawned)
            {
                if (spawned) Object.DestroyImmediate(spawned);
            }

            _spawned.Clear();
        }

        /// <summary>
        /// Pins the premise: the conversion the binders relied on yields nothing for an id-based reference.
        /// </summary>
        [Test]
        public void UnityTableEntryReference_ById_ConvertsToNull()
        {
            TableEntryReference byId = 4242L;

            Assert.AreEqual(TableEntryReference.Type.Id, byId.ReferenceType);
            Assert.IsNull((string)byId, "Unity начала выдавать имя ключа для ссылки по id");
        }

        [Test]
        public void EntryBinder_WithAnIdReference_SaysWhyTheViewModelGetsNothing()
        {
            var binder = NewBinder(4242L);

            LogAssert.Expect(LogType.Error, new Regex("referenced by id"));
            var received = ReadProperty(binder);

            Assert.IsNull(received, "Ожидалось null — резолв id без таблицы невозможен");
        }

        /// <summary>
        /// The other half of the premise: a name-based reference does convert, so the guard must stay quiet for it.
        /// </summary>
        /// <remarks>
        /// Checked on the reference itself: the binder path is covered by the id case, which is where the defect is.
        /// </remarks>
        [Test]
        public void UnityTableEntryReference_ByName_ConvertsToTheKey()
        {
            TableEntryReference byName = "Greeting";

            Assert.AreEqual(TableEntryReference.Type.Name, byName.ReferenceType);
            Assert.AreEqual("Greeting", (string)byName, "Unity перестала выдавать имя ключа для именной ссылки");
        }

        private static string ReadProperty(LocalizeStringEventEntryMonoBinder binder)
        {
            var property = typeof(LocalizeStringEventEntryMonoBinder)
                .GetProperty("Property", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.IsNotNull(property, "Свойство Property переименовано — тест больше ничего не проверяет");
            return (string)property.GetValue(binder);
        }

        private LocalizeStringEventEntryMonoBinder NewBinder(TableEntryReference reference)
        {
            var gameObject = new GameObject("Localize");
            _spawned.Add(gameObject);

            var component = gameObject.AddComponent<LocalizeStringEvent>();
            component.StringReference.TableEntryReference = reference;

            return gameObject.AddComponent<LocalizeStringEventEntryMonoBinder>();
        }
    }
}
#endif
