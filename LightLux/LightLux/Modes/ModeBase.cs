using EloBuddy.SDK;

namespace LightLux.Modes
{
    public abstract class ModeBase
    {
        protected Spell.Skillshot E
        {
            get { return SpellManager.E; }
        }

        protected Spell.Active E2
        {
            get { return SpellManager.E2; }
        }

        // Change the spell type to whatever type you used in the SpellManager
        // here to have full features of that spells, if you don't need that,
        // just change it to Spell.SpellBase, this way it's dynamic with still
        // the most needed functions
        protected Spell.Skillshot Q
        {
            get { return SpellManager.Q; }
        }

        protected Spell.Skillshot R
        {
            get { return SpellManager.R; }
        }

        protected Spell.Skillshot W
        {
            get { return SpellManager.W; }
        }

        public abstract void Execute();

        public abstract bool ShouldBeExecuted();
    }
}