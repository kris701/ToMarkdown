using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToMarkdown.Tables
{
    /// <summary>
    /// Extensions to create markdown lists.
    /// </summary>
    public static class ToMarkdownListExtensions
    {
        /// <summary>
        /// <seealso href="https://www.markdownguide.org/basic-syntax/#unordered-lists">List styles</seealso>. The options are "-", "*" and "+"
        /// </summary>
        public enum ListStyle { 
            Dash, 
            Star, 
            Plus 
        }
        private static char StyleToChar(ListStyle style)
        {
            switch(style)
            {
                case ListStyle.Dash: return '-';
                case ListStyle.Star: return '*';
                case ListStyle.Plus: return '+';
                default: throw new Exception("Unknown style!");
            }
        }

        /// <summary>
        /// Convert a <seealso cref="IEnumerable{T}"/> into a <seealso href="https://www.markdownguide.org/basic-syntax/#lists-1">markdown list</seealso>.
        /// You can additionally use different list styles here.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static string ToMarkdownList<T>(this IEnumerable<T> list, ListStyle style = ListStyle.Star)
        {
            if (list.Count() == 0)
                return "";

            var sb = new StringBuilder();
            foreach (var item in list)
                sb.AppendLine($"{StyleToChar(style)} {item}");

            return sb.ToString();
        }

        /// <summary>
        /// Convert a <seealso cref="IEnumerable{T}"/> in a enumerated <seealso href="https://www.markdownguide.org/basic-syntax/#lists-1">markdown list</seealso>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToMarkdownEnumeratedList<T>(this IEnumerable<T> list)
        {
            if (list.Count() == 0)
                return "";

            var sb = new StringBuilder();
            int counter = 1;
            foreach(var item in list)
                sb.AppendLine($"{counter++}. {item}");

            return sb.ToString();
        }
    }
}
