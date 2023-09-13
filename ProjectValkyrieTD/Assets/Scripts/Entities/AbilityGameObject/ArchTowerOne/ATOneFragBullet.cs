using UnityEngine;

public class ATOneFragBullet : MonoBehaviour
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
    private int pierceTime = default;
    private int fragAmount = default;

    private Vector2 aimDirection = default;
    private float targetAimAngle = default;

    private Vector3 randomDirection = default;
    private Vector3 targetDirection = default;
    private Transform smallFragBulletPool = default;

    //===========================================================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (fragAmount == 0)
                pierceTime--;

            if (Random.value <= (critChance * 0.01f))
            {
                collision.GetComponent<EnemyHealth>().UpdateCurrentHealth(-damage * (critDamageModifier * 0.01f));
            }
            else
            {
                collision.GetComponent<EnemyHealth>().UpdateCurrentHealth(-damage);
            }

            if (fragAmount == 0 && pierceTime == 0)
            {
                gameObject.SetActive(false);
                transform.position = Vector3.zero;
            }
            else if (fragAmount != 0)
            {
                for (int i = 0; i < fragAmount; i++)
                {
                    CreateSmallFragBullet();
                }

                fragAmount = 0;

                gameObject.SetActive(false);
                transform.position = Vector3.zero;
            }
        }
    }

    //===========================================================================
    private void OnEnable()
    {
        if (currentTarget == null)
            return;

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
    private void CreateSmallFragBullet()
    {
        foreach (Transform bullet in smallFragBulletPool.transform)
        {
            if (bullet.gameObject.activeSelf == false)
            {
                ATOneDroneBullet _bullet = bullet.gameObject.GetComponent<ATOneDroneBullet>();

                randomDirection = CultyMarbleHelper.GetRamdomDirection();
                targetDirection = new Vector3(transform.position.x + randomDirection.x, transform.position.y + randomDirection.y);

                _bullet.SetShootingPoint(targetDirection);
                _bullet.SetCurrentTarget(transform);

                aimDirection = (targetDirection - transform.position).normalized;
                targetAimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                _bullet.SetRotationAngle(targetAimAngle);

                _bullet.SetBulletDamage(damage * 0.2f);

                _bullet.transform.position = transform.position;
                _bullet.gameObject.SetActive(true);

                return;
            }
        }
    }

    //===========================================================================
    public void SetSmallBulletPool(Transform newPool) { smallFragBulletPool = newPool; }

    public void SetShootingPoint(Transform newShootingPoint) { shootingPoint = newShootingPoint; }
    public void SetCurrentTarget(Transform newTarget) { currentTarget = newTarget; }

    public void SetBulletDamage(float newDamage) { damage = newDamage; }

    public void SetBulletCriticalChance(float newChance) { critChance = newChance; }
    public void SetBulletCriticalDamage(float newModifier) { critDamageModifier = newModifier; }

    public void SetPierceTime(int newAmount) { pierceTime = newAmount; }
    public void ConvertPierceTimeToFragAmount(int modifier) { fragAmount = pierceTime * modifier; }
}