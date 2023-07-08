using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Trace : UnitState
{    
    public UnitState_Trace(Unit unit) : base(unit,Unit.State.TRACE)
    {
        m_owner = unit;

    }
    public override void Enter()
    {
        base.Enter();
        m_owner.m_unitMove.SetTarget(m_owner.Target.transform.position);
    }

    public override void End()
    {
        base.End();
        m_owner.m_unitMove.MoveStop();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (m_owner.Target == null )
        {
            m_owner.fsm.SetState(Actor.State.IDLE);
            return;
        }            
        var distance = Actor.Distance(m_owner, m_owner.Target);        
        if(distance>m_owner.ActorInfo.field  || m_owner.Target.fsm.GetHighType==Actor.HighState.DIE)
        {
            m_owner.fsm.SetState(Actor.State.IDLE);
            m_owner.Target = null;
            return;
        }
        m_owner.m_unitMove.SetTarget(m_owner.Target.transform.position);
    }
}
