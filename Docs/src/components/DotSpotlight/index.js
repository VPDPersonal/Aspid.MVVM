import {useEffect} from 'react';
import {burstAt, chargeLevel, isCanvas, overload} from '../DotRipple';

const RADIUS = 130;       // px, the visible part of the spotlight
const EDGE = 150;         // px, mean distance at which the light fades out
const CHARGED_EDGE = 45;  // px, the same once a wave is fully charged: the light gathers around the pointer
const GRID = 20;          // px, must match the CSS dot texture (background-size)
const DOT = 1.4;          // px, radius of a lit dot
const SHAKE = 3;          // px, how far the whole light jumps about just before it bursts
const JITTER = 2;         // px, how far each of its dots jumps about on top of that
const DARK = 200;         // ms the light stays out after a burst
const RELIGHT = 600;      // ms it then takes to come back
const FADE = 400;         // ms, must match the opacity transition of .dot-spotlight in custom.css
const TAIL = 300;         // ms, time constant with which a dot the light has left goes dark, so a moving light trails a tail
const TAIL_LIFT = 0.75;   // power below 1 that keeps the tail brighter than the light it left
const TAIL_SPREAD = 0.6;  // how much TAIL varies from dot to dot, so the tail breaks up into embers
const TAIL_TWINKLE = 0.35; // how much a dot of the tail flickers as it cools
const TAIL_DIM = 0.4;     // brightness of most of the tail; only a few dots stay near full
// Waves running round the edge of the light: [lobes, angular speed per ms, relative amplitude]. Their different lobe
// counts and speeds keep the outline from ever closing into a circle or repeating visibly.
const WOBBLE = [[2, 0.00031, 0.07], [3, -0.00047, 0.09], [5, 0.00083, 0.05]];
const REACH = EDGE * (1 + WOBBLE.reduce((sum, [, , amplitude]) => sum + amplitude, 0));
// Now and then a glint runs across the light from a random side, as over a polished surface.
const GLINT_PERIOD = 4000; // ms from one glint to the next
const GLINT_SWEEP = 900;   // ms a glint takes to cross the light
const GLINT_WIDTH = 28;    // px, half-width of the glint band
const GLINT_BRIGHT = 1.2;  // extra brightness of a dot at the centre of the band
const GLINT_GROW = 0.6;    // extra radius of that dot, relative to DOT
const GLINT_BEND = 22;     // px, how far the band bends away from a straight line
const GLINT_SWELL = 0.45;  // how much the band widens and narrows along its length, relative to GLINT_WIDTH
const GLINT_SPECKLE = 0.6; // how much the shine of single dots varies, so the band looks broken up

// A stable pseudo-random number in [0, 1) for `n`.
const hash = (n) => {
  const s = Math.sin(n) * 43758.5453;
  return s - Math.floor(s);
};
const ARTICLE = '[class*="docMainContainer_"] > .container > .row > .col:first-child';

const inViewport = (x, y) => x >= 0 && y >= 0 && x < innerWidth && y < innerHeight;

// True when any part of the spotlight around (x, y) falls on the empty canvas. The opaque article surface lies above the
// spotlight, so over the article it is lit near the surface's edge, and only the part past it is seen.
function nearCanvas(x, y) {
  const target = document.elementFromPoint(x, y);
  if (isCanvas(target)) return true;
  const article = target?.closest(ARTICLE);
  if (!article) return false;

  const rect = article.getBoundingClientRect();
  const beyondEdges = [
    [rect.left - 1, y, x - rect.left],
    [rect.right + 1, y, rect.right - x],
    [x, rect.top - 1, y - rect.top],
    [x, rect.bottom + 1, rect.bottom - y],
  ];
  // The introduction's banner opens the top of the article onto the canvas, so the light also shows there from below.
  const banner = article.querySelector('.readme-banner')?.getBoundingClientRect();
  if (banner && y > banner.bottom) beyondEdges.push([x, banner.bottom - 1, y - banner.bottom]);
  return beyondEdges.some(([px, py, distance]) => distance < RADIUS && inViewport(px, py) && isCanvas(document.elementFromPoint(px, py)));
}

// Distance from the centre to the edge of the light in direction `angle` at time `now`, for a light of mean radius `edge`.
const edgeAt = (angle, now, edge) =>
  edge * (1 + WOBBLE.reduce((sum, [lobes, speed, amplitude]) => sum + amplitude * Math.sin(lobes * angle + now * speed * lobes), 0));

function readColor() {
  const style = getComputedStyle(document.documentElement);
  return style.getPropertyValue('--venom-ripple').trim() || style.getPropertyValue('--venom-accent').trim();
}

/** Lights the dot texture around the pointer while it is over or close to the empty canvas of a docs page. The lit patch
 *  fades out towards a slowly wobbling edge, so it never reads as a perfect circle. A charge held too long makes it
 *  tremble until it bursts. */
export default function DotSpotlight() {
  useEffect(() => {
    if (!matchMedia('(hover: hover) and (pointer: fine)').matches) return undefined;
    if (matchMedia('(prefers-reduced-motion: reduce)').matches) return undefined;

    // The canvas is created when the light first shows, so a page without dots never holds one.
    let spot = null;
    let ctx = null;

    const resize = () => {
      if (!spot) return;
      const dpr = Math.min(devicePixelRatio || 1, 2);
      spot.width = Math.ceil(innerWidth * dpr);
      spot.height = Math.ceil(innerHeight * dpr);
      ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
    };

    const create = () => {
      spot = document.createElement('canvas');
      spot.className = 'dot-spotlight';
      spot.setAttribute('aria-hidden', 'true');
      document.body.appendChild(spot);
      ctx = spot.getContext('2d');
      resize();
      // Reading the style lets the fade-in start from the canvas's own opacity of 0.
      void getComputedStyle(spot).opacity;
    };

    let x = -1;
    let y = -1;
    // Where the light is drawn: the last position over the page, kept while it fades out after the pointer leaves.
    let lightX = -1;
    let lightY = -1;
    let on = false;
    let offAt = 0;
    let frame = 0;
    let draw = 0;
    let color = readColor();
    // The pale light canvas shows the light green faintly: its dots light up half again as strongly.
    let boost = document.documentElement.dataset.theme === 'light' ? 1.5 : 1;
    let colorAt = 0;
    // How lit each dot is, keyed by grid cell; it decays after the light moves on.
    const heat = new Map();
    let heatAt = 0;

    const render = (now) => {
      ctx.clearRect(0, 0, innerWidth, innerHeight);
      if (!on && now - offAt > FADE) {
        draw = 0;
        return;
      }
      draw = requestAnimationFrame(render);
      // The theme or accent may change while the page is open.
      if (now - colorAt > 1000) {
        color = readColor();
        boost = document.documentElement.dataset.theme === 'light' ? 1.5 : 1;
        colorAt = now;
      }

      // Holding the button shrinks the light and makes its dots bigger and brighter.
      const shrink = chargeLevel(now);
      const edge = EDGE - (EDGE - CHARGED_EDGE) * shrink;
      const dot = DOT * (1 + 0.4 * shrink);
      // Held past the full charge, the gathered light trembles, harder towards the burst, and flickers.
      const tremble = overload(now) ** 1.5;
      const jump = () => (Math.random() * 2 - 1) * tremble;
      const centerX = lightX + SHAKE * jump();
      const centerY = lightY + SHAKE * jump();
      // After a burst the light is out for a moment, then comes back.
      const relit = Math.min(Math.max((now - burstAt() - DARK) / RELIGHT, 0), 1);
      const elapsed = now - heatAt;
      heatAt = now;
      if (relit === 0) {
        heat.clear();
        return;
      }
      for (const [key, value] of heat) {
        const cooled = value * Math.exp(-elapsed / (TAIL * (1 + TAIL_SPREAD * (2 * hash(key) - 1))));
        if (cooled <= 0.02) heat.delete(key);
        else heat.set(key, cooled);
      }
      // Position of the glint band across the light, or null between glints; a charge puts it out.
      const sweep = (now % GLINT_PERIOD) / GLINT_SWEEP;
      const glint = sweep < 1 && shrink === 0 ? -REACH + 2 * REACH * sweep : null;
      // Each glint gets its own bends and swells, so no two look the same.
      const seed = Math.floor(now / GLINT_PERIOD);
      const phases = [hash(seed), hash(seed + 0.31), hash(seed + 0.67)].map((value) => value * Math.PI * 2);
      // It also starts from its own side of the light.
      const heading = hash(seed + 0.89) * Math.PI * 2;
      const headingX = Math.cos(heading);
      const headingY = Math.sin(heading);
      const gx0 = Math.max(0, Math.floor((lightX - REACH) / GRID));
      const gx1 = Math.min(Math.ceil(innerWidth / GRID), Math.ceil((lightX + REACH) / GRID));
      const gy0 = Math.max(0, Math.floor((lightY - REACH) / GRID));
      const gy1 = Math.min(Math.ceil(innerHeight / GRID), Math.ceil((lightY + REACH) / GRID));
      const current = new Map();
      for (let gy = gy0; gy <= gy1; gy++) {
        for (let gx = gx0; gx <= gx1; gx++) {
          const dx = gx * GRID + GRID / 2 - centerX;
          const dy = gy * GRID + GRID / 2 - centerY;
          const light = 1 - Math.hypot(dx, dy) / edgeAt(Math.atan2(dy, dx), now, edge);
          if (light <= 0.02) continue;
          const key = gy * 100000 + gx;
          current.set(key, light);
          if (light > (heat.get(key) ?? 0)) heat.set(key, light);
        }
      }
      ctx.fillStyle = color;
      for (const [key, warmth] of heat) {
        const twinkle = 1 - TAIL_TWINKLE * (0.5 + 0.5 * Math.sin(now / 70 + hash(key + 0.5) * Math.PI * 2));
        const ember = TAIL_DIM + (1 - TAIL_DIM) * hash(key + 0.25) ** 4;
        const light = Math.max(current.get(key) ?? 0, warmth ** TAIL_LIFT * twinkle * ember);
        const gy = Math.floor(key / 100000);
        const gx = key - gy * 100000;
        const cx = gx * GRID + GRID / 2;
        const cy = gy * GRID + GRID / 2;
        const dx = cx - centerX;
        const dy = cy - centerY;
        const flicker = 1 - 0.4 * tremble * Math.random();
        let shine = 0;
        // The glint runs only across the light itself, not its tail.
        if (glint !== null && current.has(key)) {
          const across = dx * headingX + dy * headingY;
          const along = dy * headingX - dx * headingY;
          const bend = GLINT_BEND * (0.6 * Math.sin(along / 37 + phases[0]) + 0.4 * Math.sin(along / 17 + phases[1]));
          const width = GLINT_WIDTH * (1 + GLINT_SWELL * Math.sin(along / 29 + phases[2]));
          const speckle = 1 - GLINT_SPECKLE * hash(gx * 12.9898 + gy * 78.233 + seed);
          shine = Math.exp(-(((across - glint - bend) / width) ** 2)) * speckle * current.get(key) / light;
        }
        ctx.globalAlpha = Math.min(light * boost * (1 + 0.5 * shrink + GLINT_BRIGHT * shine), 1) * flicker * relit;
        ctx.beginPath();
        ctx.arc(cx + JITTER * jump(), cy + JITTER * jump(), dot * (1 + 0.3 * tremble * Math.random() + GLINT_GROW * shine), 0, Math.PI * 2);
        ctx.fill();
      }
      ctx.globalAlpha = 1;
    };

    // Scrolling moves the page under a resting pointer, so the element under it is looked up again.
    const update = (now) => {
      frame = 0;
      const lit = x >= 0 && nearCanvas(x, y);
      if (lit) {
        lightX = x;
        lightY = y;
      } else if (on) {
        offAt = now;
      }
      on = lit;
      if (lit && !spot) create();
      spot?.toggleAttribute('data-on', lit);
      if (spot && (lit || now - offAt <= FADE) && !draw) draw = requestAnimationFrame(render);
    };
    const schedule = () => { if (!frame) frame = requestAnimationFrame(update); };
    const onMove = (event) => {
      if (event.pointerType !== 'mouse') return;
      x = event.clientX;
      y = event.clientY;
      schedule();
    };
    const onLeave = () => {
      x = -1;
      schedule();
    };

    document.addEventListener('pointermove', onMove, {passive: true});
    document.documentElement.addEventListener('pointerleave', onLeave);
    addEventListener('scroll', schedule, {passive: true});
    addEventListener('resize', resize);
    return () => {
      document.removeEventListener('pointermove', onMove);
      document.documentElement.removeEventListener('pointerleave', onLeave);
      removeEventListener('scroll', schedule);
      removeEventListener('resize', resize);
      if (frame) cancelAnimationFrame(frame);
      if (draw) cancelAnimationFrame(draw);
      spot?.remove();
    };
  }, []);
  return null;
}
