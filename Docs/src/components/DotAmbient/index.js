import {useEffect} from 'react';

const GRID = 20;          // px, must match the CSS dot texture (background-size)
const MIN_WIDTH = 997;    // px, below it the article covers the canvas, so only the banner's glow is drawn
const FRAME = 50;         // ms between redraws: the glow changes slowly, 20 fps is enough
const DEPTH = 0.2;        // share of the viewport height the glow reaches at its highest, next to the article
const SIDE = 0.3;         // share of that height left at the outer edges of the screen: the glow slopes down to them
const ARTICLE = '[class*="docMainContainer_"] > .container > .row > .col:first-child';
// The introduction's banner lies open over the canvas (custom.css), and a lower glow rises from its bottom edge too.
const BANNER = '.readme-banner';
// Below MIN_WIDTH the banner paints its own dots over the fixed canvases (custom.css), so the canvases move into this
// layer of the banner, between those dots and its content, and draw in the banner's coordinates.
const BANNER_LAYER = '.readme-banner__ambient';
const BANNER_DEPTH = 0.35; // share of the banner height that glow reaches at its highest
const BANNER_SLOPE = 0.25; // share of the banner width over which it slopes down and dims out to the banner's sides
// During the entrance (src/intro.js) that glow waits until IntroBanner stamps `data-glow-at`, then grows in.
const GLOW_IN = 700;      // ms
// Sparks rise from the article's top edge into that glow and burn out on the way.
const SPARKS = 4;                // sparks per second across the banner, fewer towards its sides
const SPARK_LIFE = [1600, 2800]; // ms, of the shortest and the longest spark
const SPARK_RISE = [0.3, 0.6];   // share of the banner height a spark climbs, least and most
const SPARK_SWAY = 8;            // px a spark drifts from side to side as it climbs
// Waves that roughen the top edge of the glow: [wavelength px, drift px per ms, amplitude]. Different lengths and speeds,
// some drifting left and some right, so the edge keeps changing without ever moving as a whole.
const EDGE = [[1100, 0.006, 0.5], [530, -0.011, 0.3], [260, 0.017, 0.2]];

// A stable pseudo-random phase per dot, so the dots in the glow twinkle out of step.
const hash = (x, y) => {
  const h = Math.sin(x * 127.1 + y * 311.7) * 43758.5453;
  return h - Math.floor(h);
};

const between = ([low, high]) => low + (high - low) * Math.random();
const smooth = (p) => {
  const q = Math.min(Math.max(p, 0), 1);
  return q * q * (3 - 2 * q);
};

// Sum of drifting sines at `x`, mapped to [0, 1].
const wave = (layers, x, now) => layers.reduce((sum, [length, drift, amplitude]) =>
  sum + amplitude * (0.5 + 0.5 * Math.sin((x + now * drift) / length * 2 * Math.PI)), 0);

function readColors() {
  const style = getComputedStyle(document.documentElement);
  return {
    accent: style.getPropertyValue('--venom-ripple').trim() || style.getPropertyValue('--venom-accent').trim(),
    dark: document.documentElement.dataset.theme !== 'light',
  };
}

/** Keeps an uneven glow along the bottom of a docs page's dot background: highest next to the article, sloping down to
 *  the edges of the screen, its top edge slowly changing shape. On the introduction a lower glow rises from the banner's
 *  bottom edge as well, and sparks fly up from that edge into it. Below MIN_WIDTH only the banner's glow and sparks are
 *  drawn, inside the banner. Where no dots are drawn (other pages, a narrow page without the banner), it holds no
 *  canvas and runs no frames. */
export default function DotAmbient() {
  useEffect(() => {
    if (matchMedia('(prefers-reduced-motion: reduce)').matches) return undefined;

    const root = document.documentElement;
    // The glow changes slowly and is drawn at FRAME. The sparks move fast, so they have a canvas of their own that is
    // drawn on every frame without the glow.
    const layer = () => {
      const canvas = document.createElement('canvas');
      canvas.className = 'dot-ambient-canvas';
      canvas.setAttribute('aria-hidden', 'true');
      document.body.appendChild(canvas);
      return canvas.getContext('2d');
    };
    let ctx = null;
    let sparkCtx = null;
    // The canvases cover the viewport, or the banner while they lie in its layer.
    let host = document.body;
    let areaWidth = 0;
    let areaHeight = 0;

    const resize = () => {
      if (!ctx) return;
      const dpr = Math.min(devicePixelRatio || 1, 2);
      areaWidth = host === document.body ? innerWidth : host.clientWidth;
      areaHeight = host === document.body ? innerHeight : host.clientHeight;
      for (const context of [ctx, sparkCtx]) {
        context.canvas.width = Math.ceil(areaWidth * dpr);
        context.canvas.height = Math.ceil(areaHeight * dpr);
        context.setTransform(dpr, 0, 0, dpr, 0, 0);
      }
    };

    let frame = 0;
    let last = 0;
    let colors = null;
    let colorsAt = 0;
    let article = null;
    let banner = null;
    // Each spark: its place across the banner (0–1), birth, lifetime, climb (share of the banner height), sway phase
    // and radius. Kept relative to the banner, so the sparks scroll with it.
    let sparks = [];
    let sparkDue = 0;
    let sparkAt = 0;
    let sparksDrawn = false;

    // Lights the dot (gx, gy) centred at (cx, cy), `u` reach lengths away from the edge its glow rises from, `dim` times
    // its full brightness.
    const light = (cx, cy, u, gx, gy, now, dim = 1) => {
      const i = dim * Math.exp(-u * u * 1.8) * (0.6 + 0.4 * Math.sin(now * 0.0015 + hash(gx, gy) * 2 * Math.PI));
      if (i < 0.05) return;
      // The light canvas is pale grey, where the light ripple green shows faintly: it needs about twice the opacity.
      ctx.globalAlpha = colors.dark ? Math.min(0.06 + i * 0.6, 0.7) : Math.min(0.1 + i * 0.8, 0.9);
      ctx.beginPath();
      ctx.arc(cx, cy, 1 + 0.8 * i, 0, Math.PI * 2);
      ctx.fill();
    };

    // The banner's box in the canvases' coordinates and how far its glow has grown in, or null while the banner is off
    // screen or its glow has not started. Over the fixed dots the banner scrolls, so its box is read on every frame.
    const bannerGlow = (now, height) => {
      const box = banner?.isConnected ? banner.getBoundingClientRect() : null;
      if (!box || box.bottom <= 0 || box.top >= height) return null;
      let grown = 1;
      if (root.hasAttribute('data-intro')) {
        const p = Math.min(Math.max((now - Number(banner.dataset.glowAt ?? Infinity)) / GLOW_IN, 0), 1);
        grown = p * p * (3 - 2 * p);
      }
      if (grown <= 0) return null;
      if (host === document.body) return {box, grown};
      return {box: {left: 0, top: 0, right: areaWidth, bottom: areaHeight, width: areaWidth, height: areaHeight}, grown};
    };

    // The glow along the bottom of the screen.
    const drawFloor = (now, width, height, breathe) => {
      // Without an article the slope runs to the middle of the screen.
      const left = article ? article.left : width / 2;
      const right = article ? article.right : width / 2;

      const maxReach = height * DEPTH;
      const rows = Math.ceil(maxReach * 1.6 / GRID);
      const lastRow = Math.floor(height / GRID);
      for (let gx = 0; gx <= Math.ceil(width / GRID); gx++) {
        const cx = gx * GRID + GRID / 2;
        // 0 at the outer edge of the screen, 1 at the article and behind it.
        const slope = cx < left ? cx / left : cx > right ? (width - cx) / (width - right) : 1;
        const reach = maxReach * breathe * (SIDE + (1 - SIDE) * Math.min(Math.max(slope, 0), 1)) * (0.5 + 0.5 * wave(EDGE, cx, now));
        for (let gy = lastRow; gy >= lastRow - rows; gy--) {
          const cy = gy * GRID + GRID / 2;
          const u = (height - cy) / reach;
          if (u > 1.6) break;
          light(cx, cy, u, gx, gy, now);
        }
      }
    };

    // The glow rising from the banner's bottom edge.
    const drawBannerGlow = (now, {box, grown}, breathe) => {
      const bannerReach = box.height * BANNER_DEPTH * breathe * grown;
      for (let gx = Math.ceil((box.left - GRID / 2) / GRID); gx * GRID + GRID / 2 < box.right; gx++) {
        const cx = gx * GRID + GRID / 2;
        const slope = Math.min(cx - box.left, box.right - cx) / (box.width * BANNER_SLOPE);
        // Shifted along the waves, so this edge does not copy the shape of the bottom glow's.
        const reach = bannerReach * (SIDE + (1 - SIDE) * Math.min(slope, 1)) * (0.5 + 0.5 * wave(EDGE, cx + 3700, now));
        // Towards the banner's sides it also dims out, so it does not end at a hard line beside the canvas's plain dots.
        const dim = smooth(slope);
        for (let gy = Math.floor((box.bottom - GRID / 2) / GRID); gy * GRID + GRID / 2 >= box.top; gy--) {
          const cy = gy * GRID + GRID / 2;
          const u = (box.bottom - cy) / reach;
          if (u > 1.6) break;
          light(cx, cy, u, gx, gy, now, dim);
        }
      }
    };

    const drawSparks = (now, elapsed, glow) => {
      if (sparksDrawn) sparkCtx.clearRect(0, 0, areaWidth, areaHeight);
      sparksDrawn = false;
      if (!glow) return;
      const {box, grown} = glow;

      // New sparks: as many as are due, fewer where the glow slopes down to the sides, none before it has grown in.
      sparkDue += elapsed / 1000 * SPARKS * grown;
      for (; sparkDue >= 1; sparkDue--) {
        const x = 0.04 + 0.92 * Math.random();
        if (Math.random() > SIDE + (1 - SIDE) * Math.min(Math.min(x, 1 - x) / BANNER_SLOPE, 1)) continue;
        sparks.push({x, born: now, life: between(SPARK_LIFE), rise: between(SPARK_RISE), phase: Math.random() * 2 * Math.PI, r: 1 + 0.8 * Math.random()});
      }
      sparks = sparks.filter((spark) => now - spark.born < spark.life);
      sparkCtx.fillStyle = colors.accent;
      for (const spark of sparks) {
        const t = (now - spark.born) / spark.life;
        // It climbs fast at first and slows down, sways more the higher it gets, flares up and burns out.
        const x = box.left + spark.x * box.width + SPARK_SWAY * t * Math.sin(2 * Math.PI * t + spark.phase);
        const y = box.bottom - spark.rise * box.height * (1 - (1 - t) ** 2);
        const i = smooth(t / 0.1) * (1 - smooth((t - 0.4) / 0.6));
        sparkCtx.globalAlpha = (colors.dark ? 0.18 : 0.22) * i;
        sparkCtx.beginPath();
        sparkCtx.arc(x, y, spark.r * 3.5, 0, Math.PI * 2);
        sparkCtx.fill();
        sparkCtx.globalAlpha = (colors.dark ? 0.9 : 0.95) * i;
        sparkCtx.beginPath();
        sparkCtx.arc(x, y, spark.r, 0, Math.PI * 2);
        sparkCtx.fill();
      }
      sparkCtx.globalAlpha = 1;
      sparksDrawn = sparks.length > 0;
    };

    const render = (now) => {
      frame = requestAnimationFrame(render);
      // A hidden tab or a long frame must not release a burst of sparks at once.
      const elapsed = Math.min(now - sparkAt, 200);
      sparkAt = now;

      const width = innerWidth;
      const height = innerHeight;
      const docs = root.classList.contains('docs-doc-page');
      const wide = width >= MIN_WIDTH;
      // The theme or accent may change while the page is open.
      if (docs && (!colors || now - colorsAt > 1000)) {
        colors = readColors();
        article = document.querySelector(ARTICLE)?.getBoundingClientRect() ?? null;
        banner = document.querySelector(BANNER);
        colorsAt = now;
      }
      // A banner removed from the page takes the canvases with it, so they come back to the body.
      const target = (docs && !wide && banner?.isConnected && banner.querySelector(BANNER_LAYER)) || document.body;
      if (ctx.canvas.parentNode !== target) {
        target.append(ctx.canvas, sparkCtx.canvas);
        host = target;
        resize();
        last = 0;
      }

      const glow = docs ? bannerGlow(now, height) : null;
      // While the banner glow grows in, it is drawn on every frame as well.
      if (now - last >= (glow && glow.grown < 1 ? 0 : FRAME)) {
        last = now;
        ctx.clearRect(0, 0, areaWidth, areaHeight);
        if (docs) {
          const breathe = 0.85 + 0.15 * Math.sin(now * 0.0004);
          ctx.fillStyle = colors.accent;
          if (wide) drawFloor(now, width, height, breathe);
          if (glow) drawBannerGlow(now, glow, breathe);
          ctx.globalAlpha = 1;
        }
      }
      drawSparks(now, elapsed, glow);
    };

    // Dots are drawn on a docs page from MIN_WIDTH up, and below it only in the introduction's banner.
    const shown = () => root.classList.contains('docs-doc-page') && (innerWidth >= MIN_WIDTH || document.querySelector(BANNER_LAYER) !== null);

    const start = () => {
      ctx = layer();
      sparkCtx = layer();
      host = document.body;
      resize();
      colors = null;
      sparks = [];
      sparksDrawn = false;
      last = 0;
      sparkAt = performance.now();
      frame = requestAnimationFrame(render);
    };

    const stop = () => {
      cancelAnimationFrame(frame);
      frame = 0;
      ctx.canvas.remove();
      sparkCtx.canvas.remove();
      ctx = null;
      sparkCtx = null;
      article = null;
      banner = null;
    };

    // The page, its width or its banner may change while the page is open.
    const sync = () => {
      const on = shown();
      if (on && !ctx) start();
      else if (!on && ctx) stop();
    };

    // A hidden tab stops drawing entirely.
    const onVisibility = () => {
      if (document.hidden) {
        cancelAnimationFrame(frame);
        frame = 0;
      } else if (ctx && !frame) {
        frame = requestAnimationFrame(render);
      }
    };

    // While the page scrolls, the banner glow on the fixed canvas is redrawn on every frame, so it does not trail behind
    // the banner. In the banner's layer it scrolls with the banner.
    const onScroll = () => {
      if (banner && host === document.body) last = 0;
    };

    const onResize = () => {
      sync();
      resize();
    };

    // Navigating changes the classes of the root; the banner of a new page is in place by then.
    const observer = new MutationObserver(sync);
    observer.observe(root, {attributes: true, attributeFilter: ['class']});
    addEventListener('resize', onResize);
    addEventListener('scroll', onScroll, {passive: true});
    document.addEventListener('visibilitychange', onVisibility);
    sync();
    return () => {
      observer.disconnect();
      removeEventListener('resize', onResize);
      removeEventListener('scroll', onScroll);
      document.removeEventListener('visibilitychange', onVisibility);
      if (ctx) stop();
    };
  }, []);
  return null;
}
