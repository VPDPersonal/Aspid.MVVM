using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aspid.MVVM.Tests
{
    internal sealed class UnityViewAttributeTests : SceneFixture
    {
        [Test]
        public void AttributesRejectInvalidArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new AddBinderContextMenuAttribute(null));
            Assert.Throws<ArgumentNullException>(() => new AddBinderContextMenuAttribute(typeof(Component), null));
            Assert.Throws<ArgumentNullException>(() => new AddBinderContextMenuByTypeAttribute(null));
            Assert.Throws<ArgumentNullException>(() => new RequireBinderAttribute((Type[])null));
            Assert.Throws<ArgumentException>(() => new RequireBinderAttribute(new Type[] { null }));
            Assert.Throws<ArgumentNullException>(() => new RequireBinderAttribute((string[])null));
            Assert.Throws<ArgumentNullException>(() => new ShowDesignViewModelAttribute((Type)null));
            Assert.Throws<ArgumentNullException>(() => new ShowDesignViewModelAttribute((Type[])null));
            Assert.Throws<ArgumentException>(() => new ShowDesignViewModelAttribute(new Type[] { null }));
            Assert.Throws<ArgumentException>(() => new ShowDesignViewModelAttribute(typeof(IViewModel), true));
            Assert.Throws<ArgumentException>(() => new ShowDesignViewModelAttribute(typeof(AbstractModel), true));
            Assert.Throws<ArgumentException>(() => new ShowDesignViewModelAttribute(typeof(GenericModel<>), true));
            Assert.DoesNotThrow(() => new ShowDesignViewModelAttribute(typeof(Model), true));
        }

        [Test]
        public void ValueTypeRequirementsAndDesignFiltersRetainTheirMeaning()
        {
            Assert.That(new RequireBinderAttribute(typeof(int)).AssemblyQualifiedNames,
                Is.EqualTo(new[] { typeof(int).AssemblyQualifiedName }));
            Assert.That(new RequireBinderAttribute().AssemblyQualifiedNames, Is.Null);
            Assert.That(new ShowDesignViewModelAttribute(typeof(Model)).Types, Is.EqualTo(new[] { typeof(Model) }));
            Assert.That(new ShowDesignViewModelAttribute(typeof(string)).Types,
                Is.EqualTo(new[] { typeof(string), typeof(IViewModel) }));
        }

        [Test]
        public void SingleGroupsDoNotReplaceRangesAndEndClosesBeforeItsMember()
        {
            var root = new VisualElement();
            var router = new HeaderGroupRouter(root);
            foreach (var name in new[] { "First", "Single", "After", "End", "Last" })
                router.Add(new Label(name), typeof(Grouped).GetField(name));
            var groups = root.Query<Foldout>().ToList();
            var a = groups.Single(group => group.text == "A");
            var b = groups.Single(group => group.text == "B");
            Assert.That(a.contentContainer.Query<Label>().ToList().Select(label => label.text), Does.Contain("After"));
            Assert.That(b.contentContainer.Query<Label>().ToList().Select(label => label.text), Does.Contain("Single"));
            Assert.That(root.Children().OfType<Label>().Select(label => label.text), Is.EquivalentTo(new[] { "End", "Last" }));
        }

        [Test]
        public void ContextMenuUsesExplicitPathsAndSubPaths()
        {
            var target = Spawn<MenuTarget>();
            using var serialized = new SerializedObject(target);
            var property = serialized.FindProperty("Value");
            var menuType = typeof(MonoViewEditor).Assembly.GetType("Aspid.MVVM.AddBinderContextMenu");
            menuType.GetField("_contexts", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, null);
            menuType.GetMethod("Initialize", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
            var result = (System.Collections.IEnumerable)menuType.GetMethod(
                "FindTypesWithTargetPropertyTypeAttribute", BindingFlags.NonPublic | BindingFlags.Static)
                .Invoke(null, new object[] { target, property });
            var names = result.Cast<object>().Select(item => (string)item.GetType().GetField("Name").GetValue(item));
            Assert.That(names, Does.Contain("Custom/Explicit"));
            Assert.That(names, Does.Contain("Add Binder/Numbers/Sub Menu Probe"));
        }

        [Test]
        public void StrictDesignTypeIsSelectedByTheRealInspector()
        {
            var view = Spawn<StrictView>();
            var editor = UnityEditor.Editor.CreateEditor(view);
            try
            {
                Assert.That(editor.serializedObject.FindProperty("_designViewModel").stringValue,
                    Is.EqualTo(typeof(Model).AssemblyQualifiedName));
                Assert.That(new ShowDesignViewModelAttribute(typeof(Model), true).StrictType, Is.True);
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(editor);
            }
        }

        [ShowDesignViewModel(typeof(Model), strictType: true)]
        private sealed class StrictView : MonoView { }

        private sealed class Grouped
        {
            [HeaderGroupStart("A")] public int First;
            [HeaderGroup("B")] public int Single;
            public int After;
            [HeaderGroupEnd] public int End;
            public int Last;
        }
        private sealed class MenuTarget : MonoBehaviour { public int Value; }
        [AddBinderContextMenu(typeof(MenuTarget), "Value", Path = "Custom/Explicit", SubPath = "Ignored")]
        internal sealed class ExplicitMenuProbe : MonoBehaviour { }
        [AddComponentMenu("Tests/Sub Menu Probe")]
        [AddBinderContextMenu(typeof(MenuTarget), "Value", SubPath = "Numbers")]
        internal sealed class SubMenuProbe : MonoBehaviour { }
        private abstract class AbstractModel : IViewModel
        {
            public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) => default;
        }
        private sealed class Model : AbstractModel { }
        private class GenericModel<T> : AbstractModel { }
    }
}
