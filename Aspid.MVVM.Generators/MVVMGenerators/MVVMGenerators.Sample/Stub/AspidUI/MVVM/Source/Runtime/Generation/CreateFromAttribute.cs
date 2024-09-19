using System;

namespace AspidUI.MVVM.Generation;

// TODO Move To UnityFastTools
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class CreateFromAttribute : Attribute
{
    public CreateFromAttribute(Type type) { }
}