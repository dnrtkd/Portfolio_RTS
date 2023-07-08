using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Die : UnitHighState
{
    //사라지는 시간
    float disapearTime = 0.0f;
    float Timer = 0.0f;
    

    public UnitState_Die(Unit unitm,float _disapear) : base(unitm, Unit.HighState.DIE)
    {
        disapearTime = _disapear;
    }
    public override void Enter()
    {
        base.Enter();
        m_owner.fsm.SetState(Actor.State.DieAction);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Timer > disapearTime)
        {
            m_owner.ReturnToPool();                                          
        }
        else
            Timer += Time.deltaTime;
    }    
}

public class UnitState_DieAciton : UnitState
{    
    public UnitState_DieAciton(Unit unitm) : base(unitm, Unit.State.DieAction)
    {        
    }
    public override void Enter()
    {
        base.Enter();
        m_owner.Close();
    }    
}
