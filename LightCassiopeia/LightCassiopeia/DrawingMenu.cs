using EloBuddy.SDK.Menu;
using LightCassiopeia.MenuList;

namespace LightCassiopeia
{
    public static class DrawingMenu
    {
        private const string MenuName = "LightCassiopeia";
        public static readonly Menu Menu;

        static DrawingMenu()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName);
            Menu.AddGroupLabel("Welcome to LightCassiopeia!");
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            static Modes()
            {
                //Menu drawing!
                Combo.Initialize();
                Harras.Initialize();
                Farm.Initialize();
                Drawing.Initialize();
                Misc.Initialize();
            }

            public static void Initialize()
            {
            }
        }
    }
}