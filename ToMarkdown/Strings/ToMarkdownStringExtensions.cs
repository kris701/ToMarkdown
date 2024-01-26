using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToMarkdown.Strings
{
    /// <summary>
    /// Extensions to format markdown strings
    /// </summary>
    public static class ToMarkdownStringExtensions
    {
        /// <summary>
        /// Styles for markdown output strings
        /// </summary>
        public enum StringStyle {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            None,
            Heading1, Heading2, Heading3,
            Bold, Italic, StrikeThrough, Code, BlockQuote, Highlight, Subscript, Superscript
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// Converts a string into a markdown string with a given <seealso cref="StringStyle"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToMarkdown(this string str, StringStyle style = StringStyle.None)
        {
            switch (style)
            {
                case StringStyle.None: return str;
                case StringStyle.Heading1: return $"# {str}";
                case StringStyle.Heading2: return $"## {str}";
                case StringStyle.Heading3: return $"### {str}";
                case StringStyle.Bold: return $"**{str}**";
                case StringStyle.Italic: return $"*{str}*";
                case StringStyle.StrikeThrough: return $"~~{str}~~";
                case StringStyle.Code: return $"`{str}`";
                case StringStyle.BlockQuote: return $"> {str}";
                case StringStyle.Highlight: return $"=={str}==";
                case StringStyle.Subscript: return $"~{str}~";
                case StringStyle.Superscript: return $"^{str}^";
                default: throw new ArgumentException("Unknown string style!");
            }
        }

        /// <summary>
        /// Converts a string to a <seealso href="https://www.markdownguide.org/basic-syntax/#links">markdown link</seealso>, with the link being the parameter
        /// </summary>
        /// <param name="str"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public static string ToMarkdownLink(this string str, string link)
        {
            return $"[{str}]({link})";
        }

        /// <summary>
        /// Converts a string to a <seealso href="https://www.markdownguide.org/basic-syntax/#images-1">markdown image link</seealso>, with the image link being the parameter
        /// </summary>
        /// <param name="str"></param>
        /// <param name="imageLink"></param>
        /// <returns></returns>
        public static string ToMarkdownImage(this string str, string imageLink)
        {
            return $"![{str}]({imageLink})";
        }

        /// <summary>
        /// Converts a string to a markdown <seealso href="https://www.markdownguide.org/extended-syntax/#fenced-code-blocks">code block</seealso>.
        /// You can also give it what language it should be <seealso href="https://www.markdownguide.org/extended-syntax/#syntax-highlighting">syntax highlighted</seealso> as.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string ToMarkdownCodeBlock(this string str, string language = "")
        {
            var sb = new StringBuilder();
            sb.AppendLine($"```{language}");
            sb.AppendLine(str);
            sb.AppendLine("```");
            return sb.ToString();
        }
    }
}
