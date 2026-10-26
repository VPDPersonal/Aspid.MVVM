using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

// Play Mode recorder for the Aspid.MVVM samples gallery. Reads "<slug> <Dark|Light>" from Temp/rec-args.txt,
// themes the scene at runtime (never saved), drives the scene's own controls and writes 1440x810 PNG frames at 20 fps
// into Temp/rec/<slug>-<theme>/.
[DefaultExecutionOrder(10000)]
public sealed class MvvmRec : MonoBehaviour
{
    private static readonly Color DarkBg = new Color32(14, 16, 21, 255);
    private static readonly Color LightBg = new Color32(238, 240, 243, 255);

    private static string Out, Slug, Theme;
    private Camera _cam;
    private RenderTexture _rt;
    private Texture2D _img;
    private int _frame = -4;
    private bool _done;
    private int _prevRate;
    private readonly Dictionary<Graphic, Color> _set = new();
    private readonly HashSet<Graphic> _backgrounds = new();

    public static object Main()
    {
        if (!EditorApplication.isPlaying) throw new Exception("Play Mode required");
        var proj = Path.GetDirectoryName(Application.dataPath);
        var a = File.ReadAllText(Path.Combine(proj, "Temp", "rec-args.txt")).Trim().Split(' ');
        Slug = a[0]; Theme = a[1];
        Out = Path.Combine(proj, "Temp", "rec", Slug + "-" + Theme.ToLowerInvariant());
        if (Directory.Exists(Out)) Directory.Delete(Out, true);
        Directory.CreateDirectory(Out);
        new GameObject("MvvmRec").AddComponent<MvvmRec>();
        return Out;
    }

    private bool Light => Theme == "Light";
    private Color Bg => Light ? LightBg : DarkBg;

    private void Awake()
    {
        _cam = Camera.main;
        _cam.clearFlags = CameraClearFlags.SolidColor;
        _cam.backgroundColor = Bg;
        _prevRate = Time.captureFramerate;
        Time.captureFramerate = 20;
        _rt = new RenderTexture(1440, 810, 24, RenderTextureFormat.ARGB32) { antiAliasing = 4 };
        _rt.Create();
        _cam.targetTexture = _rt;
        _img = new Texture2D(1440, 810, TextureFormat.RGB24, false);
        foreach (var c in FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (!c.isRootCanvas) continue;
            if (c.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                c.worldCamera = _cam;
                c.renderMode = RenderMode.ScreenSpaceCamera;
                if (c.renderMode != RenderMode.ScreenSpaceCamera) Debug.LogWarning("renderMode did not stick");
                c.planeDistance = _cam.orthographic ? 10f : 100f;
            }
            if (c.renderMode == RenderMode.ScreenSpaceCamera) c.worldCamera = _cam;
            // Full-screen background images get the exact theme background.
            foreach (var img in c.GetComponentsInChildren<Image>(true))
            {
                var rt = img.rectTransform;
                if (rt.parent != c.transform || rt.anchorMin != Vector2.zero || rt.anchorMax != Vector2.one) continue;
                if (rt.sizeDelta.sqrMagnitude > 1f) continue;
                if (img.GetComponent<Selectable>() || img.GetComponentsInChildren<Graphic>(true).Length > 1) continue;
                img.sprite = null;
                img.color = Bg;
                _backgrounds.Add(img);
            }
        }
        Prepare();
        Recolor();
        Canvas.ForceUpdateCanvases();
        StartCoroutine(Safe(Drive()));
    }

    // ---- theme ----

    private static void ToHsl(Color c, out float h, out float s, out float l)
    {
        float max = Mathf.Max(c.r, Mathf.Max(c.g, c.b)), min = Mathf.Min(c.r, Mathf.Min(c.g, c.b));
        l = (max + min) / 2f;
        if (max - min < 1e-5f) { h = 0; s = 0; return; }
        var d = max - min;
        s = l > 0.5f ? d / (2f - max - min) : d / (max + min);
        if (max == c.r) h = (c.g - c.b) / d + (c.g < c.b ? 6f : 0f);
        else if (max == c.g) h = (c.b - c.r) / d + 2f;
        else h = (c.r - c.g) / d + 4f;
        h /= 6f;
    }

    private static float Hue(float p, float q, float t)
    {
        if (t < 0) t += 1; if (t > 1) t -= 1;
        if (t < 1f / 6f) return p + (q - p) * 6f * t;
        if (t < 0.5f) return q;
        if (t < 2f / 3f) return p + (q - p) * (2f / 3f - t) * 6f;
        return p;
    }

    private static Color FromHsl(float h, float s, float l, float a)
    {
        if (s <= 0) return new Color(l, l, l, a);
        var q = l < 0.5f ? l * (1 + s) : l + s - l * s;
        var p = 2 * l - q;
        return new Color(Hue(p, q, h + 1f / 3f), Hue(p, q, h), Hue(p, q, h - 1f / 3f), a);
    }

    private static Color Flip(Color c)
    {
        ToHsl(c, out var h, out var s, out var l);
        l = 1f - l;
        if (s > 0.35f) l = Mathf.Clamp(l, 0.30f, 0.45f);
        return FromHsl(h, s, l, c.a);
    }

    private readonly Dictionary<Graphic, Color> _authored = new();
    private readonly Dictionary<Sprite, Color> _texels = new();

    // The colour of the sprite's centre texel: builtin UI sprites are not white, so the tint alone is not what shows.
    private Color Texel(Graphic g)
    {
        if (g is not Image img || img.sprite == null) return Color.white;
        if (_texels.TryGetValue(img.sprite, out var known)) return known;
        var tex = img.sprite.texture;
        var r = img.sprite.textureRect;
        var tmp = RenderTexture.GetTemporary(tex.width, tex.height, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(tex, tmp);
        var prev = RenderTexture.active;
        RenderTexture.active = tmp;
        var one = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        one.ReadPixels(new Rect((int)(r.x + r.width / 2), (int)(r.y + r.height / 2), 1, 1), 0, 0);
        one.Apply();
        var c = one.GetPixel(0, 0);
        RenderTexture.active = prev;
        RenderTexture.ReleaseTemporary(tmp);
        Destroy(one);
        if (c.maxColorComponent < 0.05f) c = Color.white;
        _texels[img.sprite] = c;
        return c;
    }

    private Color Shown(Graphic g, Color tint)
    {
        var t = Texel(g);
        return new Color(tint.r * t.r, tint.g * t.g, tint.b * t.b, tint.a);
    }

    private Color Authored(Graphic g) =>
        _authored.TryGetValue(g, out var c) ? c : g.color;

    // True when the graphic sits on a control surface that keeps its authored colours in the light theme:
    // a saturated button face (its label stays light) or a near-white field or check box (its content stays as is).
    private bool KeepsAuthored(Graphic g)
    {
        var sel = g.GetComponentInParent<Selectable>(true);
        if (sel == null || sel is Slider || sel is Scrollbar || sel.targetGraphic == null) return false;
        var face = sel.targetGraphic;
        if (face is not Image) return false;
        var mask = face.GetComponent<Mask>();
        if (mask != null && !mask.showMaskGraphic) return false;
        if (!g.transform.IsChildOf(face.transform)) return false;
        ToHsl(Shown(face, Authored(face)), out _, out var s, out var l);
        if (s > 0.35f) return g != face;
        return l > 0.85f && s < 0.1f;
    }

    private void Recolor()
    {
        foreach (var g in FindObjectsByType<Graphic>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (_backgrounds.Contains(g)) { if (g.color != Bg) g.color = Bg; continue; }
            if (!Light) continue;
            if (_set.TryGetValue(g, out var last) && g.color == last) continue;
            _authored[g] = g.color;
        }
        if (!Light) return;
        foreach (var g in FindObjectsByType<Graphic>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (_backgrounds.Contains(g)) continue;
            if (_set.TryGetValue(g, out var last) && g.color == last) continue;
            var authored = Authored(g);
            Color c;
            if (KeepsAuthored(g)) c = authored;
            else
            {
                var t = Texel(g);
                var want = Flip(Shown(g, authored));
                c = new Color(Mathf.Clamp01(want.r / Mathf.Max(t.r, 0.01f)), Mathf.Clamp01(want.g / Mathf.Max(t.g, 0.01f)),
                    Mathf.Clamp01(want.b / Mathf.Max(t.b, 0.01f)), authored.a);
            }
            g.color = c;
            _set[g] = c;
        }
    }

    // ---- capture ----

    // VirtualizedList waits for WaitForEndOfFrame, which never fires in a batch-mode Editor: run that step by hand.
    private static void PumpVirtualizedLists()
    {
        foreach (var mb in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
        {
            if (mb.GetType().Name != "VirtualizedList") continue;
            var field = mb.GetType().GetField("_initializing", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field.GetValue(mb) is not Coroutine running) continue;
            mb.StopCoroutine(running);
            var routine = (IEnumerator)mb.GetType().GetMethod("InitializeAsync", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(mb, null);
            while (routine.MoveNext()) { }
        }
    }

    private void LateUpdate()
    {
        PumpVirtualizedLists();
        Recolor();
        if (_frame++ < 0) return;
        Canvas.ForceUpdateCanvases();
        var active = RenderTexture.active;
        try
        {
            _cam.Render();
            RenderTexture.active = _rt;
            _img.ReadPixels(new Rect(0, 0, 1440, 810), 0, 0);
            _img.Apply();
            File.WriteAllBytes(Path.Combine(Out, $"frame-{_frame:000}.png"), _img.EncodeToPNG());
        }
        finally { RenderTexture.active = active; }
        if (_done || _frame >= 400)
        {
            File.WriteAllText(Path.Combine(Out, "done.txt"), (_frame + 1) + " frames at 20 fps");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Time.captureFramerate = _prevRate;
        if (_cam) _cam.targetTexture = null;
        if (_rt) { _rt.Release(); Destroy(_rt); }
        if (_img) Destroy(_img);
    }

    // ---- driver helpers ----

    private static IEnumerator W(float seconds)
    {
        var n = Mathf.RoundToInt(seconds * 20f);
        for (var i = 0; i < n; i++) yield return null;
    }

    // Some sample objects carry the component type in their name ("Earn UnityEngine.UI.Button").
    private static GameObject F(string path) =>
        GameObject.Find(path)
        ?? GameObject.Find(path + " UnityEngine.UI.Button")
        ?? GameObject.Find(path + " UnityEngine.UI.Slider")
        ?? GameObject.Find(path + " UnityEngine.UI.Toggle")
        ?? throw new Exception("Not found: " + path);

    private static PointerEventData Ev() =>
        new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left };

    private static IEnumerator Click(GameObject go)
    {
        var e = Ev();
        ExecuteEvents.Execute(go, e, ExecuteEvents.pointerEnterHandler);
        yield return W(0.1f);
        ExecuteEvents.Execute(go, e, ExecuteEvents.pointerDownHandler);
        yield return W(0.15f);
        ExecuteEvents.Execute(go, e, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.Execute(go, e, ExecuteEvents.pointerClickHandler);
        yield return W(0.1f);
        ExecuteEvents.Execute(go, e, ExecuteEvents.pointerExitHandler);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private static IEnumerator Click(string path) => Click(F(path));

    private static IEnumerator Type(string path, string text, float perChar = 0.12f)
    {
        var field = F(path).GetComponent<TMPro.TMP_InputField>();
        foreach (var ch in text)
        {
            field.text += ch;
            yield return W(perChar);
        }
    }

    private static IEnumerator Erase(string path, float perChar = 0.06f)
    {
        var field = F(path).GetComponent<TMPro.TMP_InputField>();
        while (field.text.Length > 0)
        {
            field.text = field.text.Substring(0, field.text.Length - 1);
            yield return W(perChar);
        }
    }

    private static IEnumerator Slide(string path, float to, float seconds)
    {
        var slider = F(path).GetComponent<Slider>();
        var from = slider.value;
        var n = Mathf.Max(1, Mathf.RoundToInt(seconds * 20f));
        for (var i = 1; i <= n; i++)
        {
            var t = i / (float)n;
            slider.value = Mathf.Lerp(from, to, t * t * (3f - 2f * t));
            yield return null;
        }
    }

    private static IEnumerator Scroll(ScrollRect[] rects, bool vertical, float to, float seconds)
    {
        var from = rects.Select(r => vertical ? r.verticalNormalizedPosition : r.horizontalNormalizedPosition).ToArray();
        var n = Mathf.Max(1, Mathf.RoundToInt(seconds * 20f));
        for (var i = 1; i <= n; i++)
        {
            var t = i / (float)n;
            t = t * t * (3f - 2f * t);
            for (var k = 0; k < rects.Length; k++)
            {
                rects[k].velocity = Vector2.zero;
                if (vertical) rects[k].verticalNormalizedPosition = Mathf.Lerp(from[k], to, t);
                else rects[k].horizontalNormalizedPosition = Mathf.Lerp(from[k], to, t);
            }
            yield return null;
        }
    }

    // ---- per-sample setup and drivers ----

    private void Prepare()
    {
        if (Slug == "virtualized-list")
        {
            // Recreate the items from a fixed seed so both themes show the same rows.
            foreach (var mb in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
            foreach (var f in mb.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (f.FieldType.IsValueType || typeof(UnityEngine.Object).IsAssignableFrom(f.FieldType)) continue;
                var vm = f.GetValue(mb);
                if (vm == null || vm.GetType().Name != "ListViewModel") continue;
                File.AppendAllText(Path.Combine(Out, "prepare.txt"), $"reseeded {mb.name}.{f.Name}\n");
                var t = vm.GetType();
                var items = t.GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(vm);
                var it = items.GetType();
                var count = (int)it.GetProperty("Count").GetValue(items);
                it.GetMethod("Clear", System.Type.EmptyTypes).Invoke(items, null);
                t.GetField("_number", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(vm, 0);
                UnityEngine.Random.InitState(7);
                var create = t.GetMethod("CreateElement", BindingFlags.Instance | BindingFlags.NonPublic);
                var add = it.GetMethods().First(m => m.Name == "Add" && m.GetParameters().Length == 1);
                for (var i = 0; i < count; i++) add.Invoke(items, new[] { create.Invoke(vm, null) });
            }
        }
    }

    private IEnumerator Safe(IEnumerator e)
    {
        var stack = new Stack<IEnumerator>();
        stack.Push(e);
        while (stack.Count > 0)
        {
            bool next;
            try { next = stack.Peek().MoveNext(); }
            catch (Exception ex) { Debug.LogException(ex); File.WriteAllText(Path.Combine(Out, "error.txt"), ex.ToString()); break; }
            if (!next) { stack.Pop(); continue; }
            if (stack.Peek().Current is IEnumerator inner) { stack.Push(inner); continue; }
            yield return stack.Peek().Current;
        }
        _done = true;
    }

    private IEnumerator Drive()
    {
        yield return W(0.6f);
        switch (Slug)
        {
            case "counter":
                for (var i = 0; i < 4; i++) { yield return Click("Canvas/Panel/Buttons/Increment"); yield return W(0.25f); }
                yield return W(0.3f);
                yield return Click("Canvas/Panel/Buttons/Decrement"); yield return W(0.6f);
                yield return Click("Canvas/Panel/Buttons/Reset"); yield return W(0.6f);
                break;
            case "greeter":
                yield return Erase("Canvas/Panel/Name Input", 0.08f); yield return W(0.3f);
                yield return Type("Canvas/Panel/Name Input", "Aspid", 0.15f);
                yield return W(1.2f);
                yield return Click("Canvas/Panel/Clear"); yield return W(0.6f);
                yield return Type("Canvas/Panel/Name Input", "World", 0.12f); yield return W(0.4f);
                break;
            case "bind-modes":
                yield return Slide("Canvas/Panel/Volume Row/Volume", 0.2f, 0.8f); yield return W(0.4f);
                yield return Slide("Canvas/Panel/Volume Row/Volume", 0.8f, 1.2f); yield return W(0.4f);
                yield return Click("Canvas/Panel/Muted"); yield return W(0.4f);
                yield return Type("Canvas/Panel/Profile Name Input", "Night", 0.12f); yield return W(0.6f);
                yield return Click("Canvas/Panel/Reset"); yield return W(0.4f);
                yield return Erase("Canvas/Panel/Profile Name Input"); yield return W(0.5f);
                break;
            case "stats":
                yield return Click("Canvas/Panel/Strength Row/Add Button"); yield return W(0.3f);
                yield return Click("Canvas/Panel/Strength Row/Add Button"); yield return W(0.3f);
                yield return Click("Canvas/Panel/Agility Row/Add Button"); yield return W(0.3f);
                yield return Click("Canvas/Panel/Intelligence Row/Add Button"); yield return W(0.3f);
                yield return Click("Canvas/Panel/Strength Row/Remove Button"); yield return W(0.8f);
                yield return Click("Canvas/Panel/Command Row/Reset Button"); yield return W(0.7f);
                break;
            case "todo-list":
            {
                var storage = "Canvas/Todo Storage View";
                var items = F(storage + "/Scroll View/Viewport/Content").transform;
                yield return Click(items.GetChild(0).GetComponentInChildren<Toggle>().gameObject); yield return W(0.4f);
                yield return Click(storage + "/Add Todo Button"); yield return W(0.6f);
                yield return Type(storage + "/Input Field", "New", 0.15f); yield return W(0.8f);
                yield return Erase(storage + "/Input Field", 0.1f); yield return W(0.4f);
                var last = items.GetChild(items.childCount - 1);
                yield return Click(last.GetComponentsInChildren<Button>().First(b => b.name == "Delete Button").gameObject); yield return W(0.4f);
                yield return Click(items.GetChild(0).GetComponentInChildren<Toggle>().gameObject); yield return W(0.6f);
                break;
            }
            case "custom-binder":
                for (var i = 0; i < 4; i++) { yield return Click("Canvas/Panel/Buttons/Hit"); yield return W(0.3f); }
                yield return W(0.3f);
                for (var i = 0; i < 3; i++) { yield return Click("Canvas/Panel/Buttons/Heal"); yield return W(0.3f); }
                yield return W(0.4f);
                break;
            case "virtualized-list":
            {
                var rects = FindObjectsByType<ScrollRect>(FindObjectsSortMode.InstanceID);
                yield return Scroll(rects, true, 0.7f, 1.2f); yield return W(1.0f);
                yield return Scroll(rects, true, 1f, 0.9f); yield return W(0.8f);
                break;
            }
            case "dynamic-view-model":
                yield return Click("Canvas/Panel/Buttons/Start Stop"); yield return W(1.2f);
                yield return Click("Canvas/Panel/Buttons/Lap"); yield return W(0.9f);
                yield return Click("Canvas/Panel/Buttons/Lap"); yield return W(0.6f);
                yield return Click("Canvas/Panel/Buttons/Start Stop"); yield return W(0.6f);
                yield return Click("Canvas/Panel/Buttons/Reset"); yield return W(0.6f);
                break;
            case "di-integration":
                for (var i = 0; i < 5; i++) { yield return Click("Canvas/Panel/Earn"); yield return W(0.4f); }
                yield return W(0.6f);
                break;
        }
        _done = true;
    }
}
