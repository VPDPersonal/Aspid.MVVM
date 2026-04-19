using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    [Flags]
    public enum TypeAllow
    {
        None = 0,
        Abstract = 1,
        Interface = 2,
        All = Abstract | Interface
    }
}
