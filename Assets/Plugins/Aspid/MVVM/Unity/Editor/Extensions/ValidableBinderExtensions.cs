#nullable enable
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Write summary
    public static class ValidableBinderExtensions
    {
        private const BindingFlags BindingFlags =
            System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.NonPublic;
        
        /// <summary>
        /// Retrieves field in the specified type that is of type `IMonoBinderValidable` or `IMonoBinderValidable[]` by id.
        /// </summary>
        /// <param name="view">The type to inspect for `IMonoBinderValidable` fields.</param>
        /// <param name="id">The name of fields.</param>
        /// <returns>
        /// FieldInfo` objects representing the field that contain `IMonoBinderValidable` binders.
        /// </returns>
        public static MonoBinderValidableFieldInfo? GetMonoBinderValidableFieldById(this IView view, string id) =>
            GetMonoBinderValidableFields(view).FirstOrDefault(field => field?.GetBinderId() == id);
        
        /// <summary>
        /// Retrieves all fields in the specified type that are of type `IMonoBinderValidable` or `IMonoBinderValidable[]`.
        /// Includes fields from base classes as well.
        /// </summary>
        /// <param name="view">The type to inspect for `IMonoBinderValidable` fields.</param>
        /// <returns>
        /// A collection of `FieldInfo` objects representing the fields that contain `IMonoBinderValidable` binders.
        /// </returns>
        public static IEnumerable<MonoBinderValidableFieldInfo> GetMonoBinderValidableFields(this IView view)
        {
            return view.GetType().GetFieldInfosIncludingBaseClasses(BindingFlags).Where(field =>
            {
                var fieldType = field.FieldType;
                return typeof(IMonoBinderValidable).IsAssignableFrom(fieldType)
                    || typeof(IMonoBinderValidable[]).IsAssignableFrom(fieldType);
            }).Select(fieldInfo => new MonoBinderValidableFieldInfo(fieldInfo));
        }
        
        // TODO Write summary
        public static bool TryGetMonoBinderValidableFieldById(this IView view, string id, out MonoBinderValidableFieldInfo? field)
        {
            field = GetMonoBinderValidableFieldById(view, id);
            return field is not null;
        }
    }
}