#nullable enable
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM.Mono
{
    // TODO Write summary
    public static class RequireBinderExtensions
    {
        /// <summary>
        /// Checks if the given binder matches the required types specified by a collection of RequireBinderAttribute.
        /// This method extracts the types from the attributes and delegates the type-checking logic to the overloaded method.
        /// </summary>
        /// <param name="attributes">A collection of RequireBinderAttribute, which contain the required types.</param>
        /// <param name="binder">The binder object to check.</param>
        /// <returns>True if the binder matches any of the required types; otherwise, false.</returns>
        public static bool IsBinderMatchRequiredType(this IEnumerable<RequireBinderAttribute> attributes, object binder) =>
            IsBinderMatchRequiredType(attributes.Select(attribute => attribute.Type), binder);
        
        /// <summary>
        /// Checks if the given binder implements an interface (either IBinder or IReverseBinder) 
        /// whose generic type argument is compatible with any of the required types.
        /// </summary>
        /// <param name="requiredTypes">A collection of required types to match against the binder's interfaces.</param>
        /// <param name="binder">The binder object to check.</param>
        /// <returns>True if the binder matches any of the required types; otherwise, false.</returns>
        public static bool IsBinderMatchRequiredType(this IEnumerable<Type> requiredTypes, object binder)
        {
            var result = binder.GetType().GetInterfaces().Any(@interface =>
            {
                if (!@interface.IsGenericType) return false;
                if (@interface.GetGenericTypeDefinition() != typeof(IBinder<>) 
                    && @interface.GetGenericTypeDefinition() != typeof(IReverseBinder<>)) return false;
                
                return requiredTypes.Any(requiredType =>
                    @interface.GetGenericArguments()[0].IsAssignableFrom(requiredType));
            });

            if (!result && !requiredTypes.Any()) return true;
            return result;
        }
        
        // TODO Write summary
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetRequiredTypes(this FieldInfo field)
        {
            var requireAttributes = field.GetCustomAttributes<RequireBinderAttribute>(false);
            return requireAttributes.Select(attribute => attribute.Type);
        }
    }
}