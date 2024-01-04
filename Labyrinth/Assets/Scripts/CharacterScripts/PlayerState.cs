using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CharacterScripts
{
    public class PlayerNormalState : IPlayerState
    {
        private readonly FirstPersonController player;

        public PlayerNormalState(FirstPersonController player)
        {
            this.player = player;
        }

        public void EnterState()
        {
            player.CanMove = true;
            player.HandleMauseLook(true);
        }

        public void ExitState()
        {
        }

        public void Update()
        {
            // Handle other normal state updates if needed
        }
    }

    public class PlayerInventoryState : IPlayerState
    {
        private readonly FirstPersonController player;

        public PlayerInventoryState(FirstPersonController player)
        {
            this.player = player;
        }

        public void EnterState()
        {
            player.CanMove = false;
            player.HandleMauseLook(false);
        }

        public void ExitState()
        {
        }

        public void Update()
        {
            // Handle other inventory state updates if needed
        }
    }

}
