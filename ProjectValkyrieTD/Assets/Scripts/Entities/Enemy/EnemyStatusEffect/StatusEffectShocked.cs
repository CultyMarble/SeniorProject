using UnityEngine;

public class StatusEffectShocked : EnemyStatusEffect
{
    // Status Effect Config
    private readonly float baseDuration = 3.0f;
    private float duration = default;

    // Shocked Effect Config
    private readonly float baseShockEffectFactor = 1.0f;
    private float shockEffectFactor = default;

    private float shockEffect = default;
    public float ShockEffect => shockEffect;

    //===========================================================================
    private void Update()
    {
        DurationCheck();
    }

    //===========================================================================
    private void DurationCheck()
    {
        duration -= Time.deltaTime;

        if (duration > 0)
            return;

        RemoveResetEffect();
    }

    private void RemoveResetEffect()
    {
        this.enabled = false;

        shockEffect = default;
    }

    //===========================================================================
    public void SetShockedEffectivenessFactor(float modifier = 1.0f)
    {
        shockEffectFactor = baseShockEffectFactor * modifier;
    }

    public void SetShockEffect(float appliedDamage, float modifier)
    {
        float _damageMaxHealthRatio = appliedDamage / enemyHealth.MaxHealth;

        shockEffect = appliedDamage * (_damageMaxHealthRatio) * shockEffectFactor * modifier;
    }

    public void SetDuration(float modifier = 1.0f)
    {
        duration = baseDuration * modifier;
    }
}