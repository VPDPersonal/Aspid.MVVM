export const normalize = (text) => text.toLocaleLowerCase().normalize('NFKC').replace(/ё/g, 'е');

/** True for Ctrl or Cmd with K. A non-Latin layout reports another letter for that key ("л"), so there the key's position decides. */
export function isSearchShortcut({ctrlKey, metaKey, key = '', code}) {
  if (!ctrlKey && !metaKey) return false;
  return key.toLowerCase() === 'k' || (code === 'KeyK' && !/^[a-z]$/i.test(key));
}

export function prepareIndex(entries) {
  return entries.map((entry) => ({...entry, normalizedTitle: normalize(entry.title), normalizedText: normalize(entry.text)}));
}

export function searchEntries(entries, query) {
  const normalized = normalize(query.trim());
  const terms = normalized.split(/\s+/).filter(Boolean);
  if (!terms.length) {
    const order = (entry) => /\/docs\/?$/.test(entry.url) ? 0 : entry.section === 'Docs' ? 1 : 2;
    return entries.filter((entry) => entry.section !== 'API').sort((a, b) => order(a) - order(b)).slice(0, 8);
  }
  return entries.map((entry) => {
    if (!terms.every((term) => entry.normalizedTitle.includes(term) || entry.normalizedText.includes(term))) return null;
    const score = (entry.normalizedTitle === normalized ? 100 : 0)
      + (entry.normalizedTitle.includes(normalized) ? 40 : 0)
      + terms.filter((term) => entry.normalizedTitle.includes(term)).length * 10
      + (entry.section === 'Docs' ? 3 : 0);
    const position = entry.normalizedText.indexOf(terms[0]);
    const start = Math.max(0, position - 55);
    const snippet = `${start ? '…' : ''}${entry.text.slice(start, start + 190)}${entry.text.length > start + 190 ? '…' : ''}`;
    return {...entry, score, snippet};
  }).filter(Boolean).sort((a, b) => b.score - a.score || a.title.localeCompare(b.title)).slice(0, 12);
}
