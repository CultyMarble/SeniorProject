using UnityEngine;

public class ATOneDroneBullet : MonoBehaviour
{
    private Vector3 shootingPoint = default;
    private Transform currentTarget = default;
    private float targetAimAngle = default;

    // Duration
    private readonly float duration = 3.0f;
    private float durationTimer = default;

    // Movement
    private Vector3 moveDirection = default;
    private readonly float moveSpeed = 5.0f;

    // Damage
    private float damage = default;

    //===========================================================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().UpdateCurrentHealth(-damage);

            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }

    //===========================================================================
    private void OnEnable()
    {
        if (currentTarget == null)
            return;

        durationTimer = duration;
        moveDirection = -(currentTarget.position - shootingPoint).normalized;
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
    public void SetShootingPoint(Vector3 newShootingPoint) { shootingPoint = newShootingPoint; }
    public void SetCurrentTarget(Transform newTarget) { currentTarget = newTarget; }

    public void SetRotationAngle(float newTargetAimAngle)
    {
        targetAimAngle = newTargetAimAngle;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, targetAimAngle);
    }

    public void SetBulletDamage(float newDamage) { damage = newDamage; }
}
