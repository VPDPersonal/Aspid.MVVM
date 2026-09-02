#nullable enable
using Aspid.MVVM.Validation;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Replace array with ImmutableArray
    /// <summary>
    /// A dictionary mapping binder IDs to their corresponding <see cref="IMonoBinderValidatable"/> slot arrays,
    /// built from a view's required binder fields.
    /// </summary>
    public sealed class ValidatableBindersById : Dictionary<string, IMonoBinderValidatable?[]>
    {
        /// <summary>
        /// Retrieves all `IMonoBinderValidatable` binders from a view and associates them with the field names they are assigned to.
        /// </summary>
        /// <param name="view">The view object containing the binders.</param>
        /// <returns>
        /// A dictionary where the key is the field name and the value is an array of `IMonoBinderValidatable` associated with that field.
        /// </returns>
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