import {useEffect, useState} from 'react';

export const prefersReducedMotion = () =>
  typeof window !== 'undefined' && window.matchMedia('(prefers-reduced-motion: reduce)').matches;

/** True while `ref` is on screen; animations use it to sleep when scrolled away. */
export function useInView(ref, {rootMargin = '0px', once = false} = {}) {
  const [inView, setInView] = useState(false);
  useEffect(() => {
    const element = ref.current;
    if (!element || !('IntersectionObserver' in window)) {
      setInView(true);
      return undefined;
    }
    const observer = new IntersectionObserver(([entry]) => {
      setInView(entry.isIntersecting);
      if (once && entry.isIntersecting) observer.disconnect();
    }, {rootMargin});
    observer.observe(element);
    return () => observer.disconnect();
  }, [ref, rootMargin, once]);
  return inView;
}

/**
 * Steps through `count` frames every `interval` ms while `active`; returns the current frame.
 * Reduced motion shows the `still` frame (the last one by default) without stepping.
 */
export function useLoop(count, interval, active, still = count - 1) {
  const [frame, setFrame] = useState(0);
  useEffect(() => {
    if (prefersReducedMotion()) {
      setFrame(still);
      return undefined;
    }
    if (!active) return undefined;
    const timer = setInterval(() => setFrame((value) => (value + 1) % count), interval);
    return () => clearInterval(timer);
  }, [count, interval, active, still]);
  return frame;
}

/**
 * A walk-through beside its steps: plays the frames while visible, `plays` times, then rests on the last one;
 * `jump` shows a frame and plays once more from there. Reduced motion shows the last frame.
 */
export function useWalkthrough(ref, durations, plays) {
  const visible = useInView(ref);
  const [frame, setFrame] = useState(0);
  const [left, setLeft] = useState(plays);
  // Bumped by a jump, so a jump to the frame already shown still restarts its timer.
  const [jumps, setJumps] = useState(0);
  const [still, setStill] = useState(false);
  useEffect(() => setStill(prefersReducedMotion()), []);
  useEffect(() => {
    const last = frame === durations.length - 1;
    if (still || !visible || (last && left <= 1)) return undefined;
    const timer = setTimeout(() => {
      if (last) setLeft((value) => value - 1);
      setFrame(last ? 0 : frame + 1);
    }, durations[frame]);
    return () => clearTimeout(timer);
  }, [frame, left, jumps, visible, still, durations]);
  const jump = (value) => {
    setLeft(1);
    setFrame(value);
    setJumps((count) => count + 1);
  };
  return [still ? durations.length - 1 : frame, jump];
}
