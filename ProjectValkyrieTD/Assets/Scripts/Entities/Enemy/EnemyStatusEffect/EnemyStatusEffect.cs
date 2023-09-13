using UnityEngine;

public abstract class EnemyStatusEffect : MonoBehaviour
{
    protected EnemyHealth enemyHealth = default;
    protected EnemyMovement enemyMovement = default;

    //======================================================================
    protected virtual void Awake()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    protected virtual void OnEnable()
    {
        enemyHealth.OnEnemyDespawn += EnemyHealth_OnEnemyDespawn;
    }

    protected virtual void OnDisable()
    {
        enemyHealth.OnEnemyDespawn -= EnemyHealth_OnEnemyDespawn;
    }

    //======================================================================
    private void EnemyHealth_OnEnemyDespawn(object sender, System.EventArgs e)
    {
        this.enabled = false;
    }
}