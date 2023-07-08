using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스타크래프트의 어택땅
/// </summary>
public class UnitState_Attack : UnitState
{
    Vector3 des;
    float m_Timer;    
    AttackType currType;

    enum AttackType
    {
        ready,
        Fire,
        end,
    }

    public UnitState_Attack(Unit unitm) : base(unitm,Unit.State.ATTACK)
    {
        
    }
    public override void Enter()
    {        
        base.Enter();
        //m_owner.m_unitMove.MoveStop();
        currType = AttackType.ready;
        //m_owner.m_Animator.SetBool("isAttack",true);
        //des = m_owner.m_unitMove.m_target;
        m_owner.Target = m_owner.Target;
        m_owner.m_agent.avoidancePriority--;

        m_owner.m_agent.enabled = false;
        m_owner.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
    }
    
    public override void End()
    {
        m_owner.m_agent.enabled = true;
        m_owner.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;

        m_owner.m_Animator.SetBool("isAttack", false);
        m_owner.m_agent.avoidancePriority++;
                
        base.End();        
    }
    private void UpdateState()
    {
        switch (currType)
        {
            case AttackType.ready:
                m_owner.transform.LookAt(m_owner.Target.Position);
                if (m_owner.m_unitAtk.isAttackAble)
                {
                    currType = AttackType.Fire;
                    m_owner.m_Animator.SetBool("isAttack", true);                    
                    m_owner.m_Animator.Play("Attack", 0, 0);
                }
                break;
            case AttackType.Fire:                
                if(m_owner.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.3)
                {
                    m_owner.m_unitAtk.AttackTarget(m_owner.Target);
                    currType = AttackType.end;                   
                }              
                break;
            case AttackType.end:
                if(m_owner.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99)
                {
                    m_owner.m_Animator.SetBool("isAttack", false);
                    currType = AttackType.ready;
                }
                break;
            default:
                break;
        }
    }
   
    public override void OnUpdate()
    {
        if (m_owner.Target == null)
        {
            m_owner.fsm.SetState(Actor.State.IDLE);
            return;
        }            
        if ( m_owner.Target.fsm.GetHighType == Actor.HighState.DIE || 
            Actor.Distance(m_owner.Target,m_owner)>m_owner.ActorInfo.maxRange)
        {
            m_owner.Target = null;
            m_owner.fsm.SetState(Actor.State.IDLE);
            return;
        }            
        UpdateState();       
    }
}
