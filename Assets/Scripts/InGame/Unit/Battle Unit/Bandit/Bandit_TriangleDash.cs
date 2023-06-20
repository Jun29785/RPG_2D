using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit_TriangleDash : UnitSkill
{
    [SerializeField] int skillTime;
    [SerializeField] bool isSkill;
    [SerializeField] float wait = .32f;
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
}
