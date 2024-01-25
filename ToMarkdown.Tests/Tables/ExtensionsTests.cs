using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToMarkdown.Tables;
using ToMarkdown.Tests.Tables.TestClasses;

namespace ToMarkdown.Tests.Tables
{
    [TestClass]
    public class ExtensionsTests
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

            EmptyCheck(new Queue<TestClass1>());

            EmptyCheck(new HashSet<TestClass1>());
        }

        [TestMethod]
        public void Can_ReturnSingleColumn_Primitives()
        {
            ColumnCheck(new List<int>() { 1 }, 1, 3);
            ColumnCheck(new List<string>() { "abc" }, 1, 3);

            ColumnCheck(new HashSet<int>() { 1 }, 1, 3);
            ColumnCheck(new HashSet<string>() { "abc" }, 1, 3);
        }

        [TestMethod]
        public void Can_ReturnSingleColumn_Complex()
        {
            ColumnCheck(new List<TestClass1>() { new TestClass1() }, 2, 3);

            ColumnCheck(new HashSet<TestClass1>() { new TestClass1() }, 2, 3);
        }

        [TestMethod]
        public void Can_ReturnMultipleColumn_Primitives()
        {
            ColumnCheck(new List<int>() { 1, 19, 325 }, 1, 5);
            ColumnCheck(new List<int>() { 1, 19, 325, 1355113541 }, 1, 6);
            ColumnCheck(new List<string>() { "abc", "q" }, 1, 4);
            ColumnCheck(new List<string>() { "abc", "q", "ccc" }, 1, 5);

            ColumnCheck(new HashSet<int>() { 1, 19, 325 }, 1, 5);
            ColumnCheck(new HashSet<int>() { 1, 19, 325, 1355113541 }, 1, 6);
            ColumnCheck(new HashSet<string>() { "abc", "q" }, 1, 4);
            ColumnCheck(new HashSet<string>() { "abc", "q", "ccc" }, 1, 5);
        }

        [TestMethod]
        public void Can_ReturnMultipleColumn_Complex()
        {
            ColumnCheck(new List<TestClass1>() { new TestClass1(), new TestClass1() }, 2, 4);
            ColumnCheck(new List<TestClass1>() { new TestClass1(), new TestClass1(), new TestClass1() }, 2, 5);

            ColumnCheck(new HashSet<TestClass1>() { new TestClass1(), new TestClass1() }, 2, 4);
            ColumnCheck(new HashSet<TestClass1>() { new TestClass1(), new TestClass1(), new TestClass1() }, 2, 5);
        }

        #region Helper Methods 

        private void EmptyCheck<T>(IEnumerable<T> item)
        {
            var result = item.ToMarkdown();
            Assert.AreEqual("", result);
        }

        private void ColumnCheck<T>(IEnumerable<T> item, int expectedColumns, int expectedRows)
        {
            var result = item.ToMarkdown();
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
