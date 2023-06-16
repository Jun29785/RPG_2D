using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit_Poke : UnitSkill
{
    [SerializeField] private GameObject pokeAttack;

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
        Instantiate(pokeAttack).TryGetComponent<UnitAttack>(out UnitAttack attack);

        yield return null;
        unit.useSkill = false;
    }
}
