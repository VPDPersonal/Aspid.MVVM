import {ACCENTS, DEFAULT_ACCENT} from '../../accents';
import {POINTS, PIECES, BODY} from './mesh';
import bodyMapUrl from './media/logo-body.png';
import eyelidsUrl from './media/eyelids.png';

/*
 * The banner logo's motion, drawn on the banner's `__fx` canvas over the logo element.
 * - The entrance: every low-poly piece of the logo (mesh.js) takes off from a dot of the texture, fades in and unfolds on
 *   the way and lands in its place, from the tail to the snout. Then the logo element takes over and the eye flashes.
 * - The life after it: now and then the snake blinks, and a light runs along its body from the tail to the snout, also
 *   when the pointer comes onto the logo. `media/logo-body.png` holds where each pixel lies along the body.
 * mesh.js, logo-body.png and eyelids.png come from `scripts/intro-logo-data.mjs`.
 */

const LOGO_WIDTH = 640;      // px of the logo images, the space of mesh.js
const LOGO_HEIGHT = 634;
const BODY_SCALE = 2;        // logo-body.png is half the logo's size
const GRID = 20;             // px, must match the CSS dot texture (background-size)
// Below 997px the banner paints its own dots from its corner (custom.css); wider, the page's fixed dots show through.
const NARROW = '(max-width: 996px)';

// The entrance.
const FLIGHT = 640;          // ms one piece flies
const SPREAD = 820;          // ms between the take-off of the tail's first piece and the snout's last one
const JITTER = 85;           // ms of random lag per piece, so neighbours do not move in step
const REACH = [0.25, 1];     // the nearest and the farthest start of a piece, in logo widths from its place
const SWING = 0.8;           // rad, how far a start may turn away from the line out of the logo's centre
const SPIN = 3.5;            // rad, the widest turn a piece unwinds on the way
const BOW = 0.17;            // the widest sideways bow of a flight, in logo widths
const APPEAR = 0.25;         // the share of its flight a piece takes to fade in; before it takes off it is not drawn
const FLASH = 180;           // ms a landed piece glows
const HANDOVER = 160;        // ms the pieces stay after the close, while the logo element fades in over them (styles.css)

// The life after it.
const EYE = {x: 293, y: 98}; // the eye's centre, in logo px
const EYE_FLASH = 820;       // ms
const BLINK = 240;           // ms for the lid to close and open again
const LID = {x: 260, y: 65, size: 66}; // where a closed lid of eyelids.png covers the eye, in logo px
const BLINK_EVERY = [4500, 7000]; // ms between blinks, the shortest and the longest
const RUN = 1300;            // ms for the light to run from the tail past the snout
const RUN_EVERY = 7000;      // ms between runs; the pointer coming onto the logo starts one as well
const RUN_WIDTH = 0.03;      // the light's falloff behind its front, as a share of the body
const RUN_FRONT = 0.2;       // the soft rise of its front, in RUN_WIDTH
const RUN_TAIL = 4;          // how far behind the front the light still shows, in RUN_WIDTH

const clamp = (v, low = 0, high = 1) => Math.min(high, Math.max(low, v));
const lerp = (a, b, t) => a + (b - a) * t;
const easeOut = (t) => 1 - (1 - clamp(t)) ** 3;
const easeInOut = (t) => {
  const q = clamp(t);
  return q < 0.5 ? 4 * q * q * q : 1 - (-2 * q + 2) ** 3 / 2;
};
const smooth = (from, to, x) => {
  const q = clamp((x - from) / (to - from));
  return q * q * (3 - 2 * q);
};
const between = ([low, high]) => low + (high - low) * Math.random();

const images = new Map();
function loadImage(url) {
  if (!images.has(url)) {
    images.set(url, new Promise((resolve, reject) => {
      const image = new Image();
      image.onload = () => resolve(image);
      image.onerror = reject;
      image.src = url;
    }));
  }
  return images.get(url);
}

// Position along the body for every pixel of logo-body.png: 0 off the body, else 1 (tail) .. 255 (snout). `pixels[v]`
// lists the pixels at position v, so the run light touches only the stretch it lights.
let bodyMap = null;
function loadBodyMap() {
  bodyMap ??= loadImage(bodyMapUrl).then((image) => {
    const canvas = document.createElement('canvas');
    canvas.width = image.width;
    canvas.height = image.height;
    const ctx = canvas.getContext('2d', {willReadFrequently: true});
    ctx.drawImage(image, 0, 0);
    const {data} = ctx.getImageData(0, 0, image.width, image.height);
    const count = new Uint32Array(256);
    for (let i = 0; i < data.length; i += 4) count[data[i]]++;
    const pixels = Array.from(count, (n) => new Uint32Array(n));
    count.fill(0);
    for (let i = 0; i < data.length / 4; i++) {
      const v = data[i * 4];
      pixels[v][count[v]++] = i;
    }
    return {width: image.width, height: image.height, pixels};
  });
  return bodyMap;
}

// The logo lit up: its own texture made brighter and tinted with the light, so light on it keeps the facets. The canvas
// lies over the logo element as a plain layer, so a light has to be drawn as this brighter logo, not added onto nothing.
const brightLogos = new WeakMap();
function brightLogo(image, light) {
  const key = `${light}`;
  const cached = brightLogos.get(image);
  if (cached?.key === key) return cached.canvas;
  const canvas = document.createElement('canvas');
  canvas.width = image.width;
  canvas.height = image.height;
  const ctx = canvas.getContext('2d');
  ctx.drawImage(image, 0, 0);
  ctx.globalCompositeOperation = 'lighter';
  ctx.globalAlpha = 0.75;
  ctx.drawImage(image, 0, 0);
  ctx.globalCompositeOperation = 'source-atop';
  ctx.globalAlpha = 0.18;
  ctx.fillStyle = `rgb(${light.join(', ')})`;
  ctx.fillRect(0, 0, canvas.width, canvas.height);
  brightLogos.set(image, {key, canvas});
  return canvas;
}

function readColors() {
  const root = document.documentElement;
  const style = getComputedStyle(root);
  const hex = style.getPropertyValue('--venom-ripple').trim() || style.getPropertyValue('--venom-accent').trim();
  const value = parseInt(hex.slice(1), 16);
  const accent = [(value >> 16) & 255, (value >> 8) & 255, value & 255];
  return {
    accent,
    // A brighter accent for light on the logo: saturated, so it reads as light and not as a white haze.
    light: accent.map((c) => Math.round(Math.min(255, c * 1.25 + 30))),
    // The mono logo has a grey eye; every other one a red eye.
    eye: root.dataset.accent === 'mono' ? [235, 235, 235] : [255, 60, 50],
  };
}

const trace = (ctx, corners) => {
  ctx.beginPath();
  ctx.moveTo(corners[0][0], corners[0][1]);
  ctx.lineTo(corners[1][0], corners[1][1]);
  ctx.lineTo(corners[2][0], corners[2][1]);
  ctx.closePath();
};

/**
 * Starts the logo's motion in `banner`. With `entrance` the logo assembles from the dots first and `onClose` runs once it
 * is whole; without it only the life runs. Returns a function that stops everything.
 */
export function startLogoMotion(banner, {entrance, onClose}) {
  const canvas = banner.querySelector('.readme-banner__fx');
  const mark = banner.querySelector('.readme-banner__mark');
  const logo = banner.querySelector('.readme-banner__logo');
  const ctx = canvas.getContext('2d');
  let width = 0;
  let height = 0;
  let dpr = 1;
  let frame = 0;
  let stopped = false;
  let visible = true;
  let colors = readColors();
  let image = null;
  // The entrance's pieces, the time they took off and whether the logo is whole yet.
  let pieces = null;
  let narrow = false;
  let start = 0;
  let closed = !entrance;
  // Running effects of the life: {kind, start, duration}.
  let effects = [];
  const timers = new Set();

  const resize = () => {
    dpr = Math.min(devicePixelRatio || 1, 2);
    width = banner.clientWidth;
    height = banner.clientHeight;
    canvas.width = Math.ceil(width * dpr);
    canvas.height = Math.ceil(height * dpr);
    wake();
  };
  const sizes = new ResizeObserver(() => resize());
  sizes.observe(banner);
  const sight = new IntersectionObserver(([entry]) => {
    visible = entry.isIntersecting;
  });
  sight.observe(banner);

  const later = (callback, delay) => {
    const timer = setTimeout(() => {
      timers.delete(timer);
      callback();
    }, delay);
    timers.add(timer);
  };

  // The logo image of the current accent: the one the logo element shows.
  const currentImage = () => {
    const url = /url\(["']?(.*?)["']?\)/.exec(getComputedStyle(logo).backgroundImage)?.[1];
    return url ? loadImage(url) : Promise.reject(new Error('The banner logo has no image.'));
  };

  // Where the logo image lies on the canvas: the element's box, the image fitted in it (`contain`).
  const logoBox = () => {
    const outer = banner.getBoundingClientRect();
    const box = logo.getBoundingClientRect();
    const scale = Math.min(box.width / LOGO_WIDTH, box.height / LOGO_HEIGHT);
    return {
      x: box.left - outer.left + (box.width - LOGO_WIDTH * scale) / 2,
      y: box.top - outer.top + (box.height - LOGO_HEIGHT * scale) / 2,
      scale,
      left: outer.left,
      top: outer.top,
    };
  };

  const addEffect = (kind, duration) => {
    colors = readColors();
    effects.push({kind, start: performance.now(), duration});
    wake();
  };

  // ---------- The entrance ----------

  const plan = () => {
    const box = logoBox();
    const size = LOGO_WIDTH * box.scale;
    // The dots: on the fixed page texture in viewport coordinates, or on the banner's own from its corner. A start is kept
    // in those coordinates, so it stays on its dot when the page scrolls under it.
    narrow = matchMedia(NARROW).matches;
    const snap = (value, offset) => Math.round((value + offset - GRID / 2) / GRID) * GRID + GRID / 2 - offset;
    const taken = new Set();
    pieces = [];
    for (let i = 0; i < BODY.length; i++) {
      const corners = [0, 1, 2].map((k) => {
        const n = PIECES[i * 3 + k];
        return [POINTS[n * 2], POINTS[n * 2 + 1]];
      });
      const mx = (corners[0][0] + corners[1][0] + corners[2][0]) / 3;
      const my = (corners[0][1] + corners[1][1] + corners[2][1]) / 3;
      const hx = box.x + mx * box.scale;
      const hy = box.y + my * box.scale;
      const angle = Math.atan2(my - LOGO_HEIGHT / 2, mx - LOGO_WIDTH / 2) + (Math.random() * 2 - 1) * SWING;
      let reach = between(REACH) * size;
      let sx = 0;
      let sy = 0;
      for (let tries = 0; tries < 20; tries++) {
        sx = snap(clamp(hx + Math.cos(angle) * reach, GRID / 2, width - GRID / 2), narrow ? 0 : box.left);
        sy = snap(clamp(hy + Math.sin(angle) * reach, GRID / 2, height - GRID / 2), narrow ? 0 : box.top);
        if (!taken.has(`${sx},${sy}`)) break;
        reach += GRID * 0.7;
      }
      taken.add(`${sx},${sy}`);
      if (!narrow) {
        sx += box.left;
        sy += box.top;
      }
      const xs = corners.map((c) => c[0]);
      const ys = corners.map((c) => c[1]);
      const x0 = Math.floor(Math.min(...xs)) - 1;
      const y0 = Math.floor(Math.min(...ys)) - 1;
      pieces.push({
        corners, mx, my, sx, sy,
        clip: [x0, y0, Math.ceil(Math.max(...xs)) + 1 - x0, Math.ceil(Math.max(...ys)) + 1 - y0],
        delay: BODY[i] / 1000 * SPREAD + Math.random() * JITTER,
        spin: (Math.random() * 2 - 1) * SPIN,
        bow: (Math.random() * 2 - 1) * BOW * size,
      });
    }
    start = performance.now();
  };

  // Returns false once the pieces are no longer needed.
  const drawPieces = (now, box) => {
    const t = now - start;
    const [ar, ag, ab] = colors.accent;
    const ox = narrow ? 0 : box.left;
    const oy = narrow ? 0 : box.top;
    let last = 0;
    for (const piece of pieces) {
      const sx = piece.sx - ox;
      const sy = piece.sy - oy;
      last = Math.max(last, piece.delay + FLIGHT);
      if (t <= piece.delay) continue;
      const u = clamp((t - piece.delay) / FLIGHT);
      const hx = box.x + piece.mx * box.scale;
      const hy = box.y + piece.my * box.scale;
      const travel = easeOut(u);
      const grow = easeInOut(u);
      // The flight bows to one side of the straight line from the dot to the place.
      const nx = sy - hy;
      const ny = hx - sx;
      const nl = Math.hypot(nx, ny) || 1;
      const bow = Math.sin(Math.PI * u) * piece.bow;
      const x = lerp(sx, hx, travel) + nx / nl * bow;
      const y = lerp(sy, hy, travel) + ny / nl * bow;
      // A dot that swells, then the piece of the logo it unfolds into.
      const texture = smooth(0.12, 0.42, grow);
      if (texture < 1) {
        ctx.globalAlpha = (1 - texture) * smooth(0, APPEAR, u);
        ctx.fillStyle = `rgb(${ar}, ${ag}, ${ab})`;
        ctx.beginPath();
        ctx.arc(x, y, 1 + grow * 7, 0, Math.PI * 2);
        ctx.fill();
        ctx.globalAlpha = 1;
      }
      if (texture <= 0) continue;
      const scale = box.scale * Math.max(grow, 0.02);
      ctx.save();
      ctx.translate(x, y);
      ctx.rotate(piece.spin * (1 - travel));
      ctx.scale(scale, scale);
      ctx.translate(-piece.mx, -piece.my);
      trace(ctx, piece.corners);
      ctx.clip();
      ctx.globalAlpha = texture;
      const [cx, cy, cw, ch] = piece.clip;
      ctx.drawImage(image, cx, cy, cw, ch, cx, cy, cw, ch);
      const flash = 1 - (t - piece.delay - FLIGHT) / FLASH;
      if (u >= 1 && flash > 0) {
        ctx.globalCompositeOperation = 'lighter';
        ctx.globalAlpha = 0.55 * Math.min(flash, 1);
        ctx.fillStyle = `rgb(${colors.light.join(', ')})`;
        ctx.fill();
      }
      ctx.restore();
    }
    if (!closed && t >= last) close(now);
    return t < last + HANDOVER;
  };

  const close = (now) => {
    closed = true;
    effects.push({kind: 'eye', start: now, duration: EYE_FLASH});
    onClose?.();
    live();
  };

  // ---------- The life ----------

  const drawEye = (e, box) => {
    const a = e < 120 ? e / 120 : 1 - (e - 120) / (EYE_FLASH - 120);
    if (a <= 0) return;
    const x = box.x + EYE.x * box.scale;
    const y = box.y + EYE.y * box.scale;
    const r = LOGO_WIDTH * box.scale * (0.025 + 0.035 * (1 - a));
    const [er, eg, eb] = colors.eye;
    ctx.save();
    ctx.globalCompositeOperation = 'lighter';
    const glow = ctx.createRadialGradient(x, y, 0, x, y, r * 2.2);
    glow.addColorStop(0, `rgba(255, ${Math.min(255, eg + 70)}, ${Math.min(255, eb + 60)}, ${0.95 * a})`);
    glow.addColorStop(0.3, `rgba(${er}, ${eg}, ${eb}, ${0.55 * a})`);
    glow.addColorStop(1, `rgba(${er}, ${eg}, ${eb}, 0)`);
    ctx.fillStyle = glow;
    ctx.beginPath();
    ctx.arc(x, y, r * 2.2, 0, Math.PI * 2);
    ctx.fill();
    // A thin glint across the eye.
    const length = r * 1.5 * a;
    ctx.strokeStyle = `rgba(255, 240, 235, ${0.75 * a})`;
    ctx.lineWidth = 1;
    ctx.beginPath();
    ctx.moveTo(x - length, y);
    ctx.lineTo(x + length, y);
    ctx.moveTo(x, y - length / 2);
    ctx.lineTo(x, y + length / 2);
    ctx.stroke();
    ctx.restore();
  };

  // The closed lid of the reader's accent fades in over the eye and out again.
  let lids = null;
  const drawBlink = (e, box) => {
    const a = e < 60 ? e / 60 : e < 150 ? 1 : 1 - (e - 150) / (BLINK - 150);
    if (a <= 0 || !lids) return;
    const k = Math.max(0, ACCENTS.indexOf(document.documentElement.dataset.accent ?? DEFAULT_ACCENT));
    if ((k + 1) * LID.size > lids.width) return;
    const size = LID.size * box.scale;
    ctx.globalAlpha = a;
    ctx.drawImage(lids, k * LID.size, 0, LID.size, LID.size, box.x + LID.x * box.scale, box.y + LID.y * box.scale, size, size);
    ctx.globalAlpha = 1;
  };

  // The light: a narrow front runs along the body, lighting the logo's own texture, sharp ahead and fading behind. Only the
  // positions lit in the last frame are cleared and only the lit stretch is filled; the mask is half the logo's size.
  let light = null;
  const drawRun = (e, box, map) => {
    if (!image) return;
    if (!light) {
      const mask = document.createElement('canvas');
      mask.width = map.width;
      mask.height = map.height;
      const lit = document.createElement('canvas');
      lit.width = LOGO_WIDTH;
      lit.height = LOGO_HEIGHT;
      const maskCtx = mask.getContext('2d');
      light = {mask, maskCtx, pixels: maskCtx.createImageData(map.width, map.height), lit, litCtx: lit.getContext('2d'), range: [1, 0]};
    }
    const {data} = light.pixels;
    for (let v = light.range[0]; v <= light.range[1]; v++) for (const i of map.pixels[v]) data[i * 4 + 3] = 0;
    const p = e / RUN * (1 + (RUN_TAIL + 1) * RUN_WIDTH) - RUN_WIDTH;
    const from = Math.max(1, Math.floor(1 + (p - RUN_TAIL * RUN_WIDTH) * 254));
    const to = Math.min(255, Math.ceil(1 + (p + RUN_FRONT * RUN_WIDTH) * 254));
    light.range = [from, to];
    for (let v = from; v <= to; v++) {
      const d = (p - (v - 1) / 254) / RUN_WIDTH;
      const alpha = d < -RUN_FRONT ? 0 : d < 0 ? 255 * (1 + d / RUN_FRONT) : 255 * Math.exp(-0.9 * d);
      for (const i of map.pixels[v]) data[i * 4 + 3] = alpha;
    }
    light.maskCtx.putImageData(light.pixels, 0, 0);
    light.litCtx.globalCompositeOperation = 'copy';
    light.litCtx.drawImage(brightLogo(image, colors.light), 0, 0);
    light.litCtx.globalCompositeOperation = 'destination-in';
    light.litCtx.drawImage(light.mask, 0, 0, map.width * BODY_SCALE, map.height * BODY_SCALE);
    ctx.drawImage(light.lit, box.x, box.y, LOGO_WIDTH * box.scale, LOGO_HEIGHT * box.scale);
  };

  let map = null;
  const drawEffects = (now, box) => {
    effects = effects.filter((effect) => now - effect.start < effect.duration);
    for (const effect of effects) {
      const e = now - effect.start;
      if (effect.kind === 'eye') drawEye(e, box);
      else if (effect.kind === 'blink') drawBlink(e, box);
      else if (effect.kind === 'run' && map) drawRun(e, box, map);
    }
    return effects.length > 0;
  };

  const tick = (now) => {
    frame = 0;
    if (stopped) return;
    ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
    ctx.clearRect(0, 0, width, height);
    const box = logoBox();
    let busy = false;
    if (pieces) {
      busy = drawPieces(now, box);
      if (!busy) pieces = null;
    }
    busy = drawEffects(now, box) || busy;
    if (busy) frame = requestAnimationFrame(tick);
  };

  function wake() {
    if (!frame && !stopped) frame = requestAnimationFrame(tick);
  }

  // Only while the banner is on screen and the tab is in front.
  const shown = () => visible && !document.hidden;
  const blink = () => later(() => {
    if (shown() && lids) addEffect('blink', BLINK);
    blink();
  }, between(BLINK_EVERY));
  const running = () => effects.some((effect) => effect.kind === 'run');
  // The light is the logo itself brightened, so it follows the accent the reader picks meanwhile. The check runs again
  // once the image is there: hovers and the timer may have queued several starts while it was loading.
  const startRun = () => {
    if (!map || running()) return;
    currentImage().then((loaded) => {
      image = loaded;
      if (!stopped && !running()) addEffect('run', RUN);
    }, () => {});
  };
  const run = () => later(() => {
    if (shown()) startRun();
    run();
  }, RUN_EVERY);
  const hover = () => {
    if (closed) startRun();
  };

  function live() {
    loadBodyMap().then((loaded) => {
      map = loaded;
    }, () => {});
    loadImage(eyelidsUrl).then((loaded) => {
      lids = loaded;
    }, () => {});
    mark.addEventListener('pointerenter', hover);
    blink();
    run();
  }

  resize();
  if (entrance) {
    currentImage().then((loaded) => {
      if (stopped) return;
      image = loaded;
      plan();
      wake();
    }, () => {
      if (!stopped) close(performance.now());
    });
  } else {
    live();
  }

  return () => {
    stopped = true;
    cancelAnimationFrame(frame);
    for (const timer of timers) clearTimeout(timer);
    sizes.disconnect();
    sight.disconnect();
    mark.removeEventListener('pointerenter', hover);
  };
}
