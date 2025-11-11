using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Core
{
    public static class FileProcessor
    {
        public static long GetDirectorySizeInBytes(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                throw new ArgumentException($"Directory with path: ${dirPath} does not exist.");
            }

            long size = 0;
            
            var directoryInfo = new DirectoryInfo(dirPath);
            var directoryFiles = directoryInfo.GetFiles();
            foreach (var file in directoryFiles)
            {
                size += file.Length;
            }
            // Add subdirectory sizes.
            var dirSubDirectories = directoryInfo.GetDirectories();
            foreach (var subDirectory in dirSubDirectories)
            {
                size += GetDirectorySizeInBytes(subDirectory.FullName);
            }

            return size;
        }
    }
}
