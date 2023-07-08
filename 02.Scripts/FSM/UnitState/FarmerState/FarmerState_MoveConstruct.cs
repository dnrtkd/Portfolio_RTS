using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerState_MoveConstruct : FarmerHighState
{
    FarmerCtrl ctrl;
    public FarmerState_MoveConstruct(Farmer farmer) : base(farmer, Actor.HighState.MoveConstruct)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        farmer.Destination = farmer.builder.processBuilding.transform.position;
        farmer.fsm.SetState(Actor.State.MOVE);
        ctrl = farmer.GetComponent<FarmerCtrl>();
        ctrl.SetState(FarmerCtrl.FState.isBuilding);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if(farmer.fsm.GetStateType==Actor.State.IDLE)
        {
            farmer.fsm.SetState(Actor.HighState.NONE);
            return;
        }
        
        var distance = Actor.Distance
            (farmer.builder.processBuilding.GetComponent<Actor>(), farmer);

        if (distance <=  0.5f)
                farmer.fsm.SetState(Actor.State.CONSTRUCT);                
    }
    public override void End()
    {
        ctrl.SetState(FarmerCtrl.FState.Normal);
        base.End();
    }
}
