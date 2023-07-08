using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Move : UnitState
{    
    public UnitState_Move(Unit unit) : base(unit, Unit.State.MOVE)
    {
        
    }
    public override void Enter()
    {       
        base.Enter();        
        m_owner.m_unitMove.SetTarget(m_owner.Destination);
    }
    public override void End()
    {
        base.End();        
        m_owner.m_unitMove.MoveStop();        
    }
    public override void OnUpdate()
    {
        if (m_owner.m_agent.isStopped == true)
        {
            m_owner.fsm.SetState(Actor.State.IDLE);
            return;
        }            
        //m_owner.m_unitMove.SetTarget(m_owner.Destination);
        base.OnUpdate();        
    }
}
