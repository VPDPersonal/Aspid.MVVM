using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    // A composite over one declared bit and one undeclared one: the declared part is consumed first
    // and the composite never matches what is left.
    [Flags]
    internal enum Signal
    {
        None = 0,
        Ping = 1,
        PingAndPong = 3,
    }
}
