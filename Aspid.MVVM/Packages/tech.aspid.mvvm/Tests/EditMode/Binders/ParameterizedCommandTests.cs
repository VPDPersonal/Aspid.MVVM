using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the concrete <see cref="ButtonCommandMonoBinder{T}"/> closures: the parameterized command binders
    /// a project can add without writing a class first.
    /// </summary>
    /// <remarks>
    /// Unity cannot add a generic MonoBehaviour as a component, so every parameterized command binder in the package
    /// was abstract — binding "buy 10 of this" required a one-line C# subclass before the Inspector would show
    /// anything.
    /// </remarks>
    [TestFixture]
    public sealed class ParameterizedCommandTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void TheIntClosure_ForwardsTheInspectorParameter()
        {
            var (button, binder) = NewButton<ButtonCommandIntMonoBinder>();
            SetParam(binder, property => property.intValue = 7);

            var received = 0;
            Bind(binder, new RelayCommand<int>(value => received = value));
            button.onClick.Invoke();

            Assert.AreEqual(7, received, "Параметр из инспектора не доехал до команды");
        }

        [Test]
        public void TheFloatClosure_ForwardsTheInspectorParameter()
        {
            var (button, binder) = NewButton<ButtonCommandFloatMonoBinder>();
            SetParam(binder, property => property.floatValue = 0.5f);

            var received = 0f;
            Bind(binder, new RelayCommand<float>(value => received = value));
            button.onClick.Invoke();

            Assert.AreEqual(0.5f, received, 0.001f, "Параметр из инспектора не доехал до команды");
        }

        [Test]
        public void TheStringClosure_ForwardsTheInspectorParameter()
        {
            var (button, binder) = NewButton<ButtonCommandStringMonoBinder>();
            SetParam(binder, property => property.stringValue = "level-2");

            var received = string.Empty;
            Bind(binder, new RelayCommand<string>(value => received = value));
            button.onClick.Invoke();

            Assert.AreEqual("level-2", received, "Параметр из инспектора не доехал до команды");
        }

        [Test]
        public void TheBoolClosure_ForwardsTheInspectorParameter()
        {
            var (button, binder) = NewButton<ButtonCommandBoolMonoBinder>();
            SetParam(binder, property => property.boolValue = true);

            var received = false;
            Bind(binder, new RelayCommand<bool>(value => received = value));
            button.onClick.Invoke();

            Assert.IsTrue(received, "Параметр из инспектора не доехал до команды");
        }

        [Test]
        public void TheObjectClosure_ForwardsTheInspectorParameter()
        {
            var (button, binder) = NewButton<ButtonCommandObjectMonoBinder>();
            var asset = new Texture2D(1, 1);

            try
            {
                SetParam(binder, property => property.objectReferenceValue = asset);

                Object received = null;
                Bind(binder, new RelayCommand<Object>(value => received = value));
                button.onClick.Invoke();

                Assert.AreSame(asset, received, "Параметр из инспектора не доехал до команды");
            }
            finally
            {
                Object.DestroyImmediate(asset);
            }
        }

        /// <summary>
        /// The interactable state has to follow the command's answer for <em>this</em> parameter, not for a default
        /// one — a "buy 10" button is disabled by the same command that leaves "buy 1" enabled.
        /// </summary>
        [Test]
        public void TheInteractableState_FollowsCanExecuteForThatParameter()
        {
            var (button, binder) = NewButton<ButtonCommandIntMonoBinder>();
            SetParam(binder, property => property.intValue = 10);

            Bind(binder, new RelayCommand<int>(_ => { }, value => value < 5));

            Assert.IsFalse(button.interactable, "Кнопка активна, хотя команда отказывает именно этому параметру");
        }

        /// <summary>
        /// Binds the command the way a View does. A command binder subscribes to the click inside
        /// <c>OnBound</c>, so a bare <c>SetValue</c> leaves the button unwired.
        /// </summary>
        private static void Bind<T>(IBinder binder, IRelayCommand<T> command) =>
            binder.Bind(new OneWayBindableMember<IRelayCommand<T>>(command));

        private (Button Button, T Binder) NewButton<T>()
            where T : MonoBinder
        {
            var gameObject = new GameObject(typeof(T).Name);
            _spawned.Add(gameObject);

            var button = gameObject.AddComponent<Button>();
            var binder = gameObject.AddComponent<T>();

            return (button, binder);
        }

        /// <summary>
        /// Writes the serialized parameter the way the Inspector does — the field has no public setter, which is the
        /// whole point of these closures.
        /// </summary>
        private static void SetParam(MonoBinder binder, System.Action<SerializedProperty> write)
        {
            var serializedObject = new SerializedObject(binder);
            var property = serializedObject.FindProperty("_param");

            Assert.IsNotNull(property, "У биндера нет сериализуемого поля _param");

            write(property);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
