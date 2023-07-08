using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unitstate나 command에서 타겟이 주어지면 
/// 공격을 수행하는 스크립트
/// </summary>
public abstract class UnitAttack : MonoBehaviour
{
    protected Unit unit;
    protected Actor target;
    protected float timer = 0.0f;
    protected float attackTime = 0.0f;
    protected float maxRange = 0.0f; //유닛 사거리
    protected float attValue = 0.0f;

    [SerializeField]
    protected List<string> audioClipKeys = new();

    public bool isAttackAble { get { return timer >= attackTime; } }
    public virtual void Init()
    {
        unit = GetComponent<Unit>();
        attackTime = unit.ActorInfo.atkTime;
        maxRange = unit.ActorInfo.maxRange;
        attValue = unit.ActorInfo.atk;
        timer = attackTime;        
    }
    private bool isTargetInRange(Actor target)
    {
        float distance = Actor.Distance(unit, target);
        Debug.Log($"{unit.ActorInfo.name} isTargetInRange : {distance <= maxRange}");
        return distance <= maxRange;
    }
    private bool isTargetDead(Actor target)
    {
        return target.fsm.GetHighType == Actor.HighState.DIE;
    }
    public virtual void OnUpdate()
    {
        if (timer < attackTime)
        {
            timer += Time.deltaTime;
            return;
        }
    }
    /// <summary>
    /// 타겟이 죽었거나 범위를 벗어나면 false를 반환한다.
    /// </summary>
    public bool AttackTarget(Actor target)
    {
        if (target == null)
            return false;
        if(isTargetDead(target) || isTargetInRange(target) == false)        
            return false;           

        if(isAttackAble)
        {
            timer = 0.0f;            
            Attack(target);
        }
        return true;
    }

    protected abstract void Attack(Actor target);
}
