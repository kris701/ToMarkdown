using ToMarkdown.Tables;
using ToMarkdown.Tests.TestClasses;

namespace ToMarkdown.Tests.Tables
{
    [TestClass]
    public class ToMarkdownTableExtensionsTests
    {
        [TestMethod]
        public void Can_ReturnEmptyIfEmpty_Primitives()
        {
            Assert.AreEqual("", new List<int>().ToMarkdownTable());
            Assert.AreEqual("", new List<double>().ToMarkdownTable());
            Assert.AreEqual("", new List<string>().ToMarkdownTable());

            Assert.AreEqual("", new Queue<int>().ToMarkdownTable());
            Assert.AreEqual("", new Queue<double>().ToMarkdownTable());
            Assert.AreEqual("", new Queue<string>().ToMarkdownTable());

            Assert.AreEqual("", new HashSet<int>().ToMarkdownTable());
            Assert.AreEqual("", new HashSet<double>().ToMarkdownTable());
            Assert.AreEqual("", new HashSet<string>().ToMarkdownTable());
        }

        [TestMethod]
        public void Can_ReturnEmptyIfEmpty_Complex()
        {
            Assert.AreEqual("", new List<TestClass1>().ToMarkdownTable());
            Assert.AreEqual("", new List<TestClass2>().ToMarkdownTable());
            Assert.AreEqual("", new List<TestClass3>().ToMarkdownTable());

            Assert.AreEqual("", new Queue<TestClass1>().ToMarkdownTable());
            Assert.AreEqual("", new Queue<TestClass2>().ToMarkdownTable());
            Assert.AreEqual("", new Queue<TestClass3>().ToMarkdownTable());

            Assert.AreEqual("", new HashSet<TestClass1>().ToMarkdownTable());
            Assert.AreEqual("", new HashSet<TestClass2>().ToMarkdownTable());
            Assert.AreEqual("", new HashSet<TestClass3>().ToMarkdownTable());
        }

        [TestMethod]
        public void Can_ReturnSingleColumn_Primitives()
        {
            TableSizeCheck(new List<int>() { 1 }.ToMarkdownTable(), 1, 3);
            TableSizeCheck(new List<string>() { "abc" }.ToMarkdownTable(), 1, 3);

            TableSizeCheck(new HashSet<int>() { 1 }.ToMarkdownTable(), 1, 3);
            TableSizeCheck(new HashSet<string>() { "abc" }.ToMarkdownTable(), 1, 3);
        }

        [TestMethod]
        public void Can_ReturnSingleColumn_Complex()
        {
            TableSizeCheck(new List<TestClass1>() { new TestClass1() }.ToMarkdownTable(), 2, 3);
            TableSizeCheck(new List<TestClass2>() { new TestClass2() }.ToMarkdownTable(), 4, 3);
            TableSizeCheck(new List<TestClass3>() { new TestClass3() }.ToMarkdownTable(), 2, 3);

            TableSizeCheck(new HashSet<TestClass1>() { new TestClass1() }.ToMarkdownTable(), 2, 3);
            TableSizeCheck(new HashSet<TestClass2>() { new TestClass2() }.ToMarkdownTable(), 4, 3);
            TableSizeCheck(new HashSet<TestClass3>() { new TestClass3() }.ToMarkdownTable(), 2, 3);
        }

        [TestMethod]
        public void Can_ReturnMultipleColumn_Primitives()
        {
            TableSizeCheck(new List<int>() { 1, 19, 325 }.ToMarkdownTable(), 1, 5);
            TableSizeCheck(new List<int>() { 1, 19, 325, 1355113541 }.ToMarkdownTable(), 1, 6);
            TableSizeCheck(new List<string>() { "abc", "q" }.ToMarkdownTable(), 1, 4);
            TableSizeCheck(new List<string>() { "abc", "q", "ccc" }.ToMarkdownTable(), 1, 5);

            TableSizeCheck(new HashSet<int>() { 1, 19, 325 }.ToMarkdownTable(), 1, 5);
            TableSizeCheck(new HashSet<int>() { 1, 19, 325, 1355113541 }.ToMarkdownTable(), 1, 6);
            TableSizeCheck(new HashSet<string>() { "abc", "q" }.ToMarkdownTable(), 1, 4);
            TableSizeCheck(new HashSet<string>() { "abc", "q", "ccc" }.ToMarkdownTable(), 1, 5);
        }

        [TestMethod]
        public void Can_ReturnMultipleColumn_Complex()
        {
            TableSizeCheck(new List<TestClass1>() { new TestClass1(), new TestClass1() }.ToMarkdownTable(), 2, 4);
            TableSizeCheck(new List<TestClass1>() { new TestClass1(), new TestClass1(), new TestClass1() }.ToMarkdownTable(), 2, 5);
            TableSizeCheck(new List<TestClass2>() { new TestClass2(), new TestClass2() }.ToMarkdownTable(), 4, 4);
            TableSizeCheck(new List<TestClass2>() { new TestClass2(), new TestClass2(), new TestClass2() }.ToMarkdownTable(), 4, 5);
            TableSizeCheck(new List<TestClass3>() { new TestClass3(), new TestClass3() }.ToMarkdownTable(), 2, 4);
            TableSizeCheck(new List<TestClass3>() { new TestClass3(), new TestClass3(), new TestClass3() }.ToMarkdownTable(), 2, 5);

            TableSizeCheck(new HashSet<TestClass1>() { new TestClass1(), new TestClass1() }.ToMarkdownTable(), 2, 4);
            TableSizeCheck(new HashSet<TestClass1>() { new TestClass1(), new TestClass1(), new TestClass1() }.ToMarkdownTable(), 2, 5);
            TableSizeCheck(new HashSet<TestClass2>() { new TestClass2(), new TestClass2() }.ToMarkdownTable(), 4, 4);
            TableSizeCheck(new HashSet<TestClass2>() { new TestClass2(), new TestClass2(), new TestClass2() }.ToMarkdownTable(), 4, 5);
        }

        [TestMethod]
        public void Can_SetCustomColumnHeaders_Primitive()
        {
            HeaderCheck(
                new List<int>() { 1, 2 }.ToMarkdownTable(new List<string>() { "*" }),
                "Int32");
            HeaderCheck(
                new List<int>() { 1, 2 }.ToMarkdownTable(new List<string>() { "Special" }),
                "Special");
        }

        [TestMethod]
        public void Can_SetCustomColumnHeaders_Complex()
        {
            HeaderCheck(
                new List<TestClass4>() { new TestClass4() }.ToMarkdownTable(new List<string>() { "*" }),
                "Value1");

            HeaderCheck(
                new List<TestClass4>() { new TestClass4() }.ToMarkdownTable(new List<string>() { "new va" }),
                "new va");

            HeaderCheck(
                new List<TestClass1>() { new TestClass1(), new TestClass1() }.ToMarkdownTable(new List<string>() { "*", "*" }),
                new List<string>() { "Value1", "Value2" });
            HeaderCheck(
                new List<TestClass1>() { new TestClass1(), new TestClass1() }.ToMarkdownTable(new List<string>() { "new name", "*" }),
                new List<string>() { "new name", "Value2" });
            HeaderCheck(
                new List<TestClass1>() { new TestClass1(), new TestClass1() }.ToMarkdownTable(new List<string>() { "new name", "other (m/s)" }),
                new List<string>() { "new name", "other (m/s)" });
        }

        #region Helper Methods 

        private void HeaderCheck(string text, List<string> expected)
        {
            var split = text.Split(Environment.NewLine).ToList();
            var columns = split[0].Split('|').ToList();
            columns.RemoveAll(x => x == "");
            for (int i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i], columns[i].Trim());
        }

        private void HeaderCheck(string text, string expected)
        {
            var split = text.Split(Environment.NewLine).ToList();
            var columns = split[0].Split('|').ToList();
            columns.RemoveAll(x => x == "");
            Assert.AreEqual(expected, columns[0].Trim());
        }

        private void TableSizeCheck(string text, int expectedColumns, int expectedRows)
        {
            var split = text.Split(Environment.NewLine).ToList();
            split.RemoveAll(x => x == "");
            Assert.AreEqual(expectedRows, split.Count);
            foreach (var row in split)
                Assert.AreEqual(expectedColumns + 1, Count(row, '|'));
        }

        private int Count(string str, char character)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
                if (str[i] == character)
                    count++;
            return count;
        }

        #endregion
    }
}
