import assert from 'node:assert/strict';
import {test} from 'node:test';
import {rowWaves} from '../src/components/DotRipple/waves.js';

test('the row span keeps every dot inside a ring and skips the rows it misses', () => {
  const live = [
    {x: 400, y: 300, inner: 0, outer: 138},
    {x: 900, y: 500, inner: 210, outer: 560},
    {x: 1500, y: 100, inner: 820, outer: 1190},
  ];
  for (let cy = 10; cy < 1080; cy += 20) {
    const row = rowWaves(live, cy);
    for (const wave of live) {
      const span = row.find((entry) => entry.wave === wave);
      for (let cx = 10; cx < 1920; cx += 20) {
        const d = Math.hypot(cx - wave.x, cy - wave.y);
        if (d < wave.inner || d > wave.outer) continue;
        assert.ok(span, `row ${cy} misses a wave that touches it`);
        const adx = Math.abs(cx - wave.x);
        assert.ok(adx >= span.near && adx <= span.far, `dot (${cx}, ${cy}) is dropped from its ring`);
      }
    }
  }
  assert.equal(rowWaves(live, 1000).length, 2);
});
