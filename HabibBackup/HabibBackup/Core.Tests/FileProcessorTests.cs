using HB.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;

namespace Core.Tests
{
    [TestClass]
    public class FileProcessorTests
    {
        [TestMethod]
        public void GetFolderSizeInBytes_FolderWithOnlyOneFile_ReturnsCorrectSize()
        {
            var newFolderPath = _createFolder(1048576);

            var folderSize = FileProcessor.GetDirectorySizeInBytes(newFolderPath);

            Assert.AreEqual(1048576, folderSize);
        }

        [TestMethod]
        public void GetFolderSizeInBytes_FolderWithSubDirectories_ReturnsCorrectSize()
        {
            var newFolderPath = _createFolderWithSubDirectories(1048576);

            var folderSize = FileProcessor.GetDirectorySizeInBytes(newFolderPath);

            Assert.AreEqual(1048576, folderSize);
        }

        [TestMethod]
        public void Tmp()
        {
            //var newFolderPath = _createFolderWithSubDirectories(4664715679);

            var folderSize = FileProcessor.GetDirectorySizeInBytes(@"C:\Users\nasri\OneDrive\Documents");

            Assert.AreEqual(1492229, folderSize);
        }

        private string _createFolderWithSubDirectories(long sizeInBytes)
        {
            var path = Path.Combine(@"D:\Temp\Tests", DateTime.Now.Ticks.ToString());
            
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(Path.Combine(path, "subDir1"));
            Directory.CreateDirectory(Path.Combine(path, "subDir2"));

            var bytes = new byte[sizeInBytes / 2];
            File.WriteAllBytes(Path.Combine(Path.Combine(path, "subDir1"), 
                $"tmp{DateTime.Now.Ticks}.txt"), bytes);
            File.WriteAllBytes(Path.Combine(Path.Combine(path, "subDir2"),
                $"tmp{DateTime.Now.Ticks}.txt"), bytes);

            return path;
        }

        private string _createFolder(long sizeInBytes)
        {
            var path = Path.Combine(@"D:\Temp\Tests", DateTime.Now.Ticks.ToString());
            Directory.CreateDirectory(path);

            var bytes = new byte[sizeInBytes];
            File.WriteAllBytes(Path.Combine(path, $"tmp{DateTime.Now.Ticks}.txt"), bytes);

            return path;
        }
    }
}
