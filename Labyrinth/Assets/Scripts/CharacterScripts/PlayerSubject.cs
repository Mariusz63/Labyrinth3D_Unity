﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CharacterScripts
{
    // Concrete subject (Player)
    public class PlayerSubject : MonoBehaviour
    {
        private List<IPlayerObserver> observers = new List<IPlayerObserver>();

        public void RegisterObserver(IPlayerObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void UnregisterObserver(IPlayerObserver observer)
        {
            observers.Remove(observer);
        }

        private void LateUpdate()
        {
            NotifyObservers(transform.position);
        }

        private void NotifyObservers(Vector3 newPosition)
        {
            foreach (var observer in observers)
            {
                observer.UpdatePosition(newPosition);
            }
        }
    }
}