using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//상위 상태
public class UnitState_LookOut : UnitHighState
{
    readonly float period = 0.3f;
    public UnitState_LookOut(Unit unitm) : base(unitm, Actor.HighState.LOOKOUT)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        m_owner.fsm.SetState(Actor.State.IDLE);
        CorutineStart(period,FindNearTarget);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();        
        
        if (m_owner.Target == null)
        {
            m_owner.fsm.SetState(Actor.State.IDLE);
            return;
        }

        var distance = Actor.Distance(m_owner.Target, m_owner);
        if (distance <= m_owner.ActorInfo.maxRange && m_owner.fsm.GetStateType != Actor.State.ATTACK)
        {
            m_owner.fsm.SetState(Actor.State.ATTACK);
        }        
        if (distance > m_owner.ActorInfo.maxRange)
        {            
            m_owner.fsm.SetState(Actor.State.TRACE);
        }       
    }
    public override void End()
    {
        CorutineStop();
        base.End();        
    }
    void FindNearTarget()
    {
        if (m_owner.Target == null)
            m_owner.Target = GameScene.I.ActorMng.
            FindNearTarget(m_owner, m_owner.ActorInfo.field);
    }
}
