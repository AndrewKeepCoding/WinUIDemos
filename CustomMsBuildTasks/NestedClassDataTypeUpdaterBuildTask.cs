using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace CustomMsBuildTasks;

public class NestedClassDataTypeUpdaterBuildTask : Task
{
    [Required]
    public string IntermediateOutputPath { get; set; } = string.Empty;

    public override bool Execute()
    {
        //Debugger.Launch();

        Log.LogMessage(importance: MessageImportance.High, $"NestedClassDataTypeUpdaterBuildTask IntermediateOutputPath: {IntermediateOutputPath}");

        var targetPathCollection = Directory.GetFiles(IntermediateOutputPath, "*.g.cs", SearchOption.AllDirectories);
        var pattern = @"(\w+)\+(\w+)";
        var regex = new Regex(pattern);

        foreach (var path in targetPathCollection)
        {
            var fileContent = File.ReadAllText(path);
            var updatedContent = regex.Replace(fileContent, match =>
            {
                var left = match.Groups[1].Value;
                var right = match.Groups[2].Value;
                var replacement = $"{left}.{right}";

                Log.LogMessage(importance: MessageImportance.Normal, $"Replaced: \"{match.Value}\" → \"{replacement}\"");

                return replacement;
            });

            if (updatedContent == fileContent)
            {
                continue;
            }

            File.WriteAllText(path, updatedContent);
            Log.LogMessage(importance: MessageImportance.High, $"Updated file: {path}");
        }

        return true;
    }
}
