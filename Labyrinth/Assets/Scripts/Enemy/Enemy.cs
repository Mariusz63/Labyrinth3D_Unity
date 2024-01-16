using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] public float spiderDamage = 25f;
    [SerializeField] public Animator animator;
    [SerializeField] public Slider healthBar;
     GameObject player;

    [SerializeField] private List<ItemDrop> itemDrops = new List<ItemDrop>();
    // Reference to the player's Health script
    private HealthPresenter healthPresenter;

    private void Update()
    {
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage, GameObject player)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            animator.SetTrigger("die");
            GetComponent<BoxCollider>().enabled = false;

            foreach (ItemDrop item in itemDrops)
            {
                int quantityToDrop = Random.Range(item.minQuantityToDrop, item.maxQuantityToDrop);

                if (quantityToDrop == 0)
                {
                    return;
                }

                Item droppedItem = Instantiate(item.ItemToDrop, transform.position, Quaternion.identity).GetComponent<Item>();
                droppedItem.currentQuantity = quantityToDrop;

                player.GetComponent<Inventory>().AddItemToInventory(droppedItem);
                Destroy(gameObject);
            }

            DropItem(player);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Damage");
           // AudioManager.instance.Play("Damage");
            animator.SetTrigger("damage");
        }
    }

    public void DropItem(GameObject player)
    {
       
    }

    public void Attack()
    {
        healthPresenter?.Damage(spiderDamage);
    }

}

[System.Serializable]
public class ItemDrop
{
    public GameObject ItemToDrop;
    public int minQuantityToDrop = 1;
    public int maxQuantityToDrop = 2;
}
