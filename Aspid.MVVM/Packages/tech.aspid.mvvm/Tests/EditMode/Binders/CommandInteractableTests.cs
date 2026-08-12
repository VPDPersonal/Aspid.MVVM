using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Records the last <see cref="ICanExecuteView.SetCanExecute"/> call so a test can tell
    /// "the view was driven" from "the view was skipped".
    /// </summary>
    internal sealed class RecordingCanExecuteView : ICanExecuteView
    {
        public int Calls { get; private set; }

        public bool LastValue { get; private set; }

        public void SetCanExecute(bool canExecute)
        {
            Calls++;
            LastValue = canExecute;
        }
    }

    /// <summary>
    /// Regression tests for how command binders reflect a command's <c>CanExecute</c> result.
    /// </summary>
    /// <remarks>
    /// Two defects are covered, both living in the same three-branch switch that all 50 command-binder sites
    /// used to carry inline.
    /// <para/>
    /// <see cref="InteractableMode.Visible"/> in the <c>*MonoBinder</c> variants called
    /// <c>gameObject.SetActive</c> — the binder's own object — while the serializable twins correctly used
    /// <c>Target.gameObject</c>. A binder pointed at a <see cref="Selectable"/> on another object therefore hid
    /// itself and left the control visible.
    /// <para/>
    /// <see cref="InteractableMode.Custom"/> dereferenced the serialized <see cref="ICanExecuteView"/> without a
    /// check. The programmatic path is guarded by the constructors, but the inspector lets the mode be chosen
    /// with the reference left empty, and the first <c>CanExecuteChanged</c> then threw a
    /// <see cref="System.NullReferenceException"/> naming neither the binder nor the object.
    /// </remarks>
    [TestFixture]
    public sealed class CommandInteractableTests
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
        public void VisibleMode_HidesTheAssignedTarget_NotTheBinderOwnObject()
        {
            var (binder, target, own) = Create(InteractableMode.Visible);

            Bind(binder, canExecute: false);

            Assert.IsFalse(target.gameObject.activeSelf, "Назначенный контрол не спрятан");
            Assert.IsTrue(own.activeSelf, "Биндер спрятал собственный GameObject");
        }

        [Test]
        public void VisibleMode_ShowsTheAssignedTarget_WhenTheCommandBecomesExecutable()
        {
            var (binder, target, own) = Create(InteractableMode.Visible);
            target.gameObject.SetActive(false);

            Bind(binder, canExecute: true);

            Assert.IsTrue(target.gameObject.activeSelf, "Назначенный контрол не показан обратно");
            Assert.IsTrue(own.activeSelf, "Биндер тронул собственный GameObject");
        }

        [Test]
        public void InteractableMode_WritesToTheAssignedTarget()
        {
            var (binder, target, _) = Create(InteractableMode.Interactable);

            Bind(binder, canExecute: false);

            Assert.IsFalse(target.interactable, "Назначенный контрол не отключён");
        }

        [Test]
        public void CustomMode_WithoutAView_ReportsInsteadOfThrowing()
        {
            var (binder, _, _) = Create(InteractableMode.Custom);

            LogAssert.Expect(LogType.Error, new Regex("no ICanExecuteView is assigned"));
            Bind(binder, canExecute: false);
        }

        [Test]
        public void CustomMode_WithAView_DrivesIt()
        {
            var (binder, _, _) = Create(InteractableMode.Custom);
            var view = new RecordingCanExecuteView();

            SetCustomView(binder, view);
            Bind(binder, canExecute: false);

            Assert.AreEqual(1, view.Calls, "Вид не получил состояние команды");
            Assert.IsFalse(view.LastValue, "В вид уехало не то состояние");
        }

        [Test]
        public void MissingTarget_ReportsInsteadOfThrowing()
        {
            var ownerObject = NewGameObject("Binder");
            var binder = ownerObject.AddComponent<ButtonCommandMonoBinder>();

            SetMode(binder, InteractableMode.Interactable);

            LogAssert.Expect(LogType.Error, new Regex("target Selectable is missing or destroyed"));
            Bind(binder, canExecute: false);
        }

        private (ButtonCommandMonoBinder binder, Button target, GameObject own) Create(InteractableMode mode)
        {
            var ownerObject = NewGameObject("Binder");
            var targetObject = NewGameObject("Target");

            var target = targetObject.AddComponent<Button>();
            var binder = ownerObject.AddComponent<ButtonCommandMonoBinder>();

            var serializedObject = new SerializedObject(binder);
            serializedObject.FindProperty("_component").objectReferenceValue = target;
            serializedObject.FindProperty("_interactableMode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return (binder, target, ownerObject);
        }

        private static void SetMode(ButtonCommandMonoBinder binder, InteractableMode mode)
        {
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_interactableMode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetCustomView(ButtonCommandMonoBinder binder, ICanExecuteView view)
        {
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_customInteractable").managedReferenceValue = view;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        /// <summary>
        /// Hands the binder a command, which is what drives the interactable state:
        /// <c>UpdateCommand</c> subscribes to <c>CanExecuteChanged</c> and invokes the handler once immediately.
        /// </summary>
        private static void Bind(ButtonCommandMonoBinder binder, bool canExecute) =>
            ((IBinder<IRelayCommand>)binder).SetValue(new RelayCommand(() => { }, () => canExecute));

        private GameObject NewGameObject(string name)
        {
            var gameObject = new GameObject(name);
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
