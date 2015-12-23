using EloBuddy;
using EloBuddy.SDK;

namespace LightLux
{
    internal class Damage
    {
        public static float QDamage(Obj_AI_Base target)
        {
            if (SpellManager.Q.IsReady())
                return DamageLibrary.GetSpellDamage(Player.Instance, target, SpellSlot.Q);
            return 0f;
        }

        public static float WDamage(Obj_AI_Base target)
        {
            return 0f;
        }

        public static float EDamage(Obj_AI_Base target)
        {
            if (SpellManager.E.IsReady())
                return DamageLibrary.GetSpellDamage(Player.Instance, target, SpellSlot.E);
            return 0f;
        }

        public static float RDamage(Obj_AI_Base target)
        {
            if (LuxPassive(target))
            {
                return DamageLibrary.GetSpellDamage(Player.Instance, target, SpellSlot.R) + LuxPassiveDamage();
            }
            else
            {
                return DamageLibrary.GetSpellDamage(Player.Instance, target, SpellSlot.R);
            }
        }

        public static float LuxPassiveDamage()
        {
            return 10 + (8 * Program._Player.Level) + (Player.Instance.TotalMagicalDamage / 100) * 25f;
        }

        public static float IgniteDamage(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite);
        }

        public static bool LuxPassive(Obj_AI_Base target)
        {
            if (target.HasBuff("LuxIlluminatingFraulein"))
            {
                return true;
            }
            return false;
        }

        public static bool LuxE()
        {
            if (Player.HasBuff("LuxLightStrikeKugel"))
            {
                return true;
            }
            return false;
        }
    }
}