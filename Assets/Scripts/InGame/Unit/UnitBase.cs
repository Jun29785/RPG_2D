using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public abstract class UnitBase : MonoBehaviour
{
    public UnitState state;

    [Header("Enemy Return To Team")]
    public Transform basicLocation;
    protected float arrivalDistance = 0.07f;
    [SerializeField] protected bool isReturning = false;
    [SerializeField] protected float moveSpeed = 3f;

    [Header("Target Enemy")]
    [SerializeField] protected float enemyDetectionRadius = 5f;
    [SerializeField] private int enemyLayer;
    [SerializeField] protected Transform targetEnemy;

    [Header("Unit Stat")]
    public int hp;
    public int damage;

    [Header("Battle")]
    [SerializeField] protected GameObject basicAttackPrefab;
    [SerializeField] protected bool isBattle;
    protected float attackRange = 1.5f;
    [SerializeField] protected float curAttackDelay = 0f;
    [SerializeField] protected float maxAttackDuration = 0.7f;
    [SerializeField] public bool useSkill = false;

    [Header("Skill")]
    protected UnitSkill firstSkill;
    protected UnitSkill secondSkill;
    protected UnitSkill thirdSkill;

    protected virtual void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    protected virtual void Update()
    {
        if (isReturning)
            ReturnTeamMove();
        if (targetEnemy == null)
        {
            isBattle = false;
            targetEnemy = EnemyDetector();
        }
        else // Move To Target
        {
            MoveToTarget();
        }
        if (isBattle)
        {
            curAttackDelay += Time.deltaTime;
            if (curAttackDelay > maxAttackDuration)
            {
                curAttackDelay = 0f;
                AttackFunc();
            }
        }
    }

    protected virtual void ReturnTeamMove()
    {
        Vector2 targetPosition = basicLocation.localPosition;
        Vector2 local = transform.localPosition;
        Vector3 direction = (targetPosition - local).normalized;

        if (Vector2.Distance(local, targetPosition) <= arrivalDistance)
        {
            isReturning = false;
            GetComponentInParent<BattleTeam>().changeState.Invoke();
            state = UnitState.Team;
        }
        else
        {
            float distance = moveSpeed * Time.deltaTime;

            if (Vector2.Distance(local, targetPosition) < distance)
            {
                distance = Vector2.Distance(local,targetPosition);
            }

            transform.Translate(direction * distance, Space.Self);
        }
    }

    protected virtual void MoveToTarget()
    {
        Vector2 direction = (targetEnemy.position - transform.position).normalized;
        if (Vector2.Distance(targetEnemy.position,transform.position) > attackRange)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            isBattle = true;
        }
    }

    protected virtual void AttackFunc()
    {
        UnitManager.Instance.UnitBasicAttack(basicAttackPrefab, transform, targetEnemy, 0, damage, maxAttackDuration/2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    protected Transform EnemyDetector()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, enemyDetectionRadius, 1 << enemyLayer);
        if (enemies.Length == 0) // Not Detected
        {
            isReturning = true;
            return null;
        }
        else
        {
            isReturning = false;
            state = UnitState.Individual;
            return InGameManager.Instance.GetClosetTarget(enemies, transform);
        }
    }

    protected virtual IEnumerator ActiveFirstSkill()
    {
        useSkill = true;
        yield return null;
        useSkill = false;
    }

    protected virtual IEnumerator ActiveSecondSkill()
    {
        useSkill = true;
        yield return null;
        useSkill = false;
    }

    protected virtual IEnumerator ActiveThirdSkill()
    {
        useSkill = true;
        yield return null;
        useSkill = false;
    }
}
