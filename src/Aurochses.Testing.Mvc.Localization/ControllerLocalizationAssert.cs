using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aurochses.Testing.Mvc.Localization
{
    /// <summary>
    /// Class ControllerLocalizationAssert.
    /// </summary>
    public class ControllerLocalizationAssert
    {
        /// <summary>
        /// Validates the controllers.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <param name="resourcesDirectoryPath">The resources directory path.</param>
        /// <param name="controllersDirectoryPath">The controllers directory path.</param>
        /// <param name="predefinedLocalizedFileItems">The predefined localized file items.</param>
        /// <returns>
        /// List of LocalizedFileItems
        /// </returns>
        public static List<LocalizedFileItem> Validate(string projectPath, string resourcesDirectoryPath = @"\Resources\Controllers", string controllersDirectoryPath = @"\Controllers", List<LocalizedFileItem> predefinedLocalizedFileItems = null)
        {
            var fileItems = FileItemHelpers.GetFileItems(projectPath, controllersDirectoryPath, "", "*Controller.cs");

            var localizedFileItems = new List<LocalizedFileItem>();

            var regex = new Regex(@"_controllerLocalization\[""(?<name>\S+)("",|""])");

            foreach (var fileItem in fileItems)
            {
                var content = File.ReadAllText(fileItem.GetFullPath());

                var item = new LocalizedFileItem(projectPath, controllersDirectoryPath, fileItem.RelativePath, fileItem.FileName);

                foreach (Match match in regex.Matches(content))
                {
                    item.Names.Add(match.Groups["name"].Value);
                }

                var predefinedLocalizedFileItem = predefinedLocalizedFileItems?.FirstOrDefault(x => x.RelativePath == item.RelativePath && x.FileNameWithoutExtension == item.FileNameWithoutExtension);

                if (predefinedLocalizedFileItem != null)
                {
                    foreach (var name in predefinedLocalizedFileItem.Names)
                    {
                        item.Names.Add(name);
                    }
                }

                if (item.Names.Any()) localizedFileItems.Add(item);
            }

            LocalizationAssert.Validate(projectPath, resourcesDirectoryPath, localizedFileItems);

            return localizedFileItems;
        }
    }
}