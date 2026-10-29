import {useEffect} from 'react';

// A press on the copy button of a code block makes the block jump a little towards the reader while a soft light of the
// accent runs across it, as over a glossy chip, and its frame catches that light.
// The copy button has no class of its own: only its icons tell it from the word wrap button. They are checked apart
// from the selector, because `closest()` throws on `:has()` in a browser without it.
const BUTTON = '.theme-code-block button';
const COPY_ICONS = '[class*="copyButtonIcons_"]';

const MAX_DPR = 2;         // device px per CSS px of the canvas at most: the light is soft and needs no more
const DURATION = 450;      // ms the effect takes on a short block
const SLOWDOWN = 1.5;      // a long block takes this many times longer, so the light does not race across it
const LONG_FROM = 8;       // lines from which a block starts to count as long
const LONG_TO = 40;        // lines from which it counts as fully long
const POP = 3;             // px, how far the edges of the block move out at the top of the jump
const MAX_POP = 0.02;      // the same as a relative scale: the limit for a small block
const RISE = 0.35;         // part of the effect the jump takes to reach its top
const LEAN = 0.4;          // the light leans like "/": a row one px lower meets it this many px further left
const UNIT = 0.05;         // the band widths grow with the block: a unit is at least this share of its diagonal span
const CORE = 1.3;          // half-width of the bright core of the band, in units
const BLOOM = 4.5;         // half-width of the soft glow round the core, in units
const CORE_LIGHT = 0.4;    // how much the core lights a dark block
const BLOOM_LIGHT = 0.2;   // how much the glow lights a dark block
const RIM_LIGHT = 0.9;     // how much the frame lights up as the glow passes it
const RIM_GLOW = 0.4;      // depth in units of the soft light just inside the frame
const TOP_LIGHT = 0.45;    // the light comes from above: the bottom edge gets this share of what the top edge gets
const LARGE_DIM = 0.4;     // a long block lights this much of a short one, so the light does not flood the page
// The core is the accent brightened like the light on the intro logo: saturated, so it reads as light, not a white haze.
const BRIGHT = 1.25;       // the accent channels are multiplied by this
const LIFT = 30 / 255;     // and raised by this
const WASH = 0.18;         // how deep the glow tints a light block with the accent
const CLEAN_CORE = 0.7;    // how much the core of the band stays clean on a light block, so it reads as a highlight

const smoothstep = (from, to, value) => {
  const t = Math.min(Math.max((value - from) / (to - from), 0), 1);
  return t * t * (3 - 2 * t);
};
const easeInOut = (t) => (1 - Math.cos(t * Math.PI)) / 2;

const VERTEX = `
attribute vec2 corner;
void main() { gl_Position = vec4(corner, 0.0, 1.0); }`;

const FRAGMENT = `
precision highp float;

uniform vec2 size;      // device px
uniform float radius;   // device px, outer corner radius
uniform float border;   // device px, width of the frame
uniform float unit;     // device px
uniform float sweep;    // 0 when the band is before the block, 1 when it is past it
uniform float calm;     // 1 on a short block, LARGE_DIM on a long one
uniform float light;    // 1 on the light theme
uniform vec3 tint;      // the accent, RGB in 0-1

void main() {
  vec2 pixel = vec2(gl_FragCoord.x, size.y - gl_FragCoord.y);

  // Signed distance to the rounded edge of the block, negative inside.
  vec2 middle = size * 0.5;
  vec2 corner = abs(pixel - middle) - (middle - radius);
  float edge = length(max(corner, 0.0)) + min(max(corner.x, corner.y), 0.0) - radius;
  float coverage = clamp(0.5 - edge, 0.0, 1.0);

  // The band runs from fully before the block to fully past it, so it never pops in or out.
  float across = pixel.x + pixel.y * ${LEAN.toFixed(4)};
  float span = size.x + size.y * ${LEAN.toFixed(4)};
  float reach = unit * ${(BLOOM * 1.5).toFixed(4)};
  float offset = across - mix(-reach, span + reach, sweep);
  float fade = sin(sweep * 3.14159265);
  float core = exp(-pow(offset / (unit * ${CORE.toFixed(4)}), 2.0)) * fade;
  float bloom = exp(-pow(offset / (unit * ${BLOOM.toFixed(4)}), 2.0)) * fade;

  // The frame takes the light in full, the inside just next to it a little.
  float depth = max(-edge, 0.0);
  float frame = max(1.0 - smoothstep(border, border + 1.5, depth),
    0.5 * exp(-max(depth - border, 0.0) / (unit * ${RIM_GLOW.toFixed(4)})));
  frame *= mix(1.0, ${TOP_LIGHT.toFixed(4)}, pixel.y / size.y);
  float rim = frame * bloom * ${RIM_LIGHT.toFixed(4)};

  if (light > 0.5) {
    // A light block has no room to get brighter, so the glow tints it with the accent instead. The canvas multiplies
    // into the block, so dark text stays dark.
    float wash = clamp(bloom * (1.0 - core * ${CLEAN_CORE.toFixed(4)}) * ${WASH.toFixed(4)} * calm + rim, 0.0, 1.0);
    gl_FragColor = vec4(mix(vec3(1.0), tint, wash), 1.0) * coverage;
  } else {
    // The glow and the frame shine in the accent, the core in the brighter accent.
    vec3 bright = min(tint * ${BRIGHT.toFixed(4)} + ${LIFT.toFixed(4)}, 1.0);
    float glow = bloom * ${BLOOM_LIGHT.toFixed(4)} * calm + rim;
    float shine = core * ${CORE_LIGHT.toFixed(4)} * calm;
    float alpha = clamp(glow + shine, 0.0, 1.0);
    gl_FragColor = vec4(min(tint * glow + bright * shine, vec3(alpha)), alpha) * coverage;
  }
}`;

// One canvas and one WebGL context serve every block, created at the first press. Null when WebGL is not available:
// then the block only jumps.
let shine;

function createShine() {
  const canvas = document.createElement('canvas');
  canvas.className = 'copy-shine';
  canvas.setAttribute('aria-hidden', 'true');
  const gl = canvas.getContext('webgl', {antialias: false});
  if (!gl) return null;

  const program = gl.createProgram();
  for (const [type, source] of [[gl.VERTEX_SHADER, VERTEX], [gl.FRAGMENT_SHADER, FRAGMENT]]) {
    const shader = gl.createShader(type);
    gl.shaderSource(shader, source);
    gl.compileShader(shader);
    gl.attachShader(program, shader);
  }
  gl.linkProgram(program);
  if (!gl.getProgramParameter(program, gl.LINK_STATUS)) return null;
  gl.useProgram(program);

  // One triangle that covers the whole canvas.
  gl.bindBuffer(gl.ARRAY_BUFFER, gl.createBuffer());
  gl.bufferData(gl.ARRAY_BUFFER, new Float32Array([-1, -1, 3, -1, -1, 3]), gl.STATIC_DRAW);
  const corner = gl.getAttribLocation(program, 'corner');
  gl.enableVertexAttribArray(corner);
  gl.vertexAttribPointer(corner, 2, gl.FLOAT, false, 0, 0);

  const uniforms = {};
  for (const name of ['size', 'radius', 'border', 'unit', 'sweep', 'calm', 'light', 'tint']) {
    uniforms[name] = gl.getUniformLocation(program, name);
  }
  // The largest canvas side the GPU draws in full.
  const limit = Math.min(...gl.getParameter(gl.MAX_VIEWPORT_DIMS), gl.getParameter(gl.MAX_RENDERBUFFER_SIZE));
  return {canvas, gl, uniforms, limit};
}

// The colour of the site's light effects (the ripple, the spotlight) as resolved inside `block`, as RGB in 0–1.
function accentOf(block) {
  const probe = document.createElement('span');
  probe.style.color = 'var(--venom-ripple, var(--venom-accent))';
  block.append(probe);
  const rgb = getComputedStyle(probe).color.match(/[\d.]+/g)?.slice(0, 3).map((channel) => channel / 255);
  probe.remove();
  return rgb ?? [1, 1, 1];
}

// Stops the effect that runs now, if any.
let stop = null;

function play(block) {
  stop?.();
  const style = getComputedStyle(block);
  const width = block.offsetWidth;
  const height = block.offsetHeight;
  const line = parseFloat(getComputedStyle(block.querySelector('pre') ?? block).lineHeight) || 22;
  const long = smoothstep(LONG_FROM, LONG_TO, height / line);
  const duration = DURATION * (1 + (SLOWDOWN - 1) * long);
  const scale = 1 + Math.min(MAX_POP, POP / (Math.max(width, height) / 2));

  const jump = block.animate([
    {transform: 'scale(1)', easing: 'cubic-bezier(0.2, 0.7, 0.3, 1)'},
    {transform: `scale(${scale})`, offset: RISE, easing: 'cubic-bezier(0.45, 0, 0.25, 1)'},
    {transform: 'scale(1)'},
  ], {duration});

  if (shine === undefined || shine?.gl.isContextLost()) shine = createShine();
  if (!shine) {
    stop = () => {
      jump.cancel();
      stop = null;
    };
    jump.onfinish = stop;
    return;
  }

  const {canvas, gl, uniforms, limit} = shine;
  // A long block gets fewer device px per CSS px, so its canvas stays inside the GPU limit.
  const dpr = Math.min(devicePixelRatio || 1, MAX_DPR, limit / width, limit / height);
  const frameWidth = parseFloat(style.borderTopWidth) || 0;
  const light = document.documentElement.dataset.theme !== 'dark';
  canvas.width = Math.round(width * dpr);
  canvas.height = Math.round(height * dpr);
  Object.assign(canvas.style, {
    left: `${-(parseFloat(style.borderLeftWidth) || 0)}px`,
    top: `${-frameWidth}px`,
    width: `${width}px`,
    height: `${height}px`,
    mixBlendMode: light ? 'multiply' : 'normal',
  });
  // The browser may still give a smaller drawing buffer than asked: the light is drawn at the size the buffer has.
  const bufferWidth = gl.drawingBufferWidth;
  const bufferHeight = gl.drawingBufferHeight;
  const px = Math.min(bufferWidth / width, bufferHeight / height);
  gl.viewport(0, 0, bufferWidth, bufferHeight);
  gl.uniform2f(uniforms.size, bufferWidth, bufferHeight);
  gl.uniform1f(uniforms.radius, Math.min(parseFloat(style.borderTopLeftRadius) || 0, width / 2, height / 2) * px);
  gl.uniform1f(uniforms.border, frameWidth * px);
  gl.uniform1f(uniforms.unit, Math.max(line, (width + height * LEAN) * UNIT) * px);
  gl.uniform1f(uniforms.calm, 1 + (LARGE_DIM - 1) * long);
  gl.uniform1f(uniforms.light, light ? 1 : 0);
  gl.uniform3fv(uniforms.tint, accentOf(block));
  block.append(canvas);

  let start;
  let frame = requestAnimationFrame(function draw(now) {
    start ??= now;
    const t = (now - start) / duration;
    if (t >= 1) {
      stop();
      return;
    }
    gl.uniform1f(uniforms.sweep, easeInOut(t));
    gl.drawArrays(gl.TRIANGLES, 0, 3);
    frame = requestAnimationFrame(draw);
  });
  stop = () => {
    cancelAnimationFrame(frame);
    jump.cancel();
    canvas.remove();
    stop = null;
  };
}

export default function CopyShine() {
  useEffect(() => {
    const onClick = (event) => {
      const button = event.target.closest?.(BUTTON);
      if (!button?.querySelector(COPY_ICONS) || matchMedia('(prefers-reduced-motion: reduce)').matches) return;
      play(button.closest('.theme-code-block'));
    };
    document.addEventListener('click', onClick);
    return () => {
      document.removeEventListener('click', onClick);
      stop?.();
    };
  }, []);
  return null;
}
