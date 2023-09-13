using UnityEngine;

public class ATOneUtimateAbilityMine : MonoBehaviour
{
    // Delay Duration
    private float delayDuration = 1.0f;
    private float delayTimer = default;

    // Duration
    private float durationTimer = default;

    // Range
    private float range = default;

    // Damage
    private float damage = default;

    // Crit Chance & Crit Damage Modifier
    private float critChance = default;
    private float critDamageModifier = default;

    //===========================================================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            delayTimer = delayDuration;
        }
    }

    //===========================================================================
    private void Update()
    {
        durationTimer -= Time.deltaTime;
        if (durationTimer <= 0)
        {
            DealDamage();
        }

        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0)
            {
                DealDamage();
            }
        }
    }

    //===========================================================================
    private void DealDamage()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.CompareTag("Enemy"))
            {
                if (Random.value <= (critChance * 0.01f)) // Check if Crit
                {
                    if (collider2D.GetComponent<EnemyHealth>().UpdateCurrentHealth(-damage * (critDamageModifier * 0.01f)))
                    {
                        transform.parent.parent.parent.GetComponentInChildren<ATOnePassiveAbility>().IncreaseHavocStackAmount();

                        // Check if target in half range
                        if (Vector3.Distance(collider2D.transform.position, transform.parent.position) <
                            (GetComponentInParent<TowerData>().Range * 0.5f))
                        {
                            transform.parent.parent.parent.GetComponentInChildren<ATOnePassiveAbility>().IncreaseChaosStackAmount();
                        }
                    }
                }
                else
                {
                    if (collider2D.GetComponent<EnemyHealth>().UpdateCurrentHealth(-damage))
                    {
                        // Check if target in range
                        if (Vector3.Distance(collider2D.transform.position, transform.parent.position) <
                            (GetComponentInParent<TowerData>().Range * 0.5f))
                        {
                            transform.parent.parent.parent.GetComponentInChildren<ATOnePassiveAbility>().IncreaseChaosStackAmount();
                        }
                    }
                }
            }
        }

        gameObject.SetActive(false);
        transform.position = Vector3.zero;
    }

    //===========================================================================
    public void SetDurationTimer(float newDuration) { durationTimer = newDuration; }

    public void SetRange(float newRange) { range = newRange; }

    public void SetDamage(float newDamage) { damage = newDamage; }

    public void SetCritChance(float newChance) { critChance = newChance; }
    public void SetCritDamageModifier(float newModifier) { critDamageModifier = newModifier; }
}
