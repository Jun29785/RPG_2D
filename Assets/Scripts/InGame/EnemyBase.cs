using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBase : MonoBehaviour
{
    private IObjectPool<EnemyBase> enemypool;

    [Header("Movement")]
    [SerializeField] private float speed = 0.7f;
    [SerializeField] private Transform targetUnit;
    [SerializeField] private int unitLayer;

    [Header("Stat")]
    [SerializeField] [Range(0, 100000)] private int hp;
    [SerializeField] private int damage;
    [SerializeField] private float detectUnitRange = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float maxAttackDuration = 0.7f;
    private float curAttackDelay = 0f;

    void Start()
    {
        unitLayer = LayerMask.NameToLayer("Unit");
    }

    void Update()
    {
        if (targetUnit == null) targetUnit = UnitDetector();
        else
        {
            MoveToTarget();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectUnitRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    protected virtual void MoveToTarget()
    {
        Vector2 direction = (targetUnit.position - transform.position).normalized;
        if (Vector2.Distance(targetUnit.position, transform.position) > attackRange)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    Transform UnitDetector()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectUnitRange, 1 << unitLayer);
        if (colliders.Length == 0)
        {
            return null;
        }
        else
        {
            return InGameManager.Instance.GetClosetTarget(colliders,transform);
        }
    }

    public void SetPool(IObjectPool<EnemyBase> pool)
    {
        enemypool = pool;
    }
}
