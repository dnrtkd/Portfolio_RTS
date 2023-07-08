using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Idle : UnitState
{
    public UnitState_Idle(Unit unit):base(unit,Unit.State.IDLE)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        m_owner.m_unitMove.MoveStop();        
        m_owner.Target = null;       
    }

    public override void OnUpdate()
    {        
        
    }

    
}
