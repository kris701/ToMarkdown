# ToMarkdown

This is a small library that enables you to convert C# datastructures into Markdown format.
It includes more or less everything that is in the [Markdown Definition](https://www.markdownguide.org/basic-syntax/).
You can find this as a package on [Nuget](https://www.nuget.org/packages/ToMarkdown/), [GitHub](https://github.com/kris701/ToMarkdown/pkgs/nuget/ToMarkdown) or under [Releases](https://github.com/kris701/ToMarkdown/releases).

Currently there are:
* [`IEnumerable` to Table](./ToMarkdown/Tables/ToMarkdownTableExtensions.cs)
* [`IEnumerable` to List](./ToMarkdown/Lists/ToMarkdownListExtensions.cs)
  * Includes different list styles (enumerated, unordered and task list) as well as definition lists
* [`string` to markdown format](./ToMarkdown/Strings/ToMarkdownStringExtensions.cs)
  * Includes links and different text formats
