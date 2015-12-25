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

        public const float hitchanceCombo = 90f;
        public const float hitchanceHarras = 90f;
        public static GameObject EObject;

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
            GameObject.OnCreate += OnCreateObject;
            GameObject.OnDelete += OnDeleteObject;
        }

        public static void DrawLog(string text, System.Drawing.Color kolor)
        {
            if (Config.Modes.Misc.Debug)
                Chat.Print(text, kolor);
        }

        private static void OnCreateObject(GameObject obj, EventArgs args)
        {
            if (obj != null && obj.Name.Contains("Lux_Base_E_mis.troy"))
            {
                //TODO: FIX
                if (obj.IsMe)
                {
                    EObject = obj;
                    Program.DrawLog(obj.Name + " created! Pos:" + obj.Position, System.Drawing.Color.YellowGreen);
                }
            }
        }

        private static void OnDeleteObject(GameObject obj, EventArgs args)
        {
            //TODO: FIX
            if (obj != null && obj.Name.Contains("Lux_Base_E_mis.troy"))
            {
                if (obj.IsMe)
                {
                    EObject = null;
                    Program.DrawLog(obj.Name + " removed! Pos:" + obj.Position, System.Drawing.Color.YellowGreen);
                }
            }
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