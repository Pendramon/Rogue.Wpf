using Rogue.Wpf.Models.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Rogue.Wpf.Models
{
    public class ThemeReader : IThemeReader
    {
        private IFileSystem fileSystem { get; set; }
        private IThemeValidator themeValidator { get; set; }

        public ThemeReader(IFileSystem fileSystem, IThemeValidator themeValidator)
        {
            this.fileSystem = fileSystem;
            this.themeValidator = themeValidator;
        }

        public async Task<Theme> ReadThemeAsync(string themeFileFullPath, CancellationToken cancellationToken = default)
        {
            await using var stream = fileSystem.File.OpenRead(themeFileFullPath);
            return await JsonSerializer.DeserializeAsync<Theme>(stream, cancellationToken: cancellationToken);
        }
        // TODO: Refactor Theme Reader
        public async Task<IEnumerable<Theme>> GetAllCustomThemesAsync(string customThemesDirectory,
            CancellationToken cancellationToken = default)
        {
            if (!fileSystem.Directory.Exists(customThemesDirectory))
            {
                return Enumerable.Empty<Theme>();
            }

            var files = fileSystem.Directory.GetFiles(customThemesDirectory);
            var validThemes = new List<Theme>();
            foreach (var file in files)
            {
                if (!file.EndsWith(".json"))
                {
                    continue;
                }

                try
                {
                    var theme = await this.ReadThemeAsync(file, cancellationToken);
                    if (!this.themeValidator.Validate(theme))
                    {
                        continue;
                    }

                    validThemes.Add(theme);
                }
                catch (JsonException)
                {
                    // Not a valid theme.
                }
                catch
                {
                    // A problem occured while attempting to read the theme.
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
                .Select(resourceEntry => JsonSerializer.Deserialize<Theme>(resourceEntry))
                .Select(theme => { theme.IsDefault = true; return theme; });
        }
    }
}
