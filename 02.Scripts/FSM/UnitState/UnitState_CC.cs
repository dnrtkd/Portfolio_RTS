using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  CC_Type
{
    None,
    Airborne,
    Stern,
}

public class UnitState_CC : UnitHighState
{
    public UnitState_CC(Unit unit) : base(unit, Actor.HighState.CC)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (m_owner.currCC == CC_Type.Airborne)
            m_owner.fsm.SetState(Actor.State.Airborne);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (m_owner.fsm.GetStateType == Actor.State.IDLE)
            m_owner.fsm.SetState(Actor.HighState.LOOKOUT);
    }

    public override void End()
    {
        base.End();
    }
}

public class Unitstate_Airborne : UnitState
{
    float timeElapsed;
    public Unitstate_Airborne(Unit unitm) : base(unitm, Actor.State.Airborne)
    {
    }

    public override void Enter()
    {
        timeElapsed = 0.0f;
        base.Enter();        
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        timeElapsed += Time.deltaTime;
        Vector3 nextMove = m_owner.Position + Vector3.up * 5.0f * timeElapsed;
        Vector3 gravity = Vector3.zero;
        gravity += Physics.gravity * (timeElapsed * timeElapsed) / 2.0f;
        nextMove += gravity;        

        m_owner.Position = nextMove;
        if (m_owner.Position.y < 0.1f)
            m_owner.fsm.SetState(Actor.State.IDLE);
    }

    public override void End()
    {
        base.End();
    }
}


