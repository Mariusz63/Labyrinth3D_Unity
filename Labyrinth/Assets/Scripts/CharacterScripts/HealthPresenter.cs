using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] TextMeshProUGUI healthText;

    private void Start()
    {
        if (health != null)
        {
            health.HealthChanged += OnHealthChanged;
        }
        UpdateView();
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.HealthChanged -= OnHealthChanged;
        }
    }

    public void Damage(float amount)
    {
        health?.Decrement(amount);
    }

    public void Heal(float amount)
    {
        health?.Increment(amount);
    }

    public void Reset()
    {
        health?.Restore();
    }

    public void UpdateView()
    {
        if (health == null)
            return;

        if (healthText != null && health.MaxHealth != 0)
        {
            healthText.text = health.CurrentHealth.ToString();
        }
    }

    public void OnHealthChanged()
    {
        UpdateView();
    }
}