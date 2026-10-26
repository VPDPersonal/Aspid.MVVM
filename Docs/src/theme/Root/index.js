import React from 'react';
import CopyShine from '../../components/CopyShine';
import DotAmbient from '../../components/DotAmbient';
import DotRipple from '../../components/DotRipple';
import DotSpotlight from '../../components/DotSpotlight';

// Match only the flat camera background, allowing one channel step for GIF quantization.
const channelMask = (channel, tolerance = 1) => Array.from({length: 256}, (_, value) =>
  Math.abs(value - channel) <= tolerance ? 1 : 0).join(' ');

function SceneBackgroundFilter({id, color, surface = 'var(--ifm-code-background)'}) {
  return (
    <filter id={id} x="0" y="0" width="100%" height="100%" colorInterpolationFilters="sRGB">
      <feComponentTransfer in="SourceGraphic" result="channels">
        <feFuncR type="discrete" tableValues={channelMask(color[0])} />
        <feFuncG type="discrete" tableValues={channelMask(color[1])} />
        <feFuncB type="discrete" tableValues={channelMask(color[2])} />
      </feComponentTransfer>
      <feColorMatrix in="channels" type="matrix" values="0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  1 1 1 0 -2" result="backgroundMask" />
      {/* Only near-background colors may join the fringe; preserve small agents and colored details. */}
      <feComponentTransfer in="SourceGraphic" result="fringeChannels">
        <feFuncR type="discrete" tableValues={channelMask(color[0], 24)} />
        <feFuncG type="discrete" tableValues={channelMask(color[1], 24)} />
        <feFuncB type="discrete" tableValues={channelMask(color[2], 24)} />
      </feComponentTransfer>
      <feColorMatrix in="fringeChannels" type="matrix" values="0 0 0 0 0  0 0 0 0 0  0 0 0 0 0  1 1 1 0 -2" result="fringeMask" />
      {/* Include the antialiased fringe, which still contains the old camera background. */}
      <feMorphology in="backgroundMask" operator="dilate" radius="1" result="expandedBackground" />
      <feGaussianBlur in="expandedBackground" stdDeviation="0.35" edgeMode="duplicate" result="softEdge" />
      <feComposite in="softEdge" in2="fringeMask" operator="in" result="softBackground" />
      <feFlood floodColor={surface} result="surface" />
      <feComposite in="surface" in2="softBackground" operator="in" result="background" />
      <feComposite in="SourceGraphic" in2="softBackground" operator="out" result="scene" />
      {/* Add complementary masks without making the softened edge translucent. */}
      <feComposite in="background" in2="scene" operator="arithmetic" k2="1" k3="1" />
    </filter>
  );
}

export default function Root({children}) {
  return (
    <>
      <svg aria-hidden="true" width="0" height="0" style={{position: 'absolute', pointerEvents: 'none'}}>
        <defs>
          <SceneBackgroundFilter id="sample-scene-background-light" color={[238, 240, 243]} />
          <SceneBackgroundFilter id="sample-scene-background-dark" color={[14, 16, 21]} />
          {/* Scene footage on a doc page sits on the article itself, without a frame. */}
          <SceneBackgroundFilter id="scene-footage-background-light" color={[238, 240, 243]} surface="var(--venom-reading-surface)" />
          <SceneBackgroundFilter id="scene-footage-background-dark" color={[14, 16, 21]} surface="var(--venom-reading-surface)" />
        </defs>
      </svg>
      <DotAmbient />
      <DotSpotlight />
      <DotRipple />
      <CopyShine />
      {children}
    </>
  );
}
