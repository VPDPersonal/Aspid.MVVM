using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Guard test: binders sharing a base class must agree on <see cref="SerializableAttribute"/>.
    /// </summary>
    /// <remarks>
    /// The assertion is deliberately narrow: it flags a type only when the majority of its siblings under the same
    /// base class are marked. A whole family may legitimately be code-only — the casters and the view binders are,
    /// and none of them is reported. It is the odd one out in an otherwise serializable family that indicates a lost
    /// attribute, and that is all this test claims.
    /// </remarks>
    [TestFixture]
    public sealed class BinderSerializableCoverageTests
    {
        [Test]
        public void ABinderInASerializableFamily_IsAlsoSerializable()
        {
            var candidates = Candidates().ToArray();
            Assert.Greater(candidates.Length, 300, "The sweep found no binders — the check would pass vacuously");

            var inconsistent = candidates
                // By the generic definition, not the constructed type: SwitcherBinder<TMP_Text, TMP_FontAsset> and
                // SwitcherBinder<Image, Sprite> are one family, otherwise every binder would be its own family of one.
                .GroupBy(type => type.BaseType!.IsGenericType
                    ? type.BaseType.GetGenericTypeDefinition()
                    : type.BaseType)
                // Direct descendants of the root are not a family: all they share is being binders. They include
                // serializable View-field binders and code-only casters and ViewBinder alike, so a majority here
                // means nothing — it shifts with whoever was added last.
                .Where(family => family.Key != typeof(Binder))
                .Where(family => family.Count(type => type.IsSerializable) > family.Count() / 2)
                .SelectMany(family => family.Where(type => !type.IsSerializable)
                    .Select(type => $"{type.FullName} — base {Name(family.Key)}, "
                        + $"marked {family.Count(sibling => sibling.IsSerializable)} of {family.Count()}"))
                .OrderBy(entry => entry)
                .ToList();

            Assert.IsEmpty(
                inconsistent,
                "Binders without [Serializable] where siblings under the same base class have it — "
                + "Unity will not show them in the inspector, the field stays null:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, inconsistent));
        }

        private static IEnumerable<Type> Candidates() =>
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.GetName().Name.StartsWith("Aspid.MVVM", StringComparison.Ordinal))
                .Where(assembly => !assembly.GetName().Name.Contains("Tests", StringComparison.Ordinal))
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false })
                .Where(type => typeof(IBinder).IsAssignableFrom(type))
                .Where(type => !typeof(UnityEngine.Object).IsAssignableFrom(type))
                .Where(type => type.BaseType is not null);

        private static string Name(Type type)
        {
            if (!type.IsGenericType) return type.Name;

            var name = type.Name[..type.Name.IndexOf('`')];
            return $"{name}<{string.Join(", ", type.GetGenericArguments().Select(Name))}>";
        }
    }
}
