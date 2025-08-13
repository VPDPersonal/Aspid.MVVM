#nullable enable
using System;
using System.Collections.Generic;

namespace Aspid.MVVM.Unity
{
    // TODO Replace array with ImmutableArray
    public sealed class ValidableBindersById : Dictionary<string, IMonoBinderValidable?[]>
    {
        /// <summary>
        /// Retrieves all `IMonoBinderValidable` binders from a view and associates them with the field names they are assigned to.
        /// </summary>
        /// <param name="view">The view object containing the binders.</param>
        /// <returns>
        /// A dictionary where the key is the field name and the value is an array of `IMonoBinderValidable` associated with that field.
        /// </returns>
        public static ValidableBindersById GetValidableBindersById(IMonoBinderSource view)
        {
            var fields = view.GetMonoBinderValidableFields();
            var bindersByFieldName = new ValidableBindersById();

            foreach (var field in fields)
            {
                var viewBinders = field?.GetValueAsArray<IMonoBinderValidable>(view) ?? Array.Empty<IMonoBinderValidable>();
                
                if (viewBinders is { Length: > 0 })
                {
                    var copyViewBinders = new IMonoBinderValidable[viewBinders.Length];
                    viewBinders.CopyTo(copyViewBinders, 0);
                    
                    bindersByFieldName.Add(field.Name, copyViewBinders);
                }
                else
                {
                    bindersByFieldName.Add(field.Name, viewBinders);
                }
            }
            
            return bindersByFieldName;
        }
    }
}