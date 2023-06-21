using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
public class Bandit_TriangleDash : UnitSkill
{
    [SerializeField] int skillTime;
    [SerializeField] bool isSkill;
    [SerializeField] float wait = .32f;

    [SerializeField] List<Transform> inRangeEnemies;
    private List<GameEffect> gameEffects;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isSkill)
            unit.transform.Translate(Vector2.up * 10f * Time.deltaTime);
    }

    protected override void SkillUsed()
    {
        base.SkillUsed();
        inRangeEnemies.Clear();
        TriangleMove();
    }

    void TriangleMove()
    {
        skillTime++;
        if (skillTime == 4)
        {
            isSkill = false;
            skillTime = 0;
            unit.useSkill = false;
            // Attack Enemies
            foreach (Transform rangeEnemy in inRangeEnemies)
            {
                rangeEnemy.TryGetComponent<EnemyBase>(out EnemyBase enemy);
                enemy.getDamage.Invoke(damage, gameEffects, AttackKind.Normal);
            }

            return;
        }
        else if (skillTime == 1)
        {
            unit.useSkill = true;
            float angle = Mathf.Atan2(unit.targetEnemy.position.y - unit.transform.position.y, unit.targetEnemy.position.x - unit.transform.position.x) * Mathf.Rad2Deg;
            unit.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            unit.transform.Rotate(Vector3.forward, -30f);
            isSkill = true;
        }
        else
        {
            unit.transform.Rotate(Vector3.forward, 120);
        }
        Invoke("TriangleMove", wait);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 1 << LayerMask.NameToLayer("Enemy"))
        {
            // ADD List
            var cnt = 0;
            foreach (Transform inRange in inRangeEnemies)
            {
                if (inRange == collision.transform)
                    cnt++;
            }
            if (cnt == 0)
            {
                inRangeEnemies.Add(collision.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
