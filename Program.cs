//############ VsCodeStack ################
//A tool to quickly synchronize books from 
//bookstack to a local format in mardown
//format. To be later stored/managed with
//git version control.


using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using CommandLine;
using BookStackApiClient;
using VsCodeStack.Configuration;
using VsCodeStack.BookStack;
using CommandLine.Text;
using Spectre.Console;

namespace VsCodeStack;


internal class Program
{
    

    static async Task Main(string[] args)
    {
        var result = new Parser(c => c.HelpWriter = null).ParseArguments<InitOptions, PullOptions, PushOptions>(args);

        await result.MapResult(
                    (InitOptions opts) => RunInitOperation(opts),
                    (PushOptions opts) => RunPushOperation(opts),
                    (PullOptions opts) => RunPullOperation(opts),
                    errs => DisplayHelp(result)
                );
    }

    private static async Task<int> DisplayHelp(ParserResult<object> parserResult)
    {
        Console.WriteLine(HelpText.AutoBuild(parserResult, h =>
        {
            h.AdditionalNewLineAfterOption = false;
            h.Heading = "VsCodeStack 0.1 Alpha";
            h.Copyright = "Copyright (c) 2023 Polyform development";
            return h;
        }));
        return 1;
    }

    private static async Task<int> RunInitOperation(InitOptions opts)
    {
        if (opts.Debug)
        {
            Console.WriteLine("");
            Console.WriteLine("Operation: INIT");
            Console.WriteLine($"Host:   {opts.Host}");
            Console.WriteLine($"Token:  {opts.Token}");
            Console.WriteLine($"Secret: {opts.Secret}");
            Console.WriteLine($"Book:   {opts.Book}");
            Console.WriteLine($"Mode:   {opts.Mode}");
        }

        var slug = opts.Book.ToLower().StartsWith("https://") ? Helpers.UrlToSlug(opts.Book) : opts.Book;

        var config = new Config()
        {
            ApiAddress = opts.Host,
            ApiToken = opts.Token,
            ApiSecret = opts.Secret,
            Book = slug,
            Mode = opts.Mode
        };


        var reader = new StackReader(config);

        var bookid = await reader.FindBook(config.Book);

        config.BookId = bookid;

        Config.SaveConfig(opts.Config, config);

        if (opts.Debug)
        {
            Console.WriteLine($"Config file saved to {opts.Config}");
        }

        if (opts.RunPull)
        {

           
        }

        return 0;
    }

    private static async Task<int> RunPullOperation(PullOptions opts)
    {
        AnsiConsole.Clear();
        AnsiConsole.Render(new FigletText("VsCodeStack").Color(Color.Green));
        Console.WriteLine("Pulling book from BookStack...");
        var config = Config.Load(opts.Config);
        
        var pull = new Operations.Pull(config);

        var success = await pull.Run();

        var bookFile = "";
        if (success)
        {
            Console.WriteLine($"Book saved to {bookFile}");
            Console.WriteLine("Press any key to exit... ");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Error during pull!");
            Console.WriteLine("Please check the configuration file and try again.");
            Console.WriteLine("Press any key to exit... ");
            Console.ReadKey();
        }
        return 0;
    }

   

    private static async Task<int> RunPushOperation(PushOptions opts)
    {
        return 0;
    }
}
