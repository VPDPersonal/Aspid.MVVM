using System.Runtime.CompilerServices;

// The AOT hints are infrastructure, not API — nothing outside the assembly should name them. The
// test that checks a scene cannot ask for an instantiation nobody seeded has to read the list, and
// that is the only reason it is visible at all.
[assembly: InternalsVisibleTo("Aspid.MVVM.StarterKit.Tests")]
