using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Aurochses.Xunit.AspNetCore.Mvc.Localization
{
    /// <summary>
    /// Class ModelLocalizationAssert.
    /// </summary>
    public class ModelLocalizationAssert
    {
        /// <summary>
        /// Validates the models.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="projectPath">The project path.</param>
        /// <param name="resourcesDirectoryPath">The resources directory path.</param>
        /// <param name="modelsDirectoryPath">The models directory path.</param>
        /// <returns>List of LocalizedFileItems</returns>
        public static List<LocalizedFileItem> Validate(Assembly assembly, string projectPath, string resourcesDirectoryPath = @"\Resources\Models", string modelsDirectoryPath = @"\Models")
        {
            var fileItems = FileItemHelpers.GetFileItems(projectPath, modelsDirectoryPath, "", "*.cs");

            var localizedFileItems = new List<LocalizedFileItem>();

            var namespaceRegex = new Regex(@"namespace (?<namespace>\S+)");
            var classRegex = new Regex(@"public class (?<class>\S+)");

            foreach (var fileItem in fileItems)
            {
                var content = File.ReadAllText(fileItem.GetFullPath());

                var @namespace = namespaceRegex.Match(content).Groups["namespace"].Value;
                var @class = classRegex.Match(content).Groups["class"].Value;

                var type = assembly.GetType($"{@namespace}.{@class}");

                var item = new LocalizedFileItem(projectPath, modelsDirectoryPath, fileItem.RelativePath, fileItem.FileName);

                foreach (var property in type.GetRuntimeProperties())
                {
                    var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();

                    if (displayAttribute == null) continue;

                    if (!string.IsNullOrWhiteSpace(displayAttribute.Name)) item.Names.Add(displayAttribute.Name);
                    if (!string.IsNullOrWhiteSpace(displayAttribute.Prompt)) item.Names.Add(displayAttribute.Prompt);
                    if (!string.IsNullOrWhiteSpace(displayAttribute.Description)) item.Names.Add(displayAttribute.Description);
                }

                if (item.Names.Any()) localizedFileItems.Add(item);
            }

            LocalizationAssert.Validate(projectPath, resourcesDirectoryPath, localizedFileItems);

            return localizedFileItems;
        }
    }
}