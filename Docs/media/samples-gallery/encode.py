"""Encodes Temp/rec/<slug>-<dark|light> frame folders into <out>/<slug>.gif and <slug>-light.gif.
Both themes share one 16:9 crop (union of non-background pixels of both, padded) and start frame 0.
GIF: 1024x576, 20 fps, 256 colours, sierra2_4a, looping (as scenes.py of the FastTools gallery)."""
import sys, subprocess, tempfile
from pathlib import Path
from PIL import Image, ImageChops
rec = Path(sys.argv[1]); out = Path(sys.argv[2]); slugs = sys.argv[3:]
BG = {'dark': (14, 16, 21), 'light': (238, 240, 243)}
CAM = (1440, 810); PAD = 40; MINW = 640
# Rows empty in every frame of both themes, cut before cropping so a tall, narrow layout still fills a 16:9 card.
CUT_ROWS = {'todo-list': (380, 690)}


def cut_rows(frame, rows, bg):
    """Removes rows [top, bottom) and pads the frame back to the camera size with the theme background."""
    top, bottom = rows
    out = Image.new('RGB', CAM, bg)
    out.paste(frame.crop((0, 0, CAM[0], top)), (0, 0))
    out.paste(frame.crop((0, bottom, CAM[0], CAM[1])), (0, top))
    return out

def pin_background(path, rgb):
    """Palette quantisation can shift the background by a step; the site keys on the exact RGB, so the global
    palette entry nearest to it is set to it exactly."""
    data = bytearray(path.read_bytes())
    first = None
    n = 2 ** ((data[10] & 7) + 1)
    entries = [tuple(data[13 + 3 * i:16 + 3 * i]) for i in range(n)]
    first = Image.open(path)
    counts = {k: c for c, k in first.getcolors(1 << 16)} if first.mode == 'P' else {}
    near = [k for k in range(n) if sum((a - b) ** 2 for a, b in zip(entries[k], rgb)) <= 48]
    i = max(near, key=lambda k: counts.get(k, 0)) if near else \
        min(range(n), key=lambda k: sum((a - b) ** 2 for a, b in zip(entries[k], rgb)))
    data[13 + 3 * i:16 + 3 * i] = bytes(rgb)
    path.write_bytes(data)


def bbox(frames, bg):
    box = None
    base = Image.new('RGB', CAM, bg)
    for f in frames[::3]:
        diff = ImageChops.difference(f, base).convert('L').point(lambda v: 255 if v > 10 else 0)
        b = diff.getbbox()
        if b: box = b if box is None else (min(box[0], b[0]), min(box[1], b[1]), max(box[2], b[2]), max(box[3], b[3]))
    return box

for slug in slugs:
    seqs = {t: [Image.open(p).convert('RGB') for p in sorted((rec / f'{slug}-{t}').glob('frame-*.png'))] for t in BG}
    if slug in CUT_ROWS:
        seqs = {t: [cut_rows(f, CUT_ROWS[slug], BG[t]) for f in seqs[t]] for t in BG}
    boxes = [bbox(seqs[t], BG[t]) for t in BG]
    x0 = min(b[0] for b in boxes) - PAD; y0 = min(b[1] for b in boxes) - PAD
    x1 = max(b[2] for b in boxes) + PAD; y1 = max(b[3] for b in boxes) + PAD
    w = max(x1 - x0, (y1 - y0) * 16 / 9, MINW); h = w * 9 / 16
    w = min(w, CAM[0]); h = min(h, CAM[1])
    cx = (x0 + x1) / 2; cy = (y0 + y1) / 2
    left = min(max(cx - w / 2, 0), CAM[0] - w); top = min(max(cy - h / 2, 0), CAM[1] - h)
    box = tuple(int(round(v)) for v in (left, top, left + w, top + h))
    for t in BG:
        name = out / (slug + ('-light' if t == 'light' else '') + '.gif')
        with tempfile.TemporaryDirectory() as d:
            for i, f in enumerate(seqs[t]):
                f.crop(box).resize((1024, 576), Image.LANCZOS).save(f'{d}/{i:04d}.png')
            subprocess.run(['ffmpeg', '-loglevel', 'error', '-y', '-framerate', '20', '-i', f'{d}/%04d.png',
                            '-filter_complex', '[0:v]split[a][b];[a]palettegen=max_colors=256[p];[b][p]paletteuse=dither=sierra2_4a',
                            '-loop', '0', str(name)], check=True)
        pin_background(name, BG[t])
        print(slug, t, 'crop', box, len(seqs[t]), 'frames', f'{len(seqs[t]) / 20:.2f}s', f'{name.stat().st_size / 1e6:.2f} MB')
