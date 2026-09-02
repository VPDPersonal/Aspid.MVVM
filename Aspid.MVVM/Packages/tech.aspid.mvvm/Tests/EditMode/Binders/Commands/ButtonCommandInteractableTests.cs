using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for how <see cref="ButtonCommandMonoBinder"/> reflects a command's <c>CanExecute</c> result.
    /// </summary>
    [TestFixture]
    public sealed class ButtonCommandInteractableTests : SceneFixture
    {
        [Test]
        public void VisibleMode_HidesTheAssignedTarget_NotTheBinderOwnObject()
        {
            var (binder, target, own) = Create(InteractableMode.Visible);

            Bind(binder, canExecute: false);

            Assert.IsFalse(target.gameObject.activeSelf, "The assigned control was not hidden");
            Assert.IsTrue(own.activeSelf, "The binder hid its own GameObject");
        }

        [Test]
        public void VisibleMode_ShowsTheAssignedTarget_WhenTheCommandBecomesExecutable()
        {
            var (binder, target, own) = Create(InteractableMode.Visible);
            target.gameObject.SetActive(false);

            Bind(binder, canExecute: true);

            Assert.IsTrue(target.gameObject.activeSelf, "The assigned control was not shown back");
            Assert.IsTrue(own.activeSelf, "The binder touched its own GameObject");
        }

        [Test]
        public void InteractableMode_WritesToTheAssignedTarget()
        {
            var (binder, target, _) = Create(InteractableMode.Interactable);

            Bind(binder, canExecute: false);

            Assert.IsFalse(target.interactable, "The assigned control was not disabled");
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

            Assert.AreEqual(1, view.Calls, "The view did not receive the command's state");
            Assert.IsFalse(view.LastValue, "The view received the wrong state");
        }

        [Test]
        public void MissingTarget_ReportsInsteadOfThrowing()
        {
            var binder = Spawn<ButtonCommandMonoBinder>("Binder");
            SetMode(binder, InteractableMode.Interactable);

            LogAssert.Expect(LogType.Error, new Regex("target Selectable is missing or destroyed"));
            Bind(binder, canExecute: false);
        }

        private (ButtonCommandMonoBinder binder, Button target, GameObject own) Create(InteractableMode mode)
        {
            var binder = Spawn<ButtonCommandMonoBinder>("Binder");
            var target = Spawn<Button>("Target");

            var serializedObject = new SerializedObject(binder);
            serializedObject.FindProperty("_component").objectReferenceValue = target;
            serializedObject.FindProperty("_interactableMode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return (binder, target, binder.gameObject);
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

        /// <summary>
        /// Records the last <see cref="ICanExecuteView.SetCanExecute"/> call so a test can tell
        /// "the view was driven" from "the view was skipped".
        /// </summary>
        private sealed class RecordingCanExecuteView : ICanExecuteView
        {
            public int Calls { get; private set; }

            public bool LastValue { get; private set; }

            public void SetCanExecute(bool canExecute)
            {
                Calls++;
                LastValue = canExecute;
            }
        }
    }
}
