using System.Text;

namespace ToMarkdown
{
    public static partial class ToMarkdownExtensions
    {
        /// <summary>
        /// Styles for markdown output strings
        /// </summary>
        public enum StringStyle
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            None,
            Heading1, Heading2, Heading3,
            Bold, Italic, StrikeThrough, Code, BlockQuote, Highlight, Subscript, Superscript
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// Converts a string into a markdown string with a given <seealso cref="StringStyle"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToMarkdown<T>(this T item, StringStyle style = StringStyle.None)
        {
            switch (style)
            {
                case StringStyle.None: return $"{item}";
                case StringStyle.Heading1: return $"# {item}";
                case StringStyle.Heading2: return $"## {item}";
                case StringStyle.Heading3: return $"### {item}";
                case StringStyle.Bold: return $"**{item}**";
                case StringStyle.Italic: return $"*{item}*";
                case StringStyle.StrikeThrough: return $"~~{item}~~";
                case StringStyle.Code: return $"`{item}`";
                case StringStyle.BlockQuote: return $"> {item}";
                case StringStyle.Highlight: return $"=={item}==";
                case StringStyle.Subscript: return $"~{item}~";
                case StringStyle.Superscript: return $"^{item}^";
                default: throw new ArgumentException("Unknown string style!");
            }
        }

        /// <summary>
        /// Converts a string to a <seealso href="https://www.markdownguide.org/basic-syntax/#links">markdown link</seealso>, with the link being the parameter
        /// </summary>
        /// <param name="item"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public static string ToMarkdownLink<T>(this T item, string link) => $"[{item}]({link})";

        /// <summary>
        /// Converts a string to a <seealso href="https://www.markdownguide.org/basic-syntax/#images-1">markdown image link</seealso>, with the image link being the parameter
        /// </summary>
        /// <param name="item"></param>
        /// <param name="imageLink"></param>
        /// <returns></returns>
        public static string ToMarkdownImage<T>(this T item, string imageLink) => $"![{item}]({imageLink})";

        /// <summary>
        /// Converts a string to a markdown <seealso href="https://www.markdownguide.org/extended-syntax/#fenced-code-blocks">code block</seealso>.
        /// You can also give it what language it should be <seealso href="https://www.markdownguide.org/extended-syntax/#syntax-highlighting">syntax highlighted</seealso> as.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string ToMarkdownCodeBlock<T>(this T item, string language = "")
        {
            var sb = new StringBuilder();
            sb.AppendLine($"```{language}");
            sb.AppendLine($"{item}");
            sb.AppendLine("```");
            return sb.ToString();
        }
    }
}
