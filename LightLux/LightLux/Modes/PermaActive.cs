﻿using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System.Linq;
using Color = System.Drawing.Color;

namespace LightLux.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            foreach (var enemy in ObjectManager.Get<AIHeroClient>().Where(t => t.IsEnemy).Where(t => Program._Player.GetAutoAttackRange() >= t.Distance(Program._Player)).Where(t => t.IsValidTarget()))
            {
                if (Damage.LuxPassive(enemy))
                {
                    Program.DrawLog("Focus:" + enemy.ChampionName + " luxpassive", Color.NavajoWhite);
                    Player.IssueOrder(GameObjectOrder.AttackUnit, enemy);
                    Orbwalker.DisableAttacking = true;
                    Orbwalker.DisableMovement = true;
                }
                else
                {
                    Orbwalker.DisableAttacking = false;
                    Orbwalker.DisableMovement = false;
                }
            }

            if (Program.EObject == null)
                return;
            foreach (var enemy in ObjectManager.Get<Obj_AI_Base>().Where(enemy => enemy.IsValidTarget() && enemy.IsEnemy && Vector3.Distance(Program.EObject.Position, enemy.Position) <= E.Width + 15))
            {
                Program.DrawLog("Wybucham E w " + enemy.Name + " bije za:" + Damage.EDamage(enemy), Color.MediumVioletRed);
                E2.Cast();
            }

            foreach (var ally in ObjectManager.Get<Obj_AI_Base>().Where(ally => ally.IsValidTarget() && ally.IsAlly && !ally.IsMinion && !ally.IsMonster && Vector3.Distance(Player.Instance.Position, ally.Position) <= W.Width))
            {
                if (ally.IsStunned || ally.IsCharmed || ally.IsFeared || ally.IsGhosted || !ally.IsZombie ||
                    !ally.IsInvulnerable || ally.HealthPercent > 20)
                {
                    W.Cast(ally);
                    Program.DrawLog("Pomagam tarcza w " + ally.Name, Color.MediumVioletRed);
                }
            }
        }
    }
}