using System.Collections.Generic;

namespace Rogue.Wpf.Models
{
    public class Theme
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public bool IsDefault { get; set; }
        public Dictionary<string, string> ThemeColors { get; set; }

        public Theme()
        {

        }
    }
}
