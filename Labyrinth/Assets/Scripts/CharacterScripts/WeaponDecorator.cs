using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CharacterScripts
{

    public abstract class WeaponDecorator : Weapon
    {
        protected Weapon _weapon;


        public WeaponDecorator(Weapon weapon)
        {
            _weapon = weapon;
        }


        public override float GetDamage()
        {
            return _weapon.GetDamage();
        }
    }
}
