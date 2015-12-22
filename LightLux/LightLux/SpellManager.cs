using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System;

namespace LightLux
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; set; }
        public static Spell.Active W { get; set; }
        public static Spell.Skillshot E { get; set; }
        public static Spell.Active E2 { get; set; }
        public static Spell.Skillshot R { get; set; }

        static SpellManager()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1300, SkillShotType.Linear, 500, 1200, 80);
            W = new Spell.Active(SpellSlot.W, 1200);
            E2 = new Spell.Active(SpellSlot.E, 350);
            E = new Spell.Skillshot(SpellSlot.E, 1100, SkillShotType.Linear, 500, 1300, 275);
            R = new Spell.Skillshot(SpellSlot.R, 3340, SkillShotType.Linear, 175, 3000, 190);
        }

        public static void Initialize()
        {
            E.AllowedCollisionCount = Int32.MaxValue;
            R.AllowedCollisionCount = Int32.MaxValue;
            Chat.Print("<< LightBlow >>");
        }
    }
}