using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float speed = 0.7f;
    [SerializeField] [Range(0,100000)] private int hp;
    [SerializeField] private int damage;
    [SerializeField] private float detectUnitRange = 10f;
    [SerializeField] Transform targetUnit;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float maxAttackDuration = 0.7f;
    private float curAttackDelay = 0f;

    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectUnitRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Update()
    {
        
    }
}
