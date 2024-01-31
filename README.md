<p align="center">
    <img src="https://github.com/kris701/PDDLSharp/assets/22596587/bc8cb98e-bf75-44ca-935b-cd09f9b9b7de" width="200" height="200" />
</p>

[![Build and Publish](https://github.com/kris701/ToMarkdown/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/kris701/ToMarkdown/actions/workflows/dotnet-desktop.yml)
![Nuget](https://img.shields.io/nuget/v/ToMarkdown)
![Nuget](https://img.shields.io/nuget/dt/ToMarkdown)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/kris701/ToMarkdown/main)
![GitHub commit activity (branch)](https://img.shields.io/github/commit-activity/m/kris701/ToMarkdown)
![Static Badge](https://img.shields.io/badge/Platform-Windows-blue)
![Static Badge](https://img.shields.io/badge/Platform-Linux-blue)
![Static Badge](https://img.shields.io/badge/Framework-dotnet--7.0-green)

# ToMarkdown

This is a small library that enables you to convert C# datastructures into Markdown format.
It includes more or less everything that is in the [Markdown Definition](https://www.markdownguide.org/basic-syntax/).
You can find this as a package on [Nuget](https://www.nuget.org/packages/ToMarkdown/), [GitHub](https://github.com/kris701/ToMarkdown/pkgs/nuget/ToMarkdown) or under [Releases](https://github.com/kris701/ToMarkdown/releases).
All the extensions are available under the namespace `ToMarkdown`.

Currently there are:
* [`IEnumerable` to Table](./ToMarkdown/Tables/ToMarkdownTableExtensions.cs)
* [`IEnumerable` to List](./ToMarkdown/Lists/ToMarkdownListExtensions.cs)
  * Includes different list styles (enumerated, unordered and task list) as well as definition lists
* [Any type to markdown strings](./ToMarkdown/Strings/ToMarkdownStringExtensions.cs)
  * Includes links and different text formats

# Examples
Here is a set of examples of how to use the package:

## Example 1

```csharp
var items = new[]
{
    new { Name = "John", Value = -1 },
    new { Name = "Allan", Value = 60 },
    new { Name = "Peter", Value = 46703 }
};
var text = items.ToMarkdownTable();
```

Gives:

| Name | Value |
| - | - |
| John | -1 |
| Allan | 60 |
| Peter | 46703 |

## Example 2

```csharp
var stringValue = "some text";
var text = stringValue.ToMarkdown(ToMarkdownExtensions.StringStyle.StrikeThrough);
```
Gives:

~~some text~~

## Example 3

```csharp
var list = new List<string>()
{
    "First Item",
    "Second Item",
    "Third Item"
};
var text = list.ToMarkdownEnumeratedList();
```
Gives:

1. First Item
2. Second Item
3. Third Item
