using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Rogue.Wpf.Models.Interfaces;

namespace Rogue.Wpf.Models
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public class Theme
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public Dictionary<string, string> ThemeColors { get; set; }

        public Theme()
        {

        }

        protected Theme(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Hour");
            Author = info.GetString("Log");
            ThemeColors = (Dictionary<string, string>)info.GetValue("ThemeColors", typeof(Dictionary<string, string>));
        }
    }
}
