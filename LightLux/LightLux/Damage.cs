using EloBuddy;
using EloBuddy.SDK;
using System.Drawing;

namespace LightLux
{
    internal class Damage
    {
        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, (new[] { 0, 60, 110, 160, 210, 260 }[SpellManager.Q.Level] + (Player.Instance.TotalMagicalDamage * 0.7f)));
        }

        public static float WDamage(Obj_AI_Base target)
        {
            return 0;
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, (new[] { 0, 60, 105, 150, 195, 240 }[SpellManager.E.Level] + (Program._Player.TotalMagicalDamage * 0.6f)));
        }

        public static float RDamage(Obj_AI_Base target)
        {
            var dmg = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, (new[] { 0, 300, 400, 500 }[SpellManager.R.Level] + (Program._Player.TotalMagicalDamage * 0.75f)));
            Program.DrawLog("R: " + int.Parse(dmg.ToString()), Color.Red);
            return dmg;
        }

        public static bool LuxPassive(Obj_AI_Base target)
        {
            if (target.HasBuff("LuxIlluminatingFraulein"))
            {
                return true;
            }
            return false;
        }

        public static bool LuxE(Obj_AI_Base target)
        {
            if (target.HasBuff(""))
            {
                return true;
            }
            return false;
        }
    }
}