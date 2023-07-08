using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//하위 상태 
//건물을 짓는 행동
public class FarmerState_Construct : FarmerState
{
    public FarmerState_Construct(Farmer farmer) : base(farmer, Actor.State.CONSTRUCT)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        farmer.m_Animator.SetBool("isAttack", true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if(farmer.builder.processBuilding.Complete == false)
            farmer.builder.Build();
        else
        {
            farmer.builder.processBuilding.OwnerBuilding.isComplete = true;
            farmer.fsm.SetState(Actor.State.IDLE);
            return;
        }
    }
    public override void End()
    {
        base.End();
        farmer.m_Animator.SetBool("isAttack", false);
        //farmer.fsm.SetState(Actor.State.IDLE);
    }
}
