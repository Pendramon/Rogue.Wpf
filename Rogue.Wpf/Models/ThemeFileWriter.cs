using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Rogue.Wpf.Models.Interfaces;

namespace Rogue.Wpf.Models
{
    public class ThemeFileWriter : IThemeFileWriter
    {
        public async Task WriteThemeAsync(Theme theme, string themeFileFullPath, CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(themeFileFullPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(themeFileFullPath));
            }
            await using var stream = File.Create(themeFileFullPath);
            await JsonSerializer.SerializeAsync<Theme>(stream, theme, jsonTypeInfo: null, cancellationToken);
            // This should write theme to file.
            // Will do unit tests as soon as i get the whole proof of concept established.
        }
    }
}
