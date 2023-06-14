using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit_Dash : UnitSkill
{
    public Transform tempObj;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override IEnumerator SkillFunction()
    {
        unit.useSkill = true;
        Vector2 direction = (unit.targetEnemy.position - unit.transform.position).normalized;
        float curDelay = 0f; float speed = 10f;
        var distance = Vector2.Distance(unit.transform.position, unit.targetEnemy.position);

        #region Test
        var dis = Mathf.Pow(distance, 2);
        var x = Mathf.Pow(unit.targetEnemy.position.x - unit.transform.position.x, 2);
        var y = Mathf.Pow(unit.targetEnemy.position.y - unit.transform.position.y, 2);
        var X = dis * (x / (x + y));
        var Y = dis * (y / (x + y));
        tempObj.localPosition = new Vector2(X, Y);

        #endregion
        //while (curDelay < distance)
        //{
        //    curDelay += Time.deltaTime * Vector2.Distance(unit.transform.position, unit.targetEnemy.position);
        //    transform.parent.Translate(direction * Time.deltaTime * speed);
        //    yield return null;
        //}
        yield return null;
        unit.useSkill = false;
    }
}
