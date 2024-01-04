using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CharacterScripts
{
    public interface IPlayerState
    {
        void Update();
        void EnterState();
        void ExitState();
    }

}
