﻿using MudBlazor;

namespace Dima.Web
{
    public static class Configuration
    {
        public static MudTheme Theme = new()
        {
            Typography = new Typography
            {
                Default = new Default
                {
                    FontFamily = ["Raleway", "sans-serif"]
                }
            },
            PaletteLight = new PaletteLight
            {
                Primary = new MudBlazor.Utilities.MudColor("#1EFA2D"),
                Secondary = Colors.LightGreen.Darken3,
                Background = Colors.Gray.Lighten4,
                AppbarBackground = new MudBlazor.Utilities.MudColor("#1EFA2D"),
                AppbarText = Colors.Shades.Black,
                TextPrimary = Colors.Shades.Black,
                PrimaryContrastText = Colors.Shades.Black,
                DrawerText = Colors.Shades.Black,
                DrawerBackground = Colors.LightGreen.Darken4,
            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.LightGreen.Accent3,
                Secondary = Colors.LightBlue.Darken3,
                AppbarBackground = Colors.LightGreen.Accent3,
                AppbarText = Colors.Shades.Black
            }
        };
    }
}