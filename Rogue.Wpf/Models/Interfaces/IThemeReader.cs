using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rogue.Wpf.Models.Interfaces
{
    public interface IThemeReader
    {
        Task<Theme> ReadThemeAsync(string themeFileName, CancellationToken cancellationToken = default);

        Task<IEnumerable<Theme>> GetAllCustomThemesAsync(string customThemesDirectory,
            CancellationToken cancellationToken = default);

        IEnumerable<Theme> GetAllDefaultThemes();
    }
}
