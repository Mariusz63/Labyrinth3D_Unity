using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewRecipe", menuName ="Inventory/Recipe")]
public class Recipe : ScriptableObject
{
    public GameObject createdItemPrefab;
    public int quantityProduced = 1;
    public List<requiredIngredients> requiredIngredients = new List<requiredIngredients>(); //all of items to create an object
}

[System.Serializable]
public class requiredIngredients
{
    public string itemName;
    public int requiredQuantity;
}