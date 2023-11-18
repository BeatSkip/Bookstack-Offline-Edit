using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VsCodeStack.Configuration
{
    public class Config
    {
        public string ApiAddress { get; set; }
        public string ApiToken { get; set; }
        public string ApiSecret { get; set; }
        public string Book { get; set; }
        public long BookId { get; set; }
        public string Mode { get; set; }

        public static Config Load(string path)
        {
            string jsonString = File.ReadAllText(path);
            Config config = JsonSerializer.Deserialize<Config>(jsonString);
            return config;
        }

        public static void SaveConfig(string path, Config conf)
        {
            string jsonString = JsonSerializer.Serialize(conf);
            File.WriteAllText(path, jsonString);
        }
    }


    public enum BookStyle
    {
        MarkDown,
        Html
    }
}
