using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools.Samples.ProfilerMarkers
{
    public class MarkerTest : MonoBehaviour
    {
        private void Update()
        {
            DoSomething1();
            DoSomething2();
        }

        private void DoSomething1()
        {
            using var _ = this.Marker();
            Load();
        }

        private void DoSomething2()
        {
            using (this.Marker())
            {
                Load();
                using var _ = this.Marker().WithName("Calculate");
                Load();
            }    
        }

        private static void Load()
        {
            object number = 0;

            for (var i = 0; i < 100000; i++)
            {
                if (number is int intNumber)
                    number = intNumber + 1;
            }
        }
    }
}
