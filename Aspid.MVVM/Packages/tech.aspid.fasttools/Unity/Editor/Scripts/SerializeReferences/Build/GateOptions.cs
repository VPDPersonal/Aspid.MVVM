// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Which checks the gate runs.
    /// </summary>
    internal readonly struct GateOptions
    {
        public readonly bool ScanMissingTypes;
        public readonly bool ScanRequiredFields;

        public static GateOptions Full =>
            new(true, true);

        public static GateOptions MissingOnly =>
            new(true, false);

        public static GateOptions RequiredOnly =>
            new(false, true);

        private GateOptions(bool scanMissingTypes, bool scanRequiredFields)
        {
            ScanMissingTypes = scanMissingTypes;
            ScanRequiredFields = scanRequiredFields;
        }
    }
}
