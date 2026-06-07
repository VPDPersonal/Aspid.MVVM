// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal readonly struct AddRowValidation
    {
        public readonly bool IsValid;
        public readonly string Error;

        private AddRowValidation(bool isValid, string error)
        {
            IsValid = isValid;
            Error = error;
        }

        public static AddRowValidation Valid() =>
            new(isValid: true, error: null);

        public static AddRowValidation Invalid(string error) =>
            new(isValid: false, error: error);
    }
}
