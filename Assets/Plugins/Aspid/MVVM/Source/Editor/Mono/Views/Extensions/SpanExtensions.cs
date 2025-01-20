using System;

namespace Aspid.MVVM.Mono
{
    public static class SpanExtensions
    {
        public static bool Contains<T>(this Span<T> span, T element)
        {
            foreach (var currentElement in span)
            {
                if (currentElement.Equals(element))
                    return true;
            }

            return false;
        }
        
        public static bool Contains<T>(this ReadOnlySpan<T> span, T element)
        {
            foreach (var currentElement in span)
            {
                if (currentElement.Equals(element))
                    return true;
            }

            return false;
        }
    }
}