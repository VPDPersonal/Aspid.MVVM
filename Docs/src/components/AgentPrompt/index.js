import React from 'react';
import styles from './styles.module.css';

/** An agent request on top of the table that compares its results; `src/remark/agentPrompt.js` builds it. */
export default function AgentPrompt({prompt, children}) {
  return (
    <div className={styles.frame}>
      <div className={styles.prompt}>
        <span className={styles.sign} aria-hidden="true">&gt;</span>
        <span className={styles.text}>{prompt}</span>
      </div>
      {children}
    </div>
  );
}
