import fs from 'node:fs';
import path from 'node:path';
import { spawnSync } from 'node:child_process';
import { fileURLToPath } from 'node:url';
import { recordApiSnapshot } from './api-freshness.mjs';

const site = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const api = path.join(site, 'api');
const backup = path.join(site, '.api-backup');
if (fs.existsSync(backup)) throw new Error('An API backup already exists; recover it before regenerating.');
function run(command, args) {
  const result = spawnSync(command, args, { cwd: site, encoding: 'utf8', maxBuffer: 128 * 1024 * 1024 });
  if (result.stdout) process.stdout.write(result.stdout);
  if (result.stderr) process.stderr.write(result.stderr);
  if (/\[Failure\]|\berror CS\d+/.test((result.stdout ?? '') + (result.stderr ?? '')))
    throw new Error(`${command} reported compilation failures`);
  if (result.error) throw result.error;
  if (result.status !== 0) throw new Error(`${command} failed with status ${result.status}`);
}
run(process.execPath, ['scripts/docfx-projects.mjs']);
fs.renameSync(api, backup);
try {
  run('docfx', ['metadata', 'docfx/docfx.json']);
  run(process.execPath, ['scripts/docfx-postprocess.mjs']);
  recordApiSnapshot();
} catch (error) {
  fs.rmSync(api, { recursive: true, force: true });
  fs.renameSync(backup, api);
  throw error;
}
fs.rmSync(backup, { recursive: true });
