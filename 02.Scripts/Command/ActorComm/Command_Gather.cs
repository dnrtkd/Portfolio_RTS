using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command_Gather : Command
{
    Farmer farmer;
    public Command_Gather(Farmer farmer,Resource resource)
    {
        this.farmer = farmer; farmer.gather.target = resource;
    }
    public override void Execute()
    {
        farmer.fsm.SetState(Actor.HighState.MoveGather);
    }

    public override void Update()
    {
        if (farmer.fsm.GetHighType == Actor.HighState.NONE)                    
            isFinish = true;
        
    }
    public override void Cancel()
    {
        base.Cancel();
        farmer.fsm.SetState(Actor.HighState.NONE);
    }
}
