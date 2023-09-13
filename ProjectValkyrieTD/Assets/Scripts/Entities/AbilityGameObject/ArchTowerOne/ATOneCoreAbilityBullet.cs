using UnityEngine;

public class ATOneCoreAbilityBullet : MonoBehaviour
{
    private Transform shootingPoint = default;
    private Transform currentTarget = default;

    // Duration
    private readonly float duration = 3.0f;
    private float durationTimer = default;

    // Movement
    private Vector3 moveDirection = default;
    private readonly float moveSpeed = 5.0f;

    // Damage
    private float damage = default;
    private float critChance = default;
    private float critDamageModifier = default;

    // Utility
    int pierceTime = default;

    //===========================================================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            pierceTime--;

            if (Random.value <= (critChance * 0.01f))
            {
                collision.GetComponent<EnemyHealth>().UpdateCurrentHealth(-damage * (critDamageModifier * 0.01f));
            }
            else
            {
                collision.GetComponent<EnemyHealth>().UpdateCurrentHealth(-damage);
            }

            if (pierceTime == 0)
            {
                gameObject.SetActive(false);
                transform.position = Vector3.zero;
            }
        }
    }

    //===========================================================================
    private void OnEnable()
    {
        durationTimer = duration;
        moveDirection = -(currentTarget.position - shootingPoint.position).normalized;
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        durationTimer -= Time.deltaTime;
        if (durationTimer <= 0)
        {
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }

    //===========================================================================
    public void SetShootingPoint(Transform newShootingPoint) { shootingPoint = newShootingPoint; }
    public void SetCurrentTarget(Transform newTarget) { currentTarget = newTarget; }

    public void SetBulletDamage(float newDamage) { damage = newDamage; }

    public void SetBulletCriticalChance(float newChance) { critChance = newChance; }
    public void SetBulletCriticalDamage(float newModifier) { critDamageModifier = newModifier; }

    public void SetPieceTime(int newTime) { pierceTime = newTime; }
}