using EloBuddy;
using EloBuddy.SDK;
using System.Linq;

namespace LightCassiopeia.Carry
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            //KS with E
            if (MenuList.Misc.KsE)
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(en => en.Health < Damage.GetEDamage(en) - 20))
                {
                    E.Cast(enemy);
                }
        }
    }
}