using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action HealthChanged;

    private const float minHealth = 0f;
    private const float maxHealth = 100f;
    private float currentHealth = 100f;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float MinHealth => minHealth;
    public float MaxHealth => maxHealth;

    public void Increment(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        UpdateHealth();
    }

    public void Decrement(float amount)
    {
        if(currentHealth <= 0)
        {
            KillPlayer();
        }

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        UpdateHealth();
    }

    public void AddHealth(float amount)
    {
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        UpdateHealth();
    }

    private void KillPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        print("DEAD");
    }

    public void Restore()
    {
        currentHealth = maxHealth;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        HealthChanged?.Invoke();
    }
}