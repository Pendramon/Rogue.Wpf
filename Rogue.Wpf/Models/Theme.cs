using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Rogue.Wpf.Models.Interfaces;

namespace Rogue.Wpf.Models
{
    public class Theme
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public Dictionary<string, string> ThemeColors { get; set; }

        public Theme()
        {

        }
    }
}
