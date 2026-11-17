#nullable enable
using Aspid.MVVM.Validation;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Replace array with ImmutableArray
    /// <summary>
    /// <see cref="Dictionary{TKey, TValue}"/> of binder slots keyed by binding identifier.
    /// </summary>
    public sealed class ValidatableBindersById : Dictionary<string, IMonoBinderValidatable?[]>
    {
        /// <summary>
        /// Collects the validatable binder slots from a View.
        /// </summary>
        /// <param name="view">The View containing the slots.</param>
        /// <returns>The slots grouped by binding identifier.</returns>
        public static ValidatableBindersById GetValidatableBindersById(IView view)
        {
            var fields = view.GetRequireBinderFields();
            var bindersByFieldName = new ValidatableBindersById();

            foreach (var field in fields)
            {
                if (!field.IsValidation()) continue;
                var viewBinders = field.GetValueAsArray<IMonoBinderValidatable>(field.FieldContainerObj);
                
                if (viewBinders is { Length: > 0 })
                {
                    var copyViewBinders = new IMonoBinderValidatable[viewBinders.Length];
                    viewBinders.CopyTo(copyViewBinders, 0);
                    
                    bindersByFieldName.Add(field.Id, copyViewBinders);
                }
                else
                {
                    bindersByFieldName.Add(field.Id, viewBinders);
                }
            }
            
            return bindersByFieldName;
        }
    }
}