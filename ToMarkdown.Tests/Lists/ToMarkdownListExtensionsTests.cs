using ToMarkdown.Lists;
using ToMarkdown.Tests.TestClasses;
using static ToMarkdown.Lists.ToMarkdownListExtensions;

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

        [TestMethod]
        public void Can_ReturnCorrectDefinitionList()
        {
            IsDefinitionList(new List<int>().ToMarkdownDefinitionList("abc"));
            IsDefinitionList(new List<int>() { 1, 5 }.ToMarkdownDefinitionList("abc"));
        }

        [TestMethod]
        public void Can_ReturnCorrectTask()
        {
            IsTaskList(new List<int>().ToMarkdownTaskList());
            IsTaskList(new List<int>() { 1, 5 }.ToMarkdownTaskList());
            IsTaskList(new List<int>() { 1, 5 }.ToMarkdownTaskList(new List<bool>() { false, true }));
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

        private void IsDefinitionList(string text)
        {
            var split = text.Split(Environment.NewLine).ToList();
            split.RemoveAll(x => x == "");
            Assert.IsFalse(split[0].StartsWith(":"));
            for (int i = 1; i < split.Count; i++)
                Assert.IsTrue(split[i].StartsWith(":"));
        }

        private void IsTaskList(string text)
        {
            var split = text.Split(Environment.NewLine).ToList();
            split.RemoveAll(x => x == "");
            for (int i = 0; i < split.Count; i++)
                Assert.IsTrue(split[i].StartsWith("- [ ]") || split[i].StartsWith("- [x]"));
        }

        #endregion
    }
}
