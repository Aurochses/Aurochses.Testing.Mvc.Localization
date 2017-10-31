using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aurochses.Xunit.AspNetCore.Mvc.Localization
{
    /// <summary>
    /// Class ViewLocalizationAssert.
    /// </summary>
    public class ViewLocalizationAssert
    {
        /// <summary>
        /// Validates the views.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <param name="resourcesDirectoryPath">The resources directory path.</param>
        /// <param name="viewsDirectoryPath">The views directory path.</param>
        /// <returns>List of LocalizedFileItems</returns>
        public static List<LocalizedFileItem> Validate(string projectPath, string resourcesDirectoryPath = @"\Resources\Views", string viewsDirectoryPath = @"\Views")
        {
            var fileItems = FileItemHelpers.GetFileItems(projectPath, viewsDirectoryPath, "", "*.cshtml");

            var localizedFileItems = new List<LocalizedFileItem>();

            var regex = new Regex(@"ViewLocalization\[""(?<name>\S+)("",|""])");

            foreach (var fileItem in fileItems)
            {
                var content = File.ReadAllText(fileItem.GetFullPath());

                var item = new LocalizedFileItem(projectPath, viewsDirectoryPath, fileItem.RelativePath, fileItem.FileName);

                foreach (Match match in regex.Matches(content))
                {
                    item.Names.Add(match.Groups["name"].Value);
                }

                if (item.Names.Any()) localizedFileItems.Add(item);
            }

            LocalizationAssert.Validate(projectPath, resourcesDirectoryPath, localizedFileItems);

            return localizedFileItems;
        }
    }
}