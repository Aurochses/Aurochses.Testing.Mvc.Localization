using System.Collections.Generic;

namespace Aurochses.Xunit.AspNetCore.Mvc.Localization
{
    /// <summary>
    /// Class LocalizedFileItem.
    /// </summary>
    /// <seealso cref="Aurochses.Xunit.AspNetCore.Mvc.Localization.FileItem" />
    public class LocalizedFileItem : FileItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedFileItem"/> class.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="fileName">Name of the file.</param>
        public LocalizedFileItem(string projectPath, string directoryPath, string relativePath, string fileName)
            : base(projectPath, directoryPath, relativePath, fileName)
        {
            Names = new HashSet<string>();
        }

        /// <summary>
        /// Gets the names.
        /// </summary>
        /// <value>
        /// The names.
        /// </value>
        public HashSet<string> Names { get; }
    }
}