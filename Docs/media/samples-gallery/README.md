# Samples gallery media

The cards of `/tutorials` (`Docs/src/components/SamplesGallery`) show a looping recording of each sample scene per site
theme: `Docs/static/img/samples/<slug>.gif` (dark) and `<slug>-light.gif` (light). Every pair shares one 16:9 crop,
start frame and driven interaction, so the two line up frame for frame: 1024×576, 20 fps, 256 colours, sierra2_4a
dithering, looping, as the Aspid.FastTools gallery (`Aspid.FastTools/Website/media/samples-gallery`).

The samples have no theme of their own; `record.cs` makes both at record time, in Play Mode, without saving a scene.
The flat background is exactly rgb(14,16,21) in dark and rgb(238,240,243) in light (the keys of the site's
`sample-scene-background-*` filters in `src/theme/Root`), and `encode.py` pins it in the GIF palette. The light theme
flips each Graphic's HSL lightness and keeps saturated accents readable; saturated button faces keep white labels and
near-white fields stay white.

## Re-recording

1. Make a throwaway Unity project: a copy of `Aspid.MVVM/` with a `cp -c` clone of a warm `Library/` (Unity 6000.6),
   and the `Samples~` folders copied with their `.meta` files into `Assets/Samples/`. Never use the project the user
   has open.
2. Start a hidden batch-mode Editor on it with the `asp-unity-capture` skill (`editor.sh open <project>`).
3. `CAPTURE_PROJECT=<project> sh all.sh [slug…]` records every sample of `all.txt` in both themes into
   `<project>/Temp/rec/<slug>-<dark|light>/` (`rec.sh` opens the scene with `open.cs`, enters Play Mode and runs the
   recorder and per-sample driver in `record.cs` through `run.sh`).
4. `python3 encode.py <project>/Temp/rec Docs/static/img/samples [slug…]` crops, encodes and pins the background
   (`CUT_ROWS` first removes the empty band of Todo List's tall column).

Notes: the Zenject DI scene is not recorded (the dev project has no Zenject), `di-integration` is the VContainer
Wallet scene; ExampleScripts has no scene. In a batch Editor `WaitForEndOfFrame` coroutines never resume, so the
recorder drives VirtualizedList's layout itself. A Screen Space Overlay canvas does not render through
`Camera.Render`: the recorder sets `worldCamera` first, then `renderMode = ScreenSpaceCamera`.
