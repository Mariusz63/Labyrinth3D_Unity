using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assets.Scripts.MenuScripts
{
    public class PauseCommand : ICommand
    {
        private PauseMenu pauseMenu;

        public PauseCommand(PauseMenu menu)
        {
            pauseMenu = menu;
        }

        public void Execute()
        {
            pauseMenu.TogglePause();
        }
    }

}
