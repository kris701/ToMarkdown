﻿using ToMarkdown;
using static ToMarkdown.ToMarkdownExtensions;

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
        [DataRow("a", StringStyle.Highlight, "==a==")]
        [DataRow("a", StringStyle.Subscript, "~a~")]
        [DataRow("a", StringStyle.Superscript, "^a^")]
        public void Can_OutputCorrectStyle(string input, StringStyle style, string expected)
        {
            Assert.AreEqual(expected, input.ToMarkdown(style));
        }

        [TestMethod]
        [DataRow("abc", "www.google.com", "[abc](www.google.com)")]
        [DataRow("abc aa", "www.google.com", "[abc aa](www.google.com)")]
        [DataRow("abc aa", "www.google.com www.google.com", "[abc aa](www.google.com www.google.com)")]
        public void Can_ConvertStringToLink(string text, string link, string expected)
        {
            Assert.AreEqual(expected, text.ToMarkdownLink(link));
        }

        [TestMethod]
        [DataRow("abc", "www.google.com", "![abc](www.google.com)")]
        [DataRow("abc aa", "www.google.com", "![abc aa](www.google.com)")]
        [DataRow("abc aa", "www.google.com www.google.com", "![abc aa](www.google.com www.google.com)")]
        public void Can_ConvertStringToImage(string text, string link, string expected)
        {
            Assert.AreEqual(expected, text.ToMarkdownImage(link));
        }

        [TestMethod]
        [DataRow("abc")]
        [DataRow("abc aa")]
        [DataRow("abc aa\r\n abbb")]
        public void Can_ConvertStringToCodeBlock(string text)
        {
            Assert.AreEqual($"```{Environment.NewLine}{text}{Environment.NewLine}```{Environment.NewLine}", text.ToMarkdownCodeBlock());
        }
    }
}
