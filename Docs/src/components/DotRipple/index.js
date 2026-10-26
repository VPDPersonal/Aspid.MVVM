import {useEffect} from 'react';
import {rowWaves} from './waves';

// Anything that reads as "content" rather than canvas. Only the filled parts of the navigation panel and the TOC count,
// so the empty space under a short menu still behaves like background.
const CONTENT = [
  '[class*="docMainContainer_"] > .container > .row > .col:first-child',
  '.theme-doc-sidebar-container [class*="header_"]',
  '.theme-doc-sidebar-container [class*="footer_"]',
  '.menu__list-item',
  '.table-of-contents li',
  '.navbar',
  'a', 'button', 'input', 'textarea', 'select', 'label',
  'dialog', '[role="dialog"]', '[role="menu"]',
].join(', ');

// The introduction's banner lies on the article, but the panel leaves it open over the canvas (custom.css).
const OPEN = '.readme-banner';
// Its name and tagline are text a reader may select: a press there charges no wave, but the spotlight still shows.
const TEXT = '.readme-banner__text';

// Below 997px the page is painted with the reading surface instead of the dots (custom.css), so there is no canvas.
let narrow = null;
// There only the introduction's banner shows dots, painted by the banner itself: a wave sent then (sendWave) runs through
// them from this layer of the banner, between those dots and its content.
const BANNER_LAYER = '.readme-banner__ambient';

/** True when `target` is the empty canvas of a docs page: the dots show there, and a click or the pointer may light them. */
export function isCanvas(target) {
  if (!document.documentElement.classList.contains('docs-doc-page')) return false;
  narrow ??= matchMedia('(max-width: 996px)');
  if (narrow.matches) return false;
  if (!(target instanceof Element)) return false;
  return target.closest(OPEN) !== null || !target.closest(CONTENT);
}

const GRID = 20;          // px, must match the CSS dot texture (background-size)
const BASE_DOT = 1;       // px, radius of the resting CSS dot
const SPEED = 0.42;       // px per ms at the start — the same pace on every screen size
const ACCEL = 0.00014;    // px per ms², the wave picks up speed as it runs out, so it leaves the screen sooner
const WIDTH = 46;         // px, half-width of a crest
const LIFT = 2.2;         // px, how much a dot grows at a full-strength crest
const PUSH = 3.5;         // px, how far a dot is pushed outwards at a full-strength crest
const FALLOFF = 700;      // px, distance at which a wave has lost half its energy
const SWAY_CYCLES = 3;    // the crest swells and sinks this many times as it travels
const SWAY = 0.4;         // relative amplitude of that swaying; it damps out with the wave
// Overlapping waves add up; the sum is clamped to what a single wave can reach, so a burst of clicks does not blow the dots up.
const MIN_HEIGHT = -1;
const MAX_HEIGHT = 1 + SWAY;
// Trailing crests behind the main one: [delay in px behind the front, relative amplitude].
const CRESTS = [[0, 1], [2.4 * WIDTH, 0.45], [4.6 * WIDTH, 0.18]];

// Holding the button charges the wave: the spotlight shrinks, and the smaller it is at release, the stronger the wave.
const CHARGE = 1000;      // ms of holding to charge fully
const RECOVER = 300;      // ms for the spotlight to grow back after release
const MIN_POWER = 1;      // strength of a wave released at once, the same as a plain click
const MAX_POWER = 2;      // strength of a fully charged wave
// Held on past the full charge, the gathered light trembles harder and harder until it bursts on its own.
const EXPLODE = 3000;     // ms of holding until the burst; the trembling runs from CHARGE to here
const BURST_POWER = 2.8;  // strength of the wave the burst sends out
const BURST_REACH = 50;   // px, the lit dots within this distance of the pointer fly apart
const SPARKS = 18;        // loose sparks added on top of those dots
const SPARK_LIFE = 750;   // ms, mean lifetime of a spark
const SPARK_DRAG = 0.004; // per ms, how fast a spark slows down
const SHAKE = 7;          // px, how far the page jumps at the burst
const SHAKE_TIME = 500;   // ms for the shake to die out

// Points per wave: a plain click scores one, a fully charged release and a burst score more and show it on the counter,
// the bigger the score the louder: the number counts up, jumps and a "+N" rises from it. `count` is the count-up in ms.
const FULL = 0.98;        // charge level from which a release counts as fully charged
const SCORES = {
  click: {points: 1, count: 0, gain: null},
  charged: {points: 10, count: 400, gain: 'dot-ripple-gain'},
  burst: {points: 100, count: 1100, gain: 'dot-ripple-gain dot-ripple-gain--burst'},
};
// How the counter jumps for each score.
const BUMPS = {
  click: [[{transform: 'scale(1.25)'}, {transform: 'scale(1)'}], {duration: 250, easing: 'ease-out'}],
  charged: [[{transform: 'scale(1)'}, {transform: 'scale(1.55)', offset: 0.3}, {transform: 'scale(0.94)', offset: 0.7}, {transform: 'scale(1)'}], {duration: 450, easing: 'ease-out'}],
  burst: [[
    {transform: 'scale(1)'},
    {transform: 'translate(-3px, 1px) rotate(-8deg) scale(1.9)', offset: 0.12},
    {transform: 'translate(3px, -1px) rotate(7deg) scale(2.2)', offset: 0.24},
    {transform: 'translate(-2px, 1px) rotate(-5deg) scale(2)', offset: 0.36},
    {transform: 'translate(2px, 0) rotate(3deg) scale(1.8)', offset: 0.5},
    {transform: 'rotate(-1deg) scale(1.4)', offset: 0.7},
    {transform: 'scale(1)'},
  ], {duration: 1000, easing: 'ease-out'}],
};
const COUNTER_KEY = 'aspid-dot-ripple-clicks';
const COUNTER_IDLE = 2500; // ms after the last click before the counter fades out

// Shared with the spotlight, which shrinks while the wave charges.
const charge = {pressAt: -1, releaseAt: -Infinity, released: 0, burstAt: -Infinity, x: -1, y: -1};

/** How far the current wave is charged, 0–1, easing out so the last part takes longer; after release it falls back to 0. */
export function chargeLevel(now) {
  if (charge.pressAt >= 0) {
    const p = Math.min((now - charge.pressAt) / CHARGE, 1);
    return 1 - (1 - p) * (1 - p);
  }
  return charge.released * Math.max(0, 1 - (now - charge.releaseAt) / RECOVER);
}

/** How close the held charge is to bursting, 0–1: 0 until it is fully charged, 1 at the burst. */
export function overload(now) {
  if (charge.pressAt < 0) return 0;
  return Math.min(Math.max((now - charge.pressAt - CHARGE) / (EXPLODE - CHARGE), 0), 1);
}

/** Time of the last burst, so the spotlight can go dark for a moment after it. */
export const burstAt = () => charge.burstAt;

// Set while the dot background runs.
let launch = null;

/** Sends a wave of strength `power` (1 is a click) from (`x`, `y`) in the viewport, without scoring it, and with `shake`
 *  shakes the page as a burst does: the introduction's entrance (IntroBanner). Below 997px it runs through the banner's
 *  own dots, and without a banner there does nothing. Does nothing while the dot background is off. */
export function sendWave(x, y, power, {shake = false} = {}) {
  launch?.(x, y, power, shake);
}

const gauss = (u) => Math.exp(-u * u);
const radiusAt = (elapsed) => elapsed * (SPEED + ACCEL * elapsed);

function readColors() {
  const style = getComputedStyle(document.documentElement);
  const hex = (style.getPropertyValue('--venom-ripple').trim() || style.getPropertyValue('--venom-accent').trim());
  const value = parseInt(hex.slice(1), 16);
  return {
    accent: [(value >> 16) & 255, (value >> 8) & 255, value & 255],
    canvas: style.getPropertyValue('--venom-canvas').trim() || '#000',
  };
}

// Signed height of the water at distance `d` from the origin of a wave whose front is at radius `r`.
// A crest is followed by a trough, and every following crest is weaker. Range roughly [-1, 1].
function height(d, r) {
  let h = 0;
  for (const [lag, amplitude] of CRESTS) {
    const crest = r - lag;
    if (crest < -WIDTH * 3) continue;
    const u = (d - crest) / WIDTH;
    h += amplitude * (gauss(u) - 0.55 * gauss(u + 1.5));
  }
  return h;
}

function readClicks() {
  try {
    return Number(localStorage.getItem(COUNTER_KEY)) || 0;
  } catch {
    return 0;
  }
}

function saveClicks(clicks) {
  try {
    localStorage.setItem(COUNTER_KEY, String(clicks));
  } catch {
    // Storage may be blocked; the counter then only lasts for this page.
  }
}

/** Sends a wave through the dot background when the user presses and releases the empty canvas of a docs page — the
 *  longer the hold, the stronger the wave; held too long, the charge bursts on its own — and scores the waves. */
export default function DotRipple() {
  useEffect(() => {
    if (matchMedia('(prefers-reduced-motion: reduce)').matches) return undefined;

    // The canvas is created by the first wave that has dots to run through (place), so a page without dots never holds one.
    let canvas = null;
    let ctx = null;

    const counter = document.createElement('div');
    counter.className = 'dot-ripple-counter';
    counter.setAttribute('aria-hidden', 'true');
    document.body.appendChild(counter);
    let clicks = readClicks();
    // The number on the counter, which lags behind `clicks` while it counts up.
    let shown = clicks;
    let countFrame = 0;
    let counterTimer = 0;

    let waves = [];
    // Burst sparks: position, velocity (px per ms), birth, lifetime and radius.
    let sparks = [];
    let burstTimer = 0;
    let frame = 0;
    let shakeFrame = 0;
    let colors = readColors();

    // The canvas covers the viewport, or the banner while it lies in the banner's layer.
    let host = document.body;
    let areaWidth = 0;
    let areaHeight = 0;

    const resize = () => {
      if (!canvas) return;
      const dpr = Math.min(devicePixelRatio || 1, 2);
      areaWidth = host === document.body ? innerWidth : host.clientWidth;
      areaHeight = host === document.body ? innerHeight : host.clientHeight;
      canvas.width = Math.ceil(areaWidth * dpr);
      canvas.height = Math.ceil(areaHeight * dpr);
      ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
    };

    const render = (now) => {
      ctx.clearRect(0, 0, areaWidth, areaHeight);
      const farthest = Math.hypot(areaWidth, areaHeight) + CRESTS[CRESTS.length - 1][0] + WIDTH * 3;
      waves = waves.filter((wave) => radiusAt(now - wave.start) < farthest);
      sparks = sparks.filter((spark) => now - spark.start < spark.life);

      // Per-frame state of every live wave: its front, how much it currently sways, and the ring of the grid it touches.
      const live = waves.map((wave) => {
        const r = radiusAt(now - wave.start);
        const progress = r / farthest;
        return {
          x: wave.x,
          y: wave.y,
          power: wave.power,
          r,
          sway: 1 + SWAY * (1 - progress) * Math.sin(progress * SWAY_CYCLES * 2 * Math.PI),
          outer: r + WIDTH * 3,
          inner: Math.max(r - CRESTS[CRESTS.length - 1][0] - WIDTH * 3, 0),
        };
      });

      const limit = Math.max(...live.map((wave) => wave.power));

      // Only the part of the grid some wave currently touches is visited.
      let x0 = Infinity, x1 = -Infinity, y0 = Infinity, y1 = -Infinity;
      for (const wave of live) {
        x0 = Math.min(x0, Math.floor((wave.x - wave.outer) / GRID));
        x1 = Math.max(x1, Math.ceil((wave.x + wave.outer) / GRID));
        y0 = Math.min(y0, Math.floor((wave.y - wave.outer) / GRID));
        y1 = Math.max(y1, Math.ceil((wave.y + wave.outer) / GRID));
      }
      x0 = Math.max(x0, 0);
      x1 = Math.min(x1, Math.ceil(areaWidth / GRID));
      y0 = Math.max(y0, 0);
      y1 = Math.min(y1, Math.ceil(areaHeight / GRID));

      for (let gy = y0; gy <= y1; gy++) {
        const cy = gy * GRID + GRID / 2;
        const row = rowWaves(live, cy);
        if (!row.length) continue;
        for (let gx = x0; gx <= x1; gx++) {
          const cx = gx * GRID + GRID / 2;
          // Every wave lifts the dot and pushes it away from its own origin; the dot is drawn once with the sum.
          let h = 0;
          let pushX = 0;
          let pushY = 0;
          for (const {wave, dy, near, far} of row) {
            const dx = cx - wave.x;
            const adx = Math.abs(dx);
            if (adx > far || adx < near) continue;
            const d = Math.hypot(dx, dy);
            if (d < wave.inner || d > wave.outer) continue;
            // A stronger wave is also taller and carries further.
            const waveHeight = wave.power * height(d, wave.r) * wave.sway / (1 + d / (FALLOFF * wave.power));
            h += waveHeight;
            if (d > 0) {
              pushX += dx / d * waveHeight;
              pushY += dy / d * waveHeight;
            }
          }
          if (Math.abs(h) < 0.03) continue;

          const scale = Math.min(Math.max(h, MIN_HEIGHT * limit), MAX_HEIGHT * limit) / h;
          h *= scale;
          const px = cx + PUSH * pushX * scale;
          const py = cy + PUSH * pushY * scale;

          if (h > 0) {
            // Crest: the dot rises — bigger, brighter, pushed outwards. The resting dot is hidden underneath it.
            const [cr, cg, cb] = colors.accent;
            ctx.fillStyle = `rgb(${cr}, ${cg}, ${cb})`;
            ctx.globalAlpha = Math.min(0.15 + h * 0.75, 0.9);
            ctx.beginPath();
            ctx.arc(px, py, BASE_DOT + LIFT * h, 0, Math.PI * 2);
            ctx.fill();
            ctx.globalAlpha = 1;
          } else {
            // Trough: the dot sinks — the resting dot is covered with the canvas colour and a fainter one drawn.
            ctx.fillStyle = colors.canvas;
            ctx.beginPath();
            ctx.arc(cx, cy, BASE_DOT + 0.6, 0, Math.PI * 2);
            ctx.fill();
            ctx.fillStyle = 'rgb(120, 128, 140)';
            ctx.globalAlpha = Math.max(0.12 + h * 0.12, 0.02);
            ctx.beginPath();
            ctx.arc(px, py, Math.max(BASE_DOT + h * 0.6, 0.3), 0, Math.PI * 2);
            ctx.fill();
            ctx.globalAlpha = 1;
          }
        }
      }

      // Sparks fly out, slow down (distance ∫ e^{-kt} = (1 − e^{-kt}) / k) and fade as they go.
      const [sr, sg, sb] = colors.accent;
      ctx.fillStyle = `rgb(${sr}, ${sg}, ${sb})`;
      for (const spark of sparks) {
        const t = now - spark.start;
        const travel = (1 - Math.exp(-SPARK_DRAG * t)) / SPARK_DRAG;
        const life = 1 - t / spark.life;
        ctx.globalAlpha = Math.min(life * 1.4, 0.95);
        ctx.beginPath();
        ctx.arc(spark.x + spark.vx * travel, spark.y + spark.vy * travel, spark.r * (0.4 + 0.6 * life), 0, Math.PI * 2);
        ctx.fill();
      }
      ctx.globalAlpha = 1;

      frame = waves.length || sparks.length ? requestAnimationFrame(render) : 0;
    };

    // On laptop widths the floating TOC button takes the corner, so the number shows inside it in place of its icon.
    let tocButton = null;
    const setShown = (value) => {
      shown = value;
      counter.textContent = value.toLocaleString();
      counter.toggleAttribute('data-long', counter.textContent.length > 3);
    };

    // Counts up from the number on display to `clicks`, fast at first and slowing into the final value.
    const countUp = (duration) => {
      cancelAnimationFrame(countFrame);
      if (!duration) {
        setShown(clicks);
        return;
      }
      const from = shown;
      const start = performance.now();
      const step = (now) => {
        const p = Math.min((now - start) / duration, 1);
        setShown(Math.round(from + (clicks - from) * (1 - (1 - p) ** 3)));
        countFrame = p < 1 ? requestAnimationFrame(step) : 0;
      };
      countFrame = requestAnimationFrame(step);
    };

    // "+N" rises from the counter and fades out.
    const floatGain = (points, className) => {
      const rect = counter.getBoundingClientRect();
      const gain = document.createElement('div');
      gain.className = className;
      gain.setAttribute('aria-hidden', 'true');
      gain.textContent = `+${points}`;
      Object.assign(gain.style, {left: `${rect.left + rect.width / 2}px`, top: `${rect.top}px`});
      document.body.appendChild(gain);
      const big = points >= SCORES.burst.points;
      gain.animate([
        {transform: 'translate(-50%, -40%) scale(0.6)', opacity: 0},
        {transform: `translate(-50%, -110%) scale(${big ? 1.3 : 1})`, opacity: 1, offset: 0.2},
        {transform: `translate(-50%, ${big ? -260 : -200}%) scale(1)`, opacity: 0},
      ], {duration: big ? 1400 : 900, easing: 'ease-out'}).finished.then(() => gain.remove(), () => gain.remove());
    };

    const showCount = (score) => {
      tocButton?.removeAttribute('data-counting');
      tocButton = document.querySelector('.floating-toc__button');
      if (tocButton && !tocButton.getClientRects().length) tocButton = null;
      if (tocButton) {
        const rect = tocButton.getBoundingClientRect();
        Object.assign(counter.style, {left: `${rect.left}px`, top: `${rect.top}px`, width: `${rect.width}px`, height: `${rect.height}px`});
        tocButton.setAttribute('data-counting', '');
      } else {
        counter.removeAttribute('style');
      }
      counter.toggleAttribute('data-in-button', Boolean(tocButton));
      const {points, count, gain} = SCORES[score];
      countUp(count);
      counter.toggleAttribute('data-on', true);
      counter.toggleAttribute('data-burst', score === 'burst');
      counter.animate(...BUMPS[score]);
      if (gain) floatGain(points, gain);
      clearTimeout(counterTimer);
      counterTimer = setTimeout(() => {
        counter.removeAttribute('data-burst');
        counter.removeAttribute('data-on');
        tocButton?.removeAttribute('data-counting');
      }, COUNTER_IDLE + count);
    };

    const stopCharging = (now) => {
      clearTimeout(burstTimer);
      charge.released = chargeLevel(now);
      charge.releaseAt = now;
      charge.pressAt = -1;
    };

    // Puts the canvas where the dots show: on the body, or in the banner's layer on a narrow screen. Returns the offset of
    // the canvas in the viewport, or null when a narrow page has no banner, so no dots to run through.
    const place = () => {
      narrow ??= matchMedia('(max-width: 996px)');
      const target = narrow.matches ? document.querySelector(BANNER_LAYER) : document.body;
      if (!target) return null;
      if (!canvas) {
        canvas = document.createElement('canvas');
        canvas.className = 'dot-ripple-canvas';
        canvas.setAttribute('aria-hidden', 'true');
        ctx = canvas.getContext('2d');
      }
      // A banner removed from the page takes the canvas with it, so it comes back to the body on the next wave.
      if (canvas.parentNode !== target) {
        // A new canvas has no waves to drop; the sparks of a burst that charged before it are waiting.
        const moved = canvas.parentNode !== null;
        target.append(canvas);
        host = target;
        resize();
        if (moved) {
          waves = [];
          sparks = [];
        }
      }
      if (target === document.body) return {x: 0, y: 0};
      const box = target.getBoundingClientRect();
      return {x: box.left, y: box.top};
    };

    // Takes (x, y) in the viewport; returns false when there are no dots for the wave.
    const spawn = (x, y, now, power) => {
      const offset = place();
      if (!offset) return false;
      colors = readColors();
      waves.push({x: x - offset.x, y: y - offset.y, start: now, power});
      if (!frame) frame = requestAnimationFrame(render);
      return true;
    };
    const release = (x, y, now, power, score) => {
      spawn(x, y, now, power);
      clicks += SCORES[score].points;
      saveClicks(clicks);
      showCount(score);
    };

    // The page swings on two unrelated frequencies, so the shake has no clear direction, and settles down.
    const root = document.documentElement.style;
    const stopShake = () => {
      cancelAnimationFrame(shakeFrame);
      shakeFrame = 0;
      root.removeProperty('--dot-shake-x');
      root.removeProperty('--dot-shake-y');
    };
    const shake = (start) => {
      const phase = Math.random() * Math.PI * 2;
      const step = (now) => {
        const p = (now - start) / SHAKE_TIME;
        if (p >= 1) {
          stopShake();
          return;
        }
        // Whole pixels keep the text sharp.
        const amplitude = SHAKE * (1 - p) ** 2;
        root.setProperty('--dot-shake-x', `${Math.round(amplitude * Math.sin(now / 16 + phase))}px`);
        root.setProperty('--dot-shake-y', `${Math.round(amplitude * Math.sin(now / 19 + 2 * phase))}px`);
        shakeFrame = requestAnimationFrame(step);
      };
      cancelAnimationFrame(shakeFrame);
      shakeFrame = requestAnimationFrame(step);
    };

    launch = (x, y, power, shakes) => {
      const now = performance.now();
      if (spawn(x, y, now, power) && shakes) shake(now);
    };

    // The gathered light bursts: its dots fly apart with loose sparks between them, a wave stronger than any
    // release runs out, and the page shakes. The button is still down, so the coming release sends nothing.
    const burst = () => {
      const now = performance.now();
      const {x, y} = charge;
      stopCharging(now);
      charge.released = 0;
      charge.burstAt = now;
      const spark = (sx, sy, angle, speed) => sparks.push({
        x: sx, y: sy, vx: Math.cos(angle) * speed, vy: Math.sin(angle) * speed,
        start: now, life: SPARK_LIFE * (0.6 + 0.8 * Math.random()), r: 1.4 + 1.4 * Math.random(),
      });
      for (let gy = Math.floor((y - BURST_REACH) / GRID); gy <= Math.ceil((y + BURST_REACH) / GRID); gy++) {
        for (let gx = Math.floor((x - BURST_REACH) / GRID); gx <= Math.ceil((x + BURST_REACH) / GRID); gx++) {
          const cx = gx * GRID + GRID / 2;
          const cy = gy * GRID + GRID / 2;
          if (Math.hypot(cx - x, cy - y) > BURST_REACH) continue;
          spark(cx, cy, Math.atan2(cy - y, cx - x) + (Math.random() - 0.5) * 0.5, 0.45 + 0.5 * Math.random());
        }
      }
      for (let i = 0; i < SPARKS; i++) spark(x, y, Math.random() * Math.PI * 2, 0.3 + 0.8 * Math.random());
      release(x, y, now, BURST_POWER, 'burst');
      shake(now);
    };

    const onPointerDown = (event) => {
      if (event.button !== 0 || !isCanvas(event.target) || event.target.closest(TEXT)) return;
      charge.pressAt = performance.now();
      charge.x = event.clientX;
      charge.y = event.clientY;
      clearTimeout(burstTimer);
      burstTimer = setTimeout(burst, EXPLODE);
    };

    const onPointerMove = (event) => {
      if (charge.pressAt < 0) return;
      charge.x = event.clientX;
      charge.y = event.clientY;
    };

    const onPointerUp = (event) => {
      if (charge.pressAt < 0 || event.button !== 0) return;
      const now = performance.now();
      const level = chargeLevel(now);
      stopCharging(now);
      release(event.clientX, event.clientY, now, MIN_POWER + (MAX_POWER - MIN_POWER) * level, level >= FULL ? 'charged' : 'click');
    };

    // A press the browser takes over (a touch scroll) or a lost window lets the charge go without a wave.
    const onCancel = () => { if (charge.pressAt >= 0) stopCharging(performance.now()); };

    document.addEventListener('pointerdown', onPointerDown);
    document.addEventListener('pointermove', onPointerMove, {passive: true});
    document.addEventListener('pointerup', onPointerUp);
    document.addEventListener('pointercancel', onCancel);
    addEventListener('blur', onCancel);
    addEventListener('resize', resize);
    return () => {
      document.removeEventListener('pointerdown', onPointerDown);
      document.removeEventListener('pointermove', onPointerMove);
      document.removeEventListener('pointerup', onPointerUp);
      document.removeEventListener('pointercancel', onCancel);
      removeEventListener('blur', onCancel);
      launch = null;
      charge.pressAt = -1;
      clearTimeout(burstTimer);
      removeEventListener('resize', resize);
      if (frame) cancelAnimationFrame(frame);
      stopShake();
      clearTimeout(counterTimer);
      cancelAnimationFrame(countFrame);
      tocButton?.removeAttribute('data-counting');
      canvas?.remove();
      counter.remove();
    };
  }, []);
  return null;
}
