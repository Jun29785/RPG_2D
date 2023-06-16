using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit_Poke : UnitSkill
{
    [SerializeField] private GameObject pokeAttack;
    [SerializeField] private int increaseValue;
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
        GameObject obj = UnitManager.Instance.UnitAttack(pokeAttack, unit.transform, unit.targetEnemy, 3.5f, damage, increaseValue, 0.4f);
        obj.TryGetComponent<PokeAttack>(out PokeAttack poke);
        poke.direction = Vector2.right;
        poke.unit = unit.transform;
        poke.target = unit.targetEnemy;
        Debug.Log("End");
    }

    protected override IEnumerator SkillFunction()
    {
        unit.useSkill = true;
        Instantiate(pokeAttack).TryGetComponent<UnitAttack>(out UnitAttack attack);
        attack.damage = damage;
        attack.direction = Vector2.up;

        attack.transform.parent = transform;
        attack.transform.position = (unit.transform.position + unit.targetEnemy.position) / 2;
        attack.transform.LookAt(unit.targetEnemy);
        while ((attack.transform.position - transform.position).sqrMagnitude < unit.attackRange * 2.5f/2)
        {
            // Move
            attack.transform.position = Vector2.MoveTowards(attack.transform.position, unit.targetEnemy.position, 0.5f);
            yield return null;
        }
        unit.useSkill = false;
    }
}
