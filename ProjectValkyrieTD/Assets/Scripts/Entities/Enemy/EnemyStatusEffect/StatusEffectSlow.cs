using UnityEngine;

public class StatusEffectSlow : EnemyStatusEffect
{
    // Status Effect Config
    private readonly float baseDuration = 3.0f;
    private float duration = default;

    // Shocked Effect Config
    private readonly float baseSlowEffect = 0.3f;

    private float slowEffect = default;
    public float SlowEffect => slowEffect;

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

        enemyMovement.SetMovementSpeed(slowEffect);
        slowEffect = default;
    }

    //===========================================================================
    public void SetSlowEffect(float modifier)
    {
        enemyMovement.SetMovementSpeed(slowEffect);

        slowEffect = baseSlowEffect * (1.0f + (modifier * 0.01f));

        enemyMovement.SetMovementSpeed(-slowEffect);
    }

    public void SetDuration(float modifier = 1.0f)
    {
        duration = baseDuration * modifier;
    }
}