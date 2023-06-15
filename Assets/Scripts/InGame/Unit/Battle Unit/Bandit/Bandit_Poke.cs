using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit_Poke : UnitSkill
{
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
        yield return null;
        unit.useSkill = false;
    }
}
