using System;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ShowDesignViewModelAttribute : Attribute
    {
        public readonly Type[] Types;
        public readonly bool StrictType;
        
        public ShowDesignViewModelAttribute()
        {
            Types = new[] { typeof(IViewModel) };
        }
        
        public ShowDesignViewModelAttribute(params Type[] types)
        {
            var hasViewModel = types.Any(type => typeof(IViewModel).IsAssignableFrom(type));

            Types = hasViewModel
                ? types
                : types.Append(typeof(IViewModel)).ToArray();
        }

        public ShowDesignViewModelAttribute(Type type, bool strictType = false)
        {
            StrictType = strictType;
            var implementsViewModel = typeof(IViewModel).IsAssignableFrom(type);
            
            if (strictType && !implementsViewModel)
                throw new ArgumentException($"Type {type.Name} does not implement IViewModel, but strictType is set to true.", nameof(type));
            
            Types = implementsViewModel 
                ? new[] { type }
                : new[] { type, typeof(IViewModel) };
        }
    }
}