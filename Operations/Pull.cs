using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using BookStackApiClient;
using VsCodeStack.Configuration;
using System.Diagnostics;
using VsCodeStack.BookStack;
using Spectre.Console;

namespace VsCodeStack.Operations
{
    public class Pull
    {
        private readonly BookStackClient _client;
        private readonly Config _config;

        private readonly string _path;
        public Pull(Config config, string path = "")
        {
            _config = config;
            _path = string.IsNullOrEmpty(path) ? Directory.GetCurrentDirectory() : path;
            _client = new BookStackClient(new Uri(config.ApiAddress), config.ApiToken, config.ApiSecret);
        }

        public async Task<bool> Run()
        {
            Backup();

            var book = await _client.ReadBookAsync(_config.BookId);
            AnsiConsole.ResetDecoration();

            var tree = await BuildTree(book);
            AnsiConsole.Write(tree);
            //go over each item in the book and store them in the filesystem
            foreach (var item in book.contents)
            {

            }

            return true;
        }

        private void SaveChapter(ChapterItem chapter)
        {
            //create nicely formatted json content from chapter record
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonText = JsonSerializer.Serialize(chapter, jsonOptions);


            var dir = Helpers.GetChapterFolder(_path, chapter);
            Directory.CreateDirectory(dir);

            var fileName = System.IO.Path.Combine(dir, "chapter.json");
            System.IO.File.WriteAllText(fileName, jsonText);
        }

        private void SavePage(PageItem page, string folder)
        {
            //create nicely formatted json content from chapter record
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonText = JsonSerializer.Serialize(page, jsonOptions);
            var fileName = System.IO.Path.Combine(_path, $"{page.name}.json");

        }

        //get current files in folder, move to backup folder
        //ignore backup folder
        private void Backup()
        {

            var backupFolder = System.IO.Path.Combine(_path, "backup");

            if (Directory.Exists(backupFolder))
            {
                Directory.Delete(backupFolder, true);

            }

            var files = System.IO.Directory.GetFiles(_path);

            Directory.CreateDirectory(backupFolder);

            foreach (var file in files)
            {
                var filename = Path.GetFileName(file);
                if (filename.StartsWith("."))
                {
                    Debug.WriteLine($"Skipping {filename}");
                    continue;
                }
                var fileName = System.IO.Path.GetFileName(file);
                var destFile = System.IO.Path.Combine(backupFolder, fileName);
                System.IO.File.Move(file, destFile);
            }

        }

        private async Task<Tree> BuildTree(ReadBookResult book)
        {
            // Create the tree
            var tree = new Tree($"{book.name}")
                .Style(Style.Parse("red"))
                .Guide(TreeGuide.Line);

            foreach (var item in book.contents)
            {
                bool ischapter = item.type == "chapter";
                var iconstr = ischapter ? $"[gold1]{Emoji.Known.ClosedBook}" : $"[dodgerblue1]{Emoji.Known.Newspaper}";
                var nodestr = $"{iconstr} - {item.name} [/]";
                var node = tree.AddNode(nodestr);

                if(ischapter){
                    var chapterpages = await _client.ReadChapterAsync(item.id);

                    foreach (var page in chapterpages.pages)
                    {
                        var pageicon = $"[dodgerblue1]{Emoji.Known.Newspaper}";
                        var pagenode = node.AddNode($"{pageicon} - {page.name} [/]");
                        node.AddNode($"{pageicon} - {page.name} [/]");
                    }
                    
                }
            }

            // Return the tree
            return tree;
        }

    }
}
