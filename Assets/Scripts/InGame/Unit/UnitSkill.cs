using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitSkill : MonoBehaviour
{
    [Header("Skill Stat")]
    public int level;
    public int damage;
    private float curCoolDownDelay;
    [SerializeField] private float maxCoolDownDuration;
    public bool canUse;

    public UnityEvent useSkill;

    protected UnitBase unit;

    protected virtual void Start()
    {
        useSkill.AddListener(SkillUsed);
        unit = GetComponentInParent<UnitBase>();
        curCoolDownDelay = maxCoolDownDuration;
    }

    protected virtual void Update()
    {
        curCoolDownDelay += Time.deltaTime;
        if (curCoolDownDelay >= maxCoolDownDuration)
        {
            canUse = true;
        }
    }

    protected virtual void SkillUsed()
    {
        canUse = false;
        curCoolDownDelay = 0f;
        
    }

    protected virtual IEnumerator SkillFunction()
    {
        yield return null;
    }
}
