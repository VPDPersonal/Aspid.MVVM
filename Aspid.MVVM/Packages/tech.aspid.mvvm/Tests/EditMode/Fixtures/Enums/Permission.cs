using System;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    // A composite over two bits neither of which is a member of its own, so nothing can consume them
    // before the composite is reached.
    [Flags]
    internal enum Permission
    {
        None = 0,
        ReadWrite = 3,
        Execute = 4,
    }
}
