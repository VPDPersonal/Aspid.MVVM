using System.Runtime.CompilerServices;

// The vector clamps put a pair the Inspector lets a user type backwards into the right order, and
// that ordering is worth a test of its own rather than one that reaches it through a converted
// vector. The helpers doing it are not API and stay internal; the test assembly is the only thing
// that needs to name them.
[assembly: InternalsVisibleTo("Aspid.MVVM.Tests.EditMode")]
