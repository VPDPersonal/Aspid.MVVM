using System.Collections.Generic;

namespace Aspid.MVVM.Mono
{
    // TODO Replace array with ImmutableArray
    public sealed class ValidableBindersById : Dictionary<string, IMonoBinderValidable[]> { }
}