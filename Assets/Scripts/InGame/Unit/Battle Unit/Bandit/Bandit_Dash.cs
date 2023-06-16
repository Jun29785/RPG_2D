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

    protected override void SkillUsed()
    {
        base.SkillUsed();
        StartCoroutine(SkillFunction());
    }

    protected override IEnumerator SkillFunction()
    {
        unit.useSkill = true;
        float speed = 10f;
        var distance = Vector2.Distance(unit.transform.position, unit.targetEnemy.position);

        #region Calculate
        var dis = Mathf.Pow(distance + 1.2f, 2);
        var x = unit.targetEnemy.position.x - unit.transform.position.x;
        var y = unit.targetEnemy.position.y - unit.transform.position.y;
        var X = dis * (Mathf.Pow(x, 2) / (Mathf.Pow(x, 2) + Mathf.Pow(y, 2)));
        var Y = dis * (Mathf.Pow(y, 2) / (Mathf.Pow(x, 2) + Mathf.Pow(y, 2)));
        X = x >= 0 ? Mathf.Sqrt(X) : -Mathf.Sqrt(X);
        Y = y >= 0 ? Mathf.Sqrt(Y) : -Mathf.Sqrt(Y);
        tempObj.localPosition = new Vector2(X, Y);
        Vector3 target = tempObj.position;
        Vector2 direction = (target - unit.transform.position).normalized;
        #endregion

        while (Vector2.Distance(unit.transform.position, target) > 0.2f )
        {
           transform.parent.Translate(direction * Time.deltaTime * speed);
            yield return null;
        }
        yield return null;
        unit.useSkill = false;
    }
}
