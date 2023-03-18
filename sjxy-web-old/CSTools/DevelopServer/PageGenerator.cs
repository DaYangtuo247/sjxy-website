using System;
using System.IO;

namespace DevelopServer
{
    class PageGenerator
    {
        static public string Generate(string sourceDir, string sourceFileName, IModuleResolver moduleResolver)
        {
            var indexPath = Path.Combine(sourceDir, sourceFileName);
            if (!File.Exists(indexPath))
            {
                throw new InvalidOperationException($"Source file at {indexPath} not exist.");
            }

            return moduleResolver.Substitute(File.ReadAllText(indexPath));
        }
    }
}
