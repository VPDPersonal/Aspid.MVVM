namespace Aspid.Collections.Observable.Extensions
{
    public static class ObservableListExtensions
    {
        public static void Swap<T>(this ObservableList<T> list, int index1, int index2)
        {
            var direction = index2 - index1;
            if (direction is 0) return;
            
            var coefficient = direction < 0 ? 1 : -1;
            
            list.Move(index1, index2);
            list.Move(index2 + coefficient, index1);
        }
    }
}