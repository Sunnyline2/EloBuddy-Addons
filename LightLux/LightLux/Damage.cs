using EloBuddy;
using EloBuddy.SDK;
using System.Drawing;

namespace LightLux
{
    internal class Damage
    {
        public static float QDamage(Obj_AI_Base target)
        {
            return DamageLibrary.GetSpellDamage(Player.Instance, target, SpellSlot.Q);
        }

        public static float WDamage(Obj_AI_Base target)
        {
            return 0f;
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return DamageLibrary.GetSpellDamage(Player.Instance, target, SpellSlot.E);
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return DamageLibrary.GetSpellDamage(Player.Instance, target, SpellSlot.R);
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