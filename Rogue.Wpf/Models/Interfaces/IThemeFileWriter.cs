using System.Threading;
using System.Threading.Tasks;

namespace Rogue.Wpf.Models.Interfaces
{
    public interface IThemeFileWriter
    {
        Task WriteThemeAsync(Theme theme, string themeFileName, CancellationToken cancellationToken = default);
    }
}
