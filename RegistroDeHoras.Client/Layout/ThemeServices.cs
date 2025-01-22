using MudBlazor;
namespace RegistroDeHoras.Client.Layout;
public class ThemeService
{
    public MudTheme DarkTheme = new MudTheme()
    {

        PaletteDark = new PaletteDark()
        {
            Primary = Colors.BlueGray.Darken3,
            Secondary = Colors.Orange.Accent3,
            Background = Colors.Gray.Darken4,
            Surface = Colors.Gray.Darken3,
            AppbarBackground = Colors.BlueGray.Darken4,
            AppbarText = Colors.Gray.Lighten3,
            DrawerBackground = Colors.Gray.Darken4,
            DrawerText = Colors.Gray.Lighten3,
            DrawerIcon = Colors.Orange.Accent3,
            TextPrimary = Colors.Gray.Lighten5,
            TextSecondary = Colors.Gray.Lighten3
        }
    };

    public MudTheme LightTheme = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = Colors.Indigo.Default,
            Secondary = Colors.Pink.Accent2,
            Background = Colors.Gray.Lighten5,
            Surface = Colors.LightBlue.Default,
            AppbarBackground = Colors.Indigo.Darken2,
            AppbarText = Colors.LightBlue.Default,
            DrawerBackground = Colors.Gray.Lighten4,
            DrawerText = Colors.BlueGray.Darken3,
            DrawerIcon = Colors.Indigo.Default,
            TextPrimary = Colors.BlueGray.Darken3,
            TextSecondary = Colors.Gray.Darken3
        }
    };

}


