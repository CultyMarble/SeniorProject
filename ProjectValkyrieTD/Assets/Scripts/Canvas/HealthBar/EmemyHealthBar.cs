using UnityEngine;

public class EmemyHealthBar : MonoBehaviour
{
    [SerializeField] private Transform enemyHealthBar;

    private EnemyHealth enemyHealth;

    //===========================================================================
    private void Awake()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();

        enemyHealth.OnHealthChanged += EnemyHealth_OnHealthChanged;
    }

    private void OnDestroy()
    {
        enemyHealth.OnHealthChanged -= EnemyHealth_OnHealthChanged;
    }

    //===========================================================================
    private void EnemyHealth_OnHealthChanged(object sender, EnemyHealth.OnHealthChangedEvenArgs e)
    {
        UpdateHealthBarScale(e.healthRatio);

        UpdateHealthBarVisibility(e.healthRatio);
    }

    private void UpdateHealthBarScale(float healthRatio)
    {
        enemyHealthBar.localScale = new Vector3(healthRatio, 1.0f, 1.0f);
    }

    private void UpdateHealthBarVisibility(float healthRatio)
    {
        if (healthRatio == 1.0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}