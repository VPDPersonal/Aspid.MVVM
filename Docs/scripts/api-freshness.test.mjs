import { test } from 'node:test';
import assert from 'node:assert/strict';
import fs from 'node:fs';
import os from 'node:os';
import path from 'node:path';
import { digestFiles } from './api-freshness.mjs';

test('API digest detects edits, additions and removals without absolute paths', () => {
  const directory = fs.mkdtempSync(path.join(os.tmpdir(), 'asp-api-'));
  try {
    const take = () => digestFiles(directory, name => name.endsWith('.cs'));
    fs.writeFileSync(path.join(directory, 'A.cs'), 'original');
    const first = take();
    assert.deepEqual(Object.keys(first), ['A.cs']);
    fs.writeFileSync(path.join(directory, 'A.cs'), 'changed');
    assert.notDeepEqual(take(), first);
    const changed = take();
    fs.writeFileSync(path.join(directory, 'B.cs'), 'new');
    assert.notDeepEqual(take(), changed);
    fs.unlinkSync(path.join(directory, 'B.cs'));
    assert.deepEqual(take(), changed);
    fs.writeFileSync(path.join(directory, 'ignored.meta'), 'ignored');
    assert.deepEqual(take(), changed);
  } finally { fs.rmSync(directory, { recursive: true }); }
});
