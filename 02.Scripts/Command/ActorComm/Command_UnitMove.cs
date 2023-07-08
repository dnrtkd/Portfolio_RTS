using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 유닛들을 목표지점까지 이동시키는 Command
/// </summary>
public class Command_UnitMove : UnitCommand
{    
    Vector3 m_destination;    
    public Command_UnitMove(Unit _unit,Vector3 des):base(_unit)
    {        
        m_destination = des;
    }
    public override void Cancel()
    {
        base.Cancel();
        unit.fsm.SetState(Actor.HighState.LOOKOUT);
    }
    public override void Execute()
    {
        unit.fsm.SetState(Actor.HighState.NONE);
        unit.Destination = m_destination;
        unit.fsm.SetState(Actor.State.MOVE);
    }    
    public override void Update()
    {
        if (unit.fsm.GetStateType==Actor.State.IDLE)
            Cancel();
    }
}
