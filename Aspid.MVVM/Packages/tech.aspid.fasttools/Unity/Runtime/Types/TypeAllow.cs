using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types
{
    /// <summary>
    /// Flags describing which special type categories the type picker should include
    /// in addition to plain concrete classes.
    /// </summary>
    /// <seealso cref="TypeSelectorAttribute"/>
    [Flags]
    public enum TypeAllow
    {
        /// <summary>
        /// Only concrete types are offered.
        /// </summary>
        None = 0,

        /// <summary>
        /// Abstract classes are allowed in addition to concrete types.
        /// </summary>
        Abstract = 1,

        /// <summary>
        /// Interfaces are allowed in addition to concrete types.
        /// </summary>
        Interface = 2,

        /// <summary>
        /// Both abstract classes and interfaces are allowed.
        /// </summary>
        All = Abstract | Interface
    }
}
