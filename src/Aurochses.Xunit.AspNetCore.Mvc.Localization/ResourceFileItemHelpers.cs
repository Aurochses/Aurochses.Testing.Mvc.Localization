using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Aurochses.Xunit.AspNetCore.Mvc.Localization
{
    /// <summary>
    /// Class ResourceFileItemHelpers.
    /// </summary>
    public class ResourceFileItemHelpers
    {
        /// <summary>
        /// Gets the resource file items.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="cultures">The cultures.</param>
        /// <returns>List of ResourceFileItems</returns>
        /// <exception cref="System.Exception">Throws exception if resource file name is not correct or resource file must be added.</exception>
        public static List<ResourceFileItem> GetResourceFileItems(string projectPath, string directoryPath, ref HashSet<string> cultures)
        {
            var fileItems = FileItemHelpers.GetFileItems(projectPath, directoryPath, "", "*.resx");

            var list = new List<ResourceFileItem>();

            var regex = new Regex(@"^(?<fileName>[^.]+)(.(?<culture>[^.]+))?.resx");

            var xmlSerializer = new XmlSerializer(typeof(Resx));

            foreach (var fileItem in fileItems)
            {
                var match = regex.Match(fileItem.FileName);

                if (match.Success)
                {
                    var resourceItem = list
                        .FirstOrDefault(
                            x => x.RelativePath == fileItem.RelativePath
                                 && x.FileName == $"{match.Groups["fileName"].Value}.resx"
                        );

                    if (resourceItem == null)
                    {
                        resourceItem = new ResourceFileItem(projectPath, directoryPath, fileItem.RelativePath, $"{match.Groups["fileName"].Value}.resx");

                        list.Add(resourceItem);
                    }

                    cultures.Add(match.Groups["culture"].Value);

                    Resx resx;

                    using (var stream = new FileStream(fileItem.GetFullPath(), FileMode.Open))
                    {
                        resx = (Resx) xmlSerializer.Deserialize(stream);
                    }

                    resourceItem.Values.Add(match.Groups["culture"].Value, resx);
                }
                else
                {
                    throw new Exception($@"Resource file '{fileItem.GetFullRelativePath()}' has invalid name.");
                }
            }

            foreach (var item in list)
            {
                foreach (var culture in cultures)
                {
                    if (!item.Values.ContainsKey(culture))
                    {
                        throw new Exception($@"Resource file '{item.GetFullRelativePath(culture)}' must be added.");
                    }
                }
            }

            return list;
        }
    }
}