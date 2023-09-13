using UnityEngine;

public class NTOneCoreAbilityBackhole : MonoBehaviour
{
    private float rotateSpeed = 0.25f;

    // CoolDown
    private readonly float baseCoolDown = 0.25f;
    private float cooldownCounter = default;

    // Damage
    private float damage = -10;

    // Utility
    private float range = 5;

    //===========================================================================
    private void Update()
    {
        RotateHead();

        cooldownCounter -= Time.deltaTime;
        if (cooldownCounter <= 0)
        {
            cooldownCounter += baseCoolDown;
            ApplyEffect();
        }
    }

    //===========================================================================
    private void ApplyEffect()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.CompareTag("Enemy"))
            {
                collider2D.GetComponent<EnemyHealth>().UpdateCurrentHealth(damage);

                StatusEffectSlow _slowEffect = collider2D.GetComponentInChildren<StatusEffectSlow>();
                _slowEffect.SetSlowEffect(50);
                _slowEffect.SetDuration();
            }
        }
    }

    private void RotateHead()
    {
        this.transform.Rotate(0.0f, 0.0f, rotateSpeed);
    }
}