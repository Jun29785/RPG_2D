using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Define;

public abstract class UnitBase : MonoBehaviour
{
    public UnitState state;
    [Header("Test")]
    public bool VisibleTargetRange;
    public bool VisibleAttackRange;

    [Header("Enemy Return To Team")]
    public Transform basicLocation;
    protected float arrivalDistance = 0.07f;
    [SerializeField] protected bool isReturning = false;
    [SerializeField] protected float moveSpeed = 3f;

    [Header("Target Enemy")]
    [SerializeField] protected float enemyDetectionRadius = 5f;
    private int enemyLayer;
    public Transform targetEnemy;

    [Header("Unit Stat")]
    public int hp;
    public int damage;
    public float criticalProbability;
    public int criticalIncreaseValue;

    [Header("Battle")]
    [SerializeField] protected GameObject attackPrefab;
    [SerializeField] protected bool isBattle;
    public float attackRange = 1.5f;
    [SerializeField] protected float curAttackDelay = 0f;
    [SerializeField] protected float maxAttackDuration = 0.7f;
    [SerializeField] public bool useSkill = false;

    [Header("Skill")]
    [SerializeField] protected UnitSkill firstSkill;
    [SerializeField] protected UnitSkill secondSkill;
    [SerializeField] protected UnitSkill thirdSkill;

    [Header("Event")]
    public UnityEvent attackEvent;
    public UnityEvent<int,List<GameEffect>> getDamageEvent;

    protected virtual void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        EventInitialize();
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
        if (isBattle && !useSkill)
        {
            curAttackDelay += Time.deltaTime;
            if (curAttackDelay > maxAttackDuration)
            {
                curAttackDelay = 0f;
                attackEvent.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && !useSkill && firstSkill.canUse)
        {
            Debug.Log("Use FirstSkill");
            firstSkill.useSkill.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.W) && !useSkill && secondSkill.canUse)
        {
            Debug.Log("Use SecondSkill");
            secondSkill.useSkill.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E) && !useSkill && thirdSkill.canUse)
        {
            Debug.Log("Use ThirdSkill");
            thirdSkill.useSkill.Invoke();
        }
    }

    void EventInitialize()
    {
        attackEvent.AddListener(AttackFunc);
        getDamageEvent.AddListener(GetDamage);
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
                distance = Vector2.Distance(local, targetPosition);
            }

            transform.Translate(direction * distance, Space.Self);
        }
    }

    protected virtual void MoveToTarget()
    {
        Vector2 direction = (targetEnemy.position - transform.position).normalized;
        if (Vector2.Distance(targetEnemy.position, transform.position) > attackRange)
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
        int increase = 100;
        if (ProbabilityCalculator(criticalProbability))
        {
            increase = criticalIncreaseValue;
        }
        InGameManager.Instance.unitManager.UnitAttack(attackPrefab, transform, targetEnemy, 0, damage, increase, 0.3f);
    }

    protected bool ProbabilityCalculator(float probability)
    {
        if (probability > Random.Range(0f, 100f))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        if (VisibleTargetRange)
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawWireSphere(transform.position, enemyDetectionRadius);
        }
        if (VisibleAttackRange)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
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

    protected virtual void GetDamage(int damage,List<GameEffect> effects)
    {
        hp -= damage;
        if(hp <= 0)
        {
            // DIE
        }
    }
}
