using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.ProfilerMarkers
{
    public sealed class FrameProfiler : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _physicsLoad = 5000;
        [SerializeField] [Min(1)] private int _aiAgents = 20;
        [SerializeField] [Min(1)] private int _aiStepsPerAgent = 500;
        [SerializeField] [Min(1)] private int _renderLoad = 3000;
        [SerializeField] [Min(1)] private int _inputLoad = 1500;
        [SerializeField] [Min(1)] private int _audioLoad = 2000;

        private void Update()
        {
            // Every call site of this.Marker() becomes a unique ProfilerMarker.
            // Display name: "{TypeName}.{WithName-or-method} ({line})".
            using (this.Marker().WithName("Physics")) // Profiler: FrameProfiler.Physics (18)
                SimulatePhysics();

            using (this.Marker().WithName("AI")) // Profiler: FrameProfiler.AI (21)
                SimulateAI();

            using (this.Marker().WithName("Render")) // Profiler: FrameProfiler.Render (24)
                SimulateRender();

            SimulateInput();
            SimulateAudio();
        }

        private void SimulatePhysics()
        {
            var sum = 0f;
            for (var i = 0; i < _physicsLoad; i++)
                sum += Mathf.Sqrt(i);
            _ = sum;
        }

        private void SimulateAI()
        {
            for (var agent = 0; agent < _aiAgents; agent++)
            {
                using (this.Marker().WithName("AI.Agent"))  // Profiler: FrameProfiler.AI.Agent (43)
                    StepAgent();
            }
        }

        private void StepAgent()
        {
            var sum = 0f;
            for (var i = 0; i < _aiStepsPerAgent; i++)
                sum += Mathf.Sin(i);
            _ = sum;
        }

        private void SimulateRender()
        {
            var sum = 0f;
            for (var i = 0; i < _renderLoad; i++)
                sum += Mathf.Cos(i);
            _ = sum;
        }

        // using-declaration without .WithName(): auto-named after the method.
        private void SimulateInput()
        {
            using var _ = this.Marker(); // Profiler: FrameProfiler.SimulateInput (67)
            DoInputWork();
        }

        private void DoInputWork()
        {
            var sum = 0f;
            for (var i = 0; i < _inputLoad; i++)
                sum += Mathf.Tan(i);
            _ = sum;
        }

        // Combined form: outer method-wide using-declaration + nested using-statement.
        // Both auto-named after the method; distinct because their line numbers differ.
        private void SimulateAudio()
        {
            using var _ = this.Marker(); // Profiler: FrameProfiler.SimulateAudio (83)
            PrepareAudio();

            using (this.Marker()) // Profiler: FrameProfiler.SimulateAudio (86)
                MixAudio();
        }

        private void PrepareAudio()
        {
            var sum = 0f;
            for (var i = 0; i < _audioLoad; i++)
                sum += Mathf.Sqrt(i);
            _ = sum;
        }

        private void MixAudio()
        {
            var sum = 0f;
            for (var i = 0; i < _audioLoad; i++)
                sum += Mathf.Cos(i);
            _ = sum;
        }
    }
}
