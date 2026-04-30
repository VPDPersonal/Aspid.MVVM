#nullable enable
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Aspid.FastTools.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Extension methods for <see cref="IView"/> that enumerate and look up
    /// <see cref="RequiredFieldForMonoBinder"/> entries by binding ID.
    /// </summary>
    public static class RequiredFieldForMonoBinderExtensions
    {
        private const BindingFlags BindingFlags =
            System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.NonPublic;
        
        public static bool TryGetRequireBinderFieldsById(this IView view, string id, out RequiredFieldForMonoBinder? field)
        {
            field = GetRequireBinderFieldById(view, id);
            return field is not null;
        }
        
        public static RequiredFieldForMonoBinder? GetRequireBinderFieldById(this IView view, string id) =>
            GetRequireBinderFields(view).FirstOrDefault(field => field.Id == id);

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