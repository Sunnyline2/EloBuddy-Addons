using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;

namespace LightLux
{
    public static class Program
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public const float hitchance = 80f;

        // Change this line to the champion you want to make the addon for,
        // watch out for the case being correct!
        public const string ChampName = "Lux";

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != ChampName)
            {
                Chat.Print("Wrong champion xDD");
                return;
            }
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
            Drawing.OnDraw += OnDraw;
        }

        private static void OnDraw(EventArgs args)
        {
            if (Program._Player.IsDead)
                return;
            if (Config.Modes.Draw.ShowQ)
            {
                Circle.Draw(Color.White, SpellManager.Q.Range, Player.Instance.Position);
            }
            if (Config.Modes.Draw.ShowE)
            {
                Circle.Draw(Color.Blue, SpellManager.E.Range, Player.Instance.Position);
            }

            if (Config.Modes.Draw.ShowR)
            {
                Circle.Draw(Color.Red, SpellManager.R.Range, Player.Instance.Position);
            }

            if (Config.Modes.Draw.ShowW)
            {
                Circle.Draw(Color.Green, SpellManager.W.Range, Player.Instance.Position);
            }
        }
    }
}