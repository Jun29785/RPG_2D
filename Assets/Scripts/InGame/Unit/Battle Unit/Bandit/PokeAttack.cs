using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeAttack : UnitAttack
{
    public Transform unit;
    public Transform target;

    private void Start()
    {
        Vector2 vectorToTarget = (target.position - unit.position);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        unit.GetComponent<UnitBase>().useSkill = false;
    }
}
