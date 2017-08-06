using System.IO;

namespace Aurochses.Testing.Mvc.Localization
{
    /// <summary>
    /// Class FileItem.
    /// </summary>
    public class FileItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileItem"/> class.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="fileName">Name of the file.</param>
        public FileItem(string projectPath, string directoryPath, string relativePath, string fileName)
        {
            ProjectPath = projectPath;
            DirectoryPath = directoryPath;
            RelativePath = relativePath;
            FileName = fileName;
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        }

        /// <summary>
        /// Gets the project path.
        /// </summary>
        /// <value>
        /// The project path.
        /// </value>
        public string ProjectPath { get; }

        /// <summary>
        /// Gets the directory path.
        /// </summary>
        /// <value>
        /// The directory path.
        /// </value>
        public string DirectoryPath { get; }

        /// <summary>
        /// Gets the relative path.
        /// </summary>
        /// <value>
        /// The relative path.
        /// </value>
        public string RelativePath { get; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; }

        /// <summary>
        /// Gets the file name without extension.
        /// </summary>
        /// <value>
        /// The file name without extension.
        /// </value>
        public string FileNameWithoutExtension { get; }

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <returns>Full path</returns>
        public string GetFullPath()
        {
            return $@"{ProjectPath}{DirectoryPath}{RelativePath}\{FileName}";
        }

        /// <summary>
        /// Gets the full relative path.
        /// </summary>
        /// <returns>Full relative path</returns>
        public string GetFullRelativePath()
        {
            return $@"{DirectoryPath}{RelativePath}\{FileName}";
        }
    }
}