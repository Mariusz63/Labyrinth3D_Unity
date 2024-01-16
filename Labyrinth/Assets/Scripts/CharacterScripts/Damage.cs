using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Damage Parameter")]
    [SerializeField] private float damage = 30;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            transform.parent = other.transform;
            other.GetComponent<Enemy>().TakeDamage(damage, transform.root.gameObject);
        }
    }


}
