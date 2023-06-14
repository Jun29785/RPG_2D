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
    }

    protected virtual void Update()
    {
        curCoolDownDelay += Time.deltaTime;
        if (curCoolDownDelay >= maxCoolDownDuration)
        {
            canUse = true;
        }
    }

    void SkillUsed()
    {
        canUse = false;
        curCoolDownDelay = 0f;
        StartCoroutine(SkillFunction());
    }

    protected abstract IEnumerator SkillFunction();
}
