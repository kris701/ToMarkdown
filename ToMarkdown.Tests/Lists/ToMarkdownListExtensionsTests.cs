using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToMarkdown.Tables;
using ToMarkdown.Tests.TestClasses;
using static ToMarkdown.Tables.ToMarkdownListExtensions;

namespace ToMarkdown.Tests.Lists
{
    [TestClass]
    public class ToMarkdownListExtensionsTests
    {
        [TestMethod]
        public void Can_ReturnEmptyIfEmpty()
        {
            Assert.AreEqual("", new List<int>().ToMarkdownList());
            Assert.AreEqual("", new List<bool>().ToMarkdownList());
            Assert.AreEqual("", new List<TestClass1>().ToMarkdownList());
            Assert.AreEqual("", new List<TestClass4>().ToMarkdownList());
        }

        [TestMethod]
        [DataRow(ListStyle.Dash)]
        [DataRow(ListStyle.Star)]
        [DataRow(ListStyle.Plus)]
        public void Can_ReturnCorrectStyle(ListStyle style)
        {
            StyleCheck(new List<int>() { 1 }.ToMarkdownList(style), style);
            StyleCheck(new List<bool>() { true }.ToMarkdownList(style), style);
            StyleCheck(new List<TestClass1>() { new TestClass1() }.ToMarkdownList(style), style);
            StyleCheck(new List<TestClass4>() { new TestClass4() }.ToMarkdownList(style), style);
        }

        [TestMethod]
        public void Can_ReturnCorrectEnumeration()
        {
            EnumerationCheck(new List<int>() { 1 }.ToMarkdownEnumeratedList());
            EnumerationCheck(new List<int>() { 1, 2, 4, 7 }.ToMarkdownEnumeratedList());
            EnumerationCheck(new List<bool>() { true }.ToMarkdownEnumeratedList());
            EnumerationCheck(new List<TestClass1>() { new TestClass1() }.ToMarkdownEnumeratedList());
            EnumerationCheck(new List<TestClass1>() { new TestClass1(), new TestClass1() }.ToMarkdownEnumeratedList());
            EnumerationCheck(new List<TestClass4>() { new TestClass4() }.ToMarkdownEnumeratedList());
        }

        #region Helper Methods 

        private void StyleCheck(string text, ListStyle style)
        {
            var split = text.Split(Environment.NewLine).ToList();
            split.RemoveAll(x => x == "");
            foreach (var line in split)
                Assert.IsTrue(line.StartsWith(StyleToChar(style)));
        }

        private static char StyleToChar(ListStyle style)
        {
            switch (style)
            {
                case ListStyle.Dash: return '-';
                case ListStyle.Star: return '*';
                case ListStyle.Plus: return '+';
                default: throw new Exception("Unknown style!");
            }
        }

        private void EnumerationCheck(string text)
        {
            var split = text.Split(Environment.NewLine).ToList();
            split.RemoveAll(x => x == "");
            int counter = 1;
            foreach (var line in split)
                Assert.IsTrue(line.StartsWith($"{counter++}"));
        }

        #endregion
    }
}
