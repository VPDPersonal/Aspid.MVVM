using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types
{
    /// <summary>
    /// Flags describing which special type categories the type picker should include
    /// in addition to plain concrete classes.
    /// </summary>
    [Flags]
    public enum TypeAllow
    {
        /// <summary>
        /// Only concrete, non-abstract, non-interface types are allowed.
        /// </summary>
        None = 0,

        /// <summary>
        /// Abstract classes are allowed in addition to concrete ones.
        /// </summary>
        Abstract = 1,

        /// <summary>
        /// Interfaces are allowed in addition to concrete classes.
        /// </summary>
        Interface = 2,

        /// <summary>
        /// Both abstract classes and interfaces are allowed.
        /// </summary>
        All = Abstract | Interface
    }
}
