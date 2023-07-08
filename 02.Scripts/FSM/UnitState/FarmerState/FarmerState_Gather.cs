using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerState_gathering : FarmerState
{
    public FarmerState_gathering(Farmer farmer) : base(farmer, Actor.State.GATHERING)
    {
    }

    public override void Enter()
    {
        base.Enter();
        farmer.gather.StartGathering();       
        farmer.transform.LookAt(farmer.gather.target.Position);
        farmer.m_Animator.SetBool("isAttack", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        farmer.gather.OnUpdate();

        if (farmer.gather.haveItem== true)
            farmer.fsm.SetState(Actor.State.IDLE);
    }

    public override void End()
    {
        base.End();
        farmer.m_Animator.SetBool("isAttack", false);        
    }
}