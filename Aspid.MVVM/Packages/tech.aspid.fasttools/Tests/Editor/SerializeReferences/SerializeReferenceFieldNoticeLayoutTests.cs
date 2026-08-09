using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Guards the cross-drawer notice-position contract (ASP-24): the UIToolkit <see cref="SerializeReferenceField"/>
    /// must render the shared-reference notice BELOW the assigned instance's child fields, exactly like the IMGUI
    /// drawer, which draws that notice after the children. A shared reference is the only notice that coexists with
    /// children (missing / required / mixed render no value, so no children), so it sits at the very bottom of the
    /// field. The notice host is a sibling placed after the foldout content, so it never inherits the child indent.
    /// </summary>
    /// <remarks>
    /// Reuses the shared <c>LinkerTestObject</c> / <c>TestSword</c> fixtures from <c>SerializeReferenceTestFixtures.cs</c>: linking two
    /// managed-reference fields onto one rid surfaces the shared-reference notice on a field that also has a value (so it
    /// renders child fields), which is the only notice that co-exists with children — the case the bug was about.
    /// </remarks>
    [TestFixture]
    internal sealed class SerializeReferenceFieldNoticeLayoutTests
    {
        [Test]
        public void SharedNotice_RendersBelowChildFields()
        {
            var obj = ScriptableObject.CreateInstance<LinkerTestObject>();

            try
            {
                var serialized = new SerializedObject(obj);
                serialized.FindProperty("a").managedReferenceValue = new TestSword { damage = 7 };
                serialized.ApplyModifiedProperties();

                // Put both fields on one shared rid so field 'a' shows the shared-reference notice while still holding a
                // value (and therefore rendering the TestSword.damage child field).
                Assert.IsTrue(SerializeReferenceLinker.LinkTo(serialized.FindProperty("b"), "a"));
                serialized.Update();

                var property = serialized.FindProperty("a");
                property.isExpanded = true;

                var field = new SerializeReferenceField("A", property);

                var notice = FindFirst<SerializeReferenceNotice>(field);
                Assert.IsNotNull(notice, "A shared reference must surface a notice in the UIToolkit field.");

                var content = FindFirstWithClass(field, Foldout.contentUssClassName);
                Assert.IsNotNull(content, "The foldout content container (which hosts the child fields) must exist.");

                // The notice must NOT live inside the content container — it is a sibling that follows it, so it never
                // inherits the child indent and always renders below the children.
                Assert.IsFalse(IsAncestorOf(content, notice),
                    "The notice must not be nested inside the foldout content (the children container).");

                Assert.Greater(
                    PreOrderIndex(field, notice),
                    PreOrderIndex(field, content),
                    "The notice must render below (after) the foldout content/children, matching the IMGUI drawer.");
            }
            finally
            {
                Object.DestroyImmediate(obj);
            }
        }

        // Pre-order (document-order) position of an element in the real visual tree, so "renders below" reduces to a
        // larger index. Walks the hierarchy (not the contentContainer view) so the toggle, the notices host and the
        // content container are all visited as siblings of the foldout.
        private static int PreOrderIndex(VisualElement root, VisualElement target)
        {
            var index = 0;
            foreach (var element in DescendantsAndSelf(root))
            {
                if (element == target) return index;
                index++;
            }

            return -1;
        }

        private static T FindFirst<T>(VisualElement root) where T : VisualElement
        {
            foreach (var element in DescendantsAndSelf(root))
            {
                if (element is T match)
                    return match;
            }

            return null;
        }

        private static VisualElement FindFirstWithClass(VisualElement root, string className)
        {
            foreach (var element in DescendantsAndSelf(root))
            {
                if (element.ClassListContains(className))
                    return element;
            }

            return null;
        }

        private static bool IsAncestorOf(VisualElement ancestor, VisualElement node)
        {
            for (var current = node.hierarchy.parent; current is not null; current = current.hierarchy.parent)
            {
                if (current == ancestor)
                    return true;
            }

            return false;
        }

        private static IEnumerable<VisualElement> DescendantsAndSelf(VisualElement root)
        {
            yield return root;

            for (var i = 0; i < root.hierarchy.childCount; i++)
            {
                foreach (var descendant in DescendantsAndSelf(root.hierarchy[i]))
                    yield return descendant;
            }
        }
    }
}
