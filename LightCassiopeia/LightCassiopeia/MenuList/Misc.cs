using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace LightCassiopeia.MenuList
{
    public static class Misc
    {
        private static readonly Menu Menu;
        private static readonly CheckBox _smartW;
        private static readonly CheckBox _HealthbarEnabled;
        private static readonly Slider _predQ;

        public static int PredQ
        {
            get { return _predQ.CurrentValue; }
        }

        public static bool HealthbarEnabled
        {
            get { return _HealthbarEnabled.CurrentValue; }
        }

        static Misc()
        {
            Menu = DrawingMenu.Menu.AddSubMenu("Misc");
            _predQ = Menu.Add("predQ", new Slider("Q Prediction", 90, 1, 100));
            Menu.AddSeparator();
            Menu.AddLabel("Damage Indicator");
            _HealthbarEnabled = Menu.Add("Healthbar", new CheckBox("Healthbar", true));
        }

        public static void Initialize()
        {
        }
    }
}