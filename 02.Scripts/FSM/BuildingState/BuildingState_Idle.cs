using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState_Idle : BuildingState
{
    public BuildingState_Idle(Building building) : base(building, Actor.State.IDLE)
    {

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (onwer.isComplete == false)
            return;

        //�ǹ��� ü���� 0.6 ���ϸ� Damaged���·�
        if((onwer.Hp / onwer.ActorInfo.maxHp)<0.6f )
        {
            onwer.fsm.SetState(Actor.State.DAMAGED);
            return;
        }       
    }
    public override void End()
    {
        base.End();
    }

    public override void Enter()
    {
        base.Enter();
    }

}
