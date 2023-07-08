using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitCommand : Command
{
    protected Unit unit;    
    public UnitCommand(Unit unit)
    {
        this.unit = unit;
    }
    public override void Execute()
    {
        //unit.fsm.SetCurrState(Actor.State.IDLE);
    }
}
