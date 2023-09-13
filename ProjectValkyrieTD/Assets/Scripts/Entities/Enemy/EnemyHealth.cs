using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealth : MonoBehaviour
{
    public struct OnHealthChangedEvenArgs { public float healthRatio; }
    public event EventHandler<OnHealthChangedEvenArgs> OnHealthChanged;

    public event EventHandler OnEnemyDespawn;

    public float MaxHealth => maxHealth;
    [SerializeField] private float maxHealth;

    public float CurrentHealth => currentHealth;
    private float currentHealth;

    //===========================================================================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CoreTower"))
        {
            UpdateCurrentHealth(-9999);

            TowerManager.Instance.UpdateArchTowerHealth(-1);
        }
    }

    //===========================================================================
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    //===========================================================================
    private void ResetParameters()
    {
        UpdateCurrentHealth(maxHealth);
    }

    private void DespawnHandler()
    {
        // Drop EXP
        ArchTower archTower = GameObject.FindGameObjectWithTag("CoreTower").GetComponent<ArchTower>();
        archTower.UpdateExp(GetComponent<Enemy>().ExpDrop);

        // Invoke Events
        OnEnemyDespawn?.Invoke(this, EventArgs.Empty);

        gameObject.SetActive(false);

        ResetParameters();
    }

    //===========================================================================
    public void SetMaxHealth(float newMaxHealth, bool setCurrentHealthToFull = true)
    {
        maxHealth = newMaxHealth;

        if (setCurrentHealthToFull)
        {
            UpdateCurrentHealth(maxHealth);
        }
    }

    public bool UpdateCurrentHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Invoke Events
        OnHealthChanged?.Invoke(this, new OnHealthChangedEvenArgs { healthRatio = currentHealth / maxHealth });

        if (currentHealth <= 0)
        {
            DespawnHandler();
            return true;
        }

        return false;
    }
}