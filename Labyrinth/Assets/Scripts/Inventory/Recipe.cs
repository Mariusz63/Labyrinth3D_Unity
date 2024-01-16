using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewRecipe", menuName ="Inventory/Recipe")]
public class Recipe : ScriptableObject
{
    public GameObject createdItemPrefab;
    public int quantityProduced = 1;
    public List<RequiredIngredients> requiredIngredients = new List<RequiredIngredients>(); //all of items to create an object
}

[System.Serializable]
public class RequiredIngredients
{
    public string itemName;
    public int requiredQuantity;
}