using System;
using System.Collections.Generic;
using System.Linq;

namespace Aurochses.Testing.Mvc.Localization
{
    /// <summary>
    /// Class LocalizationAssert.
    /// </summary>
    public static class LocalizationAssert
    {
        /// <summary>
        /// Validates the specified project path.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <param name="resourcesDirectoryPath">The resources directory path.</param>
        /// <param name="localizedFileItems">The localized file items.</param>
        /// <exception cref="System.Exception">Throws exception if errors.</exception>
        public static void Validate(string projectPath, string resourcesDirectoryPath, List<LocalizedFileItem> localizedFileItems)
        {
            var cultures = new HashSet<string> {""};

            var resourceFileItems = ResourceFileItemHelpers.GetResourceFileItems(projectPath, $"{resourcesDirectoryPath}", ref cultures);

            foreach (var localizedFileItem in localizedFileItems)
            {
                var resourceFileItem = resourceFileItems.FirstOrDefault(x => x.RelativePath == localizedFileItem.RelativePath && x.FileNameWithoutExtension == localizedFileItem.FileNameWithoutExtension);

                if (resourceFileItem == null) throw new Exception($@"Resource file for '{localizedFileItem.GetFullRelativePath()}' not found.");

                foreach (var culture in cultures)
                {
                    var resx = resourceFileItem.Values[culture];

                    foreach (var name in localizedFileItem.Names)
                    {
                        var resxData = resx.Data.FirstOrDefault(x => x.Name == name);

                        if (resxData == null) throw new Exception($@"Resource file '{resourceFileItem.GetFullRelativePath(culture)}' has no value for '{name}' from localized file '{localizedFileItem.GetFullRelativePath()}'.");
                    }

                    foreach (var data in resx.Data)
                    {
                        if (localizedFileItem.Names.All(x => x != data.Name)) throw new Exception($@"Value for '{data.Name}' from resource file '{resourceFileItem.GetFullRelativePath(culture)}' is not used.");
                    }
                }
            }

            foreach (var resourceFileItem in resourceFileItems)
            {
                if (!localizedFileItems.Exists(x => x.RelativePath == resourceFileItem.RelativePath && x.FileNameWithoutExtension == resourceFileItem.FileNameWithoutExtension))
                {
                    foreach (var culture in resourceFileItem.Values.Keys)
                    {
                        throw new Exception($@"Resource file '{resourceFileItem.GetFullRelativePath(culture)}' not used.");
                    }
                }
            }
        }
    }
}