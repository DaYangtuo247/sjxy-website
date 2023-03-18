using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DevelopServer
{
    class ModuleResolver : IModuleResolver
    {
        private string GetModuleFileBaseName(string name) =>
            name switch
            {
                "网站导航" => "common-nav",
                "xyxw" => "index-xyxw",
                "tzgg" => "index-tzgg",
                "sjgs" => "index-sqsj",
                "xslt" => "index-xslt",
                "服务专区" => "index-fwzq",
                "当前位置" => "current-position",
                "侧边导航" => "left-nav",
                "单篇正文" => "list-single-content",
                "静态翻页列表" => "list-list",
                "导师列表" => "teachers-list",
                "文章内容" => "post-content",
                _ => throw new InvalidOperationException($"Module {name} is not in the known module list.")
            };

        // newsCount是展示的新闻的条数，titleMaxLength是新闻标题的长度限制，都在后台可以设置
        (int newsCount, int titleMaxLength) GetIndexPageNewsColumnConfig(string module) =>
           module switch
           {
               "index-sqsj" => (6, 16),
               "index-tzgg" => (5, 18),
               "index-xyxw" => (5, 21),
               "index-xslt" => (6, 16),
               _ => throw new InvalidOperationException($"Module {module} is not a known index page news column mudlue.")
           };

        private Dictionary<string, IModuleGenerator> moduleFileNameGeneratorMap;

        private string moduleDirPath;

        public ModuleResolver(string modulePath)
        {
            this.moduleDirPath = modulePath;

            moduleFileNameGeneratorMap = new Dictionary<string, IModuleGenerator>
            {
                ["common-nav"] = new CommonNavGenerator(),
                ["index-fwzq"] = new IndexPageNewsColumnGenerator(),
                ["current-position"] = new CurrentPosGenerator(),
                ["left-nav"] = new LeftNavGenerator(),
                ["list-single-content"] = new ListSinglePageContentGenerator(),
                ["list-list"] = new ListPageListGenerator(),
                ["post-content"] = new PostPageContentGenerator(),
                ["teachers-list"] = new TeachersListGenerator()
            };

            foreach (var module in new string[] { "index-xyxw", "index-tzgg", "index-sqsj", "index-xslt" })
            {
                moduleFileNameGeneratorMap.Add(module, new IndexPageNewsColumnGenerator(GetIndexPageNewsColumnConfig(module)));
            }
        }

        private string Generate(string moduleName)
        {
            var fileBaseName = GetModuleFileBaseName(moduleName);
            var generator = moduleFileNameGeneratorMap.GetValueOrDefault(fileBaseName);
            if (generator == null)
            {
                throw new InvalidOperationException($"No generator for {fileBaseName}.");
            }

            var filePath = Path.Combine(moduleDirPath, fileBaseName + ".html");
            if (!File.Exists(filePath))
            {
                throw new InvalidOperationException($"Module file {filePath} not exist.");
            }

            return generator.Generate(File.ReadAllText(filePath));
        }

        private static readonly Regex elementReplaceRegex = new Regex(@"<!--#begineditable.*?name=""(.*?)"".*?<!--#endeditable-->", RegexOptions.Singleline);

        public string Substitute(string source)
        {
            return elementReplaceRegex.Replace(source, match =>
            {
                return Generate(match.Groups[1].Value);
            });
        }
    }
}
