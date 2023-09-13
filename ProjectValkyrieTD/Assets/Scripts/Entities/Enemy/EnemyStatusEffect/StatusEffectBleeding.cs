using UnityEngine;

public class StatusEffectBleeding : EnemyStatusEffect
{
    // Status Effect Config
    private readonly float baseDuration = 3.0f;
    private readonly float baseTriggerInterval = 0.5f;

    private float duration = default;
    private float triggerInterval = default;
    private float triggerTimeCounter = default;
    private float damagePerTrigger = default;

    // Bleeding Damage Config
    private readonly float baseDamagePerStack = 3.0f;
    private float damagePerStack = default;
    private int stack = default;

    //===========================================================================
    private void Update()
    {
        DurationCheck();

        TriggerEffect();
    }

    //===========================================================================
    private void DurationCheck()
    {
        duration -= Time.deltaTime;

        if (duration > 0)
            return;

        RemoveResetEffect();
    }

    private void TriggerEffect()
    {
        triggerTimeCounter += Time.deltaTime;
        if (triggerTimeCounter >= triggerInterval)
        {
            triggerTimeCounter -= triggerInterval;

            damagePerTrigger = damagePerStack * stack;

            enemyHealth.UpdateCurrentHealth(-damagePerTrigger);
        }
    }

    private void RemoveResetEffect()
    {
        this.enabled = false;

        triggerInterval = default;
        damagePerStack = default;
        stack = default;
    }

    //===========================================================================
    public void AddStack(int amount)
    {
        stack += amount;
    }

    public void SetDamage(float modifier)
    {
        damagePerStack = baseDamagePerStack * modifier;
    }

    public void SetDuration(float modifier)
    {
        duration = baseDuration * modifier;
    }

    public void SetTriggerInterval(float modifier)
    {
        triggerInterval = baseTriggerInterval * modifier;
    }
}