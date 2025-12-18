// using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class NumberRestrictions
    {
        public static bool CalculateMinAndMax(IFieldContext context, ref double min, ref double max)
        {
            if (// context.IsDefined(typeof(RangeAttribute)) ||
                context.IsDefined(typeof(UnityEngine.RangeAttribute)))
            {
                // if (context.IsDefined(typeof(RangeAttribute)))
                // {
                //     var rangeAttribute = context.GetCustomAttribute<RangeAttribute>();
                //     max = Math.Min(max, rangeAttribute.Max);
                //     min = Math.Min(Math.Max(min, rangeAttribute.Min), (float)max);
                // }
                // else
                {
                    var rangeAttribute = context.GetCustomAttribute<UnityEngine.RangeAttribute>();
                    max = Mathf.Min((float)max, rangeAttribute.max);
                    min = Mathf.Min(Mathf.Max((float)min, rangeAttribute.min), (float)max);
                }

                return true;
            }
            
            var isMin = false;
            var isMax = false;
                
            // if (context.IsDefined(typeof(MinAttribute)))
            // {
            //     isMin = true;
            //     var minAttribute = context.GetCustomAttribute<MinAttribute>();
            //     min = Math.Min(Math.Max(min, minAttribute.Min), max);
            // }
            // else
            if (context.IsDefined(typeof(UnityEngine.MinAttribute)))
            {
                isMin = true;
                var minAttribute = context.GetCustomAttribute<UnityEngine.MinAttribute>();
                min = Mathf.Min(Mathf.Max((float)min, minAttribute.min), (float)max);
            }

            // if (context.IsDefined(typeof(MaxAttribute)))
            // {
            //     isMax = true;
            //     var maxAttribute = context.GetCustomAttribute<MaxAttribute>();
            //     max = Math.Min(max, maxAttribute.Max);
            // }

            return isMin && isMax;
        }
    }
}
