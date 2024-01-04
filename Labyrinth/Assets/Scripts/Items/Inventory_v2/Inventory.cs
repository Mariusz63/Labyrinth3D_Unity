using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class Inventory : MonoBehaviour
{
    [Header("UI")]
    public GameObject inventory;
    private List<Slot> allInventorySlots = new List<Slot>(); //must be private
    public List<Slot> inventorySlots = new List<Slot>();
    public List<Slot> hotbarSlots = new List<Slot>();
    public Image crosshair;
    public TMP_Text itemHoverText;

    [Header("Raycast")]
    public float raycastDistance = 5f;
    public LayerMask itemLayer;
    public Transform dropLocation; //The location items will drooped from

    [Header("Drag and drop")]
    public Image dragIconImage;
    private Item currentDraggedItem;
    private int currentDragSlotIndex = -1;

    [Header("Inventory controls")]
    [SerializeField] private KeyCode itemTakeKey = KeyCode.E;
    [SerializeField] private KeyCode openInventoryKey = KeyCode.I;

    [Header("Equippable Items")]
    public List<GameObject> equippableItems = new List<GameObject>();
    public Transform selectedItemImage;

    [Header("Crafting")]
    public List<Recipe> itemRecipes = new List<Recipe>();

    public void Start()
    {
        ToggleInventory(false);

        //Add items to hotbarSlot at first, next to the inventory
        allInventorySlots.AddRange(hotbarSlots);
        allInventorySlots.AddRange(inventorySlots);

        foreach (Slot uiSlot in allInventorySlots)
        {
            uiSlot.InitialiseSlot();
        }
    }

    public void Update()
    {
        ItemRaycast(Input.GetMouseButtonDown(0));


        if (Input.GetKeyDown(openInventoryKey))
            ToggleInventory(!inventory.activeInHierarchy);

        if(inventory.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            DragInventoryIcon();
        }
        else if(currentDragSlotIndex != -1 && Input.GetMouseButtonUp(0) || currentDragSlotIndex != -1 && !inventory.activeInHierarchy)
        {
            DropInventoryIcon();
        }

        if (Input.GetKeyDown(KeyCode.Q)) // The button we press to drop items from the inventory
        {
            DropItem();
        }

        for(int i = 1;i<hotbarSlots.Count+1;i++)
        {
            if (Input.GetKeyDown(i.ToString()))//any input within the size range of hotbar
            {
                EnableHotbarItem(i-1);// because we start from 1
            }
        }

        dragIconImage.transform.position = Input.mousePosition;
    }

    private void DropItem()
    {
        for (int i = 0; i < allInventorySlots.Count; i++)
        {
            Slot curSlot = allInventorySlots[i];
            if (curSlot.hovered && curSlot.HasItem())
            {
                curSlot.GetItem().gameObject.SetActive(true);
                curSlot.GetItem().transform.position = dropLocation.position;
                curSlot.SetItem(null);
                break;
            }
        }
    }

    private void ItemRaycast(bool hasClicked = false)
    {
        Debug.Log("ItemRaycast started");

        itemHoverText.text = "";
        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, itemLayer))
        {
            if (hit.collider != null)
            {
                if (hasClicked) // Pick up
                {
                    Item newItem = hit.collider.GetComponent<Item>();
                    if (newItem)
                    {
                        AddItemToInventory(newItem);
                    }
                }
                else // Get the name
                {
                    Item newItem = hit.collider.GetComponent<Item>();

                    if (newItem)
                    {
                        itemHoverText.text = newItem.name;
                    }
                }
            }
        }
    }

    private void AddItemToInventory(Item itemToAdd)
    {
        Debug.Log("AddItemToInventory item: " + itemToAdd.name);
        int leftoverQuantity = itemToAdd.currentQuantity;
        Slot openSlot = null;
        for (int i = 0; i < allInventorySlots.Count; i++)
        {
            Item heldItem = allInventorySlots[i].GetItem();

            if (heldItem != null && itemToAdd.name == heldItem.name)
            {
                int freeSpaceInSlot = heldItem.maxQuantity - heldItem.currentQuantity;

                if (freeSpaceInSlot >= leftoverQuantity)
                {
                    heldItem.currentQuantity += leftoverQuantity;
                    Destroy(itemToAdd.gameObject);
                    allInventorySlots[i].UpdateData();
                    return;
                }
                else // Add as much as we can to the current slot
                {
                    heldItem.currentQuantity = heldItem.maxQuantity;
                    leftoverQuantity -= freeSpaceInSlot;
                }
            }
            else if (heldItem == null)
            {
                if (!openSlot)
                    openSlot = allInventorySlots[i];
            }

            allInventorySlots[i].UpdateData();
        }

        if (leftoverQuantity > 0 && openSlot)
        {
            openSlot.SetItem(itemToAdd);
            itemToAdd.currentQuantity = leftoverQuantity;
            itemToAdd.gameObject.SetActive(false);
        }
        else
        {
            itemToAdd.currentQuantity = leftoverQuantity;
        }
    }


    private void ToggleInventory(bool enable)
    {
        inventory.SetActive(enable);

        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;

        // Disable the rotation of the camera.
       // Camera.main.GetComponent<FirstPersonLook>().sensitivity = enable ? 0 : 2;
    }

    private void DragInventoryIcon()
    {
        for(int i=0; i< allInventorySlots.Count; i++)
        {
            Slot curSlot = allInventorySlots[i];
            if(curSlot.hovered && curSlot.HasItem())
            {
                currentDragSlotIndex = i; //Update  the current drag slot variable
                currentDraggedItem = curSlot.GetItem();// Get the item from the current slot
                dragIconImage.sprite = currentDraggedItem.icon;
                dragIconImage.color = new Color(1,1,1,1);// Make the follow mouse icon opaque (visible)

                curSlot.SetItem(null); // Remove the item from the slot we just picked up the list 
            }
        }
    }

    private void DropInventoryIcon()
    {
        //Reset our drag item variable
        dragIconImage.sprite = null;
        dragIconImage.color = new Color(1, 1, 1, 0);

        for (int i = 0; i < allInventorySlots.Count; i++)
        {
            Slot curSlot = allInventorySlots[i];
            if (curSlot.hovered)
            {
                if (curSlot.HasItem()) //Swap the item
                {
                    Item itemToSwap = curSlot.GetItem();

                    curSlot.SetItem(currentDraggedItem);

                    allInventorySlots[currentDragSlotIndex].SetItem(itemToSwap);

                    ResetDragVariables();
                    return;
                }
                else //Place item with no swap
                {
                    curSlot.SetItem(currentDraggedItem);
                    ResetDragVariables();
                    return;
                }
            }
        }

        //If we get to this point we dropped the item in an invalid location (or closed inventory)
        allInventorySlots[currentDragSlotIndex].SetItem(currentDraggedItem);
        ResetDragVariables();
    }

    private void ResetDragVariables()
    {
        currentDraggedItem = null;
        currentDragSlotIndex = -1;
    }

    private void EnableHotbarItem(int hotbarIndex)
    {
        foreach(GameObject a in equippableItems)
        {
            a.SetActive(false);
        }

        Slot hotbarSlot = hotbarSlots[hotbarIndex];
        selectedItemImage.transform.position = hotbarSlots[hotbarIndex].transform.position;

        if (hotbarSlot.HasItem())
        {
            if(hotbarSlot.GetItem().equippableItemIndex!= -1)
            {
                equippableItems[hotbarSlot.GetItem().equippableItemIndex].SetActive(true);
            }
        }
    }

    public void CraftItem(string itemName)
    {
        foreach(Recipe recipe in itemRecipes)
        {
            if(recipe.createdItemPrefab.GetComponent<Item>().name == itemName)
            {
                bool haveAllIngredients = true;
                for(int i = 0;i<recipe.requiredIngredients.Count;i++)
                {
                    if(haveAllIngredients)
                        haveAllIngredients= HaveIngredient(recipe.requiredIngredients[i].itemName, recipe.requiredIngredients[i].requiredQuantity);

                }

                if (haveAllIngredients)
                {
                    for (int i = 0; i < recipe.requiredIngredients.Count; i++)
                    {
                        RemoveIngredient(recipe.requiredIngredients[i].itemName, recipe.requiredIngredients[i].requiredQuantity);
                    }

                    GameObject craftedItem = Instantiate(recipe.createdItemPrefab,dropLocation.position, Quaternion.identity);
                    craftedItem.GetComponent<Item>().currentQuantity = recipe.quantityProduced;

                    AddItemToInventory(craftedItem.GetComponent<Item>());
                }
                break;
            }
        }
    }

    private void RemoveIngredient(string itemName, int requiredQuantity)
    {
        if (!HaveIngredient(itemName, requiredQuantity))
            return;

        int remainingQuantity = requiredQuantity;

        foreach(Slot slot in allInventorySlots)
        {
            Item item = slot.GetItem();

            if(item != null && item.name == itemName)
            {
                if(item.currentQuantity>= remainingQuantity)
                {
                    item.currentQuantity -= remainingQuantity;

                    if(item.currentQuantity == 0)
                    {
                        slot.SetItem(null);
                       // slot.UpdateData();
                    }
                    return;
                }
                else
                {
                    remainingQuantity -= item.currentQuantity;
                    slot.SetItem(null);
                }
            }
        }
    }

    private bool HaveIngredient(string itemName, int requiredQuantity)
    {

        int foundQuantity = 0;
        foreach(Slot curSlot in allInventorySlots)
        {
            if(curSlot.HasItem() && curSlot.GetItem().name == itemName)
            {
                foundQuantity += curSlot.GetItem().currentQuantity;

                if(foundQuantity >= requiredQuantity)
                {
                    return true;
                }
            
            }
        }

        return false;
    }
}
