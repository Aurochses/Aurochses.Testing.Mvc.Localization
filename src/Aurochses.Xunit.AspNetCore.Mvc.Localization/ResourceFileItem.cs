using System.Collections.Generic;

namespace Aurochses.Xunit.AspNetCore.Mvc.Localization
{
    /// <summary>
    /// Class ResourceFileItem.
    /// </summary>
    /// <seealso cref="Aurochses.Xunit.AspNetCore.Mvc.Localization.FileItem" />
    public class ResourceFileItem : FileItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceFileItem"/> class.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="fileName">Name of the file.</param>
        public ResourceFileItem(string projectPath, string directoryPath, string relativePath, string fileName)
            : base(projectPath, directoryPath, relativePath, fileName)
        {
            Values = new Dictionary<string, Resx>();
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public Dictionary<string, Resx> Values { get; }

        /// <summary>
        /// Gets the full relative path.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>Full relative path</returns>
        public string GetFullRelativePath(string culture)
        {
            if (string.IsNullOrEmpty(culture)) return GetFullRelativePath();

            return $@"{DirectoryPath}{RelativePath}\{FileNameWithoutExtension}.{culture}.resx";
        }
    }
}