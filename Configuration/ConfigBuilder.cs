using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsCodeStack.Configuration
{
    internal static class ConfigBuilder
    {
        internal static Config RunWizard()
        {
            var config = new Config();

            var instance = StringInput("Enter the Bookstack API address:");
            var apitoken = StringInput("Enter the Bookstack API token:");
            var apisecret = StringInput("Enter the Bookstack API secret:");

            var browsebook = BoolSelector("Browse for Bookstack book?");
            if (browsebook)
            {
                Console.WriteLine("Sorry, this feature is not yet implemented!\r\n");
            }
            

            var bookId = StringInput("Enter the book url:");
            var mode = StringInput("Enter the Bookstack book style ([M]arkdown/[H]tml):");
            bool selectionvalid = mode.ToLower().Equals("m") || mode.ToLower().Equals("h");
            while (!selectionvalid)
            {
                Console.WriteLine("Invalid selection. Please enter M or H.");
                mode = StringInput("Enter the Bookstack book style ([M]arkdown/[H]tml):");
                selectionvalid = mode.ToLower().Equals("m") || mode.ToLower().Equals("h");
            }

            var bookStyle = mode.ToLower().Equals("m") ? BookStyle.MarkDown.ToString() : BookStyle.Html.ToString();

            config.ApiAddress = instance;
            config.ApiToken = apitoken;
            config.ApiSecret = apisecret;
            config.Book = bookId;
            config.Mode = bookStyle;

            return config;
        }

        private static string StringInput(string title)
        {
            Console.WriteLine($"{title}");
            string input = Console.ReadLine();
            return input;
        }
        private static bool BoolSelector(string prompt)
        {
            var mode = StringInput($"{prompt} ([N]o/[Y]es):").ToLower().Trim();
            bool selectionvalid = mode.Equals("y") || mode.Equals("n");
            while (!selectionvalid)
            {
                Console.WriteLine("Invalid selection. Please enter Y or N.");
                mode = StringInput($"{prompt} ([N]o/[Y]es):").ToLower().Trim();
                selectionvalid = mode.Equals("y") || mode.Equals("n");
            }
            return mode.Equals("y");
        }
    }
}
