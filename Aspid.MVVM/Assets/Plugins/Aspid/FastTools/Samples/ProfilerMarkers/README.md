# ProfilerMarkers Sample

Demonstrates the three supported usage forms of `this.Marker()`. In every
form the Aspid.FastTools source generator replaces each call site with a
unique `ProfilerMarker` keyed by `(type, method, line)`. `.WithName(...)`
is optional — when omitted, the generator auto-names the marker after the
enclosing type and method.

## Supported forms

1. **Block `using`-statement with an explicit name.** Marker scope is the
   block; the full display name is `"{TypeName}.{WithName} ({line})"`, so
   pass the short suffix only.
   ```csharp
   using (this.Marker().WithName("Physics")) // Profiler: "FrameProfiler.Physics (<line>)"
       SimulatePhysics();
   ```
2. **`using`-declaration without `WithName`.** Marker scope is the rest of
   the enclosing block; the generator auto-names the marker after the
   enclosing method.
   ```csharp
   using var _ = this.Marker(); // Profiler: "FrameProfiler.SimulateInput (<line>)"
   ```
3. **Combined form.** A method-wide `using`-declaration paired with a
   nested `using`-statement — useful when you want one outer marker for the
   whole method and a narrower marker around a hot sub-step. Both get
   auto-named after the method; their different line numbers produce
   distinct Profiler entries.
   ```csharp
   using var _ = this.Marker(); // Profiler: "FrameProfiler.SimulateAudio (<outer-line>)"
   using (this.Marker())        // Profiler: "FrameProfiler.SimulateAudio (<inner-line>)"
   {
       // Some code.
   }
   ```

## How to run

1. Open `Scenes/ProfilerMarkers.unity` — it already contains a `FrameProfiler`
   GameObject. The `Prefabs/ProfilerMarkers.prefab` variant can be dropped
   into your own scenes.
2. Open `Window → Analysis → Profiler`.
3. Enter Play Mode and inspect the CPU track for the named markers.

## Where to look

- `Scripts/FrameProfiler.cs:18,21,24` — three top-level markers with explicit
  names in `Update` (`FrameProfiler.Physics`, `FrameProfiler.AI`,
  `FrameProfiler.Render`).
- `Scripts/FrameProfiler.cs:43` — nested `FrameProfiler.AI.Agent` marker
  emitted once per loop iteration in `SimulateAI`, appearing under the `AI`
  scope.
- `Scripts/FrameProfiler.cs:67` — `using`-declaration form without `WithName`
  in `SimulateInput`; the generator names the marker after the method.
- `Scripts/FrameProfiler.cs:83,86` — combined form in `SimulateAudio`: an
  outer method-wide `using`-declaration plus a nested `using`-statement
  around `MixAudio()`.

Every `using` scope (statement or declaration) starts and ends the
generated marker automatically, so the Profiler shows precise self-time
for every phase.
