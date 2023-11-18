using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsCodeStack
{
    
        [Verb("init", HelpText = "initialize project in current folder")]
        public class InitOptions
        {
            [Option('h', "host", Required = true, HelpText = "The bookstack api endpoint")]
            public string Host { get; set; } = "";

            [Option('t', "token", Required = true, HelpText = "The bookstack api token")]
            public string Token { get; set; } = "";

            [Option('s', "secret", Required = true, HelpText = "The bookstack api secret")]
            public string Secret { get; set; } = "";

            [Option('b', "book", Required = true, HelpText = "The bookstack book id")]
            public string Book { get; set; } = "";

            [Option('m', "mode", Required = false, Default = "markdown", HelpText = "The bookstack book style")]
            public string Mode { get; set; } = "";

            [Option('p', "pull", Required = false, Default = false, HelpText = "Pull book from bookstack server")]
            public bool RunPull { get; set; } = false;

            [Option("debug", Required = false, Default = false, HelpText = "print debug information")]
            public bool Debug { get; set; } = false;

            [Option('c', "config", Required = false, Default = ".vscodestack.json", HelpText = "The configuration file")]
            public string Config { get; set; } = "";
        }

        [Verb("pull", HelpText = "Pull book from bookstack server")]
        public class PullOptions
        {
            //configuration file (default is .vscodestack.json)
            [Option('c', "config", Required = false, Default = ".vscodestack.json", HelpText = "The configuration file")]
            public string Config { get; set; } = "";

            //unsafe mode, do not ask for confirmation
            [Option('u', "unsafe", Required = false, Default = false, HelpText = "Do not ask for confirmation")]
            public bool Unsafe { get; set; } = false;

            [Option("debug", Required = false, Default = false, HelpText = "print debug information")]
            public bool Debug { get; set; } = false;
    }

        [Verb("push", HelpText = "Push current folder to Bookstack server")]
        public class PushOptions
        {
            //configuration file (default is .vscodestack.json)
            [Option('c', "config", Required = false, Default = ".vscodestack.json", HelpText = "The configuration file")]
            public string Config { get; set; } = "";

            //unsafe mode, do not ask for confirmation
            [Option('u', "unsafe", Required = false, Default = false, HelpText = "Do not ask for confirmation")]
            public bool Unsafe { get; set; } = false;

            [Option("debug", Required = false, Default = false, HelpText = "print debug information")]
            public bool Debug { get; set; } = false;
    }


        [Verb("help", HelpText = "print the help information")]
        public class HelpOptions
        {
            //clone options here
        }
    
}
