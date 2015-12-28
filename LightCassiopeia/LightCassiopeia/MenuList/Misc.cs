using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace LightCassiopeia.MenuList
{
    public static class Misc
    {
        private static readonly Menu Menu;
        private static readonly CheckBox _smartW;
        private static readonly CheckBox _ksE;
        private static readonly CheckBox _onlyPoisoned;
        private static readonly CheckBox _lasthitE;
        private static readonly CheckBox _HealthbarEnabled;
        private static readonly Slider _predQ;

        public static int PredQ
        {
            get { return _predQ.CurrentValue; }
        }

        public static bool KsE
        {
            get { return _ksE.CurrentValue; }
        }

        public static bool LasthitE
        {
            get { return _lasthitE.CurrentValue; }
        }

        public static bool Poisoned
        {
            get { return _onlyPoisoned.CurrentValue; }
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
            _ksE = Menu.Add("KS", new CheckBox("KS with E", true));
            _onlyPoisoned = Menu.Add("onlypoisoned", new CheckBox("Use E only if target is poisoned!", true));
            Menu.AddSeparator();
            Menu.AddLabel("Damage Indicator");
            _HealthbarEnabled = Menu.Add("Healthbar", new CheckBox("Healthbar", true));
            Menu.AddSeparator();
            Menu.AddLabel("Lasthiting");
            _lasthitE = Menu.Add("lasthite", new CheckBox("Last hit using E", true));
        }

        public static void Initialize()
        {
        }
    }
}