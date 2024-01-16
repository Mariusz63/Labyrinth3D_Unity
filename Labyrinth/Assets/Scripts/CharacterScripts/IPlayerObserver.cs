using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CharacterScripts
{
    // Subject interface
    public interface IPlayerObserver
    {
        void UpdatePosition(Vector3 newPosition);
    }
}
