#nullable enable
using System.Linq;
using Aspid.FastTools;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for required binder fields on <see cref="IView"/>.
    /// </summary>
    public static class RequiredFieldForMonoBinderExtensions
    {
        private const BindingFlags BindingFlags =
            System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.NonPublic;
        
        /// <summary>
        /// Finds the first required binder field with the given identifier.
        /// </summary>
        /// <param name="view">The view to inspect; <see langword="null"/> has no fields.</param>
        /// <param name="id">The binding identifier.</param>
        /// <param name="field">The matching field; <see langword="null"/> when absent.</param>
        /// <returns><see langword="true"/> if a matching field is found; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetRequireBinderFieldsById(this IView view, string id, out RequiredFieldForMonoBinder? field)
        {
            field = GetRequireBinderFieldById(view, id);
            return field is not null;
        }
        
        /// <summary>
        /// Finds the first required binder field with the given identifier.
        /// </summary>
        /// <param name="view">The view to inspect; <see langword="null"/> has no fields.</param>
        /// <param name="id">The binding identifier.</param>
        /// <returns>The matching field; otherwise, <see langword="null"/>.</returns>
        public static RequiredFieldForMonoBinder? GetRequireBinderFieldById(this IView view, string id) =>
            GetRequireBinderFields(view).FirstOrDefault(field => field.Id == id);

        /// <summary>
        /// Collects required binder fields, including nested fields.
        /// </summary>
        /// <param name="view">The view to inspect; <see langword="null"/> has no fields.</param>
        /// <returns>The required fields in depth-first order.</returns>
        public static IEnumerable<RequiredFieldForMonoBinder> GetRequireBinderFields(this IView view)
        {
            if (view is null)
                return Enumerable.Empty<RequiredFieldForMonoBinder>();
            
            var fields = view.GetType()
                .GetMembersInfosIncludingBaseClasses(BindingFlags)
                .OfType<FieldInfo>()
                .Where(RequiredFieldForMonoBinder.IsRequireBinderField)
                .Select(field => new RequiredFieldForMonoBinder(view, field));

            return GetRequireBinderFieldsInternal(fields);
        }

        private static IEnumerable<RequiredFieldForMonoBinder> GetRequireBinderFieldsInternal(IEnumerable<RequiredFieldForMonoBinder> fields)
        {
            var list = new List<RequiredFieldForMonoBinder>();

            foreach (var field in fields)
            {
                list.Add(field);
                list.AddRange(GetRequireBinderFieldsInternal(field.Children));
            }

            return list;
        }
    }
}