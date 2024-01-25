using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            EmptyCheck(new List<int>());
            EmptyCheck(new List<double>());
            EmptyCheck(new List<string>());

            EmptyCheck(new Queue<int>());
            EmptyCheck(new Queue<double>());
            EmptyCheck(new Queue<string>());

            EmptyCheck(new HashSet<int>());
            EmptyCheck(new HashSet<double>());
            EmptyCheck(new HashSet<string>());
        }

        [TestMethod]
        public void Can_ReturnEmptyIfEmpty_Complex()
        {
            EmptyCheck(new List<TestClass1>());
            EmptyCheck(new List<TestClass2>());
            EmptyCheck(new List<TestClass3>());

            EmptyCheck(new Queue<TestClass1>());
            EmptyCheck(new Queue<TestClass2>());
            EmptyCheck(new Queue<TestClass3>());

            EmptyCheck(new HashSet<TestClass1>());
            EmptyCheck(new HashSet<TestClass2>());
            EmptyCheck(new HashSet<TestClass3>());
        }

        [TestMethod]
        public void Can_ReturnSingleColumn_Primitives()
        {
            TableSizeCheck(new List<int>() { 1 }, 1, 3);
            TableSizeCheck(new List<string>() { "abc" }, 1, 3);

            TableSizeCheck(new HashSet<int>() { 1 }, 1, 3);
            TableSizeCheck(new HashSet<string>() { "abc" }, 1, 3);
        }

        [TestMethod]
        public void Can_ReturnSingleColumn_Complex()
        {
            TableSizeCheck(new List<TestClass1>() { new TestClass1() }, 2, 3);
            TableSizeCheck(new List<TestClass2>() { new TestClass2() }, 4, 3);
            TableSizeCheck(new List<TestClass3>() { new TestClass3() }, 2, 3);

            TableSizeCheck(new HashSet<TestClass1>() { new TestClass1() }, 2, 3);
            TableSizeCheck(new HashSet<TestClass2>() { new TestClass2() }, 4, 3);
            TableSizeCheck(new HashSet<TestClass3>() { new TestClass3() }, 2, 3);
        }

        [TestMethod]
        public void Can_ReturnMultipleColumn_Primitives()
        {
            TableSizeCheck(new List<int>() { 1, 19, 325 }, 1, 5);
            TableSizeCheck(new List<int>() { 1, 19, 325, 1355113541 }, 1, 6);
            TableSizeCheck(new List<string>() { "abc", "q" }, 1, 4);
            TableSizeCheck(new List<string>() { "abc", "q", "ccc" }, 1, 5);

            TableSizeCheck(new HashSet<int>() { 1, 19, 325 }, 1, 5);
            TableSizeCheck(new HashSet<int>() { 1, 19, 325, 1355113541 }, 1, 6);
            TableSizeCheck(new HashSet<string>() { "abc", "q" }, 1, 4);
            TableSizeCheck(new HashSet<string>() { "abc", "q", "ccc" }, 1, 5);
        }

        [TestMethod]
        public void Can_ReturnMultipleColumn_Complex()
        {
            TableSizeCheck(new List<TestClass1>() { new TestClass1(), new TestClass1() }, 2, 4);
            TableSizeCheck(new List<TestClass1>() { new TestClass1(), new TestClass1(), new TestClass1() }, 2, 5);
            TableSizeCheck(new List<TestClass2>() { new TestClass2(), new TestClass2() }, 4, 4);
            TableSizeCheck(new List<TestClass2>() { new TestClass2(), new TestClass2(), new TestClass2() }, 4, 5);
            TableSizeCheck(new List<TestClass3>() { new TestClass3(), new TestClass3() }, 2, 4);
            TableSizeCheck(new List<TestClass3>() { new TestClass3(), new TestClass3(), new TestClass3() }, 2, 5);

            TableSizeCheck(new HashSet<TestClass1>() { new TestClass1(), new TestClass1() }, 2, 4);
            TableSizeCheck(new HashSet<TestClass1>() { new TestClass1(), new TestClass1(), new TestClass1() }, 2, 5);
            TableSizeCheck(new HashSet<TestClass2>() { new TestClass2(), new TestClass2() }, 4, 4);
            TableSizeCheck(new HashSet<TestClass2>() { new TestClass2(), new TestClass2(), new TestClass2() }, 4, 5);
        }

        [TestMethod]
        public void Can_SetCustomColumnHeaders_Primitive()
        {
            HeaderCheck(
                new List<int>() { 1, 2 },
                "*",
                "Int32");
            HeaderCheck(
                new List<int>() { 1, 2 },
                "Special",
                "Special");
        }

        [TestMethod]
        public void Can_SetCustomColumnHeaders_Complex()
        {
            HeaderCheck(
                new List<TestClass4>() { new TestClass4() },
                "*",
                "Value1");

            HeaderCheck(
                new List<TestClass4>() { new TestClass4() },
                "new va",
                "new va");

            HeaderCheck(
                new List<TestClass1>() { new TestClass1(), new TestClass1() }, 
                new List<string>() { "*", "*" },
                new List<string>() { "Value1", "Value2" });
            HeaderCheck(
                new List<TestClass1>() { new TestClass1(), new TestClass1() },
                new List<string>() { "new name", "*" },
                new List<string>() { "new name", "Value2" });
            HeaderCheck(
                new List<TestClass1>() { new TestClass1(), new TestClass1() },
                new List<string>() { "new name", "other (m/s)" },
                new List<string>() { "new name", "other (m/s)" });
        }

        #region Helper Methods 

        private void EmptyCheck<T>(IEnumerable<T> item)
        {
            var result = item.ToMarkdownTable();
            Assert.AreEqual("", result);
        }

        private void HeaderCheck<T>(IEnumerable<T> item, List<string> input, List<string> expected)
        {
            var result = item.ToMarkdownTable(input);
            var split = result.Split(Environment.NewLine).ToList();
            var columns = split[0].Split('|').ToList();
            columns.RemoveAll(x => x == "");
            for (int i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i], columns[i].Trim());
        }

        private void HeaderCheck<T>(IEnumerable<T> item, string input, string expected)
        {
            var result = item.ToMarkdownTable(input);
            var split = result.Split(Environment.NewLine).ToList();
            var columns = split[0].Split('|').ToList();
            columns.RemoveAll(x => x == "");
            Assert.AreEqual(expected, columns[0].Trim());
        }

        private void TableSizeCheck<T>(IEnumerable<T> item, int expectedColumns, int expectedRows)
        {
            var result = item.ToMarkdownTable();
            var split = result.Split(Environment.NewLine).ToList();
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
