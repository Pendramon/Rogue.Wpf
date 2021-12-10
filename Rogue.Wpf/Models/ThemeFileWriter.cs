using Rogue.Wpf.Models.Interfaces;
using System.IO.Abstractions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Rogue.Wpf.Models
{
    public class ThemeFileWriter : IThemeFileWriter
    {
        private IFileSystem fileSystem;

        public ThemeFileWriter(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public async Task WriteThemeAsync(Theme theme, string themeFileFullPath, CancellationToken cancellationToken = default)
        {
            if (!fileSystem.Directory.Exists(themeFileFullPath))
            {
                fileSystem.Directory.CreateDirectory(fileSystem.Path.GetDirectoryName(themeFileFullPath));
            }
            await using var stream = fileSystem.File.Create(themeFileFullPath);
            await JsonSerializer.SerializeAsync<Theme>(stream, theme, cancellationToken: cancellationToken);
            // This should write theme to file.
            // Will do unit tests as soon as i get the whole proof of concept established.
        }
    }
}
