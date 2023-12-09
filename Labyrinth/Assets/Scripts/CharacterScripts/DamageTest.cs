using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    [Header("Damage Parameter")]
    [SerializeField] private float damage = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            FirstPersonController.OnTakeDamage(damage);
    }
}
