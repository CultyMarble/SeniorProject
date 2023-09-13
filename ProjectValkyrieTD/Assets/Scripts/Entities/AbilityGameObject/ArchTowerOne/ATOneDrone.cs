using UnityEngine;

public class ATOneDrone : MonoBehaviour
{
    [SerializeField] private Transform parentTower = default;

    private Vector3 targetDirection = default;

    private readonly float moveDistanceMin = 2.0f;
    private readonly float moveDistanceMax = 4.0f;
    private float moveDistance = default;

    private Vector3 targetDestination = default;

    private readonly float waitTimeMin = 1.0f;
    private readonly float waitTimeMax = 2.0f;
    private float waitTime = default;

    private readonly float moveSpeed = 10.0f;

    private Vector2 aimDirection = default;
    private float targetAimAngle = default;

    // Ability CoolDown
    private readonly float coolDown = 0.33f;
    private float cooldownCounter = default;

    private float damage = 25.0f;

    // Pooling
    [SerializeField] private Transform shootingPoint = default;
    [SerializeField] private Transform bulletPool = default;

    //===========================================================================
    private void OnEnable()
    {
        cooldownCounter = coolDown;

        RandomTargetDestination();
    }

    private void Update()
    {
        UpdateAbility();

        UpdateWaitTime();

        MoveTowardsTargetDirection();

        AimAtTargetDestination();
    }

    //===========================================================================
    private void UpdateAbility()
    {
        cooldownCounter -= Time.deltaTime;
        if (cooldownCounter <= 0)
        {
            cooldownCounter += coolDown;

            TriggerAbility();
        }
    }

    private void TriggerAbility()
    {
        // Create Bullets
        foreach (Transform bullet in bulletPool.transform)
        {
            if (bullet.gameObject.activeSelf == false)
            {
                ATOneDroneBullet _bullet = bullet.gameObject.GetComponent<ATOneDroneBullet>();

                _bullet.SetShootingPoint(shootingPoint.position);
                _bullet.SetCurrentTarget(transform);
                _bullet.SetRotationAngle(targetAimAngle);

                _bullet.SetBulletDamage(damage);

                _bullet.transform.position = shootingPoint.position;
                _bullet.gameObject.SetActive(true);

                return;
            }
        }
    }

    private void UpdateWaitTime()
    {
        if (waitTime <= 0)
            return;

        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            RandomTargetDestination();
        }
    }

    private void RandomTargetDestination()
    {
        targetDirection = CultyMarbleHelper.GetRamdomDirection();
        moveDistance = Random.Range(moveDistanceMin, moveDistanceMax);
        targetDestination = new Vector3(parentTower.position.x + (targetDirection * moveDistance).x, parentTower.position.x + (targetDirection * moveDistance).y);

        waitTime = Random.Range(waitTimeMin, waitTimeMax);
    }

    private void MoveTowardsTargetDirection()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetDestination, moveSpeed * Time.deltaTime);
    }

    private void AimAtTargetDestination()
    {
        if (Vector3.Distance(targetDestination, transform.position) < 0.01)
            return;

        aimDirection = (targetDestination - transform.position).normalized;
        targetAimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0.0f, 0.0f, targetAimAngle);
    }
}