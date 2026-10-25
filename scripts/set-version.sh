#!/bin/sh
# Set the package version everywhere it is written by hand: package.json and the README install URLs (the channel
# branch and the pinned upm/<version> tag). The unreleased notes of CHANGELOG.md and CHANGELOG.ru.md become the
# version's section, dated today, with its release link.
# The version also picks the channel .github/workflows/release.yml publishes to: a prerelease (1.1.0-rc.1) installs
# from `upm-preview`, a stable version (1.1.0) from `upm`.
# The unreleased notes are under "## [Unreleased]", or under the version's own heading marked unreleased
# ("## [1.1.0] — Unreleased", "## [1.1.0] — Не выпущено"). A prerelease of a line whose notes sit under the line's
# heading ("## [1.1.0] — Unreleased" for 1.1.0-rc.1) needs its own dated "## [1.1.0-rc.1] — YYYY-MM-DD" section first.
# Running it again with the version already set repairs files that drifted (a hand-edited package.json, say).
#   scripts/set-version.sh 1.1.0-rc.1
set -eu
cd "$(dirname "$0")/.."
NEW="${1:?usage: scripts/set-version.sh <version>}"
# \A..\z on the whole argument: a line-by-line grep would let "1.0.0<newline>x" through into package.json. SemVer
# forbids a leading zero in a number and an empty prerelease identifier (01.0.0, 1.0.0-rc.01, 1.0.0-rc..1).
perl -e '$n = qr/0|[1-9][0-9]*/; $id = qr/$n|[0-9]*[A-Za-z-][0-9A-Za-z-]*/;
  exit($ARGV[0] !~ /\A$n\.$n\.$n(?:-$id(?:\.$id)*)?\z/)' "$NEW" || { echo "$NEW is not a SemVer version" >&2; exit 1; }
# The unreleased heading of each CHANGELOG, matched whole: a trailing space or a CRLF fails here, before any change.
UNRELEASED='(?:Unreleased\]|\Q$v\E\] — (?:Unreleased|Не выпущено))$'
#   has_section <file> [<pattern after the "]">]
has_section() { perl -CSDA -Mutf8 -ne 'BEGIN { ($v, $t) = splice @ARGV, 0, 2 } $f = 1 if /^## \[\Q$v\E\]$t/; END { exit !$f }' "$NEW" "${2:-}" "$1"; }
# Checked for both files before any change. A CHANGELOG that already has the version's dated section keeps it.
for f in CHANGELOG.md CHANGELOG.ru.md; do
  if has_section "$f" ' — [0-9]{4}-[0-9]{2}-[0-9]{2}$'; then continue; fi
  V=$NEW perl -CSDA -Mutf8 -ne 'BEGIN { $v = $ENV{V} } if (/^## \[/) { last if $on; $on = /^## \['"$UNRELEASED"'/; next }
    $notes = 1 if $on && /\S/ && !/^#/; END { exit !$notes }' "$f" ||
    { echo "$f: no \"## [Unreleased]\" or \"## [$NEW] — Unreleased\" heading with notes to release as $NEW, and no dated \"## [$NEW] — YYYY-MM-DD\" section" >&2; exit 1; }
done
PKG=Aspid.MVVM/Packages/tech.aspid.mvvm
OLD=$(sed -n 's/^  "version": "\(.*\)",$/\1/p' "$PKG/package.json")
[ -n "$OLD" ] || { echo "package.json version not found" >&2; exit 1; }
case "$NEW" in
  *-*) BRANCH=upm-preview ;;
  *) BRANCH=upm ;;
esac
export NEW BRANCH
# Only the "version" line: a dependency can be at the same number.
OLD=$OLD perl -pi -e 's/^(  "version": ")\Q$ENV{OLD}\E(",)$/$1$ENV{NEW}$2/' "$PKG/package.json"
# The channel URL and the pinned one; other packages' URLs (Aspid.FastTools, Aspid.Collections) keep their channel.
perl -pi -e 's/Aspid\.MVVM\.git#upm\/[0-9A-Za-z.+-]+/Aspid.MVVM.git#upm\/$ENV{NEW}/g;
  s/Aspid\.MVVM\.git#upm(?:-preview)?(?![\/0-9A-Za-z-])/Aspid.MVVM.git#$ENV{BRANCH}/g' README.md
# The unreleased heading becomes the version's, dated, under a new empty [Unreleased]. The version's link points at
# its release, and the [Unreleased] link compares the version with HEAD. A link of the version to another URL (the
# "compare/...HEAD" of an unreleased line) is replaced, not defined twice.
DATE=$(date +%Y-%m-%d) REPO=https://github.com/VPDPersonal/Aspid.MVVM
export DATE REPO
for f in CHANGELOG.md CHANGELOG.ru.md; do
  if ! has_section "$f" ' — [0-9]{4}-[0-9]{2}-[0-9]{2}$'; then
    V=$NEW perl -CSDA -Mutf8 -0pi -e '$v = $ENV{V};
      s/^## \['"$UNRELEASED"'/## [Unreleased]\n\n## [$ENV{NEW}] — $ENV{DATE}/m or die "$ARGV: no unreleased heading\n"' "$f"
  fi
  perl -CSDA -Mutf8 -0pi -e 'my $link = "[$ENV{NEW}]: $ENV{REPO}/releases/tag/v$ENV{NEW}";
    s/^\[\Q$ENV{NEW}\E\]: .*$/$link/m or s/^(?=\[[0-9][^\]]*\]: )/$link\n/m or $_ .= "\n$link\n";
    my $unreleased = "[Unreleased]: $ENV{REPO}/compare/v$ENV{NEW}...HEAD";
    if (/^## \[Unreleased\]$/m) { s/^\[Unreleased\]: .*$/$unreleased/m or s/^(?=\[[0-9][^\]]*\]: )/$unreleased\n/m }' "$f"
done
# Whatever a pattern above missed fails here instead of at the release.
node scripts/check-version.mjs
echo "$OLD -> $NEW ($BRANCH)"
git status --short
