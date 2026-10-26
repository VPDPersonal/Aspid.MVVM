/**
 * Generate the data of the introduction banner's logo animation (src/components/IntroBanner) from the logo image:
 * - `media/logo-body.png`: where each pixel lies along the snake's body, 1 at the tail .. 255 at the snout, 0 off it;
 * - `mesh.js`: the low-poly triangles the logo assembles from, ordered from the tail to the snout;
 * - `media/eyelids.png`: a closed eyelid per accent, side by side in the order of ACCENTS in src/accents.js. Its first cell,
 *   the green lid, is the source: it is kept as it is, and the other cells are recoloured from it.
 * Every accent's logo has the same shape, so the green one serves for the shape. Run it again only when a logo changes:
 *   node scripts/intro-logo-data.mjs
 * Needs `ffmpeg` on PATH to decode the WebP and encode the PNG.
 */
import fs from 'node:fs';
import {execFileSync} from 'node:child_process';
import {fileURLToPath} from 'node:url';
import {ACCENTS} from '../src/accents.js';

const dir = fileURLToPath(new URL('../src/components/IntroBanner/', import.meta.url));
const W = 640;
const H = 634;
// The image as raw RGBA, optionally cut down by an ffmpeg filter first.
const decode = (file, width, height, filter = 'null') => {
  const rgba = execFileSync('ffmpeg', ['-loglevel', 'error', '-i', `${dir}media/${file}`, '-vf', filter, '-f', 'rawvideo', '-pix_fmt', 'rgba', '-'],
    {maxBuffer: width * height * 8});
  if (rgba.length !== width * height * 4) throw new Error(`${file} is not ${width}×${height}`);
  return rgba;
};
const pixels = decode('logo-green.webp', W, H);
const alphaAt = (x, y) => pixels[(Math.min(H - 1, Math.max(0, Math.round(y))) * W + Math.min(W - 1, Math.max(0, Math.round(x)))) * 4 + 3];

// ---------- The body: an outer ring from the tail, then the inner S up to the snout ----------
// The ring starts at its junction with the S at the bottom and runs clockwise once around; the S, drawn here from that
// junction to the snout, lies over the ring where they cross.
const CENTER = {x: 320, y: 317};
const RING = 254; // px, radius of the ring's centre line
const S_PATH = [[400, 565], [330, 572], [262, 552], [222, 505], [212, 440], [232, 375], [285, 328], [365, 300], [450, 282],
  [515, 248], [545, 185], [538, 115], [498, 66], [430, 46], [345, 60], [262, 92], [198, 122]];
const TAIL = Math.atan2(S_PATH[0][1] - CENTER.y, S_PATH[0][0] - CENTER.x);
const along = [0];
for (let i = 1; i < S_PATH.length; i++) along.push(along[i - 1] + Math.hypot(S_PATH[i][0] - S_PATH[i - 1][0], S_PATH[i][1] - S_PATH[i - 1][1]));
const S_LENGTH = along.at(-1);
const RING_LENGTH = 2 * Math.PI * RING;
const LENGTH = RING_LENGTH + S_LENGTH;

// Distance from (x, y) to the S and how far along the S its nearest point lies.
function nearestOnS(x, y) {
  let dist = Infinity;
  let s = 0;
  for (let i = 1; i < S_PATH.length; i++) {
    const [ax, ay] = S_PATH[i - 1];
    const dx = S_PATH[i][0] - ax;
    const dy = S_PATH[i][1] - ay;
    const length2 = dx * dx + dy * dy;
    const u = Math.min(1, Math.max(0, ((x - ax) * dx + (y - ay) * dy) / length2));
    const d = Math.hypot(x - ax - u * dx, y - ay - u * dy);
    if (d < dist) {
      dist = d;
      s = along[i - 1] + u * Math.sqrt(length2);
    }
  }
  return {dist, s};
}

// The S is yellow-green and the ring blue-green: a hue below 130° marks the S, smoothed so facets and outlines do not
// speckle the split.
const hueScore = new Float32Array(W * H);
for (let i = 0; i < W * H; i++) {
  const r = pixels[i * 4] / 255;
  const g = pixels[i * 4 + 1] / 255;
  const b = pixels[i * 4 + 2] / 255;
  const max = Math.max(r, g, b);
  const chroma = max - Math.min(r, g, b);
  let hue = 0;
  if (chroma > 0.02) {
    if (max === r) hue = 60 * (((g - b) / chroma) % 6);
    else if (max === g) hue = 60 * ((b - r) / chroma + 2);
    else hue = 60 * ((r - g) / chroma + 4);
  }
  if (hue < 0) hue += 360;
  hueScore[i] = pixels[i * 4 + 3] < 128 ? 0.5 : hue < 130 || hue > 300 ? 1 : 0;
}

function boxBlur(source, radius) {
  const rows = new Float32Array(W * H);
  const out = new Float32Array(W * H);
  const sum = new Float64Array(Math.max(W, H) + 1);
  for (let y = 0; y < H; y++) {
    for (let x = 0; x < W; x++) sum[x + 1] = sum[x] + source[y * W + x];
    for (let x = 0; x < W; x++) {
      const lo = Math.max(0, x - radius);
      const hi = Math.min(W, x + radius + 1);
      rows[y * W + x] = (sum[hi] - sum[lo]) / (hi - lo);
    }
  }
  for (let x = 0; x < W; x++) {
    for (let y = 0; y < H; y++) sum[y + 1] = sum[y] + rows[y * W + x];
    for (let y = 0; y < H; y++) {
      const lo = Math.max(0, y - radius);
      const hi = Math.min(H, y + radius + 1);
      out[y * W + x] = (sum[hi] - sum[lo]) / (hi - lo);
    }
  }
  return out;
}
const sLikeness = boxBlur(boxBlur(hueScore, 5), 5);

// Position along the body, 0 at the tail .. 1 at the snout, or -1 off the logo.
function bodyAt(x, y) {
  x = Math.round(x);
  y = Math.round(y);
  if (alphaAt(x, y) < 16) return -1;
  const near = nearestOnS(x, y);
  const offRing = Math.abs(Math.hypot(x - CENTER.x, y - CENTER.y) - RING) > 72;
  // Close to the lower half of the S its darker bowl counts as the S whatever its hue; near the snout the ring stays ring.
  const onBowl = near.dist < 42 && near.s < S_LENGTH / 2;
  if (onBowl || (near.dist < 95 && (offRing || sLikeness[y * W + x] > 0.5))) return (RING_LENGTH + near.s) / LENGTH;
  let angle = (Math.atan2(y - CENTER.y, x - CENTER.x) - TAIL) % (2 * Math.PI);
  if (angle < 0) angle += 2 * Math.PI;
  return angle * RING / LENGTH;
}

// ---------- logo-body.png, at half the logo's size ----------
const FW = W / 2;
const FH = Math.ceil(H / 2);
const field = Buffer.alloc(FW * FH);
for (let y = 0; y < FH; y++) {
  for (let x = 0; x < FW; x++) {
    const t = bodyAt(x * 2, y * 2);
    field[y * FW + x] = t < 0 ? 0 : 1 + Math.round(t * 254);
  }
}
execFileSync('ffmpeg', ['-loglevel', 'error', '-y', '-f', 'rawvideo', '-pix_fmt', 'gray', '-s', `${FW}x${FH}`, '-i', '-', `${dir}media/logo-body.png`],
  {input: field});

// ---------- mesh.js: points on the outline and inside, Delaunay, triangles off the logo dropped ----------
let seed = 7;
const random = () => (seed = (seed * 16807) % 2147483647) / 2147483647;

function delaunay(points) {
  const n = points.length;
  const all = points.concat([{x: -1e5, y: -1e5}, {x: 2e5, y: -1e5}, {x: W / 2, y: 2e5}]);
  const circle = (a, b, c) => {
    const A = all[a];
    const B = all[b];
    const C = all[c];
    const d = 2 * (A.x * (B.y - C.y) + B.x * (C.y - A.y) + C.x * (A.y - B.y));
    const a2 = A.x * A.x + A.y * A.y;
    const b2 = B.x * B.x + B.y * B.y;
    const c2 = C.x * C.x + C.y * C.y;
    const x = (a2 * (B.y - C.y) + b2 * (C.y - A.y) + c2 * (A.y - B.y)) / d;
    const y = (a2 * (C.x - B.x) + b2 * (A.x - C.x) + c2 * (B.x - A.x)) / d;
    return {a, b, c, x, y, r2: (A.x - x) ** 2 + (A.y - y) ** 2};
  };
  let triangles = [circle(n, n + 1, n + 2)];
  for (let i = 0; i < n; i++) {
    const p = all[i];
    const kept = [];
    const edges = new Map();
    for (const t of triangles) {
      if ((p.x - t.x) ** 2 + (p.y - t.y) ** 2 >= t.r2) {
        kept.push(t);
        continue;
      }
      for (const [u, v] of [[t.a, t.b], [t.b, t.c], [t.c, t.a]]) {
        const key = u < v ? `${u},${v}` : `${v},${u}`;
        edges.set(key, edges.has(key) ? null : [u, v]);
      }
    }
    for (const edge of edges.values()) if (edge) kept.push(circle(edge[0], edge[1], i));
    triangles = kept;
  }
  return triangles.filter((t) => t.a < n && t.b < n && t.c < n).map((t) => [t.a, t.b, t.c]);
}

const points = [];
const CELL = 12;
const grid = new Map();
const crowded = (x, y, spacing) => {
  const cx = Math.floor(x / CELL);
  const cy = Math.floor(y / CELL);
  const reach = Math.ceil(spacing / CELL);
  for (let j = cy - reach; j <= cy + reach; j++) {
    for (let i = cx - reach; i <= cx + reach; i++) {
      for (const q of grid.get(`${i},${j}`) ?? []) if ((q.x - x) ** 2 + (q.y - y) ** 2 < spacing * spacing) return true;
    }
  }
  return false;
};
const addPoint = (x, y) => {
  const point = {x: Math.round(x), y: Math.round(y)};
  points.push(point);
  const key = `${Math.floor(x / CELL)},${Math.floor(y / CELL)}`;
  if (!grid.has(key)) grid.set(key, []);
  grid.get(key).push(point);
};

const outline = [];
for (let y = 0; y < H; y += 3) {
  for (let x = 0; x < W; x += 3) {
    if (alphaAt(x, y) < 128) continue;
    if (alphaAt(x - 3, y) < 128 || alphaAt(x + 3, y) < 128 || alphaAt(x, y - 3) < 128 || alphaAt(x, y + 3) < 128) outline.push([x, y]);
  }
}
for (let i = outline.length - 1; i > 0; i--) {
  const j = Math.floor(random() * (i + 1));
  [outline[i], outline[j]] = [outline[j], outline[i]];
}
for (const [x, y] of outline) if (!crowded(x, y, 22)) addPoint(x, y);
for (let i = 0; i < 9000; i++) {
  const x = random() * W;
  const y = random() * H;
  if (alphaAt(x, y) >= 160 && !crowded(x, y, 31)) addPoint(x, y);
}

const pieces = [];
for (const t of delaunay(points)) {
  const p = t.map((n) => points[n]);
  const cx = (p[0].x + p[1].x + p[2].x) / 3;
  const cy = (p[0].y + p[1].y + p[2].y) / 3;
  const mids = [0, 1, 2].map((n) => [(p[n].x + p[(n + 1) % 3].x) / 2, (p[n].y + p[(n + 1) % 3].y) / 2]);
  if (alphaAt(cx, cy) < 110 || mids.some(([x, y]) => alphaAt(x, y) < 40)) continue;
  let body = bodyAt(cx, cy);
  for (const [x, y] of mids) if (body < 0) body = bodyAt(x, y);
  pieces.push({t, body: body < 0 ? 0.5 : body});
}
pieces.sort((a, b) => a.body - b.body);

const used = [...new Set(pieces.flatMap((piece) => piece.t))].sort((a, b) => a - b);
const index = new Map(used.map((n, i) => [n, i]));
const lines = (values, perLine) => {
  const out = [];
  for (let i = 0; i < values.length; i += perLine) out.push(`  ${values.slice(i, i + perLine).join(', ')},`);
  return out.join('\n');
};
fs.writeFileSync(`${dir}mesh.js`, `// Generated by scripts/intro-logo-data.mjs from media/logo-green.webp: do not edit by hand.

/** Corners of the logo's low-poly pieces, as x, y pairs in the logo's ${W}×${H} pixels. */
export const POINTS = [
${lines(used.flatMap((n) => [points[n].x, points[n].y]), 20)}
];

/** The pieces, three indices into POINTS each, ordered from the tail to the snout. */
export const PIECES = [
${lines(pieces.flatMap((piece) => piece.t.map((n) => index.get(n))), 24)}
];

/** Where each piece lies along the body, in thousandths: 0 at the tail .. 1000 at the snout. */
export const BODY = [
${lines(pieces.map((piece) => Math.round(piece.body * 1000)), 24)}
];
`);
// ---------- eyelids.png: the green lid in the colours of every accent's logo ----------
// The green lid was cut from a closed-eye render of the green logo. Each other accent's logo is a recolouring of the green
// one, so an affine colour map fitted on the skin around the eye (the eye itself left out) carries the lid over.
const LID = 66; // px, the lid's square, as cut from the logo at (260, 65)
if (ACCENTS[0] !== 'green') throw new Error('eyelids.png keeps its source, the green lid, in the first cell: keep green first in ACCENTS.');
const lid = decode('eyelids.png', LID, LID, `crop=${LID}:${LID}:0:0`);

function solve(matrix, vector) {
  const n = vector.length;
  for (let i = 0; i < n; i++) {
    let pivot = i;
    for (let r = i + 1; r < n; r++) if (Math.abs(matrix[r][i]) > Math.abs(matrix[pivot][i])) pivot = r;
    [matrix[i], matrix[pivot]] = [matrix[pivot], matrix[i]];
    [vector[i], vector[pivot]] = [vector[pivot], vector[i]];
    for (let r = i + 1; r < n; r++) {
      const f = matrix[r][i] / matrix[i][i];
      for (let c = i; c < n; c++) matrix[r][c] -= f * matrix[i][c];
      vector[r] -= f * vector[i];
    }
  }
  const x = new Array(n).fill(0);
  for (let i = n - 1; i >= 0; i--) {
    let sum = vector[i];
    for (let c = i + 1; c < n; c++) sum -= matrix[i][c] * x[c];
    x[i] = sum / matrix[i][i];
  }
  return x;
}

// Least squares: every channel of `target` as a weighted sum of the green logo's channels plus a constant.
function colourMap(target) {
  const ata = Array.from({length: 4}, () => new Array(4).fill(0));
  const atb = [0, 1, 2].map(() => new Array(4).fill(0));
  for (let y = 40; y < 170; y++) {
    for (let x = 190; x < 420; x++) {
      const i = (y * W + x) * 4;
      if (pixels[i + 3] < 250 || ((x - 293) / 20) ** 2 + ((y - 98) / 17) ** 2 < 1.4) continue;
      const v = [pixels[i] / 255, pixels[i + 1] / 255, pixels[i + 2] / 255, 1];
      for (let a = 0; a < 4; a++) {
        for (let b = 0; b < 4; b++) ata[a][b] += v[a] * v[b];
        for (let c = 0; c < 3; c++) atb[c][a] += v[a] * target[i + c] / 255;
      }
    }
  }
  return atb.map((column) => solve(ata.map((row) => [...row]), [...column]));
}

const lids = Buffer.alloc(LID * ACCENTS.length * LID * 4);
ACCENTS.forEach((accent, k) => {
  // The identity for green itself, so the source never drifts from re-runs.
  const map = k === 0 ? [[1, 0, 0, 0], [0, 1, 0, 0], [0, 0, 1, 0]] : colourMap(decode(`logo-${accent}.webp`, W, H));
  for (let y = 0; y < LID; y++) {
    for (let x = 0; x < LID; x++) {
      const i = (y * LID + x) * 4;
      const o = (y * LID * ACCENTS.length + k * LID + x) * 4;
      const v = [lid[i] / 255, lid[i + 1] / 255, lid[i + 2] / 255, 1];
      for (let c = 0; c < 3; c++) lids[o + c] = Math.max(0, Math.min(255, Math.round(255 * map[c].reduce((sum, m, j) => sum + m * v[j], 0))));
      lids[o + 3] = lid[i + 3];
      if (!lid[i + 3]) lids.fill(0, o, o + 3);
    }
  }
});
execFileSync('ffmpeg', ['-loglevel', 'error', '-y', '-f', 'rawvideo', '-pix_fmt', 'rgba', '-s', `${LID * ACCENTS.length}x${LID}`, '-i', '-',
  `${dir}media/eyelids.png`], {input: lids});

console.log(`${pieces.length} pieces, ${used.length} points; logo-body.png ${FW}×${FH}; eyelids.png for ${ACCENTS.join(', ')}`);
