using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Transform currentTargetTransform;
    private Rigidbody2D enemy_rb2D;
    private Vector2 movingDirection;

    private float totalSpeedModifier = default;
    private float speed = default;

    //===========================================================================
    private void Awake()
    {
        enemy_rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GameObject coreTower = GameObject.FindGameObjectWithTag("CoreTower");
        if (coreTower == null)
            return;

        currentTargetTransform = GameObject.FindGameObjectWithTag("CoreTower").transform;

        totalSpeedModifier = 1.0f;
        speed = baseSpeed * totalSpeedModifier;
    }

    private void FixedUpdate()
    {
        if (currentTargetTransform == null)
            return;

        MoveTowardCurrentTarget();

        FlipSprite();
    }

    //===========================================================================
    private void MoveTowardCurrentTarget()
    {
        movingDirection = (currentTargetTransform.transform.position - transform.position).normalized;

        enemy_rb2D.velocity = movingDirection * speed;
    }

    private void FlipSprite()
    {
        if (currentTargetTransform.position.x < transform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    //===========================================================================
    public void SetMovementSpeed(float modifier)
    {
        totalSpeedModifier += modifier;

        speed = baseSpeed * totalSpeedModifier; 
    }
}