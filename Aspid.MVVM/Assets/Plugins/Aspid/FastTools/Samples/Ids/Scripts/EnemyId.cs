using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Ids
{
    // Declaring `partial struct : IId` triggers IdStructGenerator, which emits __stringId, _id,
    // and the Id property — so consumers only ever write the one-liner below.
    [Serializable]
    public partial struct EnemyId : IId { }
}
