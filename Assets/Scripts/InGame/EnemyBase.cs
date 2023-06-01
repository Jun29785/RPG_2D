using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBase : MonoBehaviour
{
    private IObjectPool<EnemyBase> enemypool;

    [Header("Movement")]
    [SerializeField] private float speed = 0.7f;
    private Transform targetUnit;
    private Vector2 direction;
    private bool isMove;

    [Header("Stat")]
    [SerializeField] [Range(0, 100000)] private int hp;
    [SerializeField] private int damage;
    [SerializeField] private float detectUnitRange = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float maxAttackDuration = 0.7f;
    private float curAttackDelay = 0f;

    void Start()
    {

    }

    void Update()
    {
        if (targetUnit == null) targetUnit = UnitDetector();
        if (isMove) Movement();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectUnitRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Movement()
    {
        if (Vector2.Distance(transform.position, targetUnit.position) <= attackRange)
        {
            // 이동 중지
            isMove = false;
        }
        else
        {
            float distance = speed * Time.deltaTime;

            if (Vector2.Distance(transform.position, targetUnit.position) < distance)
            {
                distance = Vector2.Distance(transform.position, targetUnit.position);
            }

            transform.Translate(direction * distance, Space.World);
        }
    }

    Transform UnitDetector()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectUnitRange);
        if (colliders.Length == 0)
        {
            return null;
        }
        else
        {
            return InGameManager.Instance.GetClosetTarget(colliders);
        }
    }



    public void SetPool(IObjectPool<EnemyBase> pool)
    {
        enemypool = pool;
    }
}
