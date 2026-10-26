/**
 * The waves whose ring crosses the grid row at `y`, each with the horizontal span of that crossing: a dot whose
 * `|x - wave.x|` lies outside `[near, far]` is outside the ring, so it is skipped without a square root. The span is
 * a pixel wider on both sides, so rounding never drops a dot the exact distance check would keep.
 */
export function rowWaves(live, y) {
  const row = [];
  for (const wave of live) {
    const dy = y - wave.y;
    const dy2 = dy * dy;
    const outer2 = wave.outer * wave.outer;
    if (dy2 > outer2) continue;
    const inner2 = wave.inner * wave.inner;
    row.push({
      wave,
      dy,
      near: dy2 < inner2 ? Math.max(Math.sqrt(inner2 - dy2) - 1, 0) : 0,
      far: Math.sqrt(outer2 - dy2) + 1,
    });
  }
  return row;
}
