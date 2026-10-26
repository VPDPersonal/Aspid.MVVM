#nullable enable
using System;
using System.Threading;
using NUnit.Framework;
using System.Globalization;
using NUnit.Framework.Interfaces;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Runs a test, or every test of a fixture, with the thread's <see cref="CultureInfo.CurrentCulture"/> set to the
    /// named culture, and restores the culture afterwards.
    /// </summary>
    /// <remarks>
    /// Stands in for NUnit's <see cref="SetCultureAttribute"/>, which the Unity Test Framework accepts but never
    /// applies: its execution context keeps the culture without handing it to the thread, so a test marked
    /// <c>[SetCulture("de-DE")]</c> runs under whatever culture the Editor started with.
    /// A method-level attribute wins over a fixture-level one. The framework calls the after-test hooks in the same
    /// order as the before-test ones rather than in reverse, so the culture the outermost attribute found is restored
    /// once the last one is done, instead of each attribute restoring its own.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class UseCultureAttribute : Attribute, ITestAction
    {
        private static int _depth;
        private static CultureInfo? _original;

        private readonly CultureInfo _culture;

        /// <param name="name">The culture name, such as <c>"de-DE"</c>; an empty string is the invariant culture.</param>
        public UseCultureAttribute(string name)
        {
            _culture = CultureInfo.GetCultureInfo(name);
        }

        /// <inheritdoc/>
        public ActionTargets Targets => ActionTargets.Test;

        /// <inheritdoc/>
        public void BeforeTest(ITest test)
        {
            if (_depth++ is 0)
                _original = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = _culture;
        }

        /// <inheritdoc/>
        public void AfterTest(ITest test)
        {
            if (--_depth is not 0) return;

            Thread.CurrentThread.CurrentCulture = _original!;
            _original = null;
        }
    }
}
