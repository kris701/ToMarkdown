using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ToMarkdown.Strings.ToMarkdownStringExtensions;

namespace ToMarkdown.Tests.Strings
{
    [TestClass]
    public class ToMarkdownStringExtensionsTests
    {
        [TestMethod]
        [DataRow("a", StringStyle.None, "a")]
        [DataRow("a", StringStyle.Heading1, "# a")]
        [DataRow("a", StringStyle.Heading2, "## a")]
        [DataRow("a", StringStyle.Heading3, "### a")]
        [DataRow("a", StringStyle.Bold, "**a**")]
        [DataRow("a", StringStyle.Italic, "*a*")]
        [DataRow("a", StringStyle.StrikeThrough, "~~a~~")]
        [DataRow("a", StringStyle.Code, "`a`")]
        public void Can_OutputCorrectStyle(string input, StringStyle style, string expected)
        {
            Assert.AreEqual(expected, input.ToMarkdown(style));
        }
    }
}
