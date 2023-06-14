using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitSkill : MonoBehaviour
{
    [Header("Skill Stat")]
    public int level;
    public int damage;
    public float curCoolDownDelay;
    public float maxCoolDownDuration;

    public IEnumerator SkillFunction(UnitBase unit)
    {
        unit.useSkill = true;
        
        yield return new WaitForEndOfFrame();

        unit.useSkill = false;
    }
}
