using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToMarkdown.Tables
{
    public static class Extensions
    {
        public static string ToMarkdown<T>(this IEnumerable<T> list)
        {
            if (list.Count() == 0)
                return "";

            if (typeof(T).IsClass && typeof(T) != typeof(string) && typeof(T) != typeof(decimal))
                return ClassTable(list);
            return PrimitivesTable(list);
        }

        private static string ClassTable<T>(IEnumerable<T> list)
        {
            var sb = new StringBuilder();

            var propInfo = typeof(T).GetProperties();
            if (propInfo.Length == 0)
                throw new ArgumentException("The class does not have any properties?");
            sb.Append("|");
            foreach (var info in propInfo)
                sb.Append($" {info.Name} |");
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

        private static string PrimitivesTable<T>(IEnumerable<T> list)
        {
            var sb = new StringBuilder();
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
