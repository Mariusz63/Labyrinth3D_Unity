using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] private GameObject chestUIPrefab;
    [SerializeField] private Transform chestUIParent;

    [HideInInspector] public List<Slot> allChestSlots = new List<Slot>();
    [HideInInspector] public GameObject chestInstantiatedParent;

    //Loot tables
    [Header("Loot Tables")]
    [SerializeField] private bool randomLoot;
    [SerializeField] private LootTable lootTable;

    private void Start()
    {
        GameObject chestSlots = Instantiate(chestUIPrefab, chestUIParent.position, chestUIParent.rotation, chestUIParent);
        foreach (Transform childSlot in chestSlots.transform.GetChild(1))
        {
            Slot childSlotScript = childSlot.GetComponent<Slot>();
            allChestSlots.Add(childSlotScript);

            childSlotScript.InitialiseSlot();
        }

        chestInstantiatedParent = chestSlots;
        chestInstantiatedParent.SetActive(false);

        // Loot tables object
        if (randomLoot)
        {
            Debug.Log("Loot tables objects");
            lootTable.InitializeLootTable();
            lootTable.SpawnLoot(allChestSlots);

            // Add debug logs to check if loot is spawning
            foreach (Slot slot in allChestSlots)
            {
                if (slot.GetItem() != null)
                {
                    Debug.Log("Item spawned in slot: " + slot.name);
                }
            }
        }
    }
}
