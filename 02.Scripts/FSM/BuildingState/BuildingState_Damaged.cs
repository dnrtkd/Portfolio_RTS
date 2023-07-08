using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState_Damaged : BuildingState
{    
    public BuildingState_Damaged(Building building) : base(building, Actor.State.DAMAGED)
    {

    }

    public override void Enter()
    {        
        base.Enter();

        onwer.Effect.EffectOn(Building_Effect.EffectType.Burnning);            
    }
    public override void End()
    {
        base.End();
        onwer.Effect.EffectOff(Building_Effect.EffectType.Burnning);        
    }
}
