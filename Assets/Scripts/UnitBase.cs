using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class UnitBase : MonoBehaviour
{
    public UnitState state;

    [Header("Enemy Return To Team")]
    public Transform basicLocation;
    protected float arrivalDistance = 0.07f;
    [SerializeField] protected bool isReturning = false;
    [SerializeField] protected float moveSpeed = 3f;

    [Header("Battle")]
    [SerializeField] protected float enemyDetectionRadius = 5f;
    [SerializeField] private int enemyLayer;
    [SerializeField] protected Transform targetEnemy;

    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    void Update()
    {
        if (isReturning)
            ReturnTeamMove();
        if (targetEnemy == null) targetEnemy = EnemyDetector();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRadius);
    }

    Transform EnemyDetector()
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
            Debug.Log($"{enemies.Length}");
            state = UnitState.Individual;
            return GetClosetTarget(enemies);
        }
    }

    Transform GetClosetTarget(Collider2D[] colliders)
    {
        float closetDis = float.PositiveInfinity;
        Transform ClosetTarget = null;
        foreach (Collider2D collider in colliders)
        {
            if (collider == null) continue;
            Vector3 offset = transform.position - collider.transform.position;

            if (offset.sqrMagnitude < closetDis)
            {
                ClosetTarget = collider.transform;
                closetDis = offset.sqrMagnitude;
            }
        }
        return ClosetTarget;
    }
}
