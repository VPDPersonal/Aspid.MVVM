using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="RichTextSanitizeConverter"/> — the two <see cref="RichTextSanitize"/>
    /// modes, the allowed-tag whitelist, the stray-bracket switch, and the shapes of malformed
    /// markup a player can type into a name field.
    /// </summary>
    /// <remarks>
    /// The mistake worth guarding against is a rewrite of the hand-written scanner that lets one class
    /// of input back through: a tag recognised by name but not by its attribute, a closing tag treated
    /// differently from its opening one, a whitelist entry matching by prefix, or a second <c>&lt;</c>
    /// hiding a real tag. Every expected value was taken from running the converter; two rows assert
    /// behavior the source's own documentation contradicts and are named to say so.
    /// </remarks>
    [TestFixture]
    internal sealed class RichTextSanitizeConverterTests
    {
        // --- pass-through --------------------------------------------------------------------

        [Test]
        public void Convert_Null_ReturnsNull() =>
            Assert.IsNull(Strip().Convert(null));

        [Test]
        public void Convert_Empty_ReturnsEmpty() =>
            Assert.AreEqual(string.Empty, Strip().Convert(string.Empty));

        // Spaces are blank too, so they take the guard rather than the scan.
        [Test]
        public void Convert_WhitespaceOnly_ReturnsTheSameInstance()
        {
            const string value = "   ";

            Assert.AreSame(value, Strip().Convert(value));
        }

        // The scanner promises to hand back the instance it was given when there is no '<' to act
        // on, which is what keeps a per-frame binding from allocating a copy of every label.
        // AreEqual would pass on a StringBuilder round-trip; AreSame is the assertion that fails.
        [Test]
        public void Convert_WithoutAnyOpeningBracket_ReturnsTheSameInstance()
        {
            var value = "plain text > here";

            Assert.AreSame(value, Strip().Convert(value));
        }

        // A parameterless converter is what a freshly added component holds before anyone touches
        // the inspector, so its defaults are a shipped behavior rather than an implementation
        // detail: strip everything, allow nothing.
        [Test]
        public void Convert_DefaultConstructed_StripsEveryTag() =>
            Assert.AreEqual("Bob", new RichTextSanitizeConverter().Convert("<size=400%>Bob"));

        // The enum's own remarks warn that the order is the serialized value. A reordering compiles
        // and silently reinterprets every converter already authored in a scene, turning a Strip
        // into an Escape.
        [Test]
        public void RichTextSanitize_OrdinalsAreFrozen()
        {
            Assert.AreEqual(0, (int)RichTextSanitize.Strip);
            Assert.AreEqual(1, (int)RichTextSanitize.Escape);
        }

        // --- Strip ---------------------------------------------------------------------------

        [TestCase("<b>hi</b>", "hi")]
        [TestCase("<size=400%>Bob", "Bob")]
        [TestCase("<color=#00000000>ghost</color>", "ghost")]
        [TestCase("<color=\"#ffffff\">w</color>", "w")]
        [TestCase("<#ff0000>red", "red")]
        [TestCase("<font-weight=700>x</font-weight>", "x")]
        [TestCase("<B>hi</B>", "hi")]
        [TestCase("<b><i><u>x</u></i></b>", "x")]
        [TestCase("<b></b><b></b>", "")]
        // A tag carrying a quoted attribute is one span from '<' to the first '>', not two.
        [TestCase("<sprite name=\"x\">", "")]
        public void Convert_Strip_RemovesTheTagAndKeepsTheTextAround(string value, string expected) =>
            Assert.AreEqual(expected, Strip().Convert(value));

        // --- Escape --------------------------------------------------------------------------

        [TestCase("<b>hi</b>", "<noparse><b></noparse>hi<noparse></b></noparse>")]
        [TestCase("<size=400%>Bob", "<noparse><size=400%></noparse>Bob")]
        [TestCase("<#ff0000>red", "<noparse><#ff0000></noparse>red")]
        public void Convert_Escape_WrapsEachTagInItsOwnNoparse(string value, string expected) =>
            Assert.AreEqual(expected, Escape().Convert(value));

        // <noparse> is the one tag Escape cannot show, because its closing half would end the
        // wrapper early and un-escape everything after it. It is dropped instead — so Escape is not
        // the "keep the tag, show it as characters" rule its summary states, and a rewrite that
        // took that summary literally would open a full escape bypass.
        [TestCase("<noparse>x</noparse>", "x")]
        [TestCase("</noparse>tail", "tail")]
        [TestCase("<NOPARSE>x</NOPARSE>", "x")]
        public void Convert_Escape_DropsNoparseItselfRatherThanShowingIt(string value, string expected) =>
            Assert.AreEqual(expected, Escape().Convert(value));

        // The noparse check compares the whole name, so a longer tag that merely starts with it is
        // still escaped. Matching by prefix here would silently drop <noparsex> instead.
        [Test]
        public void Convert_Escape_TagStartingWithNoparse_IsStillEscaped() =>
            Assert.AreEqual("<noparse><noparsex></noparse>x", Escape().Convert("<noparsex>x"));

        // A binding chain can run a converter twice, and a string that grew a noparse wrapper on
        // every pass would double in length each time.
        [Test]
        public void Convert_Escape_RunTwice_IsIdempotent()
        {
            var once = Escape().Convert("<b>hi</b>");

            Assert.AreEqual(once, Escape().Convert(once));
        }

        // --- whitelist -----------------------------------------------------------------------

        [TestCase("b", "<b><i>x</i></b>", "<b>x</b>")]
        // The closing half is admitted under the same entry, or a whitelist would leave every
        // opening tag unbalanced.
        [TestCase("b", "unmatched</b>", "unmatched</b>")]
        [TestCase("b", "<B>x</B>", "<B>x</B>")]
        [TestCase("CoLoR", "<color=red>x", "<color=red>x")]
        // The entry names the tag, not the whole tag: everything after '=' rides along unread.
        [TestCase("size", "<size=400%>Bob", "<size=400%>Bob")]
        [TestCase("font-weight", "<font-weight=700>x", "<font-weight=700>x")]
        public void Convert_AllowedTag_GoesThroughUntouched(string allowed, string value, string expected) =>
            Assert.AreEqual(expected, Strip(allowed).Convert(value));

        // Allowing "b" must not admit <bold>: the name is compared by length as well as content.
        [Test]
        public void Convert_AllowedTag_DoesNotMatchALongerTagByPrefix() =>
            Assert.AreEqual("x", Strip("b").Convert("<bold>x</bold>"));

        // <#RRGGBB> is <color=#RRGGBB> under a shorter spelling, so the entry that admits one has
        // to admit the other — otherwise whitelisting color still breaks half the authored text.
        [TestCase("<#ff0000>red", "<#ff0000>red")]
        [TestCase("</#ff0000>x", "</#ff0000>x")]
        public void Convert_AllowingColor_AlsoAllowsTheHashSpelling(string value, string expected) =>
            Assert.AreEqual(expected, Strip("color").Convert(value));

        // The reverse is not true and there is no way to spell it: the hash form is only ever
        // matched against the literal name "color", so a designer who types "#" or "#ff0000" into
        // the array gets a whitelist entry that can never fire.
        [TestCase("#")]
        [TestCase("#ff0000")]
        [TestCase("b")]
        public void Convert_HashSpelling_IsNotAllowedByAnyEntryButColor(string allowed) =>
            Assert.AreEqual("red", Strip(allowed).Convert("<#ff0000>red"));

        // Whitelisting a tag whitelists every value it can carry. Allowing "color" therefore keeps
        // letting a fully transparent name through — the whitelist is a decision about which
        // attacks stay possible, not a safe list.
        [Test]
        public void Convert_AllowingColor_StillLetsAnInvisibleNameThrough() =>
            Assert.AreEqual(
                "<color=#00000000>ghost</color>",
                Strip("color").Convert("<color=#00000000>ghost</color>"));

        // A designer who adds an array element and never fills it in leaves a null or an empty
        // string in the list. Neither may match anything — an empty entry matching the nameless
        // <> would be the whole whitelist collapsing open.
        [Test]
        public void Convert_NullEntryInTheAllowedList_IsSkippedWithoutDisturbingTheRest() =>
            Assert.AreEqual("<b>x</b>", new RichTextSanitizeConverter(
                RichTextSanitize.Strip, new[] { null, "b" }).Convert("<b>x</b>"));

        [Test]
        public void Convert_EmptyEntryInTheAllowedList_MatchesNoTag() =>
            Assert.AreEqual("x", Strip("").Convert("<b>x</b>"));

        [Test]
        public void Convert_EmptyEntryInTheAllowedList_DoesNotMatchTheNamelessTag() =>
            Assert.AreEqual("ab", new RichTextSanitizeConverter(
                RichTextSanitize.Strip, new[] { "" }, keepStrayBrackets: false).Convert("a<>b"));

        // The constructor's optional argument is a null array, not an empty one; it must fall back
        // to the field initializer rather than storing null and throwing on the first tag.
        [Test]
        public void Convert_NullAllowedArray_KeepsTheEmptyWhitelist() =>
            Assert.AreEqual("x", new RichTextSanitizeConverter(
                RichTextSanitize.Strip, allowedTags: null).Convert("<b>x</b>"));

        // "</>" has a slash and nothing after it. The allowed-list check reads the character after
        // the slash to spot the hash spelling, and that read is one past the tag here.
        [Test]
        public void Convert_ClosingSlashWithNoName_DoesNotReadPastTheTag() =>
            Assert.AreEqual(string.Empty, new RichTextSanitizeConverter(
                RichTextSanitize.Strip, new[] { "color" }, keepStrayBrackets: false).Convert("</>"));

        [Test]
        public void Convert_Escape_AllowedTagIsNotWrappedWhileItsNeighboursAre() =>
            Assert.AreEqual(
                "<b><noparse><i></noparse>x<noparse></i></noparse></b>",
                Escape("b").Convert("<b><i>x</i></b>"));

        // Whitelisting noparse in Escape mode hands the attacker the wrapper the mode depends on:
        // the tag is admitted by the allowed-list check before the noparse special case is reached.
        [Test]
        public void Convert_Escape_AllowingNoparse_LetsTheWrapperTagThrough() =>
            Assert.AreEqual("<noparse>x</noparse>", Escape("noparse").Convert("<noparse>x</noparse>"));

        // --- stray brackets ------------------------------------------------------------------

        // Chat is full of brackets that are not markup. Each of these must come back byte for byte,
        // or "a < b" turns into "a  c" for every player who types an inequality.
        [TestCase("a < b > c")]
        [TestCase("5<10>3")]
        [TestCase("<3>")]
        [TestCase("</>")]
        [TestCase("a<>b")]
        [TestCase("a<=b>c")]
        [TestCase("< b>bold")]
        // A name is only a tag name when '=' or a space ends it, so a slash or a tab makes the whole
        // run ordinary text.
        [TestCase("<b/>x")]
        [TestCase("<b\t>x")]
        [TestCase("<!b>x")]
        [TestCase("<1b>x")]
        public void Convert_StrayBracketsKept_LeavesNonTagRunsAlone(string value) =>
            Assert.AreEqual(value, Strip().Convert(value));

        [TestCase("a < b > c", "a  c")]
        [TestCase("5<10>3", "53")]
        [TestCase("<3>", "")]
        [TestCase("</>", "")]
        [TestCase("a<>b", "ab")]
        [TestCase("< b>bold", "bold")]
        [TestCase("<b\t>x", "x")]
        public void Convert_StrayBracketsOff_TreatsEveryBracketPairAsMarkup(string value, string expected) =>
            Assert.AreEqual(expected, StripNoStray().Convert(value));

        [Test]
        public void Convert_StrayBracketsOff_Escape_WrapsTheStrayRunInNoparse() =>
            Assert.AreEqual("a <noparse>< b ></noparse> c", EscapeNoStray().Convert("a < b > c"));

        // --- unbalanced and malformed --------------------------------------------------------

        // A '<' with no '>' after it ends the scan and the remainder is copied verbatim. The
        // keepStrayBrackets tooltip claims turning the switch off makes every "<…>" markup; it does
        // not reach this case, and the row is here to pin the behavior the code actually has.
        [TestCase("<size=400%", true)]
        [TestCase("<size=400%", false)]
        [TestCase("a < b", false)]
        [TestCase("<", false)]
        public void Convert_UnterminatedBracket_SurvivesEvenWhenStrayBracketsAreOff(
            string value,
            bool keepStrayBrackets) =>
            Assert.AreEqual(value, new RichTextSanitizeConverter(
                RichTextSanitize.Strip, null, keepStrayBrackets).Convert(value));

        [Test]
        public void Convert_Escape_UnterminatedBracket_IsNotWrapped() =>
            Assert.AreEqual("<size=400%", EscapeNoStray().Convert("<size=400%"));

        // The unterminated tail is reached only after the earlier, well-formed tags were handled.
        [Test]
        public void Convert_TagThenUnterminatedBracket_StillStripsTheTag() =>
            Assert.AreEqual("hi<color=red", Strip().Convert("<b>hi<color=red"));

        [Test]
        public void Convert_BareClosingBracket_IsOrdinaryText() =>
            Assert.AreEqual(">", StripNoStray().Convert(">"));

        // A '<' inside a tag does not restart the span: the span still ends at the first '>', which
        // leaves the outer '>' behind as a loose character.
        [Test]
        public void Convert_Strip_NestedOpeningBracket_LeavesTheOuterCloseBehind() =>
            Assert.AreEqual("<size=>", Strip().Convert("<size=<b>>"));

        [Test]
        public void Convert_Escape_NestedOpeningBracket_LeavesTheOuterCloseOutsideTheWrapper() =>
            Assert.AreEqual("<size=<noparse><b></noparse>>", Escape().Convert("<size=<b>>"));

        // THE BYPASS THAT WAS. A run from '<' to the first '>' used to be judged as a whole, so
        // "< <size=400%>" — which begins with a space and therefore does not scan as a tag — was
        // copied out untouched with the live tag inside it, and a text component re-scanned from the
        // inner '<' and obeyed it. The exact attack this converter exists to stop, hidden behind
        // one extra bracket, and reachable on the DEFAULT settings. The scanner now stops at a
        // nested '<', emits the stray bracket alone and judges the inner tag on its own.
        [TestCase("< <size=400%>Bob", "< Bob")]
        [TestCase("<<b>bold", "<bold")]
        [TestCase("<b>hi< <i>there", "hi< there")]
        // Only the tag swallowed by the stray run escapes; the next one is still stripped.
        [TestCase("< <b><i>x", "< x")]
        public void Convert_StrayBracketBeforeATag_StillSanitizesTheTagBehindIt(
            string value,
            string expected) =>
            Assert.AreEqual(expected, Strip().Convert(value));

        [Test]
        public void Convert_Escape_StrayBracketBeforeATag_StillWrapsTheTagBehindIt() =>
            Assert.AreEqual("< <noparse><size=400%></noparse>Bob", Escape().Convert("< <size=400%>Bob"));

        // Turning stray brackets off additionally drops the lone '<' itself; the tag behind it is
        // sanitized either way now.
        [TestCase("< <size=400%>Bob", "< Bob")]
        [TestCase("<<b>bold", "<bold")]
        [TestCase("< <b><i>x", "< x")]
        public void Convert_StrayBracketsOff_SanitizesATagHiddenBehindABracket(
            string value,
            string expected) =>
            Assert.AreEqual(expected, StripNoStray().Convert(value));

        // --- undeclared mode -----------------------------------------------------------------

        // The serialized enum can hold a value no member declares — a scene authored against a
        // build that had a third mode, or a hand-edited asset. Every tag it is asked to deal with is
        // reported and stripped: handing live markup back is the one answer a sanitizer must not give.
        [Test]
        public void Convert_UndeclaredMode_WithATagToSanitize_ReportsAndStrips()
        {
            LogAssert.Expect(LogType.Error, new Regex("RichTextSanitizeConverter.*not a declared"));
            LogAssert.Expect(LogType.Error, new Regex("RichTextSanitizeConverter.*not a declared"));

            Assert.AreEqual("x", new RichTextSanitizeConverter((RichTextSanitize)42).Convert("<b>x</b>"));
        }

        // The report sits on the sanitize branch only, so a broken mode stays quiet until a string
        // that actually needs sanitizing arrives.
        [TestCase("plain")]
        [TestCase("a < b > c")]
        public void Convert_UndeclaredMode_WithNothingToSanitize_ReportsNothing(string value) =>
            Assert.AreEqual(value, new RichTextSanitizeConverter((RichTextSanitize)42).Convert(value));

        [Test]
        public void Convert_UndeclaredMode_AllowedTag_ReportsNothing() =>
            Assert.AreEqual("<b>x</b>", new RichTextSanitizeConverter(
                (RichTextSanitize)42, new[] { "b" }).Convert("<b>x</b>"));

        // --- instance reuse ------------------------------------------------------------------

        // The StringBuilder is a field kept alive between calls. Dropping the Clear would make the
        // second push of a bound value return the first one glued in front of it.
        [Test]
        public void Convert_CalledRepeatedly_DoesNotCarryTheBuilderOver()
        {
            var converter = Strip();

            Assert.AreEqual("one", converter.Convert("<b>one</b>"));
            Assert.AreEqual("two", converter.Convert("<b>two</b>"));
            Assert.AreEqual("three", converter.Convert("<i>three</i>"));
        }

        // --- factories -----------------------------------------------------------------------

        private static RichTextSanitizeConverter Strip(params string[] allowedTags) =>
            new RichTextSanitizeConverter(RichTextSanitize.Strip, allowedTags);

        private static RichTextSanitizeConverter Escape(params string[] allowedTags) =>
            new RichTextSanitizeConverter(RichTextSanitize.Escape, allowedTags);

        private static RichTextSanitizeConverter StripNoStray() =>
            new RichTextSanitizeConverter(RichTextSanitize.Strip, keepStrayBrackets: false);

        private static RichTextSanitizeConverter EscapeNoStray() =>
            new RichTextSanitizeConverter(RichTextSanitize.Escape, keepStrayBrackets: false);
    }
}
