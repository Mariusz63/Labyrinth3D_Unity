using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Inventory
{
    public class ItemFactory
    {
        public static Item CreateItem(string name, string description, Sprite icon, int maxQuantity, int equippableItemIndex, UnityEvent myEvent, bool removeOneUse)
        {
            GameObject itemObject = new GameObject(name);
            Item itemComponent = itemObject.AddComponent<Item>();

            itemComponent.name = name;
            itemComponent.description = description;
            itemComponent.icon = icon;
            itemComponent.maxQuantity = maxQuantity;
            itemComponent.equippableItemIndex = equippableItemIndex;
            itemComponent.myEvent = myEvent;
            itemComponent.removeOneUse = removeOneUse;

            return itemComponent;
        }
    }
}
