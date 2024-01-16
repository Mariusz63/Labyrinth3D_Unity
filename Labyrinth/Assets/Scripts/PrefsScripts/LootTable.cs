using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ChestLootTable", menuName = "Inventory/ChestLootTable")]
public class LootTable : ScriptableObject
{
    [Serializable]
    public class LootItem
    {
        public GameObject itemPrefab;
        public int minSpawn;
        public int maxSpawn;
        [Range(0f, 100f)] public float spawnChance;
    }

    public List<LootItem> lootItems = new List<LootItem>();
    [Range(0, 100)] public int spawnChancePerSlot = 15;

    public void InitializeLootTable()
    {
        float totalSpawnChance = 0f;
        foreach (LootItem lootItem in lootItems)
        {
            totalSpawnChance += lootItem.spawnChance;
        }

        if (totalSpawnChance > 100f)
        {
            NormalizeSpawnChance();
        }
    }

    private void NormalizeSpawnChance()
    {
        float normalizationFactor = 100f / CalculateTotalSpawnChance();
        foreach (LootItem item in lootItems)
        {
            item.spawnChance *= normalizationFactor;
        }
    }

    private float CalculateTotalSpawnChance()
    {
        float totalSpawnChance = 0f;

        foreach (LootItem item in lootItems)
        {
            totalSpawnChance += item.spawnChance;
        }
        return totalSpawnChance;
    }

    public void SpawnLoot(List<Slot> allChestSlots)
    {
        foreach (Slot chestSlot in allChestSlots)
        {
            if (Random.Range(0f, 100f) <= spawnChancePerSlot)
            {
                SpawnRandomItem(chestSlot);
            }
        }
    }

    //private void SpawnRandomItem(Slot chestSlot)
    //{
    //    LootItem chosenItem = ChooseRandomItem();

    //    if (chosenItem != null && chosenItem.itemPrefab != null)
    //    {
    //        int spawnCount = Random.Range(chosenItem.minSpawn, chosenItem.maxSpawn + 1);

    //        GameObject spawnedItem = Instantiate(chosenItem.itemPrefab, Vector3.zero, Quaternion.identity);
    //        spawnedItem.SetActive(false);

    //        Item itemComponent = spawnedItem.GetComponent<Item>();
    //        if (itemComponent != null)
    //        {
    //            itemComponent.currentQuantity = spawnCount;
    //            chestSlot.SetItem(itemComponent);
    //            chestSlot.UpdateData();
    //        }
    //        else
    //        {
    //            Debug.LogError("Item component is missing on the spawned item.");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Chosen item or item prefab is null.");
    //    }

    //    Debug.Log("SpawnRandomItem executed successfully"); 
    //}

    private void SpawnRandomItem(Slot chestSlot)
    {
        LootItem chosenItem = ChooseRandomItem();

        if (chosenItem != null && chosenItem.itemPrefab != null)
        {
            int spawnCount = Random.Range(chosenItem.minSpawn, chosenItem.maxSpawn + 1);

            GameObject spawnedItem = Instantiate(chosenItem.itemPrefab, Vector3.zero, Quaternion.identity);
            spawnedItem.SetActive(false);

            Item itemComponent = spawnedItem.GetComponent<Item>();
            if (itemComponent != null)
            {
                itemComponent.currentQuantity = spawnCount;

                // Debug logs to identify the issue
                Debug.Log($"Spawned item: {chosenItem.itemPrefab.name}");
                Debug.Log($"SpawnCount: {spawnCount}");
                Debug.Log($"ItemComponent: {itemComponent}");

                // Debug logs to track the execution flow
                Debug.Log($"Before SetItem in Slot: {chestSlot.GetItem()}");

                chestSlot.SetItem(itemComponent);

                // Debug logs after SetItem in Slot
                Debug.Log($"After SetItem in Slot: {chestSlot.GetItem()}");

                chestSlot.UpdateData();
            }
            else
            {
                Debug.LogError("Item component is missing on the spawned item.");
            }
        }
        else
        {
            Debug.LogError($"Chosen item or item prefab is null. ChosenItem: {chosenItem}");
        }

        Debug.Log("SpawnRandomItem executed successfully");
    }



    private LootItem ChooseRandomItem()
    {
        float randomValue = Random.Range(0f, 100f);

        while (true)
        {
            float cumulativeValue = 0f;

            foreach (LootItem item in lootItems)
            {
                cumulativeValue += item.spawnChance;

                if (cumulativeValue >= randomValue)
                {
                    return item;
                }
            }

            // If no item is found, generate a new random value and try again
            randomValue = Random.Range(0f, 100f);
        }
    }

}
