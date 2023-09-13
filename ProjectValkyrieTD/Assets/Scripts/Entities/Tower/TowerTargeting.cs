using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    [SerializeField] private Transform towerHead = default;
    [SerializeField] private TowerData towerData = default;
    [SerializeField] private bool rotateHead = default;

    private Enemy currentTarget = default;
    public Enemy CurrentTarget => currentTarget;

    private Vector2 aimDirection = default;
    private float targetAimAngle = default;

    private float rotateSpeed = 0.1f;

    //===========================================================================
    private void Update()
    {
        FindTarget();

        if (rotateHead)
        {
            RotateHead();
        }
        else
        {
            AimAtTarget();
        }
    }

    //===========================================================================
    private void FindTarget()
    {
        if (currentTarget)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) > towerData.Range)
                currentTarget = null;
            else if (currentTarget.gameObject.activeInHierarchy == false)
                currentTarget = null;
        }

        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, towerData.Range);
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.TryGetComponent(out Enemy enemy))
            {// is an enemy
                if (currentTarget == null)
                {
                    currentTarget = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < 
                        Vector3.Distance(transform.position, currentTarget.transform.position))
                    {
                        currentTarget = enemy;
                    }
                }
            }
        }
    }

    private void AimAtTarget()
    {
        if (currentTarget == null || currentTarget.gameObject.activeInHierarchy == false)
            return;

        aimDirection = (currentTarget.ShootingPosition.transform.position - towerHead.position).normalized;

        targetAimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        towerHead.eulerAngles = new Vector3(0.0f, 0.0f, targetAimAngle);
    }

    private void RotateHead()
    {
        towerHead.Rotate(0.0f, 0.0f, rotateSpeed);
    }
}