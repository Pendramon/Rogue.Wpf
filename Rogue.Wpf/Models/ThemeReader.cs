using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Rogue.Wpf.Models.Interfaces;

namespace Rogue.Wpf.Models
{
    public class ThemeReader : IThemeReader
    {
        public async Task<Theme> ReadThemeAsync(string themeFileFullPath, CancellationToken cancellationToken = default)
        {
            await using var stream = File.OpenRead(themeFileFullPath);
            return await JsonSerializer.DeserializeAsync<Theme>(stream, jsonTypeInfo: null, cancellationToken);
        }

        public async Task<IEnumerable<Theme>> GetAllCustomThemesAsync(string customThemesDirectory,
            CancellationToken cancellationToken = default)
        {
            var files = Directory.GetFiles(customThemesDirectory);
            var validThemes = new List<Theme>();
            foreach (var file in files)
            {
                if (!file.EndsWith(".json"))
                {
                    continue;
                }

                await using var fileStream = File.OpenRead(file);
                try
                {
                    validThemes.Add(await JsonSerializer.DeserializeAsync<Theme>(fileStream, jsonTypeInfo: null, cancellationToken));
                }
                catch
                {
                    // Not a valid theme.
                }
            }
            return validThemes;
        }

        public IEnumerable<Theme> GetAllDefaultThemes()
        {
            var resourceSet =
                Properties.DefaultThemes.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true);

            return resourceSet?.Cast<DictionaryEntry>()
                .Where(resourceEntry => resourceEntry.Value != null)
                .Select(resourceEntry => (byte[])resourceEntry.Value)
                .Select(resourceEntry => Encoding.UTF8.GetString(resourceEntry))
                .Select(resourceEntry => JsonSerializer.Deserialize<Theme>(resourceEntry));
        }
    }
}
