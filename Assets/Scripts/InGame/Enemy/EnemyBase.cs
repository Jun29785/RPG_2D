using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;
using Define;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected EnemyType enemyType;

    [Header("Test")]
    public bool VisibleTargetRange;
    public bool VisibleAttackRange;

    [Header("Movement")]
    [SerializeField] private float speed = 0.7f;
    [SerializeField] private Transform targetUnit;
    [SerializeField] private int unitLayer;

    [Header("Stat")]
    [SerializeField] [Range(0, 100000)] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] private float detectUnitRange = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float curAttackDelay = 0f;
    [SerializeField] private float maxAttackDuration = 0.7f;

    [Header("Game")]
    private bool isBattle;
    [SerializeField] protected GameObject attackObject;
    public UnityEvent<int, List<GameEffect>, AttackKind> getDamage;
    [SerializeField] private UnityEvent attackEvent;

    void Start()
    {
        detectUnitRange = int.MaxValue;
        unitLayer = LayerMask.NameToLayer("Unit");
        getDamage.AddListener(GetDamage);
        attackEvent.AddListener(AttackFunc);
    }

    void Update()
    {
        if (targetUnit == null) targetUnit = UnitDetector();
        else
        {
            MoveToTarget(); 
        }

        if (isBattle)
        {
            curAttackDelay += Time.deltaTime;
            if (maxAttackDuration < curAttackDelay)
            {
                curAttackDelay = 0f;
                attackEvent.Invoke();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (VisibleTargetRange)
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawWireSphere(transform.position, detectUnitRange);
        }
        if (VisibleAttackRange)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }

    protected virtual void MoveToTarget()
    {
        Vector2 direction = (targetUnit.position - transform.position).normalized;
        if (Vector2.Distance(targetUnit.position, transform.position) > attackRange)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            isBattle = true;
        }
    }

    Transform UnitDetector()
    {
        isBattle = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectUnitRange, 1 << unitLayer);
        if (colliders.Length == 0)
        {
            return null;
        }
        else
        {
            return InGameManager.Instance.GetClosetTarget(colliders, transform);
        }
    }

    private void GetDamage(int damage, List<GameEffect> gameEffect, AttackKind kind)
    {
        Debug.Log($"Get Damaged : {damage}");
        hp -= damage;

    }

    protected virtual void AttackFunc()
    {
        InGameManager.Instance.enemyManager.EnemyAttack(attackObject, transform, targetUnit, attackSpeed, damage, .3f);
    }
}