// Lives in the test assembly on purpose: it verifies the generator inside Unity itself,
// not just in Roslyn unit tests. The chosen property has no hand-written binder in the
// package, so the generated one is never shadowed.
[assembly: Aspid.MVVM.StarterKit.GenerateBinders(
    typeof(UnityEngine.UI.CanvasScaler), "referencePixelsPerUnit",
    Prefix = "GeneratedCanvasScalerReferencePixels",
    Menu = "Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – Reference Pixels Per Unit",
    SerializedName = "m_ReferencePixelsPerUnit")]
