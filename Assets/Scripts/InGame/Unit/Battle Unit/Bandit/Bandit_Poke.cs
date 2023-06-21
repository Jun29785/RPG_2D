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
        GameObject obj = InGameManager.Instance.unitManager.UnitAttack(pokeAttack, unit.transform, unit.targetEnemy, 3.5f, damage, increaseValue, 0.4f);
        obj.TryGetComponent<PokeAttack>(out PokeAttack poke);
        poke.direction = Vector2.right;
        poke.unit = unit.transform;
        poke.target = unit.targetEnemy;
        Debug.Log("End");
    }
}
