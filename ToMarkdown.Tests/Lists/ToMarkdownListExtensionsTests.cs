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
            EmptyCheck(new List<int>());
            EmptyCheck(new List<bool>());
            EmptyCheck(new List<TestClass1>());
            EmptyCheck(new List<TestClass4>());
        }

        [TestMethod]
        [DataRow(ListStyle.Dash)]
        [DataRow(ListStyle.Star)]
        [DataRow(ListStyle.Plus)]
        public void Can_ReturnCorrectStyle(ListStyle style)
        {
            StyleCheck(new List<int>() { 1 }, style);
            StyleCheck(new List<bool>() { true }, style);
            StyleCheck(new List<TestClass1>() { new TestClass1() }, style);
            StyleCheck(new List<TestClass4>() { new TestClass4() }, style);
        }

        [TestMethod]
        public void Can_ReturnCorrectEnumeration()
        {
            EnumerationCheck(new List<int>() { 1 });
            EnumerationCheck(new List<int>() { 1, 2, 4, 7 });
            EnumerationCheck(new List<bool>() { true });
            EnumerationCheck(new List<TestClass1>() { new TestClass1() });
            EnumerationCheck(new List<TestClass1>() { new TestClass1(), new TestClass1() });
            EnumerationCheck(new List<TestClass4>() { new TestClass4() });
        }

        #region Helper Methods 

        private void EmptyCheck<T>(IEnumerable<T> item)
        {
            var result = item.ToMarkdownList();
            Assert.AreEqual("", result);
        }

        private void StyleCheck<T>(IEnumerable<T> item, ListStyle style)
        {
            var result = item.ToMarkdownList(style);
            var split = result.Split(Environment.NewLine).ToList();
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

        private void EnumerationCheck<T>(IEnumerable<T> item)
        {
            var result = item.ToMarkdownEnumeratedList();
            var split = result.Split(Environment.NewLine).ToList();
            split.RemoveAll(x => x == "");
            int counter = 1;
            foreach (var line in split)
                Assert.IsTrue(line.StartsWith($"{counter++}"));
        }

        #endregion
    }
}
