using Rogue.Wpf.Models.Interfaces;

namespace Rogue.Wpf.Models
{
    public class ThemeValidator : IThemeValidator
    {
        public bool Validate(Theme theme)
        {
            // Just a few basic requirements, no need to overcomplicate anything.
            if (theme == null)
                return false;

            if (string.IsNullOrWhiteSpace(theme.Name))
                return false;

            if (string.IsNullOrWhiteSpace(theme.Author))
                return false;

            if (theme.ThemeColors == null)
                return false;

            return true;
        }
    }
}
