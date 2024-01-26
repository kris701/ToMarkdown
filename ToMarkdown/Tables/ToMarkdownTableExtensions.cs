using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToMarkdown.Tables
{
    /// <summary>
    /// Extensions to create markdown tables.
    /// </summary>
    public static class ToMarkdownTableExtensions
    {
        /// <summary>
        /// Allows you to add custom headers.
        /// The parameter <paramref name="columnHeaders"/> must match the exact amount of puplic properties that a class has.
        /// For properties you are not interested in changing the name for, put a "*" for that given item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="columnHeaders"></param>
        /// <returns></returns>
        public static string ToMarkdownTable<T>(this IEnumerable<T> list, List<string> columnHeaders)
        {
            if (list.Count() == 0)
                return "";

            if (typeof(T).IsClass && typeof(T) != typeof(string) && typeof(T) != typeof(decimal))
                return ClassTable(list, columnHeaders);
            return PrimitivesTable(list, columnHeaders[0]);
        }

        /// <summary>
        /// Allows you to add custom headers.
        /// The parameter <paramref name="columnHeader"/> must match the exact amount of puplic properties that a class has.
        /// For properties you are not interested in changing the name for, put a "*" for that given item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="columnHeader"></param>
        /// <returns></returns>
        public static string ToMarkdownTable<T>(this IEnumerable<T> list, string columnHeader)
        {
            if (list.Count() == 0)
                return "";

            if (typeof(T).IsClass && typeof(T) != typeof(string) && typeof(T) != typeof(decimal))
                return ClassTable(list, new List<string>() { columnHeader });
            return PrimitivesTable(list, columnHeader);
        }

        /// <summary>
        /// Converts a <seealso cref="IEnumerable{T}"/> into a <seealso href="https://www.markdownguide.org/extended-syntax/#tables">markdown table</seealso>.
        /// If the IEnumerable is of a primitive type, the header will be the typename
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>Markdown table as a string</returns>
        public static string ToMarkdownTable<T>(this IEnumerable<T> list)
        {
            if (list.Count() == 0)
                return "";

            if (typeof(T).IsClass && typeof(T) != typeof(string) && typeof(T) != typeof(decimal))
                return ClassTable(list, Enumerable.Repeat("*", typeof(T).GetProperties().Length).ToList());
            return PrimitivesTable(list, "*");
        }

        private static string ClassTable<T>(IEnumerable<T> list, List<string> columnHeaders)
        {
            var sb = new StringBuilder();

            var propInfo = typeof(T).GetProperties();
            if (propInfo.Length == 0)
                throw new ArgumentException("The class does not have any properties?");
            if (propInfo.Length != columnHeaders.Count)
                throw new ArgumentException($"Custom column header count ({columnHeaders.Count}) must match property count ({propInfo.Length})!");

            sb.Append("|");
            for(int i = 0; i < propInfo.Length; i++)
            {
                if (columnHeaders[i] != "*")
                    sb.Append($" {columnHeaders[i]} |");
                else
                    sb.Append($" {propInfo[i].Name} |");
            }
            sb.AppendLine();
            sb.AppendLine(GenerateSpacer(propInfo.Length));
            foreach (var item in list)
            {
                sb.Append("|");
                foreach (var info in propInfo)
                {
                    var value = info.GetValue(item);
                    if (value != null)
                        sb.Append($" {value} |");
                    else
                        sb.Append($" ERR |");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private static string PrimitivesTable<T>(IEnumerable<T> list, string columnHeader)
        {
            var sb = new StringBuilder();
            if (columnHeader != "*")
                sb.AppendLine($"| {columnHeader} |");
            else
                sb.AppendLine($"| {typeof(T).Name} |");
            sb.AppendLine(GenerateSpacer(1));
            foreach (var item in list)
                sb.AppendLine($"| {item} |");
            return sb.ToString();
        }

        private static string GenerateSpacer(int cols)
        {
            var sb = new StringBuilder();
            sb.Append("|");
            for (int i = 0; i < cols; i++)
                sb.Append(" - |");
            return sb.ToString();
        }
    }
}
