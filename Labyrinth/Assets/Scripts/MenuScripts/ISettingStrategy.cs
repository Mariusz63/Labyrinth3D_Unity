using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.MenuScripts
{
    // Interface for all setting strategies
    public interface ISettingStrategy
    {
        void LoadSetting();
    }
}
