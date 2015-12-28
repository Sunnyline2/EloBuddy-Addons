using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Utils;
using LightCassiopeia.Carry;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using Color = System.Drawing.Color;

namespace LightCassiopeia
{
    internal class Program
    {
        private static List<ModeBase> Modes { get; set; }
        private const int BarWidth = 106;
        private const int LineThickness = 9;
        private static readonly Vector2 BarOffset = new Vector2(0, 0);

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            if (!Loading.IsLoadingComplete || Player.Instance.ChampionName != "Cassiopeia")
            {
                if (Player.Instance.ChampionName == "Cassiopeia")
                {
                    Chat.Print("LightCassiopeia - Error with loading!", Color.Red);
                    Chat.Print("LightCassiopeia - Please reload the addon!", Color.Red);
                }
                else
                {
                    Chat.Print("LightCassiopeia - You choose wrong champion!", Color.Red);
                }
                return;
            }
            Chat.Print("LightCassiopeia - Successfully loaded!", Color.ForestGreen);
            DrawingMenu.Initialize();
            Drawing.OnDraw += OnDraw;
            Game.OnTick += OnTick;
            Game.OnUpdate += OnTick;
            Drawing.OnEndScene += OnEndScene;
            GameObject.OnCreate += GameObject_OnCreate;
            Gapcloser.OnGapcloser += OnGapCloser;
            Interrupter.OnInterruptableSpell += OnPossibleToInterrupt;
            Modes = new List<ModeBase>();
            Modes.AddRange(new ModeBase[]
            {
                new PermaActive(),
                new Combo(),
                new Harras(),
                new LaneClear(),
                new LastHit(),
                new JungleClear()
            });
        }

        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var rengar = EntityManager.Heroes.Enemies.FirstOrDefault(a => a.Hero == Champion.Rengar);
            if (sender.Name == "Rengar_LeapSound.troy" && ObjectManager.Player.Distance(Player.Instance.Position) <= SpellManager.R.Range && rengar != null)
            {
                SpellManager.R.Cast(rengar);
            }
        }

        public static void OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender == null || sender.IsAlly)
                return;

            if ((sender.IsAttackingPlayer || e.End.Distance(Player.Instance.Position) <= 70))
            {
                SpellManager.R.Cast(sender);
            }
        }

        public static void OnPossibleToInterrupt(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs interruptableSpellEventArgs)
        {
            if (sender == null || sender.IsAlly)
                return;

            if ((sender.IsAttackingPlayer || sender.Distance(Player.Instance.Position) <= 70))
            {
                SpellManager.R.Cast(sender);
            }
        }

        private static void OnEndScene(EventArgs args)
        {
            foreach (var unit in EntityManager.Heroes.Enemies.Where(u => u.IsValidTarget() && u.IsHPBarRendered))
            {
                var damage = Damage.GetMaxSpellDamage(unit);
                if (damage <= 0)
                    continue;

                if (MenuList.Misc.HealthbarEnabled)
                {
                    var damagePercentage = ((unit.TotalShieldHealth() - damage) > 0
                        ? (unit.TotalShieldHealth() - damage)
                        : 0) /
                                           (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);
                    var currentHealthPercentage = unit.TotalShieldHealth() /
                                                  (unit.MaxHealth + unit.AllShield + unit.AttackShield +
                                                   unit.MagicShield);
                    var startPoint =
                        new Vector2((int)(unit.HPBarPosition.X + BarOffset.X + damagePercentage * BarWidth),
                            (int)(unit.HPBarPosition.Y + BarOffset.Y) - 5);
                    var endPoint =
                        new Vector2(
                            (int)(unit.HPBarPosition.X + BarOffset.X + currentHealthPercentage * BarWidth) + 1,
                            (int)(unit.HPBarPosition.Y + BarOffset.Y) - 5);
                    Drawing.DrawLine(startPoint, endPoint, LineThickness, Color.LimeGreen);
                }
            }
        }

        private static void OnDraw(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;
            if (MenuList.Drawing.DrawQ)
                Drawing.DrawCircle(Player.Instance.Position, SpellManager.Q.Range, Color.LimeGreen);
            if (MenuList.Drawing.DrawW)
                Drawing.DrawCircle(Player.Instance.Position, SpellManager.W.Range, Color.CornflowerBlue);
            if (MenuList.Drawing.DrawE)
                Drawing.DrawCircle(Player.Instance.Position, SpellManager.E.Range, Color.YellowGreen);
            if (MenuList.Drawing.DrawR)
                Drawing.DrawCircle(Player.Instance.Position, SpellManager.R.Range, Color.OrangeRed);
        }

        private static void OnTick(EventArgs args)
        {
            Modes.ForEach(mode =>
            {
                try
                {
                    if (mode.ShouldBeExecuted())
                    {
                        mode.Execute();
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, "Error executing mode '{0}'\n{1}", mode.GetType().Name, e);
                }
            });
        }
    }
}