using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CharacterScripts
{
    public class Sword : WeaponDecorator
    {
        public Sword(Weapon weapon) : base(weapon)
        {
        }

        public override float GetDamage()
        {
            return base.GetDamage() + 4f;
        }
    }
}
