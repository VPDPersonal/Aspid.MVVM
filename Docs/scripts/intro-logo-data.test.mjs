import fs from 'node:fs';
import assert from 'node:assert/strict';
import {test} from 'node:test';
import {ACCENTS} from '../src/accents.js';
import {POINTS, PIECES, BODY} from '../src/components/IntroBanner/mesh.js';

// The data of the banner logo's motion comes from scripts/intro-logo-data.mjs; these tests catch a missed re-run.
const media = new URL('../src/components/IntroBanner/media/', import.meta.url);
const LID = 66; // px, the side of one lid in eyelids.png, as in motion.js

function pngSize(name) {
  const png = fs.readFileSync(new URL(name, media));
  return [png.readUInt32BE(16), png.readUInt32BE(20)];
}

test('eyelids.png holds a lid for every accent', () => {
  assert.deepEqual(pngSize('eyelids.png'), [LID * ACCENTS.length, LID]);
});

test('logo-body.png covers the logo at half its size', () => {
  assert.deepEqual(pngSize('logo-body.png'), [320, 317]);
});

test('every piece of the mesh has three corners and runs from the tail to the snout', () => {
  assert.equal(PIECES.length, BODY.length * 3);
  assert.ok(PIECES.every((n) => Number.isInteger(n) && n >= 0 && n < POINTS.length / 2));
  assert.ok(BODY.every((v, i) => v >= 0 && v <= 1000 && (i === 0 || v >= BODY[i - 1])));
});
