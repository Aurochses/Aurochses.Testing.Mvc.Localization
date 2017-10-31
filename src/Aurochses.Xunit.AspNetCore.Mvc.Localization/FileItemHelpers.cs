using System.Collections.Generic;
using System.IO;

namespace Aurochses.Xunit.AspNetCore.Mvc.Localization
{
    /// <summary>
    /// Class FileItemHelpers.
    /// </summary>
    public static class FileItemHelpers
    {
        /// <summary>
        /// Gets the file items.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns>List of FileItems</returns>
        public static List<FileItem> GetFileItems(string projectPath, string directoryPath, string relativePath, string searchPattern)
        {
            var list = new List<FileItem>();

            var path = $"{projectPath}{directoryPath}{relativePath}";

            foreach (var filePath in Directory.GetFiles(path, searchPattern))
            {
                var file = new FileInfo(filePath);

                list.Add(new FileItem(projectPath, directoryPath, relativePath, file.Name));
            }

            foreach (var subDirectoryPath in Directory.GetDirectories(path))
            {
                var directory = new DirectoryInfo(subDirectoryPath);

                list.AddRange(GetFileItems(projectPath, directoryPath, $@"{relativePath}\{directory.Name}", searchPattern));
            }

            return list;
        }
    }
}