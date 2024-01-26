using System.Text;

namespace ToMarkdown
{
    /// <summary>
    /// Markdown Extensions
    /// </summary>
    public static partial class ToMarkdownExtensions
    {
        /// <summary>
        /// <seealso href="https://www.markdownguide.org/basic-syntax/#unordered-lists">List styles</seealso>. The options are "-", "*" and "+"
        /// </summary>
        public enum ListStyle
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            Dash,
            Star,
            Plus
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
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
            foreach (var item in list)
                sb.AppendLine($"{counter++}. {item}");

            return sb.ToString();
        }

        /// <summary>
        /// Converts a list into a <seealso href="https://www.markdownguide.org/extended-syntax/#definition-lists">markdown definition list</seealso> with a given <paramref name="title"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string ToMarkdownDefinitionList<T>(this IEnumerable<T> list, string title)
        {
            var sb = new StringBuilder(title);
            foreach (var item in list)
                sb.AppendLine($": {item}");
            return sb.ToString();
        }

        /// <summary>
        /// Converts a list into a <seealso href="https://www.markdownguide.org/extended-syntax/#task-lists">markdown task list</seealso>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToMarkdownTaskList<T>(this IEnumerable<T> list)
        {
            var sb = new StringBuilder();
            foreach (var item in list)
                sb.AppendLine($"- [ ] {item}");
            return sb.ToString();
        }

        /// <summary>
        /// Converts a list into a <seealso href="https://www.markdownguide.org/extended-syntax/#task-lists">markdown task list</seealso>.
        /// This also has a list defining what items should be marked as checked.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="checks"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToMarkdownTaskList<T>(this IEnumerable<T> list, List<bool> checks)
        {
            if (list.Count() != checks.Count)
                throw new ArgumentException("Checks list must contain just as many items as the input list!");

            var sb = new StringBuilder();
            var index = 0;
            foreach (var item in list)
            {
                if (checks[index++])
                    sb.AppendLine($"- [x] {item}");
                else
                    sb.AppendLine($"- [ ] {item}");
            }
            return sb.ToString();
        }
    }
}
